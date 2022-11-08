<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm14_DocRequeridos.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm14_DocRequeridos" Culture="es-PE" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
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
    <script src="../javascript/jquery.zoom.min.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            height: 46px;
        }
    </style>
</head>
<body>
    <div id="contentStepsAdmision"></div>
    <form id="form1" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="tksmDocumentos" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
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
                                                <asp:Label ID="lblTitulo" runat="server" Text="Carga los siguientes documentos"></asp:Label>
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
                                <td class="tablaFrameDesc" style="height:30px;">
                                    <div class="box-icon">
                                        <i class="fa fa-info-circle"></i>
                                    </div>
                                    <div class="box-content">
                                        <asp:Label ID="lblExplicacion1" runat="server" Text="Nota:" CssClass="tdTextoDetalle" Font-Bold="True"></asp:Label><br />
                                        
                                        
                                        <%--Ini[Christian Ramirez - Caso76999]--%>
                                        <div id="divExplicacion" runat="server" class="tdTextoDetalle"> </div>
                                       <%-- <asp:Label ID="lblExplicacion" 
                                            runat="server" Text="Antes de cargar tus documentos ten en cuenta lo siguiente: la revisión de los documentos procederá solo si son cargados en su totalidad." 
                                            CssClass="tdTextoDetalle"></asp:Label>--%>
                                        <%--Fin[Christian Ramirez - Caso76999]--%>
                                    
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 18px;">
                                    <table style="width: 100%;" class="tablaInterna">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gvDocumentos" runat="server" AutoGenerateColumns="False" CssClass="Grilla" EmptyDataText="No se encontraron registros..." OnRowDataBound="gvDocumentos_RowDataBound" Width="100%">
                                                            <HeaderStyle CssClass="HeaderGrilla" />
                                                            <AlternatingRowStyle CssClass="AlternateItemGrilla" />
                                                            <RowStyle CssClass="ItemGrilla" />
                                                            <PagerStyle CssClass="PagerGrilla" />
                                                            <Columns>
                                                                <asp:BoundField />
                                                                <asp:TemplateField HeaderText="Modelo" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigo" runat="server" Style="display: none;" Text='<%# Bind("CodDoc") %>'></asp:Label>
                                                                        <asp:Label ID="lblApplicationId" runat="server" Style="display: none;" Text='<%# Bind("APPLICATION_ID") %>' />
                                                                        <asp:HyperLink ID="hlkDocumento" runat="server" ImageUrl="~/Images/ico_Documento.png" ToolTip="Descargar plantilla del documento" CssClass="tdTextLink">[hlkDocumento]</asp:HyperLink>
                                                                        <asp:Label ID="lblIdDocumento" runat="server" Style="display: none;" Text='<%# Bind("ApplicationAttachmentId") %>' />
                                                                        <asp:Label ID="lblDescEstado" runat="server" Style="display: none;" Text='<%# Bind("Estado") %>' />
                                                                        <asp:Label ID="lblDescripcion" runat="server" Style="display: none;" Text='<%# Bind("Descripcion") %>' />
                                                                        <asp:Label ID="lblObservacion" runat="server" Style="display: none;" Text='<%# Bind("Observacion") %>' />
                                                                        <asp:Label ID="lblGovernmentId" runat="server" Style="display: none;" Text='<%# Bind("GovernmentId") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Descripción de Documentos" DataField="DESCRIPCION" Visible="False">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Descripción de Documentos">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text="Label" CssClass="ItemGrilla"></asp:Label>
                                                                        <asp:Label ID="Label5" runat="server" Text="Label" CssClass="MsgAlerta" Visible="false"></asp:Label>                                                                        
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <%--<asp:BoundField HeaderText="Estado" DataField="Estado" />--%>
                                                                <asp:TemplateField HeaderText="Estado">
                                                                    <ItemTemplate>
                                                                        <table class="tablaInterna" style="width: 5%;">
                                                                            <tr>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lblEstado" runat="server" Text="Label"></asp:Label>                                                                                    
                                                                                </td>
                                                                                <td>&nbsp;</td>
                                                                                <td style="text-align: center;">
                                                                                    <asp:Image ID="imgEstado" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cargar">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbCargarDoc" runat="server" CausesValidation="false" CssClass="detalleCarga" ImageUrl="~/Images/ico_Upload.png" Height="16px" Width="16px" OnClick="imbCargarDoc_OnClick" />
                                                                        <asp:ImageButton ID="imgDelDoc" runat="server" CausesValidation="false" CssClass="detalleCarga" ImageUrl="~/Images/ico_Delete.png" Height="16px" Width="16px" OnClick="imbDeleteDoc_OnClick" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ver">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbDescargaDoc" runat="server" CausesValidation="false" CssClass="detalleCarga" ImageUrl="~/Images/ico_DownLoad.png" Height="16px" Width="16px" OnClick="imbDescargaDoc_OnClick" />
                                                                        
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    </EditItemTemplate>
                                                                    <ItemStyle Width="5%" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField ControlStyle-Width="0">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text="Label" CssClass="ItemGrilla dni" style="color:transparent" ></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    </EditItemTemplate>
                                                                    <ItemStyle Width="0" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <input id="hCodDocumento" name="hCodDocumento" runat="server" type="hidden" />
                                                        <input id="hIdDocumento" name="hIdDocumento" runat="server" type="hidden" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="gvDocumentos" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; padding: 10px 0px;">
                                                <table style="width: auto;" border="0" align="center">
                                                    <tr>
                                                        <td style="width: 50%;">
                                                            <asp:Label ID="lblMensajeCarreras" runat="server" CssClass="tdTextoDetalle">Tipo de documentos soportados:</asp:Label>
                                                        </td>
                                                        <td style="width: 16%;">
                                                            <asp:Image ID="imgJPG" runat="server" ImageUrl="~/Images/Ico_JPG.png" Height="32px" Width="32px" />
                                                        </td>
                                                        <td style="width: 16%;">
                                                            <asp:Image ID="imgPdf" runat="server" ImageUrl="~/Images/Ico_PDF.png" Height="32px" Width="32px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <table style="width: 70%;" class="grillaAdm" border="1" align="center">
                                                    <tr>
                                                        <td style="text-align: center;" class="TablaDetalle LetraTablaDetalle" colspan="2">Leyenda</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 32px;" class="TablaDetalle LetraTablaDetalle">Imagen
                                                        </td>
                                                        <td style="text-align: center;" class="TablaDetalle LetraTablaDetalle">Descripción
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; height: 25px;">
                                                            <asp:ImageButton ID="ImageButton2" runat="server"
                                                                ImageUrl="~/Images/ico_Upload.png" ToolTip="En revisión" />
                                                        </td>
                                                        <td>Subir documento.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; height: 25px;">
                                                            <asp:ImageButton ID="imgLeyEstadoDocFAEPG" runat="server"
                                                                ImageUrl="~/Images/icoEnRevision.png" ToolTip="En revisión" />
                                                        </td>
                                                        <td>Documento se encuentra en proceso de revisión.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; height: 25px;">
                                                            <asp:ImageButton ID="imgLeyEstadoDocOKEPG" runat="server" ImageUrl="~/Images/icoObservado.png" ToolTip="Observado" />
                                                        </td>
                                                        <td>Documento fue observado. Postulante debe actualizarlo.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; height: 25px;">
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icoAprobado.png" ToolTip="Aprobado" />
                                                        </td>
                                                        <td>DocDocDocumento cumple con los requisitos solicitados por admisión.
                                                        </td>
                                                    </tr>

                                                    <%--                                            <tr>
                                                <td align="center">
                                                    <asp:ImageButton ID="imgLeyDescargarEPG" runat="server" ImageUrl="~/Imagenes/Admision V2/download.png"  ToolTip="Descargar archivo" onclick="imgDescargarEPG_Click" />
                                                </td>
                                                <td>
                                                    Descargar documento enviado.
                                                </td>
                                            </tr>--%>
                                                    <%--
                                            <tr>
                                                <td align="center">
                                                    <asp:ImageButton ID="imgLeyPlantillaEPG" runat="server" ImageUrl="~/Imagenes/plantillaEPG.png"  ToolTip="Ver Plantilla" />
                                                </td>
                                                <td>
                                                    Descarga documentos de ejemplos antes de subir el archivo en la documentación
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="center">
                                                    <asp:ImageButton ID="imgLeyUploadEPG" runat="server" ImageUrl="~/Imagenes/uploadEPG.png"  ToolTip="Subir Archivo" />
                                                </td>
                                                <td>
                                                    Permite adjuntar archivos a las documentación
                                                </td>
                                            </tr>
                                                    --%>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
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
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" OnClientClick="return confirmaAprobar();" ValidationGroup="registraSitAcademica" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnCargaDocumentos" Style="display: none;" runat="server" />
                                    <asp:Button ID="btnCancelarDocumentos" Style="display: none;" runat="server" />
                                    <ajax:ModalPopupExtender ID="mpeCargaDocumentos" runat="server" TargetControlID="btnCargaDocumentos"
                                        PopupControlID="pnlupCargaDocumentos" PopupDragHandleControlID="pnlupCargaDocumentos" BackgroundCssClass="modalBackground"
                                        CancelControlID="btnCancelarDocumentos" Enabled="True" />
                                    <asp:Panel ID="pnlupCargaDocumentos" runat="server" CssClass="modalPopup" Width="80%" Style="display: block;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upCargaDocumentos" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <table style="width: 100%;" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td class="Titulo">
                                                                                    <asp:Label ID="Label1" runat="server" Text="Carga de Documentos"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 100%;" class="tablaFrame">
                                                                                        <tr style="height: 40px;">
                                                                                            <td>
                                                                                                <asp:Label ID="lblDocumento" runat="server" Text="Documento"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 40px;">
                                                                                            <td>
                                                                                                <asp:Label ID="Label2" runat="server" CssClass="tdTextNormal" Text="Seleccione el archivo que desea adjuntar.&lt;strong&gt;Tamaño máximo permitido para el archivo 2 MB."></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <table style="width: 100%;">
                                                                                                    <tr>
                                                                                                        <td style="width: 95%;">
                                                                                                            <asp:FileUpload ID="fuDocumento" runat="server" Width="100%" />
                                                                                                        </td>
                                                                                                        <td style="width: 5%;">
                                                                                                            <asp:RequiredFieldValidator ID="rfvDocumento" runat="server" ControlToValidate="fuDocumento"
                                                                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione achivo a cargar" InitialValue=""
                                                                                                                SetFocusOnError="true" ValidationGroup="registraDocumentos" Text="*" Style="display: inline"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblError" runat="server" CssClass="MsgAlertaIncompleto" Text="Error" Visible="False"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center" class="auto-style1">
                                                                                                <table style="width: 25%; text-align: center" align="center">
                                                                                                    <tr style="height: 40px;">
                                                                                                        <td>
                                                                                                            <asp:Button ID="btnAdjuntar" runat="server" Text="Guardar" OnClick="btnAdjuntar_Click" Width="80px" ValidationGroup="registraDocumentos" />
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Width="80px" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <table style="width: auto;" border="0" align="center">
                                                                                                    <tr style="height: 60px;">
                                                                                                        <td style="width: 50%;">
                                                                                                            <asp:Label ID="Label3" runat="server" CssClass="tdTextoDetalle">Tipo de documentos soportados:</asp:Label>
                                                                                                        </td>
                                                                                                        <td style="width: 16%;">
                                                                                                            <asp:Image ID="imgJPGPopUp" runat="server" ImageUrl="~/Images/Ico_JPG.png" />
                                                                                                        </td>
                                                                                                        <td style="width: 16%;">
                                                                                                            <asp:Image ID="imgPDFPopUp" runat="server" ImageUrl="~/Images/Ico_PDF.png" />
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
                                                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false"
                                                                                        ValidationGroup="registraDocumentos" CssClass="MsgAlertaIncompleto" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnAdjuntar" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
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
                        function confirmaAprobar() {
                            var prueba = $($('.dni')[0]).text();
                                                     
                            //console.log($($('.dni')[0]).text());
                            //return;
                            var parte1 = "Antes de continuar al siguiente paso, verifica tu número de DNI/CE/PAS a continuación:\n\n";
                            var parte2 = "DNI/CE/PAS: " +prueba;
                            var parte3 = "\n\n Si es correcto, haz clic en ACEPTAR para continuar al siguiente paso. \n\n";
                            var parte4 = "De lo contrario, haz clic en CANCELAR y regresa al paso 2 y modifícalo antes de continuar (toma en cuenta que tu boleta de pago se generará con el DNI/CE/PAS que registraste y este debe ser únicamente el que corresponde al postulante).  ";
                            var mensaje = parte1 + parte2 +  parte3 + parte4;
                            //if (confirm("¿confirmar si el DNI es correcto del postulante para la emision de boleta de admisión?") == true) {
                            if (confirm(mensaje) == true) {
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                    </script>
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
        var UrlAcion = {
            UrlGetStepsAdmision: 'frm14_DocRequeridos.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
</body>
</html>
