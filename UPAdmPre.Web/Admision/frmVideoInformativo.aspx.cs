using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frmVideoInformativo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                //Int32? MinInicio = int.Parse(Request.QueryString["start"]);
                //Int32? MinFin = int.Parse(Request.QueryString["end"]);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}