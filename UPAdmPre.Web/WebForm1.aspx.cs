using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UPAdmPre.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("Default.aspx?usrredid=u.admision&ApplicationId=gzDd2fMmtFc=&IdPrograma=19");
            Response.Redirect("Default.aspx?usrredid=u.admision&ApplicationId=&IdPrograma=7");
        }
    }
}