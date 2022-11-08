<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm15_BoletaPagos.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm15_BoletaPagos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <style type="text/css">
        .auto-style1 {
            width: 66px;
            height: 38px;
        }

        .auto-style2 {
            color: #000099;
        }
    </style>
</head>
<body>
    <div id="contentStepsAdmision"></div>
    <form id="form1" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="scr_principal" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <table style="width: 100%; margin: auto; table-layout: fixed;">
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
                <td class="SubTitulo">
                     <table style="width: 100%;" class="tablaInterna">
                        <tr>
                            <td style="width: 70%;">
                                <%--Ini: Christian Ramirez - Caso45554 - 20180530--%>
                                <%--<asp:Label ID="lblTitulo" runat="server" Text="Pago Derecho de Admisión"></asp:Label>--%>
                                <asp:Label ID="lblTitulo" runat="server" Text="Pago Derecho de Inscripción"></asp:Label>
                                <%--Fin: Christian Ramirez - Caso45554 - 20180530--%>
                            </td>
                            <td style="width: 20%;">
                                <asp:Label ID="lblPasos" runat="server"></asp:Label>
                            </td>
                            <td style="width: 10%; text-align: right;">
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="tr1" runat="server" visible="false">
                <td style="height: 25px; font-family: Arial; font-size: 13px; color: Red; font-weight: bold">
                    <asp:Label ID="lblMensajePago1" runat="server" Text="Por favor, póngase en contacto con la Oficina de Admisión (admision@up.edu.pe) para la generación de su código de pago."></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 20%; font-family: Arial; font-size: 13px; font-weight: bold">
                                <asp:Label ID="lblPostulanteLabel" runat="server">Nombres y Apellidos:</asp:Label></td>
                            <td style="width: 80%; font-family: Arial; font-size: 13px">
                                <asp:Label ID="lblnompostulante" runat="server"></asp:Label></td>
                        </tr>
                        <tr style="height: 25px">
                            <td style="width: 20%; font-family: Arial; font-size: 13px; font-weight: bold">Código de pago:</td>
                            <td style="width: 80%; font-family: Arial; font-size: 13px">
                                <asp:Label ID="lbldocumento" runat="server"></asp:Label></td>
                        </tr>
                        <tr style="height: 25px">
                            <td style="width: 20%; font-family: Arial; font-size: 13px; font-weight: bold">Fecha Vencimiento:</td>
                            <td style="width: 80%; font-family: Arial; font-size: 13px">
                                <asp:Label ID="lblfecvec" runat="server"></asp:Label></td>
                        </tr>
                        <tr style="height: 25px">
                            <td style="width: 20%; font-family: Arial; font-size: 13px; font-weight: bold">Importe:</td>
                            <td style="width: 80%; font-family: Arial; font-size: 13px">
                                <asp:Label ID="lblimporte" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td style="height: 40px">
                    <div style="width: 100%; font-family: Arial; color: Orange; font-weight: bold">PAGO <i>ONLINE</i></div>
                </td>
            </tr>
            <tr>
                <td style="text-align: justify; font-family: Arial; font-size: 13px">Usted puede hacer el pago <i>online</i> del derecho de inscripción con tarjeta de débito o crédito Visa, Mastercard o American Express. Para realizar el pago deberá hacer clic en la imagen “<b>Realizar Pago</b>” (por motivos de seguridad se volverá a solicitar su usuario y contraseña). Una vez realizado el pago, vuelva a esta página y haga clic en la imagen “<b>Inicio</b>”.<br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <table style="width: 100%; align-content: center">
                        <tr>
                            <td style="width: 50%; height: 60px; text-align:center;">
                                <asp:ImageButton ID="btnPagoOnline" runat="server"
                                    ImageUrl="~/Images/PagoOnLine.png" OnClick="btnPagoOnline_Click" Height="40px" Width="180px" /></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; height: 55px; text-align:center">
                                <img src="../Images/LogoTarjetas.png" />
                                <img class="auto-style1" src="../Images/LogoDinersClub.jpg" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: left; text-align: justify; font-family: Arial; font-size: 13px">En caso tenga problemas al abrir el enlace, copiar el siguiente texto y pegar en su navegador: <span class="auto-style2"><strong>pagosvirtuales.up.edu.pe</strong></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 40px">
                    <div style="width: 100%; font-family: Arial; color: Orange; font-weight: bold">PAGO PRESENCIAL</div>
                </td>
            </tr>
            <tr>
                <td style="text-align: justify; font-family: Arial; font-size: 13px">Usted puede realizar el pago de manera presencial en cualquiera de los cuatro bancos indicados*. Para ello deberá acercarse al banco e indicar el siguiente <b>código de pago:
                        <asp:Label ID="lblCodigoPago" runat="server"></asp:Label></b>.<br />
                    <br />
                    Tome en cuenta que usted recién podrá realizar el pago en el banco al siguiente día útil de la emisión del código de pago. *Algunos bancos pueden aplicar un recargo adicional por concepto de comisión por Atención en Ventanilla.<br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <table style="text-align: center; width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <img alt="" src="../Images/LogoBancos.png" />
                            </td>
                            <td style="width: 50%;">
                                <asp:ImageButton ID="btnImpPago" runat="server" Visible="false"
                                    ImageUrl="~/Images/ImprimirCuponPago.png" OnClick="btnImpPago_Click" />
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
                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" ValidationGroup="registraPadres" />
                            </td>
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
      <script type="text/javascript">
        var UrlAcion = {
            UrlGetStepsAdmision: 'frm15_BoletaPagos.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
</body>
</html>
