using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using UPAdmPre.BE;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frm06_RendimientoAcademico : BasePage
    {
        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                ///*Solo para pruebas*/
                //Session["usrRedId"] = "U.Admision";
                //Session["ModPostulacion"] = 41;
                //Session["AplicanteId"] = 110605;

                String EsBachillerato = "2";    /// 1 = Si; 2 = No;
                if (!IsPostBack)
                {
                    /*Ini:Christian Ramirez -REQ91569*/
                    int ? aplicanteId = UIConvertNull.Int32(Session["AplicanteId"].ToString());
                    AplicanteBE oAplicanteBE = new AplicanteBL().ObtenerRendAcademicoRegistrado(aplicanteId);

                    Session["SituacionAcademica"] = oAplicanteBE.LDetalleEducacion[0].SituaAcademica.Value;

                    if(Session["SituacionAcademica"].ToString() == "9")
                    {
                        trCuadroCompetencias.Visible = true;
                    }

                    this.MostrarOcultarBotones(false);
                    this.MostrarOcultarControles(EsBachillerato);
                    this.CargarCombos();
                    this.CargarComboTipoCalificacion(oAplicanteBE.LDetalleEducacion[0].SituaAcademica.Value);
                    this.HabilitaControles();
                    this.CargarRendimientoAcademicoRegistrado(oAplicanteBE);
                    /*Fin:Christian Ramirez -REQ91569*/
                    this.CargarAnioAcademico(UIConvertNull.Int32(Session["AplicanteId"].ToString()));

                    //Ocultando rendimiento 3ro competencias
                    OcultarRendimientoAcademico();
                    //**************************************
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F06, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        private void OcultarRendimientoAcademico()
        {
            if (Session["SituacionAcademica"].ToString() == "9" || Session["SituacionAcademica"].ToString() == "34")
            {
                trOrdenMeritoTercero.Visible = false;
                trNroAlumnosTercero.Visible = false;
                trOrdenMeritoCuarto.Visible = false;
                trNroAlumnosCuarto.Visible = false;
                trOrdenMeritoQuinto.Visible = false;
                trNroAlumnosQuinto.Visible = false;

                trOrdenMeritoPopup_PopUp.Visible = false;
                trNroAlmunosPopup_PopUp.Visible = false;
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
                    if (PaginaActual == UIConstantes.Formularios.F06)
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
                bool validacion = ValidarLlenadoDatos(Convert.ToInt32(Session["SituacionAcademica"]));

                if (validacion)
                {
                    this.GuardarDatos();
                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F06, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO], false);
            }
        }

        private bool ValidarLlenadoDatos(int situacionAcademica)
        {
            bool resultado = true;
            if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.EGRESADO)
            {
                //Validando notas tercero
                if(txtNotaMateTercero.Text.ToString() == "" || txtNotaMateTercero.Text.ToString() == "0")
                {
                    String Mensaje = "Es necesario colocar la nota de matemática y comunicación de tercero.";
                    this.mpeMostrarError.Show();
                    this.lblmessage.Text = Mensaje;
                    resultado = false;
                    return resultado;
                }

                if (txtNotaLengTercero.Text.ToString() == "" || txtNotaLengTercero.Text.ToString() == "0")
                {
                    String Mensaje = "Es necesario colocar la nota de matemática y comunicación de tercero.";
                    this.mpeMostrarError.Show();
                    this.lblmessage.Text = Mensaje;
                    resultado = false;
                    return resultado;
                }

                //Validando notas cuarto
                if (txtNotaMateCuarto.Text.ToString() == "" || txtNotaMateCuarto.Text.ToString() == "0")
                {
                    String Mensaje = "Es necesario colocar la nota de matemática y comunicación de cuarto.";
                    this.mpeMostrarError.Show();
                    this.lblmessage.Text = Mensaje;
                    resultado = false;
                    return resultado;
                }

                if (txtNotaLengCuarto.Text.ToString() == "" || txtNotaLengCuarto.Text.ToString() == "0")
                {
                    String Mensaje = "Es necesario colocar la nota de matemática y comunicación de cuarto.";
                    this.mpeMostrarError.Show();
                    this.lblmessage.Text = Mensaje;
                    resultado = false;
                    return resultado;
                }

                //Validando notas quinto
                if (txtNotaMateQuinto.Text.ToString() == "" || txtNotaMateQuinto.Text.ToString() == "0")
                {
                    String Mensaje = "Es necesario colocar la nota de matemática y comunicación de quinto.";
                    this.mpeMostrarError.Show();
                    this.lblmessage.Text = Mensaje;
                    resultado = false;
                    return resultado;
                }

                if (txtNotaLengQuinto.Text.ToString() == "" || txtNotaLengQuinto.Text.ToString() == "0")
                {
                    String Mensaje = "Es necesario colocar la nota de matemática y comunicación de quinto.";
                    this.mpeMostrarError.Show();
                    this.lblmessage.Text = Mensaje;
                    resultado = false;
                    return resultado;
                }
            }
            else if(situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.QUINTO_SECUNDARIA)
            {
                if (txtNotaMateTercero.Text.ToString() == "" || txtNotaMateTercero.Text.ToString() == "0")
                {
                    String Mensaje = "Es necesario colocar la nota de matemática y comunicación de tercero.";
                    this.mpeMostrarError.Show();
                    this.lblmessage.Text = Mensaje;
                    resultado = false;
                    return resultado;
                }

                if (txtNotaLengTercero.Text.ToString() == "" || txtNotaLengTercero.Text.ToString() == "0")
                {
                    String Mensaje = "Es necesario colocar la nota de matemática y comunicación de tercero.";
                    this.mpeMostrarError.Show();
                    this.lblmessage.Text = Mensaje;
                    resultado = false;
                    return resultado;
                }

                if(CT01Quinto.Text == "")
                {
                    String Mensaje = "Es necesario llenar el detalle de competencias de 5to";
                    this.mpeMostrarError.Show();
                    this.lblmessage.Text = Mensaje;
                    resultado = false;
                    return resultado;
                }
            }

            return resultado;
        }

        /*Ini:Christian Ramirez -REQ91569*/
        #region comentado
        //protected void ddlNotaMateTercero_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UIHelper.SessionActiva(Page);
        //    if (ddlNotaMateTerceroPopup.SelectedValue == UIConstantes._valorNotaOtro)
        //        this.txtNotaMateTerceroPopup.Visible = true;
        //    else
        //        this.txtNotaMateTerceroPopup.Visible = false;
        //}

        //protected void ddlNotaMateCuarto_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UIHelper.SessionActiva(Page);
        //    if (ddlNotaMateCuartoPopup.SelectedValue == UIConstantes._valorNotaOtro)
        //        this.txtNotaMateCuartoPopup.Visible = true;
        //    else
        //        this.txtNotaMateCuartoPopup.Visible = false;
        //}


        //protected void ddlNotaMateQuinto_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UIHelper.SessionActiva(Page);
        //    if (ddlNotaMateQuinto.SelectedValue == UIConstantes._valorNotaOtro)
        //    {
        //        this.txtNotaMateQuinto.Visible = true;
        //    }
        //    else
        //    {
        //        this.txtNotaMateQuinto.Visible = false;
        //    }
        //}

        //protected void ddlNotaLengTercero_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UIHelper.SessionActiva(Page);
        //    if (ddlNotaLengTercero.SelectedValue == UIConstantes._valorNotaOtro)
        //    {
        //        this.txtNotaLengTercero.Visible = true;
        //    }
        //    else
        //    {
        //        this.txtNotaLengTercero.Visible = false;
        //    }
        //}


        //protected void ddlNotaLengCuarto_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UIHelper.SessionActiva(Page);
        //    if (ddlNotaLengCuarto.SelectedValue == UIConstantes._valorNotaOtro)
        //    {
        //        this.txtNotaLengCuarto.Visible = true;
        //    }
        //    else
        //    {
        //        this.txtNotaLengCuarto.Visible = false;
        //    }
        //}


        //protected void ddlNotaLengQuinto_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UIHelper.SessionActiva(Page);
        //    if (ddlNotaLengQuinto.SelectedValue == UIConstantes._valorNotaOtro)
        //    {
        //        this.txtNotaLengQuinto.Visible = true;
        //    }
        //    else
        //    {
        //        this.txtNotaLengQuinto.Visible = false;
        //    }
        //}

        //protected void ddlNotaPromTercero_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlNotaPromTercero.SelectedValue == UIConstantes._valorNotaOtro)
        //    {
        //        this.txtNotaPromTercero.Visible = true;
        //    }
        //    else
        //    {
        //        this.txtNotaPromTercero.Visible = false;
        //    }
        //}

        //protected void ddlNotaPromCuarto_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlNotaPromCuarto.SelectedValue == UIConstantes._valorNotaOtro)
        //    {
        //        this.txtNotaPromCuarto.Visible = true;
        //    }
        //    else
        //    {
        //        this.txtNotaPromCuarto.Visible = false;
        //    }
        //}

        //protected void ddlNotaPromQuinto_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlNotaPromQuinto.SelectedValue == UIConstantes._valorNotaOtro)
        //    {
        //        this.txtNotaPromQuinto.Visible = true;
        //    }
        //    else
        //    {
        //        this.txtNotaPromQuinto.Visible = false;
        //    }
        //}
        #endregion
        /*Fin:Christian Ramirez -REQ91569*/

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
                    if (PaginaActual == UIConstantes.Formularios.F06)
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

        private void CargarCombos()
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

        private bool ValidarOrderMeritoNumerico(ref AplicanteBE oAplicanteBE)
        {
            bool validacionProcesar = true;
            
            AplicanteBL oAplicanteBL = null;
            DataTable dtValidacion = null;
            string ordMeritoTer, ordMeritoCua, ordMeritoQui, cantAlumTer, cantAlumCua, cantAlumQui = null;

            int? mdodPostulacion = oAplicanteBE.ModalidadPostulacion;
            ordMeritoTer = txtOrdenMeritoTercero.Text == "" ? "0" : txtOrdenMeritoTercero.Text;
            ordMeritoCua = txtOrdenMeritoCuarto.Text == "" ? "0" : txtOrdenMeritoCuarto.Text;
            ordMeritoQui = txtOrdenMeritoQuinto.Text == "" ? "0" : txtOrdenMeritoQuinto.Text;
            cantAlumTer = txtNroAlmunosTercero.Text == "" ? "0" : txtNroAlmunosTercero.Text;
            cantAlumCua = txtNroAlmunosCuarto.Text == "" ? "0" : txtNroAlmunosCuarto.Text;
            cantAlumQui = txtNroAlmunosQuinto.Text == "" ? "0" : txtNroAlmunosQuinto.Text;

            #region Si es Modalidad SELECTIVA
            if (mdodPostulacion == 40 || mdodPostulacion == 58 || mdodPostulacion == 60)
            {
                oAplicanteBL = new AplicanteBL();
                dtValidacion = oAplicanteBL.ObtenerValidacionOrdenMerito(oAplicanteBE.IdAplicante, ordMeritoTer,
                    ordMeritoCua, ordMeritoQui, cantAlumTer, cantAlumCua, cantAlumQui, oAplicanteBE.ModalidadPostulacion);

                if (dtValidacion != null && dtValidacion.Rows.Count > 0)
                {
                    Int32? Estado = UIConvertNull.Int32(dtValidacion.Rows[0][0].ToString());
                    String Mensaje = dtValidacion.Rows[0][1].ToString();
                    if (Estado == 0)
                    {
                        this.mpeMostrarError.Show();
                        this.lblmessage.Text = Mensaje;
                        //txtOrdenMeritoTercero.Focus();
                        validacionProcesar = false;
                        return validacionProcesar;
                    }
                }
            }
            #endregion

            #region Si es EXCELENCIA ACADEMICA
            if (mdodPostulacion == 49 || mdodPostulacion == 57 || mdodPostulacion == 59)
            {
                oAplicanteBL = new AplicanteBL();
                dtValidacion = oAplicanteBL.ObtenerValidacionOrdenMerito(oAplicanteBE.IdAplicante, ordMeritoTer,
                    ordMeritoCua, ordMeritoQui, cantAlumTer, cantAlumCua, cantAlumQui, oAplicanteBE.ModalidadPostulacion);

                if (dtValidacion != null && dtValidacion.Rows.Count > 0)
                {
                    Int32? Estado = UIConvertNull.Int32(dtValidacion.Rows[0][0].ToString());
                    String Mensaje = dtValidacion.Rows[0][1].ToString();
                    if (Estado == 0)
                    {
                        this.mpeMostrarError.Show();
                        // this.lblmessage.Text = "Usted no cumple con el requisito de encontrarse en el quinto superior del cuadro de mérito de su promoción."; //CMCS
                        this.lblmessage.Text = Mensaje;
                        //txtOrdenMeritoTercero.Focus();
                        validacionProcesar = false;
                        return validacionProcesar;
                    }
                }
            }
            #endregion

            // Si es BACHILLETATO INTERNACIONAL Ticket RQ 45752 07/06/2018
            if (mdodPostulacion == 41) { }

            //oAplicanteBE = new AplicanteBE();
            oAplicanteBE = obtenerDatosInformacionAcademica(oAplicanteBE);

            return validacionProcesar;
        }

        private void GuardarDatos()
        {
            /*Ini:Christian Ramirez -REQ91569*/
            try
            {
                bool validacionProcesar = true;

                AplicanteBE oAplicanteBE = new AplicanteBE()
                {
                    IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]),
                    ModalidadPostulacion = UIConvertNull.Int32(Session["ModPostulacion"])
                };


                DataTable dtPagSigui = null;
                string PaginaActual, PaginaSiguiente = null;

                validacionProcesar = ValidarOrderMeritoNumerico(ref oAplicanteBE);

                if (validacionProcesar)
                {
                    ObtenerDatosLetrasParaRegistro(ref oAplicanteBE);
                    AplicanteBL oAplicanteBL = new AplicanteBL();
                    Boolean operacionOK = oAplicanteBL.InsertaDatosFormCinco_OrdenMerito_Y_NotaLetra(oAplicanteBE);

                    if (operacionOK)
                    {
                        Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);

                        GeneralBL oGeneralBL = new GeneralBL();
                        dtPagSigui = oGeneralBL.obtenerSiguientePagina(ModalidadId);

                        for (Int32 i = 0; i < dtPagSigui.Rows.Count; i++)
                        {
                            PaginaActual = dtPagSigui.Rows[i]["NombreFormulario"].ToString();
                            if (PaginaActual == UIConstantes.Formularios.F06)
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
                        Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()
                            [UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] + "&descError=" + "Ha ocurrido un error en el registro", false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            /*Fin:Christian Ramirez -REQ91569*/
        }


        private AplicanteBE obtenerDatosInformacionAcademica(AplicanteBE oAplicanteBE)
        {
            EducacionDetalleBE DetColegioTercero = null;
            EducacionDetalleBE DetColegioCuarto = null;
            EducacionDetalleBE DetColegioQuinto = null;

            /*Ini:Christian Ramirez -REQ91569*/
            int tipoCalificacionTercero = Convert.ToInt32(ddlTipoCalificacionTercero.SelectedValue);
            int tipoCalificacionCuarto = Convert.ToInt32(ddlTipoCalificacionCuarto.SelectedValue);
            int tipoCalificacionQuinto = Convert.ToInt32(ddlTipoCalificacionQuinto.SelectedValue);
            /*Fin:Christian Ramirez -REQ91569*/

            #region Tercer Grado
            //if (tipoCalificacionTercero == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO)
            //{
            if (DetColegioTercero == null) DetColegioTercero = new EducacionDetalleBE();
            DetColegioTercero.IdApplication = UIConvertNull.Int32(Session["AplicanteId"].ToString());
            DetColegioTercero.IdApplicationEducation = UIConvertNull.Int32(ddlColegioTercero.SelectedValue);
            DetColegioTercero.IdApplicationEducationEnroll = UIConvertNull.Int32(txtIdApplicationEducationEnrollTercero.Text);
            DetColegioTercero.IdGrado = Int32.Parse(UIConstantes.GRADO_PREGRADO.TERCERO_SECUNDARIA.ToString("D"));  //Tercero - 33

            String tmpFechaIniTercero = "01/01/" + this.ddlAnioLectivoTercero.SelectedItem.Text;
            CultureInfo culturaIniTercero = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            DateTime tempFechaIniTercero;

            if (DateTime.TryParse(tmpFechaIniTercero, culturaIniTercero, System.Globalization.DateTimeStyles.None, out tempFechaIniTercero))
                DetColegioTercero.FechaInicio = UIConvertNull.DateTime(tmpFechaIniTercero);

            String tmpFechaFinTercero = "31/12/" + this.ddlAnioLectivoTercero.SelectedItem.Text;
            CultureInfo culturaFinTercero = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            DateTime tempFechaFinTercero;

            if (DateTime.TryParse(tmpFechaFinTercero, culturaFinTercero, System.Globalization.DateTimeStyles.None, out tempFechaFinTercero))
                DetColegioTercero.FechaFin = UIConvertNull.DateTime(tmpFechaFinTercero);

            DetColegioTercero.SituaAcademica = UIConvertNull.Int32(Session["SituacionAcademica"]);
            DetColegioTercero.IdMerito = UIConvertNull.Int32(txtOrdenMeritoTercero.Text);
            DetColegioTercero.CantidadEstudiantes = UIConvertNull.Int32(txtNroAlmunosTercero.Text);

            /*Ini:Christian Ramirez -REQ91569*/
            //if (ddlNotaMateTercero.SelectedValue == UIConstantes._valorNotaOtro) CMCS RQ89808
            string notaMateTercero = txtNotaMateTercero.Text.Trim();
            if (notaMateTercero == "AD" || notaMateTercero == "A" || notaMateTercero == "B" || notaMateTercero == "C")
                DetColegioTercero.OtraNotaMateTercero = notaMateTercero;
            else
                DetColegioTercero.NotaMateTercero = UIConvertNull.Int32(notaMateTercero);

            //if (ddlNotaLengTercero.SelectedValue == UIConstantes._valorNotaOtro) CMCS RQ89808
            string notaLengTercero = txtNotaLengTercero.Text.Trim();
            if (notaLengTercero == "AD" || notaLengTercero == "A" || notaLengTercero == "B" || notaLengTercero == "C")
                DetColegioTercero.OtraNotaLengTercero = notaLengTercero;
            else
                DetColegioTercero.NotaLengTercero = UIConvertNull.Int32(notaLengTercero);

            DetColegioTercero.CodTipoCalificacion = Convert.ToInt32(ddlTipoCalificacionTercero.SelectedValue);
            DetColegioTercero.DescTipoCalificacion = ddlTipoCalificacionTercero.SelectedItem.Text;
            /*Fin:Christian Ramirez -REQ91569*/

            DetColegioTercero.Revision_Opid = Session["usrRedId"].ToString();
            //}
            #endregion

            #region Cuarto Grado
            //if (tipoCalificacionCuarto == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO)
            //{
            if (DetColegioCuarto == null) DetColegioCuarto = new EducacionDetalleBE();
            DetColegioCuarto.IdApplication = UIConvertNull.Int32(Session["AplicanteId"].ToString());
            DetColegioCuarto.IdApplicationEducation = UIConvertNull.Int32(ddlColegioCuarto.SelectedValue);
            DetColegioCuarto.IdApplicationEducationEnroll = UIConvertNull.Int32(txtIdApplicationEducationEnrollCuarto.Text);
            DetColegioCuarto.IdGrado = Int32.Parse(UIConstantes.GRADO_PREGRADO.CUARTO_SECUNDARIA.ToString("D"));  //Cuarto - 8

            String tmpFechaIniCuarto = "01/01/" + this.ddlAnioLectivoCuarto.SelectedItem.Text;
            CultureInfo culturaIniCuarto = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            DateTime tempFechaIniCuarto;

            if (DateTime.TryParse(tmpFechaIniCuarto, culturaIniCuarto, System.Globalization.DateTimeStyles.None, out tempFechaIniCuarto))
                DetColegioCuarto.FechaInicio = UIConvertNull.DateTime(tmpFechaIniCuarto);

            String tmpFechaFinCuarto = "31/12/" + this.ddlAnioLectivoCuarto.SelectedItem.Text;
            CultureInfo culturaFinCuarto = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            DateTime tempFechaFinCuarto;
            if (DateTime.TryParse(tmpFechaIniCuarto, culturaFinCuarto, System.Globalization.DateTimeStyles.None, out tempFechaFinCuarto))
                DetColegioCuarto.FechaFin = UIConvertNull.DateTime(tmpFechaFinCuarto);

            DetColegioCuarto.SituaAcademica = UIConvertNull.Int32(Session["SituacionAcademica"]);
            DetColegioCuarto.IdMerito = UIConvertNull.Int32(txtOrdenMeritoCuarto.Text);
            DetColegioCuarto.CantidadEstudiantes = UIConvertNull.Int32(txtNroAlmunosCuarto.Text);

            /*Ini:Christian Ramirez -REQ91569*/
            //if (ddlNotaMateCuarto.SelectedValue == UIConstantes._valorNotaOtro)CMCS 89808
            string notaMateCuarto = txtNotaMateCuarto.Text.Trim();
            if (notaMateCuarto == "AD" || notaMateCuarto == "A" || notaMateCuarto == "B" || notaMateCuarto == "C")
                DetColegioCuarto.OtraNotaMateCuarto = notaMateCuarto;
            else
                DetColegioCuarto.NotaMateCuarto = UIConvertNull.Int32(notaMateCuarto);

            //if (ddlNotaLengCuarto.SelectedValue == UIConstantes._valorNotaOtro) CMCS 89808
            string notaLengCuarto = txtNotaLengCuarto.Text.Trim();
            if (notaLengCuarto == "AD" || notaLengCuarto == "A" || notaLengCuarto == "B" || notaLengCuarto == "C")
                DetColegioCuarto.OtraNotaLengCuarto = notaLengCuarto;
            else
                DetColegioCuarto.NotaLengCuarto = UIConvertNull.Int32(notaLengCuarto);

            DetColegioCuarto.CodTipoCalificacion = Convert.ToInt32(ddlTipoCalificacionCuarto.SelectedValue);
            DetColegioCuarto.DescTipoCalificacion = ddlTipoCalificacionCuarto.SelectedItem.Text;
            /*Fin:Christian Ramirez -REQ91569*/

            DetColegioCuarto.Revision_Opid = Session["usrRedId"].ToString();
            //}
            #endregion

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
                if (DetColegioTercero != null) oAplicanteBE.LDetalleEducacion.Add(DetColegioTercero);
                if (DetColegioCuarto != null) oAplicanteBE.LDetalleEducacion.Add(DetColegioCuarto);
                if (DetColegioQuinto != null) oAplicanteBE.LDetalleEducacion.Add(DetColegioQuinto);
            }
            return oAplicanteBE;
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
                    Div2.Visible = true;
                    Div3.Visible = true;
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


        #endregion


        /*Ini:Christian Ramirez -REQ91569*/
        #region "Eventos y Metodos de Rend Academico Letras"

        #region Eventos
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

        #endregion

        #region Metodos

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

            //Ini:CHRISTIAN RAMIREZ(EXT09) - REQ104565
            if (situacionAcademica == (int)UIConstantes.SITUACION_ACADEMICA.ESTUDIANTE)
            {
                ddlTipoCalificacionTercero.Items.AddRange(new ListItem[] {
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
            //Fin:CHRISTIAN RAMIREZ(EXT09) - REQ104565

            ddlTipoCalificacionTercero.SelectedIndex = 0;
            ddlTipoCalificacionCuarto.SelectedIndex = 0;
            ddlTipoCalificacionQuinto.SelectedIndex = 0;
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

        private bool GuardarDelPopupNotasPorTipoCalificacionYGrado(int tipoCalificacion, string grado)
        {
            bool rpta = true;

            #region Guardar notas del popup => Numerico
            if (tipoCalificacion == (int)UIConstantes.REND_ACADEMICO_TIPO_CALIFICACION.NUMERICO)
            {
                string ordenMeritoPopup = (txtOrdenMeritoPopup.Text == "" ? "0" : txtOrdenMeritoPopup.Text);
                string nroAlmunosPopup = (txtNroAlmunosPopup.Text == "" ? "0" : txtNroAlmunosPopup.Text);
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

        private AplicanteBE ObtenerDatosLetrasParaRegistro(ref AplicanteBE oAplicanteBE)
        {
            RendimientoAcademicoBE oRendimientoAcademicoBETercero = null;
            RendimientoAcademicoBE oRendimientoAcademicoBECuarto = null;
            RendimientoAcademicoBE oRendimientoAcademicoBEQuinto = null;


            int tipoCalificacionTercero = Convert.ToInt32(ddlTipoCalificacionTercero.SelectedValue);
            int tipoCalificacionCuarto = Convert.ToInt32(ddlTipoCalificacionCuarto.SelectedValue);
            int tipoCalificacionQuinto = Convert.ToInt32(ddlTipoCalificacionQuinto.SelectedValue);


            oRendimientoAcademicoBETercero = new RendimientoAcademicoBE()
            {
                ApplicationId = oAplicanteBE.IdAplicante.Value,
                CodTipoCalificacion = tipoCalificacionTercero,
                ApplicationEducationId = Convert.ToInt32(ddlColegioTercero.SelectedValue),
                DegreeId = (int)UIConstantes.GRADO_PREGRADO.TERCERO_SECUNDARIA
            };
            oRendimientoAcademicoBETercero.ListaRendimientoAcademicoEvaluacionBE = ObtenerDatosLetrasParaRegistroTerceroEvaluacion();

            oRendimientoAcademicoBECuarto = new RendimientoAcademicoBE()
            {
                ApplicationId = oAplicanteBE.IdAplicante.Value,
                CodTipoCalificacion = tipoCalificacionCuarto,
                ApplicationEducationId = Convert.ToInt32(ddlColegioCuarto.SelectedValue),
                DegreeId = (int)UIConstantes.GRADO_PREGRADO.CUARTO_SECUNDARIA
            };
            oRendimientoAcademicoBECuarto.ListaRendimientoAcademicoEvaluacionBE = ObtenerDatosLetrasParaRegistroCuartoEvaluacion();

            oRendimientoAcademicoBEQuinto = new RendimientoAcademicoBE()
            {
                ApplicationId = oAplicanteBE.IdAplicante.Value,
                CodTipoCalificacion = tipoCalificacionQuinto,
                ApplicationEducationId = Convert.ToInt32(ddlColegioQuinto.SelectedValue),
                DegreeId = (int)UIConstantes.GRADO_PREGRADO.QUINTO_SECUNDARIA
            };
            oRendimientoAcademicoBEQuinto.ListaRendimientoAcademicoEvaluacionBE = ObtenerDatosLetrasParaRegistroQuintoEvaluacion();

            oAplicanteBE.ListaRendimientoAcademicoBE = new List<RendimientoAcademicoBE>();
            if (oRendimientoAcademicoBETercero != null) oAplicanteBE.ListaRendimientoAcademicoBE.Add(oRendimientoAcademicoBETercero);
            if (oRendimientoAcademicoBECuarto != null) oAplicanteBE.ListaRendimientoAcademicoBE.Add(oRendimientoAcademicoBECuarto);
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

        private List<RendimientoAcademicoEvaluacionBE> ObtenerDatosLetrasParaRegistroCuartoEvaluacion()
        {
            return new List<RendimientoAcademicoEvaluacionBE>()
            {
                new RendimientoAcademicoEvaluacionBE()
                {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciaTecnologia,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Diseña_y_construye_soluciones_tecnológicas_para_resolver_problemas,
                             CalificacionId = UIConvertNull.Int32(CT01CodigoCuarto.Text)
                },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciaTecnologia,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Explica_el_mundo_físico_basándose_en_conocimientos_sobre_los_seres_vivos_materia_y_energía_biodiversidad,
                             CalificacionId = UIConvertNull.Int32(CT02CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciaTecnologia,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Indaga_mediante_metodos_científicos_para_construir_sus_conocimientos,
                             CalificacionId = UIConvertNull.Int32(CT03CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciasSociales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Construye_interpretaciones_historicas,
                             CalificacionId = UIConvertNull.Int32(CS01CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciasSociales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Gestiona_responsablemente_el_espacio_y_el_ambiente,
                             CalificacionId = UIConvertNull.Int32(CS02CodigoCuarto.Text)
                        },
                /*Ini:Christian Ramirez - REQ95070*/
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciasSociales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Gestiona_responsablemente_los_recursos_económicos,
                             CalificacionId = UIConvertNull.Int32(CS03CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CompetenciasTransversales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Gestiona_su_aprendizaje_de_manera_autonoma,
                             CalificacionId = UIConvertNull.Int32(CTR01CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CompetenciasTransversales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Se_desenvuelve_en_los_entornos_virtuales_generados_por_las_TIC,
                             CalificacionId = UIConvertNull.Int32(CTR02CodigoCuarto.Text)
                        },
                /*Fin:Christian Ramirez - REQ95070*/
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.ComunicacionLenguaMaterna,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Escribe_diversos_tipos_de_texto,
                             CalificacionId = UIConvertNull.Int32(CL01CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.ComunicacionLenguaMaterna,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Lee_diversos_tipos_de_textos_escritos,
                             CalificacionId = UIConvertNull.Int32(CL02CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.ComunicacionLenguaMaterna,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Se_comunica_oralmente,
                             CalificacionId = UIConvertNull.Int32(CL03CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_cantidad,
                             CalificacionId = UIConvertNull.Int32(MA01CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_forma_y_movimiento,
                             CalificacionId = UIConvertNull.Int32(MA02CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_gestion_de_datos_e_incertidumbre,
                             CalificacionId = UIConvertNull.Int32(MA03CodigoCuarto.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_regularidad_equivalencia_y_cambio,
                             CalificacionId = UIConvertNull.Int32(MA04CodigoCuarto.Text)
                        },
            };
        }

        private List<RendimientoAcademicoEvaluacionBE> ObtenerDatosLetrasParaRegistroTerceroEvaluacion()
        {
            return new List<RendimientoAcademicoEvaluacionBE>()
            {
                new RendimientoAcademicoEvaluacionBE()
                {
                     CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciaTecnologia,
                     CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                        .Diseña_y_construye_soluciones_tecnológicas_para_resolver_problemas,
                     CalificacionId = UIConvertNull.Int32(CT01CodigoTercero.Text)
                },
                new RendimientoAcademicoEvaluacionBE()
                {
                     CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciaTecnologia,
                     CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                        .Explica_el_mundo_físico_basándose_en_conocimientos_sobre_los_seres_vivos_materia_y_energía_biodiversidad,
                     CalificacionId = UIConvertNull.Int32(CT02CodigoTercero.Text)
                },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciaTecnologia,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Indaga_mediante_metodos_científicos_para_construir_sus_conocimientos,
                             CalificacionId = UIConvertNull.Int32(CT03CodigoTercero.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciasSociales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Construye_interpretaciones_historicas,
                             CalificacionId = UIConvertNull.Int32(CS01CodigoTercero.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciasSociales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Gestiona_responsablemente_el_espacio_y_el_ambiente,
                             CalificacionId = UIConvertNull.Int32(CS02CodigoTercero.Text)
                        },
                /*Ini:Christian Ramirez - REQ95070*/
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CienciasSociales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Gestiona_responsablemente_los_recursos_económicos,
                             CalificacionId = UIConvertNull.Int32(CS03CodigoTercero.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CompetenciasTransversales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Gestiona_su_aprendizaje_de_manera_autonoma,
                             CalificacionId = UIConvertNull.Int32(CTR01CodigoTercero.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.CompetenciasTransversales,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Se_desenvuelve_en_los_entornos_virtuales_generados_por_las_TIC,
                             CalificacionId = UIConvertNull.Int32(CTR02CodigoTercero.Text)
                        },
                /*Fin:Christian Ramirez - REQ95070*/
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.ComunicacionLenguaMaterna,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Escribe_diversos_tipos_de_texto,
                             CalificacionId = UIConvertNull.Int32(CL01CodigoTercero.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.ComunicacionLenguaMaterna,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Lee_diversos_tipos_de_textos_escritos,
                             CalificacionId = UIConvertNull.Int32(CL02CodigoTercero.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.ComunicacionLenguaMaterna,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Se_comunica_oralmente,
                             CalificacionId = UIConvertNull.Int32(CL03CodigoTercero.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_cantidad,
                             CalificacionId = UIConvertNull.Int32(MA01CodigoTercero.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_forma_y_movimiento,
                             CalificacionId = UIConvertNull.Int32(MA02CodigoTercero.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_gestion_de_datos_e_incertidumbre,
                             CalificacionId = UIConvertNull.Int32(MA03CodigoTercero.Text)
                        },
                new RendimientoAcademicoEvaluacionBE()
                        {
                             CursoId = (int)UIConstantes.REND_ACADEMICO_CURSO.Matematica,
                             CompetenciaId = (int)UIConstantes.REND_ACADEMICO_COMPETENCIA
                                .Resuelve_problemas_de_regularidad_equivalencia_y_cambio,
                             CalificacionId = UIConvertNull.Int32(MA04CodigoTercero.Text)
                        },
            };
        }
        #endregion

        #endregion "Eventos y Metodos de Rend Academico Letras"
        /*Fin:Christian Ramirez -REQ91569*/
    }
}
