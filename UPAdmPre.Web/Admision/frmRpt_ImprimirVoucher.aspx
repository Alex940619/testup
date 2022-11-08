<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRpt_ImprimirVoucher.aspx.cs" Inherits="UPAdmPre.Web.Admision.frmRpt_ImprimirVoucher" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ajax:ToolkitScriptManager ID="scr_principal" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="" Width="100%"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
