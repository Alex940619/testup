<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm10_InfoReferencias.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm10_InfoReferencias" Culture="es-PE" %>

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
    <script type="text/javascript">
        $("document").ready(function () {

            // validate signup form on keyup and submit
            $("#frm10_InfoReferencias").validate({
                rules: {
                    txtEmailRef1: {
                        required: false,
                        email: true
                    },
                    txtEmailRef2: {
                        required: false,
                        email: true
                    },
                    txtEmailRef3: {
                        required: false,
                        email: true
                    },
                    txtEmailRef4: {
                        required: false,
                        email: true
                    },
                    txtEmailRef5: {
                        required: false,
                        email: true
                    }
                },
                messages: {
                    txtEmailRef1: "Debe ingresar un correo válido.",
                    txtEmailRef2: "Debe ingresar un correo válido.",
                    txtEmailRef3: "Debe ingresar un correo válido.",
                    txtEmailRef4: "Debe ingresar un correo válido.",
                    txtEmailRef5: "Debe ingresar un correo válido."
                }
            });

        });
    </script>
    <style type="text/css">
        .td_txt_Email label.error {
            color: red;
        }
    </style>
</head>
<body>
    <div id="contentStepsAdmision"></div>
    <form id="frm09_InfoReferencias" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="scr_principal" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <table style="width: 100%; margin: auto;">
            <tr>
                <td>
                    <table style="width: 100%;visibility: hidden">
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
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="tablaFrameDesc">
                                                <div class="box-icon">
                                                    <i class="fa fa-info-circle"></i>
                                                </div>
                                                <div class="box-content">
                                                    <asp:Label ID="lblExplicacion1" runat="server" Text="Nota:" CssClass="tdTextoDetalle" Font-Bold="True"></asp:Label><br />
                                                    <asp:Label ID="lblExplicacion2" runat="server" Text="Ingrese dos referencias de dos profesores o autoridades de una institución educativa, cultural, deportiva o religiosa, que te conozcan por un periodo mínimo de un año. Cabe resaltar que las referencias no podrán ser realizadas por familiares directos." CssClass="tdTextoDetalle"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="tablaFrame">
                                                    <tr>
                                                        <td colspan="10" class="SubTitulo">
                                                            <table style="width: 100%;" class="tablaInterna">
                                                                <tr>
                                                                    <td style="width: 70%;">
                                                                        <asp:Label ID="lblSubTituloReferencia1" runat="server" Text="Referencia Nro. 1"></asp:Label>
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
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 15%;">
                                                            <asp:Label ID="lblEmailRef1" runat="server" Text="Correo electrónico:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 32%; padding-top: 18px;" class="td_txt_Email">
                                                            <asp:TextBox ID="txtEmailRef1" runat="server" CssClass="txtCajaTexto" MaxLength="50" placeholder="Ejemplo: ejemplo@correo.com"></asp:TextBox>
                                                        </td>
                                                        <%--<td style="width: 1%; padding: 15px 8px 0px 7px;" class="requerido">
                                                            <asp:ImageButton ID="imgBtnBuscaEmailRef1" runat="server" Height="16px" ImageUrl="~/Images/Buttons/btnSearch.png"
                                                                Width="16px" OnClick="imgBtnBuscaEmailRef1_Click" ToolTip="Buscar Referente por su Email" />
                                                        </td>--%>
                                                        <td style="padding: 0px 8px 0px 7px; width: 2%;" class="requerido">
                                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvEmailRef1" runat="server" ControlToValidate="txtEmailRef1"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese correo del Referente" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraReferencias" Text="*" Style="display: inline"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 13%;"></td>
                                                        <td style="width: 32%;"></td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 2%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 15%;">
                                                            <asp:Label ID="lblNomRef1" runat="server" Text="Nombres:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 32%;">
                                                            <asp:TextBox ID="txtNomRef1" runat="server" CssClass="txtCajaTexto" MaxLength="55" placeholder="Nombre del Referente"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 0px 8px 0px 7px;" class="requerido">
                                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvNomRef1" runat="server" ControlToValidate="txtNomRef1"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre del Referente" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraReferencias" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 13%;">
                                                            <asp:Label ID="lblApeRef1" runat="server" Text="Apellidos:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 32%;">
                                                            <asp:TextBox ID="txtApeRef1" runat="server" CssClass="txtCajaTexto" MaxLength="55" placeholder="Apellidos del Referente"></asp:TextBox>
                                                        </td>
                                                        <td  style="width:2%; padding: 0px 8px 0px 7px;" class="requerido">
                                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvApeRef1" runat="server" ControlToValidate="txtApeRef1"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Apellidos del Referente" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraReferencias" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblInstitucionRef1" runat="server" Text="Institución:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInstitucionRef1" runat="server" CssClass="txtCajaTexto" MaxLength="55"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 0px 8px 0px 7px;" class="requerido">
                                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvInstitucionRef1" runat="server" ControlToValidate="txtInstitucionRef1"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese institución" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraReferencias" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblCargoRef1" runat="server" Text="Cargo:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 20%;">
                                                            <asp:TextBox ID="txtCargoRef1" runat="server" CssClass="txtCajaTexto" MaxLength="40"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 2%; padding: 0px 8px 0px 7px;" class="requerido">
                                                            <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvCargoRef1" runat="server" ControlToValidate="txtCargoRef1"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese cargo" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraReferencias" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <div style="display: none;">
                                                                <asp:TextBox ID="txtIdReferencia1" runat="server"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdReferencia2" runat="server"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdReferencia3" runat="server"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdReferencia4" runat="server"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdReferencia5" runat="server"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="tablaFrame">
                                                    <tr>
                                                        <td colspan="10" class="SubTitulo" style="text-align: left;">
                                                            <asp:Label ID="lblSubTituloReferencia2" runat="server" Text="Referencia Nro. 2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 15%;">
                                                            <asp:Label ID="lblEmailRef2" runat="server" Text="Correo electrónico:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 32%; padding-top: 18px;" class="td_txt_Email">
                                                            <asp:TextBox ID="txtEmailRef2" runat="server" CssClass="txtCajaTexto" MaxLength="50" placeholder="Ejemplo: ejemplo@correo.com"></asp:TextBox>
                                                        </td>
                                                        <%--<td style="width: 2%; padding: 15px 8px 0px 7px;" class="requerido">
                                                            <asp:ImageButton ID="imgBtnBuscaEmailRef2" runat="server" Height="16px" ImageUrl="~/Images/Buttons/btnSearch.png"
                                                                Width="16px" OnClick="imgBtnBuscaEmailRef2_Click" ToolTip="Buscar Referente por su Email" />
                                                        </td>--%>
                                                        <td style="padding: 0px 8px 0px 7px; width: 2%;" class="requerido">
                                                            <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvEmailRef2" runat="server" ControlToValidate="txtEmailRef2"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Correo del Referente" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraReferencias" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 13%;"></td>
                                                        <td style="width: 32%;"></td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 2%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 15%;">
                                                            <asp:Label ID="lblNomRef2" runat="server" Text="Nombres:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 32%;">
                                                            <asp:TextBox ID="txtNomRef2" runat="server" CssClass="txtCajaTexto" MaxLength="55" placeholder="Nombre del Referente"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 2px 8px 0px 7px;" class="requerido">
                                                            <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
							
                                                       <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvNomRef2" runat="server" ControlToValidate="txtNomRef2"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre del Referente" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraReferencias" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 13%;">
                                                            <asp:Label ID="lblApeRef2" runat="server" Text="Apellidos:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 32%;">
                                                            <asp:TextBox ID="txtApeRef2" runat="server" CssClass="txtCajaTexto" MaxLength="55" placeholder="Apellidos del Referente"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 2%; padding: 0px 8px 0px 7px;" class="requerido">
                                                            <asp:Image ID="Image25" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvApeRef2" runat="server" ControlToValidate="txtApeRef2"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Apellidos del Referente" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraReferencias" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblInstitucionRef2" runat="server" Text="Institución:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInstitucionRef2" runat="server" CssClass="txtCajaTexto" MaxLength="55"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 2px 8px 0px 7px;" class="requerido">
                                                            <asp:Image ID="Image12" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvInstitucionRef2" runat="server" ControlToValidate="txtInstitucionRef2"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese institución" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraReferencias" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblCargoRef2" runat="server" Text="Cargo:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 20%;">
                                                            <asp:TextBox ID="txtCargoRef2" runat="server" CssClass="txtCajaTexto" MaxLength="40"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 2%; padding: 0px 8px 0px 7px;" class="requerido">
                                                            <asp:Image ID="Image26" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvCargoRef2" runat="server" ControlToValidate="txtCargoRef2"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese cargo" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraReferencias" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <%--<asp:Button ID="btnQuitarRef3" runat="server" Text="Quitar Referencia" CssClass="btnQuitar" OnClick="btnQuitarRef3_Click" ToolTip="Quitar referencia" />
                                                <asp:Button ID="btnAgregaRef3" runat="server" Text="Agregar Referencia" CssClass="btnAgregar" OnClick="btnAgregaRef3_Click" ToolTip="Agregar referencia" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlRef3" runat="server">
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td colspan="10" class="SubTitulo" style="text-align: left;">
                                                                <asp:Label ID="lblSubTituloReferencia3" runat="server" Text="Referencia Nro. 3"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 15%;">
                                                                <asp:Label ID="lblEmailRef3" runat="server" Text="Correo electrónico:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 32%; padding-top: 18px;" class="td_txt_Email">
                                                                <asp:TextBox ID="txtEmailRef3" runat="server" CssClass="txtCajaTexto" MaxLength="50" placeholder="Ejemplo: ejemplo@correo.com"></asp:TextBox>
                                                            </td>
                                                            <%--<td style="width: 2%; padding: 15px 8px 0px 7px;" class="requerido">
                                                                <asp:ImageButton ID="imgBtnBuscaEmailRef3" runat="server" Height="16px" ImageUrl="~/Images/Buttons/btnSearch.png" OnClick="imgBtnBuscaEmailRef3_Click" ToolTip="Buscar Referente por su Email" Width="16px" />
                                                            </td>--%>
                                                            <td style="padding: 0px 8px 0px 7px; width: 2%;" class="requerido">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvEmailRef3" runat="server" ControlToValidate="txtEmailRef3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre del Referente" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 2%;"></td>
                                                            <td style="width: 13%;"></td>
                                                            <td style="width: 32%;"></td>
                                                            <td style="width: 2%;"></td>
                                                            <td style="width: 2%;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 15%;">
                                                                <asp:Label ID="lblNomRef3" runat="server" Text="Nombres:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 32%;">
                                                                <asp:TextBox ID="txtNomRef3" runat="server" CssClass="txtCajaTexto" MaxLength="55" placeholder="Nombre del Referente"></asp:TextBox>
                                                            </td>
                                                            <td style="width:2%; padding: 2px 8px 0px 7px;" class="requerido">
                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvNomRef3" runat="server" ControlToValidate="txtNomRef3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre del Referente" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 2%;"></td>
                                                            <td style="width: 13%;">
                                                                <asp:Label ID="lblApeRef3" runat="server" CssClass="TextoEtiqueta" Text="Apellidos:"></asp:Label>
                                                            </td>
                                                            <td style="width: 32%;">
                                                                <asp:TextBox ID="txtApeRef3" runat="server" CssClass="txtCajaTexto" MaxLength="55" placeholder="Apellidos del Referente"></asp:TextBox>
                                                            </td>
                                                            <td style="width:2%; padding: 0px 8px 0px 7px;" class="requerido"">
                                                                <asp:Image ID="Image23" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvApeRef3" runat="server" ControlToValidate="txtApeRef3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Apellidos del Referente" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblInstitucionRef3" runat="server" Text="Institución:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtInstitucionRef3" runat="server" CssClass="txtCajaTexto" MaxLength="55"></asp:TextBox>
                                                            </td>
                                                            <td style="width:2%; padding: 2px 8px 0px 7px;" class="requerido"">
                                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="rfvInstitucionRef3" runat="server" ControlToValidate="txtInstitucionRef3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese institución" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblCargoRef3" runat="server" CssClass="TextoEtiqueta" Text="Cargo:"></asp:Label>
                                                            </td>
                                                            <td style="width: 20%; text-align: left;">
                                                                <asp:TextBox ID="txtCargoRef3" runat="server" CssClass="txtCajaTexto" MaxLength="40"></asp:TextBox>
                                                            </td>
                                                            <td style="width:2%; padding: 2px 8px 0px 7px;" class="requerido">
                                                                <asp:Image ID="Image24" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvCargoRef3" runat="server" ControlToValidate="txtCargoRef3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese cargo" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 1px;"></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                               <%-- <asp:Button ID="btnQuitarRef4" runat="server" Text="Quitar Referencia" CssClass="btnQuitar" OnClick="btnQuitarRef4_Click" ToolTip="Quitar referencia" />
                                                <asp:Button ID="btnAgregaRef4" runat="server" Text="Agregar Referencia" CssClass="btnAgregar" OnClick="btnAgregaRef4_Click" ToolTip="Agregar referencia" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlRef4" runat="server">
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td colspan="10" class="SubTitulo" style="text-align: left;">
                                                                <asp:Label ID="lblSubTituloReferencia4" runat="server" Text="Referencia Nro. 4"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 15%;">
                                                                <asp:Label ID="lblEmailRef4" runat="server" Text="Correo electrónico:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 32%; padding-top: 18px;" class="td_txt_Email">
                                                                <asp:TextBox ID="txtEmailRef4" runat="server" CssClass="txtCajaTexto" MaxLength="50" placeholder="Ejemplo: ejemplo@correo.com"></asp:TextBox>
                                                            </td>
                                                            <%--<td style="width: 2%; padding: 15px 8px 0px 7px;" class="requerido">
                                                                <asp:ImageButton ID="imgBtnBuscaEmailRef4" runat="server" Height="16px" ImageUrl="~/Images/Buttons/btnSearch.png" OnClick="imgBtnBuscaEmailRef4_Click" ToolTip="Buscar Referente por su Email" Width="16px" />
                                                            </td>--%>
                                                            <td style="padding: 0px 8px 0px 7px; width: 2%;" class="requerido">
                                                                <asp:Image ID="Image13" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvEmailRef4" runat="server" ControlToValidate="txtEmailRef4" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre del Referente" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 2%;"></td>
                                                            <td style="width: 13%;"></td>
                                                            <td style="width: 32%;"></td>
                                                            <td style="width: 2%;"></td>
                                                            <td style="width: 2%;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 15%;">
                                                                <asp:Label ID="lblNomRef4" runat="server" Text="Nombres:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 32%;">
                                                                <asp:TextBox ID="txtNomRef4" runat="server" CssClass="txtCajaTexto" MaxLength="55" placeholder="Nombre del Referente"></asp:TextBox>
                                                            </td>
                                                            <td  style="width:2%; padding: 0px 8px 0px 7px;" class="requerido">
                                                                <asp:Image ID="Image14" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvNomRef4" runat="server" ControlToValidate="txtNomRef4" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre del Referente" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 2%;"></td>
                                                            <td style="width: 13%;">
                                                                <asp:Label ID="lblApeRef4" runat="server" CssClass="TextoEtiqueta" Text="Apellidos:"></asp:Label>
                                                            </td>
                                                            <td style="width: 32%;">
                                                                <asp:TextBox ID="txtApeRef4" runat="server" CssClass="txtCajaTexto" MaxLength="55" placeholder="Apellidos del Referente"></asp:TextBox>
                                                            </td>
                                                            <td  style="width:2%; padding: 0px 8px 0px 7px;" class="requerido">
                                                                <asp:Image ID="Image21" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvApeRef4" runat="server" ControlToValidate="txtApeRef4" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Apellidos del Referente" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblInstitucionRef4" runat="server" Text="Institución:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtInstitucionRef4" runat="server" CssClass="txtCajaTexto" MaxLength="55"></asp:TextBox>
                                                            </td>
                                                            <td  style="width:2%; padding: 2px 8px 0px 7px;" class="requerido">
                                                                <asp:Image ID="Image15" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="rfvInstitucionRef4" runat="server" ControlToValidate="txtInstitucionRef4" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese institución" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblCargoRef4" runat="server" CssClass="TextoEtiqueta" Text="Cargo:"></asp:Label>
                                                            </td>
                                                            <td style="width: 20%; text-align: left;">
                                                                <asp:TextBox ID="txtCargoRef4" runat="server" CssClass="txtCajaTexto" MaxLength="40"></asp:TextBox>
                                                            </td>
                                                            <td  style="width:2%; padding: 2px 8px 0px 7px;" class="requerido">
                                                                <asp:Image ID="Image22" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvCargoRef4" runat="server" ControlToValidate="txtCargoRef4" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese cargo" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <%--<asp:Button ID="btnQuitarRef5" runat="server" Text="Quitar Referencia" CssClass="btnQuitar" OnClick="btnQuitarRef5_Click" ToolTip="Quitar referencia" />
                                                <asp:Button ID="btnAgregaRef5" runat="server" Text="Agregar Referencia" CssClass="btnAgregar" OnClick="btnAgregaRef5_Click" ToolTip="Agregar referencia" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlRef5" runat="server">
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td colspan="10" class="SubTitulo" style="text-align: left;">
                                                                <asp:Label ID="lblSubTituloReferencia5" runat="server" Text="Referencia Nro. 5"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 15%;">
                                                                <asp:Label ID="lblEmailRef5" runat="server" Text="Correo electrónico:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 32%; padding-top: 18px;" class="td_txt_Email">
                                                                <asp:TextBox ID="txtEmailRef5" runat="server" CssClass="txtCajaTexto" MaxLength="50" placeholder="Ejemplo: ejemplo@correo.com"></asp:TextBox>
                                                            </td>
                                                            <%--<td style="width: 2%; padding: 15px 8px 0px 7px;" class="requerido">
                                                                <asp:ImageButton ID="imgBtnBuscaEmailRef5" runat="server" Height="16px" ImageUrl="~/Images/Buttons/btnSearch.png" OnClick="imgBtnBuscaEmailRef5_Click" ToolTip="Buscar Referente por su Email" Width="16px" />
                                                            </td>--%>
                                                            <td style="padding: 0px 8px 0px 7px; width: 2%;" class="requerido">
                                                                <asp:Image ID="Image16" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvEmailRef5" runat="server" ControlToValidate="txtEmailRef5" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese email" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 2%;"></td>
                                                            <td style="width: 13%;"></td>
                                                            <td style="width: 32%;"></td>
                                                            <td style="width: 2%;"></td>
                                                            <td style="width: 2%;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 2%;"></td>
                                                            <td style="text-align: left; width: 15%;">
                                                                <asp:Label ID="lblNomRef5" runat="server" Text="Nombres:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 32%;">
                                                                <asp:TextBox ID="txtNomRef5" runat="server" CssClass="txtCajaTexto" MaxLength="55" placeholder="Nombre del Referente"></asp:TextBox>
                                                            </td>
                                                            <td  style="width:2%; padding: 0px 8px 0px 7px;" class="requerido">
                                                                <asp:Image ID="Image17" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvNomRef5" runat="server" ControlToValidate="txtNomRef5" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre del Referente" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 2%;"></td>
                                                            <td style="width: 13%;">
                                                                <asp:Label ID="lblApeRef5" runat="server" CssClass="TextoEtiqueta" Text="Apellidos:"></asp:Label>
                                                            </td>
                                                            <td style="width: 32%;">
                                                                <asp:TextBox ID="txtApeRef5" runat="server" CssClass="txtCajaTexto" MaxLength="55" placeholder="Apellidos del Referente"></asp:TextBox>
                                                            </td>
                                                            <td  style="width:2%; padding: 0px 8px 0px 7px;" class="requerido">
                                                                <asp:Image ID="Image19" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvApeRef5" runat="server" ControlToValidate="txtApeRef5" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Apellidos del Referente" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblInstitucionRef5" runat="server" Text="Institución:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtInstitucionRef5" runat="server" CssClass="txtCajaTexto" MaxLength="55"></asp:TextBox>
                                                            </td>
                                                            <td  style="width:2%; padding: 0px 8px 0px 7px;" class="requerido">
                                                                <asp:Image ID="Image18" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="rfvInstitucionRef5" runat="server" ControlToValidate="txtInstitucionRef5" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese institución" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblCargoRef5" runat="server" CssClass="TextoEtiqueta" Text="Cargo:"></asp:Label>
                                                            </td>
                                                            <td style="width: 20%; text-align: left;">
                                                                <asp:TextBox ID="txtCargoRef5" runat="server" CssClass="txtCajaTexto" MaxLength="40"></asp:TextBox>
                                                            </td>
                                                            <td  style="width:2%; padding: 0px 8px 0px 7px;" class="requerido">
                                                                <asp:Image ID="Image20" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 2%;">
                                                                <asp:RequiredFieldValidator ID="rfvCargoRef5" runat="server" ControlToValidate="txtCargoRef5" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese cargo" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraReferencias"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
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
                            <tr>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label3" runat="server" CssClass="tdTextNormal" Text="("></asp:Label>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                    <asp:Label ID="Label4" runat="server" CssClass="tdTextNormal" Text=")  "></asp:Label>
                                    <asp:Label ID="Label5" runat="server" CssClass="tdTextNormal" Text="Campos Obligatorios"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="controles" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/Images/Buttons/btnPrev.png" ToolTip="Página anterior" OnClick="imgBtnBack_Click" />
                                            </td>
                                            <td></td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" ValidationGroup="registraReferencias" />
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
                                            <asp:Label runat="server" ID="lblmessage" CssClass="tdTextNormalAlerta"></asp:Label></p>
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
            UrlGetStepsAdmision: 'frm10_InfoReferencias.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
</body>
</html>
