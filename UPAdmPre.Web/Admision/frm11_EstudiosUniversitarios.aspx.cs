using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.Globalization;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm11_EstudiosUniversitarios : BasePage
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
                //Session["AplicanteId"] = 109323;

                if (!IsPostBack)
                {
                    //this.CargarTitulos();
                    this.LimpiarControles();
                    this.LlenarScripts();
                    this.MostrarOcultarBotones(false);
                    this.cargarCombos();
                    this.LimpiarControles();
                    this.CargarEstudiosUniversitariosRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F11, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
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
                    if (PaginaActual == UIConstantes.Formularios.F11)
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
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F11, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void BtnAgregaUniversidad2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlUniversidad2.Visible = true;
            this.btnQuitarUniversidad2.Visible = true;
            this.BtnAgregaUniversidad2.Visible = false;
            this.btnQuitarUniversidad3.Visible = false;
            this.BtnAgregaUniversidad3.Visible = true;
        }

        protected void btnQuitarUniversidad2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEducacion = UIConvertNull.Int32(txtIdEducacion2.Text);
            Int32? IdDetEducacion = UIConvertNull.Int32(txtIdDetalleEducacion2.Text);
            if (IdEducacion != null && IdDetEducacion != null)
            {
                this.EliminaEstudioUniversitarioRegistrado(IdEducacion, IdDetEducacion);
            }
            this.pnlUniversidad2.Visible = false;
            this.pnlUniversidad3.Visible = false;
            this.btnQuitarUniversidad2.Visible = false;
            this.BtnAgregaUniversidad2.Visible = true;
            this.btnQuitarUniversidad3.Visible = false;
            this.BtnAgregaUniversidad3.Visible = false;
        }

        protected void btnQuitarUniversidad3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEducacion = UIConvertNull.Int32(txtIdEducacion3.Text);
            Int32? IdDetEducacion = UIConvertNull.Int32(txtIdDetalleEducacion3.Text);
            if (IdEducacion != null && IdDetEducacion != null)
            {
                this.EliminaEstudioUniversitarioRegistrado(IdEducacion, IdDetEducacion);
            }
            this.pnlUniversidad3.Visible = false;
            this.btnQuitarUniversidad3.Visible = false;
            this.BtnAgregaUniversidad3.Visible = true;
        }

        protected void BtnAgregaUniversidad3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlUniversidad3.Visible = true;
            this.btnQuitarUniversidad3.Visible = true;
            this.BtnAgregaUniversidad3.Visible = false;
        }

        #endregion "Eventos"

        #region "Métodos Privados"

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
                    if (PaginaActual == UIConstantes.Formularios.F11)
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

            this.txtDireccionUniversidad1.Attributes.Add("readonly", "readonly");
            this.txtDireccionUniversidad2.Attributes.Add("readonly", "readonly");

            this.txtAnioDesdeUniversidad1.Attributes.Add("OnKeyPress", "soloNumeros(this);");
            this.txtAnioHastaUniversidad1.Attributes.Add("OnKeyPress", "soloNumeros(this);");
            this.txtAnioDesdeUniversidad2.Attributes.Add("OnKeyPress", "soloNumeros(this);");
            this.txtAnioHastaUniversidad2.Attributes.Add("OnKeyPress", "soloNumeros(this);");

            this.txtNroCreditosCursadosUni1.Attributes.Add("OnKeyPress", "soloNumeros(this);");
            this.txtNroCreditosAprobadosUni1.Attributes.Add("OnKeyPress", "soloNumeros(this);");
            this.txtNroCreditosCursadosUni2.Attributes.Add("OnKeyPress", "soloNumeros(this);");
            this.txtNroCreditosAprobadosUni2.Attributes.Add("OnKeyPress", "soloNumeros(this);");
        }

        private void MostrarOcultarBotones(Boolean blnAccion)
        {
            this.pnlUniversidad2.Visible = blnAccion;
            this.pnlUniversidad3.Visible = blnAccion;
            this.btnQuitarUniversidad2.Visible = blnAccion;
            this.btnQuitarUniversidad3.Visible = blnAccion;
            this.BtnAgregaUniversidad3.Visible = blnAccion;
        }

        private void cargarCombos()
        {
            this.cargarComboCarreras();
            this.cargarComboGrados();

        }

        private void cargarComboCarreras()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtCarreras = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.CARRERA_PREGRADO].Key, "", null);
                if (dtCarreras != null && dtCarreras.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlCarreraUniversidad1, dtCarreras.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlCarreraUniversidad2, dtCarreras.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlCarreraUniversidad3, dtCarreras.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarComboGrados()
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();
                DataTable dtGrados = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.GRADO_EPG].Key, "", null);
                if (dtGrados != null && dtGrados.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlGradoUniversidad1, dtGrados.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlGradoUniversidad2, dtGrados.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(this.ddlGradoUniversidad3, dtGrados.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod()]
        public static List<EducacionBE> getUniversidades(String term)
        {
            EducacionBL oEducacionBL = null;
            try
            {
                String txtDegreeId = HttpContext.Current.Session["ModPostulacion"].ToString();
                oEducacionBL = new EducacionBL();
                return oEducacionBL.ListarUniversidades(term, txtDegreeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarEstudiosUniversitariosRegistrados(Int32? AplicanteId)
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ObtenerUniversidadesRegistradas(AplicanteId);
                this.LLenarDatosUniversidadesRegistradas(oAplicanteBE);
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
            try
            {
                oAplicanteBE = new AplicanteBE();
                oAplicanteBE = ObtenerDatosUniversidad(oAplicanteBE);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormDoce_EstudiosUniversitarios(oAplicanteBE);
                if (operacionOK)
                {
                    Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                    oGeneralBL = new GeneralBL();
                    dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    {
                        PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F11)
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

        private AplicanteBE ObtenerDatosUniversidad(AplicanteBE oAplicanteBE)
        {
            EducacionBE Universidad1 = null;
            EducacionBE Universidad2 = null;
            EducacionBE Universidad3 = null;
            EducacionDetalleBE DetUniversidad1 = null;
            EducacionDetalleBE DetUniversidad2 = null;
            EducacionDetalleBE DetUniversidad3 = null;
            try
            {
                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"].ToString());
                oAplicanteBE.RedId = Session["usrRedId"].ToString();

                //======================================================================
                //Universidad 1
                //======================================================================
                if (txtNomUniversidad1.Text != null && txtNomUniversidad1.Text != String.Empty)
                {
                    Universidad1 = new EducacionBE();
                    if (this.txtIdEducacion1.Text != null && txtIdEducacion1.Text != String.Empty)
                    {
                        Universidad1.IdEducacion = UIConvertNull.Int32(txtIdEducacion1.Text);
                    }
                    else
                    {
                        Universidad1.IdApplicationEducation = null;
                    }

                    if (this.txtCodUniversidad1.Text != string.Empty)
                    {
                        if (Universidad1.Institucion == null)
                        {
                            Universidad1.Institucion = new InstitucionBE();
                        }
                        Universidad1.Institucion.Codigo = UIConvertNull.Int32(txtCodUniversidad1.Text);
                    }
                    if (this.txtNomUniversidad1.Text != null && this.txtNomUniversidad1.Text != string.Empty)
                    {
                        Universidad1.NombreInstitucion = this.txtNomUniversidad1.Text;
                    }
                    Universidad1.SeccionEnFormulario = UIConstantes.SECCION_EN_FORMULARIO.FORM_POSITION_1U;

                    ///Detalle de la Universidad
                    DetUniversidad1 = new EducacionDetalleBE();
                    DetUniversidad1.IdDetalleEducacion = UIConvertNull.Int32(txtIdDetalleEducacion1.Text);
                    DetUniversidad1.IdCarrera = UIConvertNull.Int32(ddlCarreraUniversidad1.SelectedValue);
                    DetUniversidad1.NombreCarrera = ddlCarreraUniversidad1.SelectedItem.Text;

                    string tmpFechaIni = "01/01/" + this.txtAnioDesdeUniversidad1.Text;
                    CultureInfo culturaIni = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaInicio;
                    if (DateTime.TryParse(tmpFechaIni, culturaIni, System.Globalization.DateTimeStyles.None, out tempFechaInicio))
                    {
                        DetUniversidad1.FechaInicio = tempFechaInicio;
                    }

                    string tmpFechaFin = "01/01/" + this.txtAnioHastaUniversidad1.Text;
                    CultureInfo culturaFin = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaFin;
                    if (DateTime.TryParse(tmpFechaFin, culturaFin, System.Globalization.DateTimeStyles.None, out tempFechaFin))
                    {
                        DetUniversidad1.FechaFin = tempFechaFin;
                    }

                    DetUniversidad1.CantidadCiclosCursados = UIConvertNull.Int32(txtNroCreditosCursadosUni1.Text);
                    DetUniversidad1.CantidadCreditosAprobados = UIConvertNull.Int32(txtNroCreditosAprobadosUni1.Text);
                    DetUniversidad1.IdGrado = UIConvertNull.Int32(ddlGradoUniversidad1.SelectedValue);
                    DetUniversidad1.NombreGrado = ddlGradoUniversidad1.SelectedItem.Text;

                    if (Universidad1 != null)
                    {
                        if (oAplicanteBE.LEducacion == null)
                        {
                            oAplicanteBE.LEducacion = new System.Collections.Generic.List<EducacionBE>();
                        }
                        if (DetUniversidad1 != null)
                        {
                            Universidad1.LDetalleEducacion = new List<EducacionDetalleBE>();
                            Universidad1.LDetalleEducacion.Add(DetUniversidad1);
                        }
                        oAplicanteBE.LEducacion.Add(Universidad1);
                    }
                }

                //======================================================================
                //Universidad 2
                //======================================================================
                if (txtNomUniversidad2.Text != null && txtNomUniversidad2.Text != String.Empty)
                {
                    Universidad2 = new EducacionBE();
                    if (this.txtIdEducacion2.Text != null && txtIdEducacion2.Text != String.Empty)
                    {
                        Universidad2.IdEducacion = UIConvertNull.Int32(txtIdEducacion2.Text);
                    }
                    else
                    {
                        Universidad2.IdApplicationEducation = null;
                    }

                    if (this.txtCodUniversidad2.Text != string.Empty)
                    {
                        if (Universidad2.Institucion == null)
                        {
                            Universidad2.Institucion = new InstitucionBE();
                        }
                        Universidad2.Institucion.Codigo = UIConvertNull.Int32(txtCodUniversidad2.Text);
                    }
                    if (this.txtNomUniversidad2.Text != null && this.txtNomUniversidad2.Text != string.Empty)
                    {
                        Universidad2.NombreInstitucion = this.txtNomUniversidad2.Text;
                    }
                    Universidad2.SeccionEnFormulario = UIConstantes.SECCION_EN_FORMULARIO.FORM_POSITION_2U;

                    ///Detalle de la Universidad
                    DetUniversidad2 = new EducacionDetalleBE();
                    DetUniversidad2.IdDetalleEducacion = UIConvertNull.Int32(txtIdDetalleEducacion2.Text);
                    DetUniversidad2.IdCarrera = UIConvertNull.Int32(ddlCarreraUniversidad2.SelectedValue);
                    DetUniversidad2.NombreCarrera = ddlCarreraUniversidad2.SelectedItem.Text;

                    string tmpFechaIni = "01/01/" + this.txtAnioDesdeUniversidad2.Text;
                    CultureInfo culturaIni = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaInicio;
                    if (DateTime.TryParse(tmpFechaIni, culturaIni, System.Globalization.DateTimeStyles.None, out tempFechaInicio))
                    {
                        DetUniversidad2.FechaInicio = tempFechaInicio;
                    }

                    string tmpFechaFin = "01/01/" + this.txtAnioHastaUniversidad2.Text;
                    CultureInfo culturaFin = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaFin;
                    if (DateTime.TryParse(tmpFechaFin, culturaFin, System.Globalization.DateTimeStyles.None, out tempFechaFin))
                    {
                        DetUniversidad2.FechaFin = tempFechaFin;
                    }

                    DetUniversidad2.CantidadCiclosCursados = UIConvertNull.Int32(txtNroCreditosCursadosUni2.Text);
                    DetUniversidad2.CantidadCreditosAprobados = UIConvertNull.Int32(txtNroCreditosAprobadosUni2.Text);
                    DetUniversidad2.IdGrado = UIConvertNull.Int32(ddlGradoUniversidad2.SelectedValue);
                    DetUniversidad2.NombreGrado = ddlGradoUniversidad2.SelectedItem.Text;

                    if (Universidad2 != null)
                    {
                        if (oAplicanteBE.LEducacion == null)
                        {
                            oAplicanteBE.LEducacion = new System.Collections.Generic.List<EducacionBE>();
                        }
                        if (DetUniversidad2 != null)
                        {
                            Universidad2.LDetalleEducacion = new List<EducacionDetalleBE>();
                            Universidad2.LDetalleEducacion.Add(DetUniversidad2);
                        }
                        oAplicanteBE.LEducacion.Add(Universidad2);
                    }
                }

                //======================================================================
                //Universidad 3
                //======================================================================
                if (txtNomUniversidad3.Text != null && txtNomUniversidad3.Text != String.Empty)
                {
                    Universidad3 = new EducacionBE();
                    if (this.txtIdEducacion3.Text != null && txtIdEducacion3.Text != String.Empty)
                    {
                        Universidad3.IdEducacion = UIConvertNull.Int32(txtIdEducacion3.Text);
                    }
                    else
                    {
                        Universidad3.IdApplicationEducation = null;
                    }

                    if (this.txtCodUniversidad3.Text != string.Empty)
                    {
                        if (Universidad3.Institucion == null)
                        {
                            Universidad3.Institucion = new InstitucionBE();
                        }
                        Universidad3.Institucion.Codigo = UIConvertNull.Int32(txtCodUniversidad3.Text);
                    }
                    if (this.txtNomUniversidad3.Text != null && this.txtNomUniversidad3.Text != string.Empty)
                    {
                        Universidad3.NombreInstitucion = this.txtNomUniversidad3.Text;
                    }
                    Universidad3.SeccionEnFormulario = UIConstantes.SECCION_EN_FORMULARIO.FORM_POSITION_3U;

                    ///Detalle de la Universidad
                    DetUniversidad3 = new EducacionDetalleBE();
                    DetUniversidad3.IdDetalleEducacion = UIConvertNull.Int32(txtIdDetalleEducacion3.Text);
                    DetUniversidad3.IdCarrera = UIConvertNull.Int32(ddlCarreraUniversidad3.SelectedValue);
                    DetUniversidad3.NombreCarrera = ddlCarreraUniversidad3.SelectedItem.Text;

                    string tmpFechaIni = "01/01/" + this.txtAnioDesdeUniversidad3.Text;
                    CultureInfo culturaIni = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaInicio;
                    if (DateTime.TryParse(tmpFechaIni, culturaIni, System.Globalization.DateTimeStyles.None, out tempFechaInicio))
                    {
                        DetUniversidad3.FechaInicio = tempFechaInicio;
                    }

                    string tmpFechaFin = "01/01/" + this.txtAnioHastaUniversidad3.Text;
                    CultureInfo culturaFin = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaFin;
                    if (DateTime.TryParse(tmpFechaFin, culturaFin, System.Globalization.DateTimeStyles.None, out tempFechaFin))
                    {
                        DetUniversidad3.FechaFin = tempFechaFin;
                    }

                    DetUniversidad3.CantidadCiclosCursados = UIConvertNull.Int32(txtNroCreditosCursadosUni3.Text);
                    DetUniversidad3.CantidadCreditosAprobados = UIConvertNull.Int32(txtNroCreditosAprobadosUni3.Text);
                    DetUniversidad3.IdGrado = UIConvertNull.Int32(ddlGradoUniversidad3.SelectedValue);
                    DetUniversidad3.NombreGrado = ddlGradoUniversidad3.SelectedItem.Text;

                    if (Universidad1 != null)
                    {
                        if (oAplicanteBE.LEducacion == null)
                        {
                            oAplicanteBE.LEducacion = new System.Collections.Generic.List<EducacionBE>();
                        }
                        if (DetUniversidad3 != null)
                        {
                            Universidad3.LDetalleEducacion = new List<EducacionDetalleBE>();
                            Universidad3.LDetalleEducacion.Add(DetUniversidad3);
                        }
                        oAplicanteBE.LEducacion.Add(Universidad3);
                    }
                }
                return oAplicanteBE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosUniversidadesRegistradas(AplicanteBE oAplicanteBE)
        {
            try
            {
                if (oAplicanteBE != null && oAplicanteBE.LEducacion != null)
                {
                    List<EducacionBE> LEducacionBE = (from itemEduca in oAplicanteBE.LEducacion
                                                      orderby itemEduca.IdEducacion
                                                      select itemEduca).ToList<EducacionBE>();
                    if (LEducacionBE != null)
                    {
                        for (int indice = 0; indice < LEducacionBE.Count; indice++)
                        {
                            EducacionBE recEducacion = LEducacionBE[indice];
                            switch (indice)
                            {
                                case 0:
                                    this.txtCodUniversidad1.Text = recEducacion.Institucion.Codigo.ToString();
                                    this.txtNomUniversidad1.Text = recEducacion.NombreInstitucion;
                                    this.txtDireccionUniversidad1.Text = recEducacion.Institucion.Direccion + UIConstantes._valorSignoGuion + recEducacion.Institucion.Distrito + UIConstantes._valorSignoGuion + recEducacion.Institucion.Provincia;
                                    this.txtIdEducacion1.Text = UIConvertNull.String(recEducacion.IdEducacion); //ApplicationEducation

                                    if (recEducacion.LDetalleEducacion != null && recEducacion.LDetalleEducacion.Count > 0)
                                    {
                                        EducacionDetalleBE oEducacionDetalleBE = recEducacion.LDetalleEducacion[0];
                                        this.txtIdDetalleEducacion1.Text = UIConvertNull.String(oEducacionDetalleBE.IdDetalleEducacion);
                                        this.ddlCarreraUniversidad1.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdCarrera);
                                        this.txtAnioDesdeUniversidad1.Text = oEducacionDetalleBE.FechaInicio.HasValue ? oEducacionDetalleBE.FechaInicio.Value.ToString("yyyy") : String.Empty;
                                        this.txtAnioHastaUniversidad1.Text = oEducacionDetalleBE.FechaFin.HasValue ? oEducacionDetalleBE.FechaFin.Value.ToString("yyyy") : String.Empty;
                                        this.txtNroCreditosCursadosUni1.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCiclosCursados);
                                        this.txtNroCreditosAprobadosUni1.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCreditosAprobados);
                                        this.ddlGradoUniversidad1.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdGrado);
                                    }
                                    this.BtnAgregaUniversidad2.Visible = true;
                                    break;
                                case 1:
                                    this.txtCodUniversidad2.Text = recEducacion.Institucion.Codigo.ToString();
                                    this.txtNomUniversidad2.Text = recEducacion.NombreInstitucion;
                                    this.txtDireccionUniversidad2.Text = recEducacion.Institucion.Direccion + UIConstantes._valorSignoGuion + recEducacion.Institucion.Distrito + UIConstantes._valorSignoGuion + recEducacion.Institucion.Provincia;
                                    this.txtIdEducacion2.Text = UIConvertNull.String(recEducacion.IdEducacion);

                                    if (recEducacion.LDetalleEducacion != null && recEducacion.LDetalleEducacion.Count > 0)
                                    {
                                        EducacionDetalleBE oEducacionDetalleBE = recEducacion.LDetalleEducacion[1];
                                        this.txtIdDetalleEducacion2.Text = UIConvertNull.String(oEducacionDetalleBE.IdDetalleEducacion);
                                        this.ddlCarreraUniversidad2.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdCarrera);
                                        this.txtAnioDesdeUniversidad2.Text = oEducacionDetalleBE.FechaInicio.HasValue ? oEducacionDetalleBE.FechaInicio.Value.ToString("yyyy") : String.Empty;
                                        this.txtAnioHastaUniversidad2.Text = oEducacionDetalleBE.FechaFin.HasValue ? oEducacionDetalleBE.FechaFin.Value.ToString("yyyy") : String.Empty;
                                        this.txtNroCreditosCursadosUni2.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCiclosCursados);
                                        this.txtNroCreditosAprobadosUni2.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCreditosAprobados);
                                        this.ddlGradoUniversidad2.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdGrado);
                                    }
                                    this.BtnAgregaUniversidad2.Visible = false;
                                    this.btnQuitarUniversidad2.Visible = true;
                                    this.BtnAgregaUniversidad3.Visible = true;
                                    this.btnQuitarUniversidad3.Visible = false;
                                    this.pnlUniversidad2.Visible = true;
                                    break;
                                case 2:
                                    this.txtCodUniversidad3.Text = recEducacion.Institucion.Codigo.ToString();
                                    this.txtNomUniversidad3.Text = recEducacion.NombreInstitucion;
                                    this.txtDireccionUniversidad3.Text = recEducacion.Institucion.Direccion + UIConstantes._valorSignoGuion + recEducacion.Institucion.Distrito + UIConstantes._valorSignoGuion + recEducacion.Institucion.Provincia;
                                    this.txtIdEducacion3.Text = UIConvertNull.String(recEducacion.IdEducacion);

                                    if (recEducacion.LDetalleEducacion != null && recEducacion.LDetalleEducacion.Count > 0)
                                    {
                                        EducacionDetalleBE oEducacionDetalleBE = recEducacion.LDetalleEducacion[2];
                                        this.txtIdDetalleEducacion3.Text = UIConvertNull.String(oEducacionDetalleBE.IdDetalleEducacion);
                                        this.ddlCarreraUniversidad3.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdCarrera);
                                        this.txtAnioDesdeUniversidad3.Text = oEducacionDetalleBE.FechaInicio.HasValue ? oEducacionDetalleBE.FechaInicio.Value.ToString("yyyy") : String.Empty;
                                        this.txtAnioHastaUniversidad3.Text = oEducacionDetalleBE.FechaFin.HasValue ? oEducacionDetalleBE.FechaFin.Value.ToString("yyyy") : String.Empty;
                                        this.txtNroCreditosCursadosUni3.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCiclosCursados);
                                        this.txtNroCreditosAprobadosUni3.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCreditosAprobados);
                                        this.ddlGradoUniversidad3.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdGrado);
                                    }
                                    this.BtnAgregaUniversidad2.Visible = false;
                                    this.btnQuitarUniversidad2.Visible = true;
                                    this.BtnAgregaUniversidad3.Visible = false;
                                    this.btnQuitarUniversidad3.Visible = true;
                                    this.pnlUniversidad3.Visible = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDetalleUniversidadesRegistradas(AplicanteBE oAplicanteBE)
        {
            try
            {
                if (oAplicanteBE != null && oAplicanteBE.LDetalleEducacion != null)
                {
                    List<EducacionDetalleBE> LEducacionDetalleBE = (from itemEduca in oAplicanteBE.LDetalleEducacion
                                                                    select itemEduca).ToList<EducacionDetalleBE>();

                    if (LEducacionDetalleBE != null)
                    {
                        for (int indice = 0; indice < LEducacionDetalleBE.Count; indice++)
                        {
                            EducacionDetalleBE oEducacionDetalleBE = LEducacionDetalleBE[indice];
                            switch (indice)
                            {
                                case 0:
                                    this.ddlCarreraUniversidad1.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdCarrera);
                                    this.txtAnioDesdeUniversidad1.Text = oEducacionDetalleBE.FechaInicio.HasValue ? oEducacionDetalleBE.FechaInicio.Value.ToString("yyyy") : String.Empty;
                                    this.txtAnioHastaUniversidad1.Text = oEducacionDetalleBE.FechaFin.HasValue ? oEducacionDetalleBE.FechaFin.Value.ToString("yyyy") : String.Empty;
                                    this.txtNroCreditosCursadosUni1.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCiclosCursados);
                                    this.txtNroCreditosAprobadosUni1.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCreditosAprobados);
                                    this.ddlGradoUniversidad1.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdGrado);
                                    break;
                                case 1:
                                    this.ddlCarreraUniversidad2.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdCarrera);
                                    this.txtAnioDesdeUniversidad2.Text = oEducacionDetalleBE.FechaInicio.HasValue ? oEducacionDetalleBE.FechaInicio.Value.ToString("yyyy") : String.Empty;
                                    this.txtAnioHastaUniversidad2.Text = oEducacionDetalleBE.FechaFin.HasValue ? oEducacionDetalleBE.FechaFin.Value.ToString("yyyy") : String.Empty;
                                    this.txtNroCreditosCursadosUni2.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCiclosCursados);
                                    this.txtNroCreditosAprobadosUni2.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCreditosAprobados);
                                    this.ddlGradoUniversidad2.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdGrado);
                                    break;
                                case 2:
                                    this.ddlCarreraUniversidad3.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdCarrera);
                                    this.txtAnioDesdeUniversidad3.Text = oEducacionDetalleBE.FechaInicio.HasValue ? oEducacionDetalleBE.FechaInicio.Value.ToString("yyyy") : String.Empty;
                                    this.txtAnioHastaUniversidad3.Text = oEducacionDetalleBE.FechaFin.HasValue ? oEducacionDetalleBE.FechaFin.Value.ToString("yyyy") : String.Empty;
                                    this.txtNroCreditosCursadosUni3.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCiclosCursados);
                                    this.txtNroCreditosAprobadosUni3.Text = UIConvertNull.String(oEducacionDetalleBE.CantidadCreditosAprobados);
                                    this.ddlGradoUniversidad3.SelectedValue = UIConvertNull.String(oEducacionDetalleBE.IdGrado);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Boolean EliminaEstudioUniversitarioRegistrado(Int32? IdEducacion, Int32? IdDetEducacion)
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            AplicanteBL oAplicanteBL = null;
            Boolean Resultado = false;
            try
            {
                oAplicanteBL = new AplicanteBL();
                Resultado = oAplicanteBL.EliminaEstudioUniversitarioRegistrado(IdEducacion, IdDetEducacion, AplicanteId);
                if (Resultado == true)
                {
                    this.LimpiarControles();
                    this.MostrarOcultarBotones(false);
                    this.CargarEstudiosUniversitariosRegistrados(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
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
            this.txtNomUniversidad1.Text = UIConstantes._valorCadenaVacia;
            this.txtDireccionUniversidad1.Text = UIConstantes._valorCadenaVacia;
            this.ddlCarreraUniversidad1.ClearSelection();
            this.txtAnioDesdeUniversidad1.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioHastaUniversidad1.Text = UIConstantes._valorCadenaVacia;
            this.txtNroCreditosCursadosUni1.Text = UIConstantes._valorCadenaVacia;
            this.txtNroCreditosAprobadosUni1.Text = UIConstantes._valorCadenaVacia;
            this.ddlGradoUniversidad1.ClearSelection();
            this.txtCodUniversidad1.Text = UIConstantes._valorCadenaVacia;
            this.txtIdEducacion1.Text = UIConstantes._valorCadenaVacia;
            this.txtIdDetalleEducacion1.Text = UIConstantes._valorCadenaVacia;

            this.txtNomUniversidad2.Text = UIConstantes._valorCadenaVacia;
            this.txtDireccionUniversidad2.Text = UIConstantes._valorCadenaVacia;
            this.ddlCarreraUniversidad2.ClearSelection();
            this.txtAnioDesdeUniversidad2.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioHastaUniversidad2.Text = UIConstantes._valorCadenaVacia;
            this.txtNroCreditosCursadosUni2.Text = UIConstantes._valorCadenaVacia;
            this.txtNroCreditosAprobadosUni2.Text = UIConstantes._valorCadenaVacia;
            this.ddlGradoUniversidad2.ClearSelection();
            this.txtCodUniversidad2.Text = UIConstantes._valorCadenaVacia;
            this.txtIdEducacion2.Text = UIConstantes._valorCadenaVacia;
            this.txtIdDetalleEducacion2.Text = UIConstantes._valorCadenaVacia;

            this.txtNomUniversidad3.Text = UIConstantes._valorCadenaVacia;
            this.txtDireccionUniversidad3.Text = UIConstantes._valorCadenaVacia;
            this.ddlCarreraUniversidad3.ClearSelection();
            this.txtAnioDesdeUniversidad3.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioHastaUniversidad3.Text = UIConstantes._valorCadenaVacia;
            this.txtNroCreditosCursadosUni3.Text = UIConstantes._valorCadenaVacia;
            this.txtNroCreditosAprobadosUni3.Text = UIConstantes._valorCadenaVacia;
            this.ddlGradoUniversidad3.ClearSelection();
            this.txtCodUniversidad3.Text = UIConstantes._valorCadenaVacia;
            this.txtIdEducacion3.Text = UIConstantes._valorCadenaVacia;
            this.txtIdDetalleEducacion3.Text = UIConstantes._valorCadenaVacia;
        }

        #endregion "Métodos Privados"
    }
}