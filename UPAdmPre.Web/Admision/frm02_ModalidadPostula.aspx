<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm02_ModalidadPostula.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm02_ModalidadPostula" Culture="es-PE" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/font-awesome.min.css" rel="stylesheet" />
    <link href="../Styles/css/paginador.css" rel="stylesheet" />
    <script type="text/javascript" src="../Styles/js/tooltip.js"></script>
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery-ui.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validate.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine-en.js" type="text/javascript"></script>
    <script src="../JavaScript/json2.js" type="text/javascript"></script>
    <script src="../JavaScript/JS.js" type="text/javascript"></script>
    <script src="../JavaScript/thickbox.js" type="text/javascript"></script>


    <style type="text/css">
        .auto-style4 {
            height: 110px;
        }
        .auto-style5 {
            width: 50%;
            height: 110px;
        }
        </style>


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
                    <table style="width: 100%; visibility: hidden">
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
                        <table style="width: 100%;">
                            <tr style="display: none">
                                <td class="SubTitulo">
                                    <table style="width: 100%;" class="tablaInterna">
                                        <tr>
                                            <td style="width: 70%;">
                                                <asp:Label ID="lblTitulo" runat="server" Text="Modalidad de Postulación"></asp:Label>
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
                                                        <td style="width: 3%; height: 30px;"></td>
                                                        <td class="SubTitulo" style="width: 50%; padding-top: 15px;">                                                             
                                                            <asp:Label ID="lblModalidadPostulacion" runat="server" Text="Modalidades de admisión"></asp:Label>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 3%;"></td>
                                                        <td class="SubTitulo" style="width: 40%; padding-top: 15px;">
                                                            <asp:Label ID="lblCarreraTitulo" runat="server" Text="Carreras"></asp:Label>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style4"></td>
                                                        <td style="text-align: left; vertical-align: middle; " class="auto-style5"> <asp:Label ID="lbltextPeriodo" runat="server" Text="Selecciona el periodo al cual vas a postular:" CssClass="TextoEtiqueta"></asp:Label>
                                                             <asp:RadioButtonList ID="rblAnioAca" runat="server" CssClass="radioButtonList" OnSelectedIndexChanged="rblAnioAca_SelectedIndexChanged" AutoPostBack="true">
                                                             </asp:RadioButtonList> 
                                                            <asp:Label ID="lblMsjePeriodo" runat="server" CssClass="tdTextoDetalle"></asp:Label><br />                                                          
                                                            <asp:Label ID="lblTextModa" runat="server" Text="Selecciona la modalidad:" CssClass="TextoEtiqueta"></asp:Label>                                                            
                                                            <asp:RadioButtonList ID="rblModalidad" runat="server" CssClass="radioButtonList" OnSelectedIndexChanged="rblModalidad_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:RadioButtonList>
                                                            <asp:Label ID="lblMensajeModalidad" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;vertical-align: bottom;" class="auto-style4"><br /><br /><br />
                                                            <asp:RequiredFieldValidator ID="rfvModalidad" runat="server" ControlToValidate="rblModalidad"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Elija una modalidad de postulación." InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraModalidad" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="text-align: left" class="auto-style4"></td>
                                                        <td style="text-align: left; vertical-align: middle; padding-top: 10px; " class="auto-style5">
                                                            <asp:RadioButtonList ID="rblCarrera" runat="server" CssClass="radioButtonList">
                                                            </asp:RadioButtonList>
                                                            <asp:Label ID="lblMensajeCarreras" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="auto-style4">
                                                            <asp:RequiredFieldValidator ID="rfvCarrera" runat="server" ControlToValidate="rblCarrera"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Elija una carrera." InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraModalidad" Text="*"></asp:RequiredFieldValidator>
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
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="tablaFrame">
                                                    <tr>
                                                       <td style="width: 3%; height: 30px;"></td>
                                                        <td class="SubTitulo" style="width: 50%; padding-top: 15px;">                                                             
                                                            <asp:Label ID="lblBeca" runat="server" Text="Beca"></asp:Label>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 3%;"></td>
                                                        <td style="width: 40%; padding-top: 15px;">&nbsp;&nbsp;                                                            
                                                        </td>
                                                        <td style="width: 2%;">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="text-align: left; vertical-align: middle; width: 50%;">
                                                            <asp:Label ID="lblMsjeBeca" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                            <asp:RadioButtonList ID="rblBeca" runat="server" CssClass="radioButtonList">
                                                            </asp:RadioButtonList>                                                            
                                                        </td>
                                                        <td style="text-align: left"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                           
                                                        </td>
                                                        <td style="text-align: left"></td>
                                                        <td style="text-align: left; vertical-align: middle; padding-top: 10px; width: 50%;">&nbsp;&nbsp;                                                           
                                                        </td>
                                                        <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                          
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: justify" colspan="4">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td style="height: 1px;"></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>                                
                                <td style="text-align: justify;" colspan="5" class="tablaFrameDesc2">
                                    <% if (Session["ModPostulacion"] != null)
                                        {
                                    %>
                                    <div id="DescripcionMod" runat="server">
                                        <div class="box-icon">
                                            <i class="fa fa-info-circle"></i>
                                        </div>
                                        <div class="box-content">
                                            <asp:Label ID="lblDescripcionModalidad" runat="server" CssClass="tdTextoDetalle2"></asp:Label>
                                            <% if (Session["ModPostulacion"].ToString() == "49")
                                                { %>
                                            <asp:Label ID="lblDescripcionDetalle0" runat="server" CssClass="tdTextoDetalle2"></asp:Label>
                                            <asp:Label ID="lblDescripcionDetalle" runat="server" CssClass="tdTextoDetalle2">
                                        <span class="mensaje-alerta">
                                                                    
                                        </span>
                                            </asp:Label>

                                            <% } %>
                                        </div>
                                    </div>
                                    <% } %>
                                </td>                               
                            </tr>
                            <tr>                                
                                <td style="text-align: justify;" colspan="5" class="tablaFrameDesc2">
                                    <% if (Session["ModPostulacion"] != null)
                                        {
                                    %>
                                    <div id="Div1" runat="server">
                                        <div class="box-icon">
                                            <i class="fa fa-info-circle"></i>
                                        </div>
                                        <div class="box-content">
                                            <asp:Label ID="lblAviso" runat="server" CssClass="tdTextoDetalle2"></asp:Label>                                            
                                        </div>
                                    </div>
                                    <% } %>
                                </td>                               
                            </tr>
                            <tr>
                                <td>
                                    <table class="controles tablaFrame" style="width: 100%;">
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/Images/Buttons/btnPrev.png" ToolTip="Página anterior" OnClick="imgBtnBack_Click" />
                                            </td>
                                            <td style="width: 33%; text-align: center; border-bottom: 0px !important" class="SubTitulo"></td>
                                            <td style="text-align: right; width: 33%">
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" ValidationGroup="registraModalidad" />
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
            UrlGetStepsAdmision: 'frm02_ModalidadPostula.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
</body>
</html>
