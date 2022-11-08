using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UPAdmPre.Web.Admision
{
    public partial class SessionExpired : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRefreshParent_Click(object sender, EventArgs e)
        {
            Response.Write("<script>debugger;  window.open('" + "http://www.up.edu.pe/admision/portal/Paginas/postulacion-online.aspx" + "','_top')</script>");
        }
    }
}