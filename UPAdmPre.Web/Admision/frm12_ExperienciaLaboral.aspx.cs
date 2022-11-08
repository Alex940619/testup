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
    public partial class frm12_ExperienciaLaboral : BasePage
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
                    this.CargarExperienciasRegistradas(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F12, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
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
                    if (PaginaActual == UIConstantes.Formularios.F12)
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
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F12, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        protected void btnAgregarExperiencia2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlExperiencia2.Visible = true;
            this.btnQuitarExperiencia2.Visible = true;
            this.btnAgregarExperiencia2.Visible = false;
            this.btnQuitarExperiencia3.Visible = false;
            this.btnAgregarExperiencia3.Visible = true;
        }

        protected void btnQuitarExperiencia2_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEmpleo = UIConvertNull.Int32(txtApplicationEmploymentId2.Text);
            if (IdEmpleo != null)
            {
                this.EliminaExperienciaLaboralRegistrada(IdEmpleo);
            }
            this.pnlExperiencia2.Visible = false;
            this.pnlExperiencia3.Visible = false;
            this.btnQuitarExperiencia2.Visible = false;
            this.btnAgregarExperiencia2.Visible = true;
            this.btnQuitarExperiencia3.Visible = false;
            this.btnAgregarExperiencia3.Visible = false;
        }

        protected void btnQuitarExperiencia3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            Int32? IdEmpleo = UIConvertNull.Int32(txtApplicationEmploymentId3.Text);
            if (IdEmpleo != null)
            {
                this.EliminaExperienciaLaboralRegistrada(IdEmpleo);
            }
            this.pnlExperiencia3.Visible = false;
            this.btnQuitarExperiencia3.Visible = false;
            this.btnAgregarExperiencia3.Visible = true;
        }

        protected void btnAgregarExperiencia3_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.pnlExperiencia3.Visible = true;
            this.btnQuitarExperiencia3.Visible = true;
            this.btnAgregarExperiencia3.Visible = false;
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
                    if (PaginaActual == UIConstantes.Formularios.F12)
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
            this.pnlExperiencia2.Visible = blnAccion;
            this.pnlExperiencia3.Visible = blnAccion;
            this.btnQuitarExperiencia2.Visible = blnAccion;
            this.btnQuitarExperiencia3.Visible = blnAccion;
            this.btnAgregarExperiencia3.Visible = blnAccion;
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
                oAplicanteBE = this.obtenerDatosExperienciaLaboral(oAplicanteBE);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormTrece_ExperienciaLaboral(oAplicanteBE);
                if (operacionOK)
                {
                    Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                    oGeneralBL = new GeneralBL();
                    dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    {
                        PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F12)
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

        private AplicanteBE obtenerDatosExperienciaLaboral(AplicanteBE oAplicanteBE)
        {
            EmpleadorBE oEmpleadorEmp1 = null;
            EmpleadorBE oEmpleadorEmp2 = null;
            EmpleadorBE oEmpleadorEmp3 = null;
            try
            {
                if (oAplicanteBE.LEmpleador == null)
                {
                    oAplicanteBE.LEmpleador = new System.Collections.Generic.List<EmpleadorBE>();
                }

                oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);

                //======================================================================
                // Experiencia Laboral 1
                //======================================================================
                oEmpleadorEmp1 = new EmpleadorBE();
                if (this.txtCargoFuncion1.Text != null && txtCargoFuncion1.Text != String.Empty)
                {
                    oEmpleadorEmp1.IdEmpleador = UIConvertNull.Int32(txtApplicationEmploymentId1.Text);
                    oEmpleadorEmp1.Cargo = this.txtCargoFuncion1.Text;
                    oEmpleadorEmp1.NombreEmpleador = this.txtNomEmpresa1.Text;

                    string tmpFechaIni = "01/01/" + this.txtAnioDesde1.Text;
                    CultureInfo culturaIni = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaInicio;
                    if (DateTime.TryParse(tmpFechaIni, culturaIni, System.Globalization.DateTimeStyles.None, out tempFechaInicio))
                    {
                        oEmpleadorEmp1.FechaIngreso = tempFechaInicio;
                    }

                    if (chkActual.Checked == false)
                    {
                        string tmpFechaFin = "01/01/" + this.txtAnioHasta1.Text;
                        CultureInfo culturaFin = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                        DateTime tempFechaFin;
                        if (DateTime.TryParse(tmpFechaFin, culturaFin, System.Globalization.DateTimeStyles.None, out tempFechaFin))
                        {
                            oEmpleadorEmp1.FechaSalida = tempFechaFin;
                        }
                    }
                    else
                    {
                        oEmpleadorEmp1.FechaSalida = null;
                    }
                    oEmpleadorEmp1.Revision_Opid = Session["usrRedId"].ToString();
                    oAplicanteBE.LEmpleador.Add(oEmpleadorEmp1);
                }

                //======================================================================
                // Experiencia Laboral 2
                //======================================================================
                oEmpleadorEmp2 = new EmpleadorBE();
                if (this.txtCargoFuncion2.Text != null && txtCargoFuncion2.Text != String.Empty)
                {
                    oEmpleadorEmp2.IdEmpleador = UIConvertNull.Int32(txtApplicationEmploymentId2.Text);
                    oEmpleadorEmp2.Cargo = this.txtCargoFuncion2.Text;
                    oEmpleadorEmp2.NombreEmpleador = this.txtNomEmpresa2.Text;

                    string tmpFechaIni = "01/01/" + this.txtAnioDesde2.Text;
                    CultureInfo culturaIni = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaInicio;
                    if (DateTime.TryParse(tmpFechaIni, culturaIni, System.Globalization.DateTimeStyles.None, out tempFechaInicio))
                    {
                        oEmpleadorEmp2.FechaIngreso = tempFechaInicio;
                    }

                    string tmpFechaFin = "01/01/" + this.txtAnioHasta2.Text;
                    CultureInfo culturaFin = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaFin;
                    if (DateTime.TryParse(tmpFechaFin, culturaFin, System.Globalization.DateTimeStyles.None, out tempFechaFin))
                    {
                        oEmpleadorEmp2.FechaSalida = tempFechaFin;
                    }
                    oEmpleadorEmp2.Revision_Opid = Session["usrRedId"].ToString();
                    oAplicanteBE.LEmpleador.Add(oEmpleadorEmp2);
                }

                //======================================================================
                // Experiencia Laboral 3
                //======================================================================
                oEmpleadorEmp3 = new EmpleadorBE();
                if (this.txtCargoFuncion3.Text != null && txtCargoFuncion3.Text != String.Empty)
                {
                    oEmpleadorEmp3.IdEmpleador = UIConvertNull.Int32(txtApplicationEmploymentId3.Text);
                    oEmpleadorEmp3.Cargo = this.txtCargoFuncion3.Text;
                    oEmpleadorEmp3.NombreEmpleador = this.txtNomEmpresa3.Text;

                    string tmpFechaIni = "01/01/" + this.txtAnioDesde3.Text;
                    CultureInfo culturaIni = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaInicio;
                    if (DateTime.TryParse(tmpFechaIni, culturaIni, System.Globalization.DateTimeStyles.None, out tempFechaInicio))
                    {
                        oEmpleadorEmp3.FechaIngreso = tempFechaInicio;
                    }

                    string tmpFechaFin = "01/01/" + this.txtAnioHasta3.Text;
                    CultureInfo culturaFin = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                    DateTime tempFechaFin;
                    if (DateTime.TryParse(tmpFechaFin, culturaFin, System.Globalization.DateTimeStyles.None, out tempFechaFin))
                    {
                        oEmpleadorEmp3.FechaSalida = tempFechaFin;
                    }
                    oEmpleadorEmp3.Revision_Opid = Session["usrRedId"].ToString();
                    oAplicanteBE.LEmpleador.Add(oEmpleadorEmp3);
                }
                return oAplicanteBE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarExperienciasRegistradas(Int32? AplicanteId)
        {
            AplicanteBE oAplicanteBE = null;
            AplicanteBL oAplicanteBL = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                oAplicanteBE = oAplicanteBL.ObtenerExperienciasRegistradas(AplicanteId);
                this.LLenarDatosExperienciasRegistradas(oAplicanteBE);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LLenarDatosExperienciasRegistradas(AplicanteBE oAplicanteBE)
        {
            if (oAplicanteBE != null)
            {
                if (oAplicanteBE.LEmpleador != null)
                {
                    for (int indice = 0; indice < oAplicanteBE.LEmpleador.Count; indice++)
                    {
                        EmpleadorBE oEmpleadorBE = oAplicanteBE.LEmpleador[indice];
                        switch (indice)
                        {
                            case 0:
                                this.txtApplicationEmploymentId1.Text = UIConvertNull.String(oEmpleadorBE.IdEmpleador);
                                this.txtCargoFuncion1.Text = UIConvertNull.String(oEmpleadorBE.Cargo);
                                this.txtNomEmpresa1.Text = UIConvertNull.String(oEmpleadorBE.NombreEmpleador);
                                this.txtAnioDesde1.Text = oEmpleadorBE.FechaIngreso.HasValue ? oEmpleadorBE.FechaIngreso.Value.ToString("yyyy") : String.Empty;
                                if (oEmpleadorBE.FechaSalida.HasValue)
                                {
                                    this.txtAnioHasta1.Text = oEmpleadorBE.FechaSalida.HasValue ? oEmpleadorBE.FechaSalida.Value.ToString("yyyy") : String.Empty;
                                }
                                else
                                {
                                    this.chkActual.Checked = true;
                                }
                                this.btnAgregarExperiencia2.Visible = true;
                                break;
                            case 1:
                                this.txtApplicationEmploymentId2.Text = UIConvertNull.String(oEmpleadorBE.IdEmpleador);
                                this.txtCargoFuncion2.Text = UIConvertNull.String(oEmpleadorBE.Cargo);
                                this.txtNomEmpresa2.Text = UIConvertNull.String(oEmpleadorBE.NombreEmpleador);
                                this.txtAnioDesde2.Text = oEmpleadorBE.FechaIngreso.HasValue ? oEmpleadorBE.FechaIngreso.Value.ToString("yyyy") : String.Empty;
                                this.txtAnioHasta2.Text = oEmpleadorBE.FechaSalida.HasValue ? oEmpleadorBE.FechaSalida.Value.ToString("yyyy") : String.Empty;
                                this.btnAgregarExperiencia2.Visible = false;
                                this.btnQuitarExperiencia2.Visible = true;
                                this.btnAgregarExperiencia3.Visible = true;
                                this.btnQuitarExperiencia3.Visible = false;
                                this.pnlExperiencia2.Visible = true;
                                break;
                            case 2:
                                this.txtApplicationEmploymentId3.Text = UIConvertNull.String(oEmpleadorBE.IdEmpleador);
                                this.txtCargoFuncion3.Text = UIConvertNull.String(oEmpleadorBE.Cargo);
                                this.txtNomEmpresa3.Text = UIConvertNull.String(oEmpleadorBE.NombreEmpleador);
                                this.txtAnioDesde3.Text = oEmpleadorBE.FechaIngreso.HasValue ? oEmpleadorBE.FechaIngreso.Value.ToString("yyyy") : String.Empty;
                                this.txtAnioHasta3.Text = oEmpleadorBE.FechaSalida.HasValue ? oEmpleadorBE.FechaSalida.Value.ToString("yyyy") : String.Empty;
                                this.btnAgregarExperiencia2.Visible = false;
                                this.btnQuitarExperiencia2.Visible = true;
                                this.btnAgregarExperiencia3.Visible = false;
                                this.btnQuitarExperiencia3.Visible = true;
                                this.pnlExperiencia3.Visible = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private Boolean EliminaExperienciaLaboralRegistrada(Int32? IdEmpleo)
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            AplicanteBL oAplicanteBL = null;
            Boolean Resultado = false;
            try
            {
                oAplicanteBL = new AplicanteBL();
                Resultado = oAplicanteBL.EliminaExperienciaLaboralRegistrada(IdEmpleo, AplicanteId);
                if (Resultado == true)
                {
                    this.LimpiarControles();
                    this.MostrarOcultarBotones(false);
                    this.CargarExperienciasRegistradas(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
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
            this.txtCargoFuncion1.Text = UIConstantes._valorCadenaVacia;
            this.txtCargoFuncion2.Text = UIConstantes._valorCadenaVacia;
            this.txtCargoFuncion3.Text = UIConstantes._valorCadenaVacia;
            this.txtNomEmpresa1.Text = UIConstantes._valorCadenaVacia;
            this.txtNomEmpresa2.Text = UIConstantes._valorCadenaVacia;
            this.txtNomEmpresa3.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioDesde1.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioDesde2.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioDesde3.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioHasta1.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioHasta2.Text = UIConstantes._valorCadenaVacia;
            this.txtAnioHasta3.Text = UIConstantes._valorCadenaVacia;
            this.txtApplicationEmploymentId1.Text = UIConstantes._valorCadenaVacia;
            this.txtApplicationEmploymentId2.Text = UIConstantes._valorCadenaVacia;
            this.txtApplicationEmploymentId3.Text = UIConstantes._valorCadenaVacia;
        }

        #endregion "Métodos Privados"
    }
}
