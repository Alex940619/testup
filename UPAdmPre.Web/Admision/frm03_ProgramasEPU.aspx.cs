using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm03_ProgramasEPU : BasePage
    {
        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (!IsPostBack)
                {
                    this.HabilitaControles();
                    //this.CargarTitulos();
                    this.MostrarOcultarBotones(false);
                    if (Session["AplicanteId"] != null)
                    {
                        //Para continuar Insccripción de programas
                        Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                        this.rblProgramas.Enabled = false;
                        String strUsrRedId = Session["UsrRedId"].ToString();
                        this.chkListCursos.Enabled = false;
                        this.CargaProgramasDeInteresContinuacion();
                        this.recuperaProgramasRegistrada(AplicanteId, strUsrRedId);
                    }
                    else
                    {
                        ///Programas para Nueva Inscripción
                        this.CargaProgramasDeInteresNuevo();
                        this.rblProgramas.Enabled = true;
                        this.chkListCursos.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F03, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] + "&descError=" + ex.Message.Replace("\n", ""), false);
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
                if (dtPagAnterior.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < dtPagAnterior.Rows.Count; i++)
                    {
                        PaginaActual = dtPagAnterior.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F03)
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('No puede regresar...!!!');", true);
                    return;
                }
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
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F03, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void rblProgramas_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdModalidad = 0;
            String strUsrRedId = null;
            Int32? ApplicationFormSettingId, IdAplicante = null;
            try
            {
                this.MostrarOcultarBotones(true);
                IdModalidad = UIConvertNull.Int32(rblProgramas.SelectedValue);
                ApplicationFormSettingId = UIConstantes._IdProgramasEPU;
                strUsrRedId = Session["UsrRedId"].ToString();
                IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
                this.CargarCursosPorPrograma(IdModalidad, strUsrRedId, IdAplicante);
                this.ObtenerDescripcionDePrograma(IdModalidad, null, ApplicationFormSettingId);
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


        private void CargaProgramasDeInteresNuevo()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtProgramas;
            Int32? TipoPost = UIConstantes._IdProgramasEPU;
            try
            {
                oGeneralBL = new GeneralBL();
                dtProgramas = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPO_PROGRAMA_EPU_NUEVO].Key, Session["usrRedId"].ToString(), TipoPost);
                if (dtProgramas != null && dtProgramas.Rows.Count > 0)
                {
                    this.rblProgramas.DataValueField = "Codigo";
                    this.rblProgramas.DataTextField = "Descripcion";
                    this.rblProgramas.DataSource = dtProgramas;
                    this.rblProgramas.DataBind();
                    this.imgBtnNext.Visible = true;
                }
                else
                {
                    this.rblProgramas.DataSource = null;
                    this.rblProgramas.DataBind();
                    this.lblMensajeProgramas.Text = UIConstantes.Alert.msgProgramasNoDisponible;
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
                dtProgramas = null;
            }
        }

        private void CargaProgramasDeInteresContinuacion()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtProgramas;
            Int32? TipoPost = UIConstantes._IdProgramasEPU;
            try
            {
                oGeneralBL = new GeneralBL();
                dtProgramas = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPO_PROGRAMA_EPU_CONTINUA].Key, Session["usrRedId"].ToString(), TipoPost);
                if (dtProgramas != null && dtProgramas.Rows.Count > 0)
                {
                    this.rblProgramas.DataValueField = "Codigo";
                    this.rblProgramas.DataTextField = "Descripcion";
                    this.rblProgramas.DataSource = dtProgramas;
                    this.rblProgramas.DataBind();
                    this.imgBtnNext.Visible = true;
                }
                else
                {
                    this.rblProgramas.DataSource = null;
                    this.rblProgramas.DataBind();
                    this.lblMensajeProgramas.Text = UIConstantes.Alert.msgProgramasNoDisponible;
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
                dtProgramas = null;
            }
        }

        private void CargarCursosPorPrograma(Int32? IdPrograma, String strUsrRedId, Int32? IdAplicante)
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtCursos = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                dtCursos = oAplicanteBL.ListarCursosPorPrograma(IdPrograma, strUsrRedId, IdAplicante);
                if (dtCursos != null && dtCursos.Rows.Count > 0)
                {
                    this.chkListCursos.DataValueField = "IdCurso";
                    this.chkListCursos.DataTextField = "DescHorario";
                    this.chkListCursos.DataSource = dtCursos;
                    this.chkListCursos.DataBind();
                    this.chkListCursos.Visible = true;
                    this.lblMensajeCursos.Visible = false;
                }
                else
                {
                    this.chkListCursos.DataSource = null;
                    this.chkListCursos.DataBind();
                    this.chkListCursos.Visible = false;
                    this.lblMensajeCursos.Text = UIConstantes.Alert.msgCursosNoDisponible;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteBL = null;
                dtCursos = null;
            }
        }

        private void recuperaProgramasRegistrada(Int32? AplicanteId, String strUsrRedId)
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtProgramas = null;
            Int32? IdProgramas, ApplicationFormSettingId, IdCursos = null;
            Int32? IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
            try
            {
                oAplicanteBL = new AplicanteBL();
                dtProgramas = oAplicanteBL.ObtenerProgramaRegistrado(AplicanteId);
                if (dtProgramas != null && dtProgramas.Rows.Count > 0)
                {
                    this.rblProgramas.SelectedValue = dtProgramas.Rows[0][0].ToString();
                    IdProgramas = UIConvertNull.Int32(dtProgramas.Rows[0][0].ToString());
                    this.CargarCursosPorPrograma(IdProgramas, strUsrRedId, IdAplicante);

                    ApplicationFormSettingId = UIConstantes._IdProgramasEPU;
                    this.ObtenerDescripcionDePrograma(IdProgramas, IdCursos, ApplicationFormSettingId);

                    //Asignando los Horarios Seleccionados
                    String ColumnValue = null;
                    for (Int32 i = 0; i < dtProgramas.Rows.Count; i++)
                    {
                        ColumnValue = dtProgramas.Rows[i]["ColumnValue"].ToString();
                        for (int j = 0; j < chkListCursos.Items.Count; j++)
                        {
                            if (ColumnValue == (chkListCursos.Items[j].Value.ToString()))
                            {
                                chkListCursos.Items[j].Selected = true;
                            }
                        }
                    }
                    this.lblCursosTitulo.Visible = true;
                    Session["ModPostulacion"] = UIConstantes.Modalidad.TallerEPU; ;
                }
                else
                {
                    this.lblMensajeProgramas.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteBL = null;
                dtProgramas = null;
            }
        }

        private void ObtenerDescripcionDePrograma(Int32? IdModalidad, Int32? IdCursos, Int32? ApplicationFormSettingId)
        {
            GeneralBL oGeneralBL = null;
            DataTable dtDescPrograma = null;
            try
            {
                oGeneralBL = new GeneralBL();
                dtDescPrograma = oGeneralBL.ObtenerDescripcionModalidad(IdModalidad, IdCursos, ApplicationFormSettingId);
                if (dtDescPrograma != null && dtDescPrograma.Rows.Count > 0)
                {
                    this.lblDescripcionPrograma.Text = dtDescPrograma.Rows[0][0].ToString();
                }
                else
                {
                    this.lblMensajeProgramas.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGeneralBL = null;
                dtDescPrograma = null;
            }
        }

        private void MostrarOcultarBotones(Boolean blnAccion)
        {
            this.lblCursosTitulo.Visible = blnAccion;
            this.lblMensajeCursos.Visible = blnAccion;
        }

        private void GuardarDatos()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            Session["ModPostulacion"] = UIConstantes.Modalidad.TallerEPU;
            string strCursos = null;
            GeneralBL oGeneralBL = null;
            DataTable dtPagSigui = null;
            String PaginaActual, PaginaSiguiente = null;
            Int32? intExisteCruce = null;
            try
            {
                ///Validar si hay Cruce de Horarios
                if (chkListCursos.Enabled == true)
                {
                    strCursos = hIdCursos.Value; //Horario
                }

                intExisteCruce = this.ValidarCruceHorarios(UIConvertNull.Int32(Session["AplicanteId"]), strCursos);
                if (intExisteCruce == 1)
                {
                    this.lblmessage.Text = "Existe cruce de horarios en los programas elegidos.";
                    this.mpeMostrarError.Show();
                    this.chkListCursos.Focus();
                    return;
                }

                oAplicanteBE = new AplicanteBE();
                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ListarDatosPersonalesPorUsrRed(Session["usrRedId"].ToString());

                oAplicanteBE.ModalidadPostulacion = UIConstantes.Modalidad.TallerEPU;
                oAplicanteBE.ProgramOfStudy = UIConvertNull.Int32(rblProgramas.SelectedValue.ToString()); //Programa Elegido
                if (chkListCursos.Enabled == true)
                {
                    strCursos = hIdCursos.Value; //Horario
                }
                else
                {
                    strCursos = null;
                }
                oAplicanteBE.Estado = UIConstantes.idValorActivo;
                oAplicanteBE.IdConfiguracionAplicacion = Int32.Parse(UIConstantes.TIPO_FORMULARIO.ADMISION_EPU.ToString("D"));
                oAplicanteBE.EstaInteresadoPlanComida = UIConstantes.idValorNulo;
                oAplicanteBE.EstaInteresadoPlanResidenciaUni = UIConstantes.idValorNulo;
                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
                oAplicanteBE.RedId = UIConvertNull.String(Session["usrRedId"]);

                oAplicanteBL = new AplicanteBL();
                Int32? AplicanteId = oAplicanteBL.InsertaDatosFormDos_ModPostul(oAplicanteBE, strCursos);
                Session["AplicanteId"] = AplicanteId;
                if (AplicanteId != 0 && AplicanteId != null)
                {
                    Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                    oGeneralBL = new GeneralBL();
                    dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    {
                        PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F03)
                        {
                            PaginaSiguiente = dtPagSigui.Rows[i + 1]["NombreFormulario"].ToString();
                            break;
                        }
                    }
                    Response.Redirect(PaginaSiguiente, false);
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

        private Int32 ValidarCruceHorarios(Int32? AplicanteId, string strCursos)
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                return oAplicanteBL.ValidarCruceHorarios(AplicanteId, strCursos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion "Métodos Privados"
    }
}
