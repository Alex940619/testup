<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMsjeErrorUP.aspx.cs" Inherits="UPAdmPre.Web.Admision.frmMsjeErrorUP" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <title>Mensaje de Error</title>
</head>
<body>
    <form id="form1" runat="server">
        <div >
            <div class="TituloModuloError">
                <div style="text-align: center;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ico_Error.gif" Width="34px" Height="34px" />
                    <asp:Label ID="lblTitulo" runat="server" CssClass="Titulo">Proceso de inscripción</asp:Label>
                </div>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 10%;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 10%;">&nbsp;</td>
                    <td>
                        <asp:Label ID="lblDescError" runat="server" CssClass="tdTextoDetalle">Existen inconvenientes al procesar tu solicitud. En breve, la Dirección de Admisión se comunicará contigo.</asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 10%;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 10%;">&nbsp;</td>
                    <td>
                        <asp:HyperLink ID="hlRegresar" runat="server" CssClass="tdTextoTitulo" NavigateUrl="javascript:history.back()">Regresar</asp:HyperLink>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
