<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm11_EstudiosUniversitarios.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm11_EstudiosUniversitarios" Culture="es-PE" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
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
            $("#txtNomUniversidad1").autocomplete({
                source: function (request, response) {

                    PageMethods.getUniversidades(request.term,
                                function (data) {
                                    var universidades = (typeof data) == 'string' ? eval('(' + data + ')') : data;

                                    response($.map(universidades, function (item) {
                                        return {
                                            label: item.NombreInstitucion,
                                            value: item.NombreInstitucion,
                                            id: item.IdEducacion,
                                            IdEducacion: item.IdEducacion,
                                            NombreInstitucion: item.NombreInstitucion,
                                            Direccion: item.Direccion,
                                            CiudadInstitucion: item.CiudadInstitucion,
                                            DepartamentoDes: item.DepartamentoDes,
                                            ModCode: item.ModCode
                                        }
                                    }))
                                }
                                );
                },
                minLength: 0,
                delay: 200,
                select: function (event, ui) {
                    $("#txtIDUniversidad").val(ui.item.IdEducacion);
                    $("#txtDireccionUniversidad1").val(ui.item.Direccion + " - " + ui.item.CiudadInstitucion + " - " + ui.item.DepartamentoDes);
                    $('#txtNomUniversidad1').validationEngine('hide');
                    $("#txtCodigoModular1").val(ui.item.ModCode);
                    $("#txtCodUniversidad1").val(ui.item.IdEducacion);
                },
                change: function (event, ui) { $('#txtNomUniversidad1').validationEngine('hide'); }
            });
        });

        $(function () {
            $("#txtNomUniversidad2").autocomplete({
                source: function (request, response) {

                    PageMethods.getUniversidades(request.term,
                                function (data) {
                                    var universidades = (typeof data) == 'string' ? eval('(' + data + ')') : data;

                                    response($.map(universidades, function (item) {
                                        return {
                                            label: item.NombreInstitucion,
                                            value: item.NombreInstitucion,
                                            id: item.IdEducacion,
                                            IdEducacion: item.IdEducacion,
                                            NombreInstitucion: item.NombreInstitucion,
                                            Direccion: item.Direccion,
                                            CiudadInstitucion: item.CiudadInstitucion,
                                            DepartamentoDes: item.DepartamentoDes,
                                            ModCode: item.ModCode
                                        }
                                    }))
                                }
                                );
                },
                minLength: 0,
                delay: 200,
                select: function (event, ui) {
                    $("#txtIDUniversidad").val(ui.item.IdEducacion);
                    $("#txtDireccionUniversidad2").val(ui.item.Direccion + " - " + ui.item.CiudadInstitucion + " - " + ui.item.DepartamentoDes);
                    $('#txtNomUniversidad2').validationEngine('hide');
                    $("#txtCodigoModular2").val(ui.item.ModCode);
                    $("#txtCodUniversidad2").val(ui.item.IdEducacion);
                },
                change: function (event, ui) { $('#txtUniversidad2').validationEngine('hide'); }
            });
        });

        $(function () {
            $("#txtNomUniversidad3").autocomplete({
                source: function (request, response) {

                    PageMethods.getUniversidades(request.term,
                                function (data) {
                                    var universidades = (typeof data) == 'string' ? eval('(' + data + ')') : data;

                                    response($.map(universidades, function (item) {
                                        return {
                                            label: item.NombreInstitucion,
                                            value: item.NombreInstitucion,
                                            id: item.IdEducacion,
                                            IdEducacion: item.IdEducacion,
                                            NombreInstitucion: item.NombreInstitucion,
                                            Direccion: item.Direccion,
                                            CiudadInstitucion: item.CiudadInstitucion,
                                            DepartamentoDes: item.DepartamentoDes,
                                            ModCode: item.ModCode
                                        }
                                    }))
                                }
                                );
                },
                minLength: 0,
                delay: 200,
                select: function (event, ui) {
                    $("#txtIDUniversidad").val(ui.item.IdEducacion);
                    $("#txtDireccionUniversidad3").val(ui.item.Direccion + " - " + ui.item.CiudadInstitucion + " - " + ui.item.DepartamentoDes);
                    $('#txtNomUniversidad3').validationEngine('hide');
                    $("#txtCodigoModular3").val(ui.item.ModCode);
                    $("#txtCodUniversidad3").val(ui.item.IdEducacion);
                },
                change: function (event, ui) { $('#txtUniversidad3').validationEngine('hide'); }
            });
        });

        //function fnLlamadaError(excepcion) {
        //    alert('Ha ocurrido un error interno: ' + excepcion.get_message());
        //}
    </script>
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
                        <table style="width: 100%;" class="tablaFrame">
                            <tr>
                                <td>
                                    <table style="width: 100%;" class="tablaInterna">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="tablaFrame">
                                                    <tr>
                                                        <td colspan="4" class="SubTitulo">
                                                            <table style="width: 100%;" class="tablaInterna">
                                                                <tr>
                                                                    <td style="width: 70%;">
                                                                        <asp:Label ID="lblSubTitulo" runat="server" Text="Universidad 1"></asp:Label>
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
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="lblBuscarUniversidad" runat="server" Text="Universidad:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="padding-top: 10px;">
                                                            <asp:TextBox ID="txtNomUniversidad1" runat="server" CssClass="txtCajaTexto" placeholder="Ejemplo: Pacifico, Lima" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;" class="requerido">
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 1%;">
                                                            <asp:RequiredFieldValidator ID="rfvUniversidad" runat="server" ControlToValidate="txtNomUniversidad1" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Universidad." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDireccionUniversidad" runat="server" Text="Dirección:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDireccionUniversidad1" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCarrera" runat="server" CssClass="TextoEtiqueta" Text="Carrera:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCarreraUniversidad1" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDesde" runat="server" CssClass="TextoEtiqueta" Text="Año Desde:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table class="tablaInterna">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAnioDesdeUniversidad1" runat="server" CssClass="txtCajaTexto" MaxLength="4" Width="120px"></asp:TextBox>
                                                                        <ajax:MaskedEditExtender ID="meeN2AnhoDesdeUni1" runat="server" CultureName="es-PE" Mask="9999" MaskType="Number" TargetControlID="txtAnioDesdeUniversidad1"></ajax:MaskedEditExtender>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td style="width: 80px;">
                                                                        <asp:Label ID="lblHasta" runat="server" CssClass="TextoEtiqueta" Text="Hasta:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAnioHastaUniversidad1" runat="server" CssClass="txtCajaTexto" MaxLength="4" Width="120px"></asp:TextBox>
                                                                        <ajax:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="es-PE" Mask="9999" MaskType="Number" TargetControlID="txtAnioHastaUniversidad1"></ajax:MaskedEditExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <% if (Session["ModPostulacion"].ToString() != "43" && Session["ModPostulacion"].ToString() != "44" && Session["ModPostulacion"].ToString() != "45")
                                                       { %>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblNroCreditos" runat="server" CssClass="TextoEtiqueta" Text="Nro de Creditos Cursados:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table class="tablaInterna">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNroCreditosCursadosUni1" runat="server" CssClass="txtCajaTexto" MaxLength="3" Width="120px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Image ID="Image22" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                    </td>
                                                                    <td class="requerido">
                                                                        <asp:RequiredFieldValidator ID="rfvNumCred1" runat="server" ControlToValidate="txtNroCreditosCursadosUni1"
                                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Nro de Creditos." InitialValue="" SetFocusOnError="true"
                                                                            Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td style="width: 80px;">
                                                                        <asp:Label ID="lblAprobados" runat="server" CssClass="TextoEtiqueta" Text="Aprobados:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNroCreditosAprobadosUni1" runat="server" CssClass="txtCajaTexto" MaxLength="3" Width="120px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Image ID="Image23" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                    </td>
                                                                    <td class="requerido">
                                                                        <asp:RequiredFieldValidator ID="rfvCredAprob1" runat="server" ControlToValidate="txtNroCreditosAprobadosUni1"
                                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Creditos Aprobados." InitialValue="" SetFocusOnError="true"
                                                                            Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <%
                                                       } %>
                                                    <% if (Session["ModPostulacion"].ToString() == "43" || Session["ModPostulacion"].ToString() == "44" || Session["ModPostulacion"].ToString() == "45")
                                                       { %>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblGrados" runat="server" CssClass="TextoEtiqueta" Text="Grado:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlGradoUniversidad1" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image19" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <div style="display:none">
                                                                <asp:TextBox ID="txtCodUniversidad1" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtCodUniversidad2" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtCodUniversidad3" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdEducacion1" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdEducacion2" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdEducacion3" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdDetalleEducacion1" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdDetalleEducacion2" runat="server" Width="100px"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdDetalleEducacion3" runat="server" Width="100px"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvGrado1" runat="server" ControlToValidate="ddlGradoUniversidad1" CssClass="MsgAlertaIncompleto"
                                                                ErrorMessage="Elija el grado." InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <%} %>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Button ID="btnQuitarUniversidad2" runat="server" Text="Quitar Universidad" CssClass="btnQuitar" OnClick="btnQuitarUniversidad2_Click" ToolTip="Quitar Universidad" />
                                                <asp:Button ID="BtnAgregaUniversidad2" runat="server" Text="Agregar Universidad" CssClass="btnAgregar" OnClick="BtnAgregaUniversidad2_Click" ToolTip="Agregar Universidad" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlUniversidad2" runat="server">
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td colspan="4" class="SubTitulo">
                                                                <asp:Label ID="lblSubTitulo2" runat="server" Text="Universidad 2"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblBuscaUniversidad2" runat="server" Text="Universidad:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtNomUniversidad2" runat="server" CssClass="txtCajaTexto" placeholder="Ejemplo: Pacifico, Lima"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 1%;">
                                                                <asp:RequiredFieldValidator ID="rfvUni2" runat="server" ControlToValidate="txtNomUniversidad2" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Universidad." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDireccionUniversidad2" runat="server" Text="Dirección:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDireccionUniversidad2" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="100" Width="95%"></asp:TextBox>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCarrera2" runat="server" CssClass="TextoEtiqueta" Text="Carrera:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCarreraUniversidad2" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDesde2" runat="server" CssClass="TextoEtiqueta" Text="Año Desde:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table class="tablaInterna">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAnioDesdeUniversidad2" runat="server" CssClass="txtCajaTexto" MaxLength="20" Width="120px"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td style="width: 80px;">
                                                                            <asp:Label ID="lblHasta2" runat="server" CssClass="TextoEtiqueta" Text="Hasta:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAnioHastaUniversidad2" runat="server" CssClass="txtCajaTexto" MaxLength="20" Width="120px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <% if (Session["ModPostulacion"].ToString() != "43" && Session["ModPostulacion"].ToString() != "44" && Session["ModPostulacion"].ToString() != "45")
                                                           { %>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNroCreditos2" runat="server" CssClass="TextoEtiqueta" Text="Nro de Creditos Cursados:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table class="tablaInterna">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNroCreditosCursadosUni2" runat="server" CssClass="txtCajaTexto" MaxLength="20" Width="120px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="Image24" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                        </td>
                                                                        <td class="requerido">
                                                                            <asp:RequiredFieldValidator ID="rfvNumCred2" runat="server" ControlToValidate="txtNroCreditosCursadosUni2" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Nro de Creditos." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td style="width: 80px;">
                                                                            <asp:Label ID="lblAprobados2" runat="server" CssClass="TextoEtiqueta" Text="Aprobados:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNroCreditosAprobadosUni2" runat="server" CssClass="txtCajaTexto" MaxLength="20" Width="120px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="Image25" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                        </td>
                                                                        <td class="requerido">
                                                                            <asp:RequiredFieldValidator ID="rfvCredAprob2" runat="server" ControlToValidate="txtNroCreditosAprobadosUni2" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Creditos Aprobados." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <%} %>
                                                        <% if (Session["ModPostulacion"].ToString() == "43" || Session["ModPostulacion"].ToString() == "44" || Session["ModPostulacion"].ToString() == "45")
                                                           { %>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblGrado2" runat="server" CssClass="TextoEtiqueta" Text="Grado:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlGradoUniversidad2" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Image ID="Image20" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="rfvGrado2" runat="server" ControlToValidate="ddlGradoUniversidad2" CssClass="MsgAlertaIncompleto"
                                                                    ErrorMessage="Elija el grado." InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <% } %>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Button ID="btnQuitarUniversidad3" runat="server" Text="Quitar Universidad" CssClass="btnQuitar" OnClick="btnQuitarUniversidad3_Click" ToolTip="Quitar Universidad" />
                                                <asp:Button ID="BtnAgregaUniversidad3" runat="server" Text="Agregar Universidad" CssClass="btnAgregar" OnClick="BtnAgregaUniversidad3_Click" ToolTip="Agregar Universidad" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlUniversidad3" runat="server">
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td colspan="4" class="SubTitulo">
                                                                <asp:Label ID="lblSubTitulo3" runat="server" Text="Universidad 3"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblBuscaUniversidad3" runat="server" Text="Universidad:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtNomUniversidad3" runat="server" CssClass="txtCajaTexto" placeholder="Ejemplo: Pacifico, Lima"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 1%;">
                                                                <asp:RequiredFieldValidator ID="rfvUni3" runat="server" ControlToValidate="txtNomUniversidad3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Universidad." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDireccionUniversidad3" runat="server" Text="Dirección:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDireccionUniversidad3" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="100"></asp:TextBox>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCarrera3" runat="server" CssClass="TextoEtiqueta" Text="Carrera:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCarreraUniversidad3" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDesde3" runat="server" CssClass="TextoEtiqueta" Text="Año Desde:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table class="tablaInterna">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAnioDesdeUniversidad3" runat="server" CssClass="txtCajaTexto" MaxLength="20" Width="120px"></asp:TextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td style="width: 80px;">
                                                                            <asp:Label ID="lblHasta3" runat="server" CssClass="TextoEtiqueta" Text="Hasta:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAnioHastaUniversidad3" runat="server" CssClass="txtCajaTexto" MaxLength="20" Width="120px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <% if (Session["ModPostulacion"].ToString() != "43" && Session["ModPostulacion"].ToString() != "44" && Session["ModPostulacion"].ToString() != "45")
                                                           { %>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNroCreditos3" runat="server" CssClass="TextoEtiqueta" Text="Nro de Creditos Cursados:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table class="tablaInterna">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNroCreditosCursadosUni3" runat="server" CssClass="txtCajaTexto" MaxLength="20" Width="120px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="Image26" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                        </td>
                                                                        <td class="requerido">
                                                                            <asp:RequiredFieldValidator ID="rfvNumCred3" runat="server" ControlToValidate="txtNroCreditosCursadosUni3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Nro de Creditos." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td style="width: 80px;">
                                                                            <asp:Label ID="lblAprobados3" runat="server" CssClass="TextoEtiqueta" Text="Aprobados:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNroCreditosAprobadosUni3" runat="server" CssClass="txtCajaTexto" MaxLength="20" Width="120px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="Image27" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:RequiredFieldValidator ID="rfvCredAprob3" runat="server" ControlToValidate="txtNroCreditosAprobadosUni3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese Creditos Aprobados." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <%} %>
                                                        <% if (Session["ModPostulacion"].ToString() == "43" || Session["ModPostulacion"].ToString() == "44" || Session["ModPostulacion"].ToString() == "45")
                                                           { %>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblGrado3" runat="server" CssClass="TextoEtiqueta" Text="Grado:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlGradoUniversidad3" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Image ID="Image21" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="rfvGrado3" runat="server" ControlToValidate="ddlGradoUniversidad3" CssClass="MsgAlertaIncompleto"
                                                                    ErrorMessage="Elija el grado." InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraUniversidad"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <% } %>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label4" runat="server" CssClass="tdTextNormal" Text="("></asp:Label>
                                    <asp:Image ID="Image18" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                    <asp:Label ID="Label5" runat="server" CssClass="tdTextNormal" Text=")  "></asp:Label>
                                    <asp:Label ID="Label6" runat="server" CssClass="tdTextNormal" Text="Campos Obligatorios"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="controles tablaInterna" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/Images/Buttons/btnPrev.png" ToolTip="Página anterior" OnClick="imgBtnBack_Click" />
                                            </td>
                                            <td></td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click"
                                                    ValidationGroup="registraUniversidad" />
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
            UrlGetStepsAdmision: 'frm11_EstudiosUniversitarios.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
</body>
</html>
