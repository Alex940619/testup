using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
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
        public static List<EducacionBE> getColegios(String term)
        {
            EducacionBL oEducacionBL = null;
            try
            {
                String txtDegreeId = HttpContext.Current.Session["ModPostulacion"].ToString();
                oEducacionBL = new EducacionBL();
                var lista = oEducacionBL.ListarColegios(term, txtDegreeId);
                return lista;
            }
            catch (Exception ex)
            {
                ex.ToString();
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
                        //trTipoEvaluacion.Attributes.Add("style", "display:none");
                    }
                    
                        this.CargaCondicionAcademica();
                    if (dtModalidad.Rows[0][7].ToString()!=null)
                    {
                        this.rblCondAcademica.SelectedValue = dtModalidad.Rows[0][7].ToString();
                        this.rblCondAcademica.Enabled = false;
                        this.rblCondAcademica.Visible = false;
                        this.lblTextCondAcademica.Visible = false;
                        this.rblEvaluacion.Enabled = false;
                    }
                     
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
                string ObtenerTipoCodigo = UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.PERIODOACA].Key;
                dtPeriodoPostulacion = oGeneralBL.ObtenerTipoCodigoPC(ObtenerTipoCodigo, Session["usrRedId"].ToString(), null);

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
        private void CargaCondicionAcademica()
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtCondicionAcademica = null;
            Int32 SettingId = 7;
            try
            {
                oAplicanteBL = new AplicanteBL();
                dtCondicionAcademica = oAplicanteBL.ListarCondicionAcademica();
                if (dtCondicionAcademica != null && dtCondicionAcademica.Rows.Count > 0)
                {
                    this.rblCondAcademica.DataValueField = "IdCondicionAcademica";
                    this.rblCondAcademica.DataTextField = "CondicionAcademica";
                    this.rblCondAcademica.DataSource = dtCondicionAcademica;
                    this.rblCondAcademica.DataBind();

                    this.lblMsjeCondAcademic.Visible = false;
                }
                else
                {
                    this.rblCondAcademica.DataSource = null;
                    this.rblCondAcademica.DataBind();

                    this.lblMsjeCondAcademic.Visible = true;
                    this.lblMsjeCondAcademic.Text = UIConstantes.Alert.msgCarreraNoDisponible;
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
                dtCondicionAcademica = null;
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
            oAplicanteBE.TipoColeProvId = Convert.ToInt16(hddColegio1TipoEvaluacionECL.Value); /*Se agrega:Christian Ramirez - REQ110609*/
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


            try
            {
                oAplicanteBE = new AplicanteBE();
                oAplicanteBE = ObtenerDatosColegio(oAplicanteBE);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormCuatro_ColegioProc(oAplicanteBE);

                if (operacionOK)
                {
                    //Response.Redirect("frm04_DatoPersonal.aspx", false); //Comentado JC.DelgadoV 20220119


                    //Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                    //oGeneralBL = new GeneralBL();
                    //dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    //for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    //{
                    //    PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                    //    if (PaginaActual == UIConstantes.Formularios.F21)
                    //    {
                    //        PaginaSiguiente = dtPagSigui.Rows[i + 1]["NombreFormulario"].ToString();
                    //        break;
                    //    }
                    //}
                    //SitAcademica = ddlSitAcademica.SelectedValue;
                    //Response.Redirect(PaginaSiguiente + "?SitAcademica=" + SitAcademica, false);
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

        private void GuardarDatosRendimiento()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;

            Session["ModPostulacion"] = UIConvertNull.Int32(rblModalidad.SelectedValue);

            GeneralBL oGeneralBL = null;
            DataTable dtPagSigui = null;
            String SitAcademica, PaginaActual, PaginaSiguiente = null;

            //Guardando Rendimiento
            Int32? AplicanteId = Convert.ToInt32(Session["AplicanteId"]);

            //Obteniendo Datos de colegio registrado
            DataTable dtColegios = new DataTable();
            try
            {
                oAplicanteBL = new AplicanteBL();
                dtColegios = oAplicanteBL.LLenarColegioRegistradoCombo(UIConvertNull.Int32(Session["AplicanteId"].ToString()));

                oAplicanteBE = new AplicanteBE();

                //Obtener datos de rendimiento 
                oAplicanteBE = obtenerDatosInformacionAcademica(oAplicanteBE, AplicanteId, dtColegios);

                Boolean operacionOK = oAplicanteBL.InsertaDatosFormVeintiuno_OrdenMerito(oAplicanteBE);
                //*************************

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
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"])
                    , UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                    [UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] + "&descError=" + ex.Message.Replace("\n", ""), false);

                throw ex;
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
                oAplicanteBE.IdCondicionAcademica= UIConvertNull.Int32(rblCondAcademica.SelectedValue.ToString());


                String correoSalida = (ConfigurationManager.AppSettings["CorresoSalidaAdmPre"] != null ? ConfigurationManager.AppSettings["CorresoSalidaAdmPre"]
                    : string.Empty);

                oAplicanteBL = new AplicanteBL();
                Int32? AplicanteId = oAplicanteBL.InsertaDatosFormDos_ModPostul(oAplicanteBE, null);
                Session["AplicanteId"] = AplicanteId;

                if (AplicanteId != 0 && AplicanteId != null)
                {
                    /*Ini:Christian Ramirez - REQ91569*/
                    int situacionAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);

                    //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                    if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA || situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.ESTUDIANTE)
                    //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                    {
                        oAplicanteBE.IdAplicante = AplicanteId;

                        //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                        if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
                        {
                            oAplicanteBE.SituacionAcademica = (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA;
                            oAplicanteBE.ListaRendimientoAcademicoBE = ObtenerDatosCompentencia();
                        }

                        if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.ESTUDIANTE)
                        {
                            oAplicanteBE.SituacionAcademica = (int)UIConstantes.SITUACION_ACADEMICA.ESTUDIANTE;
                            oAplicanteBE.ListaRendimientoAcademicoBE = ObtenerDatosCompentenciaEstudiante();
                        }
                        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

                        RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
                        int resultado = oRendimientoAcademicoBL.InsertarDatosFormVeintiUno_CantidadCompetencia(oAplicanteBE);

                        //if (resultado > 0) Response.Redirect("frm04_DatoPersonal.aspx", false);
                        //else
                        //{
                        //    Session["EstadoEnvio"] = null;
                        //    Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                        //        [UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                        //}

                        if (resultado > 0)
                        {

                        }
                        else
                        {
                            Session["EstadoEnvio"] = null;
                            Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                                [UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                        }
                    }
                    //else
                    //    Response.Redirect("frm04_DatoPersonal.aspx", false); //Comentado JC.Delgado 20220119
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

        private AplicanteBE obtenerDatosInformacionAcademica(AplicanteBE oAplicanteBE, int? ApplicationId, DataTable dtColegios)
        {
            EducacionDetalleBE DetColegioTercero = null;
            EducacionDetalleBE DetColegioCuarto = null;
            EducacionDetalleBE DetColegioQuinto = null;

            //Obteniendo el IdApplicationEducation
            int idApplicationEducation = Convert.ToInt32(dtColegios.Rows[0]["ApplicationEducationId"]);
            //

            /*Ini:Christian Ramirez -REQ91569*/
            //int tipoCalificacionTercero = Convert.ToInt32(ddlTipoCalificacionTercero.SelectedValue);
            //int tipoCalificacionCuarto = Convert.ToInt32(ddlTipoCalificacionCuarto.SelectedValue);
            //int tipoCalificacionQuinto = Convert.ToInt32(ddlTipoCalificacionQuinto.SelectedValue);
            /*Fin:Christian Ramirez -REQ91569*/

            #region Tercer Grado
            if (DetColegioTercero == null) DetColegioTercero = new EducacionDetalleBE();
            DetColegioTercero.IdApplication = UIConvertNull.Int32(ApplicationId);
            DetColegioTercero.IdApplicationEducation = UIConvertNull.Int32(idApplicationEducation);
            //DetColegioTercero.IdApplicationEducationEnroll = UIConvertNull.Int32(txtIdApplicationEducationEnrollTercero.Text);
            DetColegioTercero.IdGrado = Int32.Parse(UIConstantes.GRADO_PREGRADO.TERCERO_SECUNDARIA.ToString("D"));  //Tercero - 33

            #region comentado
            //String tmpFechaIniTercero = "01/01/" + this.ddlAnioLectivoTercero.SelectedItem.Text;
            //CultureInfo culturaIniTercero = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            //DateTime tempFechaIniTercero;

            //if (DateTime.TryParse(tmpFechaIniTercero, culturaIniTercero, System.Globalization.DateTimeStyles.None, out tempFechaIniTercero))
            //    DetColegioTercero.FechaInicio = UIConvertNull.DateTime(tmpFechaIniTercero);

            //String tmpFechaFinTercero = "31/12/" + this.ddlAnioLectivoTercero.SelectedItem.Text;
            //CultureInfo culturaFinTercero = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            //DateTime tempFechaFinTercero;

            //if (DateTime.TryParse(tmpFechaFinTercero, culturaFinTercero, System.Globalization.DateTimeStyles.None, out tempFechaFinTercero))
            //    DetColegioTercero.FechaFin = UIConvertNull.DateTime(tmpFechaFinTercero);
            #endregion

            DetColegioTercero.SituaAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);
            DetColegioTercero.IdMerito = Convert.ToInt32(ddlSitAcademica.SelectedValue) == 9 ?
                                            UIConvertNull.Int32(txtOrdenMeritoTercero_Competencias.Text) : UIConvertNull.Int32(txtOrdenMeritoTercero.Text);

            DetColegioTercero.CantidadEstudiantes = Convert.ToInt32(ddlSitAcademica.SelectedValue) == 9 ?
                                                        UIConvertNull.Int32(txtTotalAlumnosTercero_Competencias.Text) : UIConvertNull.Int32(txtTotalAlumnosTercero.Text);


            /*Ini:Christian Ramirez -REQ91569*/
            #region comentado
            //if (ddlNotaMateTercero.SelectedValue == UIConstantes._valorNotaOtro) CMCS RQ89808
            //string notaMateTercero = txtNotaMateTercero.Text.Trim();
            //if (notaMateTercero == "AD" || notaMateTercero == "A" || notaMateTercero == "B" || notaMateTercero == "C")
            //    DetColegioTercero.OtraNotaMateTercero = notaMateTercero;
            //else
            //    DetColegioTercero.NotaMateTercero = UIConvertNull.Int32(notaMateTercero);

            //if (ddlNotaLengTercero.SelectedValue == UIConstantes._valorNotaOtro) CMCS RQ89808
            //string notaLengTercero = txtNotaLengTercero.Text.Trim();
            //if (notaLengTercero == "AD" || notaLengTercero == "A" || notaLengTercero == "B" || notaLengTercero == "C")
            //    DetColegioTercero.OtraNotaLengTercero = notaLengTercero;
            //else
            //    DetColegioTercero.NotaLengTercero = UIConvertNull.Int32(notaLengTercero);
            #endregion

            //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
            DetColegioTercero = ObtenerTipoCalificacionPorSituacionAcademica(ref DetColegioTercero);
            #region comentado
            //DetColegioTercero.CodTipoCalificacion = 1; //1: Numérico - 2: Letras
            //DetColegioTercero.DescTipoCalificacion = "Numérico";
            #endregion
            //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

            /*Fin:Christian Ramirez -REQ91569*/

            DetColegioTercero.Revision_Opid = Session["usrRedId"].ToString();
            //}
            #endregion



            #region Cuarto Grado
            //if (tipoCalificacionCuarto == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO)
            //{
            if (DetColegioCuarto == null) DetColegioCuarto = new EducacionDetalleBE();
            DetColegioCuarto.IdApplication = UIConvertNull.Int32(ApplicationId);
            DetColegioCuarto.IdApplicationEducation = UIConvertNull.Int32(idApplicationEducation);
            //DetColegioCuarto.IdApplicationEducationEnroll = UIConvertNull.Int32(txtIdApplicationEducationEnrollCuarto.Text);
            DetColegioCuarto.IdGrado = Int32.Parse(UIConstantes.GRADO_PREGRADO.CUARTO_SECUNDARIA.ToString("D"));  //Cuarto - 8

            #region comentado
            //String tmpFechaIniCuarto = "01/01/" + this.ddlAnioLectivoCuarto.SelectedItem.Text;
            //CultureInfo culturaIniCuarto = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            //DateTime tempFechaIniCuarto;

            //if (DateTime.TryParse(tmpFechaIniCuarto, culturaIniCuarto, System.Globalization.DateTimeStyles.None, out tempFechaIniCuarto))
            //    DetColegioCuarto.FechaInicio = UIConvertNull.DateTime(tmpFechaIniCuarto);

            //String tmpFechaFinCuarto = "31/12/" + this.ddlAnioLectivoCuarto.SelectedItem.Text;
            //CultureInfo culturaFinCuarto = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            //DateTime tempFechaFinCuarto;
            //if (DateTime.TryParse(tmpFechaIniCuarto, culturaFinCuarto, System.Globalization.DateTimeStyles.None, out tempFechaFinCuarto))
            //    DetColegioCuarto.FechaFin = UIConvertNull.DateTime(tmpFechaFinCuarto);
            #endregion

            DetColegioCuarto.SituaAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);
            DetColegioCuarto.IdMerito = UIConvertNull.Int32(txtOrdenMeritoCuarto.Text);
            DetColegioCuarto.CantidadEstudiantes = UIConvertNull.Int32(txtTotalAlumnosCuarto.Text);

            /*Ini:Christian Ramirez -REQ91569*/
            #region comentado
            //if (ddlNotaMateCuarto.SelectedValue == UIConstantes._valorNotaOtro)CMCS 89808
            //string notaMateCuarto = txtNotaMateCuarto.Text.Trim();
            //if (notaMateCuarto == "AD" || notaMateCuarto == "A" || notaMateCuarto == "B" || notaMateCuarto == "C")
            //    DetColegioCuarto.OtraNotaMateCuarto = notaMateCuarto;
            //else
            //    DetColegioCuarto.NotaMateCuarto = UIConvertNull.Int32(notaMateCuarto);

            ////if (ddlNotaLengCuarto.SelectedValue == UIConstantes._valorNotaOtro) CMCS 89808
            //string notaLengCuarto = txtNotaLengCuarto.Text.Trim();
            //if (notaLengCuarto == "AD" || notaLengCuarto == "A" || notaLengCuarto == "B" || notaLengCuarto == "C")
            //    DetColegioCuarto.OtraNotaLengCuarto = notaLengCuarto;
            //else
            //    DetColegioCuarto.NotaLengCuarto = UIConvertNull.Int32(notaLengCuarto);
            #endregion

            //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
            DetColegioCuarto = ObtenerTipoCalificacionPorSituacionAcademica(ref DetColegioCuarto);
            #region comentado
            //DetColegioCuarto.CodTipoCalificacion = 1; //1: Numérico - 2: Letras
            //DetColegioCuarto.DescTipoCalificacion = "Numérico"; 
            #endregion
            //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

            /*Fin:Christian Ramirez -REQ91569*/

            DetColegioCuarto.Revision_Opid = Session["usrRedId"].ToString();
            //}
            #endregion



            #region Quinto Grado
            //if (tipoCalificacionQuinto == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO)
            //{
            if (DetColegioQuinto == null) DetColegioQuinto = new EducacionDetalleBE();
            DetColegioQuinto.IdApplication = UIConvertNull.Int32(ApplicationId);
            DetColegioQuinto.IdApplicationEducation = UIConvertNull.Int32(idApplicationEducation);
            //DetColegioQuinto.IdApplicationEducationEnroll = UIConvertNull.Int32(txtIdApplicationEducationEnrollQuinto.Text);
            DetColegioQuinto.IdGrado = Int32.Parse(UIConstantes.GRADO_PREGRADO.QUINTO_SECUNDARIA.ToString("D"));  //Quinto - 9

            #region comentado
            //String tmpFechaIniQuinto = "01/01/" + this.ddlAnioLectivoQuinto.SelectedItem.Text;
            //CultureInfo culturaIniQuinto = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            //DateTime tempFechaIniQuinto;

            //if (DateTime.TryParse(tmpFechaIniQuinto, culturaIniQuinto, System.Globalization.DateTimeStyles.None, out tempFechaIniQuinto))
            //    DetColegioQuinto.FechaInicio = UIConvertNull.DateTime(tmpFechaIniQuinto);

            //String tmpFechaFinQuinto = "31/12/" + this.ddlAnioLectivoQuinto.SelectedItem.Text;
            //CultureInfo culturaFinQuinto = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            //DateTime tempFechaFinQuinto;
            //if (DateTime.TryParse(tmpFechaIniQuinto, culturaFinQuinto, System.Globalization.DateTimeStyles.None, out tempFechaFinQuinto))
            //    DetColegioQuinto.FechaFin = UIConvertNull.DateTime(tmpFechaFinQuinto);
            #endregion

            DetColegioQuinto.SituaAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);
            DetColegioQuinto.IdMerito = UIConvertNull.Int32(txtOrdenMeritoQuinto.Text);
            DetColegioQuinto.CantidadEstudiantes = UIConvertNull.Int32(txtTotalAlumnosQuinto.Text);

            /*Ini:Christian Ramirez -REQ91569*/
            #region comentado
            //if (ddlNotaMateQuinto.SelectedValue == UIConstantes._valorNotaOtro) RQ 89808
            //string notaMateQuinto = txtNotaMateQuinto.Text;
            //if (notaMateQuinto == "AD" || notaMateQuinto == "A" || notaMateQuinto == "B" || notaMateQuinto == "C")
            //    DetColegioQuinto.OtraNotaMateQuinto = notaMateQuinto;
            //else
            //    DetColegioQuinto.NotaMateQuinto = UIConvertNull.Int32(notaMateQuinto == "0" ? null : notaMateQuinto);

            ////if (ddlNotaLengQuinto.SelectedValue == UIConstantes._valorNotaOtro) RQ 89808
            //string notaLengQuinto = txtNotaLengQuinto.Text.Trim();
            //if (notaLengQuinto == "AD" || notaLengQuinto == "A" || notaLengQuinto == "B" || notaLengQuinto == "C")
            //    DetColegioQuinto.OtraNotaLengQuinto = notaLengQuinto;
            //else
            //    DetColegioQuinto.NotaLengQuinto = UIConvertNull.Int32(notaLengQuinto == "0" ? null : notaLengQuinto);
            #endregion

            //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
            DetColegioQuinto = ObtenerTipoCalificacionPorSituacionAcademica(ref DetColegioQuinto);
            #region comentado
            //DetColegioQuinto.CodTipoCalificacion = 1; //1: Numérico - 2: Letras
            //DetColegioQuinto.DescTipoCalificacion = "Numérico"; 
            #endregion
            //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

            /*Fin:Christian Ramirez -REQ91569*/

            DetColegioQuinto.Revision_Opid = Session["usrRedId"].ToString();
            //}
            #endregion



            //========================================================
            if (oAplicanteBE.LDetalleEducacion == null)
                oAplicanteBE.LDetalleEducacion = new System.Collections.Generic.List<EducacionDetalleBE>();

            if (oAplicanteBE != null)
            {
                if (oAplicanteBE.LDetalleEducacion == null) oAplicanteBE.LDetalleEducacion = new List<EducacionDetalleBE>();
                if (DetColegioTercero != null) oAplicanteBE.LDetalleEducacion.Add(DetColegioTercero);
                if (Convert.ToInt32(ddlSitAcademica.SelectedValue) != 9)
                {
                    if (DetColegioCuarto != null) oAplicanteBE.LDetalleEducacion.Add(DetColegioCuarto);
                    if (DetColegioQuinto != null) oAplicanteBE.LDetalleEducacion.Add(DetColegioQuinto);
                }
            }
            return oAplicanteBE;
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
                                        if (Convert.ToInt32(ddlSitAcademica.SelectedValue)== (int)UIConstantes.SITUACION_ACADEMICA.ADMISION_PRE_PACIFICO)
                                        {
                                            this.rblCondAcademica.Visible = true;
                                            this.lblTextCondAcademica.Visible = true;
                                        }

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

                //Llenando Rendimiento
                oAplicanteBE = new AplicanteBL().ObtenerRendAcademicoRegistrado(oAplicanteBE.IdAplicante);
                this.CargarRendimientoAcademicoRegistrado(oAplicanteBE);
                HabilitarDeshabilitarTextoNotasCompetencia(false);
                //


            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
        }

        private void CargarRendimientoAcademicoRegistrado(AplicanteBE oAplicanteBE)
        {
            try
            {
                this.LLenarDatosRendimeintoAcademicoRegistrado(oAplicanteBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosRendimeintoAcademicoRegistrado(AplicanteBE oAplicanteBE)
        {
            try
            {
                if (oAplicanteBE != null && oAplicanteBE.LDetalleEducacion != null)
                {
                    List<EducacionDetalleBE> LRendAcademico = (from itemEduca in oAplicanteBE.LDetalleEducacion
                                                               orderby itemEduca.IdDetalleEducacion
                                                               select itemEduca).ToList<EducacionDetalleBE>();

                    if (LRendAcademico != null)
                    {
                        for (int indice = 0; indice < LRendAcademico.Count; indice++)
                        {
                            EducacionDetalleBE recEducacionDetalle = LRendAcademico[indice];
                            string codTipoCalificacion = recEducacionDetalle.CodTipoCalificacion.ToString(); //

                            if (codTipoCalificacion == UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO.ToString("D"))
                                LLenarDatosRendimientoAcademicoRegistradoPorGradoNumerico(recEducacionDetalle);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosRendimientoAcademicoRegistradoPorGradoNumerico(EducacionDetalleBE recEducacionDetalle)
        {
            int situacionAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);

            if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
            {
                if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.TERCERO_SECUNDARIA.ToString("D"))
                {
                    this.txtOrdenMeritoTercero_Competencias.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                    this.txtTotalAlumnosTercero_Competencias.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                }
            }
            else if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.EGRESADO)
            {
                if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.TERCERO_SECUNDARIA.ToString("D"))
                {
                    this.txtOrdenMeritoTercero.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                    this.txtTotalAlumnosTercero.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                }

                if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.CUARTO_SECUNDARIA.ToString("D"))
                {
                    this.txtOrdenMeritoCuarto.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                    this.txtTotalAlumnosCuarto.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                }

                if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.QUINTO_SECUNDARIA.ToString("D"))
                {
                    this.txtOrdenMeritoQuinto.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                    this.txtTotalAlumnosQuinto.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                }

                trRendimiento.Attributes.Add("style", "display:contents");
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
                if (UIConvertNull.Int32(Session["ModPostulacion"]) == UIConstantes.Modalidad.TallerEPU ||
                    UIConvertNull.Int32(Session["ModPostulacion"]) == UIConstantes.Modalidad.ProgramaEPU)
                {
                    oGeneralBL = new GeneralBL();
                    DataTable dtEgresado = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.ESTA_ACADEMICO].Key,
                        "", null);

                    if (dtEgresado != null && dtEgresado.Rows.Count > 0)
                    {
                        Funciones.cargarComboYSeleccione(this.ddlSitAcademica, dtEgresado.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                    }
                }
                else
                {
                    oGeneralBL = new GeneralBL();
                    string ESTA_ACADEMICOMOD = (UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.ESTA_ACADEMICOMOD].Key);
                    DataTable dtEgresado = oGeneralBL.ObtenerTipoCodigoPC(ESTA_ACADEMICOMOD,"", null, ModalidadId);

                    if (dtEgresado != null && dtEgresado.Rows.Count > 0)
                    {
                        Funciones.cargarComboYSeleccione(this.ddlSitAcademica, dtEgresado.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO]
                    + "&descError=" + ex.Message.Replace("\n", ""), false);
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

            string OrdenMeritoTercero = txtOrdenMeritoTercero_Competencias.Text;
            string NroAlumnosPromocionTercero = txtTotalAlumnosTercero_Competencias.Text;

            string TotalCompetencia_AD_Cuarto = txtTotalADCuarto.Text;
            string TotalCompetencia_A_Cuarto = txtTotalACuarto.Text;
            string TotalCompetencia_B_Cuarto = txtTotalBCuarto.Text;
            string TotalCompetencia_C_Cuarto = txtTotalCCuarto.Text;
            string TotalCompetencia_Cuarto = txtTotalCompetenciasCuarto.Text;

            string TotalCompetencia_AD_Quinto = txtTotalADQuinto.Text;
            string TotalCompetencia_A_Quinto = txtTotalAQuinto.Text;
            string TotalCompetencia_B_Quinto = txtTotalBQuinto.Text;
            string TotalCompetencia_C_Quinto = txtTotalCQuinto.Text;
            string TotalCompetencia_Quinto = txtTotalCompetenciasQuinto.Text;

            return new List<RendimientoAcademicoBE>
            {
                new RendimientoAcademicoBE() {
                    CodigoModular = CodigoModular,
                    SessionPeriodId = Convert.ToInt32(SessionPeriodId),
                    DegreeId = string.IsNullOrEmpty(DegreeId) ? 0 : Convert.ToInt32(DegreeId),

                    OrdenMeritoTercero = string.IsNullOrEmpty(OrdenMeritoTercero) ? 0 : Convert.ToInt32(OrdenMeritoTercero),
                    NroAlumnosPromocionTercero = string.IsNullOrEmpty(NroAlumnosPromocionTercero) ? 0 : Convert.ToInt32(NroAlumnosPromocionTercero),
                    TotalCompetencia_AD_Cuarto = string.IsNullOrEmpty(TotalCompetencia_AD_Cuarto) ? 0 : Convert.ToInt32(TotalCompetencia_AD_Cuarto),
                    TotalCompetencia_A_Cuarto = string.IsNullOrEmpty(TotalCompetencia_A_Cuarto) ? 0 : Convert.ToInt32(TotalCompetencia_A_Cuarto),
                    TotalCompetencia_B_Cuarto = string.IsNullOrEmpty(TotalCompetencia_B_Cuarto) ? 0 : Convert.ToInt32(TotalCompetencia_B_Cuarto),
                    TotalCompetencia_C_Cuarto = string.IsNullOrEmpty(TotalCompetencia_C_Cuarto) ? 0 : Convert.ToInt32(TotalCompetencia_C_Cuarto),
                    TotalCompentencias_Cuarto = string.IsNullOrEmpty(TotalCompetencia_Cuarto) ? 0 : Convert.ToInt32(TotalCompetencia_Cuarto),
                    TotalCompetencia_AD_Quinto = string.IsNullOrEmpty(TotalCompetencia_AD_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_AD_Quinto),
                    TotalCompetencia_A_Quinto = string.IsNullOrEmpty(TotalCompetencia_A_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_A_Quinto),
                    TotalCompetencia_B_Quinto = string.IsNullOrEmpty(TotalCompetencia_B_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_B_Quinto),
                    TotalCompetencia_C_Quinto = string.IsNullOrEmpty(TotalCompetencia_C_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_C_Quinto),
                    TotalCompentencias_Quinto = string.IsNullOrEmpty(TotalCompetencia_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_Quinto),

                }
            };
        }

        private List<RendimientoAcademicoBE> ObtenerDatosRendimiento()
        {
            string CodigoModular = txtCodigoModular1.Text;
            string SessionPeriodId = rblAnioAca.SelectedValue;
            string DegreeId = rblModalidad.SelectedValue;

            string OrdenMeritoTercero = txtOrdenMeritoTercero.Text;
            string NroAlumnosPromocionTercero = txtTotalAlumnosTercero.Text;

            string OrdenMeritoCuarto = txtOrdenMeritoCuarto.Text;
            string NroAlumnosPromocionCuarto = txtTotalAlumnosCuarto.Text;

            string OrdenMeritoQuinto = txtOrdenMeritoQuinto.Text;
            string NroAlumnosPromocionQuinto = txtTotalAlumnosQuinto.Text;


            return new List<RendimientoAcademicoBE>
            {
                new RendimientoAcademicoBE() {
                    CodigoModular = CodigoModular,
                    SessionPeriodId = Convert.ToInt32(SessionPeriodId),
                    DegreeId = string.IsNullOrEmpty(DegreeId) ? 0 : Convert.ToInt32(DegreeId),

                    OrdenMeritoTercero = string.IsNullOrEmpty(OrdenMeritoTercero) ? 0 : Convert.ToInt32(OrdenMeritoTercero),
                    NroAlumnosPromocionTercero = string.IsNullOrEmpty(NroAlumnosPromocionTercero) ? 0 : Convert.ToInt32(NroAlumnosPromocionTercero),
                    OrdenMeritoCuarto = string.IsNullOrEmpty(OrdenMeritoCuarto) ? 0 : Convert.ToInt32(OrdenMeritoCuarto),
                    NroAlumnosPromocionCuarto = string.IsNullOrEmpty(NroAlumnosPromocionCuarto) ? 0 : Convert.ToInt32(NroAlumnosPromocionCuarto),
                    OrdenMeritoQuinto = string.IsNullOrEmpty(OrdenMeritoQuinto) ? 0 : Convert.ToInt32(OrdenMeritoQuinto),
                    NroAlumnosPromocionQuinto = string.IsNullOrEmpty(NroAlumnosPromocionQuinto) ? 0 : Convert.ToInt32(NroAlumnosPromocionQuinto)
                }
            };
        }

        private void LimpiarControleFormularioNoColegio()
        {
            //Competencias
            txtOrdenMeritoTercero_Competencias.Text = "0";
            txtTotalAlumnosTercero_Competencias.Text = "0";

            txtTotalADCuarto.Text = "0";
            txtTotalACuarto.Text = "0";
            txtTotalBCuarto.Text = "0";
            txtTotalCCuarto.Text = "0";
            txtTotalCompetenciasCuarto.Text = "0";

            txtTotalADQuinto.Text = "0";
            txtTotalAQuinto.Text = "0";
            txtTotalBQuinto.Text = "0";
            txtTotalCQuinto.Text = "0";
            txtTotalCompetenciasQuinto.Text = "0";

            //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
            //Competencias Estudiante
            txtTotalADTerceroEstudiante.Text = "0";
            txtTotalATerceroEstudiante.Text = "0";
            txtTotalBTerceroEstudiante.Text = "0";
            txtTotalCTerceroEstudiante.Text = "0";
            txtTotalCompetenciasTerceroEstudiante.Text = "0";

            txtTotalADCuartoEstudiante.Text = "0";
            txtTotalACuartoEstudiante.Text = "0";
            txtTotalBCuartoEstudiante.Text = "0";
            txtTotalCCuartoEstudiante.Text = "0";
            txtTotalCompetenciasCuartoEstudiante.Text = "0";

            txtTotalADQuintoEstudiante.Text = "0";
            txtTotalAQuintoEstudiante.Text = "0";
            txtTotalBQuintoEstudiante.Text = "0";
            txtTotalCQuintoEstudiante.Text = "0";
            txtTotalCompetenciasQuintoEstudiante.Text = "0";
            //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

            //Rendimiento
            txtOrdenMeritoTercero.Text = "0";
            txtTotalAlumnosTercero.Text = "0";

            txtOrdenMeritoCuarto.Text = "0";
            txtTotalAlumnosCuarto.Text = "0";

            txtOrdenMeritoQuinto.Text = "0";
            txtTotalAlumnosQuinto.Text = "0";

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
            {
                Funciones.CargarRadioButtonList(rblModalidad, dtModalidades, "Codigo", "Descripcion");
            }
            else
            {
                rblModalidad.Items.Clear();
                mpeNoModalidad.Show();
            }

        }

        private void ValidarYMostrarModalidadPostulacionEgresado()
        {
            List<RendimientoAcademicoBE> oRendimientoAcademicoBE = ObtenerDatosRendimiento();
            RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
            DataTable dtModalidades = new DataTable();

            dtModalidades = oRendimientoAcademicoBL.ObtenerModalidadesPorRendimiento(Session["usrRedId"].ToString(),
                oRendimientoAcademicoBE[0]);

            if (dtModalidades.Rows.Count > 0)
            {
                Funciones.CargarRadioButtonList(rblModalidad, dtModalidades, "Codigo", "Descripcion");
            }
            else
            {
                rblModalidad.Items.Clear();
                mpeNoModalidad.Show();
            }

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
                //Competencias
                txtOrdenMeritoTercero_Competencias.CssClass = "txtCajaTexto";
                txtTotalAlumnosTercero_Competencias.CssClass = "txtCajaTexto";

                txtTotalADCuarto.CssClass = "txtCajaTexto";
                txtTotalACuarto.CssClass = "txtCajaTexto";
                txtTotalBCuarto.CssClass = "txtCajaTexto";
                txtTotalCCuarto.CssClass = "txtCajaTexto";
                txtTotalCompetenciasCuarto.CssClass = "txtCajaTexto";

                txtTotalADQuinto.CssClass = "txtCajaTexto";
                txtTotalAQuinto.CssClass = "txtCajaTexto";
                txtTotalBQuinto.CssClass = "txtCajaTexto";
                txtTotalCQuinto.CssClass = "txtCajaTexto";
                txtTotalCompetenciasQuinto.CssClass = "txtCajaTexto";

                //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                //Competencias Estudiante
                txtTotalADTerceroEstudiante.CssClass = "txtCajaTexto";
                txtTotalATerceroEstudiante.CssClass = "txtCajaTexto";
                txtTotalBTerceroEstudiante.CssClass = "txtCajaTexto";
                txtTotalCTerceroEstudiante.CssClass = "txtCajaTexto";
                txtTotalCompetenciasTerceroEstudiante.CssClass = "txtCajaTexto";

                txtTotalADCuartoEstudiante.CssClass = "txtCajaTexto";
                txtTotalACuartoEstudiante.CssClass = "txtCajaTexto";
                txtTotalBCuartoEstudiante.CssClass = "txtCajaTexto";
                txtTotalCCuartoEstudiante.CssClass = "txtCajaTexto";
                txtTotalCompetenciasCuartoEstudiante.CssClass = "txtCajaTexto";

                txtTotalADQuintoEstudiante.CssClass = "txtCajaTexto";
                txtTotalAQuintoEstudiante.CssClass = "txtCajaTexto";
                txtTotalBQuintoEstudiante.CssClass = "txtCajaTexto";
                txtTotalCQuintoEstudiante.CssClass = "txtCajaTexto";
                txtTotalCompetenciasQuintoEstudiante.CssClass = "txtCajaTexto";
                //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

                //Rendimiento
                txtOrdenMeritoTercero.CssClass = "txtCajaTexto";
                txtTotalAlumnosTercero.CssClass = "txtCajaTexto";

                txtOrdenMeritoCuarto.CssClass = "txtCajaTexto";
                txtTotalAlumnosCuarto.CssClass = "txtCajaTexto";

                txtOrdenMeritoQuinto.CssClass = "txtCajaTexto";
                txtTotalAlumnosQuinto.CssClass = "txtCajaTexto";

            }
            else
            {
                //Competencias
                txtOrdenMeritoTercero_Competencias.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalAlumnosTercero_Competencias.CssClass = "txtCajaTexto Deshabilitado";

                txtTotalADCuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalACuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalBCuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCCuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCompetenciasCuarto.CssClass = "txtCajaTexto Deshabilitado";

                txtTotalADQuinto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalAQuinto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalBQuinto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCQuinto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCompetenciasQuinto.CssClass = "txtCajaTexto Deshabilitado";

                //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                //Competencias Estudiante
                txtTotalADTerceroEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalATerceroEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalBTerceroEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCTerceroEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCompetenciasTerceroEstudiante.CssClass = "txtCajaTexto Deshabilitado";

                txtTotalADCuartoEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalACuartoEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalBCuartoEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCCuartoEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCompetenciasCuartoEstudiante.CssClass = "txtCajaTexto Deshabilitado";

                txtTotalADQuintoEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalAQuintoEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalBQuintoEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCQuintoEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCompetenciasQuintoEstudiante.CssClass = "txtCajaTexto Deshabilitado";
                //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

                //Rendimiento
                txtOrdenMeritoTercero.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalAlumnosTercero.CssClass = "txtCajaTexto Deshabilitado";

                txtOrdenMeritoCuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalAlumnosCuarto.CssClass = "txtCajaTexto Deshabilitado";

                txtOrdenMeritoQuinto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalAlumnosQuinto.CssClass = "txtCajaTexto Deshabilitado";
            }

            //Competencias
            txtOrdenMeritoTercero_Competencias.Enabled = condicion;
            txtTotalAlumnosTercero_Competencias.Enabled = condicion;

            txtTotalADCuarto.Enabled = condicion;
            txtTotalACuarto.Enabled = condicion;
            txtTotalBCuarto.Enabled = condicion;
            txtTotalCCuarto.Enabled = condicion;
            txtTotalCompetenciasCuarto.Enabled = condicion;

            txtTotalADQuinto.Enabled = condicion;
            txtTotalAQuinto.Enabled = condicion;
            txtTotalBQuinto.Enabled = condicion;
            txtTotalCQuinto.Enabled = condicion;
            txtTotalCompetenciasQuinto.Enabled = condicion;

            //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
            //Competencias Estudiante
            txtTotalADTerceroEstudiante.Enabled = condicion;
            txtTotalATerceroEstudiante.Enabled = condicion;
            txtTotalBTerceroEstudiante.Enabled = condicion;
            txtTotalCTerceroEstudiante.Enabled = condicion;
            txtTotalCompetenciasTerceroEstudiante.Enabled = condicion;

            txtTotalADCuartoEstudiante.Enabled = condicion;
            txtTotalACuartoEstudiante.Enabled = condicion;
            txtTotalBCuartoEstudiante.Enabled = condicion;
            txtTotalCCuartoEstudiante.Enabled = condicion;
            txtTotalCompetenciasCuartoEstudiante.Enabled = condicion;

            txtTotalADQuintoEstudiante.Enabled = condicion;
            txtTotalAQuintoEstudiante.Enabled = condicion;
            txtTotalBQuintoEstudiante.Enabled = condicion;
            txtTotalCQuintoEstudiante.Enabled = condicion;
            txtTotalCompetenciasQuintoEstudiante.Enabled = condicion;
            //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

            //Rendimiento
            txtOrdenMeritoTercero.Enabled = condicion;
            txtTotalAlumnosTercero.Enabled = condicion;

            txtOrdenMeritoCuarto.Enabled = condicion;
            txtTotalAlumnosCuarto.Enabled = condicion;

            txtOrdenMeritoQuinto.Enabled = condicion;
            txtTotalAlumnosQuinto.Enabled = condicion;

        }

        private void RecuperarNotaADsCompetenciaRegistrada(AplicanteBE oAplicanteBE)
        {
            int situacionAcadecmica = Convert.ToInt32(oAplicanteBE.LEducacion[0].SituacionAcademica);
            if (situacionAcadecmica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
            {
                RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();

                //Cuarto
                RendimientoAcademicoBE oRendimientoAcademicoBE = oRendimientoAcademicoBL.ObtenerNotasADsPorCompetenciaRegistrada_Cuarto(oAplicanteBE);

                if (oRendimientoAcademicoBE != null)
                {
                    txtTotalCompetenciasCuarto.Text = oRendimientoAcademicoBE.TotalCompentencias_Cuarto.ToString();
                    txtTotalADCuarto.Text = oRendimientoAcademicoBE.TotalCompetencia_AD_Cuarto.ToString();
                    txtTotalACuarto.Text = oRendimientoAcademicoBE.TotalCompetencia_A_Cuarto.ToString();
                    txtTotalBCuarto.Text = oRendimientoAcademicoBE.TotalCompetencia_B_Cuarto.ToString();
                    txtTotalCCuarto.Text = oRendimientoAcademicoBE.TotalCompetencia_C_Cuarto.ToString();
                }

                //Quinto

                oRendimientoAcademicoBE = oRendimientoAcademicoBL.ObtenerNotasADsPorCompetenciaRegistrada_Quinto(oAplicanteBE);

                if (oRendimientoAcademicoBE != null)
                {
                    txtTotalCompetenciasQuinto.Text = oRendimientoAcademicoBE.TotalCompentencias_Quinto.ToString();
                    txtTotalADQuinto.Text = oRendimientoAcademicoBE.TotalCompetencia_AD_Quinto.ToString();
                    txtTotalAQuinto.Text = oRendimientoAcademicoBE.TotalCompetencia_A_Quinto.ToString();
                    txtTotalBQuinto.Text = oRendimientoAcademicoBE.TotalCompetencia_B_Quinto.ToString();
                    txtTotalCQuinto.Text = oRendimientoAcademicoBE.TotalCompetencia_C_Quinto.ToString();
                }

                trCompetencias.Attributes.Add("style", "display:contents");
                HabilitarDeshabilitarTextoNotasCompetencia(false);
                btnEditarCantidadCompetencia.Visible = false;
                spMensajeBotonEditar.Visible = false;
                lblMensajeCantidadCompentenciaAD01.Visible = false;
                lblMensajeModalidadPostulacion01.Visible = false;
            }
            //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
            else if (situacionAcadecmica == (int)UIConstantes.SITUACION_ACADEMICA.ESTUDIANTE)
            {
                RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
                RendimientoAcademicoBE oRendimientoAcademicoBE = null;

                //Cargamos los datos para 3ro
                oRendimientoAcademicoBE = new RendimientoAcademicoBE();
                oRendimientoAcademicoBE = oRendimientoAcademicoBL.ObtenerNotasADsPorCompetenciaRegistrada_Tercero(oAplicanteBE);
                if (oRendimientoAcademicoBE != null)
                {
                    txtTotalCompetenciasTerceroEstudiante.Text = oRendimientoAcademicoBE.TotalCompentencias_Tercero.ToString();
                    txtTotalADTerceroEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_AD_Tercero.ToString();
                    txtTotalATerceroEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_A_Tercero.ToString();
                    txtTotalBTerceroEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_B_Tercero.ToString();
                    txtTotalCTerceroEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_C_Tercero.ToString();
                }

                //Cargamos los datos para 4to
                oRendimientoAcademicoBE = new RendimientoAcademicoBE();
                oRendimientoAcademicoBE = oRendimientoAcademicoBL.ObtenerNotasADsPorCompetenciaRegistrada_Cuarto(oAplicanteBE);
                if (oRendimientoAcademicoBE != null)
                {
                    txtTotalCompetenciasCuartoEstudiante.Text = oRendimientoAcademicoBE.TotalCompentencias_Cuarto.ToString();
                    txtTotalADCuartoEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_AD_Cuarto.ToString();
                    txtTotalACuartoEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_A_Cuarto.ToString();
                    txtTotalBCuartoEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_B_Cuarto.ToString();
                    txtTotalCCuartoEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_C_Cuarto.ToString();
                }

                //Cargamos los datos para 5to
                oRendimientoAcademicoBE = new RendimientoAcademicoBE();
                oRendimientoAcademicoBE = oRendimientoAcademicoBL.ObtenerNotasADsPorCompetenciaRegistrada_Quinto(oAplicanteBE);
                if (oRendimientoAcademicoBE != null)
                {
                    txtTotalCompetenciasQuintoEstudiante.Text = oRendimientoAcademicoBE.TotalCompentencias_Quinto.ToString();
                    txtTotalADQuintoEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_AD_Quinto.ToString();
                    txtTotalAQuintoEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_A_Quinto.ToString();
                    txtTotalBQuintoEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_B_Quinto.ToString();
                    txtTotalCQuintoEstudiante.Text = oRendimientoAcademicoBE.TotalCompetencia_C_Quinto.ToString();
                }

                trCompetenciasEstudiante.Attributes.Add("style", "display:contents");
                btnEditarCantidadCompetenciaEstudiante.Visible = false;
                spMensajeBotonEditarEstudiante.Visible = false;
                lblMensajeCantidadCompentenciaAD01Estudiante.Visible = false;
                lblMensajeModalidadPostulacion01.Visible = false;
            }
            //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565
        }
        /*Fin:Christian Ramirez - REQ91569*/

        //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
        private List<RendimientoAcademicoBE> ObtenerDatosCompentenciaEstudiante()
        {
            string CodigoModular = txtCodigoModular1.Text;
            string CodigoModular2 = txtCodigoModular2.Text;
            string CodigoModular3 = txtCodigoModular3.Text;
            string SessionPeriodId = rblAnioAca.SelectedValue;
            string DegreeId = rblModalidad.SelectedValue;

            string TotalCompetencia_AD_Tercero = txtTotalADTerceroEstudiante.Text;
            string TotalCompetencia_A_Tercero = txtTotalATerceroEstudiante.Text;
            string TotalCompetencia_B_Tercero = txtTotalBTerceroEstudiante.Text;
            string TotalCompetencia_C_Tercero = txtTotalCTerceroEstudiante.Text;
            string TotalCompetencia_Tercero = txtTotalCompetenciasTerceroEstudiante.Text;

            string TotalCompetencia_AD_Cuarto = txtTotalADCuartoEstudiante.Text;
            string TotalCompetencia_A_Cuarto = txtTotalACuartoEstudiante.Text;
            string TotalCompetencia_B_Cuarto = txtTotalBCuartoEstudiante.Text;
            string TotalCompetencia_C_Cuarto = txtTotalCCuartoEstudiante.Text;
            string TotalCompetencia_Cuarto = txtTotalCompetenciasCuartoEstudiante.Text;

            string TotalCompetencia_AD_Quinto = txtTotalADQuintoEstudiante.Text;
            string TotalCompetencia_A_Quinto = txtTotalAQuintoEstudiante.Text;
            string TotalCompetencia_B_Quinto = txtTotalBQuintoEstudiante.Text;
            string TotalCompetencia_C_Quinto = txtTotalCQuintoEstudiante.Text;
            string TotalCompetencia_Quinto = txtTotalCompetenciasQuintoEstudiante.Text;

            return new List<RendimientoAcademicoBE>
            {
                new RendimientoAcademicoBE() {
                    CodigoModular = CodigoModular,
                    CodigoModular2 = CodigoModular2,
                    CodigoModular3 = CodigoModular3,
                    SessionPeriodId = Convert.ToInt32(SessionPeriodId),
                    DegreeId = string.IsNullOrEmpty(DegreeId) ? 0 : Convert.ToInt32(DegreeId),

                    TotalCompetencia_AD_Tercero = string.IsNullOrEmpty(TotalCompetencia_AD_Tercero) ? 0 : Convert.ToInt32(TotalCompetencia_AD_Tercero),
                    TotalCompetencia_A_Tercero = string.IsNullOrEmpty(TotalCompetencia_A_Tercero) ? 0 : Convert.ToInt32(TotalCompetencia_A_Tercero),
                    TotalCompetencia_B_Tercero = string.IsNullOrEmpty(TotalCompetencia_B_Tercero) ? 0 : Convert.ToInt32(TotalCompetencia_B_Tercero),
                    TotalCompetencia_C_Tercero = string.IsNullOrEmpty(TotalCompetencia_C_Tercero) ? 0 : Convert.ToInt32(TotalCompetencia_C_Tercero),
                    TotalCompentencias_Tercero = string.IsNullOrEmpty(TotalCompetencia_Tercero) ? 0 : Convert.ToInt32(TotalCompetencia_Tercero),

                    TotalCompetencia_AD_Cuarto = string.IsNullOrEmpty(TotalCompetencia_AD_Cuarto) ? 0 : Convert.ToInt32(TotalCompetencia_AD_Cuarto),
                    TotalCompetencia_A_Cuarto = string.IsNullOrEmpty(TotalCompetencia_A_Cuarto) ? 0 : Convert.ToInt32(TotalCompetencia_A_Cuarto),
                    TotalCompetencia_B_Cuarto = string.IsNullOrEmpty(TotalCompetencia_B_Cuarto) ? 0 : Convert.ToInt32(TotalCompetencia_B_Cuarto),
                    TotalCompetencia_C_Cuarto = string.IsNullOrEmpty(TotalCompetencia_C_Cuarto) ? 0 : Convert.ToInt32(TotalCompetencia_C_Cuarto),
                    TotalCompentencias_Cuarto = string.IsNullOrEmpty(TotalCompetencia_Cuarto) ? 0 : Convert.ToInt32(TotalCompetencia_Cuarto),

                    TotalCompetencia_AD_Quinto = string.IsNullOrEmpty(TotalCompetencia_AD_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_AD_Quinto),
                    TotalCompetencia_A_Quinto = string.IsNullOrEmpty(TotalCompetencia_A_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_A_Quinto),
                    TotalCompetencia_B_Quinto = string.IsNullOrEmpty(TotalCompetencia_B_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_B_Quinto),
                    TotalCompetencia_C_Quinto = string.IsNullOrEmpty(TotalCompetencia_C_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_C_Quinto),
                    TotalCompentencias_Quinto = string.IsNullOrEmpty(TotalCompetencia_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_Quinto),
                }
            };
        }
        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565
        #endregion


        //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ103573
        private void MostrarColegioPorSituacionAcademica(int idSituacionAcademica)
        {
            ddlNroColegios.Items.Clear();

            if (idSituacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.TRASLADO_GRADUADO ||
                idSituacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.ADMISION_PRE_PACIFICO)
            {
                ddlNroColegios.Items.AddRange(new ListItem[] {
                            new ListItem(){ Value = "0", Text="-- Seleccionar --" },
                            new ListItem(){ Value = "1", Text="1" }
                 });
            }
            else
            {
                ddlNroColegios.Items.AddRange(new ListItem[] {
                        new ListItem(){ Value = "0", Text="-- Seleccionar --" },
                        new ListItem(){ Value = "1", Text="1" },
                        new ListItem(){ Value = "2", Text="2" },
                        new ListItem(){ Value = "3", Text="3" }
                 });
            }

            CargarColegios(0);
        }

        private void ValidarYMostrarModalidadPostulacionGraduadoTraslado()
        {
            int sessionPeriodId = Convert.ToInt32(rblAnioAca.SelectedValue);
            RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
            DataTable dtModalidades = new DataTable();

            dtModalidades = oRendimientoAcademicoBL.ObtenerModalidadesParaGraduadoTraslado(
                Session["usrRedId"].ToString(), sessionPeriodId);

            if (dtModalidades.Rows.Count > 0)
            {
                Funciones.CargarRadioButtonList(rblModalidad, dtModalidades, "Codigo", "Descripcion");
                spnModalidadAviso.InnerText = "";
            }
            else
            {
                rblModalidad.Items.Clear();
                spnModalidadAviso.InnerText = "No hay modalidades activas para seleccionar";
            }

        }

        private void ValidarYMostrarModalidadPostulacionAdmisionPrePacifico()
        {
            int sessionPeriodId = Convert.ToInt32(rblAnioAca.SelectedValue);
            RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
            DataTable dtModalidades = new DataTable();

            dtModalidades = oRendimientoAcademicoBL.ObtenerModalidadesParaAdmisionPrePacifico(
                Session["usrRedId"].ToString(), sessionPeriodId);

            if (dtModalidades.Rows.Count > 0)
            {
                Funciones.CargarRadioButtonList(rblModalidad, dtModalidades, "Codigo", "Descripcion");
                spnModalidadAviso.InnerText = "";
            }
            else
            {
                rblModalidad.Items.Clear();
                spnModalidadAviso.InnerText = "No hay modalidades activas para seleccionar";
            }

        }
        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ103573

        //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
        private void ValidarYMostrarModalidadPostulacionEstudiante()
        {
            List<RendimientoAcademicoBE> oRendimientoAcademicoBE = ObtenerDatosCompentenciaEstudiante();
            RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
            DataTable dtModalidades = new DataTable();

            dtModalidades = oRendimientoAcademicoBL.ObtenerModalidadesPorCompetenciaEstudiante(Session["usrRedId"].ToString(), oRendimientoAcademicoBE[0]);

            if (dtModalidades.Rows.Count > 0) Funciones.CargarRadioButtonList(rblModalidad, dtModalidades, "Codigo", "Descripcion");
            else
            {
                rblModalidad.Items.Clear();
                mpeNoModalidad.Show();
            }

        }

        private EducacionDetalleBE ObtenerTipoCalificacionPorSituacionAcademica(ref EducacionDetalleBE DetColegio)
        {
            int situacionAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);

            switch (situacionAcademica)
            {
                case (int)UIConstantes.SITUACION_ACADEMICA.ESTUDIANTE:
                    DetColegio.CodTipoCalificacion = (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR;
                    DetColegio.DescTipoCalificacion = UIConstantes.ObtenerRendAcademicoTipoCalificacion()
                        [UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR];
                    break;

                case (int)UIConstantes.SITUACION_ACADEMICA.TRASLADO_GRADUADO:
                    DetColegio.CodTipoCalificacion = (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR;
                    DetColegio.DescTipoCalificacion = UIConstantes.ObtenerRendAcademicoTipoCalificacion()
                        [UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR];
                    break;

                case (int)UIConstantes.SITUACION_ACADEMICA.ADMISION_PRE_PACIFICO:
                    DetColegio.CodTipoCalificacion = (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR;
                    DetColegio.DescTipoCalificacion = UIConstantes.ObtenerRendAcademicoTipoCalificacion()
                        [UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR];
                    break;

                default:
                    DetColegio.CodTipoCalificacion = (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO;
                    DetColegio.DescTipoCalificacion = UIConstantes.ObtenerRendAcademicoTipoCalificacion()
                        [UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO];
                    break;
            }

            return DetColegio;
        }
        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

        //[Ini:Christian Ramirez - REQ114900]
        private void GuardarDatosRequisitosDocumentos()
        {
            try
            {
                AplicanteBE oAplicanteBE = null;
                AplicanteBL oAplicanteBL = null;

                oAplicanteBE = new AplicanteBE();
                oAplicanteBE.ModalidadPostulacion = UIConvertNull.Int32(rblModalidad.SelectedValue.ToString());
                oAplicanteBE.RedId = Session["usrRedId"].ToString();
                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"].ToString());

                oAplicanteBL = new AplicanteBL();
                bool operacionOK = oAplicanteBL.InsertaDatosForm21_RequisitoDocumento(oAplicanteBE);

                if (!operacionOK)
                {
                    Session["EstadoEnvio"] = null;
                    string msgError = "No se pudo registrar requisitos documentos: frm21_ModalidadColegioPostula - GuardarDatosRequisitosDocumentos()";

                    UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, msgError, UIConvertNull.String(Session["usrRedId"])
                        , UIConvertNull.String(Session["AplicanteId"]));

                    Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                        [UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                }
            }
            catch (Exception ex)
            {
                Session["EstadoEnvio"] = null;
                string msgError = "Ha ocurrido un error de excepction en el registro: frm21_ModalidadColegioPostula - GuardarDatosRequisitosDocumentos()";

                UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message
                    , UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));

                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                    [UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + msgError, false);
            }
        }
        //[Fin:Christian Ramirez - REQ114900]
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
                            bool rptaModalidad = CargaModalidadPostulacionContinuacion();
                            if (rptaModalidad)
                            {
                                Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                                rblModalidad.Enabled = false;
                                CargaBecaPostulacionNuevo();
                                recuperaModalidadRegistrada(AplicanteId);
                                cargarCombos();
                                trInformacionModalidad.Visible = false;
                                CargarColegiosRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                                OcultarBotonEditar();
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
                        bool rptaPeriodo = CargaPeriodoPostulacionNuevo();

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

        private void OcultarBotonEditar()
        {
            trBtnEditarRendimiento.Attributes.Add("style", "display:none");
            trBtnEditarCompetenciasEstudiante.Attributes.Add("style", "display:none");
            trBtnEditarCompetencias.Attributes.Add("style", "display:none");
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
                    int situacionAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);
                   
                    if (situacionAcademica== (int)UIConstantes.SITUACION_ACADEMICA.ADMISION_PRE_PACIFICO)
                    {
                        lblTextCondAcademica.Visible = true;
                        this.CargaCondicionAcademica();
                    }

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
                        //trTipoEvaluacion.Attributes.Add("style", "display:none");
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
                    this.lblMsjeCondAcademic.Visible = false;
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
                    rblCondAcademica.Items.Clear();
                    lblTextCondAcademica.Visible = false;

                    //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ103573
                    switch (situacionAcademica)
                    {
                        case (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA:
                            #region 02 validar y mostrar modalidades
                            ValidarYMostrarModalidadPostulacion();
                            rblCarrera.Items.Clear();
                            #endregion
                            break;

                        case (int)UIConstantes.SITUACION_ACADEMICA.EGRESADO:
                            ValidarYMostrarModalidadPostulacionEgresado();
                            string anioAcademico = "";
                            anioAcademico = rblAnioAca.Items[rblAnioAca.SelectedIndex].Text.Split('-')[0];

                            string script = $"ValidarColegio('{txtCodigoModular1.Text}','{anioAcademico}');";
                            ScriptManager.RegisterStartupScript(this, GetType(), "script", script, true);
                            break;

                        case (int)UIConstantes.SITUACION_ACADEMICA.TRASLADO_GRADUADO:
                            ValidarYMostrarModalidadPostulacionGraduadoTraslado();
                            rblCarrera.Items.Clear();
                            break;

                        case (int)UIConstantes.SITUACION_ACADEMICA.ADMISION_PRE_PACIFICO:
                            ValidarYMostrarModalidadPostulacionAdmisionPrePacifico();
                            rblCarrera.Items.Clear();
                            break;


                        case (int)UIConstantes.SITUACION_ACADEMICA.ESTUDIANTE:
                            //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                            ValidarYMostrarModalidadPostulacionEstudiante();
                            rblCarrera.Items.Clear();
                            break;
                        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

                        case (int)UIConstantes.SITUACION_ACADEMICA.EXAMEN_ADMISION_REGULAR:
                            //Ini:Christian Ramirez - REQ113651
                            ValidarYMostrarModalidadPostulacionExamenAdmisionRegular();
                            rblCarrera.Items.Clear();
                            break;
                            //Fin:Christian Ramirez - REQ113651
                    }
                    //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ103573
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
                int situacionAcademica = Convert.ToInt32(ddlSitAcademica.SelectedValue);

                if (situacionAcademica== (int)UIConstantes.SITUACION_ACADEMICA.ADMISION_PRE_PACIFICO)
                {
                    if (rblCondAcademica.SelectedValue =="")
                    {
                        lblMsjeCondAcademic.Text = "*Debe elegir una condición académica ";
                        lblMsjeCondAcademic.Visible = true;
                    }
                    else
                    {
                        GuardarDatosModalidad();
                        GuardarDatosColegio();
                        GuardarDatosRendimiento();
                        GuardarDatosRequisitosDocumentos();
                    }
                }
                else
                {
                    GuardarDatosModalidad();
                    GuardarDatosColegio();
                    GuardarDatosRendimiento();
                    GuardarDatosRequisitosDocumentos();
                }
              
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

                //Ini:Christian Ramirez - REQ113651
                switch (situacionAcademica)
                {
                    case (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA:
                        lblInformacionModalidad.Text = "Ingrese su colegio actual y las cantidades de " +
                            "competencias para que pueda seleccionar la modalidad de postulación";
                        trCompetencias.Attributes.Add("style", "display:contents");
                        trRendimiento.Attributes.Add("style", "display:none");
                        trCompetenciasEstudiante.Attributes.Add("style", "display:none"); //Se agrega:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                        break;

                    case (int)UIConstantes.SITUACION_ACADEMICA.TRASLADO_GRADUADO:
                    case (int)UIConstantes.SITUACION_ACADEMICA.ADMISION_PRE_PACIFICO:
                    case (int)UIConstantes.SITUACION_ACADEMICA.EXAMEN_ADMISION_REGULAR:
                        trCompetencias.Attributes.Add("style", "display:none");
                        trRendimiento.Attributes.Add("style", "display:none");
                        trCompetenciasEstudiante.Attributes.Add("style", "display:none"); //Se agrega:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                        break;

                    case (int)UIConstantes.SITUACION_ACADEMICA.ESTUDIANTE:
                        trCompetenciasEstudiante.Attributes.Add("style", "display:contents");
                        trCompetencias.Attributes.Add("style", "display:none");
                        trRendimiento.Attributes.Add("style", "display:none");
                        break;

                    default:
                        lblInformacionModalidad.Text = "Ingrese su colegio actual y su rendimiento para que pueda seleccionar la modalidad de postulación";
                        trCompetencias.Attributes.Add("style", "display:none");
                        trRendimiento.Attributes.Add("style", "display:contents");
                        trCompetenciasEstudiante.Attributes.Add("style", "display:none"); //Se agrega:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                        break;
                }
                //Fin:Christian Ramirez - REQ113651

                #region se comenta
                /*
                if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
                {
                    lblInformacionModalidad.Text = "Ingrese su colegio actual y las cantidades de " +
                            "competencias para que pueda seleccionar la modalidad de postulación";
                    trCompetencias.Attributes.Add("style", "display:contents");
                    trRendimiento.Attributes.Add("style", "display:none");
                    trCompetenciasEstudiante.Attributes.Add("style", "display:none"); //Se agrega:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                }
                //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ103573
                else if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.TRASLADO_GRADUADO || 
                    situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.ADMISION_PRE_PACIFICO)
                {
                    trCompetencias.Attributes.Add("style", "display:none");
                    trRendimiento.Attributes.Add("style", "display:none");
                    trCompetenciasEstudiante.Attributes.Add("style", "display:none"); //Se agrega:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                }
                //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ103573
                //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                else if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.ESTUDIANTE)
                {
                    trCompetenciasEstudiante.Attributes.Add("style", "display:contents");
                    trCompetencias.Attributes.Add("style", "display:none");
                    trRendimiento.Attributes.Add("style", "display:none");
                }
                //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                else
                {
                    lblInformacionModalidad.Text = "Ingrese su colegio actual y su rendimiento para que pueda seleccionar la modalidad de postulación";
                    trCompetencias.Attributes.Add("style", "display:none");
                    trRendimiento.Attributes.Add("style", "display:contents");
                    trCompetenciasEstudiante.Attributes.Add("style", "display:none"); //Se agrega:CHRISTIAN RAMIREZ(EXT09) - REQ104565
                }
                */
                #endregion
            }
            else
                trInformacionModalidad.Attributes.Add("style", "display:none");
            /*Fin:Christian Ramirez - REQ91569*/

            MostrarColegioPorSituacionAcademica(Convert.ToInt32(ddlSitAcademica.SelectedValue)); //Se agrega:CHRISTIAN RAMIREZ(EXT09) - REQ103573
            spnModalidadAviso.InnerText = "";
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
                rblCondAcademica.Items.Clear();
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

        protected void btnEditarRendimiento_Click(object sender, EventArgs e)
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

        //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
        protected void btnEditarCantidadCompetenciaEstudiante_Click(object sender, EventArgs e)
        {
            HabilitarDeshabilitarTextoNotasCompetencia(true);
            LimpiarControleFormularioNoColegio();
        }
        //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565


        //Ini:Christian Ramirez - REQ113651
        private void ValidarYMostrarModalidadPostulacionExamenAdmisionRegular()
        {
            int sessionPeriodId = Convert.ToInt32(rblAnioAca.SelectedValue);
            RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
            DataTable dtModalidades = new DataTable();

            dtModalidades = oRendimientoAcademicoBL.ObtenerModalidadExamenAdmisionRegular(
                Session["usrRedId"].ToString(), sessionPeriodId);

            if (dtModalidades.Rows.Count > 0)
            {
                Funciones.CargarRadioButtonList(rblModalidad, dtModalidades, "Codigo", "Descripcion");
                spnModalidadAviso.InnerText = "";
            }
            else
            {
                rblModalidad.Items.Clear();
                spnModalidadAviso.InnerText = "No hay modalidades activas para seleccionar";
            }
        }
        //Fin:Christian Ramirez - REQ113651

        
        #endregion

    }
}