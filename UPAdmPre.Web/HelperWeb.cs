using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using AjaxControlToolkit;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web
{
    public class HelperWeb : System.Web.UI.Page
    {
        public HelperWeb()
        { }

         /// <summary>
        /// Metodo que se muestra mensaje en una Ventana PopUp.
        /// Enviar el parametro strOpcion igual a 'null' si solo se quiere mostrar mensaje.
        /// Enviar el parametro strOpcion diferente a 'null' si se quiere mostrar mensaje y grabar error en la Base de Datos.
        /// </summary>
        /// <param name="uppRegistro"></param>
        /// <param name="type"></param>
        /// <param name="strMensaje"></param>
        /// <param name="oPage"></param>
        /// <param name="strOpcion"></param>       
        public static void JsMensajeAlert(System.Web.UI.UpdatePanel uppRegistro, System.Type type, String strMensaje, Page oPage, String strOpcion)
        {
            HttpContext oHttpContext = null;
            //LogBL oLogBL = null;
            //LogBE oLogBE = null;
            try
            {
                oHttpContext = HttpContext.Current;
                if (string.IsNullOrEmpty(strOpcion) == false)
                {
                    //oLogBL = new LogBL();
                    //oLogBE = new LogBE();
                    //oLogBE.aplicacion = UIConstantes._strAplicacion;
                    //oLogBE.opcion = oPage.ToString() + UIConstantes._valorCadenaEspacio + UIConstantes._valorSigoSlash + UIConstantes._valorCadenaEspacio + strOpcion;
                    //oLogBE.usuario = HelperWeb.RetornaIdUsuarioSession(); ;
                    //oLogBE.error = strMensaje;

                    //oLogBL.InsertLog(oLogBE);
                    oHttpContext.Session["UPADMPRE_MENSAJE"] = strMensaje;
                    HelperWeb.JsMensajeAlertError(uppRegistro, type, oPage, strMensaje);
                    //HelperWeb.JsMensajeAlertError(uppRegistro, type, oPage);
                }
                else
                {
                    oHttpContext.Session["UPADMPRE_MENSAJE"] = strMensaje;
                    HelperWeb.JsMensajeAlertAccion(uppRegistro, type, oPage, strMensaje);
                    //HelperWeb.JsMensajeAlertAccion(uppRegistro, type, oPage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static String JsMensajeAlert(String strMensaje)
        {
            return ("alert('" + strMensaje + "'); ");
        }

        /// <summary>
        /// Envía mensaje de Error
        /// </summary>
        /// <param name="uppRegistro"></param>
        /// <param name="type"></param>
        /// <param name="oPage"></param>
        /// <param name="strMensaje"></param>
        private static void JsMensajeAlertError(System.Web.UI.UpdatePanel uppRegistro, System.Type type, Page oPage, String strMensaje)
        {            
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(oPage, type, uppRegistro.ClientID + "Mensaje", "javascript:parent.AbrirVentanaModal('Mensaje del Sistema', 'frmMensajeError.aspx',340,195)", true);
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(oPage, type, uppRegistro.ClientID + "Mensaje", "javascript:parent.parent.AbrirVentanaModal('Mensaje del Sistema', 'frmMensajeError.aspx',340,195)", true);
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(oPage, type, uppRegistro.ClientID + "Mensaje", "javascript:parent.parent.parent.AbrirVentanaModal('Mensaje del Sistema', 'frmMensajeError.aspx',340,195)", true);            
        }

        /// <summary>
        /// Envía mensaje de Acción
        /// </summary>
        /// <param name="uppRegistro"></param>
        /// <param name="type"></param>
        /// <param name="oPage"></param>
        /// <param name="strMensaje"></param>
        private static void JsMensajeAlertAccion(System.Web.UI.UpdatePanel uppRegistro, System.Type type, Page oPage, String strMensaje)
        {
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(oPage, type, uppRegistro.ClientID + "Mensaje", "javascript:parent.AbrirVentanaModal('Mensaje del Sistema', 'frmMensajeExito.aspx',340,195)", true);
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(oPage, type, uppRegistro.ClientID + "Mensaje", "javascript:parent.parent.AbrirVentanaModal('Mensaje del Sistema', 'frmMensajeExito.aspx',340,195)", true);
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(oPage, type, uppRegistro.ClientID + "Mensaje", "javascript:parent.parent.parent.AbrirVentanaModal('Mensaje del Sistema', 'frmMensajeExito.aspx',340,195)", true);            
        }    
    }
}