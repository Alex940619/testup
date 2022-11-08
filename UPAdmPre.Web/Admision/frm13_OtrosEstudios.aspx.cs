using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Globalization;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm13_OtrosEstudios : BasePage
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
                    this.MostrarOcultarBotones(false);
                    this.CargarEstudiosRegistradas(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F13, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
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
                    if (PaginaActual == UIConstantes.Formularios.F13)
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
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F13, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void btnAgregaEstudios2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlEstudios2.Visible = true;
            this.btnQuitarEstudios2.Visible = true;
            this.btnAgregaEstudios2.Visible = false;
            this.btnQuitarEstudios3.Visible = false;
            this.btnAgregaEstudios3.Visible = true;
        }

        protected void btnQuitarEstudios2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEducacion = UIConvertNull.Int32(txtIdEducacion2.Text);
            Int32? IdDetEducacion = UIConvertNull.Int32(txtIdDetalleEducacion2.Text);
            if (IdEducacion != null && IdDetEducacion != null)
            {
                this.EliminaOtrosEstudiosRegistrado(IdEducacion, IdDetEducacion);
            }
            this.pnlEstudios2.Visible = false;
            this.pnlEstudios3.Visible = false;
            this.btnQuitarEstudios2.Visible = false;
            this.btnAgregaEstudios2.Visible = true;
            this.btnQuitarEstudios3.Visible = false;
            this.btnAgregaEstudios3.Visible = false;
        }

        protected void btnQuitarEstudios3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEducacion = UIConvertNull.Int32(txtIdEducacion3.Text);
            Int32? IdDetEducacion = UIConvertNull.Int32(txtIdDetalleEducacion3.Text);
            if (IdEducacion != null && IdDetEducacion != null)
            {
                this.EliminaOtrosEstudiosRegistrado(IdEducacion, IdDetEducacion);
            }
            this.pnlEstudios3.Visible = false;
            this.btnQuitarEstudios3.Visible = false;
            this.btnAgregaEstudios3.Visible = true;
        }

        protected void btnAgregaEstudios3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlEstudios3.Visible = true;
            this.btnQuitarEstudios3.Visible = true;
            this.btnAgregaEstudios3.Visible = false;
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
                    if (PaginaActual == UIConstantes.Formularios.F13)
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
            this.pnlEstudios2.Visible = blnAccion;
            this.pnlEstudios3.Visible = blnAccion;
            this.btnQuitarEstudios2.Visible = blnAccion;
            this.btnQuitarEstudios3.Visible = blnAccion;
            this.btnAgregaEstudios3.Visible = blnAccion;
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
                oAplicanteBE = this.obtenerDatosOtrosEstudios(oAplicanteBE);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormCatorce_OtrosEstudios(oAplicanteBE);
                if (operacionOK)
                {
                    Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                    oGeneralBL = new GeneralBL();
                    dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    {
                        PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F13)
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

        private AplicanteBE obtenerDatosOtrosEstudios(AplicanteBE oAplicanteBE)
        {
            EducacionBE oEducacionBE1 = null;
            EducacionBE oEducacionBE2 = null;
            EducacionBE oEducacionBE3 = null;
            EducacionDetalleBE oEducacionDetalleBE1 = null;
            EducacionDetalleBE oEducacionDetalleBE2 = null;
            EducacionDetalleBE oEducacionDetalleBE3 = null;
            try
            {
                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);

                //======================================================================
                // Otros Estudios 1
                //======================================================================
                oEducacionBE1 = new EducacionBE();
                if (this.txtCursoPrograma1.Text != null && txtCursoPrograma1.Text != String.Empty)
                {
                    oEducacionBE1.IdEducacion = UIConvertNull.Int32(txtIdEducacion1.Text);
                    oEducacionBE1.NombreInstitucion = txtNomInstitucion1.Text;
                    oEducacionBE1.SeccionEnFormulario = UIConstantes.SECCION_EN_FORMULARIO.FORM_POSITION_1O;
                    oEducacionBE1.Revision_Opid = Session["usrRedId"].ToString();

                    ///Detalle de Educación
                    oEducacionDetalleBE1 = new EducacionDetalleBE();
                    oEducacionDetalleBE1.IdDetalleEducacion = UIConvertNull.Int32(txtIdDetalleEducacion1.Text);
                    string tmpFechaIni = "01/01/" + this.txtAnioDesde1.Text;
                    CultureInfo culturaIni = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaInicio;
                    if (DateTime.TryParse(tmpFechaIni, culturaIni, System.Globalization.DateTimeStyles.None, out tempFechaInicio))
                    {
                        oEducacionDetalleBE1.FechaInicio = tempFechaInicio;
                    }

                    string tmpFechaFin = "01/01/" + this.txtAnioHasta1.Text;
                    CultureInfo culturaFin = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaFin;
                    if (DateTime.TryParse(tmpFechaFin, culturaFin, System.Globalization.DateTimeStyles.None, out tempFechaFin))
                    {
                        oEducacionDetalleBE1.FechaFin = tempFechaFin;
                    }
                    oEducacionDetalleBE1.NombreCarrera = txtCursoPrograma1.Text;
                    oEducacionDetalleBE1.Revision_Opid = Session["usrRedId"].ToString();

                    if (oEducacionBE1 != null)
                    {
                        if (oAplicanteBE.LEducacion == null)
                        {
                            oAplicanteBE.LEducacion = new System.Collections.Generic.List<EducacionBE>();
                        }
                        if (oEducacionDetalleBE1 != null)
                        {
                            oEducacionBE1.LDetalleEducacion = new List<EducacionDetalleBE>();
                            oEducacionBE1.LDetalleEducacion.Add(oEducacionDetalleBE1);
                        }
                        oAplicanteBE.LEducacion.Add(oEducacionBE1);
                    }
                }

                //======================================================================
                // Otros Estudios 2
                //======================================================================
                oEducacionBE2 = new EducacionBE();
                if (this.txtCursoPrograma2.Text != null && txtCursoPrograma2.Text != String.Empty)
                {
                    oEducacionBE2.IdEducacion = UIConvertNull.Int32(txtIdEducacion2.Text);
                    oEducacionBE2.NombreInstitucion = txtNomInstitucion2.Text;
                    oEducacionBE2.SeccionEnFormulario = UIConstantes.SECCION_EN_FORMULARIO.FORM_POSITION_2O;
                    oEducacionBE2.Revision_Opid = Session["usrRedId"].ToString();

                    ///Detalle de Educación
                    oEducacionDetalleBE2 = new EducacionDetalleBE();
                    oEducacionDetalleBE2.IdDetalleEducacion = UIConvertNull.Int32(txtIdDetalleEducacion2.Text);
                    string tmpFechaIni = "01/01/" + this.txtAnioDesde2.Text;
                    CultureInfo culturaIni = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaInicio;
                    if (DateTime.TryParse(tmpFechaIni, culturaIni, System.Globalization.DateTimeStyles.None, out tempFechaInicio))
                    {
                        oEducacionDetalleBE2.FechaInicio = tempFechaInicio;
                    }

                    string tmpFechaFin = "01/01/" + this.txtAnioHasta2.Text;
                    CultureInfo culturaFin = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaFin;
                    if (DateTime.TryParse(tmpFechaFin, culturaFin, System.Globalization.DateTimeStyles.None, out tempFechaFin))
                    {
                        oEducacionDetalleBE2.FechaFin = tempFechaFin;
                    }
                    oEducacionDetalleBE2.NombreCarrera = txtCursoPrograma2.Text;
                    oEducacionDetalleBE2.Revision_Opid = Session["usrRedId"].ToString();

                    if (oEducacionBE2 != null)
                    {
                        if (oAplicanteBE.LEducacion == null)
                        {
                            oAplicanteBE.LEducacion = new System.Collections.Generic.List<EducacionBE>();
                        }
                        if (oEducacionDetalleBE2 != null)
                        {
                            oEducacionBE2.LDetalleEducacion = new List<EducacionDetalleBE>();
                            oEducacionBE2.LDetalleEducacion.Add(oEducacionDetalleBE2);
                        }
                        oAplicanteBE.LEducacion.Add(oEducacionBE2);
                    }
                }

                //======================================================================
                // Otros Estudios 3
                //======================================================================
                oEducacionBE3 = new EducacionBE();
                if (this.txtCursoPrograma3.Text != null && txtCursoPrograma3.Text != String.Empty)
                {
                    oEducacionBE3.IdEducacion = UIConvertNull.Int32(txtIdEducacion3.Text);
                    oEducacionBE3.NombreInstitucion = txtNomInstitucion3.Text;
                    oEducacionBE3.SeccionEnFormulario = UIConstantes.SECCION_EN_FORMULARIO.FORM_POSITION_3O;
                    oEducacionBE3.Revision_Opid = Session["usrRedId"].ToString();

                    ///Detalle de Educación
                    oEducacionDetalleBE3 = new EducacionDetalleBE();
                    oEducacionDetalleBE3.IdDetalleEducacion = UIConvertNull.Int32(txtIdDetalleEducacion3.Text);
                    string tmpFechaIni = "01/01/" + this.txtAnioDesde3.Text;
                    CultureInfo culturaIni = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaInicio;
                    if (DateTime.TryParse(tmpFechaIni, culturaIni, System.Globalization.DateTimeStyles.None, out tempFechaInicio))
                    {
                        oEducacionDetalleBE3.FechaInicio = tempFechaInicio;
                    }

                    string tmpFechaFin = "01/01/" + this.txtAnioHasta3.Text;
                    CultureInfo culturaFin = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaFin;
                    if (DateTime.TryParse(tmpFechaFin, culturaFin, System.Globalization.DateTimeStyles.None, out tempFechaFin))
                    {
                        oEducacionDetalleBE3.FechaFin = tempFechaFin;
                    }
                    oEducacionDetalleBE3.NombreCarrera = txtCursoPrograma3.Text;
                    oEducacionDetalleBE3.Revision_Opid = Session["usrRedId"].ToString();

                    if (oEducacionBE3 != null)
                    {
                        if (oAplicanteBE.LEducacion == null)
                        {
                            oAplicanteBE.LEducacion = new System.Collections.Generic.List<EducacionBE>();
                        }
                        if (oEducacionDetalleBE3 != null)
                        {
                            oEducacionBE3.LDetalleEducacion = new List<EducacionDetalleBE>();
                            oEducacionBE3.LDetalleEducacion.Add(oEducacionDetalleBE3);
                        }
                        oAplicanteBE.LEducacion.Add(oEducacionBE3);
                    }
                }
                return oAplicanteBE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarEstudiosRegistradas(Int32? AplicanteId)
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ObtenerOtrosEstudiosRegistrados(AplicanteId);
                this.LLenarDatosEstudiosRegistradas(oAplicanteBE);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosEstudiosRegistradas(AplicanteBE oAplicanteBE)
        {
            if (oAplicanteBE != null && oAplicanteBE.LEducacion != null)
            {

                List<EducacionBE> LEducacion = (from itemEduca in oAplicanteBE.LEducacion
                                                orderby itemEduca.IdEducacion
                                                select itemEduca).ToList<EducacionBE>();

                if (oAplicanteBE.LEducacion != null)
                {
                    for (int indice = 0; indice < oAplicanteBE.LEducacion.Count; indice++)
                    {
                        EducacionBE oEducacionBE = oAplicanteBE.LEducacion[indice];
                        switch (indice)
                        {
                            case 0:
                                this.txtIdEducacion1.Text = UIConvertNull.String(oEducacionBE.IdEducacion);
                                this.txtNomInstitucion1.Text = UIConvertNull.String(oEducacionBE.NombreInstitucion);

                                if (oEducacionBE.LDetalleEducacion != null && oEducacionBE.LDetalleEducacion.Count > 0)
                                {
                                    EducacionDetalleBE oEducacionDetalleBE = oEducacionBE.LDetalleEducacion[0];
                                    this.txtIdDetalleEducacion1.Text = UIConvertNull.String(oEducacionDetalleBE.IdApplicationEducationEnroll);
                                    this.txtCursoPrograma1.Text = UIConvertNull.String(oEducacionDetalleBE.NombreCarrera);
                                    this.txtAnioDesde1.Text = oEducacionDetalleBE.FechaInicio.HasValue ? oEducacionDetalleBE.FechaInicio.Value.ToString("yyyy") : String.Empty;
                                    this.txtAnioHasta1.Text = oEducacionDetalleBE.FechaFin.HasValue ? oEducacionDetalleBE.FechaFin.Value.ToString("yyyy") : String.Empty;
                                }
                                this.btnAgregaEstudios2.Visible = true;
                                break;
                            case 1:
                                this.txtIdEducacion2.Text = UIConvertNull.String(oEducacionBE.IdEducacion);
                                this.txtNomInstitucion2.Text = UIConvertNull.String(oEducacionBE.NombreInstitucion);

                                if (oEducacionBE.LDetalleEducacion != null && oEducacionBE.LDetalleEducacion.Count > 0)
                                {
                                    EducacionDetalleBE oEducacionDetalleBE = oEducacionBE.LDetalleEducacion[1];
                                    this.txtIdDetalleEducacion2.Text = UIConvertNull.String(oEducacionDetalleBE.IdApplicationEducationEnroll);
                                    this.txtCursoPrograma2.Text = UIConvertNull.String(oEducacionDetalleBE.NombreCarrera);
                                    this.txtAnioDesde2.Text = oEducacionDetalleBE.FechaInicio.HasValue ? oEducacionDetalleBE.FechaInicio.Value.ToString("yyyy") : String.Empty;
                                    this.txtAnioHasta2.Text = oEducacionDetalleBE.FechaFin.HasValue ? oEducacionDetalleBE.FechaFin.Value.ToString("yyyy") : String.Empty;
                                }
                                this.btnAgregaEstudios2.Visible = false;
                                this.btnQuitarEstudios2.Visible = true;
                                this.btnAgregaEstudios3.Visible = true;
                                this.btnQuitarEstudios3.Visible = false;
                                this.pnlEstudios2.Visible = true;
                                break;
                            case 2:
                                this.txtIdEducacion3.Text = UIConvertNull.String(oEducacionBE.IdEducacion);
                                this.txtNomInstitucion3.Text = UIConvertNull.String(oEducacionBE.NombreInstitucion);

                                if (oEducacionBE.LDetalleEducacion != null && oEducacionBE.LDetalleEducacion.Count > 0)
                                {
                                    EducacionDetalleBE oEducacionDetalleBE = oEducacionBE.LDetalleEducacion[2];
                                    this.txtIdDetalleEducacion3.Text = UIConvertNull.String(oEducacionDetalleBE.IdApplicationEducationEnroll);
                                    this.txtCursoPrograma3.Text = UIConvertNull.String(oEducacionDetalleBE.NombreCarrera);
                                    this.txtAnioDesde3.Text = oEducacionDetalleBE.FechaInicio.HasValue ? oEducacionDetalleBE.FechaInicio.Value.ToString("yyyy") : String.Empty;
                                    this.txtAnioHasta3.Text = oEducacionDetalleBE.FechaFin.HasValue ? oEducacionDetalleBE.FechaFin.Value.ToString("yyyy") : String.Empty;
                                }
                                this.btnAgregaEstudios2.Visible = false;
                                this.btnQuitarEstudios2.Visible = true;
                                this.btnAgregaEstudios3.Visible = false;
                                this.btnQuitarEstudios3.Visible = true;
                                this.pnlEstudios3.Visible = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private Boolean EliminaOtrosEstudiosRegistrado(Int32? IdEducacion, Int32? IdDetEducacion)
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
                    this.CargarEstudiosRegistradas(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
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
            this.txtCursoPrograma1.Text = UIConstantes._valorCadenaVacia;
            this.txtCursoPrograma2.Text = UIConstantes._valorCadenaVacia;
            this.txtCursoPrograma3.Text = UIConstantes._valorCadenaVacia;
            this.txtNomInstitucion1.Text = UIConstantes._valorCadenaVacia;
            this.txtNomInstitucion2.Text = UIConstantes._valorCadenaVacia;
            this.txtNomInstitucion3.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioDesde1.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioDesde2.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioDesde3.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioHasta1.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioHasta2.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioHasta3.Text = UIConstantes._valorCadenaVacia;
            this.txtIdEducacion1.Text = UIConstantes._valorCadenaVacia;
            this.txtIdEducacion2.Text = UIConstantes._valorCadenaVacia;
            this.txtIdEducacion3.Text = UIConstantes._valorCadenaVacia;
            this.txtIdDetalleEducacion1.Text = UIConstantes._valorCadenaVacia;
            this.txtIdDetalleEducacion2.Text = UIConstantes._valorCadenaVacia;
            this.txtIdDetalleEducacion3.Text = UIConstantes._valorCadenaVacia;
        }

        #endregion "Métodos Privados"
    }
}
