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
    public partial class frm16_TerminosyCondiciones : BasePage
    {
        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            //*Solo para pruebas*/
            //Session["usrRedId"] = "U.Admision";
            //Session["ModPostulacion"] = 49;
            //Session["AplicanteId"] = 110602;
            UIHelper.SessionActiva(Page);
            try
            {


                if (!IsPostBack)
                {
                    //this.CargarTitulos();
                    this.ValidarEstadoPagos();
                    this.HabilitaControles();
                    this.MostrarOcultarBotones(false);
                    this.CargaMensajesPortal();
                    this.CargarInfoRegistrado(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F16, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        protected void imgBtnVideo_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Response.Redirect("frm01_VideoIntro.aspx");
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
                    if (PaginaActual == UIConstantes.Formularios.F16)
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
                //this.GuardarDatos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void chkConsideraciones_CheckedChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (chkConsideraciones.Checked == true)
                {
                    this.txtConsideracion.Enabled = true;
                }
                else
                {
                    this.txtConsideracion.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void chkTerminosCondiciones_CheckedChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (chkTerminosCondiciones.Checked == true)
                {
                    this.lblAlertaTerminosCondiciones.Visible = false;
                }
                else
                {
                    this.lblAlertaTerminosCondiciones.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void chkMayor14ConsentimientoDatPer_CheckedChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (chkMayor14ConsentimientoDatPer.Checked == true)
                {
                    chkApoderadoLegalTitularDatPer.Checked = false;
                }

                if (chkMayor14ConsentimientoDatPer.Checked == false && chkApoderadoLegalTitularDatPer.Checked == false)
                {
                    this.lblAlertaMayor14ConsentimientoDatPer.Visible = true;
                    this.lblAlertaApoderadoLegalTitularDatPer.Visible = true;
                }
                else
                {
                    this.lblAlertaMayor14ConsentimientoDatPer.Visible = false;
                    this.lblAlertaApoderadoLegalTitularDatPer.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void chkApoderadoLegalTitularDatPer_CheckedChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (chkApoderadoLegalTitularDatPer.Checked == true)
                {
                    chkMayor14ConsentimientoDatPer.Checked = false;
                }

                if (chkApoderadoLegalTitularDatPer.Checked == false && chkMayor14ConsentimientoDatPer.Checked == false)
                {
                    this.lblAlertaApoderadoLegalTitularDatPer.Visible = true;
                    this.lblAlertaMayor14ConsentimientoDatPer.Visible = true;
                }
                else
                {
                    this.lblAlertaApoderadoLegalTitularDatPer.Visible = false;
                    this.lblAlertaMayor14ConsentimientoDatPer.Visible = false;
                }
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
                this.EnviarDatos();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F16, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void imgBtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);

            if(chkTerminosCondiciones.Checked == false)
            {
                lblAlertaTerminosCondiciones.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Es necesario confirmar la lectura y aceptación de los términos y condiciones.');", true);
                return;
            }

            if (chkMayor14ConsentimientoDatPer.Checked == false && chkApoderadoLegalTitularDatPer.Checked == false)
            {
                lblAlertaMayor14ConsentimientoDatPer.Visible = true;
                lblApoderadoLegalTitularDatPer.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Es necesario confirmar que es mayor de 14 años y comprende los términos del consentimiento de datos personales o que es el apoderado legal del titular de los datos personales.');", true);
                return;
            }

            try
            {
                this.GuardarDatos();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F16, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        #endregion "Eventos"

        #region "Métodos Privados"

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
                    if (PaginaActual == UIConstantes.Formularios.F16)
                    {
                        this.lblPantallaAnterior.Text = dtTitulos.Rows[i - 2]["NombreFicha"].ToString();
                        this.lblPantallaVigente.Text = dtTitulos.Rows[i - 1]["NombreFicha"].ToString();
                        this.lblPantallaSiguiente.Text = dtTitulos.Rows[i]["NombreFicha"].ToString();
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

        private void CargaMensajesPortal()
        {
            GeneralBL oGeneralBL = null;
            DataTable DTMensajes = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DTMensajes = new DataTable();
                DTMensajes = oGeneralBL.obtenerMensajeporId(1);
                lblConsideraciones.Text = DTMensajes.Rows[0][2].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MostrarOcultarBotones(Boolean blnAccion)
        {
            this.txtConsideracion.Enabled = blnAccion;
        }

        private void ValidarEstadoPagos()
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);

                ///Validando que Boleta este en estado CO
                oAplicanteBL = new AplicanteBL();
                DataTable dtEstadoPago = oAplicanteBL.ObtenerEstadoPagoBoleta(AplicanteId);
                DataRow drPago = dtEstadoPago.Rows[0];
                String strDocPendientes = drPago["DocPendientes"].ToString();
                String strEstadoPago = drPago["Estado"].ToString();
                if (strDocPendientes == "0")
                {
                    this.imgBtnEnviar.Enabled = true;
                    this.imgBtnEnviar.ImageUrl = "~/Images/Buttons/btnEnabled.png";
                    this.imgBtnGuardar.Enabled = true;

                    
                    if (strEstadoPago != "CO")
                    {
                        this.lblMsgIncompleto.Visible = true;
                    }
                }
                else
                {
                    this.imgBtnEnviar.Enabled = false;
                    this.imgBtnEnviar.ImageUrl = "~/Images/Buttons/btnDisabled.png";
                    this.imgBtnGuardar.Enabled = true;

                    if (strEstadoPago != "CO")
                    {
                        this.lblMsgIncompleto.Visible = true;
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
            try
            {
                oAplicanteBE = new AplicanteBE();
                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
                if (chkConsideraciones.Checked == true)
                {
                    oAplicanteBE.isDiscapacitado = true;
                    if (this.txtConsideracion.Text != null)
                    {
                        oAplicanteBE.Discapacitado = this.txtConsideracion.Text;
                    }
                }
                else
                {
                    oAplicanteBE.isDiscapacitado = false;
                }
                oAplicanteBE.Autorizacion = UIConvertNull.Int32(rblAutorizacion.SelectedValue);
                oAplicanteBE.AutorizacionTerceros = UIConvertNull.Int32(rblautorizacionTercer.SelectedValue);
                oAplicanteBE.AceptTermCond = (chkTerminosCondiciones.Checked == true ? true : false);
                oAplicanteBE.Mayor14ConsentimientoDatPer = (chkMayor14ConsentimientoDatPer.Checked == true ? true : false);
                oAplicanteBE.ApoderadoLegalTitularDatPer = (chkApoderadoLegalTitularDatPer.Checked == true ? true : false);
                oAplicanteBE.RedId = Session["usrRedId"].ToString();
                // oAplicanteBE.idAntecedentes = UIConvertNull.Int32(rblAntecedentes.SelectedValue);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormDieciseis_TermRef(oAplicanteBE);
                if (operacionOK)
                {
                    //Response.Redirect("frm17_ResumenFinal.aspx", false);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Las consideraciones se guardarón con éxito.');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviarDatos()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBE = new AplicanteBE();
                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
                if (chkConsideraciones.Checked == true)
                {
                    oAplicanteBE.isDiscapacitado = true;
                    if (this.txtConsideracion.Text != null)
                    {
                        oAplicanteBE.Discapacitado = this.txtConsideracion.Text;
                    }
                }
                else
                {
                    oAplicanteBE.isDiscapacitado = false;
                }
                oAplicanteBE.Autorizacion = UIConvertNull.Int32(rblAutorizacion.SelectedValue);
                oAplicanteBE.AutorizacionTerceros = UIConvertNull.Int32(rblautorizacionTercer.SelectedValue);
               // oAplicanteBE.idAntecedentes = UIConvertNull.Int32(rblAntecedentes.SelectedValue);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.EnviarDatosFormDieciseis_TermRef(oAplicanteBE);
                if (operacionOK)
                {
                    Response.Redirect("frm17_ResumenFinal.aspx", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CargarInfoRegistrado(Int32? AplicanteId)
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ObtenerInfoTerminosCondicionesRegistrados(AplicanteId);
                this.LLenarDatosInfoRegistrados(oAplicanteBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosInfoRegistrados(AplicanteBE oAplicanteBE)
        {
            if (oAplicanteBE != null)
            {
                if (oAplicanteBE.Discapacitado != null)
                {
                    this.chkConsideraciones.Checked = true;
                    this.txtConsideracion.Enabled = true;
                }
                else
                {
                    this.chkConsideraciones.Checked = false;
                    this.txtConsideracion.Enabled = false;
                }
                this.txtConsideracion.Text = oAplicanteBE.Discapacitado;
                this.rblAutorizacion.SelectedValue = UIConvertNull.String(oAplicanteBE.Autorizacion);
                this.rblautorizacionTercer.SelectedValue = UIConvertNull.String(oAplicanteBE.AutorizacionTerceros);

                if (oAplicanteBE.AceptTermCond != false)
                {
                    this.chkTerminosCondiciones.Checked = true;
                }
                else
                {
                    this.chkTerminosCondiciones.Checked = false;
                }

                if (oAplicanteBE.Mayor14ConsentimientoDatPer != false)
                {
                    this.chkMayor14ConsentimientoDatPer.Checked = true;
                }
                else
                {
                    this.chkMayor14ConsentimientoDatPer.Checked = false;
                }

                if (oAplicanteBE.ApoderadoLegalTitularDatPer != false)
                {
                    this.chkApoderadoLegalTitularDatPer.Checked = true;
                }
                else
                {
                    this.chkApoderadoLegalTitularDatPer.Checked = false;
                }
                // this.rblAntecedentes.SelectedValue = UIConvertNull.String(oAplicanteBE.idAntecedentes);
            }
        }

        #endregion "Métodos Privados"
    }
}
