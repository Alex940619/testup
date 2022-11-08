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
    public partial class frm07_ActividadExtracurricular : BasePage
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
                //Session["AplicanteId"] = 110604;

                if (!IsPostBack)
                {
                    this.HabilitaControles();
                    //this.CargarTitulos();
                    this.AgregarFilaVacia("A");
                    this.LLenarGrillaActividadesRegistradas(UIConvertNull.Int32(Session["AplicanteId"].ToString()));
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F07, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        protected void gvActividades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            UIHelper.SessionActiva(Page);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drw = (DataRowView)e.Row.DataItem;
                DataRow dr = drw.Row;
                e.Row.Cells[0].Text = ((this.gvActividades.PageIndex * this.gvActividades.PageSize) + (e.Row.RowIndex + 1)).ToString();

                Label _lblIdApplicationActivity = (Label)e.Row.FindControl("lblIdApplicationActivity");
                Label _lblIdActividad = (Label)e.Row.FindControl("lblIdActividad");
                TextBox _txtNomActividad = (TextBox)e.Row.FindControl("txtNomActividad");
                DropDownList _ddlActividad = (DropDownList)e.Row.FindControl("ddlActividad");
                DropDownList _ddlAnioDesde = (DropDownList)e.Row.FindControl("ddlAnioDesde");
                DropDownList _ddlAnioHasta = (DropDownList)e.Row.FindControl("ddlAnioHasta");
                RadioButtonList _rblPromovidoPorCole = (RadioButtonList)e.Row.FindControl("rblPromovidoPorCole");
                ImageButton _imgBtnEliminar = (ImageButton)e.Row.FindControl("imgBtnEliminar");

                _ddlAnioDesde.Attributes.Add("onchange", "validaFecha('" + _ddlAnioDesde.ClientID + "','" + _ddlAnioHasta.ClientID + "')");
                _ddlAnioHasta.Attributes.Add("onchange", "validaFecha('" + _ddlAnioDesde.ClientID + "','" + _ddlAnioHasta.ClientID + "')");
                _imgBtnEliminar.Attributes.Add(UIConstantes.EventosJS.JsEventoOnClick, UIHelper.JsConfirmarAccionPaginaWeb(UIConstantes.Alert.msgConfirmaEliminarRegistro));

                GeneralBL oGeneralBL = null;
                oGeneralBL = new GeneralBL();
                DataTable dtAnnio = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.ANNIO2].Key, null, null);
                if (dtAnnio != null && dtAnnio.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(_ddlAnioDesde, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                    Funciones.cargarComboYSeleccione(_ddlAnioHasta, dtAnnio.Copy(), "Descripcion", "IdCodigo", "-- Seleccionar --");
                }

                //GeneralBL oGeneralBL = null;
                oGeneralBL = new GeneralBL();
                DataTable dtActividad = oGeneralBL.ObtenerTipoCodigoPC(UIConstantes.ObtenerTipoCodigo()[UIConstantes.TIPO_CODIGO.TIPO_ACTIVIDAD].Key, "", null);
                if (dtActividad != null && dtActividad.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(_ddlActividad, dtActividad.Copy(), "descripcion", "codigo", "-- Seleccionar --");
                }

                if (UIConvertNull.String(dr["ApplicationActivityId"]) != null)
                {
                    _lblIdApplicationActivity.Text = UIConvertNull.String(dr["ApplicationActivityId"]);
                }
                if (UIConvertNull.String(dr["codigo"]) != null)
                {
                    _ddlActividad.SelectedValue = UIConvertNull.String(dr["codigo"]);
                }
                if (UIConvertNull.String(dr["descripcion"]) != null)
                {
                    _txtNomActividad.Text = UIConvertNull.String(dr["descripcion"]);
                }
                if (UIConvertNull.String(dr["StartDate"]) != null)
                {
                    _ddlAnioDesde.SelectedValue = UIConvertNull.String(dr["StartDate"]);
                }
                if (UIConvertNull.String(dr["EndDate"]) != null)
                {
                    _ddlAnioHasta.SelectedValue = UIConvertNull.String(dr["EndDate"]);
                }
                if (UIConvertNull.String(dr["isPromotedSchool"]) != null)
                {
                    _rblPromovidoPorCole.SelectedValue = UIConvertNull.String(dr["isPromotedSchool"]);
                }

                String strCadena = UIConstantes._valorCadenaVacia;
                strCadena = strCadena + UIHelper.AsignarDatoControlHtml(this.hIdActividad.ClientID, UIConvertNull.String(dr["codigo"]));
                UIHelper.SeleccionarItemGrillaOnClickMoverRaton(e, strCadena);
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
                    if (PaginaActual == UIConstantes.Formularios.F07)
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
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F07, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                this.AgregarFilaVacia("A");
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            ActividadBE oActividadBE = null;
            AplicanteBL oAplicanteBL = null;
            Boolean Resultado = false;
            try
            {
                ImageButton _imgBtnEliminar = (ImageButton)(sender);
                GridViewRow gvrow = (GridViewRow)_imgBtnEliminar.NamingContainer;
                Label _lblIdApplicationActivity = (Label)gvrow.FindControl("lblIdApplicationActivity");
                if (!String.IsNullOrEmpty(_lblIdApplicationActivity.Text))
                {
                    oActividadBE = new ActividadBE();
                    oActividadBE.IdAplicacion = UIConvertNull.Int32(Session["AplicanteId"]);
                    oActividadBE.IdApplicationActivity = UIConvertNull.Int32(_lblIdApplicationActivity.Text);

                    oAplicanteBL = new AplicanteBL();
                    Resultado = oAplicanteBL.EliminaActividad(oActividadBE);
                    if (Resultado == true)
                    {
                        this.LLenarGrillaActividadesRegistradas(UIConvertNull.Int32(Session["AplicanteId"]));
                    }
                }
                else
                {
                    this.LLenarGrillaActividadesRegistradas(UIConvertNull.Int32(Session["AplicanteId"]));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + ex.Message.Replace("\n", ""), false);
            }
            finally
            {
                oActividadBE = null;
                oAplicanteBL = null;
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
                    if (PaginaActual == UIConstantes.Formularios.F07)
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

        private void LLenarGrillaActividadesRegistradas(Int32? AplicanteId)
        {
            AplicanteBL oAplicanteBL = null;
            AplicanteBE oAplicanteBE = null;
            oAplicanteBE = new AplicanteBE();
            DataTable dt = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                dt = oAplicanteBL.ObtenerActExtracurricularRegistrado(AplicanteId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.gvActividades.DataSource = dt;
                    this.gvActividades.DataBind();
                }
                else
                {
                    this.gvActividades.DataSource = null;
                    this.gvActividades.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GuardarDatos()
        {
            List<ActividadBE> oList = null;
            AplicanteBL oAplicanteBL = null;
            GeneralBL oGeneralBL = null;
            DataTable dtPagSigui = null;
            String PaginaActual, PaginaSiguiente = null;
            try
            {
                if (gvActividades.Rows.Count > 0)
                {
                    oList = new List<ActividadBE>();
                    oList = this.obtenerDatosActividadExtraCurricular();

                    if (oList.Count > 0)
                    {
                        oAplicanteBL = new AplicanteBL();
                        Boolean operacionOK = oAplicanteBL.InsertaDatosFormSeis_ActivExtracurricular(oList);
                        if (operacionOK)
                        {
                            Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                            oGeneralBL = new GeneralBL();
                            dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                            for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                            {
                                PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                                if (PaginaActual == UIConstantes.Formularios.F07)
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
                        Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                        oGeneralBL = new GeneralBL();
                        dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                        for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                        {
                            PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                            if (PaginaActual == UIConstantes.Formularios.F07)
                            {
                                PaginaSiguiente = dtPagSigui.Rows[i + 1]["NombreFormulario"].ToString();
                                break;
                            }
                        }
                        Response.Redirect(PaginaSiguiente, false);
                    }
                }
                else
                {
                    Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                    oGeneralBL = new GeneralBL();
                    dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                    for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                    {
                        PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                        if (PaginaActual == UIConstantes.Formularios.F07)
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

        private List<ActividadBE> obtenerDatosActividadExtraCurricular()
        {
            ActividadBE oActividadBE = null;
            List<ActividadBE> oList = null;
            Int32? i = 1;
            try
            {
                oList = new List<ActividadBE>();
                foreach (GridViewRow gvr in this.gvActividades.Rows)
                {
                    oActividadBE = new ActividadBE();
                    oActividadBE.IdAplicacion = UIConvertNull.Int32(Session["AplicanteId"]);

                    DropDownList _ddlActividad = (DropDownList)gvr.FindControl("ddlActividad");
                    Label _lblIdApplicationActivity = (Label)gvr.FindControl("lblIdApplicationActivity");
                    TextBox _txtNomActividad = (TextBox)gvr.FindControl("txtNomActividad");
                    DropDownList _ddlFechaDesde = (DropDownList)gvr.FindControl("ddlAnioDesde");
                    DropDownList _ddlFechaHasta = (DropDownList)gvr.FindControl("ddlAnioHasta");
                    RadioButtonList _rblPromovi = (RadioButtonList)gvr.FindControl("rblPromovidoPorCole");

                    if (_ddlActividad.SelectedValue != "0" && _txtNomActividad.Text != null && _ddlFechaDesde.SelectedValue != "0" && _ddlFechaHasta.SelectedValue != "0"
                        && _rblPromovi.SelectedValue != null)
                    {
                        oActividadBE.IdTipoActividad = UIConvertNull.Int32(_ddlActividad.SelectedValue);
                        oActividadBE.IdApplicationActivity = UIConvertNull.Int32(_lblIdApplicationActivity.Text);
                        oActividadBE.Posicion = "POS" + UIConvertNull.String(i);
                        oActividadBE.HorasPorSemana = 0.00;
                        oActividadBE.SemanasPorAnho = UIConstantes.idValorNulo;
                        oActividadBE.NumeroAnhos = UIConstantes.idValorNulo;
                        oActividadBE.GradoParticipacion09 = UIConstantes.idValorNulo;
                        oActividadBE.GradoParticipacion10 = UIConstantes.idValorNulo;
                        oActividadBE.GradoParticipacion11 = UIConstantes.idValorNulo;
                        oActividadBE.GradoParticipacion12 = UIConstantes.idValorNulo;
                        oActividadBE.ParticipacionSecundaria = UIConstantes.idValorNulo;
                        oActividadBE.NombreActividad = _txtNomActividad.Text;

                        String tmpFechaInicio = "01/01/" + _ddlFechaDesde.SelectedItem.Text;
                        CultureInfo culturaInicio = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                        DateTime tempFechaInicio;
                        if (DateTime.TryParse(tmpFechaInicio, culturaInicio, System.Globalization.DateTimeStyles.None, out tempFechaInicio))
                        {
                            oActividadBE.FechaInicio = UIConvertNull.DateTime(tmpFechaInicio);
                        }

                        String tmpFechaFin = "31/12/" + _ddlFechaHasta.SelectedItem.Text;
                        CultureInfo culturaFin = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
                        DateTime tempFechaFin;
                        if (DateTime.TryParse(tmpFechaFin, culturaFin, System.Globalization.DateTimeStyles.None, out tempFechaFin))
                        {
                            oActividadBE.FechaFin = UIConvertNull.DateTime(tmpFechaFin);
                        }
                        oActividadBE.EsPromovidoPorColegio = UIConvertNull.Int32(_rblPromovi.SelectedValue);
                        oActividadBE.Revision_Opid = UIConvertNull.String(Session["usrRedId"]);
                        oList.Add(oActividadBE);
                        i++;
                    }
                }
                return oList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AgregarFilaVacia(String tipo)
        {
            DataTable dt = null;
            dt = Actividades();
            DataRow dr;
            foreach (GridViewRow row in this.gvActividades.Rows)
            {
                Label _lblIdApplicationActivity = (Label)row.FindControl("lblIdApplicationActivity");
                Label _lblIdActividad = (Label)row.FindControl("lblIdActividad");
                DropDownList _ddlActividad = (DropDownList)row.FindControl("ddlActividad");
                TextBox _txtNomActividad = (TextBox)row.FindControl("txtNomActividad");
                DropDownList _ddlFechaDesde = (DropDownList)row.FindControl("ddlAnioDesde");
                DropDownList _ddlFechaHasta = (DropDownList)row.FindControl("ddlAnioHasta");
                RadioButtonList _rblPromovi = (RadioButtonList)row.FindControl("rblPromovidoPorCole");

                dr = dt.NewRow();
                dr[0] = _lblIdApplicationActivity.Text.ToString();
                dr[1] = _ddlActividad.Text.ToString();
                dr[2] = _txtNomActividad.Text.ToString();
                dr[3] = _ddlFechaDesde.Text.ToString();
                dr[4] = _ddlFechaHasta.Text.ToString();
                dr[5] = _rblPromovi.Text.ToString();
                dt.Rows.Add(dr);
            }
            if (tipo == "A")
            {
                ViewState["DataTemp"] = dt;
                dr = dt.NewRow();
                dr[0] = "";
                dr[1] = "";
                dr[2] = "";
                dr[3] = "";
                dr[4] = "";
                dr[5] = "";
                dt.Rows.Add(dr);
            }
            ViewState["DataTemp"] = dt;
            this.gvActividades.DataSource = ViewState["DataTemp"];
            this.gvActividades.DataBind();
        }

        private DataTable Actividades()
        {
            DataTable medidaTabla = new DataTable();
            medidaTabla.Columns.Add("ApplicationActivityId", typeof(String));
            medidaTabla.Columns.Add("codigo", typeof(String));
            medidaTabla.Columns.Add("descripcion", typeof(String));
            medidaTabla.Columns.Add("StartDate", typeof(String));
            medidaTabla.Columns.Add("EndDate", typeof(String));
            medidaTabla.Columns.Add("isPromotedSchool", typeof(String));
            return medidaTabla;
        }

        #endregion "Métodos Privados"
    }
}
