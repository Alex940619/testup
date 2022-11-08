<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMensajeError.aspx.cs" Inherits="UPAdmPre.Web.frmMensajeError" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP - Admisión Pregrado</title>
    <link href="Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <base target="_self" />
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="Form1" method="post" runat="server">
        <table style="width: 98%; border: 0; background-color: #ffffff;">
            <tr style="width: 98%; height: 47px; border: 0px;">
                <td class="tdTextNormal" colspan="4">
                    <img src="Images/BannerUP.jpg" alt="" />
                </td>
            </tr>
            <tr style="height: 100px;">
                <td style="width: 5%;"></td>
                <td style="text-align: center; vertical-align: middle;">
                    <img src="Images/icoAlerta.gif" alt="" />
                </td>
                <td style="width: 85%; vertical-align: middle;">
                    <asp:Label runat="server" ID="lblmessage" CssClass="tdTextNormalAlerta"></asp:Label>
                </td>
                <td style="width: 5%;"></td>
            </tr>
            <tr style="width: 98%; height: 12px; border: 0px; vertical-align: baseline;">
                <td style="text-align: right;" colspan="4">
                    <img src="Images/icoCancelar.png" onclick="self.close()" alt="Cerrar" style="cursor: hand; display: none;" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
