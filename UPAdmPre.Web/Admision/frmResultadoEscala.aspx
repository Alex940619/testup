<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmResultadoEscala.aspx.cs" Inherits="UPAdmPre.Web.Admision.frmResultadoEscala" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="tksmDocumentos" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <table style="width: 100%; margin: auto;">
            <tr>
                <td>
                    <table style="width: 100%;" class="tablaFrame">
                        <tr>
                            <td style="text-align: center;">
                                <h2>
                                    <asp:Label ID="LblTitulo" runat="server" CssClass="letra_azul"></asp:Label>
                                </h2>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 20%">
                                            <asp:Label ID="LblTituloCodigo" runat="server" CssClass="letra_negra negrita" Text="DNI:"></asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            <asp:Label ID="LblCodigo" runat="server" CssClass="letra_negra"></asp:Label>
                                        </td>
                                        <td rowspan="3" style="width: 30%; text-align: right;">
                                            <asp:Image ID="ImgFoto" runat="server" Width="100px" Height="125px" ImageUrl="~/Images/discapacidad.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblTituloNombre" runat="server" CssClass="letra_negra negrita" Text="Nombre:"></asp:Label>
                                        </td>
                                        <td colspan="2" style="text-align: justify">
                                            <asp:Label ID="LblNombre" runat="server" CssClass="letra_negra"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; height: 30px;" colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="font-weight: bold; font-size: 16px; text-align: center;">
                                            <asp:Label ID="LblSaludo" runat="server" CssClass="letra_negra"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="font-weight: bold; font-size: 16px; text-align: center;">
                                            <asp:Label ID="LblResultado" runat="server" CssClass="letra_azul negrita"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: justify">
                                            <br />
                                            <asp:Label ID="LblParrafo" runat="server" CssClass="letra_negra"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center; font-size: 8">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Image ID="imgQR" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
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
