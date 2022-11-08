using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UPAdmPre.Web.Admision
{
    public partial class frmMsjeErrorUP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //this.lblTitulo.Text = Request.QueryString["tipError"];
                //this.lblDescError.Text = Request.QueryString["descError"];
            }
        }
    }
}