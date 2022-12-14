<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm13_OtrosEstudios.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm13_OtrosEstudios" Culture="es-PE" %>

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
                                    <table style="width: 100%;" class="tablaInterna">
                                        <tr>
                                            <td style="width: 70%;">
                                                <asp:Label ID="lblTitulo1" runat="server" Text="Otros Estudios 1"></asp:Label>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:Label ID="lblPasos" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 10%; text-align: right;">
                                                <asp:ImageButton ID="imgBtnVideo" runat="server" ImageUrl="~/Images/Buttons/btnVideo.png" OnClick="imgBtnVideo_Click" ToolTip="Ver video" Visible="False" Height="32px" Width="32px" />
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
                                                <table style="width: 100%;" class="tablaFrame">
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 18%;">
                                                            <asp:Label ID="lblCurso1" runat="server" Text="Curso o Programa:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="padding-top: 10px;">
                                                            <asp:TextBox ID="txtCursoPrograma1" runat="server" CssClass="txtCajaTexto" MaxLength="45" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 2%;" class="requerido"></td>
                                                        <td style="width: 2%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblInstitucion1" runat="server" Text="Institución:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNomInstitucion1" runat="server" CssClass="txtCajaTexto" MaxLength="45" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblAnioDesde1" runat="server" CssClass="TextoEtiqueta" Text="Año Desde:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table class="tablaInterna">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAnioDesde1" runat="server" CssClass="txtCajaTexto" MaxLength="4" Width="120px" Style="text-align: center"></asp:TextBox>
                                                                        <ajax:MaskedEditExtender ID="meeAnhoDesde1" runat="server" CultureName="es-PE" Mask="9999" MaskType="Number" TargetControlID="txtAnioDesde1"></ajax:MaskedEditExtender>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td style="width: 80px;">
                                                                        <asp:Label ID="lblAnioHasta1" runat="server" CssClass="TextoEtiqueta" Text="Hasta:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAnioHasta1" runat="server" CssClass="txtCajaTexto" MaxLength="4" Width="120px" Style="text-align: center"></asp:TextBox>
                                                                        <ajax:MaskedEditExtender ID="meeAnioHasta1" runat="server" CultureName="es-PE" Mask="9999" MaskType="Number" TargetControlID="txtAnioHasta1"></ajax:MaskedEditExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <div style="display: none;">
                                                                <asp:TextBox ID="txtIdEducacion1" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdEducacion2" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdEducacion3" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdDetalleEducacion1" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdDetalleEducacion2" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdDetalleEducacion3" runat="server" Width="100px"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Button ID="btnQuitarEstudios2" runat="server" Text="Quitar otros estudios" CssClass="btnQuitar" OnClick="btnQuitarEstudios2_Click" ToolTip="Quitar otros estudios" />
                                                <asp:Button ID="btnAgregaEstudios2" runat="server" Text="Agregar otros estudios" CssClass="btnAgregar" OnClick="btnAgregaEstudios2_Click" ToolTip="Agregar otros estudios" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlEstudios2" runat="server">
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td colspan="5" class="SubTitulo">
                                                                <asp:Label ID="lblTitulo2" runat="server" Text="Otros Estudios 2"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 18%;">
                                                                <asp:Label ID="lblCurso2" runat="server" CssClass="TextoEtiqueta" Text="Curso o Programa:"></asp:Label>
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtCursoPrograma2" runat="server" CssClass="txtCajaTexto" MaxLength="45" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 2%;" class="requerido"></td>
                                                            <td style="width: 2%;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblInstitucion2" runat="server" Text="Institución:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNomInstitucion2" runat="server" CssClass="txtCajaTexto" MaxLength="45" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblAnioDesde2" runat="server" CssClass="TextoEtiqueta" Text="Año Desde:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table class="tablaInterna">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAnioDesde2" runat="server" CssClass="txtCajaTexto" MaxLength="4" Width="120px" Style="text-align: center"></asp:TextBox>
                                                                            <ajax:MaskedEditExtender ID="meeAnhoDesde2" runat="server" CultureName="es-PE" Mask="9999" MaskType="Number" TargetControlID="txtAnioDesde2"></ajax:MaskedEditExtender>
                                                                        </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td style="width: 80px;">
                                                                            <asp:Label ID="lblAnioHasta2" runat="server" CssClass="TextoEtiqueta" Text="Hasta:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAnioHasta2" runat="server" CssClass="txtCajaTexto" MaxLength="4" Width="120px" Style="text-align: center"></asp:TextBox>
                                                                            <ajax:MaskedEditExtender ID="meeAnioHasta2" runat="server" CultureName="es-PE" Mask="9999" MaskType="Number" TargetControlID="txtAnioHasta2"></ajax:MaskedEditExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Button ID="btnQuitarEstudios3" runat="server" Text="Quitar otros estudios" CssClass="btnQuitar" OnClick="btnQuitarEstudios3_Click" ToolTip="Quitar otros estudios" />
                                                <asp:Button ID="btnAgregaEstudios3" runat="server" Text="Agregar otros estudios" CssClass="btnAgregar" OnClick="btnAgregaEstudios3_Click" ToolTip="Agregar otros estudios" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlEstudios3" runat="server">
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td colspan="5" class="SubTitulo">
                                                                <asp:Label ID="lblTitulo3" runat="server" Text="Otros Estudios 3"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 18%;">
                                                                <asp:Label ID="lblCurso3" runat="server" CssClass="TextoEtiqueta" Text="Curso o Programa:"></asp:Label>
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtCursoPrograma3" runat="server" CssClass="txtCajaTexto" MaxLength="45" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 2%;" class="requerido"></td>
                                                            <td style="width: 2%;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblInstitucion3" runat="server" Text="Institución:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNomInstitucion3" runat="server" CssClass="txtCajaTexto" MaxLength="45" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblAnioDesde3" runat="server" CssClass="TextoEtiqueta" Text="Año Desde:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table class="tablaInterna">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAnioDesde3" runat="server" CssClass="txtCajaTexto" MaxLength="4" Width="120px" Style="text-align: center"></asp:TextBox>
                                                                            <ajax:MaskedEditExtender ID="meeAnhoDesde3" runat="server" CultureName="es-PE" Mask="9999" MaskType="Number" TargetControlID="txtAnioDesde3"></ajax:MaskedEditExtender>
                                                                        </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td style="width: 80px;">
                                                                            <asp:Label ID="lblAnioHasta3" runat="server" CssClass="TextoEtiqueta" Text="Hasta:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAnioHasta3" runat="server" CssClass="txtCajaTexto" MaxLength="4" Width="120px" Style="text-align: center"></asp:TextBox>
                                                                            <ajax:MaskedEditExtender ID="meeAnioHasta3" runat="server" CultureName="es-PE" Mask="9999" MaskType="Number" TargetControlID="txtAnioHasta3"></ajax:MaskedEditExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label4" runat="server" CssClass="tdTextNormal" Text="("></asp:Label>
                                    <asp:Image ID="Image18" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                    <asp:Label ID="Label5" runat="server" CssClass="tdTextNormal" Text=")  "></asp:Label>
                                    <asp:Label ID="Label3" runat="server" CssClass="tdTextNormal" Text="Campos Obligatorios"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="controles tablaInterna" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/Images/Buttons/btnPrev.png" ToolTip="Página anterior" OnClick="imgBtnBack_Click" />
                                            </td>
                                            <td></td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" ValidationGroup="registraOtrosEstudios" />
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
     <script type="text/javascript">
        var UrlAcion = {
            UrlGetStepsAdmision: 'frm13_OtrosEstudios.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
</body>
</html>
