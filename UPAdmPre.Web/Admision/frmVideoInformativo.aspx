<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVideoInformativo.aspx.cs" Inherits="UPAdmPre.Web.Admision.frmVideoInformativo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>UP - Admisión Pregrado</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <video controls="controls" onprogress="" width="620" height="360">
                <source src="../Video/ColdPlay-InMyPlace.mp4" type="video/mp4" />
                <%--<source src="../Video/ColdPlay-InMyPlace.mp4 # t=60" type="video/mp4" />--%>
            </video>
        </div>
    </form>
</body>
</html>
