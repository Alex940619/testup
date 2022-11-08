<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm01_VideoIntro.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm01_VideoIntro" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="scr_principal" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <table style="width: 100%; margin: auto;">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="CabeceraSeleccionada" style="width: 33%;">
                                <asp:Label ID="lblPantallaAnterior" runat="server"></asp:Label>
                            </td>
                            <td class="CabeceraNoSeleccionada" style="width: 33%;">
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
                                <td class="title-box">
                                    <%--<video src="../Video/Wildlife.wmv" controls="controls" autoplay="autoplay" preload="auto" poster="../Images/cancelar.png"></video>--%><%--<section></section>--%>
                                    <asp:Label class="adm_title pull-left" ID="lblTitulo" runat="server" Text="Video Instructivo"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 15px;">
                                    <asp:Label ID="lblNota" runat="server" CssClass="tdTextoTitulo" Text="Nota: "></asp:Label>
                                    <asp:Label ID="lblNotaDescripcion" runat="server" CssClass="tdTextoDetalle" Text="Se recomienda ver el video, para aclarar alguna duda que pueda tener durante el proceso de inscripción."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <br />
                                    <video controls="controls" width="560" height="315">
                                        <source src="../Video/ColdPlay-InMyPlace.mp4" type="video/mp4" />
                                    </video>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="controles tablaFrame" style="width: 100%;">
                                        <tr>
                                            <td style="width: 219px;"></td>
                                            <td style="width: 281px;"></td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" OnClick="imgBtnNext_Click" />
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
