using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;
using System.Globalization;
using System.Collections;
using System.Collections.ObjectModel;
using System.Configuration;

namespace UPAdmPre.Web.Admision
{
    public partial class frm20_FormalizaIng : System.Web.UI.Page
    {
        private Hashtable _extensionesValidas;
        Collection<AvailableFileType> _tiposArchivos;

        protected void Page_Load(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            try
            {
                ///*Solo para pruebas*/
                //Session["AplicanteId"] = "551752";// ---; PRUEBA

                this.ConfigurarTiposArchivos();
                if (!IsPostBack)
                {
                    EncryptBL _EncryptBL = new EncryptBL();
                    Int32? AplicanteId = null;

                    Session["AplicanteId"] = UIConvertNull.Int32(_EncryptBL.DecryptKey(Request.QueryString["ApplicationId"]));
                    AplicanteId = UIConvertNull.Int32(_EncryptBL.DecryptKey(Request.QueryString["ApplicationId"]));
                    //AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]); //PRUEBA

                    Session["Flag_Preformalizar"] = false;
                    Session["Flag_EncuestaRGC"] = false;
                    string slink = "http://www.up.edu.pe/admision/portal/Paginas/conoce-tu-escala.aspx?appId=";


                    this.CargarTextos();
                    this.CargarTextosEtiquetas();
                    this.CargarComboHorarios(AplicanteId);
                    this.LLenarDatosPersona();
                    this.MostrarOcultarBotones(false);
                    this.CargarCombos();
                    this.obtenerDatosPersonalesPorAplicanteId(AplicanteId);
                    this.CargarInfRelFamRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                    HabilitarControlesMayusculas();
                    //**************************                                    
                    this.LLenarGrillaDocumentos();
                    ObtenerExamenesXModalidad();
                    ObtenerCursosConvalidar();
                    ObtenerDatosFinales();
                    ObtenerAutorizacionDatos();
                    ValidarPreformalizacion();

                    //INI: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRGC)
                    //this.CargarEncuesta();
                    //this.ComprobarEncuesta();
                    //FIN: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRGC)

                    //INI: JC.DelgadoV[RQ103950] Observaciones Pre Formalización

                    Session["TieneNotasRendimientoAcademico"] = false;
                    if (Session["ModPostulacion"].ToString() != "43" &&
                       Session["ModPostulacion"].ToString() != "44" && 
                       Session["ModPostulacion"].ToString() != "42" &&
                       Session["ModPostulacion"].ToString() != "46")
                    {
                        Session["TieneNotasRendimientoAcademico"] = true;
                    }

                    if (Convert.ToBoolean(Session["TieneNotasRendimientoAcademico"]))
                    {
                        MostrarDetalleRendimientoAcademico();
                        CargarDetalleRendimientoAcademico(AplicanteId);
                    }
                    //FIN: JC.DelgadoV[RQ103950] Observaciones Pre Formalización

                    //Comprobar fecha fin formalizacion
                    bool flagFechaFinFormalizacion = ComprobarFechaFinFormalizacion(AplicanteId);
                    //*********************************

                    if (!flagFechaFinFormalizacion)
                    {
                        BloquearFormalizacion();
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F20, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        private void BloquearFormalizacion()
        {
            pnlDatosPersonales.Visible = false;
            pnlDatosContacto.Visible = false;
            pnlDatosPadres.Visible = false;
            pnlDetalleCompetencias.Visible = false;
            pnlArchivos.Visible = false;
            pnlDatosFinales.Visible = false;

            pnlMensajeCierreFormalizacion.Visible = true;


            //**********************************************
            //Bloqueando Botones
            this.imgGuardarDatosPersonales.Enabled = false;
            this.BtnAgregaPariente2.Enabled = false;
            this.BtnAgregaPariente3.Enabled = false;
            this.BtnOcultaPariente2.Enabled = false;
            this.BtnOcultaPariente3.Enabled = false;
            this.btnGuardarDatosFamiliares.Enabled = false;
            this.btnGuardarDatosFinales.Enabled = false;
            this.btnEnviarExpedientes.Enabled = false;

            this.imgGuardarDatosPersonales.Visible = false;
            this.BtnAgregaPariente2.Visible = false;
            this.BtnAgregaPariente3.Visible = false;
            this.BtnOcultaPariente2.Visible = false;
            this.BtnOcultaPariente3.Visible = false;
            this.btnGuardarDatosFamiliares.Visible = false;
            this.btnGuardarDatosFinales.Visible = false;
            this.btnEnviarExpedientes.Visible = false;

            BloquearCursosYExamenes();

            //INI: JC.DelgadoV[RQ103950] Observaciones Pre Formalización
            //Datos finales deshabilitando combos
            ddlCarrera.Enabled = false;
            ddlSeguroRenta.Enabled = false;
            ddlReservaMatricula.Enabled = false;
            ddlDeportistaDestacado.Enabled = false;

            //****
            this.imgActualizarCantidadCompetencias.Visible = false;
            this.btnEditarNotasQuintoLetra.Visible = false;
            this.imgGuardarDetalleCompRend.Visible = false;

            //Detalle Competencias deshabilitando combos
            ddlColegioQuinto.Enabled = false;
            ddlAnioLectivoQuinto.Enabled = false;
            ddlTipoCalificacionQuinto.Enabled = false;
            //FIN: JC.DelgadoV[RQ103950] Observaciones Pre Formalización

            //CheckboxUsoDeDatos
            chkUsoDatos.Enabled = false;
            chkUsoDatosTerceros.Enabled = false;

            LLenarGrillaDocumentos();


            if (Convert.ToBoolean(Session["TieneNotasRendimientoAcademico"]))
            {
                MostrarDetalleRendimientoAcademico();
                CargarDetalleRendimientoAcademico(Convert.ToInt32(Session["AplicanteId"]));
            }
        }

        private bool ComprobarFechaFinFormalizacion(Int32? AplicanteId)
        {
            bool flagFechaFinFormalizacion = false;

            AplicanteBL oAplicanteBL = null;
            DataTable dtDatos = null;

            oAplicanteBL = new AplicanteBL();
            dtDatos = oAplicanteBL.ObtenerFlagFechaFinFormalizacion(AplicanteId);

            flagFechaFinFormalizacion = Convert.ToBoolean(dtDatos.Rows[0]["FlagFormalizacionHabilitada"]);
            lblFechaFinFormalizacion.Text = dtDatos.Rows[0]["FechaFinFormalizacion"].ToString();

            return flagFechaFinFormalizacion;
        }

        private void CargarTextos()
        {
            EncryptBL _EncryptBL = new EncryptBL();
            Int32? AplicanteId = null;
            AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            this.ObtenerTextoEnsayo(AplicanteId, 28); // 28 por el mensaje de formalización

            this.ObtenerTextoInformativo(AplicanteId, 16);
            this.lblMensajeInformativoInf.Visible = false;
        }

        private void CargarTextosEtiquetas()
        {
                this.lblTituloSeleccion.Text = "Información Personal";
                //this.lblSubTituloHorario.Text = "Horario de Formalización:";
        }

        private void ObtenerTextoEnsayo(Int32? AplicanteId, Int32? @VC_TipoMensajesId)
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                DataTable dtTextoInf = oAplicanteBL.ObtenerTextoInformativo(AplicanteId, @VC_TipoMensajesId);
                if (dtTextoInf.Rows.Count > 0)
                {
                    DataRow drPago = dtTextoInf.Rows[0];
                    String strTexto = drPago["Texto"].ToString();
                    //this.lblMsg.Text = strTexto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerTextoInformativo(Int32? AplicanteId, Int32? @VC_TipoMensajesId)
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                DataTable dtTextoInf = oAplicanteBL.ObtenerTextoInformativo(AplicanteId, @VC_TipoMensajesId);
                if (dtTextoInf.Rows.Count > 0)
                {
                    DataRow drPago = dtTextoInf.Rows[0];
                    String strTexto = drPago["Texto"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboHorarios(Int32? AplicanteId)
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtHorarios = oGeneralBL.ObtenerHorariosDeFormalizacion(AplicanteId);
                if (dtHorarios != null && dtHorarios.Rows.Count > 0)
                {
                    //Funciones.cargarComboYSeleccione(this.ddlHorario, dtHorarios.Copy(), "Descripcion", "Codigo", "-- Seleccionar --");
                }
                else
                {
                    //Funciones.cargarComboYSeleccione(this.ddlHorario, null, "Descripcion", "Codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosPersona()
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtDatos = null;
            try
            {
                EncryptBL _EncryptBL = new EncryptBL();
                Int32? AplicanteId = null;
                AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);

                oAplicanteBL = new AplicanteBL();
                dtDatos = oAplicanteBL.ObtenerDatosPostulanteParaEntrevista(AplicanteId);
                if (dtDatos != null && dtDatos.Rows.Count > 0)
                {
                    this.lblPostulante.Text = dtDatos.Rows[0]["Aplicante"].ToString();
                    this.lblNroDocumento.Text = dtDatos.Rows[0]["DNI"].ToString();
                    this.lblModalidad.Text = dtDatos.Rows[0]["Modalidad"].ToString();
                    this.lblCarrera.Text = dtDatos.Rows[0]["Carrera"].ToString();

                    Session["SituacionAcademica"] = dtDatos.Rows[0]["SituacionAcademica"].ToString();
                    Session["ModPostulacion"] = dtDatos.Rows[0]["IdModalidad"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ObtenerTextoInformativoInf(Int32? AplicanteId, Int32? MensajesId)
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                this.lblMensajeInformativoInf.Visible = true;
                oAplicanteBL = new AplicanteBL();
                DataTable dtTextoInf = oAplicanteBL.ObtenerTextoInformativo(AplicanteId, MensajesId);
                if (dtTextoInf.Rows.Count > 0)
                {
                    DataRow drPago = dtTextoInf.Rows[0];
                    String strTexto = drPago["Texto"].ToString();
                    this.lblMensajeInformativoInf.Text = strTexto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            this.mpeMostrarError.Hide();
        }

        //Inicio: JC.DelgadoV - Formalización [20210120]

        private void CargarCombos()
        {
            this.CargarComboPais();
            this.cargarComboTipoVia();
            this.cargarComboTipoDoc();
            //
            this.CargarComboTipoDocumento();
            this.CargarComboRelFam();
            //
            this.cargarComboCarreras();
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

        protected void ddlPaisNacimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
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

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
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

        protected void ddlDpto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
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
            //UIHelper.SessionActiva(Page);
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
            //UIHelper.SessionActiva(Page);
            this.DireccionCompleta();
            this.updDireccionCompleta.Update();
        }

        protected void ddlTipoVia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            this.DireccionCompleta();
            this.updDireccionCompleta.Update();
        }

        protected void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            this.DireccionCompleta();
            this.updDireccionCompleta.Update();
        }

        protected void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            this.DireccionCompleta();
            this.updDireccionCompleta.Update();
        }

        protected void txtInterior_TextChanged(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            this.DireccionCompleta();
            this.updDireccionCompleta.Update();
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

                Session["usrRedId"] = oAplicanteBE.RedId;

                this.txtPrimNombre.Text = oAplicanteBE.PrimerNombre;
                this.txtSegNombre.Text = oAplicanteBE.SegundoNombre;

                string[] apellidos = oAplicanteBE.Apellidos.Split(' ');

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

                //Desabilitando ddlTipoDocumento y txtNumDocumento
                this.ddlTipoDocumento.Enabled = false;
                this.txtNumDocumento.Enabled = false;

                if (oAplicanteBE.UbigeoNacimiento != null)
                {
                    this.txtUbigeoNacimiento.Text = oAplicanteBE.UbigeoNacimiento.ToString();
                }

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

        //****************************************************
        //Datos Familiares
        //****************************************************
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

        protected void BtnOcultaPariente2_Click(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
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

        private void MostrarOcultarBotones(Boolean blnAccion)
        {
            this.pnlRelFam2.Visible = blnAccion;
            this.pnlRelFam3.Visible = blnAccion;
            this.BtnOcultaPariente2.Visible = blnAccion;
            this.BtnAgregaPariente3.Visible = blnAccion;
            this.BtnOcultaPariente3.Visible = blnAccion;
        }

        protected void BtnAgregaPariente2_Click(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            this.pnlRelFam2.Visible = true;
            this.BtnOcultaPariente2.Visible = true;
            this.BtnAgregaPariente2.Visible = false;
            this.BtnOcultaPariente3.Visible = false;
            this.BtnAgregaPariente3.Visible = true;
        }

        protected void BtnOcultaPariente3_Click(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
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
            //UIHelper.SessionActiva(Page);
            this.pnlRelFam3.Visible = true;
            this.BtnOcultaPariente3.Visible = true;
            this.BtnAgregaPariente3.Visible = false;
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

        //**********************************************
        protected void imbCargarDoc_OnClick(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            ImageButton imbCargarDoc = (ImageButton)(sender);
            GridViewRow gvrow = (GridViewRow)imbCargarDoc.NamingContainer;
            try
            {
                this.lblError.Text = null;
                if (hCodDocumento.Value == "21PRE")
                {
                    this.imgJPGPopUp.Visible = true;
                    this.imgPDFPopUp.Visible = false;
                }
                else
                {
                    this.imgJPGPopUp.Visible = false;
                    this.imgPDFPopUp.Visible = true;
                }
                this.mpeCargaDocumentos.Show();
                upResultado.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.upCargaDocumentos.Update();
            }
        }

        protected void imbDeleteDoc_OnClick(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            ImageButton imgDelDoc = (ImageButton)(sender);
            GridViewRow gvrow = (GridViewRow)imgDelDoc.NamingContainer;


            Label DocumentId = (Label)gvrow.FindControl("lblCodigo");
            Label ApplicationId = (Label)gvrow.FindControl("lblApplicationId");
            Label ApplicationAttachmentId = (Label)gvrow.FindControl("lblIdDocumento");

            try
            {
                int resp = new DocumentoBL().PreformalizacionUpdDocumentoAdjunto(Convert.ToInt32(ApplicationAttachmentId.Text),
                                                                 Convert.ToInt32(ApplicationId.Text),
                                                                 DocumentId.Text);
                if (resp == 1)
                {
                    this.LLenarGrillaDocumentos();
                    upResultado.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.upCargaDocumentos.Update();
            }
        }

        private void LLenarGrillaDocumentos()
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtDocumentos = oGeneralBL.PreformalizacionObtenerDocsPorModalidadPostulacion(AplicanteId);
                if (dtDocumentos != null && dtDocumentos.Rows.Count > 0)
                {
                    this.gvDocumentos.DataSource = dtDocumentos;
                    this.gvDocumentos.DataBind();
                }
                else
                {
                    this.gvDocumentos.DataSource = null;
                    this.gvDocumentos.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void imbDescargaDoc_OnClick(object sender, ImageClickEventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            ImageButton _imbEditar = (ImageButton)(sender);
            GridViewRow gvrow = (GridViewRow)_imbEditar.NamingContainer;
            try
            {
                this.DescargarDocumento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DescargarDocumento()
        {
            DocumentoBE oDocumentoBE = null;
            DocumentoBL oDocumentoBL = null;
            try
            {
                oDocumentoBE = new DocumentoBE();
                oDocumentoBE.IdAplicacion = UIConvertNull.Int32(Session["AplicanteId"]);
                oDocumentoBE.TituloDocumento = hCodDocumento.Value;

                oDocumentoBL = new DocumentoBL();
                oDocumentoBE = oDocumentoBL.PreformalizacionObtenerDocumentoAdjunto(oDocumentoBE);

                String nombreArchivo = oDocumentoBE.TituloDocumento + oDocumentoBE.Extension;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + nombreArchivo + "\"");
                HttpContext.Current.Response.ContentType = oDocumentoBE.TipoMedio;
                HttpContext.Current.Response.BinaryWrite(oDocumentoBE.ContenidoDocumento);
                HttpContext.Current.Response.Flush();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drw = (DataRowView)e.Row.DataItem;
                    DataRow dr = drw.Row;
                    e.Row.Cells[0].Text = ((this.gvDocumentos.PageIndex * this.gvDocumentos.PageSize) + (e.Row.RowIndex + 1)).ToString();

                    Label _lblIdDocumento = (Label)e.Row.FindControl("lblIdDocumento");
                    Label _lblDescEstado = (Label)e.Row.FindControl("lblDescEstado");
                    HyperLink _hlkDocumento = (HyperLink)e.Row.FindControl("hlkDocumento");
                    ImageButton _imbCargarDoc = (ImageButton)e.Row.FindControl("imbCargarDoc");
                    ImageButton _imbDescargaDoc = (ImageButton)e.Row.FindControl("imbDescargaDoc");
                    ImageButton _imgDelDoc = (ImageButton)e.Row.FindControl("imgDelDoc");
                    Label _lblDescripcion = (Label)e.Row.FindControl("Label4");
                    Label _lblObservacion = (Label)e.Row.FindControl("Label5");
                    Label _lblEstado = (Label)e.Row.FindControl("lblEstado");
                    Image _imgEstado = (Image)e.Row.FindControl("imgEstado");
                    Label _lblGovernmentId = (Label)e.Row.FindControl("Label6");


                    if (UIConvertNull.String(dr["LinkDoc"]) != null)
                    {
                        _hlkDocumento.NavigateUrl = UIConvertNull.String(dr["LinkDoc"]);
                        _hlkDocumento.Target = "_blank";
                        _hlkDocumento.Enabled = true;
                    }

                    if (UIConvertNull.String(dr["Estado"]) != null)
                    {
                        _lblDescEstado.Text = UIConvertNull.String(dr["Estado"]).ToUpper();
                        _lblEstado.Text = UIConvertNull.String(dr["Estado"]);

                        if (UIConvertNull.String(dr["Estado"]) == UIConstantes.EstadoDocumento.strEnRevision)
                        {
                            _imgEstado.ImageUrl = "../Images/icoEnRevision.png";
                        }
                        if (UIConvertNull.String(dr["Estado"]) == UIConstantes.EstadoDocumento.strObservado)
                        {
                            _imgEstado.ImageUrl = "../Images/icoObservado.png";
                        }
                        if (UIConvertNull.String(dr["Estado"]) == UIConstantes.EstadoDocumento.strAprobado)
                        {
                            _imgEstado.ImageUrl = "../Images/icoAprobado.png";
                        }
                    }

                    if (UIConvertNull.String(dr["Observacion"]) != "()")
                    {
                        _lblDescripcion.Text = UIConvertNull.String(dr["Descripcion"]).ToString();
                        _lblObservacion.Text = UIConvertNull.String(dr["Observacion"]).ToString();
                        _lblGovernmentId.Text = UIConvertNull.String(dr["GovernmentId"]).ToString();
                        _lblObservacion.Visible = true;
                    }
                    else
                    {
                        _lblDescripcion.Text = UIConvertNull.String(dr["Descripcion"]).ToString();
                        _lblGovernmentId.Text = UIConvertNull.String(dr["GovernmentId"]).ToString();
                        _lblObservacion.Visible = false;
                    }

                    if (Convert.ToBoolean(Session["Flag_Preformalizar"]))
                    {
                        switch (_lblDescEstado.Text)
                        {
                            case "PENDIENTE":
                                _imbCargarDoc.Enabled = false;
                                _imbCargarDoc.Visible = false;
                                _imbDescargaDoc.Enabled = false;
                                _imbDescargaDoc.Visible = false;
                                _imgDelDoc.Visible = false;
                                break;
                            case "EN REVISIÓN":
                                _imbCargarDoc.Enabled = false;
                                _imbCargarDoc.Visible = false;
                                _imbDescargaDoc.Enabled = true;
                                _imbDescargaDoc.Visible = true;
                                _imgDelDoc.Visible = false;
                                break;
                            case "OBSERVADO":
                                _imbCargarDoc.Enabled = true;
                                _imbCargarDoc.Visible = true;
                                _imbDescargaDoc.Enabled = false;
                                _imbDescargaDoc.Visible = false;
                                _imgDelDoc.Visible = false;
                                break;
                            case "APROBADO":
                                _imbCargarDoc.Enabled = false;
                                _imbCargarDoc.Visible = false;
                                _imbDescargaDoc.Enabled = true;
                                _imbDescargaDoc.Visible = true;
                                _imgDelDoc.Visible = false;
                                break;
                        }
                    }
                    else
                    {
                        switch (_lblDescEstado.Text)
                        {
                            case "PENDIENTE":
                                _imbCargarDoc.Enabled = true;
                                _imbCargarDoc.Visible = true;
                                _imbDescargaDoc.Enabled = false;
                                _imbDescargaDoc.Visible = false;
                                _imgDelDoc.Visible = false;
                                break;
                            case "EN REVISIÓN":
                                _imbCargarDoc.Enabled = false;
                                _imbCargarDoc.Visible = false;
                                _imbDescargaDoc.Enabled = true;
                                _imbDescargaDoc.Visible = true;
                                _imgDelDoc.Visible = true;
                                break;
                            case "OBSERVADO":
                                _imbCargarDoc.Enabled = true;
                                _imbCargarDoc.Visible = true;
                                _imbDescargaDoc.Enabled = false;
                                _imbDescargaDoc.Visible = false;
                                _imgDelDoc.Visible = false;
                                break;
                            case "APROBADO":
                                _imbCargarDoc.Enabled = false;
                                _imbCargarDoc.Visible = false;
                                _imbDescargaDoc.Enabled = true;
                                _imbDescargaDoc.Visible = true;
                                _imgDelDoc.Visible = false;
                                break;
                        }
                    }
                    
                    String strCadena = UIConstantes._valorCadenaVacia;
                    strCadena = strCadena + UIHelper.AsignarDatoControlHtml(this.hCodDocumento.ClientID, UIConvertNull.String(dr["CodDoc"]));
                    strCadena = strCadena + UIHelper.AsignarDatoControlHtml(this.hIdDocumento.ClientID, UIConvertNull.String(dr["ApplicationAttachmentId"]));
                    UIHelper.SeleccionarItemGrillaOnClickMoverRaton(e, strCadena);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAdjuntar_Click(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            AplicanteBE oAplicanteBE = null;
            DocumentoBE oDocumentoBE = null;
            DocumentoBL oDocumentoBL = null;
            try
            {
                this.lblError.Visible = false;
                String TempDocumentoId = hIdDocumento.Value.ToString();
                Int32 idDocumento = 0;
                Int32.TryParse(TempDocumentoId, out idDocumento);

                if (this.fuDocumento.HasFile && Session["AplicanteId"] != null)
                {
                    oDocumentoBE = new DocumentoBE();
                    oDocumentoBE.IdDocumento = UIConvertNull.Int32(hIdDocumento.Value);
                    oDocumentoBE.IdAplicacion = UIConvertNull.Int32(Session["AplicanteId"]);
                    oDocumentoBE.TituloDocumento = hCodDocumento.Value;
                    oDocumentoBE.Extension = ObtenerExtensionArchivo(fuDocumento);
                    oDocumentoBE.ContenidoDocumento = fuDocumento.FileBytes;
                    oDocumentoBE.IdTipoMedio = ObtenerTipoArchivo(oDocumentoBE.Extension);
                    oDocumentoBE.TipoMedio = fuDocumento.PostedFile.ContentType;
                    oDocumentoBE.UsuarioActualiza = Session["usrRedId"].ToString();
                    oDocumentoBE.Estado = 2;

                    if (oDocumentoBE.IdTipoMedio == 0 || (oDocumentoBE.Extension == ".docx" || oDocumentoBE.Extension == ".doc"))
                    {
                        this.lblmessage.Text = "El archivo que desea subir no se encuentra configurado en el sistema para el documento seleccionado.";
                        this.mpeMostrarError.Show();
                        return;
                    }

                    if (oDocumentoBE.TituloDocumento == "21PRE")
                    {
                        if (oDocumentoBE.Extension == ".jpg" || oDocumentoBE.Extension == ".jpeg")
                        { }
                        else
                        {
                            this.lblmessage.Text = "El archivo que desea subir no se encuentra configurado en el sistema para el documento seleccionado.";
                            this.mpeMostrarError.Show();
                            return;
                        }
                    }

                    if (oDocumentoBE.TituloDocumento == "36PRE")
                    {
                        if (oDocumentoBE.Extension == ".docx" || oDocumentoBE.Extension == ".doc")
                        { }
                        else
                        {
                            this.lblmessage.Text = "El archivo que desea subir no se encuentra configurado en el sistema para el documento seleccionado.";
                            this.mpeMostrarError.Show();
                            return;
                        }
                    }

                    if (oDocumentoBE.TituloDocumento != "21PRE" && oDocumentoBE.TituloDocumento != "36PRE")
                    {
                        if (oDocumentoBE.Extension != ".pdf")
                        {
                            this.lblmessage.Text = "El archivo que desea subir no se encuentra configurado en el sistema para el documento seleccionado.";
                            this.mpeMostrarError.Show();
                            return;
                        }
                    }

                    if (!ValidaAtributosArchivo(fuDocumento))
                    {
                        this.lblmessage.Text = "El archivo que desea subir no debe exceder de 2MB.";
                        this.mpeMostrarError.Show();
                        return;
                    }

                    oDocumentoBL = new DocumentoBL();
                    if (idDocumento > 0)
                    {
                        if (oDocumentoBL.PreformalizacionModificarDocumentoAplicante(oDocumentoBE))
                        {
                            oAplicanteBE = new AplicanteBE();
                            oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
                            oAplicanteBE.IdConfiguracionAplicacion = UIConstantes._IdPostulacionPreGrado;   //7

                            oDocumentoBL = new DocumentoBL();
                            String status = oDocumentoBL.GetStatusCargaDocs(UIConvertNull.Int32(Session["AplicanteId"]));
                            if (status.Equals("true"))
                            {
                                this.mpeCargaDocumentos.Hide();
                                this.LLenarGrillaDocumentos();
                                this.fuDocumento.Attributes.Clear();
                            }
                            else
                            {
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Se ha adjuntado el archivo con éxito');", true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (oDocumentoBL.PreformalizacionInsertarDocumentoAplicante(oDocumentoBE))
                        {
                            oAplicanteBE = new AplicanteBE();
                            oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
                            oAplicanteBE.IdConfiguracionAplicacion = UIConstantes._IdPostulacionPreGrado;

                            oDocumentoBL = new DocumentoBL();
                            String status = oDocumentoBL.GetStatusCargaDocs(UIConvertNull.Int32(Session["AplicanteId"]));
                            if (status.Equals("true"))
                            {
                                this.mpeCargaDocumentos.Hide();
                                this.LLenarGrillaDocumentos();
                                this.fuDocumento.Attributes.Clear();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Por favor ingresar un archivo.');", true);
                    this.lblmessage.Text = "Por favor ingresar un archivo.";
                    this.mpeMostrarError.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.LLenarGrillaDocumentos();
                this.mpeCargaDocumentos.Hide();
                this.upCargaDocumentos.Update();
                upResultado.Focus();
            }
        }

        private String ObtenerExtensionArchivo(FileUpload archivo)
        {
            if (archivo != null)
            {
                String fileName = Server.HtmlEncode(archivo.FileName);
                return System.IO.Path.GetExtension(fileName).ToLower();
            }
            return String.Empty;
        }

        private Int32 ObtenerTipoArchivo(String extension)
        {
            Int32 idTipoArchivo = 0;

            if (_extensionesValidas.Contains(extension))
            {
                Int32.TryParse(_extensionesValidas[extension].ToString(), out idTipoArchivo);
            }
            return idTipoArchivo;
        }

        private Boolean ValidaAtributosArchivo(FileUpload fuTempArchivo)
        {
            Boolean respuesta = true;
            if (fuTempArchivo.PostedFile.ContentLength > 2 * 1000000)
            {
                respuesta = false;
            }
            return respuesta;
        }

        private void ConfigurarTiposArchivos()
        {
            DocumentoBL oDocumentoBL = null;
            DataTable dtTiposArchivos = null;

            oDocumentoBL = new DocumentoBL();
            dtTiposArchivos = oDocumentoBL.ListaTiposArchivosPermitidos(UIConstantes._IdPostulacionPreGrado);
            _tiposArchivos = GetValidExtentions(dtTiposArchivos);
            SplitFileTypeExtensions(_tiposArchivos);
        }

        private Collection<AvailableFileType> GetValidExtentions(DataTable dtTiposArchivos)
        {
            Collection<AvailableFileType> fileTypes = new Collection<AvailableFileType>();

            foreach (DataRow drFila in dtTiposArchivos.Rows)
            {
                Int32 tempIdTipoMedio = 0;
                Int32.TryParse(drFila["MediaTypeId"].ToString(), out tempIdTipoMedio);
                String icono = drFila["Icon"].ToString();
                String extension = drFila["Extension"].ToString();
                String descripcion = drFila["MediaTypeDesc"].ToString();
                fileTypes.Add(new AvailableFileType(tempIdTipoMedio, icono, extension, descripcion));
            }

            return fileTypes;
        }

        private void SplitFileTypeExtensions(Collection<AvailableFileType> tipoArchivos)
        {
            String extention;
            foreach (AvailableFileType tipArch in tipoArchivos)
            {
                if (tipArch != null)
                {
                    extention = tipArch.FileExtension;
                    String[] extentions = extention.Split(';');
                    for (int x = 0; x < extentions.Length; x++)
                    {
                        extentions[x] = extentions[x].Replace("*", String.Empty);
                        extentions[x] = extentions[x].Replace(" ", String.Empty);

                        if (_extensionesValidas == null)
                        {
                            _extensionesValidas = new Hashtable();
                        }
                        if (!String.IsNullOrEmpty(extentions[x]) && !_extensionesValidas.ContainsKey(extentions[x].ToLower()))
                            _extensionesValidas.Add(extentions[x].ToLower(), tipArch.MediaTypeId);
                    }
                }
            }
        }

        //private string ObtenerDocumentosRequeridosNota()
        //{
        //    string rpta = "";
        //    DocumentoBL oDocumentoBL = new DocumentoBL();
        //    rpta = oDocumentoBL.ObtenerDocumentosRequeridosNota();
        //    divExplicacion.InnerHtml = rpta;
        //    return rpta;
        //}

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //UIHelper.SessionActiva(Page);
            try
            {
                this.mpeCargaDocumentos.Hide();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //****************************************
        private void cargarComboCarreras()
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtCarreras = null;
            Int32 SettingId = 7;
            DataTable dtModalidad = null;
            int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);

            

            try
            {
                oAplicanteBL = new AplicanteBL();
                dtModalidad = oAplicanteBL.ObtenerModalidadRegistrada(AplicanteId);
                Int32? IdModalidad = UIConvertNull.Int32(dtModalidad.Rows[0][0].ToString());
                dtCarreras = oAplicanteBL.ListarCarrerasPorModalidad(IdModalidad, SettingId);
                if (dtCarreras != null && dtCarreras.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlCarrera, dtCarreras.Copy(), "LONG_DESC", "ProgramOfStudyId", "-- Seleccionar --");
                    this.ddlCarrera.SelectedValue = dtModalidad.Rows[0][2].ToString();
                }
                else
                {
                    Funciones.cargarComboYSeleccione(this.ddlCarrera, null, "LONG_DESC", "ProgramOfStudyId", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oAplicanteBL = null;
                dtModalidad = null;
            }
        }        

        //Guardado de información
        protected void imgGuardarDatosPersonales_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.GuardarDatosPersonales();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F20, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        private void GuardarDatosPersonales()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            String claveTipPostul = String.Empty;
            
            if(UIConvertNull.Int32(Session["AplicanteId"]) != null && UIConvertNull.String(Session["usrRedId"]) != null)
            {
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
                    Boolean operacionOK = oAplicanteBL.PreformalizacionActualizarDatosPersonales(oAplicanteBE);
                    if (operacionOK)
                    {
                        this.lblmessage.Text = "Se registraron sus datos personales correctamente.";
                        this.mpeMostrarError.Show();
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
            else
            {
                this.lblmessage.Text = "No se pudo realizar la actualización, favor de volver a iniciar sesión.";
                this.mpeMostrarError.Show();
            }            
        }

        private AplicanteBE obtenerDatosPersonales(AplicanteBE oAplicanteBE)
        {
            //Este Valor se debe obtener de la sesion anterior y tambien nulo
            oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"].ToString());
            oAplicanteBE.RedId = Session["usrRedId"].ToString();
            oAplicanteBE.DireccionConcatenado = ddlTipoVia.SelectedValue + " " + txtDireccion.Text + " " + txtNumeracion.Text + " " + txtInterior.Text
                                                + " - " + ddlDistrito.SelectedItem + " - " + ddlProvincia.SelectedItem + " - " + ddlDpto.SelectedItem;

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

            return oAplicanteBE;
        }
        
        protected void imgGuardarDatosFamiliares_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.GuardarDatosFamiliares();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F20, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        private void GuardarDatosFamiliares()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            Int32 intExiste = 0;
            if (UIConvertNull.Int32(Session["AplicanteId"]) != null && UIConvertNull.String(Session["usrRedId"]) != null)
            {
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
                    Boolean operacionOK = oAplicanteBL.PreformalizacionActualizar_InfoPadres(oAplicanteBE);
                    if (operacionOK)
                    {
                        this.lblmessage.Text = "Se registraron los datos de su(s) familiar(es) correctamente.";
                        this.mpeMostrarError.Show();
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
            else
            {
                this.lblmessage.Text = "No se pudo realizar la actualización, favor de volver a iniciar sesión.";
                this.mpeMostrarError.Show();
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

        private void ObtenerExamenesXModalidad()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);
                DataTable dtExamenes = oGeneralBL.PreformalizacionObtenerExamenesXModalidad(AplicanteId);
                if (dtExamenes != null && dtExamenes.Rows.Count > 0)
                {
                    ListItem i;
                    foreach(DataRow r in dtExamenes.Rows)
                    {
                        i = new ListItem(r["EXAMEN"].ToString(), r["EXAMEN"].ToString());
                        string nomExamen = r["EXAMEN"].ToString();
                        this.chLstExamenes.Items.Add(i);
                        if (Convert.ToBoolean(r["ESTADO"]))
                        {
                            this.chLstExamenes.Items.FindByValue(nomExamen).Selected = true;
                        }
                    }

                    trPregExoneratorios.Attributes.CssStyle.Value = "display: ";
                    trChLstExoneratorios.Attributes.CssStyle.Value = "display: ";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerCursosConvalidar()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);
                DataTable dtCursos = oGeneralBL.PreformalizacionObtenerCursosConvalidar(AplicanteId);
                if (dtCursos != null && dtCursos.Rows.Count > 0)
                {
                    ListItem i;
                    foreach (DataRow r in dtCursos.Rows)
                    {
                        i = new ListItem(r["CURSO"].ToString(), r["CURSO"].ToString());
                        string nomCurso = r["CURSO"].ToString();
                        this.chLstCursosConv.Items.Add(i);
                        if (Convert.ToBoolean(r["ESTADO"]))
                        {
                            this.chLstCursosConv.Items.FindByValue(nomCurso).Selected = true;
                        }
                    }

                    trPregConvalidar.Attributes.CssStyle.Value = "display: ";
                    trChLstConvalidar.Attributes.CssStyle.Value = "display: ";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerDatosFinales()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);
                DataTable dtDatosFinales = oGeneralBL.PreformalizacionObtenerDatosFinales(AplicanteId);
                if (dtDatosFinales != null && dtDatosFinales.Rows.Count > 0)
                {
                    this.ddlCarrera.SelectedValue = dtDatosFinales.Rows[0][1].ToString();
                    this.ddlSeguroRenta.SelectedValue = (Convert.ToBoolean(dtDatosFinales.Rows[0][2]) ? "1" : "2");
                    this.ddlReservaMatricula.SelectedValue = (Convert.ToBoolean(dtDatosFinales.Rows[0][3]) ? "1" : "2");
                    this.ddlDeportistaDestacado.SelectedValue = (Convert.ToBoolean(dtDatosFinales.Rows[0][4]) ? "1" : "2");                    
                    Session["Flag_Preformalizar"] = Convert.ToBoolean(dtDatosFinales.Rows[0][5]);                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerAutorizacionDatos()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);
                DataTable dtAutorizacionDatos = oGeneralBL.PreformalizacionObtenerAutorizacionDatos(AplicanteId);
                if (dtAutorizacionDatos != null && dtAutorizacionDatos.Rows.Count > 0)
                {
                    this.chkUsoDatos.Checked = (Convert.ToInt32(dtAutorizacionDatos.Rows[0][0]) == 1 ? true : false);
                    this.chkUsoDatosTerceros.Checked = (Convert.ToInt32(dtAutorizacionDatos.Rows[0][1]) == 1 ? true : false);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private void ValidarPreformalizacion()
        {
            ObtenerEstadoPreformalizacion();
            
            if (Convert.ToBoolean(Session["Flag_Preformalizar"]))
            {
                this.imgGuardarDatosPersonales.Enabled = false;
                this.BtnAgregaPariente2.Enabled = false;
                this.BtnAgregaPariente3.Enabled = false;
                this.BtnOcultaPariente2.Enabled = false;
                this.BtnOcultaPariente3.Enabled = false;
                this.btnGuardarDatosFamiliares.Enabled = false;
                this.btnGuardarDatosFinales.Enabled = false;
                this.btnEnviarExpedientes.Enabled = false;

                this.imgGuardarDatosPersonales.Visible = false;
                this.BtnAgregaPariente2.Visible = false;
                this.BtnAgregaPariente3.Visible = false;
                this.BtnOcultaPariente2.Visible = false;
                this.BtnOcultaPariente3.Visible = false;
                this.btnGuardarDatosFamiliares.Visible = false;
                this.btnGuardarDatosFinales.Visible = false;
                this.btnEnviarExpedientes.Visible = false;

                BloquearCursosYExamenes();

                //INI: JC.DelgadoV[RQ103950] Observaciones Pre Formalización
                //Datos finales deshabilitando combos
                ddlCarrera.Enabled = false;
                ddlSeguroRenta.Enabled = false;
                ddlReservaMatricula.Enabled = false;
                ddlDeportistaDestacado.Enabled = false;

                //****
                this.imgActualizarCantidadCompetencias.Visible = false;
                this.btnEditarNotasQuintoLetra.Visible = false;
                this.imgGuardarDetalleCompRend.Visible = false;

                //Detalle Competencias deshabilitando combos
                ddlColegioQuinto.Enabled = false;
                ddlAnioLectivoQuinto.Enabled = false;
                ddlTipoCalificacionQuinto.Enabled = false;
                //FIN: JC.DelgadoV[RQ103950] Observaciones Pre Formalización

                //CheckboxUsoDeDatos
                chkUsoDatos.Enabled = false;
                chkUsoDatosTerceros.Enabled = false;

                LLenarGrillaDocumentos();


                if (Convert.ToBoolean(Session["TieneNotasRendimientoAcademico"]))
                {
                    MostrarDetalleRendimientoAcademico();
                    CargarDetalleRendimientoAcademico(Convert.ToInt32(Session["AplicanteId"]));
                }
            }
        }

        private void BloquearCursosYExamenes()
        {
            //Cursos
            BloquearCursos();

            //Examenes
            BloquearExamenes();
        }

        private void BloquearCursos() 
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);
                DataTable dtCursos = oGeneralBL.PreformalizacionObtenerCursosConvalidar(AplicanteId);
                if (dtCursos != null && dtCursos.Rows.Count > 0)
                {
                    foreach (DataRow r in dtCursos.Rows)
                    {
                        string nomCurso = r["CURSO"].ToString();
                        this.chLstCursosConv.Items.FindByValue(nomCurso).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void BloquearExamenes() 
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);
                DataTable dtExamenes = oGeneralBL.PreformalizacionObtenerExamenesXModalidad(AplicanteId);
                if (dtExamenes != null && dtExamenes.Rows.Count > 0)
                {
                    foreach (DataRow r in dtExamenes.Rows)
                    {
                        string nomExamen = r["EXAMEN"].ToString();
                        this.chLstExamenes.Items.FindByValue(nomExamen).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerEstadoPreformalizacion()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);
                DataTable dtDatosFinales = oGeneralBL.PreformalizacionObtenerEstado(AplicanteId);
                if (dtDatosFinales != null && dtDatosFinales.Rows.Count > 0)
                {
                    this.lblEstadoFormalizacion.Text = dtDatosFinales.Rows[0][0].ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        protected void imgGuardarDatosFinales_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.GuardarDatosFinales();                
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F20, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        private void GuardarDatosFinales()
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;

            if (UIConvertNull.Int32(Session["AplicanteId"]) != null && UIConvertNull.String(Session["usrRedId"]) != null)
            {
                try
                {
                    oAplicanteBE = new AplicanteBE();
                    oAplicanteBE = obtenerDatosFinales(oAplicanteBE);

                    oAplicanteBL = new AplicanteBL();
                    Boolean operacionOK = oAplicanteBL.PreformalizacionGuardarDatosFinales(oAplicanteBE);

                    //Guardando la encuesta
                    //operacionOK = this.GuardarEncuesta();
                    //*********************

                    if (operacionOK)
                    {
                        this.lblmessage.Text = "Se registraron los datos finales correctamente.";
                        this.mpeMostrarError.Show();
                        ObtenerDatosFinales();
                        ObtenerAutorizacionDatos();
                        //LlenarEncuesta();
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
            else
            {
                this.lblmessage.Text = "No se pudo realizar la actualización, favor de volver a iniciar sesión.";
                this.mpeMostrarError.Show();
            }
        }

        private AplicanteBE obtenerDatosFinales(AplicanteBE oAplicanteBE)
        {
            oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"].ToString());
            oAplicanteBE.RedId = Session["usrRedId"].ToString();
            oAplicanteBE.CambioCarrera = UIConvertNull.Int32(ddlCarrera.SelectedValue);
            oAplicanteBE.SeguroRentaEstudiantil = UIConvertNull.Int32(ddlSeguroRenta.SelectedValue);
            oAplicanteBE.ReservaMatricula = UIConvertNull.Int32(ddlReservaMatricula.SelectedValue);
            oAplicanteBE.DeportistaDestacado = UIConvertNull.Int32(ddlDeportistaDestacado.SelectedValue);

            //Obteniendo los exámenes elegidos
            if (oAplicanteBE.LExamenFormalizacion == null)
            {
                oAplicanteBE.LExamenFormalizacion = new List<ExamenFormalizacionBE>();
            }    
            
            foreach (ListItem examen in chLstExamenes.Items)
            {
                if (examen.Selected == true)
                {
                    ExamenFormalizacionBE examenFormalizacion = new ExamenFormalizacionBE();
                    examenFormalizacion.NomExamen = examen.Value.ToString();
                    examenFormalizacion.Estado = true;
                    oAplicanteBE.LExamenFormalizacion.Add(examenFormalizacion);
                }
                else
                {
                    ExamenFormalizacionBE examenFormalizacion = new ExamenFormalizacionBE();
                    examenFormalizacion.NomExamen = examen.Value.ToString();
                    examenFormalizacion.Estado = false;
                    oAplicanteBE.LExamenFormalizacion.Add(examenFormalizacion);
                }
            }
            //********************************

            //Obteniendo los cursos a convalidar
            if (oAplicanteBE.LCursoConvalidacionFormalizacion == null)
            {
                oAplicanteBE.LCursoConvalidacionFormalizacion = new List<CursoConvalidacionFormalizacionBE>();
            }

            foreach (ListItem curso in chLstCursosConv.Items)
            {
                if (curso.Selected == true)
                {
                    CursoConvalidacionFormalizacionBE cursoFormalizacion = new CursoConvalidacionFormalizacionBE();
                    cursoFormalizacion.NomCursoConvalidacion = curso.Value.ToString();
                    cursoFormalizacion.Estado = true;
                    oAplicanteBE.LCursoConvalidacionFormalizacion.Add(cursoFormalizacion);
                }
                else
                {
                    CursoConvalidacionFormalizacionBE cursoFormalizacion = new CursoConvalidacionFormalizacionBE();
                    cursoFormalizacion.NomCursoConvalidacion = curso.Value.ToString();
                    cursoFormalizacion.Estado = false;
                    oAplicanteBE.LCursoConvalidacionFormalizacion.Add(cursoFormalizacion);
                }
            }
            //********************************

            //Obteniendo autorización de datos
            oAplicanteBE.Autorizacion = UIConvertNull.Int32(chkUsoDatos.Checked ? 1 : 0);
            oAplicanteBE.AutorizacionTerceros = UIConvertNull.Int32(chkUsoDatosTerceros.Checked ? 1 : 0);
            //********************************

            return oAplicanteBE;
        }

        protected void btnEnviarExpedientes_Click(object sender, ImageClickEventArgs e)
        {
            

            try
            {                
                this.GuardarDatosPersonales();
                this.GuardarDatosFamiliares();

                //INI: JC.DelgadoV[RQ103950] Observaciones Pre Formalización
                if (Convert.ToBoolean(Session["TieneNotasRendimientoAcademico"]))
                {

                    string resultadoDetalleCompetencias = ValidarDetalleCompetencias();

                    if (resultadoDetalleCompetencias == "")
                    {
                        this.GuardarCantidadCompetencias();
                        this.GuardarDetalleCompetenciasFormalizacion();
                    }
                    else
                    {
                        this.lblmessage.Text = resultadoDetalleCompetencias;
                        this.mpeMostrarError.Show();
                        return;
                    }
                }
                //FIN: JC.DelgadoV[RQ103950] Observaciones Pre Formalización

                this.GuardarDatosFinales();
                this.GuardarFormalizacion();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F20, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }

        }

        private void GuardarFormalizacion()
        {
            if (UIConvertNull.Int32(Session["AplicanteId"]) != null && UIConvertNull.String(Session["usrRedId"]) != null)
            {
                AplicanteBE oAplicanteBE = null;
                AplicanteBL oAplicanteBL = null;

                try
                {
                    oAplicanteBE = new AplicanteBE();
                    oAplicanteBE = obtenerDatosPersonales(oAplicanteBE);
                    oAplicanteBL = new AplicanteBL();
                    Boolean operacionOK = oAplicanteBL.PreformalizacionGuardarFormalizacion(oAplicanteBE);
                    if (operacionOK)
                    {
                        this.lblmessage.Text = "Se envió su expediente correctamente al área de Admisión. En breve le llegará un correo de confirmación.";
                        this.mpeMostrarError.Show();
                        ObtenerDatosFinales();
                        ValidarPreformalizacion();
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
            else
            {
                this.lblmessage.Text = "La sesión caducó, favor de volver a iniciar sesión.";
                this.mpeMostrarError.Show();
            }
        }

        //Fin: JC.DelgadoV - Formalización [20210120]

        //INI: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRC)
        private void CargarEncuesta()
        {
            GeneralBL oGeneralBL = null;
            int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);
            DataTable dtPreguntas = null;
            DataTable dtAlternativas = null;

            try
            {
                oGeneralBL = new GeneralBL();
                DataSet dsEncuesta = oGeneralBL.PreformalizacionObtenerEncuesta(AplicanteId);

                if(dsEncuesta != null && dsEncuesta.Tables.Count == 2)
                {
                    //Asignando las tablas
                    dtPreguntas = dsEncuesta.Tables[0];
                    dtAlternativas = dsEncuesta.Tables[1];

                    //Llenando las preguntas
                    CargarPreguntasEncuestaRGC(dtPreguntas);

                    //Llenando las alternativas
                    CargarAlternativasEncuestaRGC(dtAlternativas);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private void CargarPreguntasEncuestaRGC(DataTable dtPreguntas)
        {
            //Preguntas
            DataRow row_pregunta_1 = dtPreguntas.AsEnumerable().Where(p => p.Field<Int32>("IdPregunta") == 1).FirstOrDefault();
            DataRow row_pregunta_2 = dtPreguntas.AsEnumerable().Where(p => p.Field<Int32>("IdPregunta") == 2).FirstOrDefault();
            DataRow row_pregunta_3 = dtPreguntas.AsEnumerable().Where(p => p.Field<Int32>("IdPregunta") == 3).FirstOrDefault();
            DataRow row_pregunta_4 = dtPreguntas.AsEnumerable().Where(p => p.Field<Int32>("IdPregunta") == 4).FirstOrDefault();
            DataRow row_pregunta_5 = dtPreguntas.AsEnumerable().Where(p => p.Field<Int32>("IdPregunta") == 5).FirstOrDefault();
            DataRow row_pregunta_6 = dtPreguntas.AsEnumerable().Where(p => p.Field<Int32>("IdPregunta") == 6).FirstOrDefault();


            encuestaRGC_P1.InnerHtml = row_pregunta_1.ItemArray[1].ToString();
            encuestaRGC_P2.InnerHtml = row_pregunta_2.ItemArray[1].ToString();
            encuestaRGC_P3.InnerHtml = row_pregunta_3.ItemArray[1].ToString();
            encuestaRGC_P4.InnerHtml = row_pregunta_4.ItemArray[1].ToString();
            encuestaRGC_P5.InnerHtml = row_pregunta_5.ItemArray[1].ToString();
            encuestaRGC_P6.InnerHtml = row_pregunta_6.ItemArray[1].ToString();
        }

        private void CargarAlternativasEncuestaRGC(DataTable dtAlternativas)
        {
            //Alternativa1
            ddlEncuestaRGC_R1.DataSource = CargarComboEncuesta(dtAlternativas, 1);
            ddlEncuestaRGC_R1.DataBind();

            //Alternativa2
            ddlEncuestaRGC_R2.DataSource = CargarComboEncuesta(dtAlternativas, 2);
            ddlEncuestaRGC_R2.DataBind();

            //Alternativa3
            ddlEncuestaRGC_R3.DataSource = CargarComboEncuesta(dtAlternativas, 3);
            ddlEncuestaRGC_R3.DataBind();

            //Alternativa5
            ddlEncuestaRGC_R5.DataSource = CargarComboEncuesta(dtAlternativas, 5);
            ddlEncuestaRGC_R5.DataBind();

            //Alternativa6
            ddlEncuestaRGC_R6.DataSource = CargarComboEncuesta(dtAlternativas, 6);
            ddlEncuestaRGC_R6.DataBind();
        }

        private DataTable CargarComboEncuesta(DataTable dtAlternativas, int pregunta)
        {
            //Obteniendo las alternativas según la pregunta
            var alternativas = from r in dtAlternativas.AsEnumerable()
                                 where r.Field<Int32>("IdPregunta") == pregunta
                               select r;
            //*********************************************

            DataTable dt = new DataTable();

            // Variables para las columnas y las filas
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "codigo";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "descripcion";
            dt.Columns.Add(column);
            //

            //Agregando la opción -- Seleccionar --
            row = dt.NewRow();

            row["codigo"] = 0;

            row["descripcion"] = "-- Seleccionar --";

            dt.Rows.Add(row);
            //

            foreach (var item in alternativas)
            {
                row = dt.NewRow();

                row["codigo"] = item.ItemArray[0];

                row["descripcion"] = item.ItemArray[2].ToString();

                dt.Rows.Add(row);
            }

            return dt;

        }

        protected void ddlEncuestaRGC_R3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int respuestaP3 = Convert.ToInt32(ddlEncuestaRGC_R3.SelectedValue);

            ddlEncuestaRGC_R5.SelectedValue = "0";
            ddlEncuestaRGC_R6.SelectedValue = "0";

            if (respuestaP3 == 6)
            {
                trP5.Visible = true;
                trP6.Visible = true;

                trA5.Visible = true;
                trA6.Visible = true;
            }
            else
            {
                trP5.Visible = false;
                trP6.Visible = false;

                trA5.Visible = false;
                trA6.Visible = false;
            }

            ddlEncuestaRGC_R3.Focus();
        }

        private void ComprobarEncuesta()
        {
            GeneralBL oGeneralBL = null;
            int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);

            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtEstadoEncuesta = oGeneralBL.PreformalizacionComprobarEncuesta(AplicanteId);
                bool estadoEncuesta = Convert.ToBoolean(dtEstadoEncuesta.Rows[0]["Flag_EncuestaRGC"]);

                if (estadoEncuesta)
                {
                    //LlenarEncuesta();                    
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void LlenarEncuesta()
        {
            GeneralBL oGeneralBL = null;
            int AplicanteId = Convert.ToInt32(Session["AplicanteId"]);

            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtEncuestaAplicante = oGeneralBL.PreformalizacionLlenarEncuesta(AplicanteId);

                if (dtEncuestaAplicante != null && dtEncuestaAplicante.Rows.Count > 0)
                {
                    int encuestaRGC_ResP1 = Convert.ToInt32(dtEncuestaAplicante.Rows[0]["EncuestaRGC_ResP1"]);
                    int encuestaRGC_ResP2 = Convert.ToInt32(dtEncuestaAplicante.Rows[0]["EncuestaRGC_ResP2"]);
                    int encuestaRGC_ResP3 = Convert.ToInt32(dtEncuestaAplicante.Rows[0]["EncuestaRGC_ResP3"]);
                    string encuestaRGC_ResP4 = dtEncuestaAplicante.Rows[0]["EncuestaRGC_ResP4"].ToString();
                    int encuestaRGC_ResP5 = Convert.ToInt32(dtEncuestaAplicante.Rows[0]["EncuestaRGC_ResP5"]);
                    int encuestaRGC_ResP6 = Convert.ToInt32(dtEncuestaAplicante.Rows[0]["EncuestaRGC_ResP6"]);

                    ddlEncuestaRGC_R1.SelectedValue = encuestaRGC_ResP1.ToString();
                    ddlEncuestaRGC_R2.SelectedValue = encuestaRGC_ResP2.ToString();
                    ddlEncuestaRGC_R3.SelectedValue = encuestaRGC_ResP3.ToString();
                    txtEncuestaRGC_R4.Text = encuestaRGC_ResP4.ToString();
                    ddlEncuestaRGC_R5.SelectedValue = encuestaRGC_ResP5.ToString();
                    ddlEncuestaRGC_R6.SelectedValue = encuestaRGC_ResP6.ToString();

                    //Validaciones
                    //No editable
                    ddlEncuestaRGC_R1.Enabled = false;
                    ddlEncuestaRGC_R2.Enabled = false;
                    ddlEncuestaRGC_R3.Enabled = false;
                    txtEncuestaRGC_R4.Enabled = false;
                    ddlEncuestaRGC_R5.Enabled = false;
                    ddlEncuestaRGC_R6.Enabled = false;

                    //Visibilidad de la P5 y P6
                    if(encuestaRGC_ResP3 == 6)
                    {
                        trP5.Visible = true;
                        trP6.Visible = true;

                        trA5.Visible = true;
                        trA6.Visible = true;
                    }
                    else
                    {
                        trP5.Visible = false;
                        trP6.Visible = false;

                        trA5.Visible = false;
                        trA6.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private Boolean GuardarEncuesta()
        {
            bool operacionOK = true;
            if (!Convert.ToBoolean(Session["Flag_EncuestaRGC"]))
            {
                AplicanteBE oAplicanteBE = null;
                GeneralBL oGeneralBL = null;

                oAplicanteBE = new AplicanteBE();
                oAplicanteBE = obtenerRespuestasEncuesta(oAplicanteBE);

                try
                {
                    oGeneralBL = new GeneralBL();
                    DataTable dtGuardarEncuestaResultado = oGeneralBL.PreformalizacionGuardarEncuesta(oAplicanteBE);

                    int resultado = Convert.ToInt32(dtGuardarEncuestaResultado.Rows[0]["Resultado"]);
                    
                    if(resultado == 1)
                    {
                        operacionOK = true;
                    }
                    else
                    {
                        operacionOK = false;
                    }

                }
                catch (Exception ex)
                {
                    operacionOK = false;
                    throw ex;
                }

            }

            return operacionOK;
        }

        private AplicanteBE obtenerRespuestasEncuesta(AplicanteBE oAplicanteBE)
        {
            oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"].ToString());
            oAplicanteBE.EncuestaRGC_ResP1 = UIConvertNull.Int32(ddlEncuestaRGC_R1.SelectedValue);
            oAplicanteBE.EncuestaRGC_ResP2 = UIConvertNull.Int32(ddlEncuestaRGC_R2.SelectedValue);
            oAplicanteBE.EncuestaRGC_ResP3 = UIConvertNull.Int32(ddlEncuestaRGC_R3.SelectedValue);
            oAplicanteBE.EncuestaRGC_ResP4 = txtEncuestaRGC_R4.Text;
            oAplicanteBE.EncuestaRGC_ResP5 = UIConvertNull.Int32(ddlEncuestaRGC_R5.SelectedValue);
            oAplicanteBE.EncuestaRGC_ResP6 = UIConvertNull.Int32(ddlEncuestaRGC_R6.SelectedValue);

            return oAplicanteBE;
        }
        //FIN: JC.DelgadoV[RQ103036] Encuesta Retorno a clases(class: encuestaRC)

        //INI: JC.DelgadoV[RQ103950] Observaciones Pre Formalización
        private void MostrarDetalleRendimientoAcademico()
        {
            trDetalleCompetencias.Attributes.Add("style", "display:table-row");
        }

        private void CargarDetalleRendimientoAcademico(int? AplicanteId)
        {
            String EsBachillerato = "2";
            AplicanteBE oAplicanteBE = new AplicanteBL().ObtenerRendAcademicoRegistrado(AplicanteId);

            Session["SituacionAcademica"] = oAplicanteBE.LDetalleEducacion[0].SituaAcademica.Value;

            if (Session["SituacionAcademica"].ToString() == "34" || Session["SituacionAcademica"].ToString() == "9")
            {
                if (Session["SituacionAcademica"].ToString() == "9")
                {
                    trCuadroCompetencias.Visible = true;
                    CargarCantidadCompetencias(AplicanteId);
                }

                this.MostrarOcultarControles(EsBachillerato);
                this.CargarCombosRedimientoDetalle();
                this.CargarComboTipoCalificacion(oAplicanteBE.LDetalleEducacion[0].SituaAcademica.Value);
                this.HabilitaControles();
                this.CargarRendimientoAcademicoRegistrado(oAplicanteBE);
                this.CargarAnioAcademico(AplicanteId);

                DeshabilitarCombosSituacionAcademica();

                if (Session["ModPostulacion"].ToString() == "50")
                {
                    OcultarOrdenMerito();
                }
            }
            
        }

        private void OcultarOrdenMerito()
        {
            trOrdenMeritoTercero.Visible = false;
            trNroAlumnosTercero.Visible = false;

            trOrdenMeritoCuarto.Visible = false;
            trNroAlumnosCuarto.Visible = false;

            trOrdenMeritoQuinto.Visible = false;
            trNroAlumnosQuinto.Visible = false;
        }

        private void DeshabilitarCombosSituacionAcademica()
        {
            ddlColegioTercero.Enabled = false;
            ddlColegioCuarto.Enabled = false;

            ddlAnioLectivoTercero.Enabled = false;
            ddlAnioLectivoCuarto.Enabled = false;

            ddlTipoCalificacionTercero.Enabled = false;
            ddlTipoCalificacionCuarto.Enabled = false;

            if (Session["SituacionAcademica"].ToString() == "34")
            {
                ddlColegioQuinto.Enabled = false;
                ddlAnioLectivoQuinto.Enabled = false;
                ddlTipoCalificacionQuinto.Enabled = false;

                btnEditarNotasCuartoNumerico.Visible = false;
                btnEditarNotasQuintoNumerico.Visible = false;
                imgGuardarDetalleCompRend.Visible = false;
            }
        }

        private void MostrarOcultarControles(String TipoColegio)
        {
            switch (TipoColegio)
            {
                case UIConstantes.TipoColegio._Bachillerato:
                    {
                        lblGrado1.Text = UIConstantes.Grado._Cuarto;
                        lblGrado2.Text = UIConstantes.Grado._Quinto;
                        lblGrado3.Text = UIConstantes.Grado._Sexto;
                        lblGrado11.Text = UIConstantes.Grado._Cuarto;
                        lblGrado22.Text = UIConstantes.Grado._Quinto;
                        lblGrado33.Text = UIConstantes.Grado._Sexto;
                        break;
                    }
                case UIConstantes.TipoColegio._Normal:
                    {
                        lblGrado1.Text = UIConstantes.Grado._Tercero;
                        lblGrado2.Text = UIConstantes.Grado._Cuarto;
                        lblGrado3.Text = UIConstantes.Grado._Quinto;
                        lblGrado11.Text = UIConstantes.Grado._Tercero;
                        lblGrado22.Text = UIConstantes.Grado._Cuarto;
                        lblGrado33.Text = UIConstantes.Grado._Quinto;
                        break;
                    }
            }
        }

        private void CargarCombosRedimientoDetalle()
        {
            this.CargarComboAnioTercero();
            this.CargarComboAnioCuarto();
            this.CargarComboAnioQuinto();
            this.CargarComboNotas();
            this.CargarComboColegios();
            this.CargarCombosPopupNotasLetra(); /*Agrega:Christian Ramirez -REQ91569*/
        }

        private void CargarComboAnioTercero()
        {
            GeneralBL oGeneralBL = null;
            String strGrado = "3";
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtAnnio = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.ANNIO].Key, strGrado, UIConvertNull.Int32(Session["ModPostulacion"]));
                if (dtAnnio != null && dtAnnio.Rows.Count > 0)
                {
                    //Funciones.cargarComboYSeleccione(this.ddlAnioLectivoQuinto, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                    //Funciones.cargarComboYSeleccione(this.ddlAnioLectivoCuarto, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlAnioLectivoTercero, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                }
                this.ddlAnioLectivoTercero.SelectedValue = UIConvertNull.String(DateTime.Today.Year - 2);
                //this.ddlAnioLectivoCuarto.SelectedValue = UIConvertNull.String(DateTime.Today.Year - 1);
                //this.ddlAnioLectivoQuinto.SelectedValue = UIConvertNull.String(DateTime.Today.Year);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboAnioCuarto()
        {
            GeneralBL oGeneralBL = null;
            String strGrado = "4";
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtAnnio = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.ANNIO].Key, strGrado, UIConvertNull.Int32(Session["ModPostulacion"]));
                if (dtAnnio != null && dtAnnio.Rows.Count > 0)
                {
                    //Funciones.cargarComboYSeleccione(this.ddlAnioLectivoQuinto, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlAnioLectivoCuarto, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                    //Funciones.cargarComboYSeleccione(this.ddlAnioLectivoTercero, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                }
                //this.ddlAnioLectivoQuinto.SelectedValue = UIConvertNull.String(DateTime.Today.Year);
                this.ddlAnioLectivoCuarto.SelectedValue = UIConvertNull.String(DateTime.Today.Year - 1);
                //this.ddlAnioLectivoTercero.SelectedValue = UIConvertNull.String(DateTime.Today.Year - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboAnioQuinto()
        {
            GeneralBL oGeneralBL = null;
            String strGrado = "5";
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtAnnio = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.ANNIO].Key, strGrado, UIConvertNull.Int32(Session["ModPostulacion"]));
                if (dtAnnio != null && dtAnnio.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlAnioLectivoQuinto, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                    //Funciones.cargarComboYSeleccione(this.ddlAnioLectivoCuarto, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                    //Funciones.cargarComboYSeleccione(this.ddlAnioLectivoTercero, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                }
                this.ddlAnioLectivoQuinto.SelectedValue = UIConvertNull.String(DateTime.Today.Year);
                //this.ddlAnioLectivoCuarto.SelectedValue = UIConvertNull.String(DateTime.Today.Year - 1);
                //this.ddlAnioLectivoTercero.SelectedValue = UIConvertNull.String(DateTime.Today.Year - 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarComboNotas()
        {
            DataTable dt = null;
            dt = UIHelper.Tabla_Notas();
            if (dt != null && dt.Rows.Count > 0)
            {
                /*Ini:Christian Ramirez -REQ91569*/
                Funciones.cargarComboYSeleccione(this.ddlNotaMatePopup, dt.Copy(), "Nombre", "IdCodigo", "-- Seleccionar --");
                //Funciones.cargarComboYSeleccione(this.ddlNotaMateCuartoPopup, dt.Copy(), "Nombre", "IdCodigo", "-- Seleccionar --");
                //Funciones.cargarComboYSeleccione(this.ddlNotaMateQuintoPopup, dt.Copy(), "Nombre", "IdCodigo", "-- Seleccionar --");

                Funciones.cargarComboYSeleccione(this.ddlNotaLengPopup, dt.Copy(), "Nombre", "IdCodigo", "-- Seleccionar --");
                //Funciones.cargarComboYSeleccione(this.ddlNotaLengCuartoPopup, dt.Copy(), "Nombre", "IdCodigo", "-- Seleccionar --");
                //Funciones.cargarComboYSeleccione(this.ddlNotaLengQuintoPopup, dt.Copy(), "Nombre", "IdCodigo", "-- Seleccionar --");
                /*Fin:Christian Ramirez -REQ91569*/
            }

            #region comentado
            // a Pedido de Cesar Ruiz RQ 89808
            //this.ddlNotaMatePopup.Items.Insert(11, new ListItem("C", "C"));
            //this.ddlNotaMateCuartoPopup.Items.Insert(11, new ListItem("C", "C"));
            //this.ddlNotaMateQuintoPopup.Items.Insert(11, new ListItem("C", "C"));
            //this.ddlNotaMatePopup.Items.Insert(11, new ListItem("B", "B"));
            //this.ddlNotaMateCuartoPopup.Items.Insert(11, new ListItem("B", "B"));
            //this.ddlNotaMateQuintoPopup.Items.Insert(11, new ListItem("B", "B"));
            //this.ddlNotaMatePopup.Items.Insert(11, new ListItem("A", "A"));
            //this.ddlNotaMateCuartoPopup.Items.Insert(11, new ListItem("A", "A"));
            //this.ddlNotaMateQuintoPopup.Items.Insert(11, new ListItem("A", "A"));
            //this.ddlNotaMatePopup.Items.Insert(11, new ListItem("AD", "AD"));
            //this.ddlNotaMateCuartoPopup.Items.Insert(11, new ListItem("AD", "AD"));
            //this.ddlNotaMateQuintoPopup.Items.Insert(11, new ListItem("AD", "AD"));

            //this.ddlNotaLengPopup.Items.Insert(11, new ListItem("C", "C"));
            //this.ddlNotaLengCuartoPopup.Items.Insert(11, new ListItem("C", "C"));
            //this.ddlNotaLengQuintoPopup.Items.Insert(11, new ListItem("C", "C"));
            //this.ddlNotaLengPopup.Items.Insert(11, new ListItem("B", "B"));
            //this.ddlNotaLengCuartoPopup.Items.Insert(11, new ListItem("B", "B"));
            //this.ddlNotaLengQuintoPopup.Items.Insert(11, new ListItem("B", "B"));
            //this.ddlNotaLengPopup.Items.Insert(11, new ListItem("A", "A"));
            //this.ddlNotaLengCuartoPopup.Items.Insert(11, new ListItem("A", "A"));
            //this.ddlNotaLengQuintoPopup.Items.Insert(11, new ListItem("A", "A"));
            //this.ddlNotaLengPopup.Items.Insert(11, new ListItem("AD", "AD"));
            //this.ddlNotaLengCuartoPopup.Items.Insert(11, new ListItem("AD", "AD"));
            //this.ddlNotaLengQuintoPopup.Items.Insert(11, new ListItem("AD", "AD"));


            //Se comenta a Solicitud de Diana Farias 24514
            //this.ddlNotaMateTercero.Items.Insert(11, new ListItem(UIConstantes._TextoNotaOtro, UIConstantes._valorNotaOtro));
            //this.ddlNotaMateCuarto.Items.Insert(11, new ListItem(UIConstantes._TextoNotaOtro, UIConstantes._valorNotaOtro));
            //this.ddlNotaMateQuinto.Items.Insert(11, new ListItem(UIConstantes._TextoNotaOtro, UIConstantes._valorNotaOtro));

            //this.ddlNotaLengTercero.Items.Insert(11, new ListItem(UIConstantes._TextoNotaOtro, UIConstantes._valorNotaOtro));
            //this.ddlNotaLengCuarto.Items.Insert(11, new ListItem(UIConstantes._TextoNotaOtro, UIConstantes._valorNotaOtro));
            //this.ddlNotaLengQuinto.Items.Insert(11, new ListItem(UIConstantes._TextoNotaOtro, UIConstantes._valorNotaOtro));

            //this.ddlNotaPromTercero.Items.Insert(11, new ListItem(UIConstantes._TextoNotaOtro, UIConstantes._valorNotaOtro));
            //this.ddlNotaPromCuarto.Items.Insert(11, new ListItem(UIConstantes._TextoNotaOtro, UIConstantes._valorNotaOtro));
            //this.ddlNotaPromQuinto.Items.Insert(11, new ListItem(UIConstantes._TextoNotaOtro, UIConstantes._valorNotaOtro));
            #endregion
        }

        private void CargarComboColegios()
        {
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                DataTable dtColegios = oAplicanteBL.LLenarColegioRegistradoCombo(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                if (dtColegios != null && dtColegios.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlColegioTercero, dtColegios.Copy(), "InstitutionName", "ApplicationEducationId", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlColegioCuarto, dtColegios.Copy(), "InstitutionName", "ApplicationEducationId", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlColegioQuinto, dtColegios.Copy(), "InstitutionName", "ApplicationEducationId", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCombosPopupNotasLetra()
        {
            RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
            DataTable dtCalificacion = oRendimientoAcademicoBL.ObtenerListaCalificacionRendimientoAcademico();

            if (dtCalificacion != null && dtCalificacion.Rows.Count > 0)
            {
                Funciones.cargarComboYSeleccione(CT01Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(CT02Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(CT03Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");

                Funciones.cargarComboYSeleccione(CS01Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(CS02Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(CS03Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");

                Funciones.cargarComboYSeleccione(CTR01Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(CTR02Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");

                Funciones.cargarComboYSeleccione(CL01Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(CL02Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(CL03Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");

                Funciones.cargarComboYSeleccione(MA01Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(MA02Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(MA03Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
                Funciones.cargarComboYSeleccione(MA04Popup, dtCalificacion, "Calificacion", "IdCodigo", "-- Seleccionar --");
            }

        }

        private void CargarComboTipoCalificacion(int situacionAcademica)
        {
            ddlTipoCalificacionTercero.Items.Clear();
            ddlTipoCalificacionCuarto.Items.Clear();
            ddlTipoCalificacionQuinto.Items.Clear();

            if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.EGRESADO)
            {
                ddlTipoCalificacionTercero.Items.AddRange(new ListItem[] {
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR]
                    },
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO]
                    }

                });

                ddlTipoCalificacionCuarto.Items.AddRange(new ListItem[] {
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR]
                    },
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO]
                    }

                });

                ddlTipoCalificacionQuinto.Items.AddRange(new ListItem[] {
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR]
                    },
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO]
                    }

                });
            }

            if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
            {
                ddlTipoCalificacionTercero.Items.AddRange(new ListItem[] {
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR]
                    },
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO]
                    }

                });

                ddlTipoCalificacionCuarto.Items.AddRange(new ListItem[] {
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR]
                    },
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS]
                    }

                });

                ddlTipoCalificacionQuinto.Items.AddRange(new ListItem[] {
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR]
                    },
                    new ListItem()
                    {
                        Value = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS.ToString("D"),
                        Text = UIConstantes.ObtenerRendAcademicoTipoCalificacion()[UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS]
                    }

                });
            }


            ddlTipoCalificacionTercero.SelectedIndex = 0;
            ddlTipoCalificacionCuarto.SelectedIndex = 0;
            ddlTipoCalificacionQuinto.SelectedIndex = 0;
        }

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

        private void CargarRendimientoAcademicoRegistrado(AplicanteBE oAplicanteBE)
        {
            /*Ini:Christian Ramirez -REQ91569*/
            try
            {
                //oAplicanteBL = new AplicanteBL();
                //oAplicanteBE = oAplicanteBL.ObtenerRendAcademicoRegistrado(AplicanteId);
                this.LLenarDatosRendimeintoAcademicoRegistrado(oAplicanteBE);
                this.LLenarDatosNotasRegistradas(oAplicanteBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            /*Fin:Christian Ramirez -REQ91569*/
        }

        private void LLenarDatosRendimeintoAcademicoRegistrado(AplicanteBE oAplicanteBE)
        {
            try
            {
                if (oAplicanteBE != null && oAplicanteBE.LDetalleEducacion != null)
                {
                    List<EducacionDetalleBE> LRendAcademico = (from itemEduca in oAplicanteBE.LDetalleEducacion
                                                               orderby itemEduca.IdDetalleEducacion
                                                               select itemEduca).ToList<EducacionDetalleBE>();

                    if (LRendAcademico != null)
                    {
                        for (int indice = 0; indice < LRendAcademico.Count; indice++)
                        {
                            /*Ini:Christian Ramirez -REQ91569*/
                            EducacionDetalleBE recEducacionDetalle = LRendAcademico[indice];
                            string codTipoCalificacion = recEducacionDetalle.CodTipoCalificacion.ToString(); //

                            #region Valores Iniciales
                            if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.TERCERO_SECUNDARIA.ToString("D"))
                            {
                                this.txtIdApplicationEducationEnrollTercero.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                                this.ddlColegioTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                                this.ddlAnioLectivoTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                                this.ddlTipoCalificacionTercero.SelectedValue = recEducacionDetalle.CodTipoCalificacion.ToString();
                            }

                            if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.CUARTO_SECUNDARIA.ToString("D"))
                            {
                                this.txtIdApplicationEducationEnrollCuarto.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                                this.ddlColegioCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                                this.ddlAnioLectivoCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                                this.ddlTipoCalificacionCuarto.SelectedValue = recEducacionDetalle.CodTipoCalificacion.ToString();
                            }

                            if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.QUINTO_SECUNDARIA.ToString("D"))
                            {
                                this.txtIdApplicationEducationEnrollQuinto.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                                this.ddlColegioQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                                this.ddlAnioLectivoQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                                this.ddlTipoCalificacionQuinto.SelectedValue = recEducacionDetalle.CodTipoCalificacion.ToString();
                            }
                            #endregion

                            #region comentado
                            //switch (indice)
                            //{
                            //    case 0:     ///TERCER AÑO
                            //        if (degreid == "33")// "33"
                            //        {
                            //            this.txtIdApplicationEducationEnrollTercero.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                            //            this.ddlColegioTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                            //            this.ddlAnioLectivoTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                            //            this.txtOrdenMeritoTercero.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                            //            this.txtNroAlmunosTercero.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                            //            if (recEducacionDetalle.NotaMateTercero == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaMateTercero.Visible = true;
                            //                this.txtNotaMateTercero.Text = UIConvertNull.String(recEducacionDetalle.NotaMateTercero);
                            //                this.ddlNotaMateTercero2.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaMateTercero.Visible = false;
                            //                this.txtNotaMateTercero.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaMateTercero2.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaMateTercero);
                            //            }

                            //            if (recEducacionDetalle.NotaLengTercero == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaLengTercero.Visible = true;
                            //                this.txtNotaLengTercero.Text = UIConvertNull.String(recEducacionDetalle.NotaLengTercero);
                            //                this.ddlNotaLengTercero.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaLengTercero.Visible = false;
                            //                this.txtNotaLengTercero.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaLengTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaLengTercero);
                            //            }
                            //        }
                            //        if (degreid == "8")
                            //        {
                            //            this.txtIdApplicationEducationEnrollCuarto.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                            //            this.ddlColegioCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                            //            this.ddlAnioLectivoCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                            //            this.txtOrdenMeritoCuarto.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                            //            this.txtNroAlmunosCuarto.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                            //            if (recEducacionDetalle.NotaMateCuarto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaMateCuarto.Visible = true;
                            //                this.txtNotaMateCuarto.Text = UIConvertNull.String(recEducacionDetalle.NotaMateCuarto);
                            //                this.ddlNotaMateCuarto.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaMateCuarto.Visible = false;
                            //                this.txtNotaMateCuarto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaMateCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaMateCuarto);
                            //            }

                            //            if (recEducacionDetalle.NotaLengCuarto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaLengCuarto.Visible = true;
                            //                this.txtNotaLengCuarto.Text = UIConvertNull.String(recEducacionDetalle.NotaLengCuarto);
                            //                this.ddlNotaLengCuartoPopup.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaLengCuarto.Visible = false;
                            //                this.txtNotaLengCuarto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaLengCuartoPopup.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaLengCuarto);
                            //            }
                            //        }
                            //        if (degreid == "9")
                            //        {
                            //            this.txtIdApplicationEducationEnrollQuinto.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                            //            this.ddlColegioQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                            //            this.ddlAnioLectivoQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                            //            this.txtOrdenMeritoQuinto.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                            //            this.txtNroAlmunosQuinto.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                            //            if (recEducacionDetalle.NotaMateQuinto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaMateQuinto.Visible = true;
                            //                this.txtNotaMateQuinto.Text = UIConvertNull.String(recEducacionDetalle.NotaMateQuinto);
                            //                this.ddlNotaMateQuinto.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaMateQuinto.Visible = false;
                            //                this.txtNotaMateQuinto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaMateQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaMateQuinto);
                            //            }

                            //            if (recEducacionDetalle.NotaLengQuinto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaLengQuinto.Visible = true;
                            //                this.txtNotaLengQuinto.Text = UIConvertNull.String(recEducacionDetalle.NotaLengQuinto);
                            //                this.ddlNotaLengQuintoPopup.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaLengQuinto.Visible = false;
                            //                this.txtNotaLengQuinto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaLengQuintoPopup.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaLengQuinto);
                            //            }
                            //        }
                            //        break;
                            //    case 1:     ///CUARTO AÑO

                            //        if (degreid == "33")
                            //        {
                            //            this.txtIdApplicationEducationEnrollTercero.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                            //            this.ddlColegioTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                            //            this.ddlAnioLectivoTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                            //            this.txtOrdenMeritoTercero.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                            //            this.txtNroAlmunosTercero.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                            //            if (recEducacionDetalle.NotaMateTercero == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaMateTercero.Visible = true;
                            //                this.txtNotaMateTercero.Text = UIConvertNull.String(recEducacionDetalle.NotaMateTercero);
                            //                this.ddlNotaMateTercero.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaMateTercero.Visible = false;
                            //                this.txtNotaMateTercero.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaMateTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaMateTercero);
                            //            }

                            //            if (recEducacionDetalle.NotaLengTercero == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaLengTercero.Visible = true;
                            //                this.txtNotaLengTercero.Text = UIConvertNull.String(recEducacionDetalle.NotaLengTercero);
                            //                this.ddlNotaLengTercero.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaLengTercero.Visible = false;
                            //                this.txtNotaLengTercero.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaLengTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaLengTercero);
                            //            }
                            //        }
                            //        if (degreid == "8")
                            //        {
                            //            this.txtIdApplicationEducationEnrollCuarto.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                            //            this.ddlColegioCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                            //            this.ddlAnioLectivoCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                            //            this.txtOrdenMeritoCuarto.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                            //            this.txtNroAlmunosCuarto.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                            //            if (recEducacionDetalle.NotaMateCuarto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaMateCuarto.Visible = true;
                            //                this.txtNotaMateCuarto.Text = UIConvertNull.String(recEducacionDetalle.NotaMateCuarto);
                            //                this.ddlNotaMateCuarto.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaMateCuarto.Visible = false;
                            //                this.txtNotaMateCuarto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaMateCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaMateCuarto);
                            //            }

                            //            if (recEducacionDetalle.NotaLengCuarto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaLengCuarto.Visible = true;
                            //                this.txtNotaLengCuarto.Text = UIConvertNull.String(recEducacionDetalle.NotaLengCuarto);
                            //                this.ddlNotaLengCuartoPopup.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaLengCuarto.Visible = false;
                            //                this.txtNotaLengCuarto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaLengCuartoPopup.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaLengCuarto);
                            //            }
                            //        }
                            //        if (degreid == "9")
                            //        {
                            //            this.txtIdApplicationEducationEnrollQuinto.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                            //            this.ddlColegioQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                            //            this.ddlAnioLectivoQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                            //            this.txtOrdenMeritoQuinto.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                            //            this.txtNroAlmunosQuinto.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                            //            if (recEducacionDetalle.NotaMateQuinto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaMateQuinto.Visible = true;
                            //                this.txtNotaMateQuinto.Text = UIConvertNull.String(recEducacionDetalle.NotaMateQuinto);
                            //                this.ddlNotaMateQuinto.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaMateQuinto.Visible = false;
                            //                this.txtNotaMateQuinto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaMateQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaMateQuinto);
                            //            }

                            //            if (recEducacionDetalle.NotaLengQuinto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaLengQuinto.Visible = true;
                            //                this.txtNotaLengQuinto.Text = UIConvertNull.String(recEducacionDetalle.NotaLengQuinto);
                            //                this.ddlNotaLengQuintoPopup.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaLengQuinto.Visible = false;
                            //                this.txtNotaLengQuinto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaLengQuintoPopup.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaLengQuinto);
                            //            }
                            //        }
                            //        break;
                            //    case 2:     ///QUINTO AÑO
                            //        if (degreid == "33")
                            //        {
                            //            this.txtIdApplicationEducationEnrollTercero.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                            //            this.ddlColegioTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                            //            this.ddlAnioLectivoTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                            //            this.txtOrdenMeritoTercero.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                            //            this.txtNroAlmunosTercero.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                            //            if (recEducacionDetalle.NotaMateTercero == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaMateTercero.Visible = true;
                            //                this.txtNotaMateTercero.Text = UIConvertNull.String(recEducacionDetalle.NotaMateTercero);
                            //                this.ddlNotaMateTercero.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaMateTercero.Visible = false;
                            //                this.txtNotaMateTercero.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaMateTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaMateTercero);
                            //            }

                            //            if (recEducacionDetalle.NotaLengTercero == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaLengTercero.Visible = true;
                            //                this.txtNotaLengTercero.Text = UIConvertNull.String(recEducacionDetalle.NotaLengTercero);
                            //                this.ddlNotaLengTercero.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaLengTercero.Visible = false;
                            //                this.txtNotaLengTercero.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaLengTercero.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaLengTercero);
                            //            }
                            //        }
                            //        if (degreid == "8")
                            //        {
                            //            this.txtIdApplicationEducationEnrollCuarto.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                            //            this.ddlColegioCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                            //            this.ddlAnioLectivoCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                            //            this.txtOrdenMeritoCuarto.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                            //            this.txtNroAlmunosCuarto.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                            //            if (recEducacionDetalle.NotaMateCuarto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaMateCuarto.Visible = true;
                            //                this.txtNotaMateCuarto.Text = UIConvertNull.String(recEducacionDetalle.NotaMateCuarto);
                            //                this.ddlNotaMateCuarto.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaMateCuarto.Visible = false;
                            //                this.txtNotaMateCuarto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaMateCuarto.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaMateCuarto);
                            //            }

                            //            if (recEducacionDetalle.NotaLengCuarto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaLengCuarto.Visible = true;
                            //                this.txtNotaLengCuarto.Text = UIConvertNull.String(recEducacionDetalle.NotaLengCuarto);
                            //                this.ddlNotaLengCuartoPopup.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaLengCuarto.Visible = false;
                            //                this.txtNotaLengCuarto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaLengCuartoPopup.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaLengCuarto);
                            //            }
                            //        }
                            //        if (degreid == "9")
                            //        {
                            //            this.txtIdApplicationEducationEnrollQuinto.Text = UIConvertNull.String(recEducacionDetalle.IdApplicationEducationEnroll);
                            //            this.ddlColegioQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.IdApplicationEducation);
                            //            this.ddlAnioLectivoQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.anioInicio);
                            //            this.txtOrdenMeritoQuinto.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                            //            this.txtNroAlmunosQuinto.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                            //            if (recEducacionDetalle.NotaMateQuinto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaMateQuinto.Visible = true;
                            //                this.txtNotaMateQuinto.Text = UIConvertNull.String(recEducacionDetalle.NotaMateQuinto);
                            //                this.ddlNotaMateQuinto.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaMateQuinto.Visible = false;
                            //                this.txtNotaMateQuinto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaMateQuinto.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaMateQuinto);
                            //            }

                            //            if (recEducacionDetalle.NotaLengQuinto == UIConstantes._valorNotaOtroEnt)
                            //            {
                            //                this.txtNotaLengQuinto.Visible = true;
                            //                this.txtNotaLengQuinto.Text = UIConvertNull.String(recEducacionDetalle.NotaLengQuinto);
                            //                this.ddlNotaLengQuintoPopup.SelectedValue = UIConstantes._valorNotaOtro;
                            //            }
                            //            else
                            //            {
                            //                this.txtNotaLengQuinto.Visible = false;
                            //                this.txtNotaLengQuinto.Text = UIConstantes._valorCadenaVacia;
                            //                this.ddlNotaLengQuintoPopup.SelectedValue = UIConvertNull.String(recEducacionDetalle.NotaLengQuinto);
                            //            }
                            //        }
                            //        break;
                            //    default:
                            //        break;
                            //}
                            #endregion

                            if (codTipoCalificacion == UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO.ToString("D"))
                                LLenarDatosRendimientoAcademicoRegistradoPorGradoNumerico(recEducacionDetalle);

                            if (codTipoCalificacion == UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS.ToString("D"))
                                LLenarDatosRendimientoAcademicoRegistradoPorGradoLetra(recEducacionDetalle);

                            /*Fin:Christian Ramirez -REQ91569*/
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosNotasRegistradas(AplicanteBE oAplicanteBE)
        {
            try
            {
                if (oAplicanteBE != null && oAplicanteBE.LDetalleEducacion != null && oAplicanteBE.LNotas != null)
                {
                    List<NotasBE> LNotas = (from itemEduca in oAplicanteBE.LNotas
                                            select itemEduca).ToList<NotasBE>();

                    if (LNotas != null)
                    {
                        for (int indice = 0; indice < LNotas.Count; indice++)
                        {
                            NotasBE oNotasBE = LNotas[indice];
                            switch (indice)
                            {
                                case 0:
                                    /**/
                                    //TERCER AÑO
                                    this.txtNotaMateTercero.Text = UIConvertNull.String(oNotasBE.M3);
                                    this.txtNotaLengTercero.Text = UIConvertNull.String(oNotasBE.L3);
                                    #region comentado
                                    //if (oNotasBE.M3 == 21)
                                    //{
                                    //    this.txtNotaMateTercero.Visible = true;
                                    //    this.txtNotaMateTercero.Text = UIConvertNull.String(oNotasBE.ONM3);
                                    //    //this.ddlNotaMateTercero.SelectedValue = UIConstantes._valorNotaOtro;
                                    //}
                                    //else
                                    //{
                                    //    this.txtNotaMateTercero.Visible = false;
                                    //    this.txtNotaMateTercero.Text = UIConstantes._valorCadenaVacia;
                                    //    if (oNotasBE.ONM3 == "AD" || oNotasBE.ONM3 == "A" || oNotasBE.ONM3 == "B" || oNotasBE.ONM3 == "C")
                                    //    {
                                    //        this.ddlNotaMateTercero.SelectedValue = oNotasBE.ONM3;
                                    //    }
                                    //    else
                                    //    {
                                    //        this.ddlNotaMateTercero.SelectedValue = UIConvertNull.String(oNotasBE.M3);
                                    //    }
                                    //}

                                    //if (oNotasBE.L3 == 21)
                                    //{
                                    //    this.txtNotaLengTercero.Visible = true;
                                    //    this.txtNotaLengTercero.Text = UIConvertNull.String(oNotasBE.ONL3);
                                    //    this.ddlNotaLengTercero.SelectedValue = UIConstantes._valorNotaOtro;
                                    //}
                                    //else
                                    //{
                                    //    this.txtNotaLengTercero.Visible = false;
                                    //    this.txtNotaLengTercero.Text = UIConstantes._valorCadenaVacia;
                                    //    if (oNotasBE.ONL3 == "AD" || oNotasBE.ONL3 == "A" || oNotasBE.ONL3 == "B" || oNotasBE.ONL3 == "C")
                                    //    {
                                    //        this.ddlNotaLengTercero.SelectedValue = oNotasBE.ONL3;
                                    //    }
                                    //    else
                                    //    {
                                    //        this.ddlNotaLengTercero.SelectedValue = UIConvertNull.String(oNotasBE.L3);
                                    //    }
                                    //}
                                    #endregion

                                    //Cuarto Año
                                    this.txtNotaMateCuarto.Text = UIConvertNull.String(oNotasBE.M4);
                                    this.txtNotaLengCuarto.Text = UIConvertNull.String(oNotasBE.L4);
                                    #region comentado
                                    //if (oNotasBE.M4 == 21)
                                    //{
                                    //    this.txtNotaMateCuarto.Visible = true;
                                    //    this.txtNotaMateCuarto.Text = UIConvertNull.String(oNotasBE.ONM4);
                                    //    this.ddlNotaMateCuarto.SelectedValue = UIConstantes._valorNotaOtro;
                                    //}
                                    //else
                                    //{
                                    //    this.txtNotaMateCuarto.Visible = false;
                                    //    this.txtNotaMateCuarto.Text = UIConstantes._valorCadenaVacia;

                                    //    if (oNotasBE.ONM4 == "AD" || oNotasBE.ONM4 == "A" || oNotasBE.ONM4 == "B" || oNotasBE.ONM4 == "C")
                                    //        this.ddlNotaMateCuarto.SelectedValue = oNotasBE.ONM4;
                                    //    else
                                    //        this.ddlNotaMateCuarto.SelectedValue = UIConvertNull.String(oNotasBE.M4);
                                    //}

                                    //if (oNotasBE.L4 == 21)
                                    //{
                                    //    this.txtNotaLengCuarto.Visible = true;
                                    //    this.txtNotaLengCuarto.Text = UIConvertNull.String(oNotasBE.ONL4);
                                    //    this.ddlNotaLengCuartoPopup.SelectedValue = UIConstantes._valorNotaOtro;
                                    //}
                                    //else
                                    //{
                                    //    this.txtNotaLengCuarto.Visible = false;
                                    //    this.txtNotaLengCuarto.Text = UIConstantes._valorCadenaVacia;
                                    //    if (oNotasBE.ONL4 == "AD" || oNotasBE.ONL4 == "A" || oNotasBE.ONL4 == "B" || oNotasBE.ONL4 == "C")
                                    //        this.ddlNotaLengCuartoPopup.SelectedValue = oNotasBE.ONL4;
                                    //    else
                                    //        this.ddlNotaLengCuartoPopup.SelectedValue = UIConvertNull.String(oNotasBE.L4);
                                    //}
                                    #endregion

                                    //Quinto Año
                                    this.txtNotaMateQuinto.Text = UIConvertNull.String(oNotasBE.M5);
                                    this.txtNotaLengQuinto.Text = UIConvertNull.String(oNotasBE.L5);
                                    #region comentado
                                    //if (oNotasBE.M5 == 21)
                                    //{
                                    //    this.txtNotaMateQuinto.Visible = true;
                                    //    this.txtNotaMateQuinto.Text = UIConvertNull.String(oNotasBE.ONM5);
                                    //    this.ddlNotaMateQuinto.SelectedValue = UIConstantes._valorNotaOtro;
                                    //}
                                    //else
                                    //{
                                    //    this.txtNotaMateQuinto.Visible = false;
                                    //    this.txtNotaMateQuinto.Text = UIConstantes._valorCadenaVacia;

                                    //    if (oNotasBE.ONM5 == "AD" || oNotasBE.ONM5 == "A" || oNotasBE.ONM5 == "B" || oNotasBE.ONM5 == "C")
                                    //        this.ddlNotaMateQuinto.SelectedValue = oNotasBE.ONM5;
                                    //    else
                                    //        this.ddlNotaMateQuinto.SelectedValue = UIConvertNull.String(oNotasBE.M5);
                                    //}

                                    //if (oNotasBE.L5 == 21)
                                    //{
                                    //    this.txtNotaLengQuinto.Visible = true;
                                    //    this.txtNotaLengQuinto.Text = UIConvertNull.String(oNotasBE.ONL5);
                                    //    this.ddlNotaLengQuintoPopup.SelectedValue = UIConstantes._valorNotaOtro;
                                    //}
                                    //else
                                    //{
                                    //    this.txtNotaLengQuinto.Visible = false;
                                    //    this.txtNotaLengQuinto.Text = UIConstantes._valorCadenaVacia;
                                    //    if (oNotasBE.ONL5 == "AD" || oNotasBE.ONL5 == "A" || oNotasBE.ONL5 == "B" || oNotasBE.ONL5 == "C")
                                    //        this.ddlNotaLengQuintoPopup.SelectedValue = oNotasBE.ONL5;
                                    //    else
                                    //        this.ddlNotaLengQuintoPopup.SelectedValue = UIConvertNull.String(oNotasBE.L5);
                                    //}
                                    #endregion

                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    #region cargar notas letras
                    /*Ini:Christian Ramirez -REQ91569*/
                    #region Valores
                    int cienciaTecnologia = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciaTecnologia;
                    int CT01 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Diseña_y_construye_soluciones_tecnológicas_para_resolver_problemas;
                    int CT02 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Explica_el_mundo_físico_basándose_en_conocimientos_sobre_los_seres_vivos_materia_y_energía_biodiversidad;
                    int CT03 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Indaga_mediante_metodos_científicos_para_construir_sus_conocimientos;

                    int cienciasSociales = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciasSociales;
                    int CS01 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Construye_interpretaciones_historicas;
                    int CS02 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Gestiona_responsablemente_el_espacio_y_el_ambiente;
                    int CS03 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Gestiona_responsablemente_los_recursos_económicos; /*Se modifica:Christian Ramirez - REQ95070*/

                    int competenciasTransversales = (int)UIConstantes.REND_ACADEMICO_CURSO.CompetenciasTransversales; /*Se modifica:Christian Ramirez - REQ95070*/
                    int CTR01 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Gestiona_su_aprendizaje_de_manera_autonoma;
                    int CTR02 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Se_desenvuelve_en_los_entornos_virtuales_generados_por_las_TIC;

                    int comunicacion = (int)UIConstantes.REND_ACADEMICO_CURSO.ComunicacionLenguaMaterna;
                    int CL01 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Escribe_diversos_tipos_de_texto;
                    int CL02 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Lee_diversos_tipos_de_textos_escritos;
                    int CL03 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Se_comunica_oralmente;

                    int matematica = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica;
                    int MA01 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Resuelve_problemas_de_cantidad;
                    int MA02 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Resuelve_problemas_de_forma_y_movimiento;
                    int MA03 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Resuelve_problemas_de_gestion_de_datos_e_incertidumbre;
                    int MA04 = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA.Resuelve_problemas_de_regularidad_equivalencia_y_cambio;
                    #endregion

                    if (oAplicanteBE.ListaRendimientoAcademicoBE != null)
                    {
                        var listaTercero = oAplicanteBE.ListaRendimientoAcademicoBE.Where(
                            x => x.DegreeId == (int)UIConstantes.GRADO_PREGRADO.TERCERO_SECUNDARIA).ToList();
                        #region Tercero
                        if (listaTercero.Count > 0)
                        {
                            RendimientoAcademicoBE oRendimientoAcademicoBE = listaTercero[0];
                            if (oRendimientoAcademicoBE.CodTipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS)
                            {
                                CT01CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciaTecnologia && x.CompetenciaId == CT01)
                                    .ToList()[0].CalificacionId.ToString();
                                CT01Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CT01CodigoTercero.Text);

                                CT02CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciaTecnologia && x.CompetenciaId == CT02)
                                    .ToList()[0].CalificacionId.ToString();
                                CT02Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CT02CodigoTercero.Text);

                                CT03CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciaTecnologia && x.CompetenciaId == CT03)
                                    .ToList()[0].CalificacionId.ToString();
                                CT03Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CT03CodigoTercero.Text);
                                //--------------------------------------------------------------------------------------------------//

                                CS01CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciasSociales && x.CompetenciaId == CS01)
                                    .ToList()[0].CalificacionId.ToString();
                                CS01Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CS01CodigoTercero.Text);

                                CS02CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == cienciasSociales && x.CompetenciaId == CS02)
                                   .ToList()[0].CalificacionId.ToString();
                                CS02Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CS02CodigoTercero.Text);

                                CS03CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == cienciasSociales && x.CompetenciaId == CS03)
                                   .ToList()[0].CalificacionId.ToString();
                                CS03Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CS03CodigoTercero.Text);
                                //--------------------------------------------------------------------------------------------------//

                                CTR01CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == competenciasTransversales && x.CompetenciaId == CTR01)
                                   .ToList()[0].CalificacionId.ToString();
                                CTR01Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CTR01CodigoTercero.Text);

                                CTR02CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == competenciasTransversales && x.CompetenciaId == CTR02)
                                   .ToList()[0].CalificacionId.ToString();
                                CTR02Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CTR02CodigoTercero.Text);
                                //--------------------------------------------------------------------------------------------------//

                                CL01CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == comunicacion && x.CompetenciaId == CL01)
                                   .ToList()[0].CalificacionId.ToString();
                                CL01Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CL01CodigoTercero.Text);

                                CL02CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == comunicacion && x.CompetenciaId == CL02)
                                   .ToList()[0].CalificacionId.ToString();
                                CL02Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CL02CodigoTercero.Text);

                                CL03CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == comunicacion && x.CompetenciaId == CL03)
                                   .ToList()[0].CalificacionId.ToString();
                                CL03Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CL03CodigoTercero.Text);
                                //--------------------------------------------------------------------------------------------------//

                                MA01CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == matematica && x.CompetenciaId == MA01)
                                   .ToList()[0].CalificacionId.ToString();
                                MA01Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA01CodigoTercero.Text);

                                MA02CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                  .Where(x => x.CursoId == matematica && x.CompetenciaId == MA02)
                                  .ToList()[0].CalificacionId.ToString();
                                MA02Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA02CodigoTercero.Text);

                                MA03CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                  .Where(x => x.CursoId == matematica && x.CompetenciaId == MA03)
                                  .ToList()[0].CalificacionId.ToString();
                                MA03Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA03CodigoTercero.Text);

                                MA04CodigoTercero.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                  .Where(x => x.CursoId == matematica && x.CompetenciaId == MA04)
                                  .ToList()[0].CalificacionId.ToString();
                                MA04Tercero.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA04CodigoTercero.Text);
                            }
                        }
                        #endregion

                        var listaCuarto = oAplicanteBE.ListaRendimientoAcademicoBE.Where(
                            x => x.DegreeId == (int)UIConstantes.GRADO_PREGRADO.CUARTO_SECUNDARIA).ToList();
                        #region Cuarto
                        if (listaCuarto.Count > 0)
                        {
                            RendimientoAcademicoBE oRendimientoAcademicoBE = listaCuarto[0];
                            if (oRendimientoAcademicoBE.CodTipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS)
                            {
                                CT01CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciaTecnologia && x.CompetenciaId == CT01)
                                    .ToList()[0].CalificacionId.ToString();
                                CT01Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CT01CodigoCuarto.Text);

                                CT02CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciaTecnologia && x.CompetenciaId == CT02)
                                    .ToList()[0].CalificacionId.ToString();
                                CT02Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CT02CodigoCuarto.Text);

                                CT03CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciaTecnologia && x.CompetenciaId == CT03)
                                    .ToList()[0].CalificacionId.ToString();
                                CT03Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CT03CodigoCuarto.Text);
                                //--------------------------------------------------------------------------------------------------//

                                CS01CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciasSociales && x.CompetenciaId == CS01)
                                    .ToList()[0].CalificacionId.ToString();
                                CS01Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CS01CodigoCuarto.Text);

                                CS02CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == cienciasSociales && x.CompetenciaId == CS02)
                                   .ToList()[0].CalificacionId.ToString();
                                CS02Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CS02CodigoCuarto.Text);

                                CS03CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == cienciasSociales && x.CompetenciaId == CS03)
                                   .ToList()[0].CalificacionId.ToString();
                                CS03Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CS03CodigoCuarto.Text);
                                //--------------------------------------------------------------------------------------------------//

                                CTR01CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == competenciasTransversales && x.CompetenciaId == CTR01)
                                   .ToList()[0].CalificacionId.ToString();
                                CTR01Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CTR01CodigoCuarto.Text);

                                CTR02CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == competenciasTransversales && x.CompetenciaId == CTR02)
                                   .ToList()[0].CalificacionId.ToString();
                                CTR02Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CTR02CodigoCuarto.Text);
                                //--------------------------------------------------------------------------------------------------//

                                CL01CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == comunicacion && x.CompetenciaId == CL01)
                                   .ToList()[0].CalificacionId.ToString();
                                CL01Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CL01CodigoCuarto.Text);

                                CL02CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == comunicacion && x.CompetenciaId == CL02)
                                   .ToList()[0].CalificacionId.ToString();
                                CL02Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CL02CodigoCuarto.Text);

                                CL03CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == comunicacion && x.CompetenciaId == CL03)
                                   .ToList()[0].CalificacionId.ToString();
                                CL03Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CL03CodigoCuarto.Text);
                                //--------------------------------------------------------------------------------------------------//

                                MA01CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == matematica && x.CompetenciaId == MA01)
                                   .ToList()[0].CalificacionId.ToString();
                                MA01Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA01CodigoCuarto.Text);

                                MA02CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                  .Where(x => x.CursoId == matematica && x.CompetenciaId == MA02)
                                  .ToList()[0].CalificacionId.ToString();
                                MA02Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA02CodigoCuarto.Text);

                                MA03CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                  .Where(x => x.CursoId == matematica && x.CompetenciaId == MA03)
                                  .ToList()[0].CalificacionId.ToString();
                                MA03Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA03CodigoCuarto.Text);

                                MA04CodigoCuarto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                  .Where(x => x.CursoId == matematica && x.CompetenciaId == MA04)
                                  .ToList()[0].CalificacionId.ToString();
                                MA04Cuarto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA04CodigoCuarto.Text);
                            }
                        }
                        #endregion

                        var listaQuinto = oAplicanteBE.ListaRendimientoAcademicoBE.Where(
                            x => x.DegreeId == (int)UIConstantes.GRADO_PREGRADO.QUINTO_SECUNDARIA).ToList();
                        #region Quinto
                        if (listaQuinto.Count > 0)
                        {
                            RendimientoAcademicoBE oRendimientoAcademicoBE = listaQuinto[0];
                            if (oRendimientoAcademicoBE.CodTipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS)
                            {
                                CT01CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciaTecnologia && x.CompetenciaId == CT01)
                                    .ToList()[0].CalificacionId.ToString();
                                CT01Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CT01CodigoQuinto.Text);

                                CT02CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciaTecnologia && x.CompetenciaId == CT02)
                                    .ToList()[0].CalificacionId.ToString();
                                CT02Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CT02CodigoQuinto.Text);

                                CT03CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciaTecnologia && x.CompetenciaId == CT03)
                                    .ToList()[0].CalificacionId.ToString();
                                CT03Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CT03CodigoQuinto.Text);
                                //--------------------------------------------------------------------------------------------------//

                                CS01CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                    .Where(x => x.CursoId == cienciasSociales && x.CompetenciaId == CS01)
                                    .ToList()[0].CalificacionId.ToString();
                                CS01Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CS01CodigoQuinto.Text);

                                CS02CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == cienciasSociales && x.CompetenciaId == CS02)
                                   .ToList()[0].CalificacionId.ToString();
                                CS02Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CS02CodigoQuinto.Text);

                                CS03CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == cienciasSociales && x.CompetenciaId == CS03)
                                   .ToList()[0].CalificacionId.ToString();
                                CS03Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CS03CodigoQuinto.Text);
                                //--------------------------------------------------------------------------------------------------//

                                CTR01CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == competenciasTransversales && x.CompetenciaId == CTR01)
                                   .ToList()[0].CalificacionId.ToString();
                                CTR01Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CTR01CodigoQuinto.Text);

                                CTR02CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == competenciasTransversales && x.CompetenciaId == CTR02)
                                   .ToList()[0].CalificacionId.ToString();
                                CTR02Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CTR02CodigoQuinto.Text);
                                //--------------------------------------------------------------------------------------------------//

                                CL01CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == comunicacion && x.CompetenciaId == CL01)
                                   .ToList()[0].CalificacionId.ToString();
                                CL01Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CL01CodigoQuinto.Text);

                                CL02CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == comunicacion && x.CompetenciaId == CL02)
                                   .ToList()[0].CalificacionId.ToString();
                                CL02Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CL02CodigoQuinto.Text);

                                CL03CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == comunicacion && x.CompetenciaId == CL03)
                                   .ToList()[0].CalificacionId.ToString();
                                CL03Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(CL03CodigoQuinto.Text);
                                //--------------------------------------------------------------------------------------------------//

                                MA01CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                   .Where(x => x.CursoId == matematica && x.CompetenciaId == MA01)
                                   .ToList()[0].CalificacionId.ToString();
                                MA01Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA01CodigoQuinto.Text);

                                MA02CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                  .Where(x => x.CursoId == matematica && x.CompetenciaId == MA02)
                                  .ToList()[0].CalificacionId.ToString();
                                MA02Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA02CodigoQuinto.Text);

                                MA03CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                  .Where(x => x.CursoId == matematica && x.CompetenciaId == MA03)
                                  .ToList()[0].CalificacionId.ToString();
                                MA03Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA03CodigoQuinto.Text);

                                MA04CodigoQuinto.Text = oRendimientoAcademicoBE.ListaRendimientoAcademicoEvaluacionBE
                                  .Where(x => x.CursoId == matematica && x.CompetenciaId == MA04)
                                  .ToList()[0].CalificacionId.ToString();
                                MA04Quinto.Text = UIConstantes.ObtenerRendAcademicoCalificacion(MA04CodigoQuinto.Text);
                            }
                        }
                        #endregion
                    }

                    /*Fin:Christian Ramirez -REQ91569*/
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarAnioAcademico(Int32? AplicanteId)
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                Boolean Respuesta = (Boolean)oAplicanteBL.ObtenerAnioAcademico(AplicanteId);
                if (Respuesta)
                {
                    //Div2.Visible = true;
                    //Div3.Visible = true;
                }
                else
                {
                    Div2.Visible = false;
                    Div3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosRendimientoAcademicoRegistradoPorGradoNumerico(EducacionDetalleBE recEducacionDetalle)
        {
            if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.TERCERO_SECUNDARIA.ToString("D"))
            {
                this.txtOrdenMeritoTercero.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                this.txtNroAlmunosTercero.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                divNumericoTercero.Visible = true;
                divLetraTercero.Visible = false;
                //ddlTipoCalificacionTercero.SelectedValue = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO.ToString("D");
            }

            if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.CUARTO_SECUNDARIA.ToString("D"))
            {
                this.txtOrdenMeritoCuarto.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                this.txtNroAlmunosCuarto.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                divNumericoCuarto.Visible = true;
                //ddlTipoCalificacionCuarto.SelectedValue = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO.ToString("D");
            }

            if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.QUINTO_SECUNDARIA.ToString("D"))
            {
                this.txtOrdenMeritoQuinto.Text = UIConvertNull.String(recEducacionDetalle.NombreGrado);
                this.txtNroAlmunosQuinto.Text = UIConvertNull.String(recEducacionDetalle.CantidadEstudiantes);
                divNumericoQuinto.Visible = true;
                //ddlTipoCalificacionQuinto.SelectedValue = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO.ToString("D");
            }
        }

        private void LLenarDatosRendimientoAcademicoRegistradoPorGradoLetra(EducacionDetalleBE recEducacionDetalle)
        {
            if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.TERCERO_SECUNDARIA.ToString("D"))
            {
                divNumericoTercero.Visible = false;
                divLetraTercero.Visible = true;
                //ddlTipoCalificacionTercero.SelectedValue = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS.ToString("D");
            }

            if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.CUARTO_SECUNDARIA.ToString("D"))
            {
                divNumericoCuarto.Visible = false;
                divLetraCuarto.Visible = true;
                //ddlTipoCalificacionCuarto.SelectedValue = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS.ToString("D");
            }

            if (recEducacionDetalle.IdGrado.ToString() == UIConstantes.GRADO_PREGRADO.QUINTO_SECUNDARIA.ToString("D"))
            {
                divNumericoQuinto.Visible = false;
                divLetraQuinto.Visible = true;
                //ddlTipoCalificacionQuinto.SelectedValue = UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS.ToString("D");
            }
        }

        protected void imgBtnVideo_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            String URL = "frmVideoInformativo.aspx";
            String vtn = "window.open('" + URL + "','video','scrollbars=yes,resizable=no,location=no,width=640,height=380')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }

        #region Tipo Calificacion => Seleccion
        protected void ddlTipoCalificacionTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionTercero.SelectedValue);

            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR)
            {
                divNumericoTercero.Visible = false;
                divLetraTercero.Visible = false;
                LimpiarControlesRendNotasNumerico(UIConstantes.Grado._Tercero);
                LimpiarControlesRendNotasLetra(UIConstantes.Grado._Tercero);
                LimpiarControlesRendNotasPopup();
            }
            else
            {
                btnCerrarPopupTipoCalificacion.Visible = true;
                MostrarPopupPorTipoCalificacion(tipoCalificacion, UIConstantes.Grado._Tercero);
            }
        }

        protected void ddlTipoCalificacionCuarto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionCuarto.SelectedValue);

            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR)
            {
                divNumericoCuarto.Visible = false;
                divLetraCuarto.Visible = false;
                LimpiarControlesRendNotasNumerico(UIConstantes.Grado._Cuarto);
                LimpiarControlesRendNotasLetra(UIConstantes.Grado._Cuarto);
                LimpiarControlesRendNotasPopup();
            }
            else
            {
                btnCerrarPopupTipoCalificacion.Visible = true;
                MostrarPopupPorTipoCalificacion(tipoCalificacion, UIConstantes.Grado._Cuarto);
            }
        }

        protected void ddlTipoCalificacionQuinto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionQuinto.SelectedValue);

            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.SELECCIONAR)
            {
                divNumericoQuinto.Visible = false;
                divLetraQuinto.Visible = false;
                LimpiarControlesRendNotasNumerico(UIConstantes.Grado._Quinto);
                LimpiarControlesRendNotasLetra(UIConstantes.Grado._Quinto);
                LimpiarControlesRendNotasPopup();
            }

            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO)
            {
                btnCerrarPopupTipoCalificacion.Visible = true;
                divNumericoQuinto.Visible = true;
                divLetraQuinto.Visible = false;
            }

            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS)
            {
                btnCerrarPopupTipoCalificacion.Visible = true;
                divLetraQuinto.Visible = true;
                divNumericoQuinto.Visible = false;
            }

        }
        #endregion

        #region Tipo Calificacion Numerico => Editar Notas
        protected void btnEditarNotasTerceroNumerico_Click(object sender, EventArgs e)
        {
            int tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionTercero.SelectedValue);
            string grado = UIConstantes.Grado._Tercero;

            btnCerrarPopupTipoCalificacion.Visible = false;
            RecuperarDatosAPopup(tipoCalificacion, grado);
            MostrarPopupPorTipoCalificacion(tipoCalificacion, grado);
        }

        protected void btnEditarNotasCuartoNumerico_Click(object sender, EventArgs e)
        {
            int tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionCuarto.SelectedValue);
            string grado = UIConstantes.Grado._Cuarto;

            btnCerrarPopupTipoCalificacion.Visible = false;
            RecuperarDatosAPopup(tipoCalificacion, grado);
            MostrarPopupPorTipoCalificacion(tipoCalificacion, grado);
        }

        protected void btnEditarNotasQuintoNumerico_Click(object sender, EventArgs e)
        {
            int tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionQuinto.SelectedValue);
            string grado = UIConstantes.Grado._Quinto;

            //btnCerrarPopupTipoCalificacion.Visible = false;
            RecuperarDatosAPopup(tipoCalificacion, grado);
            MostrarPopupPorTipoCalificacion(tipoCalificacion, grado);
        }

        protected void btnEditarNotasTerceroLetra_Click(object sender, EventArgs e)
        {
            int tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionTercero.SelectedValue);
            string grado = UIConstantes.Grado._Tercero;
            btnCerrarPopupTipoCalificacion.Visible = false;
            RecuperarDatosAPopup(tipoCalificacion, grado);
            MostrarPopupPorTipoCalificacion(tipoCalificacion, grado);
        }

        protected void btnEditarNotasCuartoLetra_Click(object sender, EventArgs e)
        {
            int tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionCuarto.SelectedValue);
            string grado = UIConstantes.Grado._Cuarto;
            btnCerrarPopupTipoCalificacion.Visible = false;
            RecuperarDatosAPopup(tipoCalificacion, grado);
            MostrarPopupPorTipoCalificacion(tipoCalificacion, grado);
        }

        protected void btnEditarNotasQuintoLetra_Click(object sender, EventArgs e)
        {
            int tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionQuinto.SelectedValue);
            string grado = UIConstantes.Grado._Quinto;
            //btnCerrarPopupTipoCalificacion.Visible = false;
            RecuperarDatosAPopup(tipoCalificacion, grado);
            MostrarPopupPorTipoCalificacion(tipoCalificacion, grado);
        }
        #endregion

        private void RecuperarDatosAPopup(int tipoCalificacion, string grado)
        {
            #region Tipo Calificacion => Numerico
            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO)
            {
                if (grado == UIConstantes.Grado._Tercero)
                {
                    txtOrdenMeritoPopup.Text = txtOrdenMeritoTercero.Text;
                    txtNroAlmunosPopup.Text = txtNroAlmunosTercero.Text;
                    ddlNotaMatePopup.SelectedValue = (string.IsNullOrEmpty(txtNotaMateTercero.Text) ? "0" : txtNotaMateTercero.Text);
                    ddlNotaLengPopup.SelectedValue = (string.IsNullOrEmpty(txtNotaLengTercero.Text) ? "0" : txtNotaLengTercero.Text);
                }

                if (grado == UIConstantes.Grado._Cuarto)
                {
                    txtOrdenMeritoPopup.Text = txtOrdenMeritoCuarto.Text;
                    txtNroAlmunosPopup.Text = txtNroAlmunosCuarto.Text;
                    ddlNotaMatePopup.SelectedValue = string.IsNullOrEmpty(txtNotaMateCuarto.Text) ? "0" : txtNotaMateCuarto.Text;
                    ddlNotaLengPopup.SelectedValue = string.IsNullOrEmpty(txtNotaLengCuarto.Text) ? "0" : txtNotaLengCuarto.Text;
                }

                if (grado == UIConstantes.Grado._Quinto)
                {
                    txtOrdenMeritoPopup.Text = txtOrdenMeritoQuinto.Text;
                    txtNroAlmunosPopup.Text = txtNroAlmunosQuinto.Text;
                    ddlNotaMatePopup.SelectedValue = string.IsNullOrEmpty(txtNotaMateQuinto.Text) ? "0" : txtNotaMateQuinto.Text;
                    ddlNotaLengPopup.SelectedValue = string.IsNullOrEmpty(txtNotaLengQuinto.Text) ? "0" : txtNotaLengQuinto.Text;
                }
            }
            #endregion

            #region Tipo Calificacion => Letra
            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS)
            {
                if (grado == UIConstantes.Grado._Tercero)
                {
                    CT01Popup.SelectedValue = CT01CodigoTercero.Text;
                    CT02Popup.SelectedValue = CT02CodigoTercero.Text;
                    CT03Popup.SelectedValue = CT03CodigoTercero.Text;

                    CS01Popup.SelectedValue = CS01CodigoTercero.Text;
                    CS02Popup.SelectedValue = CS02CodigoTercero.Text;
                    CS03Popup.SelectedValue = CS03CodigoTercero.Text;

                    CTR01Popup.SelectedValue = CTR01CodigoTercero.Text;
                    CTR02Popup.SelectedValue = CTR02CodigoTercero.Text;

                    CL01Popup.SelectedValue = CL01CodigoTercero.Text;
                    CL02Popup.SelectedValue = CL02CodigoTercero.Text;
                    CL03Popup.SelectedValue = CL03CodigoTercero.Text;

                    MA01Popup.SelectedValue = MA01CodigoTercero.Text;
                    MA02Popup.SelectedValue = MA02CodigoTercero.Text;
                    MA03Popup.SelectedValue = MA03CodigoTercero.Text;
                    MA04Popup.SelectedValue = MA04CodigoTercero.Text;
                }

                if (grado == UIConstantes.Grado._Cuarto)
                {
                    CT01Popup.SelectedValue = CT01CodigoCuarto.Text;
                    CT02Popup.SelectedValue = CT02CodigoCuarto.Text;
                    CT03Popup.SelectedValue = CT03CodigoCuarto.Text;

                    CS01Popup.SelectedValue = CS01CodigoCuarto.Text;
                    CS02Popup.SelectedValue = CS02CodigoCuarto.Text;
                    CS03Popup.SelectedValue = CS03CodigoCuarto.Text;

                    CTR01Popup.SelectedValue = CTR01CodigoCuarto.Text;
                    CTR02Popup.SelectedValue = CTR02CodigoCuarto.Text;

                    CL01Popup.SelectedValue = CL01CodigoCuarto.Text;
                    CL02Popup.SelectedValue = CL02CodigoCuarto.Text;
                    CL03Popup.SelectedValue = CL03CodigoCuarto.Text;

                    MA01Popup.SelectedValue = MA01CodigoCuarto.Text;
                    MA02Popup.SelectedValue = MA02CodigoCuarto.Text;
                    MA03Popup.SelectedValue = MA03CodigoCuarto.Text;
                    MA04Popup.SelectedValue = MA04CodigoCuarto.Text;
                }

                if (grado == UIConstantes.Grado._Quinto)
                {
                    /*Ini:Christian Ramirez - REQ95070*/
                    CT01Popup.SelectedValue = string.IsNullOrEmpty(CT01CodigoQuinto.Text) ? "0" : CT01CodigoQuinto.Text;
                    CT02Popup.SelectedValue = string.IsNullOrEmpty(CT02CodigoQuinto.Text) ? "0" : CT02CodigoQuinto.Text;
                    CT03Popup.SelectedValue = string.IsNullOrEmpty(CT03CodigoQuinto.Text) ? "0" : CT03CodigoQuinto.Text;

                    CS01Popup.SelectedValue = string.IsNullOrEmpty(CS01CodigoQuinto.Text) ? "0" : CS01CodigoQuinto.Text;
                    CS02Popup.SelectedValue = string.IsNullOrEmpty(CS02CodigoQuinto.Text) ? "0" : CS02CodigoQuinto.Text;
                    CS03Popup.SelectedValue = string.IsNullOrEmpty(CS03CodigoQuinto.Text) ? "0" : CS03CodigoQuinto.Text;

                    CTR01Popup.SelectedValue = string.IsNullOrEmpty(CTR01CodigoQuinto.Text) ? "0" : CTR01CodigoQuinto.Text;
                    CTR02Popup.SelectedValue = string.IsNullOrEmpty(CTR02CodigoQuinto.Text) ? "0" : CTR02CodigoQuinto.Text;

                    CL01Popup.SelectedValue = string.IsNullOrEmpty(CL01CodigoQuinto.Text) ? "0" : CL01CodigoQuinto.Text;
                    CL02Popup.SelectedValue = string.IsNullOrEmpty(CL02CodigoQuinto.Text) ? "0" : CL02CodigoQuinto.Text;
                    CL03Popup.SelectedValue = string.IsNullOrEmpty(CL03CodigoQuinto.Text) ? "0" : CL03CodigoQuinto.Text;

                    MA01Popup.SelectedValue = string.IsNullOrEmpty(MA01CodigoQuinto.Text) ? "0" : MA01CodigoQuinto.Text;
                    MA02Popup.SelectedValue = string.IsNullOrEmpty(MA02CodigoQuinto.Text) ? "0" : MA02CodigoQuinto.Text;
                    MA03Popup.SelectedValue = string.IsNullOrEmpty(MA03CodigoQuinto.Text) ? "0" : MA03CodigoQuinto.Text;
                    MA04Popup.SelectedValue = string.IsNullOrEmpty(MA04CodigoQuinto.Text) ? "0" : MA04CodigoQuinto.Text;
                    /*Fin:Christian Ramirez - REQ95070*/
                }
            }
            #endregion
        }

        private void MostrarPopupPorTipoCalificacion(int tipoCalificacion, string gradoColegio)
        {
            #region Tipo Calificacion => Numerico
            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO)
            {
                lblPopupTipoCalificacionHeader.Text = $"Tipo de Calificación Numérico - { gradoColegio }";
                lblPopupTipoCalificacionGrado.Text = gradoColegio;
                divNotasNumerico.Attributes.Add("style", "display:contents");
                divNotasLetra.Attributes.Add("style", "display:none");
                trPopupAvisoLetra.Visible = false;
            }
            #endregion

            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS)
            {
                lblPopupTipoCalificacionHeader.Text = $"Tipo de Calificación Letras - { gradoColegio }";
                lblPopupTipoCalificacionGrado.Text = gradoColegio;
                divNotasNumerico.Attributes.Add("style", "display:none");
                divNotasLetra.Attributes.Add("style", "display:block; height:220px; overflow-y:scroll;");
                trPopupAvisoLetra.Visible = true;
            }

            mpePopupTipoCalificacion.Show();
        }

        private void LimpiarControlesRendNotasNumerico(string grado)
        {
            if (grado == UIConstantes.Grado._Tercero)
            {
                txtOrdenMeritoTercero.Text = string.Empty;
                txtNroAlmunosTercero.Text = string.Empty;
                txtNotaMateTercero.Text = string.Empty;
                txtNotaLengTercero.Text = string.Empty;
            }

            if (grado == UIConstantes.Grado._Cuarto)
            {
                txtOrdenMeritoCuarto.Text = string.Empty;
                txtNroAlmunosCuarto.Text = string.Empty;
                txtNotaMateCuarto.Text = string.Empty;
                txtNotaLengCuarto.Text = string.Empty;
            }

            if (grado == UIConstantes.Grado._Quinto)
            {
                txtOrdenMeritoQuinto.Text = string.Empty;
                txtNroAlmunosQuinto.Text = string.Empty;
                txtNotaMateQuinto.Text = string.Empty;
                txtNotaLengQuinto.Text = string.Empty;
            }
        }

        private void LimpiarControlesRendNotasLetra(string grado)
        {
            if (grado == UIConstantes.Grado._Tercero)
            {
                #region Datos Tercero
                CT01CodigoTercero.Text = string.Empty;
                CT01Tercero.Text = string.Empty;
                CT02CodigoTercero.Text = string.Empty;
                CT02Tercero.Text = string.Empty;
                CT03CodigoTercero.Text = string.Empty;
                CT03Tercero.Text = string.Empty;

                CS01CodigoTercero.Text = string.Empty;
                CS01Tercero.Text = string.Empty;
                CS02CodigoTercero.Text = string.Empty;
                CS02Tercero.Text = string.Empty;
                CS03CodigoTercero.Text = string.Empty;
                CS03Tercero.Text = string.Empty;

                CTR01CodigoTercero.Text = string.Empty;
                CTR01Tercero.Text = string.Empty;
                CTR02CodigoTercero.Text = string.Empty;
                CTR02Tercero.Text = string.Empty;

                CL01CodigoTercero.Text = string.Empty;
                CL01Tercero.Text = string.Empty;
                CL02CodigoTercero.Text = string.Empty;
                CL02Tercero.Text = string.Empty;
                CL03CodigoTercero.Text = string.Empty;
                CL03Tercero.Text = string.Empty;

                MA01CodigoTercero.Text = string.Empty;
                MA01Tercero.Text = string.Empty;
                MA02CodigoTercero.Text = string.Empty;
                MA02Tercero.Text = string.Empty;
                MA03CodigoTercero.Text = string.Empty;
                MA03Tercero.Text = string.Empty;
                MA04CodigoTercero.Text = string.Empty;
                MA04Tercero.Text = string.Empty;
                #endregion
            }

            if (grado == UIConstantes.Grado._Cuarto)
            {
                #region Datos Cuarto
                CT01CodigoCuarto.Text = string.Empty;
                CT01Cuarto.Text = string.Empty;
                CT02CodigoCuarto.Text = string.Empty;
                CT02Cuarto.Text = string.Empty;
                CT03CodigoCuarto.Text = string.Empty;
                CT03Cuarto.Text = string.Empty;

                CS01CodigoCuarto.Text = string.Empty;
                CS01Cuarto.Text = string.Empty;
                CS02CodigoCuarto.Text = string.Empty;
                CS02Cuarto.Text = string.Empty;
                CS03CodigoCuarto.Text = string.Empty;
                CS03Cuarto.Text = string.Empty;

                CTR01CodigoCuarto.Text = string.Empty;
                CTR01Cuarto.Text = string.Empty;
                CTR02CodigoCuarto.Text = string.Empty;
                CTR02Cuarto.Text = string.Empty;

                CL01CodigoCuarto.Text = string.Empty;
                CL01Cuarto.Text = string.Empty;
                CL02CodigoCuarto.Text = string.Empty;
                CL02Cuarto.Text = string.Empty;
                CL03CodigoCuarto.Text = string.Empty;
                CL03Cuarto.Text = string.Empty;

                MA01CodigoCuarto.Text = string.Empty;
                MA01Cuarto.Text = string.Empty;
                MA02CodigoCuarto.Text = string.Empty;
                MA02Cuarto.Text = string.Empty;
                MA03CodigoCuarto.Text = string.Empty;
                MA03Cuarto.Text = string.Empty;
                MA04CodigoCuarto.Text = string.Empty;
                MA04Cuarto.Text = string.Empty;
                #endregion
            }

            if (grado == UIConstantes.Grado._Quinto)
            {
                CT01CodigoQuinto.Text = string.Empty;
                CT01Quinto.Text = string.Empty;
                CT02CodigoQuinto.Text = string.Empty;
                CT02Quinto.Text = string.Empty;
                CT03CodigoQuinto.Text = string.Empty;
                CT03Quinto.Text = string.Empty;

                CS01CodigoQuinto.Text = string.Empty;
                CS01Quinto.Text = string.Empty;
                CS02CodigoQuinto.Text = string.Empty;
                CS02Quinto.Text = string.Empty;
                CS03CodigoQuinto.Text = string.Empty;
                CS03Quinto.Text = string.Empty;

                CTR01CodigoQuinto.Text = string.Empty;
                CTR01Quinto.Text = string.Empty;
                CTR02CodigoQuinto.Text = string.Empty;
                CTR02Quinto.Text = string.Empty;

                CL01CodigoQuinto.Text = string.Empty;
                CL01Quinto.Text = string.Empty;
                CL02CodigoQuinto.Text = string.Empty;
                CL02Quinto.Text = string.Empty;
                CL03CodigoQuinto.Text = string.Empty;
                CL03Quinto.Text = string.Empty;

                MA01CodigoQuinto.Text = string.Empty;
                MA01Quinto.Text = string.Empty;
                MA02CodigoQuinto.Text = string.Empty;
                MA02Quinto.Text = string.Empty;
                MA03CodigoQuinto.Text = string.Empty;
                MA03Quinto.Text = string.Empty;
                MA04CodigoQuinto.Text = string.Empty;
                MA04Quinto.Text = string.Empty;
            }
        }

        private void LimpiarControlesRendNotasPopup()
        {
            txtOrdenMeritoPopup.Text = string.Empty;
            txtNroAlmunosPopup.Text = string.Empty;
            ddlNotaMatePopup.SelectedIndex = 0;
            ddlNotaLengPopup.SelectedIndex = 0;
            lblPopupTipoCalificacionGrado.Text = string.Empty;
            lblPopupTipoCalificacionHeader.Text = string.Empty;

            CT01Popup.SelectedIndex = 0;
            CT02Popup.SelectedIndex = 0;
            CT03Popup.SelectedIndex = 0;

            CS01Popup.SelectedIndex = 0;
            CS02Popup.SelectedIndex = 0;
            CS03Popup.SelectedIndex = 0;

            CTR01Popup.SelectedIndex = 0;
            CTR02Popup.SelectedIndex = 0;

            CL01Popup.SelectedIndex = 0;
            CL02Popup.SelectedIndex = 0;
            CL03Popup.SelectedIndex = 0;

            MA01Popup.SelectedIndex = 0;
            MA02Popup.SelectedIndex = 0;
            MA03Popup.SelectedIndex = 0;
            MA04Popup.SelectedIndex = 0;
        }

        protected void btnGuardarPopupTipoCalificacion_Click(object sender, EventArgs e)
        {
            string grado = lblPopupTipoCalificacionGrado.Text;
            int tipoCalificacion = 0;

            if (grado == UIConstantes.Grado._Tercero)
                tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionTercero.SelectedValue);

            if (grado == UIConstantes.Grado._Cuarto)
                tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionCuarto.SelectedValue);

            if (grado == UIConstantes.Grado._Quinto)
                tipoCalificacion = Convert.ToInt32(ddlTipoCalificacionQuinto.SelectedValue);

            bool rpta = GuardarDelPopupNotasPorTipoCalificacionYGrado(tipoCalificacion, grado);

            if (rpta)
            {
                LimpiarControlesRendNotasPopup();
                mpePopupTipoCalificacion.Hide();
            }
        }

        private bool GuardarDelPopupNotasPorTipoCalificacionYGrado(int tipoCalificacion, string grado)
        {
            bool rpta = true;

            #region Guardar notas del popup => Numerico
            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO)
            {
                string ordenMeritoPopup = txtOrdenMeritoPopup.Text;
                string nroAlmunosPopup = txtNroAlmunosPopup.Text;
                string notaMatePopup = ddlNotaMatePopup.SelectedValue;
                string notaLengPopup = ddlNotaLengPopup.SelectedValue;

                if (string.IsNullOrEmpty(ordenMeritoPopup) || string.IsNullOrEmpty(nroAlmunosPopup)
                    || notaMatePopup.Equals("0") || notaLengPopup.Equals("0"))
                {
                    rpta = false;
                }
                else
                {
                    if (grado == UIConstantes.Grado._Tercero)
                    {
                        txtOrdenMeritoTercero.Text = txtOrdenMeritoPopup.Text;
                        txtNroAlmunosTercero.Text = txtNroAlmunosPopup.Text;
                        txtNotaMateTercero.Text = ddlNotaMatePopup.SelectedValue;
                        txtNotaLengTercero.Text = ddlNotaLengPopup.SelectedValue;
                        divNumericoTercero.Visible = true;
                        divLetraTercero.Visible = false;
                    }

                    if (grado == UIConstantes.Grado._Cuarto)
                    {
                        txtOrdenMeritoCuarto.Text = txtOrdenMeritoPopup.Text;
                        txtNroAlmunosCuarto.Text = txtNroAlmunosPopup.Text;
                        txtNotaMateCuarto.Text = ddlNotaMatePopup.SelectedValue;
                        txtNotaLengCuarto.Text = ddlNotaLengPopup.SelectedValue;
                        divNumericoCuarto.Visible = true;
                        divLetraCuarto.Visible = false;
                    }

                    if (grado == UIConstantes.Grado._Quinto)
                    {
                        txtOrdenMeritoQuinto.Text = txtOrdenMeritoPopup.Text;
                        txtNroAlmunosQuinto.Text = txtNroAlmunosPopup.Text;
                        txtNotaMateQuinto.Text = ddlNotaMatePopup.SelectedValue;
                        txtNotaLengQuinto.Text = ddlNotaLengPopup.SelectedValue;
                        divNumericoQuinto.Visible = true;
                        divLetraQuinto.Visible = false;
                    }

                    LimpiarControlesRendNotasLetra(grado);
                }

            }
            #endregion

            #region Guardar notas del popup => Letra
            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.LETRAS)
            {
                string cT01Popup = CT01Popup.SelectedValue;
                string cT02Popup = CT02Popup.SelectedValue;
                string cT03Popup = CT03Popup.SelectedValue;

                string cS01Popup = CS01Popup.SelectedValue;
                string cS02Popup = CS02Popup.SelectedValue;
                string cS03Popup = CS03Popup.SelectedValue;

                string cTR01Popup = CTR01Popup.SelectedValue;
                string cTR02Popup = CTR02Popup.SelectedValue;

                string cL01Popup = CL01Popup.SelectedValue;
                string cL02Popup = CL02Popup.SelectedValue;
                string cL03Popup = CL03Popup.SelectedValue;

                string mA01Popup = MA01Popup.SelectedValue;
                string mA02Popup = MA02Popup.SelectedValue;
                string mA03Popup = MA03Popup.SelectedValue;
                string mA04Popup = MA04Popup.SelectedValue;

                if (cT01Popup.Equals("0") || cT02Popup.Equals("0") || cT03Popup.Equals("0")
                    || cS01Popup.Equals("0") || cS02Popup.Equals("0") || cS03Popup.Equals("0")
                    || cTR01Popup.Equals("0") || cTR02Popup.Equals("0")
                    || cL01Popup.Equals("0") || cL02Popup.Equals("0") || cL03Popup.Equals("0")
                    || mA01Popup.Equals("0") || mA02Popup.Equals("0") || mA03Popup.Equals("0") || mA04Popup.Equals("0")
                    )
                {
                    rpta = false;
                }
                else
                {
                    if (grado == UIConstantes.Grado._Tercero)
                    {
                        #region Datos Tercero
                        CT01Tercero.Text = CT01Popup.SelectedItem.Text;
                        CT01CodigoTercero.Text = cT01Popup;
                        CT02Tercero.Text = CT02Popup.SelectedItem.Text;
                        CT02CodigoTercero.Text = cT02Popup;
                        CT03Tercero.Text = CT03Popup.SelectedItem.Text;
                        CT03CodigoTercero.Text = cT03Popup;

                        CS01Tercero.Text = CS01Popup.SelectedItem.Text;
                        CS01CodigoTercero.Text = cS01Popup;
                        CS02Tercero.Text = CS02Popup.SelectedItem.Text;
                        CS02CodigoTercero.Text = cS02Popup;
                        CS03Tercero.Text = CS03Popup.SelectedItem.Text;
                        CS03CodigoTercero.Text = cS03Popup;

                        CTR01Tercero.Text = CTR01Popup.SelectedItem.Text;
                        CTR01CodigoTercero.Text = cTR01Popup;
                        CTR02Tercero.Text = CTR02Popup.SelectedItem.Text;
                        CTR02CodigoTercero.Text = cTR02Popup;

                        CL01Tercero.Text = CL01Popup.SelectedItem.Text;
                        CL01CodigoTercero.Text = cL01Popup;
                        CL02Tercero.Text = CL02Popup.SelectedItem.Text;
                        CL02CodigoTercero.Text = cL02Popup;
                        CL03Tercero.Text = CL03Popup.SelectedItem.Text;
                        CL03CodigoTercero.Text = cL03Popup;

                        MA01Tercero.Text = MA01Popup.SelectedItem.Text;
                        MA01CodigoTercero.Text = mA01Popup;
                        MA02Tercero.Text = MA02Popup.SelectedItem.Text;
                        MA02CodigoTercero.Text = mA02Popup;
                        MA03Tercero.Text = MA03Popup.SelectedItem.Text;
                        MA03CodigoTercero.Text = mA03Popup;
                        MA04Tercero.Text = MA04Popup.SelectedItem.Text;
                        MA04CodigoTercero.Text = mA04Popup;
                        #endregion

                        divNumericoTercero.Visible = false;
                        divLetraTercero.Visible = true;
                    }

                    if (grado == UIConstantes.Grado._Cuarto)
                    {
                        #region Datos Cuarto
                        CT01Cuarto.Text = CT01Popup.SelectedItem.Text;
                        CT01CodigoCuarto.Text = cT01Popup;
                        CT02Cuarto.Text = CT02Popup.SelectedItem.Text;
                        CT02CodigoCuarto.Text = cT02Popup;
                        CT03Cuarto.Text = CT03Popup.SelectedItem.Text;
                        CT03CodigoCuarto.Text = cT03Popup;

                        CS01Cuarto.Text = CS01Popup.SelectedItem.Text;
                        CS01CodigoCuarto.Text = cS01Popup;
                        CS02Cuarto.Text = CS02Popup.SelectedItem.Text;
                        CS02CodigoCuarto.Text = cS02Popup;
                        CS03Cuarto.Text = CS03Popup.SelectedItem.Text;
                        CS03CodigoCuarto.Text = cS03Popup;

                        CTR01Cuarto.Text = CTR01Popup.SelectedItem.Text;
                        CTR01CodigoCuarto.Text = cTR01Popup;
                        CTR02Cuarto.Text = CTR02Popup.SelectedItem.Text;
                        CTR02CodigoCuarto.Text = cTR02Popup;

                        CL01Cuarto.Text = CL01Popup.SelectedItem.Text;
                        CL01CodigoCuarto.Text = cL01Popup;
                        CL02Cuarto.Text = CL02Popup.SelectedItem.Text;
                        CL02CodigoCuarto.Text = cL02Popup;
                        CL03Cuarto.Text = CL03Popup.SelectedItem.Text;
                        CL03CodigoCuarto.Text = cL03Popup;

                        MA01Cuarto.Text = MA01Popup.SelectedItem.Text;
                        MA01CodigoCuarto.Text = mA01Popup;
                        MA02Cuarto.Text = MA02Popup.SelectedItem.Text;
                        MA02CodigoCuarto.Text = mA02Popup;
                        MA03Cuarto.Text = MA03Popup.SelectedItem.Text;
                        MA03CodigoCuarto.Text = mA03Popup;
                        MA04Cuarto.Text = MA04Popup.SelectedItem.Text;
                        MA04CodigoCuarto.Text = mA04Popup;
                        #endregion

                        divNumericoCuarto.Visible = false;
                        divLetraCuarto.Visible = true;
                    }

                    if (grado == UIConstantes.Grado._Quinto)
                    {
                        #region Datos Quinto
                        CT01Quinto.Text = CT01Popup.SelectedItem.Text;
                        CT01CodigoQuinto.Text = cT01Popup;
                        CT02Quinto.Text = CT02Popup.SelectedItem.Text;
                        CT02CodigoQuinto.Text = cT02Popup;
                        CT03Quinto.Text = CT03Popup.SelectedItem.Text;
                        CT03CodigoQuinto.Text = cT03Popup;

                        CS01Quinto.Text = CS01Popup.SelectedItem.Text;
                        CS01CodigoQuinto.Text = cS01Popup;
                        CS02Quinto.Text = CS02Popup.SelectedItem.Text;
                        CS02CodigoQuinto.Text = cS02Popup;
                        CS03Quinto.Text = CS03Popup.SelectedItem.Text;
                        CS03CodigoQuinto.Text = cS03Popup;

                        CTR01Quinto.Text = CTR01Popup.SelectedItem.Text;
                        CTR01CodigoQuinto.Text = cTR01Popup;
                        CTR02Quinto.Text = CTR02Popup.SelectedItem.Text;
                        CTR02CodigoQuinto.Text = cTR02Popup;

                        CL01Quinto.Text = CL01Popup.SelectedItem.Text;
                        CL01CodigoQuinto.Text = cL01Popup;
                        CL02Quinto.Text = CL02Popup.SelectedItem.Text;
                        CL02CodigoQuinto.Text = cL02Popup;
                        CL03Quinto.Text = CL03Popup.SelectedItem.Text;
                        CL03CodigoQuinto.Text = cL03Popup;

                        MA01Quinto.Text = MA01Popup.SelectedItem.Text;
                        MA01CodigoQuinto.Text = mA01Popup;
                        MA02Quinto.Text = MA02Popup.SelectedItem.Text;
                        MA02CodigoQuinto.Text = mA02Popup;
                        MA03Quinto.Text = MA03Popup.SelectedItem.Text;
                        MA03CodigoQuinto.Text = mA03Popup;
                        MA04Quinto.Text = MA04Popup.SelectedItem.Text;
                        MA04CodigoQuinto.Text = mA04Popup;
                        #endregion

                        divNumericoQuinto.Visible = false;
                        divLetraQuinto.Visible = true;
                    }

                    LimpiarControlesRendNotasNumerico(grado);
                }

            }
            #endregion

            return rpta;
        }

        protected void btnCerrarPopupTipoCalificacion_Click(object sender, EventArgs e)
        {
            string tipoCalificacion = lblPopupTipoCalificacionGrado.Text;
            if (tipoCalificacion == UIConstantes.Grado._Tercero)
            {
                ddlTipoCalificacionTercero.SelectedIndex = 0;
                divNumericoTercero.Visible = false;
                divLetraTercero.Visible = false;
            }

            if (tipoCalificacion == UIConstantes.Grado._Cuarto)
            {
                ddlTipoCalificacionCuarto.SelectedIndex = 0;
                divNumericoCuarto.Visible = false;
                divLetraCuarto.Visible = false;
            }

            if (tipoCalificacion == UIConstantes.Grado._Quinto)
            {
                ddlTipoCalificacionQuinto.SelectedIndex = 0;
                divNumericoQuinto.Visible = false;
                divLetraQuinto.Visible = false;
            }

            LimpiarControlesRendNotasPopup();
            mpePopupTipoCalificacion.Hide();
        }

        private void CargarCantidadCompetencias (int? AplicanteId)
        {
            AplicanteBE oAplicanteBE = new AplicanteBE();
            oAplicanteBE.IdAplicante = AplicanteId;
            RecuperarNotaADsCompetenciaRegistrada(oAplicanteBE);
        }

        private void RecuperarNotaADsCompetenciaRegistrada(AplicanteBE oAplicanteBE)
        {
            int situacionAcadecmica = Convert.ToInt32(Session["SituacionAcademica"]);
            if (situacionAcadecmica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
            {
                RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();

                //Cuarto
                RendimientoAcademicoBE oRendimientoAcademicoBE = oRendimientoAcademicoBL.ObtenerNotasADsPorCompetenciaRegistrada_Cuarto(oAplicanteBE);

                if (oRendimientoAcademicoBE != null)
                {
                    txtTotalCompetenciasCuarto.Text = oRendimientoAcademicoBE.TotalCompentencias_Cuarto.ToString();
                    txtTotalADCuarto.Text = oRendimientoAcademicoBE.TotalCompetencia_AD_Cuarto.ToString();
                    txtTotalACuarto.Text = oRendimientoAcademicoBE.TotalCompetencia_A_Cuarto.ToString();
                    txtTotalBCuarto.Text = oRendimientoAcademicoBE.TotalCompetencia_B_Cuarto.ToString();
                    txtTotalCCuarto.Text = oRendimientoAcademicoBE.TotalCompetencia_C_Cuarto.ToString();
                }

                //Quinto

                oRendimientoAcademicoBE = oRendimientoAcademicoBL.ObtenerNotasADsPorCompetenciaRegistrada_Quinto(oAplicanteBE);

                if (oRendimientoAcademicoBE != null)
                {
                    txtTotalCompetenciasQuinto.Text = oRendimientoAcademicoBE.TotalCompentencias_Quinto.ToString();
                    txtTotalADQuinto.Text = oRendimientoAcademicoBE.TotalCompetencia_AD_Quinto.ToString();
                    txtTotalAQuinto.Text = oRendimientoAcademicoBE.TotalCompetencia_A_Quinto.ToString();
                    txtTotalBQuinto.Text = oRendimientoAcademicoBE.TotalCompetencia_B_Quinto.ToString();
                    txtTotalCQuinto.Text = oRendimientoAcademicoBE.TotalCompetencia_C_Quinto.ToString();
                }

                trCantidadCompetencias.Attributes.Add("style", "display:contents");
                HabilitarDeshabilitarTextoNotasCompetencia(Convert.ToBoolean(Session["Flag_Preformalizar"]));
                //btnEditarCantidadCompetencia.Visible = false;
                //spMensajeBotonEditar.Visible = false;
                //lblMensajeCantidadCompentenciaAD01.Visible = false;
                //lblMensajeModalidadPostulacion01.Visible = false;
            }
        }

        private void HabilitarDeshabilitarTextoNotasCompetencia(bool flag_Preformalizar)
        {
            if (flag_Preformalizar)
            {
                txtTotalADCuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalACuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalBCuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCCuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCompetenciasCuarto.CssClass = "txtCajaTexto Deshabilitado";

                txtTotalADQuinto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalAQuinto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalBQuinto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCQuinto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCompetenciasQuinto.CssClass = "txtCajaTexto Deshabilitado";


            }
            else
            {
                //Competencias
                //txtOrdenMeritoTercero_Competencias.CssClass = "txtCajaTexto Deshabilitado";
                //txtTotalAlumnosTercero_Competencias.CssClass = "txtCajaTexto Deshabilitado";

                txtTotalADCuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalACuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalBCuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCCuarto.CssClass = "txtCajaTexto Deshabilitado";
                txtTotalCompetenciasCuarto.CssClass = "txtCajaTexto Deshabilitado";

                txtTotalADQuinto.CssClass = "txtCajaTexto";
                txtTotalAQuinto.CssClass = "txtCajaTexto";
                txtTotalBQuinto.CssClass = "txtCajaTexto";
                txtTotalCQuinto.CssClass = "txtCajaTexto";
                txtTotalCompetenciasQuinto.CssClass = "txtCajaTexto";

                //txtTotalADQuinto.CssClass = "txtCajaTexto Deshabilitado";
                //txtTotalAQuinto.CssClass = "txtCajaTexto Deshabilitado";
                //txtTotalBQuinto.CssClass = "txtCajaTexto Deshabilitado";
                //txtTotalCQuinto.CssClass = "txtCajaTexto Deshabilitado";
                //txtTotalCompetenciasQuinto.CssClass = "txtCajaTexto Deshabilitado";

                //Rendimiento
                //txtOrdenMeritoTercero.CssClass = "txtCajaTexto Deshabilitado";
                //txtTotalAlumnosTercero.CssClass = "txtCajaTexto Deshabilitado";

                //txtOrdenMeritoCuarto.CssClass = "txtCajaTexto Deshabilitado";
                //txtTotalAlumnosCuarto.CssClass = "txtCajaTexto Deshabilitado";

                //txtOrdenMeritoQuinto.CssClass = "txtCajaTexto Deshabilitado";
                //txtTotalAlumnosQuinto.CssClass = "txtCajaTexto Deshabilitado";
            }

            //Competencias
            //txtOrdenMeritoTercero_Competencias.Enabled = condicion;
            //txtTotalAlumnosTercero_Competencias.Enabled = condicion;

            txtTotalADCuarto.Enabled = false;
            txtTotalACuarto.Enabled = false;
            txtTotalBCuarto.Enabled = false;
            txtTotalCCuarto.Enabled = false;
            txtTotalCompetenciasCuarto.Enabled = false;

            txtTotalADQuinto.Enabled = !flag_Preformalizar;
            txtTotalAQuinto.Enabled = !flag_Preformalizar;
            txtTotalBQuinto.Enabled = !flag_Preformalizar;
            txtTotalCQuinto.Enabled = !flag_Preformalizar;
            txtTotalCompetenciasQuinto.Enabled = !flag_Preformalizar;

            //Rendimiento
            //txtOrdenMeritoTercero.Enabled = condicion;
            //txtTotalAlumnosTercero.Enabled = condicion;

            //txtOrdenMeritoCuarto.Enabled = condicion;
            //txtTotalAlumnosCuarto.Enabled = condicion;

            //txtOrdenMeritoQuinto.Enabled = condicion;
            //txtTotalAlumnosQuinto.Enabled = condicion;

        }

        protected void imgActualizarCantidadCompetencias_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.GuardarCantidadCompetencias();
                imgActualizarCantidadCompetencias.Focus();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F20, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }        

        private void GuardarCantidadCompetencias()
        {
            if (Session["SituacionAcademica"].ToString() == "9")
            {
                AplicanteBE oAplicanteBE = null;
                AplicanteBL oAplicanteBL = null;

                oAplicanteBE = new AplicanteBE();
                Int32? AplicanteId = Convert.ToInt32(Session["AplicanteId"]);

                DataTable dtColegios = new DataTable();

                try
                {
                    oAplicanteBL = new AplicanteBL();

                    oAplicanteBE.IdAplicante = AplicanteId;
                    oAplicanteBE.RedId = Session["usrRedId"].ToString();
                    oAplicanteBE.ListaRendimientoAcademicoBE = ObtenerDatosCompentencia();

                    RendimientoAcademicoBL oRendimientoAcademicoBL = new RendimientoAcademicoBL();
                    int resultado = oRendimientoAcademicoBL.InsertarDatosFormVeinte_CantidadCompetencia(oAplicanteBE);

                    if (resultado > 0) 
                    {
                        this.lblmessage.Text = "Se actualizó la cantidad de competencias de quinto correctamente.";
                        this.mpeMostrarError.Show();
                    }
                    else
                    {
                        Session["EstadoEnvio"] = null;
                        Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                            [UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                    }

                }
                catch (Exception ex)
                {
                    UIHelper.EnviarCorreo(UIConstantes.Formularios.F21, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"])
                        , UIConvertNull.String(Session["AplicanteId"]));
                    Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                        [UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO] + "&descError=" + ex.Message.Replace("\n", ""), false);

                    throw ex;
                }
            }
        }

        private List<RendimientoAcademicoBE> ObtenerDatosCompentencia()
        {
            string TotalCompetencia_AD_Quinto = txtTotalADQuinto.Text;
            string TotalCompetencia_A_Quinto = txtTotalAQuinto.Text;
            string TotalCompetencia_B_Quinto = txtTotalBQuinto.Text;
            string TotalCompetencia_C_Quinto = txtTotalCQuinto.Text;
            string TotalCompetencia_Quinto = txtTotalCompetenciasQuinto.Text;

            return new List<RendimientoAcademicoBE>
            {
                new RendimientoAcademicoBE() {
                    TotalCompetencia_AD_Quinto = string.IsNullOrEmpty(TotalCompetencia_AD_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_AD_Quinto),
                    TotalCompetencia_A_Quinto = string.IsNullOrEmpty(TotalCompetencia_A_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_A_Quinto),
                    TotalCompetencia_B_Quinto = string.IsNullOrEmpty(TotalCompetencia_B_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_B_Quinto),
                    TotalCompetencia_C_Quinto = string.IsNullOrEmpty(TotalCompetencia_C_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_C_Quinto),
                    TotalCompentencias_Quinto = string.IsNullOrEmpty(TotalCompetencia_Quinto) ? 0 : Convert.ToInt32(TotalCompetencia_Quinto),
                }
            };
        }

        protected void imgGuardarDetalleCompRend_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.GuardarDetalleCompetenciasFormalizacion();
                imgGuardarDetalleCompRend.Focus();
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F20, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        private void GuardarDetalleCompetenciasFormalizacion()
        {
            if (Session["SituacionAcademica"].ToString() == "9")
            {
                bool validacionProcesar = true;

                AplicanteBE oAplicanteBE = new AplicanteBE()
                {
                    IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]),
                    ModalidadPostulacion = UIConvertNull.Int32(Session["ModPostulacion"])
                };

                
                ObtenerDatosInformacionAcademica(ref oAplicanteBE);
                ObtenerDatosLetrasParaRegistro(ref oAplicanteBE);
                AplicanteBL oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormVeninte_NotaLetra_Formalizacion(oAplicanteBE);

                if (operacionOK)
                {
                    this.lblmessage.Text = "Se registro el detalle de sus competencias de quinto correctamente.";
                    this.mpeMostrarError.Show();
                }
                else
                {
                    Session["EstadoEnvio"] = null;
                    Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                        [UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                }
            }
        }

        private string ValidarDetalleCompetencias()
        {
            string resultado = "";

            if(UIConvertNull.Int32(Session["SituacionAcademica"]) != 34)
            {
                //Validar Colegio
                if (ddlColegioQuinto.SelectedValue.ToString() == "0")
                {
                    resultado = "Es necesario seleccionar el colegio de quinto.";
                }

                //Validar Año
                if (ddlAnioLectivoQuinto.SelectedValue.ToString() == "0")
                {
                    resultado = "Es necesario seleccionar el año que curso quinto.";
                }

                //Validar Tipo Calificación
                if (ddlTipoCalificacionQuinto.SelectedValue.ToString() == "0")
                {
                    resultado = "Es necesario seleccionar el tipo de calificación de quinto.";
                }

                //Validando detalle competencias
                if (CT01Quinto.Text.ToString() == "" ||
                    CT02Quinto.Text.ToString() == "" ||
                    CT03Quinto.Text.ToString() == "" ||
                    CS01Quinto.Text.ToString() == "" ||
                    CS02Quinto.Text.ToString() == "" ||
                    CS03Quinto.Text.ToString() == "" ||
                    CL01Quinto.Text.ToString() == "" ||
                    CL02Quinto.Text.ToString() == "" ||
                    CL03Quinto.Text.ToString() == "" ||
                    MA01Quinto.Text.ToString() == "" ||
                    MA02Quinto.Text.ToString() == "" ||
                    MA03Quinto.Text.ToString() == "" ||
                    MA04Quinto.Text.ToString() == "" ||
                    CTR01Quinto.Text.ToString() == "" ||
                    CTR02Quinto.Text.ToString() == ""
                  )
                {
                    resultado = "Es necesario colocar el detalle de las competencias de quinto.";
                }
            }
            
            return resultado;
        }

        private AplicanteBE ObtenerDatosLetrasParaRegistro(ref AplicanteBE oAplicanteBE)
        {
            RendimientoAcademicoBE oRendimientoAcademicoBEQuinto = null;


            int tipoCalificacionQuinto = Convert.ToInt32(ddlTipoCalificacionQuinto.SelectedValue);

            oRendimientoAcademicoBEQuinto = new RendimientoAcademicoBE()
            {
                ApplicationId = oAplicanteBE.IdAplicante.Value,
                CodTipoCalificacion = tipoCalificacionQuinto,
                ApplicationEducationId = Convert.ToInt32(ddlColegioQuinto.SelectedValue),
                DegreeId = (int)UIConstantes.GRADO_PREGRADO.QUINTO_SECUNDARIA
            };
            oRendimientoAcademicoBEQuinto.ListaRendimientoAcademicoEvaluacionBE = ObtenerDatosLetrasParaRegistroQuintoEvaluacion();

            oAplicanteBE.ListaRendimientoAcademicoBE = new List<RendimientoAcademicoBE>();
            if (oRendimientoAcademicoBEQuinto != null) oAplicanteBE.ListaRendimientoAcademicoBE.Add(oRendimientoAcademicoBEQuinto);

            oAplicanteBE.RedId = Session["usrRedId"].ToString();
            return oAplicanteBE;
        }

        private List<RendimientoAcademicoEvaluacionBE> ObtenerDatosLetrasParaRegistroQuintoEvaluacion()
        {
            return new List<RendimientoAcademicoEvaluacionBE>()
            {
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciaTecnologia,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Diseña_y_construye_soluciones_tecnológicas_para_resolver_problemas,
                             CalificacionId = UIConvertNull.Int32(CT01CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciaTecnologia,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Explica_el_mundo_físico_basándose_en_conocimientos_sobre_los_seres_vivos_materia_y_energía_biodiversidad,
                             CalificacionId = UIConvertNull.Int32(CT02CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciaTecnologia,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Indaga_mediante_metodos_científicos_para_construir_sus_conocimientos,
                             CalificacionId = UIConvertNull.Int32(CT03CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciasSociales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Construye_interpretaciones_historicas,
                             CalificacionId = UIConvertNull.Int32(CS01CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciasSociales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Gestiona_responsablemente_el_espacio_y_el_ambiente,
                             CalificacionId = UIConvertNull.Int32(CS02CodigoQuinto.Text)
                        },
                /*Ini:Christian Ramirez - REQ95070*/
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciasSociales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Gestiona_responsablemente_los_recursos_económicos,
                             CalificacionId = UIConvertNull.Int32(CS03CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CompetenciasTransversales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Gestiona_su_aprendizaje_de_manera_autonoma,
                             CalificacionId = UIConvertNull.Int32(CTR01CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CompetenciasTransversales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Se_desenvuelve_en_los_entornos_virtuales_generados_por_las_TIC,
                             CalificacionId = UIConvertNull.Int32(CTR02CodigoQuinto.Text)
                        },
                /*Fin:Christian Ramirez - REQ95070*/
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.ComunicacionLenguaMaterna,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Escribe_diversos_tipos_de_texto,
                             CalificacionId = UIConvertNull.Int32(CL01CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.ComunicacionLenguaMaterna,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Lee_diversos_tipos_de_textos_escritos,
                             CalificacionId = UIConvertNull.Int32(CL02CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.ComunicacionLenguaMaterna,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Se_comunica_oralmente,
                             CalificacionId = UIConvertNull.Int32(CL03CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_cantidad,
                             CalificacionId = UIConvertNull.Int32(MA01CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_forma_y_movimiento,
                             CalificacionId = UIConvertNull.Int32(MA02CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_gestion_de_datos_e_incertidumbre,
                             CalificacionId = UIConvertNull.Int32(MA03CodigoQuinto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_regularidad_equivalencia_y_cambio,
                             CalificacionId = UIConvertNull.Int32(MA04CodigoQuinto.Text)
                        },
            };
        }

        private AplicanteBE ObtenerDatosInformacionAcademica(ref AplicanteBE oAplicanteBE)
        {
            EducacionDetalleBE DetColegioQuinto = null;

            /*Ini:Christian Ramirez -REQ91569*/
            int tipoCalificacionQuinto = Convert.ToInt32(ddlTipoCalificacionQuinto.SelectedValue);
            /*Fin:Christian Ramirez -REQ91569*/

            #region Quinto Grado
            //if (tipoCalificacionQuinto == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO)
            //{
            if (DetColegioQuinto == null) DetColegioQuinto = new EducacionDetalleBE();
            DetColegioQuinto.IdApplication = UIConvertNull.Int32(Session["AplicanteId"].ToString());
            DetColegioQuinto.IdApplicationEducation = UIConvertNull.Int32(ddlColegioQuinto.SelectedValue);
            DetColegioQuinto.IdApplicationEducationEnroll = UIConvertNull.Int32(txtIdApplicationEducationEnrollQuinto.Text);
            DetColegioQuinto.IdGrado = Int32.Parse(UIConstantes.GRADO_PREGRADO.QUINTO_SECUNDARIA.ToString("D"));  //Quinto - 9

            String tmpFechaIniQuinto = "01/01/" + this.ddlAnioLectivoQuinto.SelectedItem.Text;
            CultureInfo culturaIniQuinto = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            DateTime tempFechaIniQuinto;

            if (DateTime.TryParse(tmpFechaIniQuinto, culturaIniQuinto, System.Globalization.DateTimeStyles.None, out tempFechaIniQuinto))
                DetColegioQuinto.FechaInicio = UIConvertNull.DateTime(tmpFechaIniQuinto);

            String tmpFechaFinQuinto = "31/12/" + this.ddlAnioLectivoQuinto.SelectedItem.Text;
            CultureInfo culturaFinQuinto = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            DateTime tempFechaFinQuinto;
            if (DateTime.TryParse(tmpFechaIniQuinto, culturaFinQuinto, System.Globalization.DateTimeStyles.None, out tempFechaFinQuinto))
                DetColegioQuinto.FechaFin = UIConvertNull.DateTime(tmpFechaFinQuinto);

            DetColegioQuinto.SituaAcademica = UIConvertNull.Int32(Session["SituacionAcademica"]);
            DetColegioQuinto.IdMerito = UIConvertNull.Int32(txtOrdenMeritoQuinto.Text);
            DetColegioQuinto.CantidadEstudiantes = UIConvertNull.Int32(txtNroAlmunosQuinto.Text);

            /*Ini:Christian Ramirez -REQ91569*/
            //if (ddlNotaMateQuinto.SelectedValue == UIConstantes._valorNotaOtro) RQ 89808
            string notaMateQuinto = txtNotaMateQuinto.Text;
            if (notaMateQuinto == "AD" || notaMateQuinto == "A" || notaMateQuinto == "B" || notaMateQuinto == "C")
                DetColegioQuinto.OtraNotaMateQuinto = notaMateQuinto;
            else
                DetColegioQuinto.NotaMateQuinto = UIConvertNull.Int32(notaMateQuinto == "0" ? null : notaMateQuinto);

            //if (ddlNotaLengQuinto.SelectedValue == UIConstantes._valorNotaOtro) RQ 89808
            string notaLengQuinto = txtNotaLengQuinto.Text.Trim();
            if (notaLengQuinto == "AD" || notaLengQuinto == "A" || notaLengQuinto == "B" || notaLengQuinto == "C")
                DetColegioQuinto.OtraNotaLengQuinto = notaLengQuinto;
            else
                DetColegioQuinto.NotaLengQuinto = UIConvertNull.Int32(notaLengQuinto == "0" ? null : notaLengQuinto);

            DetColegioQuinto.CodTipoCalificacion = Convert.ToInt32(ddlTipoCalificacionQuinto.SelectedValue);
            DetColegioQuinto.DescTipoCalificacion = ddlTipoCalificacionQuinto.SelectedItem.Text;
            /*Fin:Christian Ramirez -REQ91569*/

            DetColegioQuinto.Revision_Opid = Session["usrRedId"].ToString();
            //}
            #endregion

            //========================================================
            if (oAplicanteBE.LDetalleEducacion == null)
                oAplicanteBE.LDetalleEducacion = new System.Collections.Generic.List<EducacionDetalleBE>();

            if (oAplicanteBE != null)
            {
                if (oAplicanteBE.LDetalleEducacion == null) oAplicanteBE.LDetalleEducacion = new List<EducacionDetalleBE>();
                if (DetColegioQuinto != null) oAplicanteBE.LDetalleEducacion.Add(DetColegioQuinto);
            }
            return oAplicanteBE;
        }

        //FIN: JC.DelgadoV[RQ103950] Observaciones Pre Formalización
    }
}