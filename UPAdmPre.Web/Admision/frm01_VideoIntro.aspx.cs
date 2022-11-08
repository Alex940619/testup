using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm01_VideoIntro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (!IsPostBack)
                {
                    this.lblPantallaAnterior.Text = UIConstantes.TitulosDepaginas.TitForm01;
                    if (UIConvertNull.Int32(Session["IdPrograma"]) == UIConstantes._IdProgramasEPU)
                    {
                        this.lblPantallaVigente.Text = UIConstantes.TitulosDepaginas.TitForm03;
                    }
                    else
                    {
                        this.lblPantallaVigente.Text = UIConstantes.TitulosDepaginas.TitForm02;
                    }
                    this.lblPantallaSiguiente.Text = UIConstantes.TitulosDepaginas.TitForm04;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (UIConvertNull.Int32(Session["IdPrograma"]) == UIConstantes._IdPostulacionPreGrado)
                {
                    /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                    //Response.Redirect("frm02_ModalidadPostula.aspx", false);
                    Response.Redirect("frm21_ModalidadColegioPostula.aspx", false);
                    /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                }
                else
                {
                    Response.Redirect("frm03_ProgramasEPU.aspx", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}