<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm07_ActividadExtracurricular.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm07_ActividadExtracurricular" Culture="es-PE" %>

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
    <script type="text/javascript">
        function validaFecha(x, y) {
            var FechaDesde = document.getElementById(x);
            var FechaHasta = document.getElementById(y);

            if (FechaDesde.value > FechaHasta.value) {
                if (FechaHasta.value != 0) {
                    $("#lblmessage").html("El año de inicio no puede ser menor que el año de fin.")
                    $find("mpeMostrarError").show();
                    FechaDesde.value = 0;
                    FechaHasta.value = 0;
                    return false;
                }
            }
        }
    </script>
</head>
<body>
    <div id="contentStepsAdmision"></div>
    <form id="form1" runat="server">
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
                    <div class="adm-box">
                        <table style="width: 100%;" class="tablaFrame">
                            <tr>
                                <td class="SubTitulo">
                                    <table style="width: 100%" class="tablaInterna">
                                        <tr>
                                            <td style="width: 70%;">
                                                <asp:Label ID="lblTitulo" runat="server" Text="Actividades Extracurriculares"></asp:Label>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:Label ID="lblPasos" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 10%;text-align: right;">
                                                <asp:ImageButton ID="imgBtnVideo" runat="server" ImageUrl="~/Images/Buttons/btnVideo.png" OnClick="imgBtnVideo_Click" ToolTip="Ver video" Visible="False" Height="32px" Width="32px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 18px;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="updActividades" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gvActividades" runat="server" AutoGenerateColumns="False" CssClass="Grilla" EmptyDataText="Si has realizado alguna actividad extracurricular, haz clic en el botón “+ Agregar Actividad”" OnRowDataBound="gvActividades_RowDataBound" Width="100%" ShowHeaderWhenEmpty="True">
                                                            <HeaderStyle CssClass="HeaderGrilla" />
                                                            <AlternatingRowStyle CssClass="AlternateItemGrilla" />
                                                            <RowStyle CssClass="ItemGrilla" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Nro">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Actividad">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdApplicationActivity" runat="server" Style="display: none;" />
                                                                        <asp:Label ID="lblIdActividad" runat="server" Style="display: none;" />
                                                                        <asp:DropDownList ID="ddlActividad" runat="server" CssClass="txtTextoCombo" Width="125px"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Detalle de Actividad">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtNomActividad" runat="server" CssClass="txtCajaTexto" Width="90%"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Desde">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlAnioDesde" runat="server" CssClass="txtTextoCombo" Width="125px"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hasta">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlAnioHasta" runat="server" CssClass="txtTextoCombo" Width="125px"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Promovido por el Colegio">
                                                                    <ItemTemplate>
                                                                        <asp:RadioButtonList ID="rblPromovidoPorCole" runat="server" CssClass="radioButtonList" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="1" Selected="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnEliminar" runat="server" ImageUrl="~/Images/ico_Delete.png" OnClick="imgBtnEliminar_Click" ToolTip="Eliminar actividad" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <input id="hIdActividad" type="hidden" name="hIdActividad" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar Actividad" CssClass="btnAgregar" ToolTip="Agregar Actividad Extracurricular" OnClick="btnAgregar_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="controles" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/Images/Buttons/btnPrev.png" ToolTip="Página anterior" OnClick="imgBtnBack_Click" />
                                            </td>
                                            <td></td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" ValidationGroup="RegistraActExtracurricular" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
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
            UrlGetStepsAdmision: 'frm07_ActividadExtracurricular.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
</body>
</html>
