using System;
using System.Web;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public class BasePage : System.Web.UI.Page
    {
        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static object GetStepsAdmision()
        {
            try
            {
                string[] Path = HttpContext.Current.Request.Path.Split('/');
                /*
                 PARA CUANDO SE QUIERA TRAER EL HTML
                 ======================================
                 */
                //StepsAdmision stepsAdmision = Helper.UtilStepAdmision.Helper<Helper.UtilStepAdmision.StepsAdmision>.Instance.Configure(UIConvertNull.Int32(HttpContext.Current.Session["ModPostulacion"]),
                //                                                                                                                       Path[Path.Length - 2]).Get();

                //var jsonStepsAdmision = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(stepsAdmision);
                //return stepsAdmision;


                /*
                 PARA CUANDO SE QUIERA TRAER EL OBJETO
                 ======================================
                 */
                string stepsHTMLAdmision = Helper.UtilStepAdmision.Helper<Helper.UtilStepAdmision.StepsAdmision>.Instance.Configure(UIConvertNull.Int32(HttpContext.Current.Session["ModPostulacion"]),
                                                                                                                                    UIConvertNull.Int32(HttpContext.Current.Session["AplicanteId"]),
                                                                                                                                    Path[Path.Length - 2]).GetForHtml().GetHtml();
                return stepsHTMLAdmision;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}