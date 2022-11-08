<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm09_InfoPadres.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm09_InfoPadres" Culture="es-PE" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/Utilitarios.css" rel="stylesheet" /> <%--Se agrega: Christian Ramirez[GIIT] Caso43692 - 20180423--%>
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
            $("#frm09_InfoPadres").validate({
                rules: {
                    txtEmailRelFam1: {
                        required: false,
                        email: true
                    },
                    txtEmailRelFam2: {
                        required: false,
                        email: true
                    },
                    txtEmailRelFam3: {
                        required: false,
                        email: true
                    },
                },
                messages: {
                    txtEmailRelFam1: "Debe ingresar un correo válido.",
                    txtEmailRelFam2: "Debe ingresar un correo válido.",
                    txtEmailRelFam3: "Debe ingresar un correo válido."
                }
            });

        });
    </script>
    <script>

        function validations()
        {
            if (valLongitudDocFam1() == false || 
                valLongitudDocFam2() == false ||
                valLongitudDocFam3() == false ||
                valLongitudTelFam1() == false ||
                valLongitudTelFam2() == false ||
                valLongitudTelFam3() == false)
            {
                return false
            } else {
                return true
            }
        }

        function returnTabIndex() {
            $("#ddlRelFam1").attr("tabindex", "1")
            $("#ddlTipDocRelFam1").attr("tabindex", "2")
            $("#txtNumDocRelFam1").attr("tabindex", "3")
            $("#txtNomRelFam1").attr("tabindex", "4")
            $("#txtApeRelFam1").attr("tabindex", "5")
            $("#txtTelefonoRelFam1").attr("tabindex", "6")
            $("#txtEmailRelFam1").attr("tabindex", "7")
            $("#BtnAgregaPariente2").attr("tabindex", "8")

            return true
        }

        function valLongitudDocFam1() {
            
            if ($("#ddlTipDocRelFam1").val() == "D.N.I.") {
                
                if ($("#txtNumDocRelFam1").val().length != 8) {
                    $("#txtNumDocRelFam1").focus();
                    $("#txtNumDocRelFam1").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                    $(".tdTextNormalAlerta").html("El N° de DNI debe tener 8 dígitos.")
                    $("#btnMostrarError").click()

                    return false
                }
                else {
                    $("#txtNumDocRelFam1").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                    return true
                }
            }
            else {
                if ($("#txtNumDocRelFam1").val().length < 4) {
                    $("#txtNumDocRelFam1").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                    return false
                } else {
                    $("#txtNumDocRelFam1").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                    return true
                }
            }
        };

        function valLongitudDocFam2() {
            if ($("#ddlTipDocRelFam2").val() == "D.N.I.") {
                if ($("#txtNumDocRelFam2").val().length != 8) {
                    $("#txtNumDocRelFam2").focus();
                    $("#txtNumDocRelFam2").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                    $(".tdTextNormalAlerta").html("El N° de DNI debe tener 8 dígitos.")
                    $("#btnMostrarError").click()
                    return false
                }
                else {
                    $("#txtNumDocRelFam2").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                    return true
                }
            }
            else {
                if ($("#txtNumDocRelFam2").val().length < 4) {
                    $("#txtNumDocRelFam2").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                    return false
                } else {
                    $("#txtNumDocRelFam2").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                    return true
                }
            }
        };

        function valLongitudDocFam3() {
            if ($("#ddlTipDocRelFam3").val() == "D.N.I.") {
                if ($("#txtNumDocRelFam3").val().length != 8) {
                    $("#txtNumDocRelFam3").focus();
                    $("#txtNumDocRelFam3").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                    $(".tdTextNormalAlerta").html("El N° de DNI debe tener 8 dígitos.")
                    $("#btnMostrarError").click()
                    return false
                }
                else {
                    $("#txtNumDocRelFam3").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                    return true
                }
            }
            else {
                if ($("#txtNumDocRelFam3").val().length < 4) {
                    $("#txtNumDocRelFam3").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                    return false
                } else {
                    $("#txtNumDocRelFam3").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                    return true
                }
            }
        };

        function valLongitudTelFam1() {
            if ($("#txtTelefonoRelFam1").val().length < 6) {
                $("#txtTelefonoRelFam1").focus();
                $("#txtTelefonoRelFam1").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                $(".tdTextNormalAlerta").html("El N° de teléfono debe tener 6 dígitos como mínimo.")
                $("#btnMostrarError").click()
                return false
            } else {
                $("#txtTelefonoRelFam1").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                return true
            }
        };

        function valLongitudTelFam2() {
            if ($("#txtTelefonoRelFam2").val().length < 6) {
                $("#txtTelefonoRelFam2").focus();
                $("#txtTelefonoRelFam2").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                $(".tdTextNormalAlerta").html("El N° de teléfono debe tener 6 dígitos como mínimo.")
                $("#btnMostrarError").click()
                return false
            } else {
                $("#txtTelefonoRelFam2").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                return true
            }
        };

        function valLongitudTelFam3() {
            if ($("#txtTelefonoRelFam3").val().length < 6) {
                $("#txtTelefonoRelFam3").focus();
                $("#txtTelefonoRelFam3").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                $(".tdTextNormalAlerta").html("El N° de teléfono debe tener 6 dígitos como mínimo.")
                $("#btnMostrarError").click()
                return false
            } else {
                $("#txtTelefonoRelFam3").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                return true
            }
        };
    </script>
    <script>
        $(function () {
            $(".transform-capitalize").blur(function () {
                var $valor = $(this).val()
                var cad = null
                if ($valor.length > 1) {
                    cad = $valor.substr(0, 1).toUpperCase()
                    $(this).val(cad + $valor.substr(1, $valor.length - 1))
                }
            })
        })
    </script>
</head>
<body>
    <div id="contentStepsAdmision"></div>
    <form id="form1" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="scr_principal" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <asp:HiddenField ID="hdfcodigosap" runat="server" Value="" />
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
        <table style="width: 100%; margin: auto;">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;" class="tablaFrame">
                                                <tr>
                                                    <td colspan="10" class="SubTitulo" style="text-align: left;">
                                                        <table style="width: 100%;" class="tablaInterna">
                                                            <tr>
                                                                <td style="width: 70%;">
                                                                    <asp:Label ID="lblSubTituloRelFam1" runat="server" Text="Datos del Familiar 1"></asp:Label>
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
                                                    <td style="width: 1%;"></td>
                                                    <td style="text-align: left; width: 20%;">
                                                        <asp:Label ID="lblRelFam1" runat="server" Text="Parentesco:" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td style="padding-top: 10px;" colspan="6">
                                                        <asp:DropDownList ID="ddlRelFam1" runat="server" CssClass="txtTextoCombo" TabIndex="1"></asp:DropDownList>
                                                    </td>
                                                    <td style="width: 2%;" class="requerido">
                                                        <asp:Image ID="Image26" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td style="width: 2%;">
                                                        <asp:RequiredFieldValidator ID="rfvddlRelFam1" runat="server" ControlToValidate="ddlRelFam1"
                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione relación Familiar" InitialValue="0"
                                                            SetFocusOnError="true" ValidationGroup="registraPadres" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 1%;"></td>
                                                    <td style="text-align: left; width: 15%;">
                                                        <asp:Label ID="lblTipoDocP1" runat="server" Text="Doc. de Identidad:" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td style="padding-top: 0px; padding-bottom: 0px;" colspan="6">
                                                        <table class="tablaInterna">
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlTipDocRelFam1" runat="server" CssClass="txtTextoCombo" Width="100%" TabIndex="2"></asp:DropDownList>

                                                                </td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="rfvTipDocRelFam1" runat="server" ControlToValidate="ddlTipDocRelFam1"
                                                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Elija tipo de documento." InitialValue="0"
                                                                        SetFocusOnError="true" ValidationGroup="registraPadres" Text="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNumDocRelFam1" runat="server" CssClass="txtCajaTextoPeq" MaxLength="15" onkeydown="return (event.keyCode!=13);" onblur="valLongitudDocFam1()" TabIndex="3"></asp:TextBox>
                                                                </td>
                                                                <td class="requerido">
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                </td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="rfvNumDocRelFam1" runat="server" ControlToValidate="txtNumDocRelFam1"
                                                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese número de documento." InitialValue=""
                                                                        SetFocusOnError="true" ValidationGroup="registraPadres" Text="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <%--<td class="requerido">
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                                        ErrorMessage="El N° de documento debe tener mínimo 5 dígitos."
                                                                        ControlToValidate="txtNumDocRelFam1" ValidationExpression="^[a-zA-Z0-9\s]{5,10}$"
                                                                        CssClass="MsgAlerta" ValidationGroup="registraPadres"></asp:RegularExpressionValidator>
                                                                </td>--%>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 2%;"></td>
                                                    <td style="width: 2%;"></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="width: 20%;">
                                                        <asp:Label ID="lblNom1" runat="server" Text="Nombres:" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <asp:TextBox ID="txtNomRelFam1" TabIndex="4" runat="server" CssClass="txtCajaTextoPeq transform-capitalize" MaxLength="60" placeholder="Nombres del Familiar"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 2%;" class="requerido">
                                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td style="width: 2%;">
                                                        <asp:RequiredFieldValidator ID="rfvNomRelFam1" runat="server" ControlToValidate="txtNomRelFam1"
                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre del RelFam1." InitialValue=""
                                                            SetFocusOnError="true" ValidationGroup="registraPadres" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="width: 2%;"></td>
                                                    <td style="width: 15%;">
                                                        <asp:Label ID="lblApe1" runat="server" Text="Apellidos:" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtApeRelFam1" TabIndex="5" runat="server" CssClass="txtCajaTextoPeq transform-capitalize" MaxLength="60" placeholder="Apellidos del Familiar"></asp:TextBox>

                                                    </td>
                                                    <td style="width: 2%;" class="requerido">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td style="width: 2%;">
                                                        <asp:RequiredFieldValidator ID="rfvApeRelFam1" runat="server" ControlToValidate="txtApeRelFam1"
                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese apellidos del RelFam1" InitialValue=""
                                                            SetFocusOnError="true" ValidationGroup="registraPadres" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="lblTelefP1" runat="server" Text="Teléfono:" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTelefonoRelFam1" TabIndex="6" runat="server" CssClass="txtCajaTextoPeq" MaxLength="15" onblur="valLongitudTelFam1()"></asp:TextBox>
                                                    </td>
                                                    <td class="requerido">
                                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvTelefonoRelFam1" runat="server" ControlToValidate="txtTelefonoRelFam1"
                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese número de telefono." InitialValue=""
                                                            SetFocusOnError="true" ValidationGroup="registraPadres" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="lblEmailP1" runat="server" Text="Correo electrónico:" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td class="td_txt_Email">
                                                        <asp:TextBox ID="txtEmailRelFam1" TabIndex="8" runat="server" CssClass="txtCajaTextoPeq" MaxLength="30" placeholder="Ejemplo: ejemplo@correo.com"></asp:TextBox>
                                                    </td>
                                                    <td class="requerido">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvEmailRelFam1" runat="server" ControlToValidate="txtEmailRelFam1"
                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese e-mail." InitialValue=""
                                                            SetFocusOnError="true" ValidationGroup="registraPadres" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td style="text-align: left" colspan="5">
                                                        <asp:CheckBox ID="chkRelFam1Fallecido" runat="server" CssClass="radioButtonList" Text="Marque en caso su familiar haya fallecido." />
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>--%>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                        <div style="display: none;">
                                                            <asp:TextBox ID="txtApplicationRelationshipId1" runat="server" Width="100px"></asp:TextBox>
                                                            <asp:TextBox ID="txtApplicationRelationshipId2" runat="server" Width="100px"></asp:TextBox>
                                                            <asp:TextBox ID="txtApplicationRelationshipId3" runat="server" Width="100px"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="tablaInterna" align="right">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnOcultaPariente2" runat="server" Text="Quitar Familiar" CssClass="btnQuitar" OnClick="BtnOcultaPariente2_Click" ToolTip="Ocultar Datos de Familiar" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="BtnAgregaPariente2" TabIndex="9" runat="server" Text="Agregar Familiar" CssClass="btnAgregar" OnClick="BtnAgregaPariente2_Click" ToolTip="Agregar Datos de Familiar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlRelFam2" runat="server">
                                                <table style="width: 100%;" class="tablaFrame">
                                                    <tr>
                                                        <td colspan="10" class="SubTitulo" style="text-align: left;">
                                                            <asp:Label ID="lblSubTituloRelFam2" runat="server" Text="Datos del Familiar 2"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 20%;">
                                                            <asp:Label ID="lblRelFam2" runat="server" CssClass="TextoEtiqueta" Text="Parentesco:"></asp:Label>
                                                        </td>
                                                        <td style="padding-top: 10px;" colspan="6">
                                                            <asp:DropDownList ID="ddlRelFam2" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                                        </td>
                                                        <td style="width: 2%;" class="requerido">
                                                            <asp:Image ID="Image32" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvddlRelFam2" runat="server" ControlToValidate="ddlRelFam2" CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione relación Familiar" InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 15%;">
                                                            <asp:Label ID="lblTipoDocP4" runat="server" CssClass="TextoEtiqueta" Text="Doc. de Identidad:"></asp:Label>
                                                        </td>
                                                        <td colspan="6" style="padding-top: 0px; padding-bottom: 0px;">
                                                            <table class="tablaInterna">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlTipDocRelFam2" runat="server" CssClass="txtTextoCombo" Width="110px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RequiredFieldValidator ID="rfvTipDocRelFam5" runat="server" ControlToValidate="ddlTipDocRelFam2" CssClass="MsgAlertaIncompleto" ErrorMessage="Elija tipo de documento." InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNumDocRelFam2" runat="server" CssClass="txtCajaTextoPeq" MaxLength="15" onkeydown="return (event.keyCode!=13);" onblur="valLongitudDocFam2()"></asp:TextBox>
                                                                    </td>
                                                                    <td class="requerido">
                                                                        <asp:Image ID="Image30" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RequiredFieldValidator ID="rfvNumDocRelFam5" runat="server" ControlToValidate="txtNumDocRelFam2" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese número de documento." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <%--<td class="requerido">
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                                            ErrorMessage="El N° de documento debe tener mínimo 5 dígitos."
                                                                            ControlToValidate="txtNumDocRelFam2" ValidationExpression="^[a-zA-Z0-9\s]{5,10}$"
                                                                            CssClass="MsgAlerta"></asp:RegularExpressionValidator>
                                                                    </td>--%>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 2%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="width: 20%;">
                                                            <asp:Label ID="lblNomP5" runat="server" CssClass="TextoEtiqueta" Text="Nombres:"></asp:Label>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <asp:TextBox ID="txtNomRelFam2" runat="server" CssClass="txtCajaTextoPeq transform-capitalize" MaxLength="60" placeholder="Nombres del Familiar"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 2%;" class="requerido">
                                                            <asp:Image ID="Image31" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvNomRelFam5" runat="server" ControlToValidate="txtNomRelFam2" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre del Familiar 2." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 15%;">
                                                            <asp:Label ID="lblApeP5" runat="server" CssClass="TextoEtiqueta" Text="Apellidos:"></asp:Label>
                                                        </td>
                                                        <td style="width: 30%; text-align: left;">
                                                            <asp:TextBox ID="txtApeRelFam2" runat="server" CssClass="txtCajaTextoPeq transform-capitalize" MaxLength="60" placeholder="Apellidos del Familiar"></asp:TextBox>
                                                        </td>
                                                        <td class="requerido" style="width: 2%;">
                                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvApeRelFam5" runat="server" ControlToValidate="txtApeRelFam2" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese apellidos de la RelFam2" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblTelefP5" runat="server" CssClass="TextoEtiqueta" Text="Teléfono:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTelefonoRelFam2" runat="server" CssClass="txtCajaTextoPeq" MaxLength="15" onblur="valLongitudTelFam2()"></asp:TextBox>
                                                        </td>
                                                        <td class="requerido">
                                                            <asp:Image ID="Image29" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvTelefonoRelFam2" runat="server" ControlToValidate="txtTelefonoRelFam2" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese número de telefono." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblEmailP5" runat="server" CssClass="TextoEtiqueta" Text="Correo electrónico:"></asp:Label>
                                                        </td>
                                                        <td class="td_txt_Email">
                                                            <asp:TextBox ID="txtEmailRelFam2" runat="server" CssClass="txtCajaTextoPeq" MaxLength="30" placeholder="Ejemplo: ejemplo@correo.com"></asp:TextBox>
                                                        </td>
                                                        <td class="requerido">
                                                            <asp:Image ID="Image28" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvEmailRelFam2" runat="server" ControlToValidate="txtEmailRelFam2" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese e-mail." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td colspan="5">
                                                            <asp:CheckBox ID="chkRelFam2Fallecido" runat="server" CssClass="radioButtonList" Text="Marque en caso su familiar haya fallecido." />
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>--%>
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
                                        <td>
                                            <table class="tablaInterna" align="right">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnOcultaPariente3" runat="server" Text="Quitar Familiar" CssClass="btnQuitar" OnClick="BtnOcultaPariente3_Click" ToolTip="Ocultar Datos de Familiar" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="BtnAgregaPariente3" runat="server" Text="Agregar Familiar" CssClass="btnAgregar" OnClick="BtnAgregaPariente3_Click" ToolTip="Agregar Datos de Familiar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlRelFam3" runat="server">
                                                <table style="width: 100%;" class="tablaFrame">
                                                    <tr>
                                                        <td colspan="10" class="SubTitulo" style="text-align: left;">
                                                            <asp:Label ID="lblSubTituloRelFam3" runat="server" Text="Datos del Familiar 3"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 20%;">
                                                            <asp:Label ID="lblRelFam3" runat="server" CssClass="TextoEtiqueta" Text="Parentesco:"></asp:Label>
                                                        </td>
                                                        <td style="padding-top: 10px;" colspan="6">
                                                            <asp:DropDownList ID="ddlRelFam3" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:Image ID="Image33" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvddlRelFam3" runat="server" ControlToValidate="ddlRelFam3" CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione relación Familiar" InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 2%;"></td>
                                                        <td style="text-align: left; width: 15%;">
                                                            <asp:Label ID="lblTipoDocP3" runat="server" CssClass="TextoEtiqueta" Text="Doc. de Identidad:"></asp:Label>
                                                        </td>
                                                        <td colspan="6" style="padding-top: 0px; padding-bottom: 0px;">
                                                            <table class="tablaInterna">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlTipDocRelFam3" runat="server" CssClass="txtTextoCombo" Width="110px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RequiredFieldValidator ID="rfvTipDocRelFam3" runat="server" ControlToValidate="ddlTipDocRelFam3" CssClass="MsgAlertaIncompleto" ErrorMessage="Elija tipo de documento." InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNumDocRelFam3" runat="server" CssClass="txtCajaTextoPeq" MaxLength="15" onkeydown="return (event.keyCode!=13);" onblur="valLongitudDocFam3()"></asp:TextBox>
                                                                    </td>
                                                                    <td class="requerido">
                                                                        <asp:Image ID="Image23" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:RequiredFieldValidator ID="rfvNumDocRelFam3" runat="server" ControlToValidate="txtNumDocRelFam3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese número de documento." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <%--<td class="requerido">
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                                            ErrorMessage="El N° de documento debe tener mínimo 5 dígitos."
                                                                            ControlToValidate="txtNumDocRelFam3" ValidationExpression="^[a-zA-Z0-9\s]{5,10}$"
                                                                            CssClass="MsgAlerta"></asp:RegularExpressionValidator>
                                                                    </td>--%>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 2%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="width: 20%;">
                                                            <asp:Label ID="lblNomP4" runat="server" CssClass="TextoEtiqueta" Text="Nombres:"></asp:Label>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <asp:TextBox ID="txtNomRelFam3" runat="server" CssClass="txtCajaTextoPeq transform-capitalize" MaxLength="60" placeholder="Nombres del Familiar"></asp:TextBox>
                                                        </td>
                                                        <td class="requerido" style="width: 2%;">
                                                            <asp:Image ID="Image24" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvNomRelFam4" runat="server" ControlToValidate="txtNomRelFam3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre del Familiar 3." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width: 2%;"></td>
                                                        <td style="width: 15%;">
                                                            <asp:Label ID="lblApeP4" runat="server" CssClass="TextoEtiqueta" Text="Apellidos:"></asp:Label>
                                                        </td>
                                                        <td style="width: 30%; text-align: left;">
                                                            <asp:TextBox ID="txtApeRelFam3" runat="server" CssClass="txtCajaTextoPeq" MaxLength="60" placeholder="Apellidos del Familiar"></asp:TextBox>
                                                        </td>
                                                        <td class="requerido" style="width: 2%;">
                                                            <asp:Image ID="Image25" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 2%;">
                                                            <asp:RequiredFieldValidator ID="rfvApeRelFam3" runat="server" ControlToValidate="txtApeRelFam3" CssClass="MsgAlertaIncompleto transform-capitalize" ErrorMessage="Ingrese apellidos del RelFam3" InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblTelefP6" runat="server" CssClass="TextoEtiqueta" Text="Teléfono:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTelefonoRelFam3" runat="server" CssClass="txtCajaTextoPeq" MaxLength="15" onblur="valLongitudTelFam3()"></asp:TextBox>
                                                        </td>
                                                        <td class="requerido">
                                                            <asp:Image ID="Image19" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvTelefonoRelFam3" runat="server" ControlToValidate="txtTelefonoRelFam3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese número de telefono." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblEmailP6" runat="server" CssClass="TextoEtiqueta" Text="Correo electrónico:"></asp:Label>
                                                        </td>
                                                        <td class="td_txt_Email">
                                                            <asp:TextBox ID="txtEmailRelFam3" runat="server" CssClass="txtCajaTextoPeq" MaxLength="30" placeholder="Ejemplo: ejemplo@correo.com"></asp:TextBox>
                                                        </td>
                                                        <td class="requerido">
                                                            <asp:Image ID="Image22" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvEmailRelFam3" runat="server" ControlToValidate="txtEmailRelFam3" CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese e-mail." InitialValue="" SetFocusOnError="true" Text="*" ValidationGroup="registraPadres"></asp:RequiredFieldValidator>
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
                                <asp:Label ID="Label4" runat="server" CssClass="tdTextNormal" Text="("></asp:Label>
                                <asp:Image ID="Image21" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                <asp:Label ID="Label5" runat="server" CssClass="tdTextNormal" Text=")  "></asp:Label>
                                <asp:Label ID="Label3" runat="server" CssClass="tdTextNormal" Text="Campos Obligatorios"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="controles" style="width: 100%;">
                                    <tr>
                                        <td style="width:33%">
                                            <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/Images/Buttons/btnPrev.png" ToolTip="Página anterior" OnClick="imgBtnBack_Click" />
                                        </td>
                                        <td style="width:33%;text-align: center;border-bottom:0px !important" class="SubTitulo">
                                            
                                        </td>
                                        <td style="text-align: right;width:33%">
                                            <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" ValidationGroup="registraPadres"  OnClientClick="javascript: return validations();"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnMostrarError" Style="display: none;" runat="server"/>
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
                                            <asp:Label runat="server" ID="lblmessage" CssClass="tdTextNormalAlerta"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="popup-footer">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnSalir_Click" OnClientClick="javascript: return returnTabIndex();">Cerrar</asp:LinkButton>
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
    <%--Ini: Christian Ramirez[GIIT] Caso43692 - 20180423--%>
    <div id="divPopupAlerta" class="popup">
        <div class="popup-contenido">
            <div class="popup-cabecera-mensaje">
                ¡Alerta!
            </div>
            <div class="popup-contenido-mensaje">
                <span id="spnPopupMensaje" class="popup-texto"></span>
            </div>
            <div class="popup-footer-mensaje">
                <input id="btnCerrar2" type="button" value="Cerrar" class="popup-footer-mensaje-cerrar" />
            </div>
        </div>
    </div>
    <%--Fin: Christian Ramirez[GIIT] Caso43692 - 20180423--%>
    <script type="text/javascript">
        var UrlAcion = {
            UrlGetStepsAdmision: 'frm09_InfoPadres.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
    <script src="../JavaScript/Webforms/frm09_InfoPadres.js"></script> <%--Se agrega: Christian Ramirez[GIIT] Caso43692 - 20180423--%>
</body>
</html>
