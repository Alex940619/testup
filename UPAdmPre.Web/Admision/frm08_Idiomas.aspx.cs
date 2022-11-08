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
    public partial class frm08_Idiomas : BasePage
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
                //Session["AplicanteId"] = 110603;

                if (!IsPostBack)
                {
                    //this.CargarTitulos();
                    this.LimpiarControles();
                    this.MostrarOcultarBotones(false);
                    this.HabilitaControles();
                    this.CargarCombos();
                    this.CargarIdiomasRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F08, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        protected void imgBtnVideo_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            String URL = "frmVideoInformativo.aspx";
            String vtn = "window.open('" + URL + "','video','scrollbars=yes,resizable=no,location=no,width=640,height=380')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }

        protected void imgBtnBack_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            GeneralBL oGeneralBL = null;
            DataTable dtPagAnterior = null;
            String PaginaActual, PaginaAnterior = null;
            try
            {
                Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                oGeneralBL = new GeneralBL();
                dtPagAnterior = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                for (Int32 i = 0; i < dtPagAnterior.Rows.Count; i++)
                {
                    PaginaActual = dtPagAnterior.Rows[i]["NombreFormulario"].ToString();
                    if (PaginaActual == UIConstantes.Formularios.F08)
                    {
                        PaginaAnterior = dtPagAnterior.Rows[i - 1]["NombreFormulario"].ToString();
                        break;
                    }
                }
                Response.Redirect(PaginaAnterior, false);
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
        }

        protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                this.GuardarDatos();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F08, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void BtnAgregaIdioma2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlIdioma2.Visible = true;
            this.BtnOcultaIdioma2.Visible = true;
            this.BtnAgregaIdioma2.Visible = false;
            this.BtnOcultaIdioma3.Visible = false;
            this.BtnAgregaIdioma3.Visible = true;
        }

        protected void BtnOcultaIdioma2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdApplicationEducation = UIConvertNull.Int32(txtApplicationEducationId2.Text);
            if (IdApplicationEducation != null)
            {
                this.EliminaIdiomaRegistrado(IdApplicationEducation);
            }
            this.pnlIdioma2.Visible = false;
            this.pnlIdioma3.Visible = false;
            this.BtnOcultaIdioma2.Visible = false;
            this.BtnAgregaIdioma2.Visible = true;
            this.BtnOcultaIdioma3.Visible = false;
            this.BtnAgregaIdioma3.Visible = false;
            this.CargarIdiomasRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
        }

        protected void BtnOcultaIdioma3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdApplicationEducation = UIConvertNull.Int32(txtApplicationEducationId3.Text);
            if (IdApplicationEducation != null)
            {
                this.EliminaIdiomaRegistrado(IdApplicationEducation);
            }
            this.pnlIdioma3.Visible = false;
            this.BtnOcultaIdioma3.Visible = false;
            this.BtnAgregaIdioma3.Visible = true;
        }

        protected void BtnAgregaIdioma3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlIdioma3.Visible = true;
            this.BtnOcultaIdioma3.Visible = true;
            this.BtnAgregaIdioma3.Visible = false;
        }

        protected void btnLimpiarControl_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.LimpiarControles();
        }

        protected void ddlIdioma1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            if (ddlIdioma1.SelectedValue != "0")
            {
                this.BtnAgregaIdioma2.Visible = true;
                if (ddlIdioma1.SelectedValue == "2")
                {
                    gdv_idioma.DataSource = CargarCertificadosIngles();
                    gdv_idioma.DataBind();
                }
                else
                {
                    gdv_idioma.DataSource = null;
                    gdv_idioma.DataBind();
                }
            }
            else
            {
                this.BtnAgregaIdioma2.Visible = false;
            }
        }

        public DataTable CargarCertificadosIngles()
        {
            DataTable dt = null;
            
            CertificadoBL oCertificadoBL = null;
            try
            {
                oCertificadoBL = new CertificadoBL();
                dt = oCertificadoBL.ListarCarrerasPorModalidad();
                return dt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oCertificadoBL != null)
                    oCertificadoBL = null;                
            }
        }

        protected void ddlIdioma2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            if (ddlIdioma2.SelectedValue != "0")
            {
                this.BtnAgregaIdioma2.Visible = true;
                if (ddlIdioma2.SelectedValue == "2")
                {
                    gdv_idioma2.DataSource = CargarCertificadosIngles();
                    gdv_idioma2.DataBind();
                }
                else
                {
                    gdv_idioma2.DataSource = null;
                    gdv_idioma2.DataBind();
                }
            }
            else
            {
                this.BtnAgregaIdioma2.Visible = false;
            }
        }

        protected void ddlIdioma3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            if (ddlIdioma3.SelectedValue != "0")
            {
                this.BtnAgregaIdioma3.Visible = true;
                if (ddlIdioma3.SelectedValue == "2")
                {
                    gdv_idioma3.DataSource = CargarCertificadosIngles();
                    gdv_idioma3.DataBind();
                }
                else
                {
                    gdv_idioma3.DataSource = null;
                    gdv_idioma3.DataBind();
                }
            }
            else
            {
                this.BtnAgregaIdioma3.Visible = false;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.mpeMostrarError.Hide();
        }   

        #endregion "Eventos"

        #region "Métodos Privados"

        private void HabilitaControles()
        {
            String VideoActivo = ConfigurationManager.AppSettings["VideoActivo"].ToString();
            if (VideoActivo == UIConstantes._valorActivo)
            {
                imgBtnVideo.Visible = true;
            }
            else
            {
                imgBtnVideo.Visible = false;
            }
        }

        private void CargarTitulos()
        {
            GeneralBL oGeneralBL = null;
            DataTable dtTitulos = null;
            String PaginaActual = null;
            try
            {
                Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                oGeneralBL = new GeneralBL();
                dtTitulos = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                for (Int32 i = 0; i < dtTitulos.Rows.Count; i++)
                {
                    PaginaActual = dtTitulos.Rows[i]["NombreFormulario"].ToString();
                    if (PaginaActual == UIConstantes.Formularios.F08)
                    {
                        this.lblPantallaAnterior.Text = dtTitulos.Rows[i - 1]["NombreFicha"].ToString();
                        this.lblPantallaVigente.Text = dtTitulos.Rows[i]["NombreFicha"].ToString();
                        this.lblPantallaSiguiente.Text = dtTitulos.Rows[i + 1]["NombreFicha"].ToString();
                        lblPasos.Text = " Paso " + (Convert.ToInt32(dtTitulos.Rows[i]["NroPaso"]) - 1) + " de " + (dtTitulos.Rows.Count - 1);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCombos()
        {
            this.CargarComboIdiomas();
            //this.CargarComboCertificaciones();
        }

        private void CargarComboIdiomas()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtIdiomas = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.IDIOMA].Key, "", null);
                if (dtIdiomas != null && dtIdiomas.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlIdioma1, dtIdiomas.Copy(), "Descripcion", "Codigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlIdioma2, dtIdiomas.Copy(), "Descripcion", "Codigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlIdioma3, dtIdiomas.Copy(), "Descripcion", "Codigo", "-- Seleccionar --");
                }
                this.ddlIdioma1.Items.Insert(9, new ListItem(UIConstantes._TextoOtroIdioma, UIConstantes._ValorOtroIdioma));
                this.ddlIdioma2.Items.Insert(9, new ListItem(UIConstantes._TextoOtroIdioma, UIConstantes._ValorOtroIdioma));
                this.ddlIdioma3.Items.Insert(9, new ListItem(UIConstantes._TextoOtroIdioma, UIConstantes._ValorOtroIdioma));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void CargarComboCertificaciones()
        //{
        //    GeneralBL oGeneralBL = null;
        //    try
        //    {
        //        oGeneralBL = new GeneralBL();
        //        DataTable dtCertiIdioma = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.CERTIFICADO_IDIOMA].Key, "", null);
        //        if (dtCertiIdioma != null && dtCertiIdioma.Rows.Count > 0)
        //        {
        //            Funciones.cargarComboYSeleccione(this.ddlCertificacionIdioma1, dtCertiIdioma.Copy(), "Descripcion", "Codigo", "-- Seleccionar --");
        //            Funciones.cargarComboYSeleccione(this.ddlCertificacionIdioma2, dtCertiIdioma.Copy(), "Descripcion", "Codigo", "-- Seleccionar --");
        //            Funciones.cargarComboYSeleccione(this.ddlCertificacionIdioma3, dtCertiIdioma.Copy(), "Descripcion", "Codigo", "-- Seleccionar --");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void MostrarOcultarBotones(Boolean blnAccion)
        {
            this.pnlIdioma2.Visible = blnAccion;
            this.pnlIdioma3.Visible = blnAccion;
            this.BtnOcultaIdioma2.Visible = blnAccion;
            this.BtnOcultaIdioma3.Visible = blnAccion;
            this.BtnAgregaIdioma3.Visible = blnAccion;
        }

        private void GuardarDatos()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            GeneralBL oGeneralBL = null;
            DataTable dtPagSigui = null;
            String PaginaActual, PaginaSiguiente = null;
            try
            {
                Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                if (ddlIdioma1.SelectedValue != "0" && ddlIdioma2.SelectedValue != "0")
                {
                    if (ddlIdioma1.SelectedValue == ddlIdioma2.SelectedValue)
                    {
                        this.lblmessage.Text = "El idioma 1 es igual al idioma 2.";
                        this.mpeMostrarError.Show();
                        this.ddlIdioma2.Focus();
                        return;
                    }
                }
                if (ddlIdioma1.SelectedValue != "0" && ddlIdioma3.SelectedValue != "0")
                {
                    if (ddlIdioma1.SelectedValue == ddlIdioma3.SelectedValue)
                    {
                        this.lblmessage.Text = "El idioma 1 es igual al idioma 3.";
                        this.mpeMostrarError.Show();
                        this.ddlIdioma3.Focus();
                        return;
                    }
                }
                if (ddlIdioma2.SelectedValue != "0" && ddlIdioma3.SelectedValue != "0")
                {
                    if (ddlIdioma2.SelectedValue == ddlIdioma3.SelectedValue)
                    {
                        this.lblmessage.Text = "El idioma 2 es igual al idioma 3.";
                        this.mpeMostrarError.Show();
                        this.ddlIdioma3.Focus();
                        return;
                    }
                }
                if (ddlIdioma1.SelectedValue == "2")
                {
                    bool validacion = false;
                    for (int i = 0; i < gdv_idioma.Rows.Count; i++)
                    {
                        if (((CheckBox)gdv_idioma.Rows[i].Cells[0].FindControl("chkCtrl")).Checked)
                        {
                            validacion = true;
                            break;
                        }
                    }
                    if (!validacion)
                    {
                        this.lblmessage.Text = "Debe seleccionar alguna certificación";
                        this.mpeMostrarError.Show();
                        this.ddlIdioma1.Focus();
                        return;
                    }
                }
                else
                {
                    if (ddlIdioma2.SelectedValue == "2")
                    {
                        bool validacion = false;
                        for (int i = 0; i < gdv_idioma2.Rows.Count; i++)
                        {
                            if (((CheckBox)gdv_idioma2.Rows[i].Cells[0].FindControl("chkCtrl")).Checked)
                            {
                                validacion = true;
                                break;
                            }
                        }
                        if (!validacion)
                        {
                            this.lblmessage.Text = "Debe seleccionar alguna certificación";
                            this.mpeMostrarError.Show();
                            this.ddlIdioma2.Focus();
                            return;
                        }
                    }
                    else if (ddlIdioma3.SelectedValue == "2")
                    {
                        bool validacion = false;
                        for (int i = 0; i < gdv_idioma3.Rows.Count; i++)
                        {
                            if (((CheckBox)gdv_idioma3.Rows[i].Cells[0].FindControl("chkCtrl")).Checked)
                            {
                                validacion = true;
                                break;
                            }
                        }
                        if (!validacion)
                        {
                            this.lblmessage.Text = "Debe seleccionar alguna certificación";
                            this.mpeMostrarError.Show();
                            this.ddlIdioma3.Focus();
                            return;
                        }
                    }
                }

                oAplicanteBE = new AplicanteBE();
                oAplicanteBE = this.obtenerDatosIdioma(oAplicanteBE);

                if (oAplicanteBE.LIdioma.Count > 0)
                {
                    oAplicanteBL = new AplicanteBL();
                    Boolean operacionOK = oAplicanteBL.InsertaDatosFormSiete_Idiomas(oAplicanteBE);
                    if (operacionOK)
                    {
                        oGeneralBL = new GeneralBL();
                        dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                        for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                        {
                            PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                            if (PaginaActual == UIConstantes.Formularios.F08)
                            {
                                PaginaSiguiente = dtPagSigui.Rows[i + 1]["NombreFormulario"].ToString();
                                break;
                            }
                        }
                        Response.Redirect(PaginaSiguiente, false);
                    }
                    else
                    {
                        Session["EstadoEnvio"] = null;
                        Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                    }
                }
                else
                {
                    oGeneralBL = new GeneralBL();
                    dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    {
                        PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F08)
                        {
                            PaginaSiguiente = dtPagSigui.Rows[i + 1]["NombreFormulario"].ToString();
                            break;
                        }
                    }
                    Response.Redirect(PaginaSiguiente, false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private AplicanteBE obtenerDatosIdioma(AplicanteBE oAplicanteBE)
        {
            IdiomaBE Idioma1 = null;
            IdiomaBE Idioma2 = null;
            IdiomaBE Idioma3 = null;
            try
            {
                if (oAplicanteBE.LIdioma == null)
                {
                    oAplicanteBE.LIdioma = new System.Collections.Generic.List<IdiomaBE>();
                }

                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);

                //======================================================================
                // IDIOMA 1
                //======================================================================
                Idioma1 = new IdiomaBE();
                if (ddlIdioma1.SelectedValue != "0" && rblNivelConocIdioma1.SelectedValue != null)
                {
                    if (this.txtApplicationEducationId1.Text != null && txtApplicationEducationId1.Text != String.Empty)
                    {
                        Idioma1.IdApplicationEducation = UIConvertNull.Int32(txtApplicationEducationId1.Text);
                    }
                    if (ddlIdioma1.SelectedValue != UIConstantes._ValorOtroIdioma)
                    {
                        Idioma1.IdIdioma = UIConvertNull.Int32(ddlIdioma1.SelectedValue);
                        if (ddlIdioma1.SelectedValue == "2")
                        {
                            for (int i = 0; i < gdv_idioma.Rows.Count; i++)
                            {
                                if (((CheckBox)gdv_idioma.Rows[i].Cells[0].FindControl("chkCtrl")).Checked)
                                {
                                    Idioma1.CertificacionId = Int32.Parse(gdv_idioma.Rows[i].Cells[3].Text);
                                }
                            }
                        }
                    }
                    else
                    {
                        Idioma1.IdIdioma = UIConvertNull.Int32(UIConstantes._ValorOtroIdioma);
                        Idioma1.OtrosIdiomas = txtOtroIdioma1.Text;
                    }
                    Idioma1.NivelEscritura = rblNivelConocIdioma1.SelectedValue;
                    Idioma1.NivelLectura = rblNivelConocIdioma1.SelectedValue;
                    Idioma1.NivelHabla = rblNivelConocIdioma1.SelectedValue;

                    //if (ddlCertificacionIdioma1.SelectedValue != UIConstantes._ValorOtraCertificacion)
                    //{
                    //    Idioma1.CertificacionId = UIConvertNull.Int32(ddlCertificacionIdioma1.SelectedValue);
                    //}
                    //else
                    //{
                    //    Idioma1.CertificacionId = UIConvertNull.Int32(UIConstantes._ValorOtraCertificacion);
                    //    Idioma1.OtrasCertificaciones = txtOtroCertIdioma1.Text;
                    //    Idioma1.Puntaje = txtPuntajeIdioma1.Text;
                    //}                    
                    Idioma1.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                }
                else
                {
                    Idioma1 = null;
                }

                //======================================================================
                // IDIOMA 2
                //======================================================================
                Idioma2 = new IdiomaBE();
                if (ddlIdioma2.SelectedValue != "0" & rblNivelConocIdioma2.SelectedValue != null)
                {
                    if (txtApplicationEducationId2.Text != null && txtApplicationEducationId2.Text != String.Empty)
                    {
                        Idioma2.IdApplicationEducation = UIConvertNull.Int32(txtApplicationEducationId2.Text);
                    }
                    if (ddlIdioma2.SelectedValue != UIConstantes._ValorOtroIdioma)
                    {
                        Idioma2.IdIdioma = UIConvertNull.Int32(ddlIdioma2.SelectedValue);
                        if (ddlIdioma2.SelectedValue == "2")
                        {
                            for (int i = 0; i < gdv_idioma2.Rows.Count; i++)
                            {
                                if (((CheckBox)gdv_idioma2.Rows[i].Cells[0].FindControl("chkCtrl")).Checked)
                                {
                                    var a = gdv_idioma2.Rows[i].Cells[3].Text;
                                    Idioma2.CertificacionId = Int32.Parse(gdv_idioma2.Rows[i].Cells[3].Text);
                                }
                            }
                        }
                    }
                    else
                    {
                        Idioma2.IdIdioma = UIConvertNull.Int32(UIConstantes._ValorOtroIdioma);
                        Idioma2.OtrosIdiomas = txtOtroIdioma2.Text;
                    }

                    Idioma2.NivelEscritura = rblNivelConocIdioma2.SelectedValue;
                    Idioma2.NivelLectura = rblNivelConocIdioma2.SelectedValue;
                    Idioma2.NivelHabla = rblNivelConocIdioma2.SelectedValue;

                    //if (ddlCertificacionIdioma2.SelectedValue != UIConstantes._ValorOtraCertificacion)
                    //{
                    //    Idioma2.CertificacionId = UIConvertNull.Int32(ddlCertificacionIdioma2.SelectedValue);
                    //}
                    //else
                    //{
                    //    Idioma2.CertificacionId = UIConvertNull.Int32(UIConstantes._ValorOtraCertificacion);
                    //    Idioma2.OtrasCertificaciones = txtOtroCertIdioma2.Text;
                    //    Idioma2.Puntaje = txtPuntajeIdioma2.Text;
                    //}
                    Idioma2.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                }
                else
                {
                    Idioma2 = null;
                }

                //======================================================================
                // IDIOMA 3
                //======================================================================
                Idioma3 = new IdiomaBE();
                if (ddlIdioma3.SelectedValue != "0" & rblNivelConocIdioma3.SelectedValue != null)
                {
                    if (txtApplicationEducationId3.Text != null && txtApplicationEducationId3.Text != String.Empty)
                    {
                        Idioma3.IdApplicationEducation = UIConvertNull.Int32(txtApplicationEducationId3.Text);
                    }
                    if (ddlIdioma3.SelectedValue != UIConstantes._ValorOtroIdioma)
                    {
                        Idioma3.IdIdioma = UIConvertNull.Int32(ddlIdioma3.SelectedValue);
                        if (ddlIdioma3.SelectedValue == "2")
                        {
                            for (int i = 0; i < gdv_idioma3.Rows.Count; i++)
                            {
                                if (((CheckBox)gdv_idioma3.Rows[i].Cells[0].FindControl("chkCtrl")).Checked)
                                {
                                    Idioma3.CertificacionId = Int32.Parse(gdv_idioma3.Rows[i].Cells[3].Text);
                                }
                            }
                        }
                    }
                    else
                    {
                        Idioma3.IdIdioma = UIConvertNull.Int32(UIConstantes._ValorOtroIdioma);
                        Idioma3.OtrosIdiomas = txtOtroIdioma1.Text;
                    }
                    
                    Idioma3.NivelEscritura = rblNivelConocIdioma3.SelectedValue;
                    Idioma3.NivelLectura = rblNivelConocIdioma3.SelectedValue;
                    Idioma3.NivelHabla = rblNivelConocIdioma3.SelectedValue;

                    //if (ddlCertificacionIdioma3.SelectedValue != UIConstantes._ValorOtraCertificacion)
                    //{
                    //    Idioma3.CertificacionId = UIConvertNull.Int32(ddlCertificacionIdioma3.SelectedValue);
                    //}
                    //else
                    //{
                    //    Idioma3.CertificacionId = UIConvertNull.Int32(UIConstantes._ValorOtraCertificacion);
                    //    Idioma3.OtrasCertificaciones = txtOtroCertIdioma3.Text;
                    //    Idioma3.Puntaje = txtPuntajeIdioma3.Text;
                    //}
                    Idioma3.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                }
                else
                {
                    Idioma3 = null;
                }

                ///========================================================
                if (oAplicanteBE != null)
                {
                    if (oAplicanteBE.LIdioma == null)
                    {
                        oAplicanteBE.LIdioma = new List<IdiomaBE>();
                    }
                    if (Idioma1 != null)
                    {
                        oAplicanteBE.LIdioma.Add(Idioma1);
                    }
                    if (Idioma2 != null)
                    {
                        oAplicanteBE.LIdioma.Add(Idioma2);
                    }
                    if (Idioma3 != null)
                    {
                        oAplicanteBE.LIdioma.Add(Idioma3);
                    }
                }
                return oAplicanteBE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarIdiomasRegistrados(Int32? AplicanteId)
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ObtenerIdiomaRegistrado(AplicanteId);
                this.LLenarDatosIdiomasRegistrados(oAplicanteBE);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosIdiomasRegistrados(AplicanteBE oAplicanteBE)
        {
            if (oAplicanteBE != null)
            {
                if (oAplicanteBE.LIdioma != null)
                {
                    for (int indice = 0; indice < oAplicanteBE.LIdioma.Count; indice++)
                    {
                        IdiomaBE oIdiomaBE = oAplicanteBE.LIdioma[indice];
                        switch (indice)
                        {
                            case 0:
                                this.txtApplicationEducationId1.Text = UIConvertNull.String(oIdiomaBE.IdApplicationEducation);
                                this.ddlIdioma1.SelectedValue = UIConvertNull.String(oIdiomaBE.IdIdioma);
                                if (ddlIdioma1.SelectedValue == "2")
                                {
                                    gdv_idioma.DataSource = CargarCertificadosIngles();
                                    gdv_idioma.DataBind();
                                    if (oIdiomaBE.CertificacionId != null)
                                    {
                                        for (int i = 0; i < gdv_idioma.Rows.Count; i++)
                                        {
                                            if (oIdiomaBE.CertificacionId == int.Parse(gdv_idioma.Rows[i].Cells[3].Text))
                                            {
                                                ((CheckBox)(gdv_idioma.Rows[i].Cells[0].FindControl("chkCtrl"))).Checked = true;
                                            }
                                        }
                                    }
                                }
                                this.rblNivelConocIdioma1.SelectedValue = oIdiomaBE.NivelLectura;
                                //this.ddlCertificacionIdioma1.SelectedValue = UIConvertNull.String(oIdiomaBE.CertificacionId);
                                this.txtOtroIdioma1.Text = oIdiomaBE.OtrosIdiomas;
                                //this.txtOtroCertIdioma1.Text = oIdiomaBE.OtrasCertificaciones;
                                //this.txtPuntajeIdioma1.Text = oIdiomaBE.Puntaje;
                                this.BtnAgregaIdioma2.Visible = true;
                                break;
                            case 1:
                                this.txtApplicationEducationId2.Text = UIConvertNull.String(oIdiomaBE.IdApplicationEducation);
                                this.ddlIdioma2.SelectedValue = UIConvertNull.String(oIdiomaBE.IdIdioma);
                                if (ddlIdioma2.SelectedValue == "2")
                                {
                                    gdv_idioma2.DataSource = CargarCertificadosIngles();
                                    gdv_idioma2.DataBind();
                                    if (oIdiomaBE.CertificacionId != null)
                                    {
                                        for (int i = 0; i < gdv_idioma2.Rows.Count; i++)
                                        {
                                            if (oIdiomaBE.CertificacionId == int.Parse(gdv_idioma2.Rows[i].Cells[3].Text))
                                            {
                                                ((CheckBox)(gdv_idioma2.Rows[i].Cells[0].FindControl("chkCtrl"))).Checked = true;
                                            }
                                        }
                                    }
                                }
                                this.rblNivelConocIdioma2.SelectedValue = oIdiomaBE.NivelLectura;
                                //this.ddlCertificacionIdioma2.SelectedValue = UIConvertNull.String(oIdiomaBE.CertificacionId);
                                this.txtOtroIdioma2.Text = oIdiomaBE.OtrosIdiomas;
                                //this.txtOtroCertIdioma2.Text = oIdiomaBE.OtrasCertificaciones;
                                //this.txtPuntajeIdioma2.Text = oIdiomaBE.Puntaje;
                                this.BtnAgregaIdioma2.Visible = false;
                                this.BtnOcultaIdioma2.Visible = true;
                                this.BtnAgregaIdioma3.Visible = true;
                                this.BtnOcultaIdioma3.Visible = false;
                                this.pnlIdioma2.Visible = true;
                                break;
                            case 2:
                                this.txtApplicationEducationId3.Text = UIConvertNull.String(oIdiomaBE.IdApplicationEducation);
                                this.ddlIdioma3.SelectedValue = UIConvertNull.String(oIdiomaBE.IdIdioma);
                                if (ddlIdioma3.SelectedValue == "2")
                                {
                                    gdv_idioma3.DataSource = CargarCertificadosIngles();
                                    gdv_idioma3.DataBind();
                                    if (oIdiomaBE.CertificacionId != null)
                                    {
                                        for (int i = 0; i < gdv_idioma3.Rows.Count; i++)
                                        {
                                            if (oIdiomaBE.CertificacionId == int.Parse(gdv_idioma3.Rows[i].Cells[3].Text))
                                            {
                                                ((CheckBox)(gdv_idioma3.Rows[i].Cells[0].FindControl("chkCtrl"))).Checked = true;
                                            }
                                        }
                                    }
                                }
                                this.rblNivelConocIdioma3.SelectedValue = oIdiomaBE.NivelLectura;
                                //this.ddlCertificacionIdioma3.SelectedValue = UIConvertNull.String(oIdiomaBE.CertificacionId);
                                this.txtOtroIdioma3.Text = oIdiomaBE.OtrosIdiomas;
                                //this.txtOtroCertIdioma3.Text = oIdiomaBE.OtrasCertificaciones;
                                //this.txtPuntajeIdioma3.Text = oIdiomaBE.Puntaje;
                                this.BtnAgregaIdioma2.Visible = false;
                                this.BtnOcultaIdioma2.Visible = true;
                                this.BtnAgregaIdioma3.Visible = false;
                                this.BtnOcultaIdioma3.Visible = true;
                                this.pnlIdioma3.Visible = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private Boolean EliminaIdiomaRegistrado(Int32? IdApplicationEducation)
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            AplicanteBL oAplicanteBL = null;
            Boolean Resultado = false;
            try
            {
                oAplicanteBL = new AplicanteBL();
                Resultado = oAplicanteBL.EliminaIdiomaRegistrado(IdApplicationEducation, AplicanteId);
                if (Resultado == true)
                {
                    this.LimpiarControles();
                    this.MostrarOcultarBotones(false);
                    this.CargarIdiomasRegistrados(AplicanteId);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarControles()
        {
            this.ddlIdioma1.ClearSelection();
            this.rblNivelConocIdioma1.ClearSelection();
            //this.ddlCertificacionIdioma1.ClearSelection();
            //this.txtOtroCertIdioma1.Text = UIConstantes._valorCadenaVacia;
            //this.txtPuntajeIdioma1.Text = UIConstantes._valorCadenaVacia;
            this.txtApplicationEducationId1.Text = UIConstantes._valorCadenaVacia;

            this.ddlIdioma2.ClearSelection();
            this.rblNivelConocIdioma2.ClearSelection();
            //this.ddlCertificacionIdioma2.ClearSelection();
            //this.txtOtroCertIdioma2.Text = UIConstantes._valorCadenaVacia;
            //this.txtPuntajeIdioma2.Text = UIConstantes._valorCadenaVacia;
            this.txtApplicationEducationId2.Text = UIConstantes._valorCadenaVacia;

            this.ddlIdioma3.ClearSelection();
            this.rblNivelConocIdioma3.ClearSelection();
            //this.ddlCertificacionIdioma3.ClearSelection();
            //this.txtOtroCertIdioma3.Text = UIConstantes._valorCadenaVacia;
            //this.txtPuntajeIdioma3.Text = UIConstantes._valorCadenaVacia;
            this.txtApplicationEducationId3.Text = UIConstantes._valorCadenaVacia;
        }

        #endregion "Métodos Privados"        
    }
}
