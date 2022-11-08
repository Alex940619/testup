using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net.Mail;
using System.Configuration;
using System.Data;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm04_DatoPersonal : BasePage
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
                    this.LlenarScripts();
                    this.HabilitaControles(false);
                    this.CargarCombos();

                    if (Session["ModPostulacion"] != null && Session["AplicanteId"] != null)
                    {
                        this.obtenerDatosPersonalesPorAplicanteId(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                        /*Ini: Christian Ramirez - GIIT [Caso 47019] - 20180627*/
                        if (UIConvertNull.Int32(Session["ModPostulacion"]) == UIConstantes.Modalidad.TallerEPU) trUbigeo.Visible = false;
                        /*Fin: Christian Ramirez - GIIT [Caso 47019] - 20180627*/
                    }

                    HabilitarControlesMayusculas(); /*Se agrega: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F04, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
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

        protected void imgBtnNext_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                
                if (UIConvertNull.Int32(Session["ModPostulacion"]) == UIConstantes.Modalidad.TallerEPU) this.GuardarDatos(); /*Se agrega: Christian Ramirez - GIIT[Caso 48793] - 20180731*/
                /*Ini:: Christian Ramirez - GIIT [Caso 48662] - 20180730*/
                else
                {
                    string ubigeo = txtUbigeoNacimiento.Text;
                    int docIdentidad = ddlTipoDocumento.SelectedIndex; //DNI
                    if (docIdentidad == 1)
                    {
                        if (ubigeo.Length == 6) this.GuardarDatos();
                        else txtUbigeoNacimiento.Focus();
                    }
                    else this.GuardarDatos();
                }
                /*Fin:: Christian Ramirez - GIIT [Caso 48662] - 20180730*/
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F04, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? idPais = 0;
            try
            {
                this.ddlDpto.Enabled = true;
                this.CargarComboDepartamento(idPais);
                this.txtCodCiudadTel.Text = UIConstantes._valorSignoMas + ddlPais.SelectedValue;
                this.txtCodCiudadCel.Text = UIConstantes._valorSignoMas + ddlPais.SelectedValue;
                if (ddlPais.SelectedValue != UIConvertNull.String(554))
                {
                    this.ddlDpto.SelectedValue = UIConvertNull.String(77);
                    this.CargarComboProvincia(0);
                    this.ddlProvincia.Enabled = true;
                    this.ddlProvincia.SelectedValue = UIConvertNull.String(17);
                    this.CargarComboDistrito(0);
                    this.ddlDistrito.Enabled = true;
                    this.ddlDistrito.SelectedValue = UIConvertNull.String(1839);
                    this.cargarComboTipoVia();
                    this.ddlTipoVia.SelectedValue = UIConvertNull.String("Av.");
                    this.txtDireccion.Text = "Extranjero";
                    this.txtNumeracion.Text = "999";
                    this.imgDirExt.Visible = true;
                    this.imgDirExt.Visible = true;
                }
                else
                {
                    this.ddlDpto.ClearSelection();
                    this.ddlProvincia.ClearSelection();
                    this.ddlDistrito.ClearSelection();

                    this.txtCodCiudadTel.Text = UIConstantes._valorCodigoTelfPeru;
                    this.txtCodCiudadCel.Text = UIConstantes._valorCodigoTelfPeru;
                    this.txtDireccionCompleta.ReadOnly = true;
                    this.imgDirExt.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlPaisNacimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? idPaisNacimiento = 0;
            try
            {
                this.ddlDptoNacimiento.Enabled = true;
                this.CargarComboDptoNacionalidad(idPaisNacimiento);
                if (ddlPaisNacimiento.SelectedValue != UIConvertNull.String(554))
                {
                    this.ddlDptoNacimiento.SelectedValue = UIConvertNull.String(77);
                    this.ddlDptoNacimiento.SelectedValue = UIConvertNull.String(UIConstantes.idDptoExt);
                    this.ddlDptoNacimiento.Enabled = false;
                    this.ddlDptoNacimiento.CssClass = "txtTextoCombo Deshabilitado";
                }
                else
                {
                    this.ddlDptoNacimiento.Enabled = true;
                    this.ddlDptoNacimiento.CssClass = "txtTextoCombo";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlDpto_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? idDpto = null;
            try
            {
                this.ddlProvincia.Enabled = true;
                idDpto = UIConvertNull.Int32(this.ddlDpto.SelectedValue);
                this.CargarComboProvincia(idDpto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? idProv = null;
            try
            {
                this.ddlDistrito.Enabled = true;
                idProv = UIConvertNull.Int32(this.ddlProvincia.SelectedValue);
                this.CargarComboDistrito(idProv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.DireccionCompleta();
            this.updDireccionCompleta.Update();
        }

        protected void ddlTipoVia_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.DireccionCompleta();
            this.updDireccionCompleta.Update();
        }

        protected void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.DireccionCompleta();
            this.updDireccionCompleta.Update();
        }

        protected void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.DireccionCompleta();
            this.updDireccionCompleta.Update();
        }

        protected void txtInterior_TextChanged(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.DireccionCompleta();
            this.updDireccionCompleta.Update();
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.mpeMostrarError.Hide();
        }

        #endregion "Eventos"

        #region "Metodos Privados"

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
                    if (PaginaActual == UIConstantes.Formularios.F04)
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

        private void LlenarScripts()
        {
            this.txtCodCiudadTel.Attributes.Add("OnKeyPress", "soloNumeros(this);");
            this.txtNumTelefono.Attributes.Add("OnKeyPress", "soloNumeros(this);");
            this.txtCodCiudadCel.Attributes.Add("OnKeyPress", "soloNumeros(this);");
            this.txtNumCelular.Attributes.Add("OnKeyPress", "soloNumeros(this);");
        }

        private void HabilitaControles(Boolean blnAccion)
        {
            this.ddlDpto.Enabled = true;
            this.ddlProvincia.Enabled = blnAccion;
            this.ddlDistrito.Enabled = blnAccion;
            this.ddlDptoNacimiento.Enabled = blnAccion;

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

        private void CargarCombos()
        {
            this.CargarComboPais();
            this.cargarComboTipoVia();
            this.cargarComboTipoDoc();
        }

        private void CargarComboPais()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtPaises = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.PAIS].Key, "", null);
                if (dtPaises != null && dtPaises.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlPais, dtPaises.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlNacionalidad, dtPaises.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlPaisNacimiento, dtPaises.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboDepartamento(Int32? idPais)
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtDepartamentos = oGeneralBL.obtenerDepartamentoPorId(idPais);
                if (dtDepartamentos != null && dtDepartamentos.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlDpto, dtDepartamentos.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarComboTipoDoc()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtTipoDoc = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPO_DOCUMENTO].Key, "", null);
                if (dtTipoDoc != null && dtTipoDoc.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlTipoDocumento, dtTipoDoc.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboDptoNacionalidad(Int32? idPaisNacimiento)
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtDepartamentos = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.DEPARTAMENTO].Key, "", null);
                if (dtDepartamentos != null && dtDepartamentos.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlDptoNacimiento, dtDepartamentos.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboProvincia(Int32? idDpto)
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtProvincias = oGeneralBL.obtenerProvinciaPorId(idDpto);
                if (dtProvincias != null && dtProvincias.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlProvincia, dtProvincias.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboDistrito(Int32? idProvincia)
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtDistrito = oGeneralBL.obtenerDistritoPorId(idProvincia);
                if (dtDistrito != null && dtDistrito.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlDistrito, dtDistrito.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarComboTipoVia()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtTipoDireccion = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPO_VIA].Key, "", null);
                if (dtTipoDireccion != null && dtTipoDireccion.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlTipoVia, dtTipoDireccion.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void obtenerDatosPersonalesPorAplicanteId(Int32? AplicanteId)
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ListarDatosPersonalesPorIdAplicante(AplicanteId);
                this.CargarDatosPersonalesRegistrados(oAplicanteBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarDatosPersonalesRegistrados(AplicanteBE oAplicanteBE)
        {
            if (oAplicanteBE != null)
            {
                this.txtPrimNombre.Text = oAplicanteBE.PrimerNombre;
                this.txtSegNombre.Text = oAplicanteBE.SegundoNombre;

                string[] apellidos = oAplicanteBE.Apellidos.Split(' ');


                //if (apellidos.Length > 0)
                //{
                //    this.txtApePaterno.Text = apellidos[0];

                //    string apellidoMaterno = string.Empty;
                //    for (int i = 1; i < apellidos.Length; i++)
                //    {
                //        apellidoMaterno += (apellidoMaterno == string.Empty ? apellidos[i] : " " + apellidos[i]);
                //    }

                //    this.txtApeMaterno.Text = apellidoMaterno;
                //}
                //else
                //{
                //    this.txtApePaterno.Text = oAplicanteBE.Apellidos;
                //    this.txtApeMaterno.Text = string.Empty;
                //}

                this.txtApePaterno.Text = oAplicanteBE.Ape_Paterno;
                this.txtApeMaterno.Text = oAplicanteBE.Ape_Materno;

                this.rblSexo.SelectedValue = oAplicanteBE.Genero.ToString();
                if (oAplicanteBE.FechaNacimiento.HasValue)
                {
                    String FechaTmp = (oAplicanteBE.FechaNacimiento != null ? oAplicanteBE.FechaNacimiento.Value.ToString("dd/MM/yyyy") : String.Empty);

                    CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaNacimiento;
                    if (DateTime.TryParse(FechaTmp, cultura, System.Globalization.DateTimeStyles.None, out tempFechaNacimiento))
                    {
                        this.cuwFechaNacimiento.SelectedDate = tempFechaNacimiento;
                    }
                }
                if (oAplicanteBE.Direccion.Pais != null)
                {
                    this.ddlPais.SelectedValue = UIConvertNull.String(oAplicanteBE.Direccion.Pais);
                }
                else
                {
                    this.ddlPais.SelectedValue = UIConstantes._valorIdPaisPeru;
                }
                if (ddlPais.SelectedValue != null && ddlPais.SelectedValue != String.Empty)
                {
                    Int32? idPais = 0; //UIConvertNull.Int32(ddlPais.SelectedValue);
                    this.ddlDpto.Enabled = true;
                    this.CargarComboDepartamento(idPais);
                }
                this.ddlDpto.SelectedValue = UIConvertNull.String(oAplicanteBE.Direccion.Departamento);

                if (ddlDpto.SelectedValue != null && ddlDpto.SelectedValue != String.Empty)
                {
                    Int32? idDpto = UIConvertNull.Int32(ddlDpto.SelectedValue);
                    this.ddlProvincia.Enabled = true;
                    this.CargarComboProvincia(idDpto);
                }
                this.ddlProvincia.SelectedValue = UIConvertNull.String(oAplicanteBE.Direccion.Provincia);

                if (ddlProvincia.SelectedValue != null && ddlProvincia.SelectedValue != String.Empty)
                {
                    Int32? idProvincia = UIConvertNull.Int32(ddlProvincia.SelectedValue);
                    this.ddlDistrito.Enabled = true;
                    this.CargarComboDistrito(idProvincia);
                }
                this.ddlDistrito.SelectedValue = UIConvertNull.String(oAplicanteBE.Direccion.Distrito);

                this.ddlTipoDocumento.SelectedValue = UIConvertNull.String(oAplicanteBE.TipoDocumento);
                this.txtNumDocumento.Text = oAplicanteBE.DocumentoIdentidad;

                /*Ini: Christian Ramirez GIIT - Caso45903 - 20180607*/
                if (oAplicanteBE.UbigeoNacimiento != null)
                {
                    this.txtUbigeoNacimiento.Text = oAplicanteBE.UbigeoNacimiento.ToString();  /*Se agrega: Christian Ramirez[GIIT] - Caso43692 - 20180423*/
                }
                /*Fin: Christian Ramirez GIIT - Caso45903 - 20180607*/

                if (oAplicanteBE.PaisNacimiento != null)
                {
                    this.ddlPaisNacimiento.SelectedValue = UIConvertNull.String(oAplicanteBE.PaisNacimiento);
                }
                else
                {
                    this.ddlPaisNacimiento.SelectedValue = UIConstantes._valorIdPaisPeru;
                }
                if (ddlPaisNacimiento.SelectedValue != null && ddlPaisNacimiento.SelectedValue != string.Empty)
                {
                    Int32? idPaisNacimiento = UIConvertNull.Int32(ddlPaisNacimiento.SelectedValue);
                    this.ddlDptoNacimiento.Enabled = true;
                    this.CargarComboDptoNacionalidad(idPaisNacimiento);
                }

                this.ddlDptoNacimiento.SelectedValue = UIConvertNull.String(oAplicanteBE.DptoNacimiento);

                this.ddlTipoVia.SelectedValue = UIConvertNull.String(oAplicanteBE.Direccion.TipoVia);
                if (ddlPais.SelectedValue != UIConstantes._valorIdPaisPeru)
                {
                    this.txtDireccionExtranjero.Text = oAplicanteBE.Direccion.Direccion1;
                }
                this.txtDireccion.Text = oAplicanteBE.Direccion.Direccion1;
                this.txtNumeracion.Text = oAplicanteBE.Direccion.Number;
                this.txtInterior.Text = oAplicanteBE.Direccion.Interior;
                this.txtDireccionCompleta.Text = oAplicanteBE.Direccion.Direccion2;
                this.txtReferencia.Text = oAplicanteBE.Direccion.Reference;

                if (oAplicanteBE.NacionalidadPrimaria != null)
                {
                    this.ddlNacionalidad.SelectedValue = UIConvertNull.String(oAplicanteBE.NacionalidadPrimaria);
                }
                else
                {
                    this.ddlNacionalidad.SelectedValue = UIConstantes._valorIdPaisPeru;
                }
                if (oAplicanteBE.Direccion.Pais == 554)
                {
                    this.txtCodCiudadTel.Text = UIConstantes._valorCodigoTelfPeru;
                    this.txtCodCiudadCel.Text = UIConstantes._valorCodigoTelfPeru;
                }
                else
                {
                    this.txtCodCiudadTel.Text = ddlPais.SelectedValue;
                    this.txtCodCiudadCel.Text = ddlPais.SelectedValue;
                }

                if (oAplicanteBE.Telefono != null)
                {
                    if (oAplicanteBE.Telefono.Pais != null)
                    {
                        if (oAplicanteBE.Telefono.Pais == 554)
                        {
                            this.txtCodCiudadTel.Text = UIConstantes._valorCodigoTelfPeru;
                        }
                        else
                        {
                            this.txtCodCiudadTel.Text = UIConvertNull.String(oAplicanteBE.Telefono.Pais);
                        }
                    }
                    if (oAplicanteBE.Telefono.NroTelefono != null)
                    {
                        this.txtNumTelefono.Text = UIConvertNull.String(oAplicanteBE.Telefono.NroTelefono);
                    }
                }
                if (oAplicanteBE.Celular != null)
                {
                    if (oAplicanteBE.Celular.Pais != null)
                    {
                        if (oAplicanteBE.Celular.Pais == 554)
                        {
                            this.txtCodCiudadCel.Text = UIConstantes._valorCodigoTelfPeru;
                        }
                        else
                        {
                            this.txtCodCiudadCel.Text = UIConvertNull.String(oAplicanteBE.Celular.Pais);
                        }
                    }
                    if (oAplicanteBE.Celular.NroCelular != null)
                    {
                        this.txtNumCelular.Text = UIConvertNull.String(oAplicanteBE.Celular.NroCelular);
                    }
                }
                this.txtEmail1.Text = oAplicanteBE.CorreoPersonal;
                this.txtEmail2.Text = oAplicanteBE.CorreoLaboral;
            }
        }

        private void GuardarDatos()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            Session["EstadoEnvio"] = true;
            String claveTipPostul = String.Empty;
            GeneralBL oGeneralBL = null;
            DataTable dtPagSigui = null;
            String PaginaActual, PaginaSiguiente = null;
            try
            {
                Int32? FechaNac = cuwFechaNacimiento.SelectedDate.Value.Year;
                Int32? FechaAct = DateTime.Today.Year;

                if ((FechaAct - FechaNac) < 10)
                {
                    this.lblmessage.Text = "Su edad no esta permitido para postular.";
                    this.mpeMostrarError.Show();
                    this.cuwFechaNacimiento.Focus();
                    return;
                }

                oAplicanteBE = new AplicanteBE();
                oAplicanteBE = obtenerDatosPersonales(oAplicanteBE);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormTres_DatoPersonal(oAplicanteBE);
                if (operacionOK)
                {
                    Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                    oGeneralBL = new GeneralBL();
                    dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    {
                        PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F04)
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

        private AplicanteBE obtenerDatosPersonales(AplicanteBE oAplicanteBE)
        {
            //Este Valor se debe obtener de la sesion anterior y tambien nulo
            oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"].ToString());

            oAplicanteBE.Estado = int.Parse(UIConstantes.ESTADO_POSTULANTE.PENDIENTE.ToString("D"));
            oAplicanteBE.RedId = Session["usrRedId"].ToString();
            oAplicanteBE.DireccionConcatenado = ddlTipoVia.SelectedValue + " " + txtDireccion.Text + " " + txtNumeracion.Text + " " + txtInterior.Text
                                                + " - " + ddlDistrito.SelectedItem + " - " + ddlProvincia.SelectedItem + " - " + ddlDpto.SelectedItem;

            //================================================================
            //OBTENIENDO EL PERIODO - INICIO
            //================================================================
            GeneralBL oGeneralBL = new GeneralBL();
            DataTable dtPeriodoUP = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.PERIODO_PREGRADO].Key, "", null);
            if (dtPeriodoUP != null && dtPeriodoUP.Rows.Count > 0)
            {
                int tempPeriodoSesion = 0;
                int.TryParse(dtPeriodoUP.Rows[0]["codigo"].ToString(), out tempPeriodoSesion);
                oAplicanteBE.IdPeriodoSesion = tempPeriodoSesion;
            }
            //================================================================
            //OBTENIENDO EL PERIODO - FIN
            //================================================================
            if (UIConvertNull.Int32(Session["ModPostulacion"]) == UIConstantes.Modalidad.TallerEPU || UIConvertNull.Int32(Session["ModPostulacion"]) == UIConstantes.Modalidad.ProgramaEPU)
            {
                oAplicanteBE.IdConfiguracionAplicacion = int.Parse(UIConstantes.TIPO_FORMULARIO.ADMISION_EPU.ToString("D"));
            }
            else
            {
                oAplicanteBE.IdConfiguracionAplicacion = int.Parse(UIConstantes.TIPO_FORMULARIO.PREGRADO.ToString("D"));
            }
            if (this.txtPrimNombre.Text != string.Empty)
            {
                oAplicanteBE.PrimerNombre = this.txtPrimNombre.Text.Replace("'", "''");
            }
            if (this.txtSegNombre.Text != string.Empty)
            {
                oAplicanteBE.SegundoNombre = this.txtSegNombre.Text.Replace("'", "''");
            }
            if (this.txtApePaterno.Text != string.Empty)
            {
                oAplicanteBE.Apellidos = this.txtApePaterno.Text.Replace("'", "''") + (this.txtApeMaterno.Text == string.Empty ? string.Empty : " " + this.txtApeMaterno.Text);
            }
            oAplicanteBE.Ape_Paterno = txtApePaterno.Text;
            oAplicanteBE.Ape_Materno = txtApeMaterno.Text;
            if (this.rblSexo.SelectedValue != null && this.rblSexo.SelectedValue.ToString() != "0")
            {
                int tempGenero = 0;
                int.TryParse(this.rblSexo.SelectedValue.ToString(), out tempGenero);
                if (tempGenero != 0)
                {
                    oAplicanteBE.Genero = tempGenero;
                }
            }

            if (this.cuwFechaNacimiento.SelectedDate != null)
            {
                CultureInfo cultura = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                DateTime tempFechaNacimiento;
                if (DateTime.TryParse(UIConvertNull.String(this.cuwFechaNacimiento.SelectedDate), cultura, System.Globalization.DateTimeStyles.None, out tempFechaNacimiento))
                {
                    oAplicanteBE.FechaNacimiento = tempFechaNacimiento;
                }
            }

            oAplicanteBE.TipoDocumento = ddlTipoDocumento.SelectedValue;

            if (ddlTipoDocumento.SelectedValue == "PASAPORTE")
            {
                oAplicanteBE.NumeroPasaporte = this.txtNumDocumento.Text.Replace("'", "''");
            }

            if (this.txtNumDocumento.Text != string.Empty)
            {
                oAplicanteBE.DocumentoIdentidad = this.txtNumDocumento.Text.Replace("'", "''");
            }

            /*Ini: Christian Ramirez[GIIT] - Caso43692 - 20180423*/
            if (this.txtUbigeoNacimiento.Text != string.Empty)
            {
                /*Ini: Christian Ramirez GIIT - Caso45903 - 20180607*/
                oAplicanteBE.UbigeoNacimiento = txtUbigeoNacimiento.Text;
                /*Fin: Christian Ramirez GIIT - Caso45903 - 20180607*/
            }
            /*Fin: Christian Ramirez[GIIT] - Caso43692 - 20180423*/

            if (this.ddlPaisNacimiento.SelectedValue != null && this.ddlPaisNacimiento.SelectedValue.ToString() != "0")
            {
                int tempPaisNac = 0;
                int.TryParse(this.ddlPaisNacimiento.SelectedValue.ToString(), out tempPaisNac);
                if (tempPaisNac != 0)
                {
                    oAplicanteBE.PaisNacimiento = tempPaisNac;
                }
            }
            if (this.ddlDptoNacimiento.SelectedValue != null && this.ddlDptoNacimiento.SelectedValue.ToString() != "0")
            {
                int tempDptoNac = 0;
                int.TryParse(this.ddlDptoNacimiento.SelectedValue.ToString(), out tempDptoNac);
                if (tempDptoNac != 0)
                {
                    oAplicanteBE.DptoNacimiento = tempDptoNac;
                }
            }

            if (this.ddlNacionalidad.SelectedValue != null && this.ddlNacionalidad.SelectedValue.ToString() != "0")
            {
                int tempNacionalidad = 0;
                int.TryParse(this.ddlNacionalidad.SelectedValue.ToString(), out tempNacionalidad);
                if (tempNacionalidad != 0)
                {
                    oAplicanteBE.NacionalidadPrimaria = tempNacionalidad;
                }
            }

            //================================================================
            //OBTENIENDO DIRECCION - INICIO
            //================================================================
            oAplicanteBE.Direccion = null;
            oAplicanteBE.Direccion = new DireccionAplicanteBE();

            ///Si la dirección es en Perú
            if (ddlPais.SelectedValue == UIConstantes._valorIdPaisPeru)
            {
                oAplicanteBE.Direccion.Pais = UIConvertNull.Int32(ddlPais.SelectedValue);
                oAplicanteBE.Direccion.Departamento = UIConvertNull.Int32(ddlDpto.SelectedValue);
                oAplicanteBE.Direccion.Provincia = UIConvertNull.Int32(ddlProvincia.SelectedValue);
                oAplicanteBE.Direccion.Distrito = UIConvertNull.String(ddlDistrito.SelectedValue);
                oAplicanteBE.Direccion.IdTipoDireccion = UIConstantes.TIPO_DIRECCION.DOMICILIO;
                oAplicanteBE.Direccion.TipoVia = ddlTipoVia.SelectedValue;
                oAplicanteBE.Direccion.Direccion1 = this.txtDireccion.Text;
                oAplicanteBE.Direccion.Number = this.txtNumeracion.Text;
                oAplicanteBE.Direccion.Interior = this.txtInterior.Text;
                oAplicanteBE.Direccion.Reference = this.txtReferencia.Text;
            }
            else ///Si la dirección es el Extranjero
            {
                oAplicanteBE.Direccion.Pais = UIConvertNull.Int32(ddlPais.SelectedValue);
                oAplicanteBE.Direccion.Departamento = UIConstantes.idDptoExt;
                oAplicanteBE.Direccion.Provincia = UIConstantes.idProvExt;
                oAplicanteBE.Direccion.Distrito = UIConvertNull.String(1839);
                oAplicanteBE.Direccion.Direccion1 = this.txtDireccionExtranjero.Text;
                oAplicanteBE.Direccion.IdTipoDireccion = UIConstantes.TIPO_DIRECCION.DOMICILIO;
                oAplicanteBE.Direccion.TipoVia = null;
                oAplicanteBE.Direccion.Number = null;
                oAplicanteBE.Direccion.Interior = null;
                oAplicanteBE.Direccion.Reference = null;
            }

            //================================================================
            //OBTENIENDO TELEFONO - INICIO
            //================================================================
            if (this.txtNumTelefono.Text != string.Empty)
            {
                if (oAplicanteBE.Telefono == null)
                {
                    oAplicanteBE.Telefono = new TelefonoBE();
                }
                oAplicanteBE.Telefono.Pais = UIConvertNull.Int32(ddlPais.SelectedValue);
                oAplicanteBE.Telefono.NroTelefono = this.txtNumTelefono.Text;
                oAplicanteBE.Telefono.TipoTelefono = UIConstantes.TIPO_TELEFONO.CASA;
            }

            if (this.txtNumCelular.Text != string.Empty)
            {
                if (oAplicanteBE.Celular == null)
                {
                    oAplicanteBE.Celular = new TelefonoBE();
                }
                oAplicanteBE.Celular.Pais = UIConvertNull.Int32(ddlPais.SelectedValue);
                oAplicanteBE.Celular.NroTelefono = this.txtNumCelular.Text;
                oAplicanteBE.Celular.TipoTelefono = UIConstantes.TIPO_TELEFONO.CELULAR;
            }

            if (this.txtEmail1.Text != null)
            {
                oAplicanteBE.CorreoPersonal = this.txtEmail1.Text;
            }

            if (this.txtEmail2.Text != null)
            {
                oAplicanteBE.CorreoLaboral = this.txtEmail2.Text;
            }
            oAplicanteBE.EstaInteresadoPlanComida = UIConstantes.idValorNulo;
            oAplicanteBE.EstaInteresadoPlanResidenciaUni = UIConstantes.idValorNulo;

            //=====================================================
            //MODALIDAD POSTULACION
            //=====================================================
            oAplicanteBE.ModalidadPostulacion = UIConvertNull.Int32(Session["ModPostulacion"]);

            return oAplicanteBE;
        }

        public void DireccionCompleta()
        {
            String direccionCompleta = String.Empty;
            if (!ddlTipoVia.SelectedItem.Text.Equals("-- Seleccionar --"))
                direccionCompleta = direccionCompleta + ddlTipoVia.SelectedItem.Text + " ";
            if (!txtDireccion.Text.Equals(String.Empty))
                direccionCompleta = direccionCompleta + txtDireccion.Text + " ";
            if (!txtNumeracion.Text.Equals(String.Empty))
                direccionCompleta = direccionCompleta + txtNumeracion.Text + " ";
            if (!txtInterior.Text.Equals(String.Empty))
                direccionCompleta = direccionCompleta + ", Interior: " + txtInterior.Text + " ";
            if (!ddlDistrito.SelectedItem.Text.Equals("- - EXTERIOR - -"))
            {
                direccionCompleta = direccionCompleta + " - " + ddlDistrito.SelectedItem.Text + " ";
            }
            txtDireccionCompleta.Text = direccionCompleta;
        }

        /*Ini: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
        private void HabilitarControlesMayusculas()
        {
            txtDireccionCompleta.Text = txtDireccionCompleta.Text.ToUpper();
            updDireccionCompleta.Update();

            foreach (Control c in Page.Controls)
            {
                foreach (Control c2 in c.Controls)
                {
                    if (c2 is TextBox)
                    {
                        TextBox txt = c2 as TextBox;
                        if (txt.UniqueID == "txtEmail1" || txt.UniqueID == "txtEmail2") continue;
                        if (txt.Text != "") txt.Text = txt.Text.ToUpper();
                    }
                }
               
            }
        }
        /*Fin: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
        #endregion "Metodos Privados"
    }
}
