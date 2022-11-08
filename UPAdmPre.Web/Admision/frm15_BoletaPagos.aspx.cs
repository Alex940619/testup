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
    public partial class frm15_BoletaPagos : BasePage
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
                //Session["AplicanteId"] = 110603;

                if (!IsPostBack)
                {
                    //this.CargarTitulos();
                    AplicanteBE oAplicanteBE = null;
                    oAplicanteBE = new AplicanteBE();
                    AplicanteBL oAplicanteBL = null;
                    oAplicanteBL = new AplicanteBL();
 
                    Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                    oAplicanteBE = oAplicanteBL.ListarDatosPersonalesPorIdAplicante(AplicanteId);
                    oAplicanteBE.IdAplicante = AplicanteId;
                    Boolean operacionOK = oAplicanteBL.CreacionSocioNegocioSAP(oAplicanteBE);
                    if (operacionOK)
                    {
                        this.obtenerPermisoEmisionBoleta(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                    }
                    
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F15, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

       
        protected void btnPagoOnline_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                Response.Write("<script>window.open('https://srvnetappseg.up.edu.pe/UPPagoVirtual/frmLogin.aspx','_blank');</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnImpPago_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            EncryptBL oEncryptBL = null;
            try
            {
                oEncryptBL = new EncryptBL();
                Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                Response.Redirect("frmRpt_ImprimirVoucher.aspx?AplicanteId=" + AplicanteId.ToString() + "&usrRedId=" + Session["usrRedId"]);
            }
            catch (Exception ex)
            {
                throw ex;
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
                    if (PaginaActual == UIConstantes.Formularios.F15)
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
            DataTable dtPagSigui = null;
            String PaginaActual, PaginaSiguiente = null;
            try
            {
                Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                oGeneralBL = new GeneralBL();
                dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                {
                    PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                    if (PaginaActual == UIConstantes.Formularios.F15)
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
                    if (PaginaActual == UIConstantes.Formularios.F15)
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

        private void obtenerPermisoEmisionBoleta(Int32? AplicanteId)
        {
            AplicanteBL oAplicanteBL = null;
            ReporteBL oReporteBL = null;
            String Resultado = null;
            DataTable dtReporte = null;
            DataSet dsDatos = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                dsDatos = oAplicanteBL.ObtenerPermisoEmisionBoleta(AplicanteId);
                if (dsDatos != null)
                {
                    if (dsDatos.Tables.Count > 0)
                    {
                        Resultado = oAplicanteBL.GenerarOVentaDerechoAdmision(dsDatos);
                    }
                }

                if (!string.IsNullOrEmpty(Resultado))
                {
                    tr1.Visible = true;
                    lblMensajePago1.Text = (Resultado.Contains("Exito")) ? "" : Resultado;
                    //btnImpPago.Enabled = false;
                    //btnImpPago.BackColor = System.Drawing.Color.DarkGray;
                    //btnImpPago.ForeColor = System.Drawing.Color.Black;
                    //btnPagoOnline.Enabled = false;
                    //btnPagoOnline.BackColor = System.Drawing.Color.DarkGray;
                    //btnPagoOnline.ForeColor = System.Drawing.Color.Black;
                }

                oReporteBL = new ReporteBL();
                dtReporte = oReporteBL.ImprimirVoucherPago(AplicanteId)[0].Tables[0];
                if (dtReporte != null && dtReporte.Rows.Count > 0)
                {
                    this.lblnompostulante.Text = dtReporte.Rows[0]["FullName"].ToString();
                    this.lbldocumento.Text = dtReporte.Rows[0]["Documento"].ToString();
                    this.lblCodigoPago.Text = dtReporte.Rows[0]["Documento"].ToString();
                    this.lblfecvec.Text = dtReporte.Rows[0]["FecVenc"].ToString();
                    this.lblimporte.Text = dtReporte.Rows[0]["Importe"].ToString();
                }

                if (Session["ModPostulacion"].ToString() == "20")
                {
                    lblPostulanteLabel.Text = "Participante:";
                    lblTitulo.Text = "Pago del Curso EPU";
                }
                else
                {
                    lblPostulanteLabel.Text = "Nombres y Apellidos:";
                    /*Ini: Christian Ramirez - Caso45554 - 20180530*/
                    lblTitulo.Text = "Pago por Derecho de Inscripción";
                    /*Fin: Christian Ramirez - Caso45554 - 20180530*/
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion "Métodos Privados"
    }
}
