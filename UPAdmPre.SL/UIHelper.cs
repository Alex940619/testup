using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO.Compression;
using System.Xml;
using System.Configuration;
using System.Net.Mail;
using System.Web.Configuration;
using UPAdmPre.SL;

namespace UPAdmPre.SL
{
    public sealed class UIHelper
    {
        public static Boolean IsDate(Object dFecha)
        {
            Boolean blRetorno = false;
            String strFecha = UIConvertNull.String(dFecha);
            try
            {
                DateTime dFechaDate = DateTime.Parse(strFecha);
                if (dFechaDate > DateTime.Parse(UIConstantes.FormatoFechas._valorFechaMinimoCarga))
                {
                    blRetorno = true;
                }
                else
                {
                    blRetorno = false;
                }
                return blRetorno;
            }
            catch
            {
                return false;
            }
        }

        public static String AsignarDatoControlHtml(String NombreControlObjetoHtml, String ValorAsignar)
        {
            String Cadena = "MostrarDatosEnCajaTexto('" + NombreControlObjetoHtml + "','" + ValorAsignar + "'); " + UIConstantes._valorCadenaEspacio;
            return Cadena;
        }

        public static String JsConfirmarAccionPaginaWeb(String strMensajeParaConfirmar)
        {
            return String.Concat(new String[] {
            " return ConfimarAccionRegistro('",
            strMensajeParaConfirmar,
            "'); "});
        }

        public static String WebConfigObtenerValorPorKey(String Key)
        {
            String valor = ConfigurationManager.AppSettings[Key];
            return valor;
        }

        public enum EventosJavaScript
        {
            OnClick,
            OnMouseDown,
            OnMouseout,
            OnMouseover,
            OnMousemove,
            Ondblclick,
            OnKeydown,
            OnKeypress,
            OnKeyup,
            OnChange,
            OnFocus,
            OnBlur,
            SinEvento
        }

        public static void SeleccionarItemGrillaOnClickMoverRaton(System.Web.UI.WebControls.GridViewRowEventArgs e, String CadenaScriptOnClick)
        {
            e.Row.Attributes.Remove(EventosJavaScript.OnMouseover.ToString());
            e.Row.Attributes.Remove(EventosJavaScript.OnMouseout.ToString());
            e.Row.Attributes.Add(EventosJavaScript.OnMouseover.ToString(), "CambiarColorPasarMouseOver(this); ");
            e.Row.Attributes.Add(EventosJavaScript.OnMouseout.ToString(), "CambiarColorPasarMouseOut(this); ");
            String Cadena = "CambiarColorSeleccion(this); " + CadenaScriptOnClick;
            e.Row.Attributes.Add(EventosJavaScript.OnClick.ToString(), Cadena);
        }

        public static DataTable Tabla_Notas()
        {
            DataTable dtNew = null;
            DataColumn dcNew = null;
            DataRow drNew = null;
            try
            {
                dtNew = new DataTable("dtNew");

                dcNew = new DataColumn();
                dcNew.DataType = System.Type.GetType("System.String");
                dcNew.ColumnName = en_tabla_Codigo_Nombre.IdCodigo.ToString();
                dtNew.Columns.Add(dcNew);

                dcNew = new DataColumn();
                dcNew.DataType = System.Type.GetType("System.String");
                dcNew.ColumnName = en_tabla_Codigo_Nombre.Nombre.ToString();
                dtNew.Columns.Add(dcNew);

                for (Int32 i = 11; i <= 20; i = i + 1)
                {
                    drNew = dtNew.NewRow();
                    drNew[en_tabla_Codigo_Nombre.IdCodigo.ToString()] = UIConvertNull.String(i);
                    drNew[en_tabla_Codigo_Nombre.Nombre.ToString()] = UIConvertNull.String(i);
                    dtNew.Rows.Add(drNew);
                }
                return dtNew;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtNew = null;
                dcNew = null;
                drNew = null;
            }
        }

        public static DataTable Tabla_RelacionFamiliar()
        {
            DataTable dtNew = null;
            DataColumn dcNew = null;
            DataRow drNew = null;
            try
            {
                dtNew = new DataTable("dtNew");

                dcNew = new DataColumn();
                dcNew.DataType = System.Type.GetType("System.String");
                dcNew.ColumnName = en_tabla_Codigo_Nombre.IdCodigo.ToString();
                dtNew.Columns.Add(dcNew);

                dcNew = new DataColumn();
                dcNew.DataType = System.Type.GetType("System.String");
                dcNew.ColumnName = en_tabla_Codigo_Nombre.Nombre.ToString();
                dtNew.Columns.Add(dcNew);

                drNew = dtNew.NewRow();
                drNew[en_tabla_Codigo_Nombre.IdCodigo.ToString()] = "5";
                drNew[en_tabla_Codigo_Nombre.Nombre.ToString()] = "Padre";
                dtNew.Rows.Add(drNew);

                drNew = dtNew.NewRow();
                drNew[en_tabla_Codigo_Nombre.IdCodigo.ToString()] = "20";
                drNew[en_tabla_Codigo_Nombre.Nombre.ToString()] = "Madre";
                dtNew.Rows.Add(drNew);

                drNew = dtNew.NewRow();
                drNew[en_tabla_Codigo_Nombre.IdCodigo.ToString()] = "19";
                drNew[en_tabla_Codigo_Nombre.Nombre.ToString()] = "Apoderado";
                dtNew.Rows.Add(drNew);

                return dtNew;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtNew = null;
                dcNew = null;
                drNew = null;
            }
        }

        public static void EnviarCorreo(String strNomFormulario, String strError, String strUserRed, String ApplicationId)
        {
            //return;
            SmtpClient oSmtpClient = null;
            MailMessage oMailMessage = null;
            try
            {
                String strCorreoOrigen = ConfigurationManager.AppSettings["CorreoSalidaAdmPre"].ToString();
                String strCorreoDestino = ConfigurationManager.AppSettings["CorreoEnvioErrores"].ToString();

                /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                //String FirmaMsgError1 = UIConstantes.Mensajes._msgErrorCorreo1;
                //String FirmaMsgError2 = UIConstantes.Mensajes._msgErrorCorreo2;
                //String FirmaMsgError3 = UIConstantes.Mensajes._msgErrorCorreo3;
 
                String strAsunto = "Universidad del Pacífico: Error en Inscripción - Admision PRE(UPAdmPre)";
                /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/

                Boolean blnFormatoHTML = true;
                System.Net.Mail.Attachment flsAdjuntos = null;
                String strMensaje = null;
                String strSMTP = ConfigurationManager.AppSettings["ServidorSMTP"].ToString();

                String html = UIConstantes._valorCadenaVacia;
                html += UIConstantes.strEstiloCorreo + Environment.NewLine;

                html += "<table style=" + "width:" + "100%;" + ">" + Environment.NewLine;
                html += "<tr class=" + "HeaderGrilla" + " >";
                html += "<td><b>Pestaña:</b></td>";
                html += "<td class=\"ItemGrilla\"><b>" + strNomFormulario + "</b></td>";
                html += "</tr>";
                html += "<tr class=\"HeaderGrilla\" >";
                html += "<td><b>Descripción del Error:</b></td>";
                html += "<td><b>" + strError + "</b></td>";
                html += "</tr>";
                html += "<tr class=\"HeaderGrilla\" >";
                html += "<td><b>Usuario:</b></td>";
                html += "<td><b>" + strUserRed + "</b></td>";
                html += "</tr>";
                html += "<tr class=\"HeaderGrilla\" >";
                html += "<td><b>ApplicationId:</b></td>";
                html += "<td><b>" + ApplicationId + "</b></td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td></td>";
                html += "</tr>";

                /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                /*se comenta*/
                //html += "</tr>";
                //html += "<tr>";
                //html += "<td colspan=" + 2 + "><br />" + FirmaMsgError1 + "</td>";
                //html += "</tr>";
                //html += "<tr>";
                //html += "<td colspan=" + 2 + "><br />" + FirmaMsgError2 + "</td>";
                //html += "</tr>";
                //html += "<tr>";
                //html += "<td colspan=" + 2 + "><br />" + FirmaMsgError3 + "</td>";
                //html += "</tr>";
                /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/

                html += "</table>";
                strMensaje = html;

                oMailMessage = new MailMessage();
                oMailMessage.From = new MailAddress(strCorreoOrigen);

                /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                //oMailMessage.To.Add(strCorreoDestino);
                string[] destinatarios = strCorreoDestino.Split(';');
                int nDestinatarios = destinatarios.Length;

                for (int i = 0; i < nDestinatarios; i++)
                {
                    oMailMessage.To.Add(destinatarios[i]);
                }
                /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/

                oMailMessage.Subject = strAsunto;
                oMailMessage.IsBodyHtml = blnFormatoHTML;
                if (flsAdjuntos != null)
                {
                    oMailMessage.Attachments.Add(flsAdjuntos);
                }
                oMailMessage.Body = "<html><body>" + strMensaje + "</body></html>";
                oSmtpClient = new SmtpClient(strSMTP);
                oSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                oSmtpClient.Send(oMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSmtpClient = null;
                oMailMessage = null;
            }
        }

        public static void EnviarCorreoDocCompletados(Int32? ApplicationId)
        {
            try
            {
                String strCorreoOrigen = ConfigurationManager.AppSettings["CorreoSalidaAdmPre"].ToString();
                String Asunto = "Documentos de aplicación completados: Taipe Mayhuasca, Walter Daniel";
                String strSMTP = ConfigurationManager.AppSettings["ServidorSMTP"].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void IniciaSessionUsuarioForm(String strUser, out Int32? iRetorno)
        {
            HttpContext oHttpContext = null;
            try
            {
                oHttpContext = HttpContext.Current;
                FormsAuthentication.SetAuthCookie(UIConvertNull.String(strUser), true);
                oHttpContext.Session["IDUSUARIO"] = strUser;
                if (UIConvertNull.Int32(oHttpContext.Session["CADUCASESSION"]) != 1)
                {
                    oHttpContext.Response.Redirect("~/Gestion/frmPrincipal.aspx");
                    iRetorno = 0;
                }
                else
                {
                    iRetorno = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oHttpContext.Session["CADUCASESSION"] = null;
                oHttpContext = null;
            }
        }

        public static void SessionActiva(Page oPage)
        {
            try
            {
                HttpContext oHttpContext = HttpContext.Current;
                if (oHttpContext.Session["usrRedId"] == null)
                {
                    oHttpContext.Response.Redirect("~/SessionExpired.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "Enumeradores"

        public enum en_tabla_Codigo_Nombre
        {
            IdCodigo,
            Nombre
        }

        #endregion
    }
}
