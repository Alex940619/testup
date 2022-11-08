using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace UPAdmPre.Web
{
    public partial class frmMensajeError : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {        
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(1));
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetSlidingExpiration(true);
            Response.Buffer = true;
            Response.Expires = 0;
            try
            {
                if ((Session["UPADMPRE_MENSAJE"] != null))
                {                    
                    this.lblmessage.Text = ((String)Session["UPADMPRE_MENSAJE"]);
                }
            }
            catch (Exception ex)
            {
                this.lblmessage.Text = ex.Message;
            }
            finally
            {
                Session.Remove("UPADMPRE_MENSAJE");
                Session["UPADMPRE_MENSAJE"] = null;
            }
        }
    }
}