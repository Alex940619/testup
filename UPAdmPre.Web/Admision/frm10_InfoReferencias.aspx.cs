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
    public partial class frm10_InfoReferencias : BasePage
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
                    this.HabilitaControles();
                    //this.CargarTitulos();
                    this.LimpiarControles();
                    this.MostrarOcultarBotones(false);
                    this.CargarInfoReferenciaRegistrada(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F10, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
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
                    if (PaginaActual == UIConstantes.Formularios.F10)
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
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F10, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void btnAgregaRef3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlRef3.Visible = true;
            //this.btnAgregaRef3.Visible = false;
            //this.btnQuitarRef3.Visible = true;
            //this.btnAgregaRef4.Visible = true;
        }

        protected void btnQuitarRef3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdReferencia = UIConvertNull.Int32(txtIdReferencia3.Text);
            if (IdReferencia != null)
            {
                this.EliminaInfoReferenciaRegistrada(IdReferencia);
            }
            this.pnlRef3.Visible = false;
            this.pnlRef4.Visible = false;
            this.pnlRef5.Visible = false;
            //this.btnQuitarRef3.Visible = false;
            //this.btnAgregaRef3.Visible = true;
            //this.btnQuitarRef4.Visible = false;
            //this.btnAgregaRef4.Visible = false;
            //this.btnQuitarRef5.Visible = false;
            //this.btnAgregaRef5.Visible = false;
        }

        protected void btnQuitarRef4_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdReferencia = UIConvertNull.Int32(txtIdReferencia4.Text);
            if (IdReferencia != null)
            {
                this.EliminaInfoReferenciaRegistrada(IdReferencia);
            }
            this.pnlRef4.Visible = false;
            this.pnlRef5.Visible = false;
            //this.btnQuitarRef4.Visible = false;
            //this.btnAgregaRef4.Visible = true;
            //this.btnQuitarRef5.Visible = false;
            //this.btnAgregaRef5.Visible = false;
        }

        protected void btnAgregaRef4_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlRef4.Visible = true;
            //this.btnQuitarRef4.Visible = true;
            //this.btnAgregaRef4.Visible = false;
            //this.btnQuitarRef5.Visible = false;
            //this.btnAgregaRef5.Visible = true;
        }

        protected void btnQuitarRef5_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdReferencia = UIConvertNull.Int32(txtIdReferencia5.Text);
            if (IdReferencia != null)
            {
                this.EliminaInfoReferenciaRegistrada(IdReferencia);
            }
            this.pnlRef5.Visible = false;
            //this.btnQuitarRef5.Visible = false;
            //this.btnAgregaRef5.Visible = true;
        }

        protected void btnAgregaRef5_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlRef5.Visible = true;
            //this.btnQuitarRef5.Visible = true;
            //this.btnAgregaRef5.Visible = false;
        }

        //protected void imgBtnBuscaEmailRef1_Click(object sender, ImageClickEventArgs e)
        //{
        //    String EmailRef1 = this.txtEmailRef1.Text;
        //    Int32? NumRef1 = 1;
        //    this.BuscarDatosReferenciaPorEmail(EmailRef1, NumRef1);
        //}

        //protected void imgBtnBuscaEmailRef2_Click(object sender, ImageClickEventArgs e)
        //{
        //    String EmailRef2 = this.txtEmailRef2.Text;
        //    Int32? NumRef2 = 2;
        //    this.BuscarDatosReferenciaPorEmail(EmailRef2, NumRef2);
        //}

        //protected void imgBtnBuscaEmailRef3_Click(object sender, ImageClickEventArgs e)
        //{
        //    String EmailRef3 = this.txtEmailRef3.Text;
        //    Int32? NumRef3 = 3;
        //    this.BuscarDatosReferenciaPorEmail(EmailRef3, NumRef3);
        //}

        //protected void imgBtnBuscaEmailRef4_Click(object sender, ImageClickEventArgs e)
        //{
        //    String EmailRef4 = this.txtEmailRef4.Text;
        //    Int32? NumRef4 = 4;
        //    this.BuscarDatosReferenciaPorEmail(EmailRef4, NumRef4);
        //}

        //protected void imgBtnBuscaEmailRef5_Click(object sender, ImageClickEventArgs e)
        //{
        //    String EmailRef5 = this.txtEmailRef5.Text;
        //    Int32? NumRef5 = 5;
        //    this.BuscarDatosReferenciaPorEmail(EmailRef5, NumRef5);
        //}

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
                    if (PaginaActual == UIConstantes.Formularios.F10)
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

        private void MostrarOcultarBotones(Boolean blnAccion)
        {
            this.pnlRef3.Visible = blnAccion;
            this.pnlRef4.Visible = blnAccion;
            this.pnlRef5.Visible = blnAccion;
            //this.btnQuitarRef3.Visible = blnAccion;
            //this.btnQuitarRef4.Visible = blnAccion;
            //this.btnAgregaRef4.Visible = blnAccion;
            //this.btnQuitarRef5.Visible = blnAccion;
            //this.btnAgregaRef5.Visible = blnAccion;
        }

        private void GuardarDatos()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            GeneralBL oGeneralBL = null;
            DataTable dtPagSigui = null;
            String PaginaActual, PaginaSiguiente = null;
            Int32 intExiste = 0;
            try
            {
                /// **************************************************************************************************************
                /// Validando que el correo del postulante no sea igual al de un Familiar, tampoco de los Familiares sean iguales
                /// **************************************************************************************************************
                if (!String.IsNullOrEmpty(txtEmailRef1.Text))
                {
                    intExiste = this.ValidaEmail(UIConvertNull.Int32(Session["AplicanteId"]), txtEmailRef1.Text);
                    if (intExiste > 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del Referente 1 debe ser diferente al Correo del Aplicante.');", true);
                        this.lblmessage.Text = "El e-mail del Referente 1 debe ser diferente al Correo del Aplicante.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef1.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef2.Text))
                {
                    intExiste = this.ValidaEmail(UIConvertNull.Int32(Session["AplicanteId"]), txtEmailRef2.Text);
                    if (intExiste > 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del Referente 2 debe ser diferente al Correo del Aplicante.');", true);
                        this.lblmessage.Text = "El e-mail del Referente 2 debe ser diferente al Correo del Aplicante.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef2.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef3.Text))
                {
                    intExiste = this.ValidaEmail(UIConvertNull.Int32(Session["AplicanteId"]), txtEmailRef3.Text);
                    if (intExiste > 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del Referente 3 debe ser diferente al Correo del Aplicante.');", true);
                        this.lblmessage.Text = "El e-mail del Referente 3 debe ser diferente al Correo del Aplicante";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef3.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef4.Text))
                {
                    intExiste = this.ValidaEmail(UIConvertNull.Int32(Session["AplicanteId"]), txtEmailRef4.Text);
                    if (intExiste > 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del Referente 4 debe ser diferente al Correo del Aplicante.');", true);
                        this.lblmessage.Text = "El e-mail del Referente 4 debe ser diferente al Correo del Aplicante.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef4.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef5.Text))
                {
                    intExiste = this.ValidaEmail(UIConvertNull.Int32(Session["AplicanteId"]), txtEmailRef5.Text);
                    if (intExiste > 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del Referente 5 debe ser diferente al Correo del Aplicante.');", true);
                        this.lblmessage.Text = "El e-mail del Referente 5 debe ser diferente al Correo del Aplicante.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef5.Focus();
                        return;
                    }
                }

                if (!String.IsNullOrEmpty(txtEmailRef1.Text) && (!String.IsNullOrEmpty(txtEmailRef2.Text)))
                {
                    if (txtEmailRef1.Text == txtEmailRef2.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del referente 1 no pueden ser igual al e-mail del referente 2.');", true);
                        this.lblmessage.Text = "El e-mail del referente 1 no pueden ser igual al e-mail del referente 2.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef2.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef1.Text) && (!String.IsNullOrEmpty(txtEmailRef3.Text)))
                {
                    if (txtEmailRef1.Text == txtEmailRef3.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del referente 1 no pueden ser igual al e-mail del referente 3.');", true);
                        this.lblmessage.Text = "El e-mail del referente 1 no pueden ser igual al e-mail del referente 3.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef3.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef1.Text) && (!String.IsNullOrEmpty(txtEmailRef4.Text)))
                {
                    if (txtEmailRef1.Text == txtEmailRef4.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del referente 1 no pueden ser igual al e-mail del referente 4.');", true);
                        this.lblmessage.Text = "El e-mail del referente 1 no pueden ser igual al e-mail del referente 4.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef4.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef1.Text) && (!String.IsNullOrEmpty(txtEmailRef5.Text)))
                {
                    if (txtEmailRef1.Text == txtEmailRef5.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del referente 1 no pueden ser igual al e-mail del referente 5.');", true);
                        this.lblmessage.Text = "El e-mail del referente 1 no pueden ser igual al e-mail del referente 5.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef5.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef2.Text) && (!String.IsNullOrEmpty(txtEmailRef3.Text)))
                {
                    if (txtEmailRef2.Text == txtEmailRef3.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del referente 2 no pueden ser igual al e-mail del referente 3.');", true);
                        this.lblmessage.Text = "El e-mail del referente 2 no pueden ser igual al e-mail del referente 3.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef3.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef2.Text) && (!String.IsNullOrEmpty(txtEmailRef4.Text)))
                {
                    if (txtEmailRef2.Text == txtEmailRef4.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del referente 2 no pueden ser igual al e-mail del referente 4.');", true);
                        this.lblmessage.Text = "El e-mail del referente 2 no pueden ser igual al e-mail del referente 4.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef4.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef2.Text) && (!String.IsNullOrEmpty(txtEmailRef5.Text)))
                {
                    if (txtEmailRef2.Text == txtEmailRef5.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del referente 2 no pueden ser igual al e-mail del referente 5.');", true);
                        this.lblmessage.Text = "El e-mail del referente 2 no pueden ser igual al e-mail del referente 5.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef5.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef3.Text) && (!String.IsNullOrEmpty(txtEmailRef4.Text)))
                {
                    if (txtEmailRef3.Text == txtEmailRef4.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del referente 3 no pueden ser igual al e-mail del referente 4.');", true);
                        this.lblmessage.Text = "El e-mail del referente 3 no pueden ser igual al e-mail del referente 4.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef4.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef3.Text) && (!String.IsNullOrEmpty(txtEmailRef5.Text)))
                {
                    if (txtEmailRef3.Text == txtEmailRef5.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del referente 3 no pueden ser igual al e-mail del referente 5.');", true);
                        this.lblmessage.Text = "El e-mail del referente 3 no pueden ser igual al e-mail del referente 5.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef5.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRef4.Text) && (!String.IsNullOrEmpty(txtEmailRef5.Text)))
                {
                    if (txtEmailRef4.Text == txtEmailRef5.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('El e-mail del referente 4 no pueden ser igual al e-mail del referente 5.');", true);
                        this.lblmessage.Text = "El e-mail del referente 4 no pueden ser igual al e-mail del referente 5.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRef5.Focus();
                        return;
                    }
                }

                oAplicanteBE = new AplicanteBE();
                oAplicanteBE = this.obtenerDatosReferencias(oAplicanteBE);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormNueve_InfoReferencias(oAplicanteBE);
                if (operacionOK)
                {
                    Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                    oGeneralBL = new GeneralBL();
                    dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    {
                        PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F10)
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alerta de Pagina Web", "alert('" + ex.Message.ToString() + "');", true);
            }
        }

        private AplicanteBE obtenerDatosReferencias(AplicanteBE oAplicanteBE)
        {
            ReferenciaBE Referencia1 = null;
            ReferenciaBE Referencia2 = null;
            ReferenciaBE Referencia3 = null;
            ReferenciaBE Referencia4 = null;
            ReferenciaBE Referencia5 = null;
            try
            {
                if (oAplicanteBE.LReferencia == null)
                {
                    oAplicanteBE.LReferencia = new System.Collections.Generic.List<ReferenciaBE>();
                }
                if (oAplicanteBE.LReferencia == null)
                {
                    oAplicanteBE.LReferencia = new List<ReferenciaBE>();
                }
                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
                //=========================================================================
                // Referencia 1
                //=========================================================================
                Referencia1 = new ReferenciaBE();
                Referencia1.IdReferencia = UIConvertNull.Int32(this.txtIdReferencia1.Text);
                Referencia1.Email = txtEmailRef1.Text;
                Referencia1.Firstname = txtNomRef1.Text;
                Referencia1.LastName = txtApeRef1.Text;
                Referencia1.NombrePersona = txtNomRef1.Text + " " + txtApeRef1.Text;
                Referencia1.NombreOrganizacion = txtInstitucionRef1.Text;
                Referencia1.Cargo = txtCargoRef1.Text;
                Referencia1.Status = UIConstantes.idValorActivo;
                Referencia1.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                oAplicanteBE.LReferencia.Add(Referencia1);

                //=========================================================================
                //Referencia 2
                //=========================================================================
                Referencia2 = new ReferenciaBE();
                Referencia2.IdReferencia = UIConvertNull.Int32(this.txtIdReferencia2.Text);
                Referencia2.Email = txtEmailRef2.Text;
                Referencia2.Firstname = txtNomRef2.Text;
                Referencia2.LastName = txtApeRef2.Text;
                Referencia2.NombrePersona = txtNomRef2.Text + " " + txtApeRef2.Text;
                Referencia2.NombreOrganizacion = txtInstitucionRef2.Text;
                Referencia2.Cargo = txtCargoRef2.Text;
                Referencia2.Status = UIConstantes.idValorActivo;
                Referencia2.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                oAplicanteBE.LReferencia.Add(Referencia2);

                //=========================================================================
                //Referencia 3
                //=========================================================================
                Referencia3 = new ReferenciaBE();
                if (txtEmailRef3.Text != null && txtNomRef3.Text != null && txtApeRef3.Text != null && txtInstitucionRef3.Text != null && txtCargoRef3.Text != null &&
                    txtEmailRef3.Text != String.Empty && txtNomRef3.Text != String.Empty && txtApeRef3.Text != String.Empty && txtInstitucionRef3.Text != String.Empty && txtCargoRef3.Text != String.Empty)
                {
                    Referencia3.IdReferencia = UIConvertNull.Int32(this.txtIdReferencia3.Text);
                    Referencia3.Email = txtEmailRef3.Text;
                    Referencia3.Firstname = txtNomRef3.Text;
                    Referencia3.LastName = txtApeRef3.Text;
                    Referencia3.NombrePersona = txtNomRef3.Text + " " + txtApeRef3.Text;
                    Referencia3.NombreOrganizacion = txtInstitucionRef3.Text;
                    Referencia3.Cargo = txtCargoRef3.Text;
                    Referencia3.Status = UIConstantes.idValorActivo;
                    Referencia3.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                    oAplicanteBE.LReferencia.Add(Referencia3);
                }

                //=========================================================================
                //Referencia 4
                //=========================================================================
                Referencia4 = new ReferenciaBE();
                if (txtEmailRef4.Text != null && txtNomRef4.Text != null && txtApeRef4.Text != null && txtInstitucionRef4.Text != null && txtCargoRef4.Text != null &&
                    txtEmailRef4.Text != String.Empty && txtNomRef4.Text != String.Empty && txtApeRef4.Text != String.Empty && txtInstitucionRef4.Text != String.Empty && txtCargoRef4.Text != String.Empty)
                {
                    Referencia4.IdReferencia = UIConvertNull.Int32(this.txtIdReferencia4.Text);
                    Referencia4.Email = txtEmailRef4.Text;
                    Referencia4.Firstname = txtNomRef4.Text;
                    Referencia4.LastName = txtApeRef4.Text;
                    Referencia4.NombrePersona = txtNomRef4.Text + " " + txtApeRef4.Text;
                    Referencia4.NombreOrganizacion = txtInstitucionRef4.Text;
                    Referencia4.Cargo = txtCargoRef4.Text;
                    Referencia4.Status = UIConstantes.idValorActivo;
                    Referencia4.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                    oAplicanteBE.LReferencia.Add(Referencia4);
                }

                //=========================================================================
                //Referencia 5
                //=========================================================================
                Referencia5 = new ReferenciaBE();
                if (txtEmailRef5.Text != null && txtNomRef5.Text != null && txtApeRef5.Text != null && txtInstitucionRef5.Text != null && txtCargoRef5.Text != null &&
                    txtEmailRef5.Text != String.Empty && txtNomRef5.Text != String.Empty && txtApeRef5.Text != String.Empty && txtInstitucionRef5.Text != String.Empty && txtCargoRef5.Text != String.Empty)
                {
                    Referencia5.IdReferencia = UIConvertNull.Int32(this.txtIdReferencia5.Text);
                    Referencia5.Email = txtEmailRef5.Text;
                    Referencia5.Firstname = txtNomRef5.Text;
                    Referencia5.LastName = txtApeRef5.Text;
                    Referencia5.NombrePersona = txtNomRef5.Text + " " + txtApeRef5.Text;
                    Referencia5.NombreOrganizacion = txtInstitucionRef5.Text;
                    Referencia5.Cargo = txtCargoRef5.Text;
                    Referencia5.Status = UIConstantes.idValorActivo;
                    Referencia5.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                    oAplicanteBE.LReferencia.Add(Referencia5);
                }
                return oAplicanteBE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarInfoReferenciaRegistrada(Int32? AplicanteId)
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ObtenerInfoReferenciasRegistrados(AplicanteId);
                this.LLenarDatosInfoReferenciasRegistrados(oAplicanteBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosInfoReferenciasRegistrados(AplicanteBE oAplicanteBE)
        {
            if (oAplicanteBE != null)
            {
                if (oAplicanteBE.LReferencia != null)
                {
                    for (int indice = 0; indice < oAplicanteBE.LReferencia.Count; indice++)
                    {
                        ReferenciaBE oReferenciaBE = oAplicanteBE.LReferencia[indice];
                        switch (indice)
                        {
                            case 0:
                                this.txtIdReferencia1.Text = UIConvertNull.String(oReferenciaBE.IdReferencia);
                                this.txtEmailRef1.Text = oReferenciaBE.Email;
                                this.txtNomRef1.Text = oReferenciaBE.Firstname;
                                this.txtApeRef1.Text = oReferenciaBE.LastName;
                                this.txtInstitucionRef1.Text = oReferenciaBE.NombreOrganizacion;
                                this.txtCargoRef1.Text = oReferenciaBE.Cargo;
                                break;
                            case 1:
                                this.txtIdReferencia2.Text = UIConvertNull.String(oReferenciaBE.IdReferencia);
                                this.txtEmailRef2.Text = oReferenciaBE.Email;
                                this.txtNomRef2.Text = oReferenciaBE.Firstname;
                                this.txtApeRef2.Text = oReferenciaBE.LastName;
                                this.txtInstitucionRef2.Text = oReferenciaBE.NombreOrganizacion;
                                this.txtCargoRef2.Text = oReferenciaBE.Cargo;
                                //this.btnQuitarRef3.Visible = false;
                                //this.btnAgregaRef3.Visible = true;
                                break;
                            case 2:
                                this.txtIdReferencia3.Text = UIConvertNull.String(oReferenciaBE.IdReferencia);
                                this.txtEmailRef3.Text = oReferenciaBE.Email;
                                this.txtNomRef3.Text = oReferenciaBE.Firstname;
                                this.txtApeRef3.Text = oReferenciaBE.LastName;
                                this.txtInstitucionRef3.Text = oReferenciaBE.NombreOrganizacion;
                                this.txtCargoRef3.Text = oReferenciaBE.Cargo;
                                this.pnlRef3.Visible = true;
                                //this.btnQuitarRef3.Visible = true;
                                //this.btnAgregaRef3.Visible = false;
                                //this.btnQuitarRef4.Visible = false;
                                break;
                            case 3:
                                this.txtIdReferencia4.Text = UIConvertNull.String(oReferenciaBE.IdReferencia);
                                this.txtEmailRef4.Text = oReferenciaBE.Email;
                                this.txtNomRef4.Text = oReferenciaBE.Firstname;
                                this.txtApeRef4.Text = oReferenciaBE.LastName;
                                this.txtInstitucionRef4.Text = oReferenciaBE.NombreOrganizacion;
                                this.txtCargoRef4.Text = oReferenciaBE.Cargo;
                                //this.btnQuitarRef4.Visible = true;
                                //this.btnAgregaRef5.Visible = false;
                                this.pnlRef4.Visible = true;
                                break;
                            case 4:
                                this.txtIdReferencia5.Text = UIConvertNull.String(oReferenciaBE.IdReferencia);
                                this.txtEmailRef5.Text = oReferenciaBE.Email;
                                this.txtNomRef5.Text = oReferenciaBE.Firstname;
                                this.txtApeRef5.Text = oReferenciaBE.LastName;
                                this.txtInstitucionRef5.Text = oReferenciaBE.NombreOrganizacion;
                                this.txtCargoRef5.Text = oReferenciaBE.Cargo;
                                this.pnlRef5.Visible = true;
                                //this.btnQuitarRef5.Visible = true;
                                //this.btnAgregaRef5.Visible = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private Int32 ValidaEmail(Int32? AplicanteId, String strEmail)
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                return oAplicanteBL.ConsultaEmailRegistrado(AplicanteId, strEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void BuscarDatosReferenciaPorEmail(String strEmail, Int32? NumRefe)
        //{
        //    ReferenciaBE oReferenciaBE = null;
        //    AplicanteBL oAplicanteBL = null;
        //    try
        //    {
        //        oAplicanteBL = new AplicanteBL();
        //        oReferenciaBE = oAplicanteBL.ListarDatosReferenciaPorEmail(strEmail, NumRefe);
        //        if (oReferenciaBE != null)
        //        {
        //            this.LLenarDatosReferenciaPorEmail(oReferenciaBE);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void LLenarDatosReferenciaPorEmail(ReferenciaBE oReferenciaBE)
        {
            Int32? indice = UIConvertNull.Int32(oReferenciaBE.NumReferencia);
            if (indice != null)
            {
                switch (indice)
                {
                    case 1:
                        this.txtEmailRef1.Text = oReferenciaBE.Email;
                        this.txtNomRef1.Text = oReferenciaBE.Firstname;
                        this.txtApeRef1.Text = oReferenciaBE.LastName;
                        this.txtInstitucionRef1.Text = oReferenciaBE.NombreOrganizacion;
                        this.txtCargoRef1.Text = oReferenciaBE.Cargo;
                        break;
                    case 2:
                        this.txtEmailRef2.Text = oReferenciaBE.Email;
                        this.txtNomRef2.Text = oReferenciaBE.Firstname;
                        this.txtApeRef2.Text = oReferenciaBE.LastName;
                        this.txtInstitucionRef2.Text = oReferenciaBE.NombreOrganizacion;
                        this.txtCargoRef2.Text = oReferenciaBE.Cargo;
                        break;
                    case 3:
                        this.txtEmailRef3.Text = oReferenciaBE.Email;
                        this.txtNomRef3.Text = oReferenciaBE.Firstname;
                        this.txtApeRef3.Text = oReferenciaBE.LastName;
                        this.txtInstitucionRef3.Text = oReferenciaBE.NombreOrganizacion;
                        this.txtCargoRef3.Text = oReferenciaBE.Cargo;
                        this.pnlRef3.Visible = true;
                        break;
                    case 4:
                        this.txtEmailRef4.Text = oReferenciaBE.Email;
                        this.txtNomRef4.Text = oReferenciaBE.Firstname;
                        this.txtApeRef4.Text = oReferenciaBE.LastName;
                        this.txtInstitucionRef4.Text = oReferenciaBE.NombreOrganizacion;
                        this.txtCargoRef4.Text = oReferenciaBE.Cargo;
                        //this.btnQuitarRef4.Visible = true;
                        this.pnlRef4.Visible = true;
                        break;
                    case 5:
                        this.txtEmailRef5.Text = oReferenciaBE.Email;
                        this.txtNomRef5.Text = oReferenciaBE.Firstname;
                        this.txtApeRef5.Text = oReferenciaBE.LastName;
                        this.txtInstitucionRef5.Text = oReferenciaBE.NombreOrganizacion;
                        this.txtCargoRef5.Text = oReferenciaBE.Cargo;
                        this.pnlRef5.Visible = true;
                       // this.btnQuitarRef5.Visible = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private Boolean EliminaInfoReferenciaRegistrada(Int32? IdReferencia)
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            AplicanteBL oAplicanteBL = null;
            Boolean Resultado = false;
            try
            {
                oAplicanteBL = new AplicanteBL();
                Resultado = oAplicanteBL.EliminaInfoReferenciaRegistrada(IdReferencia, AplicanteId);
                if (Resultado == true)
                {
                    this.LimpiarControles();
                    this.MostrarOcultarBotones(false);
                    this.CargarInfoReferenciaRegistrada(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
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
            this.txtEmailRef1.Text = UIConstantes._valorCadenaVacia;
            this.txtNomRef1.Text = UIConstantes._valorCadenaVacia;
            this.txtApeRef1.Text = UIConstantes._valorCadenaVacia;
            this.txtInstitucionRef1.Text = UIConstantes._valorCadenaVacia;
            this.txtCargoRef1.Text = UIConstantes._valorCadenaVacia;
            this.txtIdReferencia1.Text = UIConstantes._valorCadenaVacia;

            this.txtEmailRef2.Text = UIConstantes._valorCadenaVacia;
            this.txtNomRef2.Text = UIConstantes._valorCadenaVacia;
            this.txtApeRef2.Text = UIConstantes._valorCadenaVacia;
            this.txtInstitucionRef2.Text = UIConstantes._valorCadenaVacia;
            this.txtCargoRef2.Text = UIConstantes._valorCadenaVacia;
            this.txtIdReferencia2.Text = UIConstantes._valorCadenaVacia;

            this.txtEmailRef3.Text = UIConstantes._valorCadenaVacia;
            this.txtNomRef3.Text = UIConstantes._valorCadenaVacia;
            this.txtApeRef3.Text = UIConstantes._valorCadenaVacia;
            this.txtInstitucionRef3.Text = UIConstantes._valorCadenaVacia;
            this.txtCargoRef3.Text = UIConstantes._valorCadenaVacia;
            this.txtIdReferencia3.Text = UIConstantes._valorCadenaVacia;

            this.txtEmailRef4.Text = UIConstantes._valorCadenaVacia;
            this.txtNomRef4.Text = UIConstantes._valorCadenaVacia;
            this.txtApeRef4.Text = UIConstantes._valorCadenaVacia;
            this.txtInstitucionRef4.Text = UIConstantes._valorCadenaVacia;
            this.txtCargoRef4.Text = UIConstantes._valorCadenaVacia;
            this.txtIdReferencia4.Text = UIConstantes._valorCadenaVacia;

            this.txtEmailRef5.Text = UIConstantes._valorCadenaVacia;
            this.txtNomRef5.Text = UIConstantes._valorCadenaVacia;
            this.txtApeRef5.Text = UIConstantes._valorCadenaVacia;
            this.txtInstitucionRef5.Text = UIConstantes._valorCadenaVacia;
            this.txtCargoRef5.Text = UIConstantes._valorCadenaVacia;
            this.txtIdReferencia5.Text = UIConstantes._valorCadenaVacia;
        }

        #endregion "Métodos Privados"
    }
}
