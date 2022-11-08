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
    public partial class frm09_InfoPadres : BasePage
    {
        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            /*Solo para pruebas*/
            //Session["usrRedId"] = "U.Admision";
            //Session["ModPostulacion"] = 49;
            //Session["AplicanteId"] = 111689;
            UIHelper.SessionActiva(Page);
            try
            {
        

                if (!IsPostBack)
                {
                    //this.CargarTitulos();
                    this.LimpiarControles();
                    this.MostrarOcultarBotones(false);
                    this.CargarCombos();
                    this.HabilitaControles();
                    this.CargarInfRelFamRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                    HabilitarControlesMayusculas(); /*Se agrega: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F09, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
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
                    if (PaginaActual == UIConstantes.Formularios.F09)
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
                throw ex;
                //UIHelper.EnviarCorreo(UIConstantes.Formularios.F09, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                //Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void BtnAgregaPariente2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlRelFam2.Visible = true;
            this.BtnOcultaPariente2.Visible = true;
            this.BtnAgregaPariente2.Visible = false;
            this.BtnOcultaPariente3.Visible = false;
            this.BtnAgregaPariente3.Visible = true;
        }

        protected void BtnOcultaPariente2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdRelacion = UIConvertNull.Int32(txtApplicationRelationshipId2.Text);
            if (IdRelacion != null)
            {
                this.EliminaInfoPadresRegistrada(IdRelacion);
            }
            this.pnlRelFam2.Visible = false;
            this.pnlRelFam3.Visible = false;
            this.BtnOcultaPariente2.Visible = false;
            this.BtnAgregaPariente2.Visible = true;
            this.BtnOcultaPariente3.Visible = false;
            this.BtnAgregaPariente3.Visible = false;
        }

        protected void BtnOcultaPariente3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdRelacion = UIConvertNull.Int32(txtApplicationRelationshipId3.Text);
            if (IdRelacion != null)
            {
                this.EliminaInfoPadresRegistrada(IdRelacion);
            }
            this.pnlRelFam3.Visible = false;
            this.BtnOcultaPariente3.Visible = false;
            this.BtnAgregaPariente3.Visible = true;
        }

        protected void BtnAgregaPariente3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlRelFam3.Visible = true;
            this.BtnOcultaPariente3.Visible = true;
            this.BtnAgregaPariente3.Visible = false;
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
                    if (PaginaActual == UIConstantes.Formularios.F09)
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
            this.CargarComboTipoDocumento();
            this.CargarComboRelFam();
        }

        private void CargarComboTipoDocumento()
        {
            GeneralBL oGeneralBL = null;
            //DataTable dtDocumentos = null;
            //dtDocumentos = UIHelper.Tabla_DocIdentidad();
            oGeneralBL = new GeneralBL();
            DataTable dtDocumentos = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPO_DOCUMENTO].Key, "", null);
            if (dtDocumentos != null && dtDocumentos.Rows.Count > 0)
            {
                Funciones.cargarComboYSeleccione(this.ddlTipDocRelFam1, dtDocumentos.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(this.ddlTipDocRelFam2, dtDocumentos.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(this.ddlTipDocRelFam3, dtDocumentos.Copy(), "descripcion", "codigo", "-- Seleccionar --");
            }
        }

        private void CargarComboRelFam()
        {
            DataTable dtRelacionFamiliar = null;
            dtRelacionFamiliar = UIHelper.Tabla_RelacionFamiliar();
            if (dtRelacionFamiliar != null && dtRelacionFamiliar.Rows.Count > 0)
            {
                Funciones.cargarComboYSeleccione(this.ddlRelFam1, dtRelacionFamiliar.Copy(), "Nombre", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(this.ddlRelFam2, dtRelacionFamiliar.Copy(), "Nombre", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(this.ddlRelFam3, dtRelacionFamiliar.Copy(), "Nombre", "IdCodigo", "-- Seleccionar --");
            }
        }

        private void MostrarOcultarBotones(Boolean blnAccion)
        {
            this.pnlRelFam2.Visible = blnAccion;
            this.pnlRelFam3.Visible = blnAccion;
            this.BtnOcultaPariente2.Visible = blnAccion;
            this.BtnAgregaPariente3.Visible = blnAccion;
            this.BtnOcultaPariente3.Visible = blnAccion;
        }

        private Int32 ValidaDNI(Int32? AplicanteId, String NroDNI)
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                return oAplicanteBL.ConsultaDNIRegistrado(AplicanteId, NroDNI);
            }
            catch (Exception ex)
            {
                throw ex;
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
                /// ***********************************************************************************************
                /// Validando que la relación Familiar no se repita
                /// ***********************************************************************************************
                if (ddlRelFam1.SelectedValue == ddlRelFam2.SelectedValue)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Inconvenientes en datos ingresados", "alert('El parentesco del familiar 1 debe ser diferente al familiar 2.');", true);
                    this.lblmessage.Text = "El parentesco del familiar 1 debe ser diferente al familiar 2.";
                    this.mpeMostrarError.Show();
                    this.ddlRelFam2.Focus();
                    return;
                }
                if (ddlRelFam1.SelectedValue != "0" || ddlRelFam3.SelectedValue != "0")
                {
                    if (ddlRelFam1.SelectedValue == ddlRelFam3.SelectedValue)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Inconvenientes en datos ingresados", "alert('El parentesco del familiar 1 debe ser diferente al familiar 3.');", true);
                        this.lblmessage.Text = "El parentesco del familiar 1 debe ser diferente al familiar 3.";
                        this.mpeMostrarError.Show();
                        this.ddlRelFam3.Focus();
                        return;
                    }
                }
                if (ddlRelFam2.SelectedValue != "0" || ddlRelFam3.SelectedValue != "0")
                {
                    if (ddlRelFam2.SelectedValue == ddlRelFam3.SelectedValue)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Inconvenientes en datos ingresados", "alert('El parentesco del familiar 2 debe ser diferente al familiar 3.');", true);
                        this.lblmessage.Text = "El parentesco del familiar 2 debe ser diferente al familiar 3.";
                        this.mpeMostrarError.Show();
                        this.ddlRelFam3.Focus();
                        return;
                    }
                }

                /// ***********************************************************************************************
                /// Validando DNI de Postulante no sea igual del Familiar; tampoco de los Familiares sean iguales
                /// ***********************************************************************************************
                if (!String.IsNullOrEmpty(txtNumDocRelFam1.Text))
                {
                    intExiste = this.ValidaDNI(UIConvertNull.Int32(Session["AplicanteId"]), txtNumDocRelFam1.Text);
                    if (intExiste > 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Inconvenientes en datos ingresados", "alert('Los documentos de identidad de los familiares deben ser diferentes al del postulante.');", true);
                        this.lblmessage.Text = "Los documentos de identidad de los familiares deben ser diferentes al del postulante.";
                        this.mpeMostrarError.Show();
                        txtNumDocRelFam1.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtNumDocRelFam2.Text))
                {
                    intExiste = this.ValidaDNI(UIConvertNull.Int32(Session["AplicanteId"]), txtNumDocRelFam2.Text);
                    if (intExiste > 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Inconvenientes en datos ingresados", "alert('Los documentos de identidad de los familiares deben ser diferentes al del postulante.');", true);
                        this.lblmessage.Text = "Los documentos de identidad de los familiares deben ser diferentes al del postulante.";
                        this.mpeMostrarError.Show();
                        txtNumDocRelFam2.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtNumDocRelFam3.Text))
                {
                    intExiste = this.ValidaDNI(UIConvertNull.Int32(Session["AplicanteId"]), txtNumDocRelFam3.Text);
                    if (intExiste > 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Inconvenientes en datos ingresados", "alert('Los documentos de identidad de los familiares deben ser diferentes al del postulante.');", true);
                        this.lblmessage.Text = "Los documentos de identidad de los familiares deben ser diferentes al del postulante.";
                        this.mpeMostrarError.Show();
                        txtNumDocRelFam3.Focus();
                        return;
                    }
                }

                if (!String.IsNullOrEmpty(txtNumDocRelFam1.Text) && (!String.IsNullOrEmpty(txtNumDocRelFam2.Text)))
                {
                    if (txtNumDocRelFam1.Text == txtNumDocRelFam2.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Inconvenientes en datos ingresados", "alert('Los documentos de identidad de los familiares deben ser diferentes.');", true);
                        this.lblmessage.Text = "Los documentos de identidad de los familiares deben ser diferentes.";
                        this.mpeMostrarError.Show();
                        this.txtNumDocRelFam2.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtNumDocRelFam1.Text) && (!String.IsNullOrEmpty(txtNumDocRelFam3.Text)))
                {
                    if (txtNumDocRelFam1.Text == txtNumDocRelFam3.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Inconvenientes en datos ingresados", "alert('Los documentos de identidad de los familiares deben ser diferentes.');", true);
                        this.lblmessage.Text = "Los documentos de identidad de los familiares deben ser diferentes.";
                        this.mpeMostrarError.Show();
                        this.txtNumDocRelFam3.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtNumDocRelFam2.Text) && (!String.IsNullOrEmpty(txtNumDocRelFam3.Text)))
                {
                    if (txtNumDocRelFam2.Text == txtNumDocRelFam3.Text)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Inconvenientes en datos ingresados", "alert('Los documentos de identidad de los familiares deben ser diferentes.');", true);
                        this.lblmessage.Text = "Los documentos de identidad de los familiares deben ser diferentes.";
                        this.mpeMostrarError.Show();
                        this.txtNumDocRelFam3.Focus();
                        return;
                    }
                }

                /// **************************************************************************************************************
                /// Validando que el correo del postulante no sea igual al de un Familiar, tampoco de los Familiares sean iguales
                /// **************************************************************************************************************
                if (!String.IsNullOrEmpty(txtEmailRelFam1.Text))
                {
                    intExiste = this.ValidaEmail(UIConvertNull.Int32(Session["AplicanteId"]), txtEmailRelFam1.Text);
                    if (intExiste > 0)
                    {
                        this.lblmessage.Text = "Los correos de los familiares deben ser diferentes al del postulante.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRelFam1.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRelFam2.Text))
                {
                    intExiste = this.ValidaEmail(UIConvertNull.Int32(Session["AplicanteId"]), txtEmailRelFam2.Text);
                    if (intExiste > 0)
                    {
                        this.lblmessage.Text = "Los correos de los familiares deben ser diferentes al del postulante.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRelFam2.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRelFam3.Text))
                {
                    intExiste = this.ValidaEmail(UIConvertNull.Int32(Session["AplicanteId"]), txtEmailRelFam3.Text);
                    if (intExiste > 0)
                    {
                        this.lblmessage.Text = "Los correos de los familiares deben ser diferentes al del postulante.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRelFam3.Focus();
                        return;
                    }
                }

                if (!String.IsNullOrEmpty(txtEmailRelFam1.Text) && (!String.IsNullOrEmpty(txtEmailRelFam2.Text)))
                {
                    if (txtEmailRelFam1.Text == txtEmailRelFam2.Text)
                    {
                        this.lblmessage.Text = "Los correos de los familiares deben ser diferentes.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRelFam2.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRelFam1.Text) && (!String.IsNullOrEmpty(txtEmailRelFam3.Text)))
                {
                    if (txtEmailRelFam1.Text == txtEmailRelFam3.Text)
                    {
                        this.lblmessage.Text = "Los correos de los familiares deben ser diferentes.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRelFam3.Focus();
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txtEmailRelFam2.Text) && (!String.IsNullOrEmpty(txtEmailRelFam3.Text)))
                {
                    if (txtEmailRelFam2.Text == txtEmailRelFam3.Text)
                    {
                        this.lblmessage.Text = "Los correos de los familiares deben ser diferentes.";
                        this.mpeMostrarError.Show();
                        this.txtEmailRelFam3.Focus();
                        return;
                    }
                }

                oAplicanteBE = new AplicanteBE();
                oAplicanteBE = this.obtenerDatosDeRelFamiliar(oAplicanteBE);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormOcho_InfoPadres(oAplicanteBE);
                if (operacionOK)
                {
                    Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                    oGeneralBL = new GeneralBL();
                    dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    {
                        PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F09)
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
                throw ex;
            }
        }

        private AplicanteBE obtenerDatosDeRelFamiliar(AplicanteBE oAplicanteBE)
        {
            RelacionBE Relacion1 = null;
            RelacionBE Relacion2 = null;
            RelacionBE Relacion3 = null;
            try
            {
                if (oAplicanteBE.LRelacion == null)
                {
                    oAplicanteBE.LRelacion = new System.Collections.Generic.List<RelacionBE>();
                }
                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);

                //=========================================================================
                // DATOS DE RelFam1
                //=========================================================================
                Relacion1 = new RelacionBE();
                Relacion1.IdTipoRelacion = UIConvertNull.Int32(ddlRelFam1.SelectedValue);
                if (this.txtApplicationRelationshipId1.Text != null && txtApplicationRelationshipId1.Text != String.Empty)
                {
                    Relacion1.IdApplicationRelationship = UIConvertNull.Int32(txtApplicationRelationshipId1.Text);
                }
                else
                {
                    Relacion1.IdApplicationRelationship = null;
                }
                if (this.txtNomRelFam1.Text != null)
                {
                    Relacion1.Nombres = this.txtNomRelFam1.Text.Replace("'", "''");
                }
                if (this.txtApeRelFam1.Text != null)
                {
                    Relacion1.Apellido = this.txtApeRelFam1.Text.Replace("'", "''");
                }
                if (ddlTipDocRelFam1.SelectedValue != "0")
                {
                    Relacion1.TipoDocumento = ddlTipDocRelFam1.SelectedValue;
                }
                if (this.txtNumDocRelFam1.Text != null)
                {
                    Relacion1.Documento = this.txtNumDocRelFam1.Text.Replace("'", "''");
                }
                if (this.txtEmailRelFam1.Text != null)
                {
                    Relacion1.CorreoPersonal = this.txtEmailRelFam1.Text.Replace("'", "''");
                }
                if (this.txtTelefonoRelFam1.Text != null)
                {
                    Relacion1.NumeroTelefono = this.txtTelefonoRelFam1.Text;
                }
                //if (this.chkRelFam1Fallecido.Checked == true)
                //{
                //    Relacion1.Fallecido = UIConstantes.idValorActivo; /// 1: Si; 
                //}
                //else
                //{
                //    Relacion1.Fallecido = UIConstantes.idValorNulo; ///0: No;
                //}
                Relacion1.AsistenciaInstitucion = UIConstantes.idValorNulo;
                Relacion1.Compartir = UIConstantes.idValorNulo;
                Relacion1.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);

                if (oAplicanteBE.LRelacion == null)
                {
                    oAplicanteBE.LRelacion = new List<RelacionBE>();
                }
                oAplicanteBE.LRelacion.Add(Relacion1);

                //=========================================================================
                //RELACION 2
                //=========================================================================
                Relacion2 = new RelacionBE();
                if (txtNomRelFam2.Text != null && txtNomRelFam2.Text != String.Empty && txtApeRelFam2.Text != null && txtApeRelFam2.Text != String.Empty &&
                    txtNumDocRelFam2.Text != null && txtNumDocRelFam2.Text != String.Empty && txtTelefonoRelFam2.Text != null && txtTelefonoRelFam2.Text != String.Empty)
                {
                    Relacion2.IdTipoRelacion = UIConvertNull.Int32(ddlRelFam2.SelectedValue);
                    if (this.txtApplicationRelationshipId2.Text != null && txtApplicationRelationshipId2.Text != String.Empty)
                    {
                        Relacion2.IdApplicationRelationship = UIConvertNull.Int32(txtApplicationRelationshipId2.Text);
                    }
                    else
                    {
                        Relacion2.IdApplicationRelationship = null;
                    }
                    if (this.txtNomRelFam2.Text != null)
                    {
                        Relacion2.Nombres = this.txtNomRelFam2.Text.Replace("'", "''");
                    }
                    if (this.txtApeRelFam2.Text != null)
                    {
                        Relacion2.Apellido = this.txtApeRelFam2.Text.Replace("'", "''");
                    }
                    if (ddlTipDocRelFam2.SelectedValue != "0")
                    {
                        Relacion2.TipoDocumento = ddlTipDocRelFam2.SelectedValue;
                    }
                    if (this.txtNumDocRelFam2.Text != null)
                    {
                        Relacion2.Documento = this.txtNumDocRelFam2.Text.Replace("'", "''");
                    }
                    if (this.txtEmailRelFam2.Text != null)
                    {
                        Relacion2.CorreoPersonal = this.txtEmailRelFam2.Text.Replace("'", "''");
                    }
                    if (this.txtTelefonoRelFam2.Text != null)
                    {
                        Relacion2.NumeroTelefono = this.txtTelefonoRelFam2.Text;
                    }
                    //if (this.chkRelFam2Fallecido.Checked == true)
                    //{
                    //    Relacion2.Fallecido = UIConstantes.idValorActivo; /// 1: Si; 
                    //}
                    //else
                    //{
                    //    Relacion2.Fallecido = UIConstantes.idValorNulo; ///0: No;
                    //}
                    Relacion2.AsistenciaInstitucion = UIConstantes.idValorNulo;
                    Relacion2.Compartir = UIConstantes.idValorNulo;
                    Relacion2.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);

                    if (oAplicanteBE.LRelacion == null)
                    {
                        oAplicanteBE.LRelacion = new List<RelacionBE>();
                    }
                    oAplicanteBE.LRelacion.Add(Relacion2);
                }

                //=========================================================================
                //RELACION 3
                //=========================================================================
                Relacion3 = new RelacionBE();
                if (txtNomRelFam3.Text != null && txtNomRelFam3.Text != String.Empty && txtApeRelFam3.Text != null && txtApeRelFam3.Text != String.Empty &&
                    txtNumDocRelFam3.Text != null && txtNumDocRelFam3.Text != String.Empty && txtTelefonoRelFam3.Text != null && txtTelefonoRelFam3.Text != String.Empty)
                {
                    Relacion3.IdTipoRelacion = UIConvertNull.Int32(ddlRelFam3.SelectedValue);
                    if (this.txtApplicationRelationshipId3.Text != null && txtApplicationRelationshipId3.Text != String.Empty)
                    {
                        Relacion3.IdApplicationRelationship = UIConvertNull.Int32(txtApplicationRelationshipId3.Text);
                    }
                    else
                    {
                        Relacion3.IdApplicationRelationship = null;
                    }
                    if (this.txtNomRelFam3.Text != null)
                    {
                        Relacion3.Nombres = this.txtNomRelFam3.Text.Replace("'", "''");
                    }
                    if (this.txtApeRelFam3.Text != null)
                    {
                        Relacion3.Apellido = this.txtApeRelFam3.Text.Replace("'", "''");
                    }
                    if (ddlTipDocRelFam3.SelectedValue != "0")
                    {
                        Relacion3.TipoDocumento = ddlTipDocRelFam3.SelectedValue;
                    }
                    if (this.txtNumDocRelFam3.Text != null)
                    {
                        Relacion3.Documento = this.txtNumDocRelFam3.Text.Replace("'", "''");
                    }
                    if (this.txtEmailRelFam3.Text != null)
                    {
                        Relacion3.CorreoPersonal = this.txtEmailRelFam3.Text.Replace("'", "''");
                    }
                    if (this.txtTelefonoRelFam3.Text != null)
                    {
                        Relacion3.NumeroTelefono = this.txtTelefonoRelFam3.Text;
                    }
                    Relacion3.Fallecido = UIConstantes.idValorNulo; ///0: No;
                    Relacion3.AsistenciaInstitucion = UIConstantes.idValorNulo;
                    Relacion3.Compartir = UIConstantes.idValorNulo;
                    Relacion3.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);

                    if (oAplicanteBE.LRelacion == null)
                    {
                        oAplicanteBE.LRelacion = new List<RelacionBE>();
                    }
                    oAplicanteBE.LRelacion.Add(Relacion3);
                }
                return oAplicanteBE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarInfRelFamRegistrados(Int32? AplicanteId)
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ObtenerInfoPadresRegistrado(AplicanteId);
                this.LLenarDatosInfoRelFam1sRegistrados(oAplicanteBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosInfoRelFam1sRegistrados(AplicanteBE oAplicanteBE)
        {
            if (oAplicanteBE != null)
            {
                if (oAplicanteBE.LRelacion != null)
                {
                    for (int indice = 0; indice < oAplicanteBE.LRelacion.Count; indice++)
                    {
                        RelacionBE oRelacionBE = oAplicanteBE.LRelacion[indice];
                        switch (indice)
                        {
                            case 0:
                                this.txtApplicationRelationshipId1.Text = UIConvertNull.String(oRelacionBE.IdApplicationRelationship);
                                this.ddlRelFam1.SelectedValue = UIConvertNull.String(oRelacionBE.IdTipoRelacion);
                                this.txtNomRelFam1.Text = oRelacionBE.Nombres;
                                this.txtApeRelFam1.Text = oRelacionBE.Apellido;
                                this.ddlTipDocRelFam1.SelectedValue = oRelacionBE.TipoDocumento;
                                this.txtNumDocRelFam1.Text = oRelacionBE.Documento;
                                this.txtEmailRelFam1.Text = oRelacionBE.CorreoPersonal;
                                this.txtTelefonoRelFam1.Text = oRelacionBE.NumeroTelefono;
                                //if (oRelacionBE.Fallecido == 1)
                                //{
                                //    chkRelFam1Fallecido.Checked = true;
                                //}
                                //else
                                //{
                                //    chkRelFam1Fallecido.Checked = false;
                                //}
                                this.BtnAgregaPariente2.Visible = true;
                                break;
                            case 1:
                                this.txtApplicationRelationshipId2.Text = UIConvertNull.String(oRelacionBE.IdApplicationRelationship);
                                this.ddlRelFam2.SelectedValue = UIConvertNull.String(oRelacionBE.IdTipoRelacion);
                                this.txtNomRelFam2.Text = oRelacionBE.Nombres;
                                this.txtApeRelFam2.Text = oRelacionBE.Apellido;
                                this.ddlTipDocRelFam2.SelectedValue = oRelacionBE.TipoDocumento;
                                this.txtNumDocRelFam2.Text = oRelacionBE.Documento;
                                this.txtEmailRelFam2.Text = oRelacionBE.CorreoPersonal;
                                this.txtTelefonoRelFam2.Text = oRelacionBE.NumeroTelefono;
                                //if (oRelacionBE.Fallecido == 1)
                                //{
                                //    chkRelFam2Fallecido.Checked = true;
                                //}
                                //else
                                //{
                                //    chkRelFam2Fallecido.Checked = false;
                                //}
                                this.pnlRelFam2.Visible = true;
                                this.BtnAgregaPariente2.Visible = false;
                                this.BtnOcultaPariente2.Visible = true;
                                this.BtnAgregaPariente3.Visible = true;
                                this.BtnOcultaPariente3.Visible = false;
                                break;
                            case 2:
                                this.txtApplicationRelationshipId3.Text = UIConvertNull.String(oRelacionBE.IdApplicationRelationship);
                                this.ddlRelFam3.SelectedValue = UIConvertNull.String(oRelacionBE.IdTipoRelacion);
                                this.txtNomRelFam3.Text = oRelacionBE.Nombres;
                                this.txtApeRelFam3.Text = oRelacionBE.Apellido;
                                this.ddlTipDocRelFam3.SelectedValue = oRelacionBE.TipoDocumento;
                                this.txtNumDocRelFam3.Text = oRelacionBE.Documento;
                                this.txtEmailRelFam3.Text = oRelacionBE.CorreoPersonal;
                                this.txtTelefonoRelFam3.Text = oRelacionBE.NumeroTelefono;
                                this.pnlRelFam3.Visible = true;
                                this.BtnAgregaPariente2.Visible = false;
                                this.BtnOcultaPariente2.Visible = true;
                                this.BtnAgregaPariente3.Visible = false;
                                this.BtnOcultaPariente3.Visible = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private Boolean EliminaInfoPadresRegistrada(Int32? IdRelacionFam)
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            AplicanteBL oAplicanteBL = null;
            Boolean Resultado = false;
            try
            {
                oAplicanteBL = new AplicanteBL();
                Resultado = oAplicanteBL.EliminaInfoPadresRegistrada(IdRelacionFam, AplicanteId);
                if (Resultado == true)
                {
                    this.LimpiarControles();
                    this.MostrarOcultarBotones(false);
                    this.CargarInfRelFamRegistrados(AplicanteId);
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
            this.ddlRelFam1.ClearSelection();
            this.ddlTipDocRelFam1.ClearSelection();
            //this.chkRelFam1Fallecido.Checked = false;
            this.txtNomRelFam1.Text = UIConstantes._valorCadenaVacia;
            this.txtApeRelFam1.Text = UIConstantes._valorCadenaVacia;
            this.txtNumDocRelFam1.Text = UIConstantes._valorCadenaVacia;
            this.txtEmailRelFam1.Text = UIConstantes._valorCadenaVacia;
            this.txtTelefonoRelFam1.Text = UIConstantes._valorCadenaVacia;
            this.txtApplicationRelationshipId1.Text = UIConstantes._valorCadenaVacia;

            this.ddlRelFam2.ClearSelection();
            this.ddlTipDocRelFam2.ClearSelection();
            //this.chkRelFam2Fallecido.Checked = false;
            this.txtNomRelFam2.Text = UIConstantes._valorCadenaVacia;
            this.txtApeRelFam2.Text = UIConstantes._valorCadenaVacia;
            this.txtNumDocRelFam2.Text = UIConstantes._valorCadenaVacia;
            this.txtEmailRelFam2.Text = UIConstantes._valorCadenaVacia;
            this.txtTelefonoRelFam2.Text = UIConstantes._valorCadenaVacia;
            this.txtApplicationRelationshipId2.Text = UIConstantes._valorCadenaVacia;

            this.ddlRelFam3.ClearSelection();
            this.ddlTipDocRelFam3.ClearSelection();
            this.txtNomRelFam3.Text = UIConstantes._valorCadenaVacia;
            this.txtApeRelFam3.Text = UIConstantes._valorCadenaVacia;
            this.txtNumDocRelFam3.Text = UIConstantes._valorCadenaVacia;
            this.txtEmailRelFam3.Text = UIConstantes._valorCadenaVacia;
            this.txtTelefonoRelFam3.Text = UIConstantes._valorCadenaVacia;
            this.txtApplicationRelationshipId3.Text = UIConstantes._valorCadenaVacia;
        }

        /*Ini: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
        private void HabilitarControlesMayusculas()
        {
            foreach (Control c in Page.Controls)
            {
                foreach (Control c2 in c.Controls)
                {
                    if (c2 is TextBox)
                    {
                        TextBox txt = c2 as TextBox;
                        if (txt.Text != "") txt.Text = txt.Text.ToUpper();
                    }
                }

            }
        }
        /*Fin: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
        #endregion "Métodos Privados"
    }
}
