<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm08_Idiomas.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm08_Idiomas" Culture="es-PE" %>

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
                        <table style="width: 100%;" class="tablaInterna">
                            <tr>
                                <td style="font-family: Arial; font-size: 13px; color: Red; font-weight: bold; text-align: left; height: 30px;">
                                    <asp:Label ID="Label3" runat="server" Text="Información opcional (no obligatoria)"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="SubTitulo">
                                    <table style="width: 100%;" class="tablaInterna">
                                        <tr>
                                            <td style="width: 70%;">
                                                <asp:Label ID="lblTitulo" runat="server" Text="Idioma 1"></asp:Label>
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
                                                <table style="width: 100%;" class="tablaInterna">
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 12%;">
                                                            <asp:Label ID="lblIdioma1" runat="server" Text="Idioma:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 82%; padding-top: 5px;">
                                                            <asp:DropDownList ID="ddlIdioma1" runat="server" CssClass="txtTextoCombo" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma1_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                        <td style="width: 2%;" class="requerido"></td>
                                                    </tr>
                                                    <% if (ddlIdioma1.SelectedValue == "99")
                                                       { 
                                                    %>
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 10%;">
                                                            <asp:Label ID="Label1" runat="server" Text="Otro Idioma:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 84%; padding-top: 5px;">
                                                            <asp:TextBox ID="txtOtroIdioma1" runat="server" CssClass="txtCajaTexto" MaxLength="30" onkeydown="return (event.keyCode!=13);" Width="96%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 2%;" class="requerido"></td>

                                                    </tr>
                                                    <% } %>
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 10%;">
                                                            <asp:Label ID="lblNivelConocIdioma1" runat="server" Text="Nivel:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 20%; padding-top: 0px; padding-bottom: 0px;">
                                                            <asp:RadioButtonList ID="rblNivelConocIdioma1" runat="server" CssClass="radioButtonList" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1">Básico</asp:ListItem>
                                                                <asp:ListItem Value="2">Intermedio</asp:ListItem>
                                                                <asp:ListItem Value="3">Avanzado</asp:ListItem>
                                                                <asp:ListItem Value="4">Nativo</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td style="width: 2%;" class="requerido"></td>
                                                    </tr>
                                                    <tr id="tbl_idioma" >
                                                        <td style="width: 2%;"></td>
                                                        <td colspan="2">
                                                            <asp:GridView ID="gdv_idioma" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkCtrl" runat="server" onclick="Check(this);"/>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField AccessibleHeaderText="Nombre" DataField="Name" HeaderText="Tipo de Examen/Certificado (originales)" />
                                                                    <asp:BoundField AccessibleHeaderText="Descripción" DataField="DESCRIPTION" HeaderText="Puntaje requerido" />
                                                                    <asp:BoundField AccessibleHeaderText="EventId" DataField="EventId" HeaderText="EventId" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"/>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblCertificacionIdioma1" runat="server" Text="Certificación:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="padding-top: 18px;">
                                                            <asp:DropDownList ID="ddlCertificacionIdioma1" runat="server" CssClass="txtTextoCombo" AutoPostBack="true"></asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <% if (ddlCertificacionIdioma1.SelectedValue == "7")
                                                       {
                                                    %>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblOtroCertIdioma1" runat="server" Text="Otra Certificación:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <table style="width: 80%;" class="tablaInterna">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOtroCertIdioma1" runat="server" CssClass="txtCajaTexto" MaxLength="30" onkeydown="return (event.keyCode!=13);" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td style="text-align: right; width: 130px;">
                                                                        <asp:Label ID="lblPuntajeIdioma1" runat="server" Text="Puntaje/Nota:" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPuntajeIdioma1" runat="server" CssClass="txtCajaTexto" MaxLength="20" Width="50%" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <% } %>--%>
                                                    <tr>
                                                        <td style="height: 1px;"></td>
                                                        <td>
                                                            <div style="display: none;">
                                                                <asp:TextBox ID="txtApplicationEducationId1" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtApplicationEducationId2" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtApplicationEducationId3" runat="server" Width="100px"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Button ID="BtnOcultaIdioma2" runat="server" Text="Quitar Idioma" CssClass="btnQuitar" OnClick="BtnOcultaIdioma2_Click" ToolTip="Quitar idioma" />
                                                <asp:Button ID="BtnAgregaIdioma2" runat="server" Text="Agregar Idioma" CssClass="btnAgregar" OnClick="BtnAgregaIdioma2_Click" ToolTip="Agregar mas idiomas" Visible="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlIdioma2" runat="server">
                                                    <table style="width: 100%;" class="tablaInterna">
                                                        <tr>
                                                            <td colspan="4" class="SubTitulo">
                                                                <asp:Label ID="lblSubTituloIdioma2" runat="server" Text="Idioma 2"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 10%;">
                                                                <asp:Label ID="lblIdioma2" runat="server" Text="Idioma:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 84%; padding-top: 10px;">
                                                                <asp:DropDownList ID="ddlIdioma2" runat="server" CssClass="txtTextoCombo" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma2_SelectedIndexChanged"></asp:DropDownList>
                                                            </td>
                                                            <td style="width: 2%;" class="requerido"></td>
                                                        </tr>
                                                        <% if (ddlIdioma2.SelectedValue == "99")
                                                           { 
                                                        %>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 10%;">
                                                                <asp:Label ID="Label2" runat="server" Text="Otro Idioma:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 84%; padding-top: 5px;">
                                                                <asp:TextBox ID="txtOtroIdioma2" runat="server" CssClass="txtCajaTexto" MaxLength="30" onkeydown="return (event.keyCode!=13);" Width="96%"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 2%;" class="requerido"></td>
                                                        </tr>
                                                        <% } %>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 10%;">
                                                                <asp:Label ID="lblNivelConocIdioma2" runat="server" CssClass="TextoEtiqueta" Text="Nivel:"></asp:Label>
                                                            </td>
                                                            <td style="width: 20%; padding-top: 0px; padding-bottom: 15px;">
                                                                <asp:RadioButtonList ID="rblNivelConocIdioma2" runat="server" CssClass="radioButtonList" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1">Básico</asp:ListItem>
                                                                    <asp:ListItem Value="2">Intermedio</asp:ListItem>
                                                                    <asp:ListItem Value="3">Avanzado</asp:ListItem>
                                                                    <asp:ListItem Value="4">Nativo</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr id="tbl_idioma2" >
                                                            <td style="width: 2%;"></td>
                                                            <td colspan="2">
                                                                <asp:GridView ID="gdv_idioma2" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkCtrl" runat="server" onclick="Check(this);"/>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField AccessibleHeaderText="Nombre" DataField="Name" HeaderText="Tipo de Examen/Certificado (originales)" />
                                                                        <asp:BoundField AccessibleHeaderText="Descripción" DataField="DESCRIPTION" HeaderText="Puntaje requerido" />
                                                                        <asp:BoundField AccessibleHeaderText="EventId" DataField="EventId" HeaderText="EventId" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"/>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblCertificacionIdioma2" runat="server" CssClass="TextoEtiqueta" Text="Certificación:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCertificacionIdioma2" runat="server" CssClass="txtTextoCombo" AutoPostBack="true"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <% if (ddlCertificacionIdioma2.SelectedValue == "7")
                                                           {
                                                        %>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblOtroCertIdioma2" runat="server" CssClass="TextoEtiqueta" Text="Otra Certificación:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <table style="width: 80%;" class="tablaInterna">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtOtroCertIdioma2" runat="server" CssClass="txtCajaTexto" MaxLength="30" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="text-align: right; width: 130px;">
                                                                            <asp:Label ID="lblPuntajeIdioma2" runat="server" CssClass="TextoEtiqueta" Text="Puntaje/Nota:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPuntajeIdioma2" runat="server" CssClass="txtCajaTexto" MaxLength="20" onkeydown="return (event.keyCode!=13);" Width="50%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <% } %>--%>
                                                        <tr>
                                                            <td style="height: 1px;"></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table align="right" class="tablaInterna">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="BtnOcultaIdioma3" runat="server" Text="Quitar Idioma" CssClass="btnQuitar" OnClick="BtnOcultaIdioma3_Click" ToolTip="Quitar idioma" Width="130px" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="BtnAgregaIdioma3" runat="server" Text="Agregar Idioma" CssClass="btnAgregar" OnClick="BtnAgregaIdioma3_Click" ToolTip="Agregar mas idiomas" Width="136px" Visible="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlIdioma3" runat="server">
                                                    <table style="width: 100%;" class="tablaInterna">
                                                        <tr>
                                                            <td colspan="4" class="SubTitulo">
                                                                <asp:Label ID="lblSubTituloIdioma3" runat="server" Text="Idioma 3"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 10%;">
                                                                <asp:Label ID="lblIdioma3" runat="server" Text="Idioma:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 84%; padding-top: 5px;">
                                                                <asp:DropDownList ID="ddlIdioma3" runat="server" CssClass="txtTextoCombo" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma3_SelectedIndexChanged"></asp:DropDownList>
                                                            </td>
                                                            <td style="width: 2%;" class="requerido"></td>
                                                        </tr>
                                                        <% if (ddlIdioma3.SelectedValue == "99")
                                                           { 
                                                        %>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 10%;">
                                                                <asp:Label ID="Label6" runat="server" Text="Otro Idioma:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 84%; padding-top: 5px;">
                                                                <asp:TextBox ID="txtOtroIdioma3" runat="server" CssClass="txtCajaTexto" MaxLength="30" onkeydown="return (event.keyCode!=13);" Width="96%"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 2%;" class="requerido"></td>
                                                        </tr>
                                                        <% } %>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 10%;">
                                                                <asp:Label ID="lblNivelConocIdioma3" runat="server" CssClass="TextoEtiqueta" Text="Nivel:"></asp:Label>
                                                            </td>
                                                            <td style="width: 20%; padding-top: 0px; padding-bottom: 15px;">
                                                                <asp:RadioButtonList ID="rblNivelConocIdioma3" runat="server" CssClass="radioButtonList" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1">Básico</asp:ListItem>
                                                                    <asp:ListItem Value="2">Intermedio</asp:ListItem>
                                                                    <asp:ListItem Value="3">Avanzado</asp:ListItem>
                                                                    <asp:ListItem Value="4">Nativo</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td style="width: 2%;"></td>
                                                        </tr>
                                                        <tr id="tbl_idioma3" >
                                                            <td style="width: 2%;"></td>
                                                            <td colspan="2">
                                                                <asp:GridView ID="gdv_idioma3" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkCtrl" runat="server" onclick="Check(this);"/>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField AccessibleHeaderText="Nombre" DataField="Name" HeaderText="Tipo de Examen/Certificado (originales)" />
                                                                        <asp:BoundField AccessibleHeaderText="Descripción" DataField="DESCRIPTION" HeaderText="Puntaje requerido" />
                                                                        <asp:BoundField AccessibleHeaderText="EventId" DataField="EventId" HeaderText="EventId" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"/>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <%-- <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblCertificacionIdioma3" runat="server" CssClass="TextoEtiqueta" Text="Certificación:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCertificacionIdioma3" runat="server" CssClass="txtTextoCombo" AutoPostBack="true"></asp:DropDownList>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <% if (ddlCertificacionIdioma3.SelectedValue == "7")
                                                           { 
                                                        %>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblOtroCertIdioma3" runat="server" CssClass="TextoEtiqueta" Text="Otro:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <table style="width: 80%;" class="tablaInterna">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtOtroCertIdioma3" runat="server" CssClass="txtCajaTexto" MaxLength="30" onkeydown="return (event.keyCode!=13);" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td style="text-align: right; width: 130px;">
                                                                            <asp:Label ID="lblPuntajeIdioma3" runat="server" CssClass="TextoEtiqueta" Text="Puntaje/Nota:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPuntajeIdioma3" runat="server" CssClass="txtCajaTexto" MaxLength="20" onkeydown="return (event.keyCode!=13);" Width="50%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <% } %>--%>
                                                        <tr>
                                                            <td style="height: 1px;"></td>
                                                            <td></td>
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
                            <%--<tr>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label4" runat="server" CssClass="tdTextNormal" Text="("></asp:Label>
                                    <asp:Image ID="Image18" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                    <asp:Label ID="Label5" runat="server" CssClass="tdTextNormal" Text=")  "></asp:Label>
                                    <asp:Label ID="Label3" runat="server" CssClass="tdTextNormal" Text="Campos Obligatorios"></asp:Label>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <table class="controles" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/Images/Buttons/btnPrev.png" ToolTip="Página anterior" OnClick="imgBtnBack_Click" />
                                            </td>
                                            <td></td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" ValidationGroup="registraIdioma" />
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
            UrlGetStepsAdmision: 'frm08_Idiomas.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
</body>
</html>
