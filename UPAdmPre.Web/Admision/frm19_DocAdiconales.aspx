<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm19_DocAdiconales.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm19_DocAdiconales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/font-awesome.min.css" rel="stylesheet" />
    <script src="../JavaScript/JS.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="tksmDocumentos" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <table style="width: 100%;">
            <tr>
                <td class="Titulo">
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
            <%--<tr style="text-align: justify;">
                <td class="tablaFrameDesc">
                    <div id="DescripcionMod" runat="server">
                        <div class="box-icon">
                            <i class="fa fa-info-circle"></i>
                        </div>
                        <div class="box-content">
                            <asp:Label ID="lblMensajeInformativo" runat="server" CssClass="tdTextNormal"></asp:Label>
                        </div>
                    </div>

                </td>
            </tr>--%>
            <tr>
                <td class="tablaFrameDesc" style="height: 30px;">
                    <div class="box-icon">
                        <i class="fa fa-info-circle"></i>
                    </div>
                    <div class="box-content">
                        <asp:Label ID="lblExplicacion1" runat="server" Text="Nota:" CssClass="tdTextoDetalle" Font-Bold="True"></asp:Label><br />
                        <asp:Label ID="lblExplicacion" runat="server" Text="Antes de cargar tus documentos ten en cuenta lo siguiente: la revisión de los documentos procederá solo si son cargados en su totalidad." CssClass="tdTextoDetalle"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlDocumentos" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td class="SubTitulo" style="background-color: aliceblue; text-align: center">
                                    <asp:Label ID="Label10" runat="server" Text="Documentos Adicionales" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                            </tr>
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
                                                    <asp:TemplateField HeaderText="Modelo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCodigo" runat="server" Style="display: none;" Text='<%# Bind("CodDoc") %>'></asp:Label>
                                                            <asp:Label ID="lblApplicationId" runat="server" Style="display: none;" Text='<%# Bind("APPLICATION_ID") %>' />
                                                            <asp:HyperLink ID="hlkDocumento" runat="server" ImageUrl="~/Images/ico_Documento.png" ToolTip="Descargar plantilla del documento" CssClass="tdTextLink">[hlkDocumento]</asp:HyperLink>
                                                            <asp:Label ID="lblIdDocumento" runat="server" Style="display: none;" Text='<%# Bind("ApplicationAttachmentId") %>' />
                                                            <asp:Label ID="lblDescEstado" runat="server" Style="display: none;" Text='<%# Bind("Estado") %>' />
                                                            <asp:Label ID="lblDescripcion" runat="server" Style="display: none;" Text='<%# Bind("Descripcion") %>' />
                                                            <asp:Label ID="lblObservacion" runat="server" Style="display: none;" Text='<%# Bind("Observacion") %>' />
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
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnCargaDocumentos" Style="display: none;" runat="server" />
                    <asp:Button ID="btnCancelarDocumentos" Style="display: none;" runat="server" />
                    <ajax:ModalPopupExtender ID="mpeCargaDocumentos" runat="server" TargetControlID="btnCargaDocumentos"
                        PopupControlID="pnlupCargaDocumentos" PopupDragHandleControlID="pnlupCargaDocumentos" BackgroundCssClass="modalBackground"
                        CancelControlID="btnCancelarDocumentos" Enabled="True" />
                    <asp:Panel ID="pnlupCargaDocumentos" runat="server" CssClass="modalPopup" Width="80%" Style="display: none;">
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
                                                                    <asp:Label ID="Label6" runat="server" Text="Carga de Documentos"></asp:Label>
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
                                                                                <asp:Label ID="Label7" runat="server" CssClass="tdTextNormal" Text="Seleccione el archivo que desea adjuntar.&lt;strong&gt;Tamaño máximo permitido para el archivo 2 MB."></asp:Label>
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
                                                                            <td style="text-align: center">
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
                                                                                            <asp:Label ID="Label8" runat="server" CssClass="tdTextoDetalle">Tipo de documentos soportados:</asp:Label>
                                                                                        </td>                                                                                        
                                                                                    <%--    <td style="width: 16%;">
                                                                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Ico_PDF.png" Height="32px" Width="32px" />
                                                                                        </td>--%>
                                                                                        <td style="width: 16%;">
                                                                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/ico_DOC.png" Height="32px" Width="32px" />
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
