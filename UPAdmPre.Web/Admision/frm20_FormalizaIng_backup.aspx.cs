using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm20_FormalizaIng_backup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            try
            {
                ///*Solo para pruebas*/
                //Session["usrRedId"] = "u.admision";
                //Session["ModPostulacion"] = 41;
                //Session["AplicanteId"] = "6CbZOH%2fvzZk%3d";// 155861;

                if (!IsPostBack)
                {
                    EncryptBL _EncryptBL = new EncryptBL();
                    Int32? AplicanteId = null;
                    AplicanteId = UIConvertNull.Int32(_EncryptBL.DecryptKey(Request.QueryString["ApplicationId"]));
                    string slink = "http://www.up.edu.pe/admision/portal/Paginas/conoce-tu-escala.aspx?appId=";

                    hlconoce.InnerHtml = "<a href='" + slink + Request.QueryString["ApplicationId"].ToString() + "' target='_blank'> <img alt='Conoce tu escala' class='auto-style1' style='animation-name:animate-btn; animation-duration:2s; animation-iteration-count:infinite; an' src='../Images/conoce-tu-escala.png'  </a>";

                    this.CargarTextos();
                    this.CargarTextosEtiquetas();
                    this.CargarComboHorarios(AplicanteId);
                    this.LLenarDatosPersona();
                    this.CargarFormalizacionRegistrado(AplicanteId);
                    //this.LLenarGrillaReferencias();
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F18, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        private void CargarTextos()
        {
            EncryptBL _EncryptBL = new EncryptBL();
            Int32? AplicanteId = null;
            AplicanteId = UIConvertNull.Int32(_EncryptBL.DecryptKey(Request.QueryString["ApplicationId"]));
            this.ObtenerTextoEnsayo(AplicanteId, 28); // 28 por el mensaje de formalización

            this.ObtenerTextoInformativo(AplicanteId, 16);
            this.lblMensajeInformativoInf.Visible = false;
        }

        private void CargarTextosEtiquetas()
        {
                this.lblTituloSeleccion.Text = "Selección de Fecha y hora de Formalización";
                this.lblSubTituloHorario.Text = "Horario de Formalización:";
        }

        private void ObtenerTextoEnsayo(Int32? AplicanteId, Int32? @VC_TipoMensajesId)
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                DataTable dtTextoInf = oAplicanteBL.ObtenerTextoInformativo(AplicanteId, @VC_TipoMensajesId);
                if (dtTextoInf.Rows.Count > 0)
                {
                    DataRow drPago = dtTextoInf.Rows[0];
                    String strTexto = drPago["Texto"].ToString();
                    this.lblMsg.Text = strTexto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerTextoInformativo(Int32? AplicanteId, Int32? @VC_TipoMensajesId)
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                DataTable dtTextoInf = oAplicanteBL.ObtenerTextoInformativo(AplicanteId, @VC_TipoMensajesId);
                if (dtTextoInf.Rows.Count > 0)
                {
                    DataRow drPago = dtTextoInf.Rows[0];
                    String strTexto = drPago["Texto"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboHorarios(Int32? AplicanteId)
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtHorarios = oGeneralBL.ObtenerHorariosDeFormalizacion(AplicanteId);
                if (dtHorarios != null && dtHorarios.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlHorario, dtHorarios.Copy(), "Descripcion", "Codigo", "-- Seleccionar --");
                }
                else
                {
                    Funciones.cargarComboYSeleccione(this.ddlHorario, null, "Descripcion", "Codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosPersona()
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtDatos = null;
            try
            {
                EncryptBL _EncryptBL = new EncryptBL();
                Int32? AplicanteId = null;
                AplicanteId = UIConvertNull.Int32(_EncryptBL.DecryptKey(Request.QueryString["ApplicationId"]));

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

        private void CargarFormalizacionRegistrado(Int32? AplicanteId)
        {
            EncryptBL _EncryptBL = new EncryptBL();
            AplicanteId = Convert.ToInt32(_EncryptBL.DecryptKey(Request.QueryString["ApplicationId"]));
            
            AplicanteBL oAplicanteBL = null;
            DataTable dtEntrevista = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                dtEntrevista = oAplicanteBL.ObtenerFormalizacionRegistrado(AplicanteId);
                if (dtEntrevista != null && dtEntrevista.Rows.Count > 0)
                {
                    this.ddlHorario.SelectedValue = dtEntrevista.Rows[0][0].ToString();
                    this.ddlHorario.Enabled = false;
                    this.imgBtnEnviar.Visible = false;
                    this.lblMensajeInformativoInf.Visible = true;
                    this.ObtenerTextoInformativoInf(AplicanteId, 16);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerTextoInformativoInf(Int32? AplicanteId, Int32? MensajesId)
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                this.lblMensajeInformativoInf.Visible = true;
                oAplicanteBL = new AplicanteBL();
                DataTable dtTextoInf = oAplicanteBL.ObtenerTextoInformativo(AplicanteId, MensajesId);
                if (dtTextoInf.Rows.Count > 0)
                {
                    DataRow drPago = dtTextoInf.Rows[0];
                    String strTexto = drPago["Texto"].ToString();
                    this.lblMensajeInformativoInf.Text = strTexto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GuardarDatos()
        {
            AplicanteBL oAplicanteBL = null;
            //Int32? AplicanteId = null;
            String strHoraEntrevista = null;
            try
            {
                EncryptBL _EncryptBL = new EncryptBL();
                Int32? AplicanteId = null;
                AplicanteId = UIConvertNull.Int32(_EncryptBL.DecryptKey(Request.QueryString["ApplicationId"]));


                //AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                strHoraEntrevista = UIConvertNull.String(ddlHorario.SelectedValue);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormVeinte_HoraFormalizacion(AplicanteId, strHoraEntrevista);
                if (operacionOK)
                {
                    this.pnlHorario.Enabled = false;
                    this.ddlHorario.Enabled = false;
                    this.imgBtnEnviar.Visible = false;
                    //this.ObtenerTextoInformativoInf(UIConvertNull.Int32(Session["AplicanteId"]), 64);
                    this.ObtenerTextoInformativoInf(AplicanteId, 16);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Se registro su horario con exito.');", true);
                    this.lblmessage.Text = "Se registro su horario con exito. <br /> Se ha enviado un correo con la confirmación de la fecha y hora seleccionada.";
                    this.mpeMostrarError.Show();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void imgBtnEnviar_Click(object sender, ImageClickEventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            try
            {
                this.GuardarDatos();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F18, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.mpeMostrarError.Hide();
        }

    }
}