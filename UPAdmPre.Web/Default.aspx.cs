using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string formMod = "Admision/frm21_ModalidadColegioPostula.aspx";
            if (!IsPostBack)
            {
                AplicanteBL oAplicanteBL = null;
                EncryptBL oEncryptBL = new EncryptBL();
                String AplicanteId = null;

                if (!String.IsNullOrEmpty(Request.QueryString["ApplicationId"]))
                {
                    String urlAplicanteId = oEncryptBL.DecryptKey(Request.QueryString["ApplicationId"]);
                    AplicanteId = urlAplicanteId;
                }
                else
                {
                    AplicanteId = null;
                }
                String usrRedId = Request.QueryString["usrredid"];
                String IdPrograma = Request.QueryString["IdPrograma"];

                Session["usrRedId"] = usrRedId;
                if (AplicanteId != null && AplicanteId != "")
                {
                    Session["AplicanteId"] = AplicanteId;
                }
                else
                {
                    Session["AplicanteId"] = null;
                }
                Session["IdPrograma"] = IdPrograma;

                if (usrRedId != null)
                {
                    String VideoActivo = ConfigurationManager.AppSettings["VideoActivo"].ToString();
                    if (IdPrograma == "7")
                    {
                        String strEstado = null;
                        DataTable dtEstado = null;
                        oAplicanteBL = new AplicanteBL();
                        try
                        {
                            dtEstado = oAplicanteBL.ObtenerEstadoPostulante(UIConvertNull.Int32(Session["AplicanteId"]));
                            if (dtEstado != null && dtEstado.Rows.Count > 0)
                            {
                                strEstado = dtEstado.Rows[0][0].ToString();
                                Session["ModPostulacion"] = dtEstado.Rows[0][2].ToString();
                                if (strEstado == "0")
                                {
                                    if (VideoActivo == UIConstantes._valorActivo)
                                    {
                                        Response.Redirect("Admision/frm01_VideoIntro.aspx", false);
                                    }
                                    else
                                    {
                                        /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                                        Response.Redirect(formMod, false);
                                        /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                                    }
                                }
                                else
                                {
                                    /// Ingresar aqui el codigo de Validación
                                    DataTable dtAplicaEntrevista = null;
                                    oAplicanteBL = new AplicanteBL();
                                    dtAplicaEntrevista = oAplicanteBL.ObtenerSiAplicaEntrevista(UIConvertNull.Int32(Session["AplicanteId"]));
                                    if (dtAplicaEntrevista != null && dtAplicaEntrevista.Rows.Count > 0)
                                    {
                                        Int32? intEntrevista = UIConvertNull.Int32(dtAplicaEntrevista.Rows[0][0].ToString());
                                        Int32? intDocAdicional = UIConvertNull.Int32(dtAplicaEntrevista.Rows[0][1].ToString());
                                        if (intEntrevista == 1 && intDocAdicional == 0)
                                        {
                                            Response.Redirect("Admision/frm18_Entrevista.aspx", false);
                                        }
                                        else
                                            if (intEntrevista == 0 && intDocAdicional == 1)
                                            {
                                                Response.Redirect("Admision/frm19_DocAdiconales.aspx", false);
                                            }
                                            else
                                            {
                                                Response.Redirect("Admision/frm17_ResumenFinal.aspx", false);
                                            }
                                    }
                                }
                            }
                            else
                            {
                                if (VideoActivo == UIConstantes._valorActivo)
                                {
                                    Response.Redirect("Admision/frm01_VideoIntro.aspx", false);
                                }
                                else
                                {
                                    /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                                    Response.Redirect(formMod, false);
                                    /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            UIHelper.EnviarCorreo(UIConstantes.Formularios.F22, ex.Message.Replace("\n", ""), 
                                UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                            Response.Redirect("frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                                [UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
                        }
                        
                    }

                    if (IdPrograma == "19")
                    {
                        if (VideoActivo == UIConstantes._valorActivo)
                        {
                            Response.Redirect("Admision/frm01_VideoIntro.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("Admision/frm03_ProgramasEPU.aspx", false);
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string formMod = "Admision/frm21_ModalidadColegioPostula.aspx";
            int? tempApplicationId = null;
            if (Request.QueryString["tempId"] != null)
            {
                int tempId = 0;
                int.TryParse(Request.QueryString["tempId"].ToString(), out tempId);
                tempApplicationId = tempId;
            }
            HttpContext.Current.Session["ApplicationIdKey"] = 475;

            if (txtUsuarioRed.Text != null)
            {
                Session["usrRedId"] = txtUsuarioRed.Text;
                if (txtApplicationId.Text.ToString() != "" & txtApplicationId.Text.ToString() != null)
                {
                    Session["AplicanteId"] = txtApplicationId.Text;
                }
                else
                {
                    Session["AplicanteId"] = null;
                }
                Session["IdPrograma"] = ddlPrograma.SelectedValue;

                /******************************************************************************/
                String VideoActivo = ConfigurationManager.AppSettings["VideoActivo"].ToString();
                if (ddlPrograma.SelectedValue == "7")
                {
                    AplicanteBL oAplicanteBL = null;

                    String strEstado = null;
                    DataTable dtEstado = null;
                    oAplicanteBL = new AplicanteBL();
                    try
                    {
                        dtEstado = oAplicanteBL.ObtenerEstadoPostulante(UIConvertNull.Int32(Session["AplicanteId"]));
                        if (dtEstado != null && dtEstado.Rows.Count > 0)
                        {
                            strEstado = dtEstado.Rows[0][0].ToString();
                            Session["ModPostulacion"] = dtEstado.Rows[0][2].ToString();
                            if (strEstado == "0")
                            {
                                if (VideoActivo == UIConstantes._valorActivo)
                                {
                                    Response.Redirect("Admision/frm01_VideoIntro.aspx", false);
                                }
                                else
                                {
                                    /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                                    Response.Redirect(formMod, false);
                                    /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                                }
                            }
                            else
                            {
                                /// Ingresar aqui el codigo de Validación
                                DataTable dtAplicaEntrevista = null;
                                oAplicanteBL = new AplicanteBL();
                                dtAplicaEntrevista = oAplicanteBL.ObtenerSiAplicaEntrevista(UIConvertNull.Int32(Session["AplicanteId"]));
                                if (dtAplicaEntrevista != null && dtAplicaEntrevista.Rows.Count > 0)
                                {
                                    Int32? intEntrevista = UIConvertNull.Int32(dtAplicaEntrevista.Rows[0][0].ToString());
                                    Int32? intDocAdicional = UIConvertNull.Int32(dtAplicaEntrevista.Rows[0][1].ToString());
                                    if (intEntrevista == 1 && intDocAdicional == 0)
                                    {
                                        Response.Redirect("Admision/frm18_Entrevista.aspx", false);
                                    }
                                    else
                                        if (intEntrevista == 0 && intDocAdicional == 1)
                                        {
                                            Response.Redirect("Admision/frm19_DocAdiconales.aspx", false);
                                        }
                                        else
                                        {
                                            Response.Redirect("Admision/frm17_ResumenFinal.aspx", false);
                                        }
                                }
                            }
                        }
                        else
                        {
                            if (VideoActivo == UIConstantes._valorActivo)
                            {
                                Response.Redirect("Admision/frm01_VideoIntro.aspx", false);
                            }
                            else
                            {
                                /*Ini:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                                //Response.Redirect("Admision/frm02_ModalidadPostula.aspx", false);
                                Response.Redirect(formMod, false);
                                /*Fin:Christian Ramirez - GIIT[caso 64015] - 20190619*/
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        UIHelper.EnviarCorreo(UIConstantes.Formularios.F22, ex.Message.Replace("\n", ""), 
                            UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                        Response.Redirect("frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                            [UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
                    }
                    
                }

                if (ddlPrograma.SelectedValue == "19")
                {
                    if (VideoActivo == UIConstantes._valorActivo)
                    {
                        Response.Redirect("Admision/frm01_VideoIntro.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("Admision/frm03_ProgramasEPU.aspx", false);
                    }
                }
            }
        }
    }
}
