using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web.Services;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm05_ColegioProcede : BasePage
    {
        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                ///*Solo para pruebas*/
                //Session["usrRedId"] = "U.Admision";
                //Session["ModPostulacion"] = 40;
                //Session["AplicanteId"] = 88108;

                if (!IsPostBack)
                {
                    this.ConfigurarControles();
                    //this.CargarTitulos();
                    this.LimpiarControles();
                    this.MostrarOcultarBotones(false);
                    this.HabilitaControles();
                    this.cargarCombos();
                    //this.CargaNuevocolegio();
                    this.CargarColegiosRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                    HabilitarControlesMayusculas(); /*Se agrega: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F05, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
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
                    if (PaginaActual == UIConstantes.Formularios.F05)
                    {
                        PaginaAnterior = dtPagAnterior.Rows[i - 1]["NombreFormulario"].ToString();
                        break;
                    }
                }
                Response.Redirect(PaginaAnterior, false);
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
        }

        protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                this.GuardarDatos();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F05, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void ddlNroColegios_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? NroColegios = null;
            try
            {
                NroColegios = UIConvertNull.Int32(ddlNroColegios.SelectedValue);
                this.CargarColegios(NroColegios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnElimina1_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEducacion = UIConvertNull.Int32(txtIdEducacion1.Text);
            if (IdEducacion != null)
            {
                this.EliminaColegioRegistrado(IdEducacion);
                this.ddlNroColegios.SelectedValue = "0";
            }
            else
            {
                this.LimpiarControles();
                this.pnlColegio2.Visible = false;
                this.pnlColegio3.Visible = false;
            }
        }

        protected void btnElimina2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEducacion = UIConvertNull.Int32(txtIdEducacion2.Text);
            if (IdEducacion != null)
            {
                this.EliminaColegioRegistrado(IdEducacion);
            }
            else
            {
                this.pnlColegio2.Visible = false;
                this.pnlColegio3.Visible = false;
                this.CargarColegiosRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
            }
        }

        protected void btnElimina3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEducacion = UIConvertNull.Int32(txtIdEducacion3.Text);
            if (IdEducacion != null)
            {
                this.EliminaColegioRegistrado(IdEducacion);
            }
            else
            {
                this.pnlColegio3.Visible = false;
                this.CargarColegiosRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
            }
        }

        protected void lnkNuevoColegio_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                this.mpeMostrarNuevoColegio.Show();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                this.EnviarNuevocolegio();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F05, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_ENVIA_COLEGIO], false);
            }
            finally
            {
                this.mpeMostrarNuevoColegio.Hide();
            }
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                this.mpeMostrarNuevoColegio.Hide();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion "Eventos"

        #region "Métodos Privados"

        private void ConfigurarControles()
        {
            this.txtCodigoModular1.Attributes.Add("readonly", "readonly");
            this.txtCodigoModular2.Attributes.Add("readonly", "readonly");
            this.txtCodigoModular3.Attributes.Add("readonly", "readonly");

            this.txtDireccionColegio1.Attributes.Add("readonly", "readonly");
            this.txtDireccionColegio2.Attributes.Add("readonly", "readonly");
            this.txtDireccionColegio3.Attributes.Add("readonly", "readonly");
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
                    if (PaginaActual == UIConstantes.Formularios.F05)
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

        private void MostrarOcultarBotones(Boolean blnAccion)
        {
            this.pnlColegio1.Visible = blnAccion;
            this.pnlColegio2.Visible = blnAccion;
            this.pnlColegio3.Visible = blnAccion;
        }

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

        private void cargarCombos()
        {
            this.cargarComboEgresado();
        }

        private void cargarComboEgresado()
        {
            Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);    
            GeneralBL oGeneralBL = null;
            try
            {
                if (UIConvertNull.Int32(Session["ModPostulacion"]) == UIConstantes.Modalidad.TallerEPU || UIConvertNull.Int32(Session["ModPostulacion"]) == UIConstantes.Modalidad.ProgramaEPU)
                {
                    oGeneralBL = new GeneralBL();
                    DataTable dtEgresado = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.ESTA_ACADEMICO].Key, "", null);
                    if (dtEgresado != null && dtEgresado.Rows.Count > 0)
                    {
                        Funciones.cargarComboYSeleccione(this.ddlSitAcademica, dtEgresado.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                    }
                }
                else
                    if (UIConvertNull.Int32(Session["ModPostulacion"]) == UIConstantes.Modalidad.ExcelAcadem || UIConvertNull.Int32(Session["ModPostulacion"]) == UIConstantes.Modalidad.ExcelAcadem)
                    {
                        oGeneralBL = new GeneralBL();
                        DataTable dtEgresado = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.ESTA_ACADEMICOEXCACA].Key, "", null);
                        if (dtEgresado != null && dtEgresado.Rows.Count > 0)
                        {
                            Funciones.cargarComboYSeleccione(this.ddlSitAcademica, dtEgresado.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                        }
                    }
                    else
                    {
                        oGeneralBL = new GeneralBL();
                        DataTable dtEgresado = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.ESTA_ACADEMICOMOD].Key, "", null, ModalidadId);
                        if (dtEgresado != null && dtEgresado.Rows.Count > 0)
                        {
                            Funciones.cargarComboYSeleccione(this.ddlSitAcademica, dtEgresado.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                        }
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarColegios(Int32? NroColegios)
        {
            try
            {
                switch (NroColegios)
                {
                    case UIConstantes.Numero._ValorNumeroUno:
                        {
                            this.pnlColegio1.Visible = true;
                            this.pnlColegio2.Visible = false;
                            this.pnlColegio3.Visible = false;
                            break;
                        }
                    case UIConstantes.Numero._ValorNumeroDos:
                        {
                            this.pnlColegio1.Visible = true;
                            this.pnlColegio2.Visible = true;
                            this.pnlColegio3.Visible = false;
                            break;
                        }
                    case UIConstantes.Numero._ValorNumeroTres:
                        {
                            this.pnlColegio1.Visible = true;
                            this.pnlColegio2.Visible = true;
                            this.pnlColegio3.Visible = true;
                            break;
                        }
                    default:
                        {
                            this.MostrarOcultarBotones(false);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod()]
        public static List<EducacionBE> getColegios(String term)
        {
            EducacionBL oEducacionBL = null;
            try
            {
                String txtDegreeId = HttpContext.Current.Session["ModPostulacion"].ToString();
                oEducacionBL = new EducacionBL();
                return oEducacionBL.ListarColegios(term, txtDegreeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarColegiosRegistrados(Int32? AplicanteId)
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ObtenerColegioRegistrado(AplicanteId);
                this.LLenarDatosColegioRegistrado(oAplicanteBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosColegioRegistrado(AplicanteBE oAplicanteBE)
        {
            try
            {
                if (oAplicanteBE != null && oAplicanteBE.LEducacion != null)
                {
                    List<EducacionBE> LColegiosEduca = (from itemEduca in oAplicanteBE.LEducacion
                                                        orderby itemEduca.IdEducacion
                                                        select itemEduca).ToList<EducacionBE>();
                    if (LColegiosEduca != null)
                    {
                        ddlNroColegios.SelectedValue = UIConvertNull.String(LColegiosEduca.Count());
                        for (int indice = 0; indice < LColegiosEduca.Count; indice++)
                        {
                            EducacionBE recEducacion = LColegiosEduca[indice];
                            if (recEducacion.NombreInstitucion != string.Empty)
                            {
                                switch (indice)
                                {
                                    case 0:
                                        this.txtIdEducacion1.Text = recEducacion.IdEducacion.ToString();
                                        ddlSitAcademica.SelectedValue = recEducacion.SituacionAcademica;
                                        if (recEducacion.Institucion != null)
                                        {
                                            this.txtCodColegio1.Text = recEducacion.Institucion.Codigo.ToString();
                                            this.txtColegio1.Text = recEducacion.NombreInstitucion;
                                            this.txtCodigoModular1.Text = recEducacion.Institucion.CodigoModular;
                                            this.txtDireccionColegio1.Text = recEducacion.Institucion.Direccion + UIConstantes._valorSignoGuion + recEducacion.Institucion.Distrito + UIConstantes._valorSignoGuion + recEducacion.Institucion.Provincia;
                                            this.pnlColegio1.Visible = true;
                                        }
                                        else
                                        {
                                            this.txtCodColegio1.Text = UIConstantes.idValorNulo.ToString();
                                            this.txtColegio1.Text = recEducacion.NombreInstitucion;
                                        }
                                        break;
                                    case 1:
                                        this.txtIdEducacion2.Text = recEducacion.IdEducacion.ToString();
                                        if (recEducacion.Institucion != null)
                                        {
                                            this.txtCodColegio2.Text = recEducacion.Institucion.Codigo.ToString();
                                            this.txtColegio2.Text = recEducacion.NombreInstitucion;
                                            this.txtCodigoModular2.Text = recEducacion.Institucion.CodigoModular;
                                            this.txtDireccionColegio2.Text = recEducacion.Institucion.Direccion + UIConstantes._valorSignoGuion + recEducacion.Institucion.Distrito + UIConstantes._valorSignoGuion + recEducacion.Institucion.Provincia;
                                            this.pnlColegio2.Visible = true;
                                        }
                                        else
                                        {
                                            this.txtCodColegio2.Text = UIConstantes.idValorNulo.ToString();
                                            this.txtColegio2.Text = recEducacion.NombreInstitucion;
                                        }
                                        break;
                                    case 2:
                                        this.txtIdEducacion3.Text = recEducacion.IdEducacion.ToString();
                                        if (recEducacion.Institucion != null)
                                        {
                                            this.txtCodColegio3.Text = recEducacion.Institucion.Codigo.ToString();
                                            this.txtColegio3.Text = recEducacion.NombreInstitucion;
                                            this.txtCodigoModular3.Text = recEducacion.Institucion.CodigoModular;
                                            this.txtDireccionColegio3.Text = recEducacion.Institucion.Direccion + UIConstantes._valorSignoGuion + recEducacion.Institucion.Distrito + UIConstantes._valorSignoGuion + recEducacion.Institucion.Provincia;
                                            this.pnlColegio3.Visible = true;
                                        }
                                        else
                                        {
                                            this.txtCodColegio3.Text = UIConstantes.idValorNulo.ToString();
                                            this.txtColegio3.Text = recEducacion.NombreInstitucion;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GuardarDatos()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;

            GeneralBL oGeneralBL = null;
            DataTable dtPagSigui = null;
            String SitAcademica, PaginaActual, PaginaSiguiente = null;
            try
            {
                oAplicanteBE = new AplicanteBE();
                oAplicanteBE = ObtenerDatosColegio(oAplicanteBE);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormCuatro_ColegioProc(oAplicanteBE);
                if (operacionOK)
                {
                    Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                    oGeneralBL = new GeneralBL();
                    dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    {
                        PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F05)
                        {
                            PaginaSiguiente = dtPagSigui.Rows[i + 1]["NombreFormulario"].ToString();
                            break;
                        }
                    }
                    SitAcademica = ddlSitAcademica.SelectedValue;
                    Response.Redirect(PaginaSiguiente + "?SitAcademica=" + SitAcademica, false);
                }
                else
                {
                    Session["EstadoEnvio"] = null;
                    Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                }
            }
            catch (Exception ex)
            {
                Session["EstadoEnvio"] = null;
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
        }

        private AplicanteBE ObtenerDatosColegio(AplicanteBE oAplicanteBE)
        {
            oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"].ToString());
            oAplicanteBE.RedId = Session["usrRedId"].ToString();
            oAplicanteBE.SituacionAcademica = UIConvertNull.Int32(ddlSitAcademica.SelectedValue);
            Session["SituacionAcademica"] = ddlSitAcademica.SelectedValue;

            //======================================================================
            //COLEGIO 1
            //======================================================================
            EducacionBE Colegio1 = null;
            if (this.txtCodColegio1.Text != string.Empty)
            {
                if (Colegio1 == null)
                {
                    Colegio1 = new EducacionBE();
                }
                Int32 tempIdInstitucion = 0;
                Int32.TryParse(this.txtCodColegio1.Text, out tempIdInstitucion);
                if (tempIdInstitucion != 0)
                {
                    if (Colegio1.Institucion == null)
                    {
                        Colegio1.Institucion = new InstitucionBE();
                    }
                    Colegio1.Institucion.Codigo = tempIdInstitucion;
                }
            }
            if (this.txtColegio1.Text != string.Empty)
            {
                if (Colegio1 == null)
                {
                    Colegio1 = new EducacionBE();
                }
                Colegio1.NombreInstitucion = this.txtColegio1.Text.Replace("'", "''");
                Colegio1.IdEducacion = UIConvertNull.Int32(txtIdEducacion1.Text);
            }
            if (oAplicanteBE.LEducacion == null)
            {
                oAplicanteBE.LEducacion = new System.Collections.Generic.List<EducacionBE>();
            }
            if (Colegio1 != null)
            {
                Colegio1.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                Colegio1.SeccionEnFormulario = UIConstantes.SECCION_EN_FORMULARIO.FORM_POSITION_1C;
                oAplicanteBE.LEducacion.Add(Colegio1);
            }

            //======================================================================
            //COLEGIO 2
            //======================================================================
            EducacionBE Colegio2 = null;
            if (this.txtCodColegio2.Text != string.Empty)
            {
                if (Colegio2 == null)
                {
                    Colegio2 = new EducacionBE();
                }
                Int32 tempIdInstitucion = 0;
                Int32.TryParse(this.txtCodColegio2.Text, out tempIdInstitucion);
                if (tempIdInstitucion != 0)
                {
                    if (Colegio2.Institucion == null)
                    {
                        Colegio2.Institucion = new InstitucionBE();
                    }
                    Colegio2.Institucion.Codigo = tempIdInstitucion;
                }
            }
            if (this.txtColegio2.Text != string.Empty)
            {
                if (Colegio2 == null)
                {
                    Colegio2 = new EducacionBE();
                }
                Colegio2.NombreInstitucion = this.txtColegio2.Text.Replace("'", "''");
                Colegio2.IdEducacion = UIConvertNull.Int32(txtIdEducacion2.Text);
            }
            if (oAplicanteBE.LEducacion == null)
            {
                oAplicanteBE.LEducacion = new System.Collections.Generic.List<EducacionBE>();
            }
            if (Colegio2 != null)
            {
                Colegio2.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                Colegio2.SeccionEnFormulario = UIConstantes.SECCION_EN_FORMULARIO.FORM_POSITION_2C;
                oAplicanteBE.LEducacion.Add(Colegio2);
            }

            //======================================================================
            //COLEGIO 3
            //======================================================================
            EducacionBE Colegio3 = null;
            if (this.txtCodColegio3.Text != string.Empty)
            {
                if (Colegio3 == null)
                {
                    Colegio3 = new EducacionBE();
                }
                Int32 tempIdInstitucion = 0;
                Int32.TryParse(this.txtCodColegio3.Text, out tempIdInstitucion);
                if (tempIdInstitucion != 0)
                {
                    if (Colegio3.Institucion == null)
                    {
                        Colegio3.Institucion = new InstitucionBE();
                    }
                    Colegio3.Institucion.Codigo = tempIdInstitucion;
                }
            }
            if (this.txtColegio3.Text != string.Empty)
            {
                if (Colegio3 == null)
                {
                    Colegio3 = new EducacionBE();
                }
                Colegio3.NombreInstitucion = this.txtColegio3.Text.Replace("'", "''");
                Colegio3.IdEducacion = UIConvertNull.Int32(txtIdEducacion3.Text);
            }
            if (oAplicanteBE.LEducacion == null)
            {
                oAplicanteBE.LEducacion = new System.Collections.Generic.List<EducacionBE>();
            }
            if (Colegio3 != null)
            {
                Colegio3.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                Colegio3.SeccionEnFormulario = UIConstantes.SECCION_EN_FORMULARIO.FORM_POSITION_3C;
                oAplicanteBE.LEducacion.Add(Colegio3);
            }
            return oAplicanteBE;
        }

        private Boolean EliminaColegioRegistrado(Int32? IdEducacion)
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            AplicanteBL oAplicanteBL = null;
            Boolean Resultado = false;
            try
            {
                oAplicanteBL = new AplicanteBL();
                Resultado = oAplicanteBL.EliminaColegioRegistrado(IdEducacion, AplicanteId);
                if (Resultado == true)
                {
                    this.LimpiarControles();
                    this.MostrarOcultarBotones(false);
                    this.CargarColegiosRegistrados(AplicanteId);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarControles()
        {
            this.txtColegio1.Text = UIConstantes._valorCadenaVacia;
            this.txtCodigoModular1.Text = UIConstantes._valorCadenaVacia;
            this.txtDireccionColegio1.Text = UIConstantes._valorCadenaVacia;

            this.txtColegio2.Text = UIConstantes._valorCadenaVacia;
            this.txtCodigoModular2.Text = UIConstantes._valorCadenaVacia;
            this.txtDireccionColegio2.Text = UIConstantes._valorCadenaVacia;

            this.txtColegio3.Text = UIConstantes._valorCadenaVacia;
            this.txtCodigoModular3.Text = UIConstantes._valorCadenaVacia;
            this.txtDireccionColegio3.Text = UIConstantes._valorCadenaVacia;
        }

        private void EnviarNuevocolegio()
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtNombre = null;
            String FullName = null;
            String usrRedId, Resultado = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                dtNombre = oAplicanteBL.ObtenerNombrePostulante(UIConvertNull.Int32(Session["AplicanteId"]));
                if (dtNombre != null && dtNombre.Rows.Count > 0)
                {
                    FullName = dtNombre.Rows[0][0].ToString();
                }
                usrRedId = Session["usrRedId"].ToString();
                Resultado = oAplicanteBL.EnviarCorreoColegioNuevo(FullName, this.txtNomColegio.Text, txtContacto.Text, this.TxtDistrito.Text, usrRedId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*Ini: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
        private void HabilitarControlesMayusculas()
        {
            if (txtColegio1.Text != "") txtColegio1.Text = txtColegio1.Text.ToUpper();
            if (txtDireccionColegio1.Text != "") txtDireccionColegio1.Text = txtDireccionColegio1.Text.ToUpper();
            if (txtColegio2.Text != "") txtColegio2.Text = txtColegio2.Text.ToUpper();
            if (txtDireccionColegio2.Text != "") txtDireccionColegio2.Text = txtDireccionColegio2.Text.ToUpper();
            if (txtColegio3.Text != "") txtColegio3.Text = txtColegio3.Text.ToUpper();
            if (txtDireccionColegio3.Text != "") txtDireccionColegio3.Text = txtDireccionColegio3.Text.ToUpper();
        }
        /*Fin: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
        #endregion "Métodos Privados"
    }
}
