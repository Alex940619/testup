<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm20_FormalizaIng_backup.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm20_FormalizaIng_backup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
    <style type="text/css">
        .auto-style1 {
            width: 151px;
            height: 26px;
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
                    <asp:Label ID="Label2" runat="server" Text="Formalización de Ingreso" CssClass="TextoEtiqueta"></asp:Label>
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
                <td>
                    <asp:Panel ID="pnlHorario" runat="server">
                        <table style="width: 100%;" class="tablaInterna">
                            <tr>
                                <td class="SubTitulo" colspan="4">
                                    <asp:Label ID="lblTituloSeleccion" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tablaFrameDesc" colspan="4">
                                    <div class="box-icon">
                                        <i class="fa fa-check"></i>
                                    </div>
                                    <div class="box-content"><br />
                                        <asp:Label ID="lblMsg" runat="server" CssClass="tdTextNormal" Text="Recuerda que para llevar a cabo el proceso de formalización, debes entregar la documentación solicitada (revisar <a href='https://campusvirtual.up.edu.pe/pdf/up-prospecto-de-admision-2017.pdf' target='_blank'>Prospecto de Admisión 2017</a>, páginas 32 y 33), y cumplir los requisitos académicos correspondientes, según a la modalidad en la que fuiste seleccionado (a)."></asp:Label>
                                    </div>
                                </td>

                            </tr>

                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblSubTituloHorario" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                </td>

                                <td style="width: 73%; padding-top: 18px;">
                                    <asp:DropDownList ID="ddlHorario" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                </td>
                                <td style="width: 1%; padding-top: 18px;">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td style="width: 1%;">
                                    <asp:RequiredFieldValidator ID="rfvHorario" runat="server" ControlToValidate="ddlHorario" CssClass="MsgAlertaIncompleto"
                                        ErrorMessage="Elija horario." InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraHorario"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <style>
                                @keyframes animate-btn {
                                    0%{
                                        transform:scale(1)
                                    }
                                    20%{
                                        transform:scale(1.1)
                                    }
                                    80%{
                                        transform:scale(1.1)
                                    }
                                    100%{
                                        transform:scale(1)
                                    }
                                }
                            </style>
                            <tr>
                                <td colspan="3" class="conoce-escala-form" style="text-align: center; background: rgba(27,130,182,.2); box-shadow:inset 0px 0px 2px #1b82b6; padding: 10px; border-radius: 10px;">
                                <span id="hlconoce" runat="server"></span>

                                </td>
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
            <tr>
                <td style="text-align: right;">
                    <asp:ImageButton ID="imgBtnEnviar" runat="server" ImageUrl="~/Images/Buttons/btnInscripcion.png" OnClick="imgBtnEnviar_Click" ValidationGroup="registraHorario" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
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
                        } catch (e) {; }

                        function ApplicationLoadHandler(sender, args) {
                            try {
                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                if (!prm.get_isInAsyncPostBack()) {
                                    prm.add_initializeRequest(initRequest);
                                    prm.add_endRequest(endRequest);
                                }
                            } catch (e) {; }
                        }

                        function initRequest(sender, args) {
                            try {
                                var pop = $find("ModalPopupExtender1");
                                pop.show();

                            } catch (e) {; }
                        }

                        function endRequest(sender, args) {
                            try {
                                var pop = $find("ModalPopupExtender1");
                                pop.hide();
                            } catch (e) {; }
                        }
                    </script>
                </td>
            </tr>
        </table>


    </form>
</body>
</html>
