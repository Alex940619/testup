<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm05_ColegioProcede.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm05_ColegioProcede" Culture="es-PE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
            $("#txtColegio1").autocomplete({
                source: function (request, response) {

                    PageMethods.getColegios(request.term,
                                function (data) {
                                    var colegios = (typeof data) == 'string' ? eval('(' + data + ')') : data;

                                    response($.map(colegios, function (item) {
                                        return {
                                            /*Ini: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
                                            label: item.NombreInstitucion.toUpperCase(),
                                            value: item.NombreInstitucion.toUpperCase(),
                                            id: item.IdEducacion,
                                            IdEducacion: item.IdEducacion,
                                            NombreInstitucion: item.NombreInstitucion.toUpperCase(),
                                            Direccion: item.Direccion.toUpperCase(),
                                            CiudadInstitucion: item.CiudadInstitucion.toUpperCase(),
                                            DepartamentoDes: item.DepartamentoDes.toUpperCase(),
                                            ModCode: item.ModCode
                                            /*Fin: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
                                        }
                                    }))
                                } //,
                                //fnLlamadaError
                                );
                },
                minLength: 0,
                delay: 0,
                select: function (event, ui) {
                    $("#txtIDColegio").val(ui.item.IdEducacion);
                    $('#txtCodModCol1').val(ui.item.ModCode);
                    $("#txtDireccionColegio1").val(ui.item.Direccion + " - " + ui.item.CiudadInstitucion + " - " + ui.item.DepartamentoDes);
                    $('#txtColegio1').validationEngine('hide');
                    $("#txtCodigoModular1").val(ui.item.ModCode);
                    $("#txtCodColegio1").val(ui.item.IdEducacion);
                    $("#rfvColegio").css('visibility', 'hidden');
                },
                change: function (event, ui) { $('#txtColegio1').validationEngine('hide'); }
            });
        });
        
        $(function () {
            $("#txtColegio2").autocomplete({
                source: function (request, response) {

                    PageMethods.getColegios(request.term,
                                function (data) {
                                    var colegios = (typeof data) == 'string' ? eval('(' + data + ')') : data;

                                    response($.map(colegios, function (item) {
                                        return {
                                            /*Ini: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
                                            label: item.NombreInstitucion.toUpperCase(),
                                            value: item.NombreInstitucion.toUpperCase(),
                                            id: item.IdEducacion,
                                            IdEducacion: item.IdEducacion,
                                            NombreInstitucion: item.NombreInstitucion.toUpperCase(),
                                            Direccion: item.Direccion.toUpperCase(),
                                            CiudadInstitucion: item.CiudadInstitucion.toUpperCase(),
                                            DepartamentoDes: item.DepartamentoDes.toUpperCase(),
                                            ModCode: item.ModCode
                                            /*Fin: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
                                        }
                                    }))
                                }
                                //,
                                //fnLlamadaError
                                );
                },
                minLength: 0,
                delay: 0,
                select: function (event, ui) {
                    $("#txtIDColegio").val(ui.item.IdEducacion);
                    $('#txtCodModCol2').val(ui.item.ModCode);
                    $("#txtDireccionColegio2").val(ui.item.Direccion + " - " + ui.item.CiudadInstitucion + " - " + ui.item.DepartamentoDes);
                    $('#txtColegio2').validationEngine('hide');
                    $("#txtCodigoModular2").val(ui.item.ModCode);
                    $("#txtCodColegio2").val(ui.item.IdEducacion);
                    $("#rfvColegio2").css('visibility', 'hidden');
                },
                change: function (event, ui) { $('#txtColegio2').validationEngine('hide'); }
            });
        });

        $(function () {
            $("#txtColegio3").autocomplete({
                source: function (request, response) {

                    PageMethods.getColegios(request.term,
                                function (data) {
                                    var colegios = (typeof data) == 'string' ? eval('(' + data + ')') : data;

                                    response($.map(colegios, function (item) {
                                        return {
                                            /*Ini: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
                                            label: item.NombreInstitucion.toUpperCase(),
                                            value: item.NombreInstitucion.toUpperCase(),
                                            id: item.IdEducacion,
                                            IdEducacion: item.IdEducacion,
                                            NombreInstitucion: item.NombreInstitucion.toUpperCase(),
                                            Direccion: item.Direccion.toUpperCase(),
                                            CiudadInstitucion: item.CiudadInstitucion.toUpperCase(),
                                            DepartamentoDes: item.DepartamentoDes.toUpperCase(),
                                            ModCode: item.ModCode
                                            /*Fin: Christian Ramirez - GIIT [Caso 45804] - 20180626*/
                                        }
                                    }))
                                }
                                //,
                                //fnLlamadaError
                                );
                },
                minLength: 0,
                delay: 0,
                select: function (event, ui) {
                    $("#txtIDColegio").val(ui.item.IdEducacion);
                    $('#txtCodModCol3').val(ui.item.ModCode);
                    $("#txtDireccionColegio3").val(ui.item.Direccion + " - " + ui.item.CiudadInstitucion + " - " + ui.item.DepartamentoDes);
                    $('#txtColegio3').validationEngine('hide');
                    $("#txtCodigoModular3").val(ui.item.ModCode);
                    $("#txtCodColegio3").val(ui.item.IdEducacion);
                    $("#rfvColegio3").css('visibility', 'hidden');
                },
                change: function (event, ui) { $('#txtColegio3').validationEngine('hide'); }
            });

            $('.txtSensitive').change(function () {
                var id = 0;
                if ($(this).attr('id') == 'txtColegio1') {
                    if ($(this).val() == '') {
                        $('#txtCodColegio1').val('');
                        $('#txtCodigoModular1').val('');
                        $('#txtDireccionColegio1').val('');
                    }
                } else if ($(this).attr('id') == 'txtColegio2') {
                    if ($(this).val() == '') {
                        $('#txtCodColegio2').val('');
                        $('#txtCodigoModular2').val('');
                        $('#txtDireccionColegio2').val('');
                    }
                } else if ($(this).attr('id') == 'txtColegio3') {
                    if ($(this).val() == '') {
                        $('#txtCodColegio3').val('');
                        $('#txtCodigoModular3').val('');
                        $('#txtDireccionColegio3').val('');
                    }
                }
            });
        });

        
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
                                <asp:Label ID="lblPantallaAnterior" runat="server" Text="Datos Personales"></asp:Label>
                            </td>
                            <td class="CabeceraSeleccionada" style="width: 33%;">
                                <asp:Label ID="lblPantallaVigente" runat="server" Text="Colegio de Procedencia"></asp:Label>
                            </td>
                            <td class="CabeceraNoSeleccionada" style="width: 33%;">
                                <asp:Label ID="lblPantallaSiguiente" runat="server" Text="Rendimiento Académico"></asp:Label>
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
                                <td class="SubTitulo tablaInterna ">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 70%;">
                                                <asp:Label ID="lblTitulo" runat="server" Text="Información de Colegio de Procedencia"></asp:Label>
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
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="tablaFrame">
                                                    <tr>
                                                        <td style="width: 30%;">
                                                            <asp:Label ID="lblEgresado" runat="server" Text="Situación Académica:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 67%; text-align: left; padding-top: 5px;">
                                                            <asp:DropDownList ID="ddlSitAcademica" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                                        </td>
                                                        <td style="width: 1%;" class="requerido">
                                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 1%;">
                                                            <asp:RequiredFieldValidator ID="rfvEgresado" runat="server" ControlToValidate="ddlSitAcademica"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Elija su situación académica" InitialValue="0"
                                                                SetFocusOnError="true" ValidationGroup="registraColegio" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%;">
                                                            <asp:Label ID="lblPrimNom8" runat="server" Text="¿En cuántos colegios estudiaste la secundaria?:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 20%; text-align: left; padding-top: 5px;">
                                                            <asp:DropDownList ID="ddlNroColegios" runat="server" CssClass="txtTextoCombo" OnSelectedIndexChanged="ddlNroColegios_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">-- Seleccionar --</asp:ListItem>
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 1%;" class="requerido">
                                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 1%;">
                                                            <asp:RequiredFieldValidator ID="rfvNroColegios" runat="server" ControlToValidate="ddlNroColegios" CssClass="MsgAlertaIncompleto" ErrorMessage="Elija el numero de colegios."
                                                                InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraColegio"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tablaFrameDesc">
                                                <div class="box-icon">
                                                    <i class="fa fa-info-circle"></i>
                                                </div>
                                                <div class="box-content">
                                                    <asp:Label ID="lblExplicacion1" runat="server" Text="Nota:" CssClass="tdTextoDetalle" Font-Bold="True"></asp:Label><br />
                                                    <asp:Label ID="lblExplicacion" runat="server" Text="La búsqueda del colegio puede demorar unos segundos, por favor, espere a que el sistema le muestre la lista de opciones y luego verifique que los campos de código modular y dirección estén completos. Asimismo, asegúrese de haber seleccionado e insertado correctamente su colegio antes de pasar a la siguiente pestaña.<br><br> El colegio o colegios que selecciones deberán coincidir con la información solicitada en pasos posteriores (libreta de notas o certificado de estudios de 1° a 5° de secundaria)." CssClass="tdTextoDetalle"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlColegio1" runat="server">
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td colspan="4" class="SubTitulo">
                                                                <table style="width: 100%;" class="tablaInterna">
                                                                    <tr>
                                                                        <td style="width: 90%;">
                                                                            <asp:Label ID="lblColegio1" runat="server" Text="Colegio Actual o de Egreso"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="BtnElimina1" runat="server" CssClass="btnQuitar" OnClick="btnElimina1_Click" Text="Quitar Colegio" ToolTip="Eliminar Datos de Colegio" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblBuscarColegio" runat="server" Text="Colegio:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtColegio1" runat="server" CssClass="txtCajaTexto txtSensitive" placeholder="Ejemplo: Trilce Lima" MaxLength="100"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 10%;">
                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="rfvColegio" runat="server" ControlToValidate="txtCodColegio1" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre de colegio." InitialValue="" SetFocusOnError="true" Text="Colegio incorrectamente ingresado. La búsqueda del colegio puede demorar unos segundos, por favor, espere a que el sistema le muestre la lista de opciones y luego verifique que los campos de código modular y dirección estén completos. " ForeColor="Red" ValidationGroup="registraColegio"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCodigoModular" runat="server" Text="Código Modular:" CssClass="TextoEtiqueta" Width="110px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCodigoModular1" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <%--<asp:Image ID="Image21" runat="server" ImageUrl="~/Images/ico_Required.gif" />--%>
                                                            </td>
                                                            <td>
                                                                <%--<asp:RequiredFieldValidator ID="rfvCodigoModular1" runat="server" ControlToValidate="txtCodigoModular1" CssClass="MsgAlertaIncompleto" ErrorMessage="Campo código modular obligatorio." InitialValue="" Text="*" ValidationGroup="registraColegio"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDireccionColegio" runat="server" Text="Dirección:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDireccionColegio1" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="100"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <%--<asp:Image ID="Image22" runat="server" ImageUrl="~/Images/ico_Required.gif" />--%>
                                                            </td>
                                                            <td>
                                                                <div style="display: none;">
                                                                    <asp:TextBox ID="txtCodColegio1" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtCodColegio2" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtCodColegio3" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtIdEducacion1" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtIdEducacion2" runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:TextBox ID="txtIdEducacion3" runat="server" Width="100px"></asp:TextBox>
                                                                </div>
                                                                <%--<asp:RequiredFieldValidator ID="rfvDireccionColegio1" runat="server" ControlToValidate="txtDireccionColegio1" CssClass="MsgAlertaIncompleto" ErrorMessage="Campo dirección obligatorio." InitialValue="" Text="*" ValidationGroup="registraColegio"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td>&nbsp;</td>
                                                            <td style="font-family: Arial; font-size: 13px; color: GrayText; font-weight: 700; height: 25px; text-decoration: underline">
                                                                <asp:LinkButton ID="lnkNuevoColegio" runat="server" OnClick="lnkNuevoColegio_Click">Agregar Nuevo Colegio</asp:LinkButton>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>--%>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlColegio2" runat="server">
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td class="SubTitulo" colspan="4">
                                                                <table style="width: 100%;" class="tablaInterna">
                                                                    <tr>
                                                                        <td style="width: 90%;">
                                                                            <asp:Label ID="lblColegio2" runat="server" Text="Otro Colegio"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%; text-align: right;">
                                                                            <asp:Button ID="BtnElimina2" runat="server" Text="Quitar Colegio" CssClass="btnQuitar" OnClick="btnElimina2_Click" ToolTip="Eliminar Datos de Colegio" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblBuscarColegio2" runat="server" Text="Colegio:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtColegio2" runat="server" CssClass="txtCajaTexto txtSensitive" placeholder="Ejemplo: Trilce Lima"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <asp:Image ID="Image19" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="rfvColegio2" runat="server" ControlToValidate="txtCodColegio2" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre de colegio." InitialValue="" SetFocusOnError="true" Text="Colegio incorrectamente ingresado. La búsqueda del colegio puede demorar unos segundos, por favor, espere a que el sistema le muestre la lista de opciones y luego verifique que los campos de código modular y dirección estén completos." ForeColor="Red" ValidationGroup="registraColegio"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCodigoModular2" runat="server" Text="Código Modular:" CssClass="TextoEtiqueta" Width="110px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCodigoModular2" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <%--<asp:Image ID="Image23" runat="server" ImageUrl="~/Images/ico_Required.gif" />--%>
                                                            </td>
                                                            <td>
                                                                <%--<asp:RequiredFieldValidator ID="rfvCodigoModular2" runat="server" ControlToValidate="txtCodigoModular2" CssClass="MsgAlertaIncompleto" ErrorMessage="Campo código modular obligatorio." InitialValue="" Text="*" ValidationGroup="registraColegio"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDireccionColegio2" runat="server" Text="Dirección:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDireccionColegio2" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="100"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <%--<asp:Image ID="Image24" runat="server" ImageUrl="~/Images/ico_Required.gif" />--%>
                                                            </td>
                                                            <td>
                                                                <%--<asp:RequiredFieldValidator ID="rfvDireccionColegio2" runat="server" ControlToValidate="txtDireccionColegio2" CssClass="MsgAlertaIncompleto" ErrorMessage="Campo dirección obligatorio." InitialValue="" Text="*" ValidationGroup="registraColegio"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlColegio3" runat="server">
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td class="SubTitulo" colspan="4">
                                                                <table style="width: 100%;" class="tablaInterna">
                                                                    <tr>
                                                                        <td style="width: 90%;">
                                                                            <asp:Label ID="lblColegio3" runat="server" Text="Otro Colegio"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%; text-align: right;">
                                                                            <asp:Button ID="btnElimina3" runat="server" Text="Quitar Colegio" CssClass="btnQuitar" OnClick="btnElimina3_Click" ToolTip="Eliminar Datos de Colegio" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lblBuscarColegio3" runat="server" Text="Colegio:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="padding-top: 10px;">
                                                                <asp:TextBox ID="txtColegio3" runat="server" CssClass="txtCajaTexto txtSensitive" placeholder="Ejemplo: Trilce Lima"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <asp:Image ID="Image20" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="rfvColegio3" runat="server" ControlToValidate="txtCodColegio3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre de colegio." InitialValue="" SetFocusOnError="true" Text="Colegio incorrectamente ingresado. La búsqueda del colegio puede demorar unos segundos, por favor, espere a que el sistema le muestre la lista de opciones y luego verifique que los campos de código modular y dirección estén completos. " ForeColor="Red" ValidationGroup="registraColegio"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCodigoModular3" runat="server" Text="Código Modular:" CssClass="TextoEtiqueta" Width="110px"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:TextBox ID="txtCodigoModular3" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <%--<asp:Image ID="Image25" runat="server" ImageUrl="~/Images/ico_Required.gif" />--%>
                                                            </td>
                                                            <td>
                                                                <%--<asp:RequiredFieldValidator ID="rfvCodigoModular3" runat="server" ControlToValidate="txtCodigoModular3" CssClass="MsgAlertaIncompleto" ErrorMessage="Campo código modular obligatorio." InitialValue="" Text="*" ValidationGroup="registraColegio"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDireccionColegio3" runat="server" Text="Dirección:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDireccionColegio3" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="100"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <%--<asp:Image ID="Image26" runat="server" ImageUrl="~/Images/ico_Required.gif" />--%>
                                                            </td>
                                                            <td>
                                                                <%--<asp:RequiredFieldValidator ID="rfvDireccionColegio3" runat="server" ControlToValidate="txtDireccionColegio3" CssClass="MsgAlertaIncompleto" ErrorMessage="Campo dirección obligatorio." InitialValue="" Text="*" ValidationGroup="registraColegio"></asp:RequiredFieldValidator>--%>
                                                            </td>
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
                                    <asp:Label ID="Label4" runat="server" CssClass="tdTextNormal" Text="("></asp:Label>
                                    <asp:Image ID="Image18" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                    <asp:Label ID="Label5" runat="server" CssClass="tdTextNormal" Text=")  "></asp:Label>
                                    <asp:Label ID="Label6" runat="server" CssClass="tdTextNormal" Text="Campos Obligatorios"></asp:Label>
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
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click"
                                                    ValidationGroup="registraColegio" />
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
                    <asp:Button ID="btnMostrarNuevoColegio" Style="display: none;" runat="server" />
                    <asp:Button ID="btnCancelarNuevoColegio" Style="display: none;" runat="server" />
                    <ajax:ModalPopupExtender ID="mpeMostrarNuevoColegio" runat="server" TargetControlID="btnMostrarNuevoColegio"
                        PopupControlID="pnlMostrarNuevoColegio" PopupDragHandleControlID="pnlMostrarNuevoColegio" BackgroundCssClass="modalBackground"
                        CancelControlID="btnCancelarNuevoColegio" Enabled="True" />
                    <asp:Panel ID="pnlMostrarNuevoColegio" runat="server" CssClass="popup-main" Width="70%" Style="display: none;">
                        <asp:UpdatePanel ID="upMostrarNuevoColegioDetalle" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <%--<div class="popup-main">--%>
                                <table style="width: 100%; border: 0px;">
                                    <tr>
                                        <td colspan="4" style="font-family: Arial; font-size: 14px; text-align: justify;">
                                            <b>Estimado interesado</b>, en caso su colegio no se encuentre en la lista de “Colegio de Procedencia” agradeceremos llenar el siguiente formulario a fin de incluirlo en la lista. El proceso de agregación demora un día útil y no podrá continuar su inscripción hasta que  su colegio sea incluido. Cualquier consulta, por favor escribir a admisión@up.edu.pe  luego de llenar el siguiente formulario.<br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%; vertical-align: central;" class="TextoEtiqueta">&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Nombre de Colegio:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNomColegio" runat="server" CssClass="txtCajaTexto"></asp:TextBox></td>
                                        <td style="width: 1%;" class="requerido">
                                            <asp:Image ID="Image27" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                        </td>
                                        <td style="width: 1%;">
                                            <asp:RequiredFieldValidator ID="rfvNomColegio" runat="server" ControlToValidate="txtNomColegio" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre de Colegio." InitialValue="" Text="*" ValidationGroup="enviarCorreo"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;" class="TextoEtiqueta">&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Contacto:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtContacto" runat="server" CssClass="txtCajaTexto"></asp:TextBox></td>
                                        <td class="requerido">
                                            <asp:Image ID="Image28" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvContacto" runat="server" ControlToValidate="txtContacto" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese datos de contacto." InitialValue="" Text="*" ValidationGroup="enviarCorreo"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%;" class="TextoEtiqueta">&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="Distrito:"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TxtDistrito" runat="server" CssClass="txtCajaTexto"></asp:TextBox></td>
                                        <td class="requerido">
                                            <asp:Image ID="Image29" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvDistrito" runat="server" ControlToValidate="TxtDistrito" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese el distrito." InitialValue="" Text="*" ValidationGroup="enviarCorreo"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnEnviar" runat="server" CssClass="btnEnviaCorreo" Text="Enviar" ToolTip="Enviar datos de colegio." OnClick="btnEnviar_Click" ValidationGroup="enviarCorreo" />
                                            <asp:Button ID="Cancelar" runat="server" CssClass="btnCerrar" Text="Cancelar" ToolTip="Cerrar ventana." OnClick="Cancelar_Click" />
                                        </td>
                                        <td align="center">&nbsp;</td>
                                        <td align="center">&nbsp;</td>
                                    </tr>
                                </table>
                                <%--</div>--%>
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
            UrlGetStepsAdmision: 'frm05_ColegioProcede.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
    <script src="../JavaScript/Webforms/frm05_ColegioProcede.js"></script> <%--Se agrega: Christian Ramirez[GIIT] Caso43692 - 20180423--%>
</body>
</html>
