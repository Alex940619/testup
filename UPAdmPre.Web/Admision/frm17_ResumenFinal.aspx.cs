using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm17_ResumenFinal : System.Web.UI.Page
    {
        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (!IsPostBack)
                {
                    this.ObtenerResumenFinal();
                    //this.EliminarSesiones();
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F17, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        #endregion "Eventos"

        #region "Métodos Privados"

        private void ObtenerResumenFinal()
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
               Int32? TipoMensajesId = 60;
               // Int32? TipoMensajesId = 16;

                oAplicanteBL = new AplicanteBL();
                DataTable dtEstadoPago = oAplicanteBL.ObtenerTextoInformativo(UIConvertNull.Int32(Session["AplicanteId"]), TipoMensajesId);
                if (dtEstadoPago.Rows.Count > 0)
                {
                    DataRow drPago = dtEstadoPago.Rows[0];
                    String strResumenFinal = drPago["Texto"].ToString();
                    this.lblMsg.Text = strResumenFinal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EliminarSesiones()
        {
            Session["usrRedId"] = null;
            Session["AplicanteId"] = null;
            Session["IdPrograma"] = null;
            Session["Opcion"] = null;
            Session["ModPostulacion"] = null;
        }

        #endregion "Métodos Privados"
    }
}
