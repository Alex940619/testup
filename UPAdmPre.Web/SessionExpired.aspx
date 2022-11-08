<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.aspx.cs" Inherits="UPAdmPre.Web.Admision.SessionExpired" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Styles/css/font.css" rel="stylesheet" />
    <link href="Styles/css/custom.css" rel="stylesheet" />
</head>
<body>
    <div class="wrap">
        <div class="contenedor-mensaje">
            <div class="cabecera">
                <i class="fa fa-info-circle" aria-hidden="true"></i>Proceso de inscripción
            </div>
            <div class="contenido">
                <h3>Su sesión ha expirado</h3>
                <!--<p>Existen inconvenientes al procesar tu solicitud. En breve, la Oficina de Admisión se comunicará contigo.</p>-->
            </div>

            <div class="botones">
                <form id="form2" runat="server">
                    
                    <asp:Button ID="btnRefreshParent" CssClass="btn btn-continuar" runat="server" Text="Continuar" OnClick="btnRefreshParent_Click" />
                    
                </form>
            </div>
        </div>
    </div>
</body>
</html>
