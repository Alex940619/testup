using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm02_ModalidadPostula : BasePage
    {
        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (!IsPostBack)
                {
                    ///*Solo para pruebas*/
                    //Session["usrRedId"] = "U.Admision";
                    //Session["ModPostulacion"] = null;
                    //Session["AplicanteId"] = null;

                    this.HabilitaControles();
                    //this.CargarTitulos();
                    this.MostrarOcultarBotones(false);
                    if (Session["AplicanteId"] != null)
                    {
                        this.CargaPeriodoPostulacionContinuacion();
                        this.CargaModalidadPostulacionContinuacion();
                        Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                        this.rblModalidad.Enabled = false;
                        this.CargaBecaPostulacionNuevo();
                        this.recuperaModalidadRegistrada(AplicanteId);
                        
                    }
                    else
                    {
                        this.CargaPeriodoPostulacionNuevo();
                       // this.CargaModalidadPostulacionNuevo();
                        this.CargaBecaPostulacionNuevo();
                        this.rblModalidad.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F02, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        protected void imgBtnVideo_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            String URL = "frmVideoInformativo.aspx?start=" + 68 + "&end=" + 80;
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
                if (dtPagAnterior.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < dtPagAnterior.Rows.Count; i++)
                    {
                        PaginaActual = dtPagAnterior.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F02)
                        {
                            PaginaAnterior = dtPagAnterior.Rows[i - 1]["NombreFormulario"].ToString();
                            break;
                        }
                    }
                    Response.Redirect(PaginaAnterior, false);
                }
                if (ConfigurationManager.AppSettings["VideoActivo"].ToString() == "1")
                {
                    Response.Redirect("frm01_VideoIntro.aspx", false);
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('No puede regresar...!!!');", true);
                    this.lblmessage.Text = "No puede regresar...!!!";
                    this.mpeMostrarError.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F02, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void rblModalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdModalidad = 0;
            try
            {
                this.rblCarrera.Visible = true;
                this.MostrarOcultarBotones(true);
                IdModalidad = UIConvertNull.Int32(rblModalidad.SelectedValue);
                Session["ModPostulacion"] = UIConvertNull.Int32(rblModalidad.SelectedValue);
                this.CargaCarreraPorModalidad(IdModalidad);
                this.ObtenerDescripcionModalidad(IdModalidad);
                this.imgBtnNext.Visible = true;
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
                lblPantallaAnterior.Visible = true;
                imgBtnBack.Visible = true;
            }
            else
            {
                imgBtnVideo.Visible = false;
                lblPantallaAnterior.Visible = false;
                imgBtnBack.Visible = false;
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
                    if (PaginaActual == UIConstantes.Formularios.F02)
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

        private void CargaModalidadPostulacionNuevo()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtTipoPostulacion;
            try
            {
                oGeneralBL = new GeneralBL();
                dtTipoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPO_POSTULACION_PREGRADO].Key, Session["usrRedId"].ToString(), null);
                if (dtTipoPostulacion != null && dtTipoPostulacion.Rows.Count > 0)
                {
                    this.rblModalidad.DataValueField = "codigo";
                    this.rblModalidad.DataTextField = "descripcion";
                    this.rblModalidad.DataSource = dtTipoPostulacion;
                    this.rblModalidad.DataBind();
                    this.imgBtnNext.Visible = true;
                }
                else
                {
                    DataTable DTMensajes = null;
                    DTMensajes = new DataTable();
                    this.rblModalidad.DataSource = null;
                    this.rblModalidad.DataBind();
                    //this.lblMensajeModalidad.Text = UIConstantes.Alert.msgModalidadNoDisponible;                    
                    DTMensajes = oGeneralBL.obtenerMensajeporId(4);
                    lblMensajeModalidad.Text = DTMensajes.Rows[0][2].ToString();
                    this.imgBtnNext.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtTipoPostulacion = null;
            }
        }

        private void CargaModalidadPostulacionContinuacion()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtTipoPostulacion;
            try
            {
                oGeneralBL = new GeneralBL();
                dtTipoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPO_POSTULACION_PREGRADO_CONT].Key, Session["usrRedId"].ToString(), null);
                if (dtTipoPostulacion != null && dtTipoPostulacion.Rows.Count > 0)
                {
                    this.rblModalidad.DataValueField = "codigo";
                    this.rblModalidad.DataTextField = "descripcion";
                    this.rblModalidad.DataSource = dtTipoPostulacion;
                    this.rblModalidad.DataBind();
                    this.imgBtnNext.Visible = true;
                }
                else
                {
                    DataTable DTMensajes = null;
                    DTMensajes = new DataTable();
                    this.rblModalidad.DataSource = null;
                    this.rblModalidad.DataBind();
                    //this.lblMensajeModalidad.Text = UIConstantes.Alert.msgModalidadNoDisponible;
                    DTMensajes = oGeneralBL.obtenerMensajeporId(4);
                    lblMensajeModalidad.Text = DTMensajes.Rows[0][2].ToString();
                    this.imgBtnNext.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtTipoPostulacion = null;
            }
        }

        private void CargaCarreraPorModalidad(Int32? DegreeId)
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtCarreras = null;
            Int32 SettingId = 7;
            try
            {
                oAplicanteBL = new AplicanteBL();
                dtCarreras = oAplicanteBL.ListarCarrerasPorModalidad(DegreeId, SettingId);
                if (dtCarreras != null && dtCarreras.Rows.Count > 0)
                {
                    this.rblCarrera.DataValueField = "ProgramOfStudyId";
                    this.rblCarrera.DataTextField = "LONG_DESC";
                    this.rblCarrera.DataSource = dtCarreras;
                    this.rblCarrera.DataBind();

                    this.lblMensajeModalidad.Visible = false;
                }
                else
                {
                    this.rblCarrera.DataSource = null;
                    this.rblCarrera.DataBind();

                    this.lblMensajeModalidad.Visible = true;
                    this.lblMensajeCarreras.Text = UIConstantes.Alert.msgCarreraNoDisponible;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteBL = null;
                dtCarreras = null;
            }
        }

        private void ObtenerDescripcionModalidad(Int32? IdModalidad)
        {
            GeneralBL oGeneralBL = null;
            DataTable dtDescModalidad = null;
            Int32? ApplicationFormSettingId = null;
            try
            {
                ApplicationFormSettingId = UIConstantes._IdPostulacionPreGrado;

                oGeneralBL = new GeneralBL();
                dtDescModalidad = oGeneralBL.ObtenerDescripcionModalidad(IdModalidad, null, ApplicationFormSettingId);
                if (dtDescModalidad != null && dtDescModalidad.Rows.Count > 0)
                {
                    this.lblDescripcionModalidad.Text = dtDescModalidad.Rows[0][0].ToString();
                    this.DescripcionMod.Visible = true;
                }
                else
                {
                    this.lblDescripcionModalidad.Text = "";
                    this.DescripcionMod.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtDescModalidad = null;
            }
        }

        private void recuperaModalidadRegistrada(Int32? AplicanteId)
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtModalidad = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                dtModalidad = oAplicanteBL.ObtenerModalidadRegistrada(AplicanteId);
                if (dtModalidad != null && dtModalidad.Rows.Count > 0)
                {
                    this.rblModalidad.SelectedValue = dtModalidad.Rows[0][0].ToString();
                    Int32? IdModalidad = UIConvertNull.Int32(dtModalidad.Rows[0][0].ToString());
                    this.CargaCarreraPorModalidad(IdModalidad);
                    this.ObtenerDescripcionModalidad(IdModalidad);
                    this.rblCarrera.SelectedValue = dtModalidad.Rows[0][2].ToString();                   
                    this.lblCarreraTitulo.Visible = true;
                    this.rblCarrera.Enabled = false;
                    this.rblAnioAca.SelectedValue = dtModalidad.Rows[0][4].ToString();
                    this.rblAnioAca.Enabled = false;
                    this.rblBeca.SelectedValue = dtModalidad.Rows[0][5].ToString();
                    this.rblBeca.Enabled = false;
                    Session["ModPostulacion"] = IdModalidad;
                }
                else
                {
                    this.lblMensajeModalidad.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteBL = null;
                dtModalidad = null;
            }
        }

        private void MostrarOcultarBotones(Boolean blnAccion)
        {
            this.lblCarreraTitulo.Visible = blnAccion;
            this.lblMensajeCarreras.Visible = blnAccion;
        }

        private void GuardarDatos()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            Session["ModPostulacion"] = UIConvertNull.Int32(rblModalidad.SelectedValue);
            try
            {
                oAplicanteBE = new AplicanteBE();

                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ListarDatosPersonalesPorUsrRed(Session["usrRedId"].ToString());

                oAplicanteBE.ModalidadPostulacion = UIConvertNull.Int32(rblModalidad.SelectedValue.ToString()); //Modalidad de Postulacion
                oAplicanteBE.ProgramOfStudy = UIConvertNull.Int32(rblCarrera.SelectedValue.ToString()); //Carrera Elegida
                oAplicanteBE.Estado = UIConstantes.idValorActivo;
                oAplicanteBE.IdConfiguracionAplicacion = int.Parse(UIConstantes.TIPO_FORMULARIO.PREGRADO.ToString("D"));
                oAplicanteBE.EstaInteresadoPlanComida = UIConstantes.idValorNulo;
                oAplicanteBE.EstaInteresadoPlanResidenciaUni = UIConstantes.idValorNulo;
                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);                
                oAplicanteBE.RedId = UIConvertNull.String(Session["usrRedId"]);
                oAplicanteBE.sessionId = UIConvertNull.Int32(rblAnioAca.SelectedValue.ToString());
                oAplicanteBE.becaId = UIConvertNull.Int32(rblBeca.SelectedValue.ToString());

                String correoSalida = (ConfigurationManager.AppSettings["CorresoSalidaAdmPre"] != null ? ConfigurationManager.AppSettings["CorresoSalidaAdmPre"] : string.Empty);

                oAplicanteBL = new AplicanteBL();
                Int32? AplicanteId = oAplicanteBL.InsertaDatosFormDos_ModPostul(oAplicanteBE, null);
                Session["AplicanteId"] = AplicanteId;
                if (AplicanteId != 0 && AplicanteId != null)
                {
                    Response.Redirect("frm04_DatoPersonal.aspx", false);
                }
                else
                {
                    Session["EstadoEnvio"] = null;
                    Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargaPeriodoPostulacionNuevo()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtPeriodoPostulacion;
            try
            {
                oGeneralBL = new GeneralBL();
                dtPeriodoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.PERIODOACA].Key, Session["usrRedId"].ToString(), null);
                if (dtPeriodoPostulacion != null && dtPeriodoPostulacion.Rows.Count > 0)
                {
                    this.rblAnioAca.DataValueField = "codigo";
                    this.rblAnioAca.DataTextField = "descripcion";
                    this.rblAnioAca.DataSource = dtPeriodoPostulacion;
                    this.rblAnioAca.DataBind();
                    if (dtPeriodoPostulacion.Rows.Count == 1) 
                    {
                        this.rblAnioAca.SelectedValue = dtPeriodoPostulacion.Rows[0][0].ToString();                     
                        this.rblAnioAca.Enabled = false;
                        this.ObtenerDescripcionPeriodo(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                        this.ObtenerDescripcionBeca(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                        this.ObtenerDescripcionAviso(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                        this.CargaModalidadPostulacionPeriodo(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                    }
                    this.imgBtnNext.Visible = false;
                }
                else
                {
                    DataTable DTMensajes = null;
                    DTMensajes = new DataTable();
                    this.rblAnioAca.DataSource = null;
                    this.rblAnioAca.DataBind();
                    //this.lblMensajeModalidad.Text = UIConstantes.Alert.msgModalidadNoDisponible;                    
                    DTMensajes = oGeneralBL.obtenerMensajeporId(4);
                    lblMensajeModalidad.Text = DTMensajes.Rows[0][2].ToString();
                    this.imgBtnNext.Visible = false;
                    this.lbltextPeriodo.Visible = false;
                    this.lblTextModa.Visible = false;
                    this.rblBeca.Visible = false;
                    this.lblMsjeBeca.Visible = false;
                    this.lblBeca.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtPeriodoPostulacion = null;
            }
        }

        protected void rblAnioAca_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdPeriodo = 0;
            try
            {
                //this.MostrarOcultarBotones(true);
                IdPeriodo = UIConvertNull.Int32(rblAnioAca.SelectedValue);
               // Session["ModPostulacion"] = UIConvertNull.Int32(rblModalidad.SelectedValue);
               // this.CargaCarreraPorModalidad(IdPeriodo);
                this.ObtenerDescripcionPeriodo(IdPeriodo);
                this.ObtenerDescripcionBeca(IdPeriodo);
                this.ObtenerDescripcionAviso(IdPeriodo);
                this.CargaModalidadPostulacionPeriodo(IdPeriodo);
                this.rblCarrera.Visible = false;
                this.lblDescripcionModalidad.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerDescripcionPeriodo(Int32? IdPeriodo)
        {
            GeneralBL oGeneralBL = null;
            DataTable dtDescPeriodo = null;
            Int32? ApplicationFormSettingId = null;
            try
            {
                ApplicationFormSettingId = UIConstantes._IdPostulacionPreGrado;

                oGeneralBL = new GeneralBL();
                dtDescPeriodo = oGeneralBL.ObtenerDescripcionPeriodo(IdPeriodo, ApplicationFormSettingId);
                if (dtDescPeriodo != null && dtDescPeriodo.Rows.Count > 0)
                {
                    this.lblMsjePeriodo.Text = dtDescPeriodo.Rows[0][0].ToString();
                    //this.DescripcionMod.Visible = true;
                }
                else
                {
                    this.lblMsjePeriodo.Text = "";
                    //this.DescripcionMod.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtDescPeriodo = null;
            }
        }

        private void CargaBecaPostulacionNuevo()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtBecaPostulacion;
            try
            {
                oGeneralBL = new GeneralBL();
                dtBecaPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.PREBECA].Key, Session["usrRedId"].ToString(), null);
                if (dtBecaPostulacion != null && dtBecaPostulacion.Rows.Count > 0)
                {
                    this.rblBeca.DataValueField = "codigo";
                    this.rblBeca.DataTextField = "descripcion";
                    this.rblBeca.DataSource = dtBecaPostulacion;
                    this.rblBeca.DataBind();               
                }
                else
                {
                    //DataTable DTMensajes = null;
                    //DTMensajes = new DataTable();
                    this.rblBeca.DataSource = null;
                    this.rblBeca.DataBind();
                    //this.lblMensajeModalidad.Text = UIConstantes.Alert.msgModalidadNoDisponible;                    
                    //DTMensajes = oGeneralBL.obtenerMensajeporId(4);
                    //lblMsjeBeca.Text = DTMensajes.Rows[0][2].ToString();
                    //this.imgBtnNext.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtBecaPostulacion = null;
            }
        }

        private void ObtenerDescripcionBeca(Int32? IdPeriodo)
        {
            GeneralBL oGeneralBL = null;
            DataTable dtDescBeca = null;
            Int32? ApplicationFormSettingId = null;
            try
            {
                ApplicationFormSettingId = UIConstantes._IdPostulacionPreGrado;

                oGeneralBL = new GeneralBL();
                dtDescBeca = oGeneralBL.ObtenerDescripcionBeca(IdPeriodo, ApplicationFormSettingId);
                if (dtDescBeca != null && dtDescBeca.Rows.Count > 0)
                {
                    this.lblMsjeBeca.Text = dtDescBeca.Rows[0][0].ToString();
                    //this.DescripcionMod.Visible = true;
                }
                else
                {
                    this.lblMsjeBeca.Text = "";
                    //this.DescripcionMod.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtDescBeca = null;
            }
        }
        private void ObtenerDescripcionAviso(Int32? IdPeriodo)
        {
            GeneralBL oGeneralBL = null;
            DataTable dtDescAviso = null;
            Int32? ApplicationFormSettingId = null;
            try
            {
                ApplicationFormSettingId = UIConstantes._IdPostulacionPreGrado;

                oGeneralBL = new GeneralBL();
                dtDescAviso = oGeneralBL.ObtenerDescripcionAviso(IdPeriodo, ApplicationFormSettingId);
                if (dtDescAviso != null && dtDescAviso.Rows.Count > 0)
                {
                    this.lblAviso.Text = dtDescAviso.Rows[0][0].ToString();
                    //this.DescripcionMod.Visible = true;
                }
                else
                {
                    this.lblAviso.Text = "";
                    //this.DescripcionMod.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtDescAviso = null;
            }
        }

        private void CargaModalidadPostulacionPeriodo(Int32? IdPeriodo)
        {
            GeneralBL oGeneralBL = null;
            DataTable dtTipoPostulacion;
            try
            {
                oGeneralBL = new GeneralBL();
                dtTipoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPO_POSTULACION_PREGRADO2].Key, Session["usrRedId"].ToString(), null, IdPeriodo);
                if (dtTipoPostulacion != null && dtTipoPostulacion.Rows.Count > 0)
                {
                    this.rblModalidad.DataValueField = "codigo";
                    this.rblModalidad.DataTextField = "descripcion";
                    this.rblModalidad.DataSource = dtTipoPostulacion;
                    this.rblModalidad.DataBind();
                    this.imgBtnNext.Visible = true;
                }
                else
                {
                    DataTable DTMensajes = null;
                    DTMensajes = new DataTable();
                    this.rblModalidad.DataSource = null;
                    this.rblModalidad.DataBind();
                    //this.lblMensajeModalidad.Text = UIConstantes.Alert.msgModalidadNoDisponible;                    
                    DTMensajes = oGeneralBL.obtenerMensajeporId(4);
                    lblMensajeModalidad.Text = DTMensajes.Rows[0][2].ToString();
                    this.imgBtnNext.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtTipoPostulacion = null;
            }
        }
        private void CargaPeriodoPostulacionContinuacion()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtPeriodoPostulacion;
            try
            {
                oGeneralBL = new GeneralBL();
                dtPeriodoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.PERIODOACACONT].Key, Session["usrRedId"].ToString(), null);
                if (dtPeriodoPostulacion != null && dtPeriodoPostulacion.Rows.Count > 0)
                {
                    this.rblAnioAca.DataValueField = "codigo";
                    this.rblAnioAca.DataTextField = "descripcion";
                    this.rblAnioAca.DataSource = dtPeriodoPostulacion;
                    this.rblAnioAca.DataBind();
                    this.ObtenerDescripcionAviso(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                    this.ObtenerDescripcionPeriodo(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                    this.ObtenerDescripcionBeca(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                }
                else
                {
                    DataTable DTMensajes = null;
                    DTMensajes = new DataTable();
                    this.rblAnioAca.DataSource = null;
                    this.rblAnioAca.DataBind();
                    //this.lblMensajeModalidad.Text = UIConstantes.Alert.msgModalidadNoDisponible;                    
                    DTMensajes = oGeneralBL.obtenerMensajeporId(4);
                    lblMsjeBeca.Text = DTMensajes.Rows[0][2].ToString();
                    this.imgBtnNext.Visible = false;
                    this.lbltextPeriodo.Visible = false;
                    this.lblTextModa.Visible = false;
                    this.rblBeca.Visible = false;
                    this.lblMsjeBeca.Visible = false;
                    this.lblBeca.Visible = false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtPeriodoPostulacion = null;
            }
        }

        #endregion "Métodos Privados"
                
    }
}
