<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm17_ResumenFinal.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm17_ResumenFinal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/font-awesome.min.css" rel="stylesheet" />
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 80%;" align="center">
            <tr>
                <td class="SubTitulo" style="text-align: center;">
                    <asp:Label ID="lblTitulo" runat="server" Text="PROCESO DE INSCRIPCIÓN"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td class="tablaFrameDesc">
                    <div class="box-icon">
                        <i class="fa fa-check"></i>
                    </div>
                    <div class="box-content">
                        <asp:Label ID="lblMsg" runat="server" CssClass="tdTextNormal">A continuación, los documentos y el pago de los derechos de admisión serán revisados por la Dirección de Admisión, la cual evaluará si cumple con los requisitos para aceptar su inscripción.
<br />
Al momento de ser aprobada la inscripción, el postulante recibirá mediante correo electrónico la confirmación de su inscripción exitosa con los pasos a seguir de acuerdo a la modalidad de postulación.</asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
