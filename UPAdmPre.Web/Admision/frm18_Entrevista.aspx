<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm18_Entrevista.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm18_Entrevista" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validate.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine-en.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery-ui.js" type="text/javascript"></script>
    <script src="../JavaScript/json2.js" type="text/javascript"></script>
    <script src="../JavaScript/JS.js" type="text/javascript"></script>
    <script src="../JavaScript/thickbox.js" type="text/javascript"></script>
    <style>
        .SubTitulo {
            border-bottom: 0px !important;
            border-top: 1px solid #337ab7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="scr_principal" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <table style="width: 100%;">
            <tr>
                <td class="Titulo CabeceraNoSeleccionada">
                    <asp:Label ID="Label2" runat="server" Text="Proceso de Inscripción" CssClass="TextoEtiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" class="tablaInterna">
                        <tr>
                            <td style="width: 10%;">
                                <asp:Label ID="Label1" runat="server" Text="Postulante:" CssClass="TextoEtiqueta"></asp:Label>
                            </td>
                            <td style="width: 40%;">
                                <asp:Label ID="lblPostulante" runat="server" CssClass="tdTextNormal"></asp:Label>
                            </td>
                            <td style="width: 18%;">
                                <asp:Label ID="Label4" runat="server" Text="Nro. Documento:" CssClass="TextoEtiqueta"></asp:Label>
                            </td>
                            <td style="width: 32%;">
                                <asp:Label ID="lblNroDocumento" runat="server" CssClass="tdTextNormal"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Modalidad:" CssClass="TextoEtiqueta"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblModalidad" runat="server" CssClass="tdTextNormal"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Carrera:" CssClass="TextoEtiqueta"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCarrera" runat="server" CssClass="tdTextNormal"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; font-size: 18px; padding:10px 2px;">
                    <label class="TextoEtiqueta">
                        Selecciona tus turnos y no olvides dar clic al botón rojo "Enviar Horarios" 
                        para guardar tus inscripciones.                           
                    </label>
                </td>

            </tr>
            <% if (Session["ModPostulacion"].ToString() != "42" && Session["ModPostulacion"].ToString() != "46" && Session["ModPostulacion"].ToString() != "52")
               { %>
            <%-- Ini:[Juan Delgado] --%>
            <tr>
                <td class="SubTitulo">
                    <label>Selección de horario para Pruebas de Conectividad</label>
                </td>
            </tr>
<%--            <tr>
                <td style="text-align: justify;">
                    <asp:Label ID="Label6" runat="server" CssClass=""></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr id="trHorarioPC" runat="server" style="display:none;">
                <td>
                    <table style="width: 100%;" class="tablaInterna">
                        <tr>
                            <td style="width: 15%;">
                                <label class="TextoEtiqueta">Selecciona un turno:
                                    <%--<br />&nbsp
                                    Selecciona un turno y no olvides dar clic en ENVIAR
                                    <br />&nbsp
                                    para que se guarde tu inscripción a dicha prueba.--%>
                                </label>
                            </td>
                            <td style="width: 20%;">
                                <asp:DropDownList ID="ddlHorarioPC" runat="server" CssClass="txtTextoCombo" style="width:16rem;" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%-- Fin:[Juan Delgado] --%>
            <tr>
                <td class="SubTitulo">
                    <%--Ini:[Christian Ramirez - Caso78630]--%>
                    <label>Selección de horario para la Evaluación de Comprensión Lectora y Competencia del Lenguaje</label>
                    <%--Fin:[Christian Ramirez - Caso78630]--%>
                </td>
            </tr>
            <tr>
                <td style="text-align: justify;">
                    <asp:Label ID="lblMensajeEnsayo" runat="server" CssClass="" style="display:none"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <%--Ini:[Christian Ramirez - Caso78630]--%>
            <tr id="trHorarioECL" runat="server" style="display:none;">
                <td>
                    <table style="width: 100%;" class="tablaInterna">
                        <tr>
                            <td style="width: 15%;">
                                <label class="TextoEtiqueta">Selecciona un turno:
                                    <%--<br />&nbsp
                                    Selecciona un turno y no olvides dar clic en ENVIAR
                                    <br />&nbsp
                                    para que se guarde tu inscripción a dicha evaluación.--%>
                                </label>
                            </td>
                            <%--Ini:[Christian Ramirez - RF1161]--%>
                            <td style="width: 20%;">
                                <asp:DropDownList ID="ddlHorarioECL" runat="server" CssClass="txtTextoCombo" style="width:16rem;" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td>
                                
                            </td>
                            <%--Fin:[Christian Ramirez - RF1161]--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--Fin:[Christian Ramirez - Caso78630]--%>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr id="trRef" runat="server" style="display: none">
                <td>
                    <table style="width: 100%;" class="tablaInterna">
                        <tr>
                            <td class="SubTitulo">
                                <asp:Label ID="Label7" runat="server" Text="Referencias"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 70%;" align="center">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upReferencias" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvReferencias" runat="server" AutoGenerateColumns="False" CssClass="Grilla" EmptyDataText="No se encontraron registros..." OnRowDataBound="gvReferencias_RowDataBound">
                                                        <HeaderStyle CssClass="HeaderGrilla" />
                                                        <AlternatingRowStyle CssClass="AlternateItemGrilla" />
                                                        <RowStyle CssClass="ItemGrilla" />
                                                        <PagerStyle CssClass="PagerGrilla" />
                                                        <Columns>
                                                            <asp:BoundField>
                                                                <ItemStyle Width="5%" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Estado">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="imgEstadoRespuesta" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Referentes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReference" runat="server" CssClass="ItemGrilla" Text="Label"></asp:Label>
                                                                    <asp:Label ID="lblErrorEnvioReferencia" runat="server" CssClass="MsgAlerta" Text="Label" Visible="false" />
                                                                    <asp:Label ID="lblEstado" runat="server" Text="Label" Visible="false" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enviar Email">
                                                                <EditItemTemplate>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgBtnEnviarEmail" runat="server" CausesValidation="false" CssClass="detalleCarga" Height="24px" ImageUrl="~/Images/icoEmail.png" Width="24px" OnClick="imgBtnEnviarEmail_Click" ToolTip="Enviar correo al referente." />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Editar">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgBtnEditar" runat="server" CausesValidation="false" CssClass="detalleCarga" Height="24px" ImageUrl="~/Images/icoEditar.png" Width="24px" OnClick="imgBtnEditarReferente_Click" ToolTip="Editar datos del referente." />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <input id="hIdReferente" name="hIdReferente" runat="server" type="hidden" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnEnviar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <% } %>

            <tr>
                <td>
                    <asp:Panel ID="pnlHorario" runat="server">
                        <table style="width: 100%;" class="tablaInterna">
                            <tr>
                                <td class="SubTitulo" colspan="5">
                                    <asp:Label ID="lblTituloSeleccion" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <%--/*Ini:Christian Ramirez - GIIT[caso60747] - 20190521*/--%>
                            <tr id="trTipoEntrevista" runat="server" visible="false">
                                <%--<td>
                                    <table style="padding-top: 10px;">
                                        <tr>
                                            <td style="padding-bottom: 10px;">
                                                <span style="color: red;">Escoge el tipo de entrevista:</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoEntrevista" runat="server" CssClass="txtTextoCombo" Width="200"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>--%>
                                <td style="padding-bottom: 10px;">
                                    <span class="TextoEtiqueta">Seleccione el tipo de evaluacion:</span>
                                </td>
                                <td style="padding-top: 18px;">
                                    <asp:DropDownList ID="ddlTipoEntrevista" runat="server" CssClass="txtTextoCombo" Width="200" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoEntrevista_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <%--/*Fin:Christian Ramirez - GIIT[caso60747] - 20190521*/--%>
                            <tr>
                                <td style="width: 15%;">
                                    <asp:Label ID="lblSubTituloHorario" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                    <%--<span class="TextoEtiqueta">
                                        <br />
                                        Selecciona un turno y no olvides dar clic en ENVIAR
                                        <br />
                                        para que se guarde tu inscripción a dicha entrevista.
                                    </span>--%>
                                </td>
                                <td style="width: 17%; padding-top: 18px;">
                                    <asp:DropDownList ID="ddlHorario" runat="server" CssClass="txtTextoCombo" style="width:16rem;"></asp:DropDownList>
                                </td>
                                <td style="width: 1%; padding-top: 18px;">
                                    <%--<asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />--%>
                                </td>
                                <td style="width: 1%;">
                                    <asp:RequiredFieldValidator ID="rfvHorario" runat="server" ControlToValidate="ddlHorario" CssClass="MsgAlertaIncompleto"
                                        ErrorMessage="Elija horario." InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraHorario"></asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: left;">                                    
                                    <asp:ImageButton ID="imgBtnEnviarTodo" runat="server" ImageUrl="~/Images/Buttons/BtnRojo_EnviarHorarios.png" OnClick="imgBtnEnviarTodo_Click" /> <%--ValidationGroup="registraHorario" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr style="text-align: justify;">
                <td>
                    <br />
                    <asp:Label ID="lblMensajeInformativoInf" runat="server" CssClass="tdTextNormal"></asp:Label>
                    <br />
                </td>
            </tr>
            <%--<tr>--%>
                <%--<td style="text-align: right;">--%>
                    <%--<asp:ImageButton ID="imgBtnEnviar" runat="server" ImageUrl="~/Images/Buttons/btnEnviarRojo_Entrevista.png" OnClick="imgBtnEnviar_Click" /> <%--ValidationGroup="registraHorario" />--%>
                <%--</td>--%>
            <%--</tr>--%>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnMostrarEnviarCorreo" Style="display: none;" runat="server" />
                    <asp:Button ID="btnCancelarEnviarCorreo" Style="display: none;" runat="server" />
                    <ajax:ModalPopupExtender ID="mpeMostrarEnviarCorreo" runat="server" TargetControlID="btnMostrarEnviarCorreo"
                        PopupControlID="pnlMostrarEnviarCorreo" PopupDragHandleControlID="pnlMostrarEnviarCorreo" BackgroundCssClass="modalBackground"
                        CancelControlID="btnCancelarEnviarCorreo" Enabled="True" />
                    <asp:Panel ID="pnlMostrarEnviarCorreo" runat="server" CssClass="popup-main" Width="80%" Style="display: none;">
                        <asp:UpdatePanel ID="upMostrarEnviarCorreoDetalle" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <table style="width: 100%; border: 0px;" class="tablaInterna" align="center">
                                    <tr style="height: 50px;">
                                        <td colspan="4" style="text-align: center; background-color: aliceblue;" class="Titulo">
                                            <b>Información de Referencia</b><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: central; width: 15%;" class="TextoEtiqueta">&nbsp;&nbsp;&nbsp;Nombres</td>
                                        <td style="width: 70%;">
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="txtCajaTexto"></asp:TextBox></td>
                                        <td style="width: 1%;">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                        </td>
                                        <td style="width: 1%;">
                                            <asp:RequiredFieldValidator ID="rfvNomColegio" runat="server" ControlToValidate="txtNombre" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre de referente." InitialValue="" Text="*" ValidationGroup="enviarCorreo"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: central;" class="TextoEtiqueta">&nbsp;&nbsp;&nbsp;Apellidos:</td>
                                        <td>
                                            <asp:TextBox ID="txtApellido" runat="server" CssClass="txtCajaTexto"></asp:TextBox></td>
                                        <td>
                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvNomColegio0" runat="server" ControlToValidate="txtApellido" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese apellido de referente." InitialValue="" Text="*" ValidationGroup="enviarCorreo"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: central;" class="TextoEtiqueta">&nbsp;&nbsp;&nbsp;Cargo:</td>
                                        <td>
                                            <asp:TextBox ID="txtCargo" runat="server" CssClass="txtCajaTexto"></asp:TextBox></td>
                                        <td>
                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvNomColegio1" runat="server" ControlToValidate="txtCargo" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese cargo del referente." InitialValue="" Text="*" ValidationGroup="enviarCorreo"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: central;" class="TextoEtiqueta">&nbsp;&nbsp;&nbsp;Institución:</td>
                                        <td>
                                            <asp:TextBox ID="txtInstitucion" runat="server" CssClass="txtCajaTexto"></asp:TextBox></td>
                                        <td>
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvNomColegio2" runat="server" ControlToValidate="txtInstitucion" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese institución del referente." InitialValue="" Text="*" ValidationGroup="enviarCorreo"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: central;" class="TextoEtiqueta">&nbsp;&nbsp;&nbsp;Email:</td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtCajaTexto"></asp:TextBox></td>
                                        <td>
                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvNomColegio3" runat="server" ControlToValidate="txtEmail" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese email del refrente." InitialValue="" Text="*" ValidationGroup="enviarCorreo"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="tr2" runat="server" visible="false">
                                        <td style="vertical-align: central;" class="TextoEtiqueta">&nbsp;&nbsp;&nbsp;Error Envio:</td>
                                        <td>
                                            <asp:CheckBox ID="cbxErrorEnvio" runat="server" CssClass="txtCajaTexto" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr id="tr1" runat="server" visible="false" style="height: 20px;">
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;" colspan="4">
                                            <asp:Button ID="btnEnviar" runat="server" CssClass="btnEnviaCorreo" Text="Enviar Correo" OnClick="btnEnviar_Click" Width="145px" ValidationGroup="enviarCorreo" />
                                            <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="btnCerrar" OnClick="btnCerrar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnMostrarError" Style="display: none;" runat="server" />
                    <asp:Button ID="btnCancelarError" Style="display: none;" runat="server" />
                    <ajax:ModalPopupExtender ID="mpeMostrarError" runat="server" TargetControlID="btnMostrarError"
                        PopupControlID="pnlMostrarError" PopupDragHandleControlID="pnlMostrarError" BackgroundCssClass="modalBackground"
                        CancelControlID="btnCancelarError" Enabled="True" />
                    <asp:Panel ID="pnlMostrarError" runat="server" CssClass="popup-content" Width="50%" Style="display: block;">
                        <asp:UpdatePanel ID="upMostrarErrorDetalle" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div class="popup-main">
                                    <div class="popup-header">
                                        <h4>¡ Alerta !</h4>
                                    </div>
                                    <div class="popup-content">
                                        <p>
                                            <asp:Label runat="server" ID="lblmessage" CssClass="tdTextNormalAlerta"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="popup-footer">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnSalir_Click">Cerrar</asp:LinkButton>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:Panel ID="pnlPopup" runat="server" CssClass="progress" Style="position: absolute; top: 0px; left: 0px; width: 250px;">
                                <div>
                                    <div class="body" style="text-align: center; font-size: 12px;">
                                        <img src="../Images/icoProgress.gif" alt="" height="64" width="64" /><br />
                                        Procesando, por favor espere...
                                    </div>
                                </div>
                            </asp:Panel>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <ajax:ModalPopupExtender ID="ModalPopupExtender1" DropShadow="true" TargetControlID="main"
                        runat="server" BackgroundCssClass="modalBackground" PopupControlID="UpdateProgress1" />
                    <script type="text/javascript">
                        try {
                            Sys.Application.add_load(ApplicationLoadHandler);
                        } catch (e) { ; }

                        function ApplicationLoadHandler(sender, args) {
                            try {
                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                if (!prm.get_isInAsyncPostBack()) {
                                    prm.add_initializeRequest(initRequest);
                                    prm.add_endRequest(endRequest);
                                }
                            } catch (e) { ; }
                        }

                        function initRequest(sender, args) {
                            try {
                                var pop = $find("ModalPopupExtender1");
                                pop.show();

                            } catch (e) { ; }
                        }

                        function endRequest(sender, args) {
                            try {
                                var pop = $find("ModalPopupExtender1");
                                pop.hide();
                            } catch (e) { ; }
                        }
                    </script>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
