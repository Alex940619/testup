<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UPAdmPre.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP - Admisión Pregrado</title>
    <link href="Styles/Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="width: 170px;">
                    <asp:Label ID="lblUsuarioRed" runat="server" Text="Usuario de Red:" CssClass="TextoEtiqueta"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtUsuarioRed" runat="server" CssClass="txtCajaTexto" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsuarioRed" runat="server" ControlToValidate="txtUsuarioRed"
                        CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Usuario de red" InitialValue=""
                        SetFocusOnError="true" ValidationGroup="Registra" Text="*" Style="display: inline"></asp:RequiredFieldValidator>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAplicationId" runat="server" Text="Application ID:" CssClass="TextoEtiqueta"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtApplicationId" runat="server" CssClass="txtCajaTexto" Width="200px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAplicationId0" runat="server" Text="ApplicationFormSettingId:" CssClass="TextoEtiqueta"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="ddlPrograma" runat="server" CssClass="txtTextoCombo" Width="205px">
                        <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                        <asp:ListItem Value="7">Admisión Pre</asp:ListItem>
                        <asp:ListItem Value="19">Programa EPU</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlPrograma" runat="server" ControlToValidate="ddlPrograma"
                        CssClass="MsgAlertaIncompleto" ErrorMessage="Elija un Programa" InitialValue="0"
                        SetFocusOnError="true" ValidationGroup="Registra" Text="*" Style="display: inline"></asp:RequiredFieldValidator>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">
                    <asp:Button ID="btnContinuar" runat="server" Text="Continuar" OnClick="Button1_Click" ValidationGroup="Registra" Width="100px" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="lblAplicationId1" runat="server" Text="Resultado o Escala:" CssClass="TextoEtiqueta"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="ddlResultado" runat="server" CssClass="txtTextoCombo" Width="205px">
                        <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                        <asp:ListItem Value="1">Escala</asp:ListItem>
                        <asp:ListItem Value="2">Resultado</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlPrograma0" runat="server" ControlToValidate="ddlResultado"
                        CssClass="MsgAlertaIncompleto" ErrorMessage="Elija una Opción" InitialValue="0"
                        SetFocusOnError="true" ValidationGroup="Registra" Text="*" Style="display: inline"></asp:RequiredFieldValidator>
                    </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="Registra" CssClass="MsgAlertaIncompleto" />
                </td>
                <td>
                    <asp:Button ID="btnVerificar" runat="server" Text="Verificar" OnClick="btnVerificar_Click" Width="100px" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>--%>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
<%--            <tr>
                <td>
                    <asp:Label ID="lblAplicationId1" runat="server" Text="Enviar Correo de Prueba:" CssClass="TextoEtiqueta"></asp:Label>
                </td>
                <td><asp:Button ID="btnEnviaCorreo" runat="server" Text="Enviar Correo" OnClick="btnEnviaCorreo_Click" />
                </td>
                <td></td>
                <td></td>
            </tr>--%>
        </table>
    </form>
</body>
</html>
