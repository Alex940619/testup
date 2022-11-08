<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMensajeExito.aspx.cs" Inherits="UPAdmPre.Web.frmMensajeExito" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP - Admisión Pregrado</title>
    <link href="Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <base target="_self" />
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" onload="setTimeout('window.close()',10000);">
    <form id="Form1" method="post" runat="server">
        <table style="width: 98%; border: 0; background-color: #ffffff;">
            <tr>
                <td class="tdTextNormal" colspan="4">
                    <img src="Images/BannerUP.jpg" alt="" />
                </td>
            </tr>
            <tr style="height: 100px;">
                <td style="width: 5%;"></td>
                <td style="text-align: center">
                    <img src="Images/icoOk.jpg" alt="" />
                </td>
                <td style="width: 80%; text-align: center;" class="tdTextNormal">
                    <asp:Label runat="server" ID="lblmessage" ForeColor="MidnightBlue"></asp:Label>
                </td>
                <td style="width: 5%;"></td>
            </tr>
            <tr style="vertical-align: baseline;">
                <td style="text-align: right;" colspan="4">
                    <img src="Images/icoCancelar.png" onclick="self.close()" alt="Cerrar" style="cursor: hand; display: none;" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
