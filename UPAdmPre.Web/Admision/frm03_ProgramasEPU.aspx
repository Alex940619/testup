<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm03_ProgramasEPU.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm03_ProgramasEPU" Culture="es-PE" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/font-awesome.min.css" rel="stylesheet" />
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validate.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine-en.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery-ui.js" type="text/javascript"></script>
    <script src="../JavaScript/json2.js" type="text/javascript"></script>
    <script src="../JavaScript/JS.js" type="text/javascript"></script>
    <script src="../JavaScript/thickbox.js" type="text/javascript"></script>
    <link href="../Styles/css/paginador.css" rel="stylesheet" />
    <script type="text/javascript" src="../Styles/js/tooltip.js"></script>

    <script>
        //Validando que tenga seleccionado por lo menos un curso
        function ValidateCheckBoxList(sender, args) {
            var isValid = false;
            $('input[type="checkbox"]:checked').each(function () {
                isValid = true;
            });

            if (!isValid) {
                $("#cvCursos").css("visibility", "visible")
            } else {
                $("#cvCursos").css("visibility", "hidden")
            }
            return isValid;
        }

        //Capturando los ID de los check seleccionados
        var fncCheckCursos = function () {
            var cursosHorarios = "";
            $('input[type="checkbox"]:checked').each(function () {
                cursosHorarios += $(this).val() + ";";
            });

            //eliminamos el últim punto y coma.
            cursosHorarios = cursosHorarios.substring(0, cursosHorarios.length - 1);
            $("#hIdCursos").val(cursosHorarios);
        }

        //LLamando a las validaciones al momento de hacer Click
        $(function () {
            $("#imgBtnNext").bind("click", fncCheckCursos);
            $("#imgBtnNext").bind("click", ValidateCheckBoxList);
        });
    </script>
</head>
<body>
    <div id="contentStepsAdmision"></div>
    <form id="frm03_ProgramasEPU" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="scr_principal" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <table style="width: 100%; margin: auto;">
            <tr>
                <td>
                    <table style="width: 100%;visibility: hidden">
                        <tr>
                            <td class="CabeceraNoSeleccionada" style="width: 33%;">
                                <asp:Label ID="lblPantallaAnterior" runat="server"></asp:Label>
                            </td>
                            <td class="CabeceraSeleccionada" style="width: 33%;">
                                <asp:Label ID="lblPantallaVigente" runat="server"></asp:Label>
                            </td>
                            <td class="CabeceraNoSeleccionada" style="width: 33%;">
                                <asp:Label ID="lblPantallaSiguiente" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="SubTitulo">
                                <table style="width: 100%;" class="tablaInterna">
                                    <tr>
                                        <td style="width: 70%;">
                                            <asp:Label ID="lblTitulo" runat="server" Text="CURSOS ESPECIALIZADOS DE LA ESCUELA PREUNIVERSITARIA"></asp:Label>
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:Label ID="lblPasos" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;" class="tablaInterna">
                                                <tr>
                                                    <td style="width: 3%; height: 30px;"></td>
                                                    <td style="width: 30%;">
                                                        <asp:Label ID="lblProgramasPostulacion" runat="server" CssClass="tdTextoTitulo" Text="Cursos Especializados"></asp:Label>
                                                    </td>
                                                    <td style="width: 2%;"></td>
                                                    <td style="width: 3%;"></td>
                                                    <td style="width: 60%;">
                                                        <asp:Label ID="lblCursosTitulo" runat="server" CssClass="tdTextoTitulo" Text="Cursos y Horarios"></asp:Label>
                                                    </td>
                                                    <td style="width: 2%;"></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="text-align: left; vertical-align: middle; width: 40%;">
                                                        <asp:RadioButtonList ID="rblProgramas" runat="server" CssClass="radioButtonList" OnSelectedIndexChanged="rblProgramas_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButtonList>
                                                        <asp:Label ID="lblMensajeProgramas" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:RequiredFieldValidator ID="rfvProgramas" runat="server" ControlToValidate="rblProgramas"
                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Selecciones programa de su interés" InitialValue=""
                                                            SetFocusOnError="true" ValidationGroup="registraProgramas" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="text-align: left"></td>
                                                    <td style="text-align: left; vertical-align: middle; width: 70%;">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:CheckBoxList ID="chkListCursos" runat="server" CssClass="radioButtonList"></asp:CheckBoxList>
                                                                <br />
                                                                <asp:Label ID="lblMensajeCursos" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="rblProgramas" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:CustomValidator ID="cvCursos" runat="server" ErrorMessage="Seleccione curso de su interés." SetFocusOnError="true"
                                                            CssClass="MsgAlertaIncompleto2" ClientValidationFunction="ValidateCheckBoxList"></asp:CustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="text-align: left; vertical-align: middle;" colspan="4">
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblDescripcionPrograma" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="chkListCursos" EventName="SelectedIndexChanged" />
                                                                <asp:AsyncPostBackTrigger ControlID="rblProgramas" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 1px;"></td>
                                                    <td>
                                                        <input id="hIdCursos" type="hidden" name="hIdCursos" runat="server" />
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="text-align: center; width: 5%; vertical-align: top">
                                            <asp:ImageButton ID="imgBtnVideo" runat="server" ImageUrl="~/Images/Buttons/btnVideo.png" OnClick="imgBtnVideo_Click" ToolTip="Ver video" Visible="False" Height="32px" Width="32px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;" class="controles tablaFrame">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/Images/Buttons/btnPrev.png" ToolTip="Página anterior" OnClick="imgBtnBack_Click" />
                                        </td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" ValidationGroup="registraProgramas" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnMostrarError" Style="display: none;" runat="server" />
                    <asp:Button ID="btnCancelarError" Style="display: none;" runat="server" />
                    <ajax:ModalPopupExtender ID="mpeMostrarError" runat="server" TargetControlID="btnMostrarError"
                        PopupControlID="pnlMostrarError" PopupDragHandleControlID="pnlMostrarError" BackgroundCssClass="modalBackground"
                        CancelControlID="btnCancelarError" Enabled="True" />
                    <asp:Panel ID="pnlMostrarError" runat="server" CssClass="popup-content" Width="50%" Style="display: none;">
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
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgBtnNext" EventName="Click" />
                            </Triggers>
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
    <script type="text/javascript">
        var UrlAcion = {
            UrlGetStepsAdmision: 'frm03_ProgramasEPU.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
</body>
</html>
