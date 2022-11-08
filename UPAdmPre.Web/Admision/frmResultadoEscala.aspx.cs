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


namespace UPAdmPre.Web.Admision
{
    public partial class frmResultadoEscala : System.Web.UI.Page
    {
        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {

                ///*Solo para pruebas*/
                //Session["usrRedId"] = "U.Admision";
                //Session["ModPostulacion"] = 40;
                //Session["ApplicationId"] = 87794;

                if (!IsPostBack)
                {
                    this.CargarInformacion();
                    
                    //this.CargarTitulos();
                    //this.obtenerPermisoEmisionBoleta(UIConvertNull.Int32(Session["ApplicationId"].ToString()));
                    //String strOpcion = Request.QueryString["strOpcion"];
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F20, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["ApplicationId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        #endregion "Eventos"

        #region "Métodos Privados"

        private void CargarInformacion()
        {
            //AplicanteBE oAplicanteBE = null;
            PostulanteBE oPostulanteBE = null;
            ResultadoBE oResultadoBE = null;
            ResultadoBL oResultadoBL = null;
            PostulanteBL oPostulanteBL = null;
            EncryptBL oEncryptBL = null;

            DataTable dtResultado = null;
            DataTable dtPostulante = null;
            String strProceso = "ADMISION";
            Int32? ApplicationId = null;
            String strOrigen = Request.QueryString["Origen"];

            String cadQR = "https://chart.googleapis.com/chart?chs=150x150Lcht=qr&chl=";

            try
            {

                oResultadoBL = new ResultadoBL();
                dtResultado = oResultadoBL.ObtenerResultadoAdmision(strProceso);
                if (dtResultado != null && dtResultado.Rows.Count > 0)
                {
                    ApplicationId = UIConvertNull.Int32(Session["ApplicationId"]);
                    if (!String.IsNullOrEmpty(strOrigen))
                    {
                        ApplicationId = Convert.ToInt32(HttpUtility.HtmlDecode(oEncryptBL.DecryptKey(Request.QueryString["ApplicationId"])));
                    }
                    else
                    {
                        //ApplicationId = Convert.ToInt32(Request.QueryString["ApplicationId"]);
                        ApplicationId = UIConvertNull.Int32(Session["ApplicationId"]);
                    }

                    oPostulanteBE = new PostulanteBE();
                    oPostulanteBE.UserName = Session["usrRedId"].ToString();
                    oPostulanteBE.Proceso = "ADMISION";

                    oPostulanteBL = new PostulanteBL();
                    dtPostulante = oPostulanteBL.buscarInfoPostulante(oPostulanteBE, ApplicationId);
                    if (dtPostulante != null && dtPostulante.Rows.Count > 0)
                    {
                        oResultadoBE = new ResultadoBE();
                        oResultadoBE.Anio = dtResultado.Rows[0]["ACADEMIC_YEAR"].ToString();
                        oResultadoBE.Periodo = dtResultado.Rows[0]["ACADEMIC_TERM"].ToString();
                        oResultadoBE.ActivacionWeb = UIConvertNull.Int32(dtResultado.Rows[0]["WEB_ACTIVACION"].ToString());
                        oResultadoBE.MsjPrevio = dtResultado.Rows[0]["PREVIOUS_MESSAGE"].ToString();
                        oResultadoBE.MsjSeleccionado = dtResultado.Rows[0]["SELECTED_MESSAGE"].ToString();
                        oResultadoBE.MsjNoSeleccionado = dtResultado.Rows[0]["NOSELECTED_MESSAGE"].ToString();

                        if (oResultadoBE.ActivacionWeb == 1 || strOrigen == "UP")
                        {
                            String strPEOPLE_ID = dtPostulante.Rows[0]["People_Id"].ToString();
                            oPostulanteBE.Codigo = dtPostulante.Rows[0]["GOVERNMENT_ID"].ToString();
                            oPostulanteBE.Nombre = dtPostulante.Rows[0]["People_Name"].ToString();
                            oPostulanteBE.Decision = dtPostulante.Rows[0]["LONG_DESC"].ToString();
                            String StrIdentificador = dtPostulante.Rows[0]["ROWGUID"].ToString();

                            if (oPostulanteBE.Decision == "SELECCIONADO" || oPostulanteBE.Decision == "INGRESÓ")
                            {
                                //this.imgPublicidad.Visible = false;
                                this.LblCodigo.Visible = true;
                                this.LblNombre.Visible = true;
                                this.LblResultado.Visible = true;
                                this.LblSaludo.Visible = true;
                                this.LblTitulo.Visible = true;
                                //Me.ImgFoto.Visible = True
                                this.LblTituloCodigo.Visible = true;
                                this.LblTituloNombre.Visible = true;
                                this.LblParrafo.Visible = true;

                                //Me.LblIdetificador.Visible = True
                                this.imgQR.Visible = true;

                                this.LblSaludo.Text = "¡FELICITACIONES!";
                                this.LblResultado.Text = oPostulanteBE.Decision;
                                this.LblTitulo.Text = "RESULTADOS ADMISIÓN " + oResultadoBE.Anio;
                                this.LblCodigo.Text = oPostulanteBE.Codigo;
                                this.LblNombre.Text = oPostulanteBE.Nombre;

                                //Me.LblIdetificador.Text = StrIdentificador
                                this.imgQR.ImageUrl = (cadQR == string.Empty ? "" : cadQR + StrIdentificador);

                                //Me.ImgFoto.ImageUrl = "https://autoservicio.up.edu.pe/pcampus_fotos/P" & strPEOPLE_ID & ".jpg"
                                this.LblParrafo.Text = oResultadoBE.MsjSeleccionado;
                            }
                            else
                            {

                                this.LblCodigo.Visible = true;
                                this.LblNombre.Visible = true;
                                this.LblResultado.Visible = true;
                                this.LblSaludo.Visible = false;
                                //this.imgPublicidad.Visible = false;
                                this.LblTitulo.Visible = true;
                                //Me.ImgFoto.Visible = True
                                this.LblTituloCodigo.Visible = true;
                                this.LblTituloNombre.Visible = true;
                                this.LblParrafo.Visible = true;

                                //Me.LblIdetificador.Visible = True
                                this.imgQR.Visible = true;

                                this.LblResultado.Text = oPostulanteBE.Decision;
                                this.LblTitulo.Text = "RESULTADOS ADMISIÓN " + oResultadoBE.Anio;
                                this.LblCodigo.Text = oPostulanteBE.Codigo;
                                this.LblNombre.Text = oPostulanteBE.Nombre;

                                //Me.LblIdetificador.Text = StrIdentificador
                                this.imgQR.ImageUrl = (cadQR == string.Empty ? "" : cadQR + StrIdentificador);

                                //Me.ImgFoto.ImageUrl = "https://autoservicio.up.edu.pe/pcampus_fotos/P" & strPEOPLE_ID & ".jpg"
                                this.LblParrafo.Text = oResultadoBE.MsjNoSeleccionado;
                            }
                        }
                        else
                        {
                            this.LblCodigo.Visible = false;
                            this.LblNombre.Visible = false;
                            this.LblResultado.Visible = false;
                            this.LblSaludo.Visible = false;
                            this.LblTitulo.Visible = false;
                            //Me.ImgFoto.Visible = False
                            this.LblTituloCodigo.Visible = false;
                            this.LblTituloNombre.Visible = false;

                            //Me.LblIdetificador.Visible = False
                            this.imgQR.Visible = false;

                            this.LblParrafo.Visible = true;
                            //this.imgPublicidad.Visible = false;
                            this.LblParrafo.Text = oResultadoBE.MsjPrevio;

                        }
                    }
                    else
                    {
                        this.LblCodigo.Visible = false;
                        this.LblNombre.Visible = false;
                        this.LblResultado.Visible = false;
                        this.LblSaludo.Visible = false;
                        this.LblTitulo.Visible = false;
                        //Me.ImgFoto.Visible = False
                        this.LblTituloCodigo.Visible = false;
                        this.LblTituloNombre.Visible = false;

                        //Me.LblIdetificador.Visible = False
                        this.imgQR.Visible = false;

                        this.LblParrafo.Visible = false;
                        //Me.imgLogo.Visible = False
                        //this.imgPublicidad.Visible = true;
                    }
                }
                else
                {
                    this.LblCodigo.Visible = false;
                    this.LblNombre.Visible = false;
                    this.LblResultado.Visible = false;
                    this.LblSaludo.Visible = false;
                    this.LblTitulo.Visible = false;
                    //Me.ImgFoto.Visible = False
                    this.LblTituloCodigo.Visible = false;
                    this.LblTituloNombre.Visible = false;

                    //Me.LblIdetificador.Visible = False
                    this.imgQR.Visible = false;

                    this.LblParrafo.Visible = false;
                    //this.imgPublicidad.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion "Métodos Privados"
    }
}