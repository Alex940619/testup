using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls; /*Christian Ramirez - GIIT - 20200129*/
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm21_ModalidadColegioPostula : BasePage
    {
        #region METODOS ASINCRONOS
        [WebMethod()]
        public static string getTipoColegio(string codModular, string anioAcademico)
        {
            string rpta = "";
            EducacionBL oEducacionBL = new EducacionBL();
            rpta = oEducacionBL.ObtenerTipoColegio(codModular, anioAcademico); /*Se agrega:Christian Ramirez - REQ91569*/
            return rpta;
        }

        #region se comenta
        //[WebMethod()]
        //public static string getTipoColegioLimaProv(string codModular)
        //{
        //   string rpta2 = "";
        //    EducacionBL oEducacionBL = new EducacionBL();
        //    string codModalidad="99";           
        //    rpta2 = oEducacionBL.ObtenerTipoColegioLimaProv(codModular, codModalidad);
        //   // Session["colegioProv"] = UIConvertNull.Int32(rpta2);
        //    return rpta2;
        //}
        #endregion

        [WebMethod()]
        public static List<EducacionBE> getColegios(String term){
            EducacionBL oEducacionBL = null;
            try
            {
                String txtDegreeId = HttpContext.Current.Session["ModPostulacion"].ToString();
                oEducacionBL = new EducacionBL();
                return oEducacionBL.ListarColegios(term, txtDegreeId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region METODOS COMUN
        private void MostrarOcultarBotonesModalidad(Boolean blnAccion)
        {
            this.lblCarreraTitulo.Visible = blnAccion;
            this.lblMensajeCarreras.Visible = blnAccion;
        }

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

        private void ConfigurarControles()
        {
            this.txtCodigoModular1.Attributes.Add("readonly", "readonly");
            this.txtCodigoModular2.Attributes.Add("readonly", "readonly");
            this.txtCodigoModular3.Attributes.Add("readonly", "readonly");

            this.txtDireccionColegio1.Attributes.Add("readonly", "readonly");
            this.txtDireccionColegio2.Attributes.Add("readonly", "readonly");
            this.txtDireccionColegio3.Attributes.Add("readonly", "readonly");
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

        private void MostrarOcultarBotones(Boolean blnAccion)
        {
            this.pnlColegio1.Visible = blnAccion;
            this.pnlColegio2.Visible = blnAccion;
            this.pnlColegio3.Visible = blnAccion;
        }

        private void HabilitarControlesMayusculas()
        {
            if (txtColegio1.Text != "") txtColegio1.Text = txtColegio1.Text.ToUpper();
            if (txtDireccionColegio1.Text != "") txtDireccionColegio1.Text = txtDireccionColegio1.Text.ToUpper();
            if (txtColegio2.Text != "") txtColegio2.Text = txtColegio2.Text.ToUpper();
            if (txtDireccionColegio2.Text != "") txtDireccionColegio2.Text = txtDireccionColegio2.Text.ToUpper();
            if (txtColegio3.Text != "") txtColegio3.Text = txtColegio3.Text.ToUpper();
            if (txtDireccionColegio3.Text != "") txtDireccionColegio3.Text = txtDireccionColegio3.Text.ToUpper();
        }

        /*Ini:Christian Ramirez - REQ91569*/
        private void MostrarPopupAviso(string contenido)
        {
            lblContenidoPopupAviso.Text = contenido;
            mpeAviso.Show();
        }
		/*Fin:Christian Ramirez - REQ91569*/
        #endregion


        #region METODOS
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
                    btnAgregarModalidad.Visible = false; /*Se comenta:Christian Ramirez - REQ91569*/

                    if (dtModalidad.Rows[0][6].ToString() != "0" && dtModalidad.Rows[0][6].ToString() != "" && dtModalidad.Rows[0][4].ToString() != "518" && dtModalidad.Rows[0][4].ToString() != "519")//&& dtModalidad.Rows[0][4].ToString()!="2021"
                    {
                        this.CargaTipoEvaluacion();                        
                        this.rblEvaluacion.SelectedValue = dtModalidad.Rows[0][6].ToString();
                        trTipoEvaluacion.Attributes.Add("style", "display:inline");
                        //trTipoEvaluacion2.Attributes.Add("style", "display:inline");
                    }
                    else
                    { trTipoEvaluacion.Attributes.Add("style", "display:none");
                    //trTipoEvaluacion2.Attributes.Add("style", "display:none");
                    }

                    this.rblEvaluacion.Enabled = false;
                    Session["ModPostulacion"] = IdModalidad;
                }
                else
                {
                    this.lblMensajeModalidad.Text = "";
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                    + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oAplicanteBL = null;
                dtModalidad = null;
            }
        }

        private bool CargaModalidadPostulacionContinuacion()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtTipoPostulacion;
            bool rpta = false;

            try
            {
                oGeneralBL = new GeneralBL();
                dtTipoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()
                    [UIConstantes.TIPO_CODIGO.TIPO_POSTULACION_PREGRADO_CONT].Key, Session["usrRedId"].ToString(), null);
                rpta = (dtTipoPostulacion != null && dtTipoPostulacion.Rows.Count > 0);

                if (rpta)
                {
                    this.rblModalidad.DataValueField = "codigo";
                    this.rblModalidad.DataTextField = "descripcion";
                    this.rblModalidad.DataSource = dtTipoPostulacion;
                    this.rblModalidad.DataBind();
                    this.imgBtnNext.Visible = true;
                }
                else
                {
                    

                    this.rblModalidad.DataSource = null;
                    this.rblModalidad.DataBind();
                    this.imgBtnNext.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                     + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtTipoPostulacion = null;
            }

            return rpta;
        }

        private bool CargaPeriodoPostulacionContinuacion()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtPeriodoPostulacion;
            bool rpta = false;

            try
            {
                oGeneralBL = new GeneralBL();
                dtPeriodoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.PERIODOACACONT].Key, Session["usrRedId"].ToString(), null);
                rpta = (dtPeriodoPostulacion != null && dtPeriodoPostulacion.Rows.Count > 0);

                if (rpta)
                {
                    this.rblAnioAca.DataValueField = "codigo";
                    this.rblAnioAca.DataTextField = "descripcion";
                    this.rblAnioAca.DataSource = dtPeriodoPostulacion;
                    this.rblAnioAca.DataBind();
                    this.ObtenerDescripcionAviso(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                    this.ObtenerDescripcionPeriodo(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                    this.ObtenerDescripcionBeca(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                    this.ObtenerDescripcionTipoEvaluacion(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                }
                else
                {
                    //DataTable DTMensajes = null;
                    //DTMensajes = new DataTable();
                    //DTMensajes = oGeneralBL.obtenerMensajeporId(4);
                    //lblMsjeBeca.Text = DTMensajes.Rows[0][2].ToString();

                    this.rblAnioAca.DataSource = null;
                    this.rblAnioAca.DataBind();                  
                    this.imgBtnNext.Visible = false;
                    this.lbltextPeriodo.Visible = false;
                    this.lblTextModa.Visible = false;
                    this.rblBeca.Visible = false;
                    this.lblMsjeBeca.Visible = false;
                    this.lblBeca.Visible = false;
                    this.lblMsjeEvaluacion.Visible = false;

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                     + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtPeriodoPostulacion = null;
            }

            return rpta;
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
                    this.rblBeca.DataSource = null;
                    this.rblBeca.DataBind();               
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                     + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtBecaPostulacion = null;
            }
        }

        private bool CargaPeriodoPostulacionNuevo()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtPeriodoPostulacion;
            bool rpta = false;

            try
            {
                oGeneralBL = new GeneralBL();
                dtPeriodoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.PERIODOACA].Key, Session["usrRedId"].ToString(), null);
                rpta = (dtPeriodoPostulacion != null && dtPeriodoPostulacion.Rows.Count > 0);

                if (rpta)
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
                        this.ObtenerDescripcionTipoEvaluacion(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));//CMCS RQ70241
                        this.CargaModalidadPostulacionPeriodo(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                    }
                    this.imgBtnNext.Visible = false;
                                     

                    dtPeriodoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPOEVALUA].Key, Session["usrRedId"].ToString(), null);

                    rpta = (dtPeriodoPostulacion != null && dtPeriodoPostulacion.Rows.Count > 0);
                    if(rpta)
                    {
                        this.rblEvaluacion.DataValueField = "codigo";
                        this.rblEvaluacion.DataTextField = "descripcion";
                        this.rblEvaluacion.DataSource = dtPeriodoPostulacion;
                        this.rblEvaluacion.DataBind();
                        if (dtPeriodoPostulacion.Rows.Count == 1)
                        {
                            this.rblEvaluacion.SelectedValue = dtPeriodoPostulacion.Rows[0][0].ToString();
                        }
                    }
                }
                else
                {
                    //DataTable DTMensajes = null;
                    //DTMensajes = new DataTable();
                    //this.lblMensajeModalidad.Text = UIConstantes.Alert.msgModalidadNoDisponible;                    
                    //DTMensajes = oGeneralBL.obtenerMensajeporId(4);
                    //lblMensajeModalidad.Text = DTMensajes.Rows[0][2].ToString();

                    this.rblAnioAca.DataSource = null;
                    this.rblAnioAca.DataBind();
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
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                     + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtPeriodoPostulacion = null;
            }

            return rpta;
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
                string anioperiodo = "";
                if (rblAnioAca.SelectedItem != null)
                {
                    anioperiodo = rblAnioAca.SelectedItem.ToString();
                }
                if (!String.IsNullOrEmpty(anioperiodo))
                {
                    dtDescModalidad = oGeneralBL.ObtenerDescripcionModalidadMensaje(IdModalidad, null, ApplicationFormSettingId, anioperiodo);
                }
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
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                    + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtDescModalidad = null;
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
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                   + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oAplicanteBL = null;
                dtCarreras = null;
            }
        }


        private void CargaTipoEvaluacion()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtPeriodoPostulacion;
            try
            {
                oGeneralBL = new GeneralBL();
                dtPeriodoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPOEVALUA].Key, Session["usrRedId"].ToString(), null);

               
                if (dtPeriodoPostulacion != null && dtPeriodoPostulacion.Rows.Count > 0)
                {
                    this.rblEvaluacion.DataValueField = "codigo";
                    this.rblEvaluacion.DataTextField = "descripcion";
                    this.rblEvaluacion.DataSource = dtPeriodoPostulacion;
                    this.rblEvaluacion.DataBind();
                    if (dtPeriodoPostulacion.Rows.Count == 1)
                    {
                        this.rblEvaluacion.SelectedValue = dtPeriodoPostulacion.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                   + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtPeriodoPostulacion = null;
            }
        }
         

        private void CargaModalidadPostulacionPeriodo(Int32? IdPeriodo)
        {
            GeneralBL oGeneralBL = null;
            DataTable dtTipoPostulacion;
            try
            {
                oGeneralBL = new GeneralBL();
                dtTipoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()
                    [UIConstantes.TIPO_CODIGO.TIPO_POSTULACION_PREGRADO2].Key, Session["usrRedId"].ToString()
                    , null, IdPeriodo);

                if (dtTipoPostulacion != null && dtTipoPostulacion.Rows.Count > 0)
                {
                    this.rblModalidad.DataValueField = "codigo";
                    this.rblModalidad.DataTextField = "descripcion";
                    this.rblModalidad.DataSource = dtTipoPostulacion;
                    this.rblModalidad.DataBind();

                    //this.imgBtnNext.Visible = true; /*Se comenta:Christian Ramirez - REQ91569*/
                }
                else
                {
                    DataTable DTMensajes = null;
                    DTMensajes = new DataTable();
                    this.rblModalidad.DataSource = null;
                    this.rblModalidad.DataBind();                   
                    DTMensajes = oGeneralBL.obtenerMensajeporId(4);
                    lblMensajeModalidad.Text = DTMensajes.Rows[0][2].ToString();
                    //this.imgBtnNext.Visible = false; /*Se comenta:Christian Ramirez - REQ91569*/
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                   + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtTipoPostulacion = null;
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

                if (dtDescAviso != null && dtDescAviso.Rows.Count > 0) this.lblAviso.Text = dtDescAviso.Rows[0][0].ToString();
                else this.lblAviso.Text = "";
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                   + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtDescAviso = null;
            }
        }

        private void ObtenerDescripcionTipoEvaluacion(Int32? IdPeriodo)
        {
            GeneralBL oGeneralBL = null;
            DataTable dtDescAviso = null;
            Int32? ApplicationFormSettingId = null;
            try
            {
                ApplicationFormSettingId = UIConstantes._IdPostulacionPreGrado;

                oGeneralBL = new GeneralBL();
                dtDescAviso = oGeneralBL.ObtenerDescTipoEvaluacion(IdPeriodo, ApplicationFormSettingId);

                if (dtDescAviso != null && dtDescAviso.Rows.Count > 0) this.lblMsjeEvaluacion.Text = dtDescAviso.Rows[0][0].ToString();
                else this.lblMsjeEvaluacion.Text = "";
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                   + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtDescAviso = null;
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
                    if (dtDescBeca.Rows[0][0].ToString() == "")
                        {
                            this.rblBeca.Visible = false;
                            this.lblBeca.Visible = false;
                    } 
                }
                else
                {
                    this.lblMsjeBeca.Text = "";
                    this.rblBeca.Visible = false;
                    this.lblBeca.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] 
                    + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtDescBeca = null;
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
                }
                else
                {
                    this.lblMsjePeriodo.Text = "";
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oGeneralBL = null;
                dtDescPeriodo = null;
            }
        }

        private AplicanteBE ObtenerDatosColegio(AplicanteBE oAplicanteBE)
        {
            oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"].ToString());
            oAplicanteBE.RedId = Session["usrRedId"].ToString();
            oAplicanteBE.SituacionAcademica = UIConvertNull.Int32(ddlSitAcademica.SelectedValue);
            oAplicanteBE.TipoColeProvId = UIConvertNull.Int32(rblEvaluacion.SelectedValue.ToString());
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

        private void GuardarDatosColegio()
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
                        if (PaginaActual == UIConstantes.Formularios.F21)
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
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
        }

        private void GuardarDatosModalidad()
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
               

                String correoSalida = (ConfigurationManager.AppSettings["CorresoSalidaAdmPre"] != null ? ConfigurationManager.AppSettings["CorresoSalidaAdmPre"] 
                    : string.Empty);

                oAplicanteBL = new AplicanteBL();
                Int32? AplicanteId = oAplicanteBL.InsertaDatosFormDos_ModPostul(oAplicanteBE, null);
                Session["AplicanteId"] = AplicanteId;

                if (AplicanteId != 0 && AplicanteId != null)
                {
                    /*Ini:Christian Ramirez - REQ91569*/
                    int situacionAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);

                    if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
                    {
                        oAplicanteBE.IdAplicante = AplicanteId;
                        oAplicanteBE.ListaRendimientoAcademicoBE = ObtenerDatosCompentencia();

                        RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
                        int resultado = oRendimientoAcademicoBL.InsertarDatosFormVeintiUno_CantidadCompetencia(oAplicanteBE);

                        if (resultado > 0) Response.Redirect("frm04_DatoPersonal.aspx", false);
                        else
                        {
                            Session["EstadoEnvio"] = null;
                            Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                                [UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                        }
                    }
                    else
                        Response.Redirect("frm04_DatoPersonal.aspx", false);
                    /*Fin:Christian Ramirez - REQ91569*/

                }
                else
                {
                    Session["EstadoEnvio"] = null;
                    Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                        [UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"])
                    , UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                    [UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] + "&descError=" + ex.Message.Replace("\n", ""), false);
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
                                        ddlSitAcademica.CssClass = "txtTextoCombo Deshabilitado";

                                        if (recEducacion.Institucion != null)
                                        {
                                            txtCodColegio1.Text = recEducacion.Institucion.Codigo.ToString();
                                            txtColegio1.Text = recEducacion.NombreInstitucion;
                                            txtCodigoModular1.Text = recEducacion.Institucion.CodigoModular;
                                            txtDireccionColegio1.Text = recEducacion.Institucion.Direccion + UIConstantes._valorSignoGuion + recEducacion.Institucion.Distrito + UIConstantes._valorSignoGuion + recEducacion.Institucion.Provincia;
                                            pnlColegio1.Visible = true;
                                            BtnElimina1.Visible = false;
                                            txtColegio1.Enabled = false;
                                            txtColegio1.CssClass = "txtCajaTexto txtSensitive Deshabilitado";
                                        }
                                        else
                                        {
                                            txtCodColegio1.Text = UIConstantes.idValorNulo.ToString();
                                            txtColegio1.Text = recEducacion.NombreInstitucion;
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
                                            BtnElimina2.Visible = false;
                                            txtColegio2.Enabled = false;
                                            txtColegio2.CssClass = "txtCajaTexto txtSensitive Deshabilitado";
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
                                            btnElimina3.Visible = false;
                                            txtColegio3.Enabled = false;
                                            txtColegio3.CssClass = "txtCajaTexto txtSensitive Deshabilitado";
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
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] + "&descError=" + ex.Message.Replace("\n", ""), false);
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
                LLenarDatosColegioRegistrado(oAplicanteBE);

                /*Ini:Christian Ramirez - REQ91569*/
                oAplicanteBE.IdAplicante = AplicanteId;
                RecuperarNotaADsCompetenciaRegistrada(oAplicanteBE);
                /*Fin:Christian Ramirez - REQ91569*/

                if (oAplicanteBE != null && oAplicanteBE.LEducacion != null)
                {
					/*IniChristian Ramirez - REQ91569*/
                    //divModalidadPostulacion.Attributes.Add("style", "display:block");
                    //trInformacionModalidad.Visible = false;
                    ddlSitAcademica.Enabled = false;
					/*Fin:Christian Ramirez - REQ91569*/
                    ddlNroColegios.Enabled = false;
                    ddlNroColegios.CssClass = "txtTextoCombo Deshabilitado";
                }
                else
                {
                    hddAplicaValidacion.Value = "0";
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
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
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_ELIMINAR_REGISTRO] + "&descError=" + ex.Message.Replace("\n", ""), false);
                return Resultado;
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
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
        }

        private void cargarCombos()
        {
            cargarComboEgresado();
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
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
        }

        private void MostrarModadalidadNoDisponible()
        {
            trColegioProcedencia.Visible = false;
            trInformacionModalidad.Visible = false;
            //trModadlidadPostulacion.Visible = false;
            trCamposObligatorios.Visible = false;
            divBotones.Visible = false;
            lblMensajeAvisoModalidad.Text = UIConstantes.Alert.msgPeriodoNoDisponible;
            lblMensajeColegio.Text = UIConstantes.Alert.msgPeriodoNoDisponible;
            divModalidadPostulacion.Attributes.Add("style", "display:block");
            btnAgregarModalidad.Visible = false; /*Comentado:Christian Ramirez - REQ91569*/
        }

        #region Metodos => Notas Competencia
        /*Ini:Christian Ramirez - REQ91569*/
        private List<RendimientoAcademicoBE> ObtenerDatosCompentencia()
        {
            string CodigoModular = txtCodigoModular1.Text;
            string SessionPeriodId = rblAnioAca.SelectedValue;
            string DegreeId = rblModalidad.SelectedValue;
            string TotalCompentencias = txtTotalCompetencia.Text;
            string TotalCompetencia_A = txtAs.Text;
            string TotalCompetencia_B = txtBs.Text;
            string TotalCompetencia_C = txtCs.Text;
            string TotalCompetencia_AD = txtAds.Text;

            return new List<RendimientoAcademicoBE>
            {
                new RendimientoAcademicoBE() {
                    CodigoModular = CodigoModular,
                    SessionPeriodId = Convert.ToInt32(SessionPeriodId),
                    DegreeId = string.IsNullOrEmpty(DegreeId) ? 0 : Convert.ToInt32(DegreeId),
                    TotalCompentencias = string.IsNullOrEmpty(TotalCompentencias) ? 0 : Convert.ToInt32(TotalCompentencias),
                    TotalCompetencia_A = string.IsNullOrEmpty(TotalCompetencia_A) ? 0 : Convert.ToInt32(TotalCompetencia_A),
                    TotalCompetencia_B = string.IsNullOrEmpty(TotalCompetencia_B) ? 0 : Convert.ToInt32(TotalCompetencia_B),
                    TotalCompetencia_C = string.IsNullOrEmpty(TotalCompetencia_C) ? 0 : Convert.ToInt32(TotalCompetencia_C),
                    TotalCompetencia_AD = string.IsNullOrEmpty(TotalCompetencia_AD) ? 0 : Convert.ToInt32(TotalCompetencia_AD)
                }
            };
        }

        private void LimpiarControleFormularioNoColegio()
        {
            txtAds.Text = "0";
            txtAs.Text = "0";
            txtBs.Text = "0";
            txtCs.Text = "0";
            txtTotalCompetencia.Text = "0";
            rblAnioAca.Items.Clear();
            rblModalidad.Items.Clear();
            rblCarrera.Items.Clear();
            trModadlidadPostulacion.Attributes.Add("style", "display:none");
            trBotonOpcion.Attributes.Add("style", "display:contents");
        }

        private void ValidarYMostrarModalidadPostulacion()
        {
            List<RendimientoAcademicoBE> oRendimientoAcademicoBE = ObtenerDatosCompentencia();
            RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
            DataTable dtModalidades = new DataTable();

            dtModalidades = oRendimientoAcademicoBL.ObtenerModalidadesPorCompetencia(Session["usrRedId"].ToString(), 
                oRendimientoAcademicoBE[0]);

            if (dtModalidades.Rows.Count > 0)
                Funciones.CargarRadioButtonList(rblModalidad, dtModalidades, "Codigo", "Descripcion");
            else
                rblModalidad.Items.Clear();
        }

        private bool ValidarFormulario(ref string mensajeError)
        {
            bool rpta = false;

            if (ddlSitAcademica == null || ddlSitAcademica.SelectedIndex == 0)
            {
                mensajeError = "Debe seleccionar una opción en Situacion Académica";
                return rpta;
            }

            if (ddlNroColegios == null || ddlNroColegios.SelectedIndex == 0)
            {
                mensajeError = "Debe seleccionar un colegio";
                return rpta;
            }

            if (string.IsNullOrEmpty(txtCodColegio1.Text))
            {
                mensajeError = "Digite un colegio";
                return rpta;
            }

            #region validar periodo academico
            bool rblAnioAcaSelected = false;
            for (int i = 0; i < rblAnioAca.Items.Count; i++)
            {
                rblAnioAcaSelected = rblAnioAca.Items[i].Selected;
                if (rblAnioAcaSelected) break;
            }

            if (rblAnioAcaSelected == false)
            {
                mensajeError = "Seleccione un periodo académico";
                return rpta;
            }
            #endregion

            #region validar modalidad
            bool rblModalidadSelected = false;
            for (int i = 0; i < rblModalidad.Items.Count; i++)
            {
                rblModalidadSelected = rblModalidad.Items[i].Selected;
                if (rblModalidadSelected) break;
            }
            
            if (rblModalidadSelected == false)
            {
                mensajeError = "Seleccione una modalidad";
                return rpta;
            }
            #endregion

            #region validar carrera
            bool rblCarreraSelected = false;
            for (int i = 0; i < rblCarrera.Items.Count; i++)
            {
                rblCarreraSelected = rblCarrera.Items[i].Selected;
                if (rblCarreraSelected) break;
            }

            if (rblCarreraSelected == false)
            {
                mensajeError = "Seleccione una carrera";
                return rpta;
            }
            #endregion

            rpta = true;
            return rpta;
        }

        private void MostrarBotonesProceso()
        {
            string mensajeError = "";
            bool rpta = ValidarFormulario(ref mensajeError);
            if (rpta)
            {
                imgBtnNext.Visible = true;
                imgBtnNext.Enabled = true;
            }
            else
            {
                imgBtnNext.Visible = false;
                imgBtnNext.Enabled = false;
            }
        }

        private void HabilitarDeshabilitarTextoNotasCompetencia(bool condicion)
        {
            if (condicion)
            {
                txtAds.CssClass = "txtCajaTexto";
                txtAs.CssClass = "txtCajaTexto";
                txtBs.CssClass = "txtCajaTexto";
                txtCs.CssClass = "txtCajaTexto";
                txtTotalCompetencia.CssClass = "txtCajaTexto";
            }
            else
            {
                txtAds.CssClass = "txtCajaTexto Deshabilitado";
                txtAs.CssClass = "txtCajaTexto Deshabilitado";
                txtBs.CssClass = "txtCajaTexto Deshabilitado";
                txtCs.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCompetencia.CssClass = "txtCajaTexto Deshabilitado";
            }

            txtAds.Enabled = condicion;
            txtAs.Enabled = condicion;
            txtBs.Enabled = condicion;
            txtCs.Enabled = condicion;
            txtTotalCompetencia.Enabled = condicion;

        }

        private void RecuperarNotaADsCompetenciaRegistrada(AplicanteBE oAplicanteBE)
        {
            int situacionAcadecmica = Convert.ToInt32(oAplicanteBE.LEducacion[0].SituacionAcademica);
            if (situacionAcadecmica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
            {
                RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
                RendimientoAcademicoBE oRendimientoAcademicoBE = oRendimientoAcademicoBL.ObtenerNotasADsPorCompetenciaRegistrada(oAplicanteBE);

                if (oRendimientoAcademicoBE != null)
                {
                    txtTotalCompetencia.Text = oRendimientoAcademicoBE.TotalCompentencias.ToString();
                    txtAds.Text = oRendimientoAcademicoBE.TotalCompetencia_AD.ToString();
                    txtAs.Text = oRendimientoAcademicoBE.TotalCompetencia_A.ToString();
                    txtBs.Text = oRendimientoAcademicoBE.TotalCompetencia_B.ToString();
                    txtCs.Text = oRendimientoAcademicoBE.TotalCompetencia_C.ToString();
                }

                trCompetencias.Attributes.Add("style", "display:contents");
                HabilitarDeshabilitarTextoNotasCompetencia(false);
                btnEditarCantidadCompetencia.Visible = false;
                spMensajeBotonEditar.Visible = false;
                lblMensajeCantidadCompentenciaAD01.Visible = false;
                lblMensajeModalidadPostulacion01.Visible = false;
            }
        }
		/*Fin:Christian Ramirez - REQ91569*/
        #endregion
        

        #endregion


        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (!IsPostBack)
                {
                    #region cargar metodos comun
                    ConfigurarControles();
                    MostrarOcultarBotones(false);
                    HabilitaControles();
                    HabilitarControlesMayusculas();
                    LimpiarControles();
                    #endregion

                    #region cargar datos modalidad
                    GeneralBL oGeneralBL = new GeneralBL();
                    if (Session["AplicanteId"] != null)
                    {
                        bool rptaPeriodo = CargaPeriodoPostulacionContinuacion();
                        if (rptaPeriodo)
                        {
                            divModalidadPostulacion.Attributes.Add("style", "display:block");
                            bool rptaModalidad =  CargaModalidadPostulacionContinuacion();
                            if (rptaModalidad)
                            {
                                Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                                rblModalidad.Enabled = false;
                                CargaBecaPostulacionNuevo();
                                recuperaModalidadRegistrada(AplicanteId);
                                cargarCombos();
                                trInformacionModalidad.Visible = false;
                                CargarColegiosRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                                trModadlidadPostulacion.Attributes.Add("style", "display:inline");
                            }
                            else
                            {
                                trColegioProcedencia.Visible = false;
                                trInformacionModalidad.Visible = false;
                                trCamposObligatorios.Visible = false;
                                divBotones.Visible = false;
                                lblMensajeColegio.Text = UIConstantes.Alert.msgModalidadNoDisponible;
                            }
                        }
                        else
                        {
                            MostrarModadalidadNoDisponible();
                        }
                    }
                    else
                    {
                        bool rptaPeriodo  = CargaPeriodoPostulacionNuevo();

                        if (rptaPeriodo)
                        {
                            CargaBecaPostulacionNuevo();
                            rblModalidad.Enabled = true;
                            cargarCombos();
                            hddAplicaValidacion.Value = "1";
                        }
                        else
                        {
                            MostrarModadalidadNoDisponible();

                            DataTable DTMensajes = oGeneralBL.obtenerMensajeporId(4);
                            lblMensajeModalidad.Text = DTMensajes.Rows[0][2].ToString();
                            lblMensajeDB.Visible = true;
                            lblTituloMensajeDB.Visible = true;
                            lblMensajeDB.Text = DTMensajes.Rows[0][2].ToString();
                            
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        protected void rblModalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdModalidad = 0;
            try
            {
                if (string.IsNullOrEmpty(txtCodigoModular1.Text))
                    MostrarPopupAviso("Ingrese un colegio");
                else
                {
                    this.rblCarrera.Visible = true;
                    rblCarrera.Attributes.Add("style", "display:block");
                    MostrarBotonesProceso(); /*Agregado:Christian Ramirez - REQ91569*/

                    this.MostrarOcultarBotonesModalidad(true);
                    /*Ini:Christian Ramirez - REQ91569*/
                    //trInformacionModalidad.Visible = false; 
                    //this.imgBtnNext.Visible = true;
                    //divModalidadPostulacion.Attributes.Add("style", "display:block");
                    /*Fin:Christian Ramirez - REQ91569*/
                    string codModalidad = rblModalidad.SelectedValue;
                    IdModalidad = UIConvertNull.Int32(rblModalidad.SelectedValue);
                    Session["ModPostulacion"] = UIConvertNull.Int32(rblModalidad.SelectedValue);
                    this.CargaCarreraPorModalidad(IdModalidad);
                    this.ObtenerDescripcionModalidad(IdModalidad);




                    /*Fin:Christian Ramirez - REQ91569*/
                    string codModular = txtCodigoModular1.Text; //String.IsNullOrEmpty(hddCodModular.Value) ? "" : hddCodModular.Value;
                    string anioAcademico = "";
                    anioAcademico = rblAnioAca.Items[rblAnioAca.SelectedIndex].Text.Split('-')[0];

                    string script = $"ValidarColegio('{codModular}','{anioAcademico}');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "script", script, true);
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "js", String.Format("ValidarColegio('{0}')", codModular), true);
                    /*Ini:Christian Ramirez - REQ91569*/


                    string rpta2 = "";
                    EducacionBL oEducacionBL = new EducacionBL();
                    rpta2 = oEducacionBL.ObtenerTipoColegioLimaProv(codModular, codModalidad);

                    //Session["colegioProv"] = UIConvertNull.Int32(rpta2);
                    string acadYear = "";
                    acadYear = UIConvertNull.String(rblAnioAca.SelectedItem);
                    if (rpta2 != "0" && acadYear != "2021-01" && acadYear != "2020-02")
                    {
                        trTipoEvaluacion.Attributes.Add("style", "display:inline");
                        //trTipoEvaluacion2.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        trTipoEvaluacion.Attributes.Add("style", "display:none");
                        //trTipoEvaluacion2.Attributes.Add("style", "display:inline");
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        protected void rblAnioAca_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdPeriodo = 0;
            //String yearAcad = "";
            
            try
            {
                if (string.IsNullOrEmpty(txtCodigoModular1.Text))
                    MostrarPopupAviso("Ingrese un colegio");
                else
                {

                    IdPeriodo = UIConvertNull.Int32(rblAnioAca.SelectedValue);
                    this.ObtenerDescripcionPeriodo(IdPeriodo);
                    this.ObtenerDescripcionBeca(IdPeriodo);
                    this.ObtenerDescripcionAviso(IdPeriodo);
                    //this.rblCarrera.Visible = false; /*Christian Ramirez - GIIT - 20200129*/
                    this.lblDescripcionModalidad.Text = "";

                    /*Ini:Christian Ramirez - REQ91569*/
                    imgBtnNext.Visible = false;
                    int situacionAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);
                    rblModalidad.Attributes.Add("style", "display:contents");
                    rblCarrera.Items.Clear();

                    if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
                    {
                        #region 02 validar y mostrar modalidades
                        ValidarYMostrarModalidadPostulacion();
                        rblCarrera.Items.Clear();
                        //trModadlidadPostulacion.Attributes.Add("style", "display:contents");
                        #endregion
                    }
                    else
                    {
                        this.CargaModalidadPostulacionPeriodo(IdPeriodo);

                        /*Christian Ramirez - GIIT - 20200129*/
                        string anioAcademico = "";
                        anioAcademico = rblAnioAca.Items[rblAnioAca.SelectedIndex].Text.Split('-')[0];

                        string script = $"ValidarColegio('{txtCodigoModular1.Text}','{anioAcademico}');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "script", script, true);

                        //rblCarrera.Attributes.Add("style", "display:none");
                        //trTipoEvaluacion.Attributes.Add("style", "display:none");
                        /*Christian Ramirez - GIIT - 20200129*/
                    }
                    /*Fin:Christian Ramirez - REQ91569*/
                }

            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"])
                    , UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                    [UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        protected void imgBtnVideo_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            String URL = "frmVideoInformativo.aspx";
            String vtn = "window.open('" + URL + "','video','scrollbars=yes,resizable=no,location=no,width=640,height=380')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }

        protected void ddlNroColegios_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? NroColegios = null;
            try
            {
                NroColegios = UIConvertNull.Int32(ddlNroColegios.SelectedValue);
                CargarColegios(NroColegios);
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message.Replace("\n", "")
                    , UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));

                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                    [UIConstantes.TIPO_ERROR.ERROR_GENERAL] + "&descError=" + ex.Message.Replace("\n", ""), false);
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

            LimpiarControleFormularioNoColegio(); /*Agregado:Christian Ramirez - REQ91569*/
        }

        protected void btnElimina2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEducacion = UIConvertNull.Int32(txtIdEducacion2.Text);

            if (IdEducacion != null) this.EliminaColegioRegistrado(IdEducacion);
            else
            {
                this.pnlColegio2.Visible = false;
                this.pnlColegio3.Visible = false;
                //divModalidadPostulacion.Attributes.Add("style", "display:block"); /*Comentado:Christian Ramirez - REQ91569*/

                if (Session["AplicanteId"] != null) this.CargarColegiosRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
            }
        }

        protected void btnElimina3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEducacion = UIConvertNull.Int32(txtIdEducacion3.Text);
            if (IdEducacion != null) this.EliminaColegioRegistrado(IdEducacion);
            else
            {
                this.pnlColegio3.Visible = false;
                //divModalidadPostulacion.Attributes.Add("style", "display:block"); /*Comentado:Christian Ramirez - REQ91569*/

                if (Session["AplicanteId"] != null) this.CargarColegiosRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
            }
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
                    if (PaginaActual == UIConstantes.Formularios.F21)
                    {
                        PaginaAnterior = dtPagAnterior.Rows[i - 1]["NombreFormulario"].ToString();
                        break;
                    }
                }
                Response.Redirect(PaginaAnterior, false);
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
        }

        protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                GuardarDatosModalidad();
                GuardarDatosColegio();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message.Replace("\n", ""), 
                    UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));

                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                    [UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void ddlSitAcademica_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region comentado
            //GeneralBL oGeneralBL = null;
            //DataTable dtPeriodoPostulacion;
            //bool rpta = false;
            //oGeneralBL = new GeneralBL();
            //dtPeriodoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()
            //    [UIConstantes.TIPO_CODIGO.PERIODOACA].Key, Session["usrRedId"].ToString(), null);
            //rpta = (dtPeriodoPostulacion != null && dtPeriodoPostulacion.Rows.Count > 0);

            //if (rpta)
            //{
            //    string SitAca = ddlSitAcademica.SelectedValue;
            //    this.rblAnioAca.DataValueField = "codigo";
            //    this.rblAnioAca.DataTextField = "descripcion";
            //    this.rblAnioAca.DataSource = dtPeriodoPostulacion;
            //    this.rblAnioAca.DataBind();

            //    if (dtPeriodoPostulacion.Rows.Count == 1)
            //    {
            //        this.rblAnioAca.SelectedValue = dtPeriodoPostulacion.Rows[0][0].ToString();
            //        this.rblAnioAca.Enabled = false;
            //        this.ObtenerDescripcionPeriodo(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
            //        this.ObtenerDescripcionBeca(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
            //        this.ObtenerDescripcionAviso(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
            //        this.ObtenerDescripcionTipoEvaluacion(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));//CMCS RQ70241
            //        this.CargaModalidadPostulacionPeriodo(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
            //    }
            #endregion

            txtColegio1.Text = "";
            txtCodigoModular1.Text = "";
            txtDireccionColegio1.Text = "";
            lblMensajeModalidad.Text = "";
            //}

            this.lblDescripcionModalidad.Text = "";
            rblModalidad.ClearSelection();
            this.rblCarrera.DataSource = null;
            this.rblCarrera.DataBind();


            /*Ini:Christian Ramirez - REQ91569*/
            LimpiarControleFormularioNoColegio();

            if (ddlSitAcademica.SelectedIndex != 0)
            {
                trInformacionModalidad.Attributes.Add("style", "display:contents");
                int situacionAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);

                if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
                {
                    lblInformacionModalidad.Text = "Ingrese su colegio actual y las cantidades de " +
                            "competencia de 4to año para que pueda seleccionar la modalidad de postulación";
                    trCompetencias.Attributes.Add("style", "display:contents");
                }
                else
                {
                    lblInformacionModalidad.Text = "Ingrese su colegio actual para que pueda seleccionar la modalidad de postulación";
                    trCompetencias.Attributes.Add("style", "display:none");
                }
            }
            else
                trInformacionModalidad.Attributes.Add("style", "display:none");
            /*Fin:Christian Ramirez - REQ91569*/
        }

        #region Eventos => Notas Competencia
        /*Ini:Christian Ramirez - REQ91569*/
        protected void btnAgregarModalidad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoModular1.Text))
                MostrarPopupAviso("Ingrese un colegio");
            else
            {
                GeneralBL oGeneralBL = null;
                DataTable dtPeriodoPostulacion;
                bool rpta = false;
                oGeneralBL = new GeneralBL();

                /*Ini:Christian Ramirez - REQ95075*/
                dtPeriodoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()
                    [UIConstantes.TIPO_CODIGO.PERIODOACA].Key, Session["usrRedId"].ToString(), null, Convert.ToInt32(ddlSitAcademica.SelectedValue));
                /*Fin:Christian Ramirez - REQ95075*/

                rpta = (dtPeriodoPostulacion != null && dtPeriodoPostulacion.Rows.Count > 0);

                if (rpta)
                {
                    string SitAca = ddlSitAcademica.SelectedValue;
                    this.rblAnioAca.DataValueField = "codigo";
                    this.rblAnioAca.DataTextField = "descripcion";
                    this.rblAnioAca.DataSource = dtPeriodoPostulacion;
                    this.rblAnioAca.DataBind();

                    if (dtPeriodoPostulacion.Rows.Count == 1)
                    {
                        /*Ini:Christian Ramirez - REQ94617*/
                        //this.rblAnioAca.SelectedValue = dtPeriodoPostulacion.Rows[0][0].ToString();
                        this.rblAnioAca.Enabled = true;
                        /*Fin:Christian Ramirez - REQ94617*/
                        this.ObtenerDescripcionPeriodo(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                        this.ObtenerDescripcionBeca(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                        this.ObtenerDescripcionAviso(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                        this.ObtenerDescripcionTipoEvaluacion(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));//CMCS RQ70241
                        this.CargaModalidadPostulacionPeriodo(UIConvertNull.Int32(dtPeriodoPostulacion.Rows[0][0].ToString()));
                    }
                }
                /*Ini:Christian Ramirez - REQ95075*/
                else
                {
                    MostrarPopupAviso("No tiene Periodo Activo o no está asociada para la Situacion Academica seleccionada");
                }
                /*Fin:Christian Ramirez - REQ95075*/

                rblCarrera.Items.Clear();
                rblModalidad.Items.Clear();

                divModalidadPostulacion.Attributes.Add("style", "display:contents");
                rblModalidad.Attributes.Add("style", "display:none");
                trBotonOpcion.Attributes.Add("style", "display:none");
                trModadlidadPostulacion.Attributes.Add("style", "display:contents");

                HabilitarDeshabilitarTextoNotasCompetencia(false);
            }
        }

        protected void btnCerrarPopuAviso_Click(object sender, EventArgs e)
        {
            mpeAviso.Hide();
        }

        protected void btnEditarCantidadCompetencia_Click(object sender, EventArgs e)
        {
            HabilitarDeshabilitarTextoNotasCompetencia(true);
            LimpiarControleFormularioNoColegio();
        }

        protected void rblCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoModular1.Text))
                MostrarPopupAviso("Ingrese un colegio");
            else
            {
                MostrarBotonesProceso();
            }
        }
        #endregion
        /*Fin:Christian Ramirez - REQ91569*/
        #endregion


    }
}