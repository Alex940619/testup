using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm19_DocAdiconales : System.Web.UI.Page
    {
        #region "Eventos"

        private Hashtable _extensionesValidas;
        Collection<AvailableFileType> _tiposArchivos;

        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                ///*Solo para pruebas*/
                //Session["usrRedId"] = "U.Admision";
                //Session["ModPostulacion"] = 49;
                //Session["AplicanteId"] = 111689;

                this.ConfigurarTiposArchivos();
                if (!IsPostBack)
                {
                    this.ObtenerTextoInformativo(UIConvertNull.Int32(Session["AplicanteId"]), 16);
                    this.LLenarDatosPersona();
                    this.LLenarGrillaDocumentos();
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F19, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        protected void gvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drw = (DataRowView)e.Row.DataItem;
                    DataRow dr = drw.Row;
                    e.Row.Cells[0].Text = ((this.gvDocumentos.PageIndex * this.gvDocumentos.PageSize) + (e.Row.RowIndex + 1)).ToString();

                    Label _lblIdDocumento = (Label)e.Row.FindControl("lblIdDocumento");
                    Label _lblDescEstado = (Label)e.Row.FindControl("lblDescEstado");
                    HyperLink _hlkDocumento = (HyperLink)e.Row.FindControl("hlkDocumento");
                    ImageButton _imbCargarDoc = (ImageButton)e.Row.FindControl("imbCargarDoc");
                    ImageButton _imbDescargaDoc = (ImageButton)e.Row.FindControl("imbDescargaDoc");
                    Label _lblDescripcion = (Label)e.Row.FindControl("Label4");
                    Label _lblObservacion = (Label)e.Row.FindControl("Label5");
                    Label _lblEstado = (Label)e.Row.FindControl("lblEstado");
                    Image _imgEstado = (Image)e.Row.FindControl("imgEstado");

                    if (UIConvertNull.String(dr["LinkDoc"]) != null)
                    {
                        _hlkDocumento.NavigateUrl = UIConvertNull.String(dr["LinkDoc"]);
                        _hlkDocumento.Target = "_blank";
                        _hlkDocumento.Enabled = true;
                    }

                    if (UIConvertNull.String(dr["Estado"]) != null)
                    {
                        _lblDescEstado.Text = UIConvertNull.String(dr["Estado"]).ToUpper();
                        _lblEstado.Text = UIConvertNull.String(dr["Estado"]);

                        if (UIConvertNull.String(dr["Estado"]) == UIConstantes.EstadoDocumento.strEnRevision)
                        {
                            _imgEstado.ImageUrl = "../Images/icoEnRevision.png";
                        }
                        if (UIConvertNull.String(dr["Estado"]) == UIConstantes.EstadoDocumento.strObservado)
                        {
                            _imgEstado.ImageUrl = "../Images/icoObservado.png";
                        }
                        if (UIConvertNull.String(dr["Estado"]) == UIConstantes.EstadoDocumento.strAprobado)
                        {
                            _imgEstado.ImageUrl = "../Images/icoAprobado.png";
                        }
                    }

                    if (UIConvertNull.String(dr["Observacion"]) != "()")
                    {
                        _lblDescripcion.Text = UIConvertNull.String(dr["Descripcion"]).ToString();
                        _lblObservacion.Text = UIConvertNull.String(dr["Observacion"]).ToString();
                        _lblObservacion.Visible = true;
                    }
                    else
                    {
                        _lblDescripcion.Text = UIConvertNull.String(dr["Descripcion"]).ToString();
                        _lblObservacion.Visible = false;
                    }

                    switch (_lblDescEstado.Text)
                    {
                        case "PENDIENTE":
                            _imbCargarDoc.Enabled = true;
                            _imbCargarDoc.Visible = true;
                            _imbDescargaDoc.Enabled = false;
                            _imbDescargaDoc.Visible = false;
                            break;
                        case "EN REVISIÓN":
                            _imbCargarDoc.Enabled = false;
                            _imbCargarDoc.Visible = false;
                            _imbDescargaDoc.Enabled = true;
                            _imbDescargaDoc.Visible = true;
                            break;
                        case "OBSERVADO":
                            _imbCargarDoc.Enabled = true;
                            _imbCargarDoc.Visible = true;
                            _imbDescargaDoc.Enabled = false;
                            _imbDescargaDoc.Visible = false;
                            break;
                        case "APROBADO":
                            _imbCargarDoc.Enabled = false;
                            _imbCargarDoc.Visible = false;
                            _imbDescargaDoc.Enabled = true;
                            _imbDescargaDoc.Visible = true;
                            break;
                    }
                    String strCadena = UIConstantes._valorCadenaVacia;
                    strCadena = strCadena + UIHelper.AsignarDatoControlHtml(this.hCodDocumento.ClientID, UIConvertNull.String(dr["CodDoc"]));
                    strCadena = strCadena + UIHelper.AsignarDatoControlHtml(this.hIdDocumento.ClientID, UIConvertNull.String(dr["ApplicationAttachmentId"]));
                    UIHelper.SeleccionarItemGrillaOnClickMoverRaton(e, strCadena);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void imbCargarDoc_OnClick(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            ImageButton imbCargarDoc = (ImageButton)(sender);
            GridViewRow gvrow = (GridViewRow)imbCargarDoc.NamingContainer;
            try
            {
                this.lblError.Text = null;
                this.mpeCargaDocumentos.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.upCargaDocumentos.Update();
            }
        }

        protected void btnAdjuntar_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            AplicanteBE oAplicanteBE = null;
            DocumentoBE oDocumentoBE = null;
            DocumentoBL oDocumentoBL = null;
            try
            {
                this.lblError.Visible = false;
                String TempDocumentoId = hIdDocumento.Value.ToString();
                Int32 idDocumento = 0;
                Int32.TryParse(TempDocumentoId, out idDocumento);

                if (this.fuDocumento.HasFile && Session["AplicanteId"] != null)
                {
                    oDocumentoBE = new DocumentoBE();
                    oDocumentoBE.IdDocumento = UIConvertNull.Int32(hIdDocumento.Value);
                    oDocumentoBE.IdAplicacion = UIConvertNull.Int32(Session["AplicanteId"]);
                    oDocumentoBE.TituloDocumento = hCodDocumento.Value;
                    oDocumentoBE.Extension = ObtenerExtensionArchivo(fuDocumento);
                    oDocumentoBE.ContenidoDocumento = fuDocumento.FileBytes;
                    oDocumentoBE.IdTipoMedio = ObtenerTipoArchivo(oDocumentoBE.Extension);
                    oDocumentoBE.TipoMedio = fuDocumento.PostedFile.ContentType;
                    oDocumentoBE.UsuarioActualiza = Session["usrRedId"].ToString();
                    oDocumentoBE.Estado = 2;

                    if (oDocumentoBE.IdTipoMedio == 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El archivo que desea subir no se encuentra configurado en el sistema.');", true);
                        this.lblmessage.Text = "El archivo que desea subir no se encuentra configurado en el sistema para el documento seleccionado.";
                        this.mpeMostrarError.Show();
                        return;
                    }

                    if (oDocumentoBE.TituloDocumento == "36PRE" &&
                        (oDocumentoBE.Extension == ".docx" || oDocumentoBE.Extension == ".doc"))
                    { }
                    else
                    {
                        this.lblmessage.Text = "El archivo que desea subir no se encuentra configurado en el sistema para el documento seleccionado.";
                        this.mpeMostrarError.Show();
                        return;
                    }


                    if (!ValidaAtributosArchivo(fuDocumento))
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El archivo que desea subir no debe exceder de 2MB');", true);
                        this.lblmessage.Text = "El archivo que desea subir no debe exceder de 2MB.";
                        this.mpeMostrarError.Show();
                        return;
                    }

                    oDocumentoBL = new DocumentoBL();
                    if (idDocumento > 0)
                    {
                        if (oDocumentoBL.modificarDocumentoAplicante(oDocumentoBE))
                        {
                            oAplicanteBE = new AplicanteBE();
                            oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
                            oAplicanteBE.IdConfiguracionAplicacion = UIConstantes._IdPostulacionPreGrado;   //7

                            oDocumentoBL = new DocumentoBL();
                            String status = oDocumentoBL.GetStatusCargaDocs(UIConvertNull.Int32(Session["AplicanteId"]));
                            if (status.Equals("true"))
                            {
                                this.mpeCargaDocumentos.Hide();
                                this.LLenarGrillaDocumentos();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (oDocumentoBL.insertarDocumentoAplicante(oDocumentoBE))
                        {
                            oAplicanteBE = new AplicanteBE();
                            oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
                            oAplicanteBE.IdConfiguracionAplicacion = UIConstantes._IdPostulacionPreGrado;

                            oDocumentoBL = new DocumentoBL();
                            String status = oDocumentoBL.GetStatusCargaDocs(UIConvertNull.Int32(Session["AplicanteId"]));
                            if (status.Equals("true"))
                            {
                                this.mpeCargaDocumentos.Hide();
                                this.LLenarGrillaDocumentos();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Por favor ingresar un archivo.');", true);
                    this.lblmessage.Text = "Por favor ingresar un archivo.";
                    this.mpeMostrarError.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.LLenarGrillaDocumentos();
                this.mpeCargaDocumentos.Hide();
                this.upCargaDocumentos.Update();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                this.mpeCargaDocumentos.Hide();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void imbDescargaDoc_OnClick(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            ImageButton _imbEditar = (ImageButton)(sender);
            GridViewRow gvrow = (GridViewRow)_imbEditar.NamingContainer;
            try
            {
                this.DescargarDocumento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void imgBtnEnviar_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                //this.GuardarDatos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.mpeMostrarError.Hide();
        }

        #endregion "Eventos"

        #region "Métodos Privados"

        private void ObtenerTextoInformativo(Int32? AplicanteId, Int32? TipoMensajesId)
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                DataTable dtTextoInf = oAplicanteBL.ObtenerTextoInformativo(AplicanteId, TipoMensajesId);
                if (dtTextoInf.Rows.Count > 0)
                {
                    DataRow drPago = dtTextoInf.Rows[0];
                    String strTexto = drPago["Texto"].ToString();
                    //this.lblMensajeInformativo.Text = strTexto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigurarTiposArchivos()
        {
            DocumentoBL oDocumentoBL = null;
            DataTable dtTiposArchivos = null;

            oDocumentoBL = new DocumentoBL();
            dtTiposArchivos = oDocumentoBL.ListaTiposArchivosPermitidos(UIConstantes._IdPostulacionPreGrado);
            _tiposArchivos = GetValidExtentions(dtTiposArchivos);
            SplitFileTypeExtensions(_tiposArchivos);
        }

        private Collection<AvailableFileType> GetValidExtentions(DataTable dtTiposArchivos)
        {
            Collection<AvailableFileType> fileTypes = new Collection<AvailableFileType>();

            foreach (DataRow drFila in dtTiposArchivos.Rows)
            {
                Int32 tempIdTipoMedio = 0;
                Int32.TryParse(drFila["MediaTypeId"].ToString(), out tempIdTipoMedio);
                String icono = drFila["Icon"].ToString();
                String extension = drFila["Extension"].ToString();
                String descripcion = drFila["MediaTypeDesc"].ToString();
                fileTypes.Add(new AvailableFileType(tempIdTipoMedio, icono, extension, descripcion));
            }

            return fileTypes;
        }

        private void SplitFileTypeExtensions(Collection<AvailableFileType> tipoArchivos)
        {
            String extention;
            foreach (AvailableFileType tipArch in tipoArchivos)
            {
                if (tipArch != null)
                {
                    extention = tipArch.FileExtension;
                    String[] extentions = extention.Split(';');
                    for (int x = 0; x < extentions.Length; x++)
                    {
                        extentions[x] = extentions[x].Replace("*", String.Empty);
                        extentions[x] = extentions[x].Replace(" ", String.Empty);

                        if (_extensionesValidas == null)
                        {
                            _extensionesValidas = new Hashtable();
                        }
                        if (!String.IsNullOrEmpty(extentions[x]) && !_extensionesValidas.ContainsKey(extentions[x].ToLower()))
                            _extensionesValidas.Add(extentions[x].ToLower(), tipArch.MediaTypeId);
                    }
                }
            }
        }

        private void LLenarDatosPersona()
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtDatos = null;
            Int32? AplicanteId = null;
            try
            {
                AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                oAplicanteBL = new AplicanteBL();
                dtDatos = oAplicanteBL.ObtenerDatosPostulanteParaEntrevista(AplicanteId);
                if (dtDatos != null && dtDatos.Rows.Count > 0)
                {
                    this.lblPostulante.Text = dtDatos.Rows[0]["Aplicante"].ToString();
                    this.lblNroDocumento.Text = dtDatos.Rows[0]["DNI"].ToString();
                    this.lblModalidad.Text = dtDatos.Rows[0]["Modalidad"].ToString();
                    this.lblCarrera.Text = dtDatos.Rows[0]["Carrera"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarGrillaDocumentos()
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtDocumentos = oGeneralBL.ObtenerDocsAdicionales(AplicanteId);
                if (dtDocumentos != null && dtDocumentos.Rows.Count > 0)
                {
                    this.gvDocumentos.DataSource = dtDocumentos;
                    this.gvDocumentos.DataBind();
                    this.pnlDocumentos.Visible = true;
                }
                else
                {
                    this.gvDocumentos.DataSource = null;
                    this.gvDocumentos.DataBind();
                    this.pnlDocumentos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private String ObtenerExtensionArchivo(FileUpload archivo)
        {
            if (archivo != null)
            {
                String fileName = Server.HtmlEncode(archivo.FileName);
                return System.IO.Path.GetExtension(fileName).ToLower();
            }
            return String.Empty;
        }

        private Int32 ObtenerTipoArchivo(String extension)
        {
            Int32 idTipoArchivo = 0;

            if (_extensionesValidas.Contains(extension))
            {
                Int32.TryParse(_extensionesValidas[extension].ToString(), out idTipoArchivo);
            }
            return idTipoArchivo;
        }

        private Boolean ValidaAtributosArchivo(FileUpload fuTempArchivo)
        {
            Boolean respuesta = true;
            if (fuTempArchivo.PostedFile.ContentLength > 2 * 1000000)
            {
                respuesta = false;
            }
            return respuesta;
        }

        private void DescargarDocumento()
        {
            DocumentoBE oDocumentoBE = null;
            DocumentoBL oDocumentoBL = null;
            try
            {
                oDocumentoBE = new DocumentoBE();
                oDocumentoBE.IdAplicacion = UIConvertNull.Int32(Session["AplicanteId"]);
                oDocumentoBE.TituloDocumento = hCodDocumento.Value;

                oDocumentoBL = new DocumentoBL();
                oDocumentoBE = oDocumentoBL.obtenerDocumentoAdjunto(oDocumentoBE);

                String nombreArchivo = oDocumentoBE.TituloDocumento + oDocumentoBE.Extension;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + nombreArchivo + "\"");
                HttpContext.Current.Response.ContentType = oDocumentoBE.TipoMedio;
                HttpContext.Current.Response.BinaryWrite(oDocumentoBE.ContenidoDocumento);
                HttpContext.Current.Response.Flush();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion "Métodos Privados"
    }
}