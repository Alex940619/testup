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
using System.Text; /*Se agrega:[Christian Ramirez - Caso78630]*/
using System.Reflection; /*Se agrega:[Christian Ramirez - Caso78630]*/

namespace UPAdmPre.Web.Admision
{
    public partial class frm18_Entrevista : System.Web.UI.Page
    {
        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                ///*Solo para pruebas*/
                //Session["usrRedId"] = "U.Admision";
                //Session["ModPostulacion"] = 42;
                //Session["AplicanteId"] = 110603;

                if (!IsPostBack)
                {
                    Int32? AplicanteId = null;
                    AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);

                    /*Ini:[Juan Delgado - Caso84943] 20201103*/
                    //Se inicia con el botón principal en false
                    //El botón se habilitará si es que el postulante 
                    //puede escoger algun horario
                    imgBtnEnviarTodo.Visible = false;
                    /*Fin:[Juan Delgado - Caso84943] 20201103*/

                    this.CargarTextos();
                    this.CargarTextosEtiquetas();
                    //this.CargarComboHorarios(AplicanteId); /*Se comenta:Christian Ramirez - GIIT[caso60747] - 20190521*/
                    this.LLenarDatosPersona();
                    this.CargarEntrevistaRegistrado(AplicanteId);
                    this.LLenarGrillaReferencias();

                    /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
                    ValidarTipoDeEntrevista();
                    ObtenerTipoEntrevista();
                    /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/

                    CargarYValidarHorarioEcl(AplicanteId); /*Se agrega:[Christian Ramirez - Caso78630]*/

                    CargarYValidarHorarioPC(AplicanteId); /*Se agrega:[Juan Delgado - Caso81646]*/

                }
            }
            catch (Exception ex)
            {
                UIHelper.EnviarCorreo(UIConstantes.Formularios.F18, ex.Message.Replace("\n", ""), UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));
                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_CARGAR_DATOS_FORMULARIO], false);
            }
        }

        protected void gvReferencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drw = (DataRowView)e.Row.DataItem;
                    DataRow dr = drw.Row;
                    e.Row.Cells[0].Text = ((this.gvReferencias.PageIndex * this.gvReferencias.PageSize) + (e.Row.RowIndex + 1)).ToString();

                    Label _lblReference = (Label)e.Row.FindControl("lblReference");
                    Label _lblErrorEnvioReferencia = (Label)e.Row.FindControl("lblErrorEnvioReferencia");
                    Image _imgEstadoRespuesta = (Image)e.Row.FindControl("imgEstadoRespuesta");
                    ImageButton _imgBtnEnviarCorreoRef = (ImageButton)e.Row.FindControl("imgBtnEnviarEmail");
                    ImageButton _imgBtnEditarReferente = (ImageButton)e.Row.FindControl("imgBtnEditar");
                    Label _lblEstado = (Label)e.Row.FindControl("lblEstado");
                    Label _lblEstadoPostulacion = (Label)e.Row.FindControl("lblEstadoApplication");

                    if (UIConvertNull.String(dr["EstadoApplication"]) != "1")
                    {
                        this.trRef.Visible = true;
                    }
                    else
                    {
                        this.trRef.Visible = false;
                    }

                    if (UIConvertNull.String(dr["Estado"]) != null)
                    {
                        if (UIConvertNull.String(dr["Estado"]) == "0")
                        {
                            _imgEstadoRespuesta.ImageUrl = "../Images/icoPendiente.png";
                            _lblReference.Text = UIConvertNull.String(dr["NombreEvaluador"]) + ": PENDIENTE";
                            _lblReference.ForeColor = System.Drawing.Color.Red;
                            _imgBtnEnviarCorreoRef.Visible = true;
                            _imgBtnEditarReferente.Visible = true;
                        }
                        if (UIConvertNull.String(dr["Estado"]) == "1")
                        {
                            _imgEstadoRespuesta.ImageUrl = "../Images/icoRevisado.png";
                            _lblReference.Text = UIConvertNull.String(dr["NombreEvaluador"]) + ": RECIBIDA";
                            _lblReference.ForeColor = System.Drawing.Color.Green;
                            _imgBtnEnviarCorreoRef.Visible = false;
                            _imgBtnEditarReferente.Visible = false;
                        }
                    }

                    String strCadena = UIConstantes._valorCadenaVacia;
                    strCadena = strCadena + UIHelper.AsignarDatoControlHtml(this.hIdReferente.ClientID, UIConvertNull.String(dr["ApplicationReferenceId"]));
                    UIHelper.SeleccionarItemGrillaOnClickMoverRaton(e, strCadena);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            ReferenciaBE oReferenciaBE = null;
            ReferenciaBL oReferenciaBL = null;
            EncryptBL oEncryptBL = null;
            try
            {
                oReferenciaBE = new ReferenciaBE();
                oReferenciaBE = ObtenerReferencia();

                oReferenciaBL = new ReferenciaBL();
                oReferenciaBL.modificarReferencia(oReferenciaBE);
                if (Request.QueryString["ErrorEnvio"] == "1")
                {
                    if (oReferenciaBE.ErrorEnvioEmail == 1)
                    {
                        oEncryptBL = new EncryptBL();
                        string sIdReferencia = HttpUtility.HtmlDecode(oEncryptBL.EncryptKey(oReferenciaBE.IdReferencia.ToString()));
                        string sIdApplication = HttpUtility.HtmlDecode(oEncryptBL.EncryptKey(oReferenciaBE.IdAplicacion.ToString()));

                        oReferenciaBL.enviaErrorEmailReferenciaAplicante(oReferenciaBE.IdReferencia, sIdReferencia, sIdApplication, false);
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Existen datos incorrectos en la referencia registrada. Se envío email al postulante para su revisión.');", true);
                        this.lblmessage.Text = "Existen datos incorrectos en la referencia registrada. Se envío email al postulante para su revisión.";
                        this.mpeMostrarError.Show();

                        string Script = "closeAndRefreshError();";
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", Script, true);
                        this.lblmessage.Text = Script;
                        this.mpeMostrarError.Show();
                    }
                }
                else
                {
                    oEncryptBL = new EncryptBL();
                    string sIdReferencia = HttpUtility.HtmlDecode(oEncryptBL.EncryptKey(oReferenciaBE.IdReferencia.ToString()));
                    oReferenciaBL.enviaEmailReferenciaAplicante(oReferenciaBE.IdReferencia, sIdReferencia, false);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Se envío email a la referencia satisfactoriamente.');", true);
                    this.lblmessage.Text = "Se envío email a la referencia satisfactoriamente.";
                    this.mpeMostrarError.Show();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oReferenciaBE = null;
                oReferenciaBL = null;
                oEncryptBL = null;
                this.mpeMostrarEnviarCorreo.Hide();
                this.LLenarGrillaReferencias();
                this.upReferencias.Update();
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.mpeMostrarEnviarCorreo.Hide();
        }

        protected void imgBtnEnviarEmail_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            ImageButton _imgBtnEnviarEmail = (ImageButton)(sender);
            GridViewRow gvrow = (GridViewRow)_imgBtnEnviarEmail.NamingContainer;
            Int32? idReferente = null;
            try
            {
                idReferente = UIConvertNull.Int32(hIdReferente.Value);
                this.EnviarCorreo(idReferente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void imgBtnEditarReferente_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);
            ImageButton _imgBtnEnviarEmail = (ImageButton)(sender);
            GridViewRow gvrow = (GridViewRow)_imgBtnEnviarEmail.NamingContainer;
            Int32? idReferente = null;
            try
            {
                idReferente = UIConvertNull.Int32(hIdReferente.Value);
                this.EditarDatosReferente(idReferente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*Ini:[Juan Delgado - Caso84943] 20201103*/
        protected void imgBtnEnviarTodo_Click(object sender, ImageClickEventArgs e)
        {
            UIHelper.SessionActiva(Page);

            // Comprobando que si hay horarios disponibles este seleccionado alguno
            if (ddlHorarioPC.Items.Count > 1 && ddlHorarioPC.SelectedValue == "0")
            {
                lblmessage.Text = "¡ERROR!: Debe seleccionar un horario para la Prueba de Conectividad";
                mpeMostrarError.Show();
                return;
            }

            if (ddlHorarioECL.Items.Count > 1 && ddlHorarioECL.SelectedValue == "0")
            {
                lblmessage.Text = "¡ERROR!: Debe seleccionar un horario para la ECL";
                mpeMostrarError.Show();
                return;
            }

            if (ddlHorario.Items.Count > 1 && ddlHorario.SelectedValue == "0")
            {
                lblmessage.Text = "¡ERROR!: Debe seleccionar un horario para la entrevista";
                mpeMostrarError.Show();
                return;
            }

            //*********************************************************************
            //Guardando los datos de las entrevistas habilitadas
            try
            {
                if (ddlHorarioPC.Items.Count > 1 && ddlHorarioPC.SelectedValue != "0" && ddlHorarioPC.Enabled == true)
                {
                    GuardarDatosPCOnline();
                }

                if (ddlHorarioECL.Items.Count > 1 && ddlHorarioECL.SelectedValue != "0" && ddlHorarioECL.Enabled == true)
                {
                    GuardarDatosEclOnline();
                }


                if (ddlHorario.Items.Count > 1 && ddlHorario.SelectedValue != "0" && ddlHorario.Enabled == true) {
                    GuardarDatosEntrevista();
                    this.GuardarDatos();
                }

                //Ocultando el boton enviar
                imgBtnEnviarTodo.Visible = false;
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                object errProcedure = ex.GetType().GetProperty("Procedure").GetValue(ex, null);

                sb.Append("Procedure:");
                sb.Append(errProcedure.ToString());
                sb.Append("; Mensaje:");
                sb.Append(ex.Message.Replace("\n", ""));

                UIHelper.EnviarCorreo(UIConstantes.Formularios.F18
                    , sb.ToString()
                    , UIConvertNull.String(Session["usrRedId"]), UIConvertNull.String(Session["AplicanteId"]));

                Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" + UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO]
                    , false);
            }
        }
        /*Fin:[Juan Delgado - Caso84943] 20201103*/

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            this.mpeMostrarError.Hide();
        }

        /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
        protected void ddlTipoEntrevista_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CargarComboHorarios(UIConvertNull.Int32(Session["AplicanteId"]), ddlTipoEntrevista.Text);
        }
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/

        #endregion "Eventos"



        #region "Métodos Privados"

        private void CargarTextos()
        {
            this.ObtenerTextoEnsayo(UIConvertNull.Int32(Session["AplicanteId"]), 25);

            this.ObtenerTextoInformativo(UIConvertNull.Int32(Session["AplicanteId"]), 16);
            this.lblMensajeInformativoInf.Visible = false;
        }

        private void CargarTextosEtiquetas()
        {
            if (UIConvertNull.Int32(Session["ModPostulacion"]) == 42)
            {
                this.lblTituloSeleccion.Text = "Selección de Horario";
                this.lblSubTituloHorario.Text = "Horario de Clases:";
            }
            else
            {
                this.lblTituloSeleccion.Text = "Selección de Entrevista";
                this.lblSubTituloHorario.Text = "Selecciona un turno:";
            }
        }
        private void CargarComboHorarios(Int32? AplicanteId, string tipoEntrevista = null)/*Se agrega:Christian Ramirez - GIIT[caso60747] - 20190522*/
        {
            GeneralBL oGeneralBL = null;
            try
            {
                oGeneralBL = new GeneralBL();

                /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
                //string tipEvaluacion = ddlTipoEntrevista.Text;
                DataTable dtHorarios = oGeneralBL.ObtenerHorariosDeEntrevista(AplicanteId, tipoEntrevista);
                /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/

                if (dtHorarios != null && dtHorarios.Rows.Count > 0)
                {
                    Funciones.cargarComboYSeleccione(this.ddlHorario, dtHorarios.Copy(), "Descripcion", "Codigo", "-- Seleccionar --");
                    if (ddlTipoEntrevista.Enabled) {
                        imgBtnEnviarTodo.Visible = true;
                    }
                    
                }
                else
                {
                    //Funciones.cargarComboYSeleccione(this.ddlHorario, null, "Descripcion", "Codigo", "-- Seleccionar --");
                    ddlHorario.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    this.lblMensajeEnsayo.Text = strTexto;
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
                    //this.blMensajeInformativoSup.Text = strTexto;
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

        private void LLenarDatosPersona()
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtDatos = null;
            Int32? AplicanteId = null;
            try
            {
                AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                oAplicanteBL = new AplicanteBL();
                dtDatos = oAplicanteBL.ObtenerDatosPostulanteParaEntrevista(AplicanteId);
                if (dtDatos != null && dtDatos.Rows.Count > 0)
                {
                    this.lblPostulante.Text = dtDatos.Rows[0]["Aplicante"].ToString();
                    this.lblNroDocumento.Text = dtDatos.Rows[0]["DNI"].ToString();
                    this.lblModalidad.Text = dtDatos.Rows[0]["Modalidad"].ToString();
                    this.lblCarrera.Text = dtDatos.Rows[0]["Carrera"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GuardarDatos()
        {
            AplicanteBL oAplicanteBL = null;
            Int32? AplicanteId = null;
            String strHoraEntrevista = null;
            try
            {
                AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                strHoraEntrevista = UIConvertNull.String(ddlHorario.SelectedValue);

                oAplicanteBL = new AplicanteBL();
                Boolean operacionOK = oAplicanteBL.InsertaDatosFormDieciOcho_HoraEntrevista(AplicanteId, strHoraEntrevista);
                if (operacionOK)
                {
                    this.pnlHorario.Enabled = false;
                    this.ddlHorario.Enabled = false;
                    this.ddlTipoEntrevista.Enabled = false; /*Ini:Christian Ramirez - GIIT[caso60747] - 20190522*/
                    //this.ObtenerTextoInformativoInf(UIConvertNull.Int32(Session["AplicanteId"]), 64);
                    this.ObtenerTextoInformativoInf(UIConvertNull.Int32(Session["AplicanteId"]), 16);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Se registro su horario con exito.');", true);
                    this.lblmessage.Text = "Se registro su(s) horario(s) con exito.";
                    this.mpeMostrarError.Show();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarEntrevistaRegistrado(Int32? AplicanteId)
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtEntrevista = null;
            try
            {
                oAplicanteBL = new AplicanteBL();
                dtEntrevista = oAplicanteBL.ObtenerEntrevistaRegistrado(AplicanteId);
                if (dtEntrevista != null && dtEntrevista.Rows.Count > 0)
                {
                    this.ddlHorario.SelectedValue = dtEntrevista.Rows[0][0].ToString();
                    this.ddlHorario.Enabled = false;
                    this.ddlTipoEntrevista.Enabled = false;  /*Ini:Christian Ramirez - GIIT[caso60747] - 20190522*/
                    //this.imgBtnEnviar.Visible = false; /*[Juan Delgado - Caso84943] 20201103*/                    
                    this.lblMensajeInformativoInf.Visible = true;
                    //this.ObtenerTextoInformativoInf(UIConvertNull.Int32(Session["AplicanteId"]), 64);
                    this.ObtenerTextoInformativoInf(UIConvertNull.Int32(Session["AplicanteId"]), 16);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtenerDatosReferente(Int32? ReferenceId)
        {
            ReferenciaBL oReferenciaBL = null;
            DataTable dtReferencia = null;
            try
            {
                oReferenciaBL = new ReferenciaBL();
                dtReferencia = oReferenciaBL.ObtenerApplicationReferencebyId(ReferenceId);
                this.txtNombre.Text = dtReferencia.Rows[0]["FirstName"].ToString();
                this.txtApellido.Text = dtReferencia.Rows[0]["LastName"].ToString();
                this.txtCargo.Text = dtReferencia.Rows[0]["Charge"].ToString();
                this.txtEmail.Text = dtReferencia.Rows[0]["Email"].ToString();
                this.txtInstitucion.Text = dtReferencia.Rows[0]["OrganizationName"].ToString();
                if (dtReferencia.Rows[0]["ErrorEnvioEmail"].ToString() == "1")
                {
                    this.cbxErrorEnvio.Checked = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReferenciaBE ObtenerReferencia()
        {
            try
            {
                ReferenciaBE oReferenciaBE = new ReferenciaBE();
                oReferenciaBE.IdReferencia = UIConvertNull.Int32(hIdReferente.Value);
                oReferenciaBE.IdAplicacion = UIConvertNull.Int32(Session["AplicanteId"]);
                oReferenciaBE.Firstname = this.txtNombre.Text;
                oReferenciaBE.LastName = this.txtApellido.Text;
                oReferenciaBE.Cargo = this.txtCargo.Text;
                oReferenciaBE.Email = this.txtEmail.Text;
                oReferenciaBE.NombreOrganizacion = this.txtInstitucion.Text;
                oReferenciaBE.NombrePersona = this.txtNombre.Text + " " + this.txtApellido.Text;
                if (oReferenciaBE.NombrePersona.Length >= 60)
                {
                    oReferenciaBE.NombrePersona = oReferenciaBE.NombrePersona.Substring(0, 60);
                }
                oReferenciaBE.Status = 1;
                if (Request.QueryString["ErrorEnvio"] == "1")
                {
                    if (this.cbxErrorEnvio.Checked == true)
                    {
                        oReferenciaBE.ErrorEnvioEmail = 1;
                    }
                    else
                    {
                        oReferenciaBE.ErrorEnvioEmail = 0;
                    }
                }
                else
                {
                    oReferenciaBE.ErrorEnvioEmail = 0;
                }
                return oReferenciaBE;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LLenarGrillaReferencias()
        {
            AplicanteBL oAplicanteBL = null;
            DataTable dtReferente = new DataTable();
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            try
            {
                oAplicanteBL = new AplicanteBL();
                dtReferente = oAplicanteBL.obtejerListadoReferentesEstado(AplicanteId);
                if (dtReferente != null && dtReferente.Rows.Count > 0)
                {
                    this.gvReferencias.DataSource = dtReferente;
                    this.gvReferencias.DataBind();
                }
                else
                {
                    this.gvReferencias.DataSource = null;
                    this.gvReferencias.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EnviarCorreo(Int32? idReferente)
        {
            EncryptBL oEncryptBL = null;
            ReferenciaBL oReferenciaBL = null;
            try
            {
                String strIdReferente = null;
                oEncryptBL = new EncryptBL();
                strIdReferente = HttpUtility.HtmlDecode(oEncryptBL.EncryptKey(idReferente.ToString()));

                oReferenciaBL = new ReferenciaBL();
                string msg = string.Empty;
                msg = oReferenciaBL.ReenvioenviaEmailReferenciaAplicante(idReferente, strIdReferente, 0, false);
                if (string.IsNullOrEmpty(msg))
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('Se reenvío email a la referencia satisfactoriamente.');", true);
                    this.lblmessage.Text = "Se reenvío email a la referencia satisfactoriamente.";
                    this.mpeMostrarError.Show();
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + msg + "');", true);
                    this.lblmessage.Text = msg;
                    this.mpeMostrarError.Show();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EditarDatosReferente(Int32? idReferente)
        {
            try
            {
                this.ObtenerDatosReferente(idReferente);
                this.mpeMostrarEnviarCorreo.Show();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.upMostrarEnviarCorreoDetalle.Update();
            }
        }



        /*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/
        private void ValidarTipoDeEntrevista()
        {
            Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);
            trTipoEntrevista.Visible = false;

            if (ModalidadId == null) return;

            AplicanteBE oAplicanteBE = new AplicanteBE();
            AplicanteBL oAplicanteBL = new AplicanteBL();

            Session["ApplicationEducationId"] = oAplicanteBL.ValidarColegioEntrevista(UIConvertNull.Int32(Session["AplicanteId"])).ToString();
            Int32? ApplicationEducationId = UIConvertNull.Int32(Session["ApplicationEducationId"]);


            if (ApplicationEducationId == null || ApplicationEducationId.Value == 0)
            {
                Int32? AplicanteId = null;
                AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
                this.CargarComboHorarios(AplicanteId);
                return;
            }
                
            ddlTipoEntrevista.Items.Clear();
            ddlTipoEntrevista.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));

            if (ModalidadId == UIConstantes.Modalidad.Selectiva || ModalidadId == UIConstantes.Modalidad.BachilleratoMitadSuperior)
            {
                ddlTipoEntrevista.Items.Insert(1, new ListItem(UIConstantes.TipoEntrevista.EntrevEclVirtual, "EntrevEclVirtual"));
                ddlTipoEntrevista.Items.Insert(2, new ListItem(UIConstantes.TipoEntrevista.EntrevEclPresencial, "EntrevEclPrecencial"));
                trTipoEntrevista.Visible = true;
                return;
            }

            if (ModalidadId == UIConstantes.Modalidad.ExcelAcadem || ModalidadId == UIConstantes.Modalidad.BachilleratoQuintoSuperior)
            {
                ddlTipoEntrevista.Items.Insert(1, new ListItem(UIConstantes.TipoEntrevista.EntrevVirtual, "EntrevVirtual"));
                ddlTipoEntrevista.Items.Insert(2, new ListItem(UIConstantes.TipoEntrevista.EntrevPrecencial, "EntrevPrecencial"));
                trTipoEntrevista.Visible = true;
                return;
            }
        }

        private void ObtenerTipoEntrevista()
        {
            Int32? ApplicationEducationId = UIConvertNull.Int32(Session["ApplicationEducationId"]);
            if (ApplicationEducationId == null || ApplicationEducationId.Value == 0) return;

            EducacionBL oEducacionBL = new EducacionBL();
            string tipoEntrevista = oEducacionBL.ObtenerTipoEntrevista(ApplicationEducationId, UIConvertNull.Int32(Session["AplicanteId"]));
            Int32? AplicanteId = null;
            AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            if (!string.IsNullOrEmpty(tipoEntrevista))
            {
                ddlTipoEntrevista.SelectedValue = tipoEntrevista;                
                this.CargarComboHorarios(AplicanteId, tipoEntrevista);
            }
            else
            {
                this.CargarComboHorarios(AplicanteId);
            }

        }

        private void GuardarDatosEntrevista()
        {
            Int32? ApplicationEducationId = UIConvertNull.Int32(Session["ApplicationEducationId"]);
            if (ApplicationEducationId.Value > 0)
            {

                try
                {
                    AplicanteBE oAplicanteBE = new AplicanteBE();
                    EducacionBL oEducacionBL = new EducacionBL();

                    oAplicanteBE.IdAplicante = UIConvertNull.Int32(Session["AplicanteId"]);
                    oAplicanteBE.RedId = UIConvertNull.String(Session["usrRedId"]);
                    oEducacionBL.ModTipoEntrevista(oAplicanteBE, ApplicationEducationId, ddlTipoEntrevista.Text);

                }
                catch (Exception ex)
                {
                    Response.Redirect("../frmMsjeErrorUP.aspx?tipError=" +
                        UIConstantes.ObtenerTipoError()[UIConstantes.TIPO_ERROR.ERROR_INSERTAR_REGISTRO] +
                        "&descError=" +
                        ex.Message.Replace("\n", ""), false);
                }
            }
        }
        /*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/


        /*Ini:[Christian Ramirez - Caso78630]*/
        private void CargarYValidarHorarioEcl(Int32? AplicanteId)
        {
            if (AplicanteId != null)
            {
                AplicanteBL oAplicanteBL = new AplicanteBL();
                DataTable dt = oAplicanteBL.ObtenerHorarioEclRegistrado(AplicanteId.Value);

                if (dt != null && dt.Rows.Count > 0)
                {
                    CargarComboHorarioECL(dt);
                    ddlHorarioECL.SelectedValue = dt.Rows[0][0].ToString();
                    ddlHorarioECL.Enabled = false;
                    //imgBtnEnviarHorarioEcl.Visible = false;/*Se modifica[Christian Ramirez - GIIT - RF1161]*/ /*[Juan Delgado - Caso84943] 20201103*/
                    
                }
                else
                {
                    ValidarObtenerEclOnline(AplicanteId.Value);
                    //if (trHorarioECL.Attributes["Style"].Contains("contents")) imgBtnEnviarHorarioEcl.Visible = true; /*Se modifica[Christian Ramirez - GIIT - RF1161]*/
                    if (trHorarioECL.Attributes["Style"].Contains("contents")) imgBtnEnviarTodo.Visible = true; /*[Juan Delgado - Caso84943] 20201103*/
                }
            }
        }

        private void GuardarDatosEclOnline()
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            AplicanteBL oAplicanteBL = new AplicanteBL();

            if (AplicanteId != null)
            {
                string horaEcl = UIConvertNull.String(ddlHorarioECL.SelectedValue);
                Boolean operacionOK = oAplicanteBL.InsertaHorarioEcl(AplicanteId.Value, horaEcl);

                if (operacionOK)
                {
                    ddlHorarioECL.Enabled = false;                    
                    lblmessage.Text = "Se registro su(s) horario(s) con exito.";
                    mpeMostrarError.Show();
                }
            }
        }

        private void ValidarObtenerEclOnline(int aplicanteId)
        {
            //bool rpta = false;
            Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);
            AplicanteBL oAplicanteBL = new AplicanteBL();
            DataTable dt = null;

            if (ModalidadId != null)
            {
                dt = oAplicanteBL.ValidarObtenerEclOnline(ModalidadId.Value, aplicanteId);
                CargarComboHorarioECL(dt);
            }

            //return rpta;

        }

        private void CargarComboHorarioECL(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                Funciones.cargarComboYSeleccione(ddlHorarioECL, dt, "Descripcion", "Codigo", "-- Seleccionar --");
                trHorarioECL.Attributes.CssStyle.Value = "display: contents";
                //rpta = true;
            }
            else
            {
                ddlHorarioECL.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
                trHorarioECL.Attributes.CssStyle.Value = "display: none";
            }
        }
        /*Fin:[Christian Ramirez - Caso78630]*/

        /*Ini:[Juan Delgado - Caso81646] 20200928*/        
        private void CargarYValidarHorarioPC(Int32? AplicanteId)
        {
            if (AplicanteId != null)
            {
                AplicanteBL oAplicanteBL = new AplicanteBL();
                DataTable dt = oAplicanteBL.ObtenerHorarioPCRegistrado(AplicanteId.Value);

                if (dt != null && dt.Rows.Count > 0)
                {
                    CargarComboHorarioPC(dt);
                    ddlHorarioPC.SelectedValue = dt.Rows[0][0].ToString();
                    ddlHorarioPC.Enabled = false;
                    //imgBtnEnviarHorarioPC.Visible = false; /*[Juan Delgado - Caso84943] 20201103*/
                    
                }
                else
                {
                    ValidarObtenerPCOnline(AplicanteId.Value);
                    //if (trHorarioPC.Attributes["Style"].Contains("contents")) imgBtnEnviarHorarioPC.Visible = true;
                    if (trHorarioPC.Attributes["Style"].Contains("contents")) imgBtnEnviarTodo.Visible = true; /*[Juan Delgado - Caso84943] 20201103*/
                }
            }
        }

        private void CargarComboHorarioPC(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                Funciones.cargarComboYSeleccione(ddlHorarioPC, dt, "Descripcion", "Codigo", "-- Seleccionar --");
                trHorarioPC.Attributes.CssStyle.Value = "display: contents";
                //rpta = true;
            }
            else
            {
                ddlHorarioPC.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
                trHorarioPC.Attributes.CssStyle.Value = "display: none";
            }
        }

        private void ValidarObtenerPCOnline(int aplicanteId)
        {
            //bool rpta = false;
            Int32? ModalidadId = UIConvertNull.Int32(Session["ModPostulacion"]);
            AplicanteBL oAplicanteBL = new AplicanteBL();
            DataTable dt = null;

            if (ModalidadId != null)
            {
                dt = oAplicanteBL.ValidarObtenerPCOnline(ModalidadId.Value, aplicanteId);
                CargarComboHorarioPC(dt);
            }

            //return rpta;

        }

        private void GuardarDatosPCOnline()
        {
            Int32? AplicanteId = UIConvertNull.Int32(Session["AplicanteId"]);
            AplicanteBL oAplicanteBL = new AplicanteBL();

            if (AplicanteId != null)
            {
                string horaPC = UIConvertNull.String(ddlHorarioPC.SelectedValue);
                Boolean operacionOK = oAplicanteBL.InsertaHorarioPC(AplicanteId.Value, horaPC);

                if (operacionOK)
                {
                    ddlHorarioPC.Enabled = false;                    
                    lblmessage.Text = "Se registro su(s) horario(s) con exito.";
                    mpeMostrarError.Show();
                }
            }
        }

        /*Fin:[Juan Delgado - Caso81646] 20200928*/

        #endregion "Métodos Privados"


    }
}
