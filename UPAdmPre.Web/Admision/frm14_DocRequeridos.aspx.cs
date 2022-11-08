using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Text;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm14_DocRequeridos : BasePage
    {
        #region "Eventos"

        private Hashtable _extensionesValidas;
        Collection<AvailableFileType> _tiposArchivos;

        protected void Page_Load(object sender, EventArgs e)
        {
            ///*Solo para pruebas*/
            //Session["usrRedId"] = "U.Admision";
            //Session["ModPostulacion"] = 49;
            //Session["AplicanteId"] = 110602;
            UIHelper.SessionActiva(Page);
            try
            {
                this.ConfigurarTiposArchivos();
                if (!IsPostBack)
                {
                    this.HabilitaControles();
                    //this.CargarTitulos();
                    this.LLenarGrillaDocumentos();
                    ObtenerDocumentosRequeridosNota();/*Agrega[Christian Ramirez - Caso76999]*/
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F14, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
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
                    ImageButton _imgDelDoc = (ImageButton)e.Row.FindControl("imgDelDoc");
                    Label _lblDescripcion = (Label)e.Row.FindControl("Label4");
                    Label _lblObservacion = (Label)e.Row.FindControl("Label5");
                    Label _lblEstado = (Label)e.Row.FindControl("lblEstado");
                    Image _imgEstado = (Image)e.Row.FindControl("imgEstado");
                    Label _lblGovernmentId = (Label)e.Row.FindControl("Label6");


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
                        _lblGovernmentId.Text = UIConvertNull.String(dr["GovernmentId"]).ToString();
                        _lblObservacion.Visible = true;
                    }
                    else
                    {
                        _lblDescripcion.Text = UIConvertNull.String(dr["Descripcion"]).ToString();
                        _lblGovernmentId.Text = UIConvertNull.String(dr["GovernmentId"]).ToString();
                        _lblObservacion.Visible = false;
                    }

                    switch (_lblDescEstado.Text)
                    {
                        case "PENDIENTE":
                            _imbCargarDoc.Enabled = true;
                            _imbCargarDoc.Visible = true;
                            _imbDescargaDoc.Enabled = false;
                            _imbDescargaDoc.Visible = false;
                            _imgDelDoc.Visible = false;
                            break;
                        case "EN REVISIÓN":
                            _imbCargarDoc.Enabled = false;
                            _imbCargarDoc.Visible = false;
                            _imbDescargaDoc.Enabled = true;
                            _imbDescargaDoc.Visible = true;
                            _imgDelDoc.Visible = true;
                            break;
                        case "OBSERVADO":
                            _imbCargarDoc.Enabled = true;
                            _imbCargarDoc.Visible = true;
                            _imbDescargaDoc.Enabled = false;
                            _imbDescargaDoc.Visible = false;
                            _imgDelDoc.Visible = false;
                            break;
                        case "APROBADO":
                            _imbCargarDoc.Enabled = false;
                            _imbCargarDoc.Visible = false;
                            _imbDescargaDoc.Enabled = true;
                            _imbDescargaDoc.Visible = true;
                            _imgDelDoc.Visible = false;
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

        protected void imgBtnVideo_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            String URL = "frmVideoInformativo.aspx";
            String vtn = "window.open('" + URL + "','video','scrollbars=yes,resizable=no,location=no,width=640,height=380')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }

        protected void imgBtnBack_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            GeneralBL oGeneralBL = null;
            DataTable dtPagAnterior = null;
            String PaginaActual, PaginaAnterior = null;
            try
            {
                Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                oGeneralBL = new GeneralBL();
                dtPagAnterior = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                for (Int32 i = 0; i < dtPagAnterior.Rows.Count; i++)
                {
                    PaginaActual = dtPagAnterior.Rows[i]["NombreFormulario"].ToString();
                    if (PaginaActual == UIConstantes.Formularios.F14)
                    {
                        PaginaAnterior = dtPagAnterior.Rows[i - 1]["NombreFormulario"].ToString();
                        break;
                    }
                }
                Response.Redirect(PaginaAnterior, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            GeneralBL oGeneralBL = null;
            AplicanteBL oAplicanteBL = null;
            DataTable dtPagSigui = null;
            String PaginaActual, PaginaSiguiente = null;
            Int32? intDocCompletos = null;
            try
            {
                /////Validando si se adjunto todos los documentos
                //oAplicanteBL = new AplicanteBL();
                //intDocCompletos = oAplicanteBL.ConsultaDocumentosCompletados(UIConvertNull.Int32(Session["AplicanteId"]));
                //if (intDocCompletos == 0)
                //{
                //    oAplicanteBL = new AplicanteBL();
                //    oAplicanteBL.EnviaCorreoSiDocCompleto(UIConvertNull.Int32(Session["AplicanteId"]));
                //}

                ///Redireccionando a la Siguiente Página
                Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);
                oGeneralBL = new GeneralBL();
                dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                {
                    PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                    if (PaginaActual == UIConstantes.Formularios.F14)
                    {
                        PaginaSiguiente = dtPagSigui.Rows[i + 1]["NombreFormulario"].ToString();
                        break;
                    }
                }
                Response.Redirect(PaginaSiguiente, false);
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
                if (hCodDocumento.Value == "21PRE")
                {
                    this.imgJPGPopUp.Visible = true;
                    this.imgPDFPopUp.Visible = false;
                }
                else
                {
                    this.imgJPGPopUp.Visible = false;
                    this.imgPDFPopUp.Visible = true;
                }
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

        protected void imbDeleteDoc_OnClick(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            ImageButton imgDelDoc = (ImageButton)(sender);
            GridViewRow gvrow = (GridViewRow)imgDelDoc.NamingContainer;


            Label DocumentId = (Label)gvrow.FindControl("lblCodigo");
            Label ApplicationId = (Label)gvrow.FindControl("lblApplicationId");
            Label ApplicationAttachmentId = (Label)gvrow.FindControl("lblIdDocumento");

            try
            {
                int resp = new DocumentoBL().UpdDocumentoAdjunto(Convert.ToInt32(ApplicationAttachmentId.Text),
                                                                 Convert.ToInt32(ApplicationId.Text), 
                                                                 DocumentId.Text);
                if (resp == 1)
                {
                    this.LLenarGrillaDocumentos();
                }
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

                    if (oDocumentoBE.IdTipoMedio == 0 || (oDocumentoBE.Extension == ".docx" || oDocumentoBE.Extension == ".doc"))
                    {
                        this.lblmessage.Text = "El archivo que desea subir no se encuentra configurado en el sistema para el documento seleccionado.";
                        this.mpeMostrarError.Show();
                        return;
                    }

                    if (oDocumentoBE.TituloDocumento == "21PRE")
                    {
                        if (oDocumentoBE.Extension == ".jpg" || oDocumentoBE.Extension == ".jpeg")
                        { }
                        else
                        {
                            this.lblmessage.Text = "El archivo que desea subir no se encuentra configurado en el sistema para el documento seleccionado.";
                            this.mpeMostrarError.Show();
                            return;
                        }
                    }

                    if (oDocumentoBE.TituloDocumento == "36PRE")
                    {
                        if (oDocumentoBE.Extension == ".docx" || oDocumentoBE.Extension == ".doc")
                        { }
                        else
                        {
                            this.lblmessage.Text = "El archivo que desea subir no se encuentra configurado en el sistema para el documento seleccionado.";
                            this.mpeMostrarError.Show();
                            return;
                        }
                    }

                    if (oDocumentoBE.TituloDocumento != "21PRE" && oDocumentoBE.TituloDocumento != "36PRE")
                    {
                        if (oDocumentoBE.Extension != ".pdf")
                        {
                            this.lblmessage.Text = "El archivo que desea subir no se encuentra configurado en el sistema para el documento seleccionado.";
                            this.mpeMostrarError.Show();
                            return;
                        }
                    }

                    if (!ValidaAtributosArchivo(fuDocumento))
                    {
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
                                this.fuDocumento.Attributes.Clear();
                            }
                            else
                            {
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Se ha adjuntado el archivo con éxito');", true);
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
                                this.fuDocumento.Attributes.Clear();
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

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.mpeMostrarError.Hide();
        }

        #endregion "Eventos"

        #region "Métodos Privados"

        private void HabilitaControles()
        {
            String VideoActivo = ConfigurationManager.AppSettings["VideoActivo"].ToString();
            if (VideoActivo == UIConstantes._valorActivo)
            {
                imgBtnVideo.Visible = true;
            }
            else
            {
                imgBtnVideo.Visible = false;
            }
        }

        private void CargarTitulos()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtTitulos = null;
            String PaginaActual = null;
            try
            {
                Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                oGeneralBL = new GeneralBL();
                dtTitulos = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                for (Int32 i = 0; i < dtTitulos.Rows.Count; i++)
                {
                    PaginaActual = dtTitulos.Rows[i]["NombreFormulario"].ToString();
                    if (PaginaActual == UIConstantes.Formularios.F14)
                    {
                        this.lblPantallaAnterior.Text = dtTitulos.Rows[i - 1]["NombreFicha"].ToString();
                        this.lblPantallaVigente.Text = dtTitulos.Rows[i]["NombreFicha"].ToString();
                        this.lblPantallaSiguiente.Text = dtTitulos.Rows[i + 1]["NombreFicha"].ToString();
                        lblPasos.Text = " Paso " + (Convert.ToInt32(dtTitulos.Rows[i]["NroPaso"]) - 1) + " de " + (dtTitulos.Rows.Count - 1);
                        break;
                    }
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

        private void LLenarGrillaDocumentos()
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtDocumentos = oGeneralBL.ObtenerDocsPorModalidadPostulacion(AplicanteId);
                if (dtDocumentos != null && dtDocumentos.Rows.Count > 0)
                {
                    this.gvDocumentos.DataSource = dtDocumentos;
                    this.gvDocumentos.DataBind();
                }
                else
                {
                    this.gvDocumentos.DataSource = null;
                    this.gvDocumentos.DataBind();
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

        /*Ini[Christian Ramirez - caso76999]*/
        private string ObtenerDocumentosRequeridosNota()
        {
            string rpta = "";
            DocumentoBL oDocumentoBL = new DocumentoBL();
            rpta = oDocumentoBL.ObtenerDocumentosRequeridosNota();
            divExplicacion.InnerHtml = rpta;
            return rpta;
        }
        /*Fin[Christian Ramirez - caso76999]*/

        #endregion "Métodos Privados"
    }
}
