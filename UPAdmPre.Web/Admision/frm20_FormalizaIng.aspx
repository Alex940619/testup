<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="frm20_FormalizaIng.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm20_FormalizaIng" %>

<%@ Register Src="../UserControl/cuwControlFecha.ascx" TagName="cuwControlFecha" TagPrefix="ucd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validate.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine-en.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery-ui.js" type="text/javascript"></script>
    <script src="../JavaScript/json2.js" type="text/javascript"></script>
    <script src="../JavaScript/JS.js" type="text/javascript"></script>
    <script src="../JavaScript/thickbox.js" type="text/javascript"></script>

    <link href="../Styles/Utilitarios.css" rel="stylesheet" />
    

    <style type="text/css">
        .auto-style1 {
            width: 151px;
            height: 26px;
        }

        #upResultado {
            overflow-x: hidden;
        }

        #lblTextoEstadoFormalizacion {
            color:red;
            font-size: 16px;
            white-space:nowrap;
            
        }

        #lblEstadoFormalizacion {
            color:red;
            font-size: 16px;
            white-space:nowrap;
        }

        #lblAvisoFinFormalizacion {
            color:red;
            font-size: 16px;
            white-space:nowrap;
            
        }

        #lblFechaFinFormalizacion {
            color:red;
            font-size: 16px;
            white-space:nowrap;
        }

        .encuestaRGC {
            display: none;
        }

    </style>
    <script type="text/javascript">
        $("document").ready(function () {

            // validate signup form on keyup and submit
            $("#frm20_FormalizaIng").validate({
                rules: {
                    txtEmail1: {
                        required: false,
                        email: true
                    },
                    txtEmail2: {
                        required: false,
                        email: true
                    },
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
                    txtEmail1: "Debe ingresar un correo válido.",
                    txtEmail2: "Debe ingresar un correo válido.",
                    txtEmailRelFam1: "Debe ingresar un correo válido.",
                    txtEmailRelFam2: "Debe ingresar un correo válido.",
                    txtEmailRelFam3: "Debe ingresar un correo válido."
                }
            });

        });

        function validarFechaMenorActual(date) {
            var x = new Date();
            var fecha = date.split("/");
            x.setFullYear(fecha[2], fecha[1] - 1, fecha[0]);
            var today = new Date();

            if (x >= today) {
                alert("Incorrecto");
                return false;
            }
            else {
                alert("Correcto");
                return true;
            }
        }
    </script>
    <script>
        function Direccion() {
            var Pais = $(".ddlPais").val();
            if (Pais == "554") {
                $(".secPeru").css("display", "")
                $(".secExtranjero").css("display", "none")
            }
            else {
                $(".secPeru").css("display", "none")
                $(".secExtranjero").css("display", "")
            }
        }

        $(function () {
            $(".ddlPais").bind("change", Direccion);
            Direccion();
        });
    </script>
    <script>
        function valLongitudDoc() {
            if ($("#ddlTipoDocumento").val() == "D.N.I.") {
                if ($("#txtNumDocumento").val().length != 8) {
                    $("#txtNumDocumento").focus();
                    $("#txtNumDocumento").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                    //alert("El N° de DNI debe tener 8 dígitos.");
                    $("#lblmessage").html("El N° de DNI debe tener 8 dígitos.")
                    $find("mpeMostrarError").show();
                }
                else {
                    $("#txtNumDocumento").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                }
            }
            else {
                if ($("#txtNumDocumento").val().length < 4) {
                    $("#txtNumDocumento").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                } else {
                    $("#txtNumDocumento").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                }
            }
        };

        function valLongitudTel() {
            if ($("#txtNumTelefono").val().length < 7) {
                $("#txtNumTelefono").focus();
                $("#txtNumTelefono").css("border-color", "#337ab7").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                //alert("El N° de teléfono debe tener 7 dígitos como mínimo.");
                $("#lblmessage").html("El N° de teléfono debe tener 7 dígitos como mínimo.")
                $find("mpeMostrarError").show();
            } else {
                $("#txtNumTelefono").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
            }
        };

    </script>
    <script>

        function validations() {
            if (valLongitudDocFam1() == false ||
                valLongitudDocFam2() == false ||
                valLongitudDocFam3() == false ||
                valLongitudTelFam1() == false ||
                valLongitudTelFam2() == false ||
                valLongitudTelFam3() == false) {
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
    <form id="form1" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="scr_principal" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <table style="width: 100%;">
            <tr>
                <td class="Titulo CabeceraNoSeleccionada">
                    <asp:Label ID="Label2" runat="server" Text="Formalización de Ingreso" CssClass="TextoEtiqueta"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" class="tablaInterna">
                        <tr>
                            <td colspan="2" style="text-align:right">
                                <asp:Label ID="lblTextoEstadoFormalizacion" runat="server" Text="Estado Formalización:" CssClass="TextoEtiqueta" ></asp:Label>
                            </td>
                            <td colspan="2" style="text-align:left">
                                <asp:Label ID="lblEstadoFormalizacion" runat="server" CssClass="tdTextNormal"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                                <asp:Label ID="Label1" runat="server" Text="Postulante:" CssClass="TextoEtiqueta"></asp:Label>
                            </td>
                            <td style="width: 40%;">
                                <asp:Label ID="lblPostulante" runat="server" CssClass="tdTextNormal"></asp:Label>
                            </td>
                            <td style="width: 18%;">
                                <asp:Label ID="Label4" runat="server" Text="Nro. Documento:" CssClass="TextoEtiqueta"></asp:Label>
                            </td>
                            <td style="width: 32%;">
                                <asp:Label ID="lblNroDocumento" runat="server" CssClass="tdTextNormal"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Modalidad:" CssClass="TextoEtiqueta"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblModalidad" runat="server" CssClass="tdTextNormal"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Carrera:" CssClass="TextoEtiqueta"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCarrera" runat="server" CssClass="tdTextNormal"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <%--<td>
                    <asp:Panel ID="pnlHorario" runat="server">
                        <table style="width: 100%;" class="tablaInterna">
                            <tr>
                                <td class="SubTitulo" colspan="4">
                                    <asp:Label ID="lblTituloSeleccion" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tablaFrameDesc" colspan="4">
                                    <div class="box-icon">
                                        <i class="fa fa-check"></i>
                                    </div>
                                    <div class="box-content"><br />
                                        <asp:Label ID="lblMsg" runat="server" CssClass="tdTextNormal" Text="Recuerda que para llevar a cabo el proceso de formalización, debes entregar la documentación solicitada (revisar <a href='https://campusvirtual.up.edu.pe/pdf/up-prospecto-de-admision-2017.pdf' target='_blank'>Prospecto de Admisión 2017</a>, páginas 32 y 33), y cumplir los requisitos académicos correspondientes, según a la modalidad en la que fuiste seleccionado (a)."></asp:Label>
                                    </div>
                                </td>

                            </tr>

                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblSubTituloHorario" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                </td>

                                <td style="width: 73%; padding-top: 18px;">
                                    <asp:DropDownList ID="ddlHorario" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                </td>
                                <td style="width: 1%; padding-top: 18px;">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td style="width: 1%;">
                                    <asp:RequiredFieldValidator ID="rfvHorario" runat="server" ControlToValidate="ddlHorario" CssClass="MsgAlertaIncompleto"
                                        ErrorMessage="Elija horario." InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraHorario"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <style>
                                @keyframes animate-btn {
                                    0%{
                                        transform:scale(1)
                                    }
                                    20%{
                                        transform:scale(1.1)
                                    }
                                    80%{
                                        transform:scale(1.1)
                                    }
                                    100%{
                                        transform:scale(1)
                                    }
                                }
                            </style>
                            <tr>
                                <td colspan="3" class="conoce-escala-form" style="text-align: center; background: rgba(27,130,182,.2); box-shadow:inset 0px 0px 2px #1b82b6; padding: 10px; border-radius: 10px;">
                                <span id="hlconoce" runat="server"></span>

                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>--%>
                <td>
                    <asp:Panel ID="pnlDatosPersonales" runat="server" >
                        <table style="width:100%" class="tablaInterna">
                            <tr>
                                <td class="SubTitulo" colspan="5">
                                    <asp:Label ID="lblTituloSeleccion" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 1%;"></td>
                                <td style="width: 20%;">
                                    <asp:Label ID="lblPrimNom" runat="server" Text="Primer Nombre:" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                                <td style="width: 75%; padding-top: 8px;">
                                    <asp:TextBox ID="txtPrimNombre" runat="server" CssClass="txtCajaTexto transform-capitalize" MaxLength="50" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </td>
                                <td class="requerido" style="width: 1%;">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td style="width: 1%;">
                                    <asp:RequiredFieldValidator ID="rfvPrimNombre" runat="server" ControlToValidate="txtPrimNombre"
                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese su Nombre." InitialValue=""
                                        SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblSegNom" runat="server" Text="Segundo Nombre:" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSegNombre" runat="server" CssClass="txtCajaTexto transform-capitalize" MaxLength="50" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblApePat" runat="server" Text="Apellido Paterno:" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApePaterno" runat="server" CssClass="txtCajaTexto transform-capitalize" MaxLength="50" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </td>
                                <td class="requerido">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvApePaterno" runat="server" ControlToValidate="txtApePaterno"
                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese su Apellido Paterno" InitialValue=""
                                        SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblApeMat" runat="server" Text="Apellido Materno:" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApeMaterno" runat="server" CssClass="txtCajaTexto transform-capitalize" MaxLength="50" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Image ID="Image20" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvApeMaterno" runat="server" ControlToValidate="txtApeMaterno"
                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese su Apellido Materno" InitialValue=""
                                        SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblSexo" runat="server" Text="Género:" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                                <td style="padding-top: 8px; padding-bottom: 15px;">
                                    <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal" CssClass="radioButtonList">
                                        <asp:ListItem Value="2">Femenino</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">Masculino</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblFecNac" runat="server" Text="Fecha Nacimiento:" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                                <td>
                                    <table class="tablaInterna">
                                        <tr>
                                            <td>
                                                <ucd:cuwControlFecha ID="cuwFechaNacimiento" runat="server" CssClass="txtCajaTexto" ValidationGroup="registraDatoPersonal" />
                                            </td>
                                            <td class="requerido">
                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblDocIdentidad" runat="server" CssClass="TextoEtiqueta" Text="Doc. Identidad:"></asp:Label>
                                </td>
                                <td>
                                    <table border="0" class="tablaInterna">
                                        <tr>
                                            <td style="width: 120px;">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="txtTextoCombo" Width="140px"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="width: 5px;">
                                                <asp:RequiredFieldValidator ID="rfvTipoDocumento" runat="server" ControlToValidate="ddlTipoDocumento"
                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija su Tipo de Documento" InitialValue="0"
                                                    SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 110px;">
                                                <asp:TextBox ID="txtNumDocumento" runat="server" CssClass="txtCajaTexto" Width="100px" MaxLength="10" onkeydown="return (event.keyCode!=13);" onblur="valLongitudDoc()"></asp:TextBox>
                                            </td>
                                            <td style="width: 5px;" class="requerido">
                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                            </td>
                                            <td style="width: 5px;">
                                                <asp:RequiredFieldValidator ID="rfvNumDocumento" runat="server" ControlToValidate="txtNumDocumento"
                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese su Número de Documento." InitialValue=""
                                                    SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trUbigeo">
                                <td></td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" CssClass="TextoEtiqueta" Text="Ubigeo de Nacimiento:"></asp:Label>
                                </td>
                                <td style="width: 500px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtUbigeoNacimiento" runat="server" CssClass="txtCajaTexto" Width="100px" MaxLength="6"  
                                                    onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                <span id="spnAyuda" class="link">
                                                    <img alt="" src="../Images/32x32_help.png" width="25" height="25" />
                                                </span>
                                            </td>
                                            <td style="width: 5px;" class="requerido">
                                                <span id="spnUbigeoRequerido" style="display:none;">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                </span>
                                            </td>
                                            <td style="width: 5px;">                                                
                                                <span id="spnUbigeoRequeridoIcono" style="display:none;">
                                                    <img src="../Images/ico_Alerta.gif" />
                                                </span>
                                            </td>
                                            <td>
                                                <span id="spnUbigeo" style="color:red; display:none;">
                                                    Debe ingresar los 6 digitos del ubigeo
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblPaisNac" runat="server" CssClass="TextoEtiqueta" Text="Pais de Nacimiento:"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlPaisNacimiento" runat="server" CssClass="txtTextoCombo" OnSelectedIndexChanged="ddlPaisNacimiento_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="requerido">
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvPaisNacimiento" runat="server" ControlToValidate="ddlPaisNacimiento"
                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Elija su Pais de Nacimiento" InitialValue="0"
                                        SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblDptoNac" runat="server" CssClass="TextoEtiqueta" Text="Dpto. de Nacimiento:"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlDptoNacimiento" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlPaisNacimiento" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="requerido">
                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvDptoNacimiento" runat="server" ControlToValidate="ddlDptoNacimiento"
                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese su Departamento de Nacimiento" InitialValue="0"
                                        SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblNacionalidad" runat="server" Text="Nacionalidad:" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlNacionalidad" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                </td>
                                <td class="requerido">
                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvNacionalidad" runat="server" ControlToValidate="ddlNacionalidad"
                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Elija su Nacionalidad" InitialValue="0"
                                        SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlDatosContacto" runat="server" >
                        <table style="width:100%" class="tablaInterna">
                            <tr>
                                <td class="SubTitulo">
                                    <asp:Label ID="lblDatosContacto" runat="server" Text="Datos de Contacto"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="SubTituloInt">&nbsp;&nbsp;Domicilio Actual</td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="tablaInterna">
                                                    <tr>
                                                        <td style="width: 1%;"></td>
                                                        <td style="width: 20%;">
                                                            <asp:Label ID="lblPais" runat="server" Text="País:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td style="width: 75%; padding-top: 18px;">
                                                            <asp:DropDownList ID="ddlPais" runat="server" CssClass="txtTextoCombo ddlPais" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </td>
                                                        <td style="width: 1%;" class="requerido">
                                                            <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td style="width: 1%;">
                                                            <asp:RequiredFieldValidator ID="rfvPais" runat="server" ControlToValidate="ddlPais"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Elija su Pais de Residencia" InitialValue="0"
                                                                SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr class="secPeru">
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblDepartamento" runat="server" Text="Departamento:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlDpto" runat="server" CssClass="txtTextoCombo" OnSelectedIndexChanged="ddlDpto_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </ContentTemplate>
                                                                <%--<Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlPais" EventName="SelectedIndexChanged" />
                                                                </Triggers>--%>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td class="requerido">
                                                            <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvDpto" runat="server" ControlToValidate="ddlDpto"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Elija su Departamento de Residencia" InitialValue="0"
                                                                SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr class="secPeru">
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblProvincia" runat="server" Text="Provincia:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="txtTextoCombo" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlDpto" EventName="SelectedIndexChanged" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td class="requerido">
                                                            <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvProvincia" runat="server" ControlToValidate="ddlProvincia"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Elija su Provincia de Residencia" InitialValue="0"
                                                                SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr class="secPeru">
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblDistrito" runat="server" Text="Distrito:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlDistrito" runat="server" CssClass="txtTextoCombo" OnSelectedIndexChanged="ddlDistrito_SelectedIndexChanged"></asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlProvincia" EventName="SelectedIndexChanged" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td class="requerido">
                                                            <asp:Image ID="Image12" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvDistrito" runat="server" ControlToValidate="ddlDistrito"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Elija su Distrito de Residencia" InitialValue="0"
                                                                SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr class="secPeru">
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table id="tblDireccion" style="width: 100%;" class="tablaInterna">
                                                                <tr>
                                                                    <td style="width: 10%;">
                                                                        <asp:DropDownList ID="ddlTipoVia" runat="server" CssClass="txtTextoCombo" Width="125px" OnSelectedIndexChanged="ddlTipoVia_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 1%;" class="requerido">
                                                                        <asp:Image ID="Image19" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="txtCajaTexto" OnTextChanged="txtDireccion_TextChanged" AutoPostBack="true" MaxLength="70" onkeydown="return (event.keyCode!=13);" Width="92%"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 1%;" class="requerido">
                                                                        <asp:Image ID="Image13" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                    </td>
                                                                    <td style="width: 1%;">
                                                                        <asp:RequiredFieldValidator ID="rfvTipoVia" runat="server" ControlToValidate="ddlTipoVia"
                                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Elija el Tipo de Vía" InitialValue="0"
                                                                            SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td style="width: 1%;">
                                                                        <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txtDireccion"
                                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese la Dirección de Domicilio" InitialValue=""
                                                                            SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td style="width: 5%;">&nbsp;</td>
                                                    </tr>
                                                    <tr class="secPeru">
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblNumeracion" runat="server" Text="Numeración:" CssClass="TextoEtiqueta"></asp:Label><br />
                                                            <asp:Label ID="lblNumeracions" runat="server" Text="(#, Mz, Lote)" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table style="width: 420px; border: 0; border-spacing: 0; empty-cells: hide" class="tablaInterna">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        <asp:TextBox ID="txtNumeracion" runat="server" CssClass="txtCajaTexto" Width="100px" OnTextChanged="txtNumeracion_TextChanged" AutoPostBack="true" MaxLength="15" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 1%;" class="requerido">
                                                                        <asp:Image ID="Image14" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                    </td>
                                                                    <td style="width: 1%;">
                                                                        <asp:RequiredFieldValidator ID="rfvNumeracion" runat="server" ControlToValidate="txtNumeracion"
                                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese la Numeración" InitialValue=""
                                                                            SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td style="width: 120px; text-align: right">
                                                                        <asp:Label ID="lblInterior" runat="server" Text="Interior/Dpto/Piso:" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtInterior" runat="server" CssClass="txtCajaTexto" Width="100px" OnTextChanged="txtInterior_TextChanged" AutoPostBack="true" MaxLength="20" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr class="secPeru">
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblDireccionCompleta" runat="server" Text="Dirección Completa:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:UpdatePanel ID="updDireccionCompleta" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtDireccionCompleta" runat="server" CssClass="txtCajaTexto Deshabilitado" ReadOnly="True"></asp:TextBox>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoVia" EventName="SelectedIndexChanged" />
                                                                    <asp:AsyncPostBackTrigger ControlID="txtDireccion" EventName="TextChanged" />
                                                                    <asp:AsyncPostBackTrigger ControlID="txtNumeracion" EventName="TextChanged" />
                                                                    <asp:AsyncPostBackTrigger ControlID="txtInterior" EventName="TextChanged" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr class="secPeru">
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblReferencia" runat="server" Text="Referencia:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReferencia" runat="server" CssClass="txtCajaTexto" MaxLength="100" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr class="secExtranjero">
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="lblDireccionExtranjero" runat="server" Text="Dirección en el Extranjero:" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDireccionExtranjero" runat="server" CssClass="txtCajaTexto" MaxLength="75" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                        </td>
                                                        <td class="requerido">
                                                            <asp:Image ID="imgDirExt" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvDirExt" runat="server" ControlToValidate="txtDireccionExtranjero"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese referencia de su domicilio" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraDatoPersonal1" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="SubTituloInt">&nbsp;&nbsp;Teléfonos y Correos Electrónicos</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="tablaInterna">
                                                    <tr>
                                                        <td class="auto-style2"></td>
                                                        <td>
                                                            <asp:Label ID="lblTelefono" runat="server" CssClass="TextoEtiqueta" Text="Teléfono 1:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table class="tablaInterna">
                                                                <tr>
                                                                    <td style="width: 55px;">
                                                                        <asp:TextBox ID="txtCodCiudadTel" runat="server" CssClass="txtCajaTexto" Width="54px" ReadOnly="True" MaxLength="5"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 105px;">
                                                                        <asp:TextBox ID="txtNumTelefono" runat="server" CssClass="txtCajaTexto" Width="100px" MaxLength="15" onkeydown="return (event.keyCode!=13);" onblur="valLongitudTel()"></asp:TextBox></td>
                                                                    <td class="requerido">
                                                                        <asp:Image ID="Image16" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                    </td>
                                                                    <td style="width: 5px;">
                                                                        <asp:RequiredFieldValidator ID="rfvNumTelefono" runat="server" ControlToValidate="txtNumTelefono"
                                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese su Número de Teléfono" InitialValue=""
                                                                            SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style2"></td>
                                                        <td>
                                                            <asp:Label ID="lblCelular" runat="server" CssClass="TextoEtiqueta" Text="Teléfono 2:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table style="width: 150px; border-spacing: 0px" class="tablaInterna">
                                                                <tr>
                                                                    <td style="width: 10%;">
                                                                        <asp:TextBox ID="txtCodCiudadCel" runat="server" CssClass="txtCajaTexto" Width="54px" ReadOnly="True" MaxLength="5"></asp:TextBox></td>
                                                                    <td style="width: 90%;">
                                                                        <asp:TextBox ID="txtNumCelular" runat="server" CssClass="txtCajaTexto" Width="100px" MaxLength="15" onkeydown="return (event.keyCode!=13);"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style2"></td>
                                                        <td style="width: 250px;">
                                                            <asp:Label ID="lblEmail1" runat="server" CssClass="TextoEtiqueta" Text="Correo electrónico 1:"></asp:Label>
                                                        </td>
                                                        <td class="td_txt_Email">
                                                            <asp:TextBox ID="txtEmail1" runat="server" CssClass="txtCajaTexto"
                                                                onkeydown="return (event.keyCode!=13);" placeholder="Ejemplo: ejemplo@correo.com">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="td_txt_Email requerido">
                                                            <asp:Image ID="Image17" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvEmail1" runat="server" ControlToValidate="txtEmail1"
                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese su Correo electrónico" InitialValue=""
                                                                SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style2"></td>
                                                        <td>
                                                            <asp:Label ID="lblEmail2" runat="server" CssClass="TextoEtiqueta" Text="Correo electrónico 2:"></asp:Label>
                                                        </td>
                                                        <td class="td_txt_Email">
                                                            <asp:TextBox ID="txtEmail2" runat="server" CssClass="txtCajaTexto" MaxLength="100" 
                                                                onkeydown="return (event.keyCode!=13);" placeholder="Ejemplo: abcabcabc@correo.com"></asp:TextBox>
                                                        </td>
                                                        <td class="td_txt_Email"></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>                                
                                <td style="text-align: right;">
                                    <asp:ImageButton ID="imgGuardarDatosPersonales" runat="server" ImageUrl="~/Images/Buttons/btnActualizar_Preform.png" OnClick="imgGuardarDatosPersonales_Click" ValidationGroup="registraDatoPersonal" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlDatosPadres" runat="server">
                        <table class="tablaInterna" style="width:100%">
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
                                                                        <%--<asp:Label ID="lblPasos" runat="server"></asp:Label>--%>
                                                                    </td>
                                                                    <td style="width: 10%; text-align: right;">
                                                                        
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
                                                                        <asp:Image ID="Image15" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                            <asp:Image ID="Image18" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                            <asp:Image ID="Image21" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                            <asp:Image ID="Image22" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                            <asp:Image ID="Image23" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                            <td style="width: 1%;"></td>
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
                                                            <td style="width: 1%;"></td>
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
                                                                <asp:Image ID="Image24" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                            <asp:Image ID="Image25" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                <asp:Image ID="Image27" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                <asp:Image ID="Image34" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                <asp:Image ID="Image35" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                <asp:Image ID="Image36" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                <td style="text-align: right;">
                                    <asp:ImageButton ID="btnGuardarDatosFamiliares" runat="server" ImageUrl="~/Images/Buttons/btnActualizar_Preform.png" OnClick="imgGuardarDatosFamiliares_Click" ValidationGroup="registraPadres" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <%-- INI: JC.DelgadoV [RQ103950] Observaciones Pre Formalización --%>
            <tr runat="server" id="trCantidadCompetencias" style="display:none;">
                <td>
                    <asp:Panel ID="pnlCantidadCompetencias" runat="server">
                        <table style="width:100%" class="tablaInterna">
                            <tr>
                                <td class="SubTitulo" colspan="4">
                                    <asp:Label ID="Label32" runat="server" Text="Cantidad Competencias"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%--<td colspan="2" style="text-align: center">
                                    <asp:Label runat="server" Text="3ro" CssClass="TextoEtiqueta"></asp:Label>
                                </td>--%>
                                <td colspan="2" style="text-align: center">
                                    <asp:Label runat="server" Text="4to" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                                <td colspan="2" style="text-align: center">
                                    <asp:Label runat="server" Text="5to" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%--<td colspan="2" style="text-align: center">
                                    <asp:TextBox ID="txtTipoNota3" runat="server" CssClass="txtCajaTexto" Text="Numérico" Enabled="false" Width="100px"></asp:TextBox>
                                </td >--%>
                                <td colspan="2" style="text-align: center">
                                    <asp:TextBox ID="txtTipoNota4" runat="server" CssClass="txtCajaTexto" Text="Letras" Enabled="false" Width="100px"></asp:TextBox>
                                </td>
                                <td colspan="2" style="text-align: center">
                                    <asp:TextBox ID="txtTipoNota5" runat="server" CssClass="txtCajaTexto" Text="Letras" Enabled="false" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <%--<td style="text-align: center">
                                    <asp:Label runat="server" Text="Orden de Mérito: " CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrdenMeritoTercero_Competencias" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumeros(this);" Width="100px"></asp:TextBox>
                                </td>--%>

                                <td style="text-align: center">
                                    <asp:Label runat="server" Text="¿Cuántas (AD) obtuviste?" CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalADCuarto" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumerosFormalizacion(this);" Width="100px"></asp:TextBox>
                                </td>

                                <td style="text-align: center">
                                    <asp:Label runat="server" Text="¿Cuántas (AD) obtuviste?" CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalADQuinto" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumerosFormalizacion(this);" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <%--<td style="text-align: center">
                                    <asp:Label runat="server" Text="Nro Alumnos en su Promoción: " CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalAlumnosTercero_Competencias" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumeros(this);" Width="100px"></asp:TextBox>
                                </td>--%>

                                <td style="text-align: center">
                                    <asp:Label runat="server" Text="¿Cuántas (A) obtuviste?" CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalACuarto" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumerosFormalizacion(this);" Width="100px"></asp:TextBox>
                                </td>

                                <td style="text-align: center">
                                    <asp:Label runat="server" Text="¿Cuántas (A) obtuviste?" CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalAQuinto" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumerosFormalizacion(this);" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <%--<td colspan="2"></td>--%>

                                <td style="text-align: center">
                                    <asp:Label runat="server" Text="¿Cuántas (B) obtuviste?" CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td style="padding-left:0px">
                                    <asp:TextBox ID="txtTotalBCuarto" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumerosFormalizacion(this);" Width="100px"></asp:TextBox>
                                </td>

                                <td style="text-align: center">
                                    <asp:Label runat="server" Text="¿Cuántas (B) obtuviste?" CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td style="padding-left:0px">
                                    <asp:TextBox ID="txtTotalBQuinto" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumerosFormalizacion(this);" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <%--<td colspan="2"></td>--%>

                                <td style="text-align: center">
                                    <asp:Label runat="server" Text="¿Cuántas (C) obtuviste?" CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td style="padding-left:0px">
                                    <asp:TextBox ID="txtTotalCCuarto" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumerosFormalizacion(this);" Width="100px"></asp:TextBox>
                                </td>

                                <td style="text-align: center">
                                    <asp:Label runat="server" Text="¿Cuántas (C) obtuviste?" CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td style="padding-left:0px">
                                    <asp:TextBox ID="txtTotalCQuinto" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumerosFormalizacion(this);" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <%--<td colspan="2"></td>--%>

                                <td style="text-align: center">
                                    <asp:Label runat="server" Text="¿Total de Competencias?" CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td style="padding-left:0px">
                                    <asp:TextBox ID="txtTotalCompetenciasCuarto" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumerosFormalizacion(this);" Width="100px"></asp:TextBox>
                                </td>

                                <td style="text-align: center">
                                    <asp:Label runat="server" Text="¿Total de Competencias?" CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td style="padding-left:0px">
                                    <asp:TextBox ID="txtTotalCompetenciasQuinto" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                        placeholder="Digite valor numérico" onkeypress="return soloNumerosFormalizacion(this);" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>                                
                                <td style="text-align: right;" colspan="4">
                                    <asp:ImageButton ID="imgActualizarCantidadCompetencias" runat="server" ImageUrl="~/Images/Buttons/btnActualizar_Preform.png" OnClick="imgActualizarCantidadCompetencias_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr runat="server" id="trDetalleCompetencias" style="display:none;">
                <td>
                    <%-- INI: Detalle Competencias --%>
                    <asp:Panel ID="pnlDetalleCompetencias" runat="server">
                        <table style="width:100%" class="tablaInterna">
                            <tr>
                                <td class="SubTitulo">
                                    <table style="width: 100%" class="tablaInterna">
                                        
                                        <tr>
                                            <td style="width: 40%;">
                                                <asp:Label ID="Label13" runat="server" Text="Situación Académica Actual"></asp:Label>
                                            </td>
                                            <td style="width: 50%;">
                                                <asp:Label ID="lblPasos" runat="server"></asp:Label>
                                                    <div id="Div2" runat="server" class="tablaFrameDesc2" visible="false">
                                                    <div class="box-icon">
                                                        <i class="fa fa-info-circle"></i>
                                                    </div>
                                                    <div id="Div3"  runat="server"   class="box-content" visible="false">
                                                        <asp:Label ID="Label14" runat="server" CssClass="tdTextoDetalle2" Text=""></asp:Label>
                                                    </div>
                                                    </div>
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
                                    <asp:UpdatePanel runat="server" ID="upNotas" UpdateMode="Always">
                                        <ContentTemplate>
                                            <table style="width: 100%;" class="tablaInterna">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%;" class="tablaFrame">
                                                            <tr>
                                                                <td style="width: 1%;">&nbsp;</td>
                                                                <td style="width: 15%;">&nbsp;</td>
                                                                <td style="width: 27%; padding-top: 10px; text-align: center;">
                                                                    <asp:Label ID="lblGrado1" runat="server" Text="Label" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 1%;">&nbsp;</td>
                                                                <td style="width: 27%; padding-top: 10px; text-align: center;">
                                                                    <asp:Label ID="lblGrado2" runat="server" Text="Label" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 1%;">&nbsp;</td>
                                                                <td style="width: 27%; padding-top: 10px; text-align: center;">
                                                                    <asp:Label ID="lblGrado3" runat="server" Text="Label" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 1%;">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td style="width: 1%;">
                                                                    <asp:Label ID="lblColegio" runat="server" Text="Colegio:" CssClass="TextoEtiqueta" Width="80px"></asp:Label>
                                                                </td>
                                                                <td style="width: 27%; padding-top: 10px;">
                                                                    <table class="tablaInterna" align="center">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlColegioTercero" runat="server" CssClass="txtTextoCombo" Width="130px"></asp:DropDownList>
                                                                            </td>
                                                                            <td class="requerido">
                                                                                <asp:Image ID="Image46" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="rfvCole1" runat="server" ControlToValidate="ddlColegioTercero"
                                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija colegio" InitialValue="0"
                                                                                    SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td style="padding-top: 10px;">
                                                                    <table class="tablaInterna" align="center">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlColegioCuarto" runat="server" CssClass="txtTextoCombo" Width="130px"></asp:DropDownList>
                                                                            </td>
                                                                            <td class="requerido">
                                                                                <asp:Image ID="Image47" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="rfvCole2" runat="server" ControlToValidate="ddlColegioCuarto"
                                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija colegio" InitialValue="0"
                                                                                    SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td style="padding-top: 10px;">
                                                                    <table class="tablaInterna" align="center">
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                <asp:DropDownList ID="ddlColegioQuinto" runat="server" CssClass="txtTextoCombo" Width="130px"></asp:DropDownList>
                                                                            </td>
                                                                            <td class="requerido" style="width: 15px;">
                                                                                <asp:Image ID="Image48" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td class="requerido" style="width: 15px;">
                                                                                <asp:RequiredFieldValidator ID="rfvCole3" runat="server" ControlToValidate="ddlColegioQuinto"
                                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija colegio" InitialValue="0"
                                                                                    SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblAnioLectivo" runat="server" Text="Año Lectivo:" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td class="requerido">
                                                                    <table class="tablaInterna" align="center">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlAnioLectivoTercero" runat="server" CssClass="txtTextoCombo" Width="130px"></asp:DropDownList>
                                                                            </td>
                                                                            <td class="requerido">
                                                                                <asp:Image ID="Image49" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="rfvCole4" runat="server" ControlToValidate="ddlAnioLectivoTercero"
                                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija Año Lectivo" InitialValue="0"
                                                                                    SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td></td>
                                                                <td class="requerido">
                                                                    <table class="tablaInterna" align="center">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlAnioLectivoCuarto" runat="server" CssClass="txtTextoCombo" Width="130px"></asp:DropDownList>
                                                                            </td>
                                                                            <td class="requerido">
                                                                                <asp:Image ID="Image50" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="rfvCole5" runat="server" ControlToValidate="ddlAnioLectivoCuarto"
                                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija Año Lectivo" InitialValue="0"
                                                                                    SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td></td>
                                                                <td class="requerido">
                                                                    <table class="tablaInterna" align="center">
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                <asp:DropDownList ID="ddlAnioLectivoQuinto" runat="server" CssClass="txtTextoCombo" Width="130px"></asp:DropDownList>
                                                                            </td>
                                                                            <td class="requerido" style="width: 15px;">
                                                                                <asp:Image ID="Image51" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td class="requerido" style="width: 15px;">
                                                                                <asp:RequiredFieldValidator ID="rfvCole6" runat="server" ControlToValidate="ddlAnioLectivoQuinto"
                                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija Año Lectivo" InitialValue="0"
                                                                                    SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="text-align: center"></td>
                                                            </tr>
                                                            <%--Ini:Christian Ramirez - REQ91569--%>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <%--<asp:Image ID="Image4" runat="server" ImageUrl="~/Images/ico_Required.gif" />--%>
                                                                    <%--<label class="MsgAlerta" style="font-size: 9px;">
                                                                        Opcional, la información debe ser completada obligatoriamente durante el 
                                                                                    proceso de formalización.</label>--%>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblTipoCalificacion" runat="server" Text="Tipo de Calificación:"
                                                                        CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td class="requerido">
                                                                    <table class="tablaInterna" align="center">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlTipoCalificacionTercero" runat="server" CssClass="txtTextoCombo"
                                                                                    Width="130px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCalificacionTercero_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="requerido">
                                                                                <asp:Image ID="Image52" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlTipoCalificacionTercero"
                                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija un tipo de calificación" InitialValue="0"
                                                                                    SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <table class="tablaInterna" align="center">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlTipoCalificacionCuarto" runat="server" CssClass="txtTextoCombo"
                                                                                    Width="130px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCalificacionCuarto_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="requerido">
                                                                                <asp:Image ID="Image53" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlTipoCalificacionCuarto"
                                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija un tipo de calificación" InitialValue="0"
                                                                                    SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td></td>
                                                                <td sclass="requerido">
                                                                    <table class="tablaInterna" align="center">
                                                                        <%-- <tr>
                                                                            <td>
                                                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                <label class="MsgAlerta" style="font-size:9px;"> Opcional, la información debe ser completada obligatoriamente durante el 
                                                                                    proceso de formalización.</label>
                                                                            </td>
                                                                        </tr>--%>
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                <asp:DropDownList ID="ddlTipoCalificacionQuinto" runat="server" CssClass="txtTextoCombo"
                                                                                    Width="130px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCalificacionQuinto_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="requerido" style="width: 15px;">
                                                                                <asp:Image ID="Image67" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td class="requerido" style="width: 15px;">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlTipoCalificacionQuinto"
                                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija un tipo de calificación" InitialValue="0"
                                                                                    SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <%--Fin:Christian Ramirez - REQ91569--%>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 1px;">
                                                        <div style="display: none;">
                                                            3ro<asp:TextBox ID="txtIdApplicationEducationEnrollTercero" runat="server" Width="100px"></asp:TextBox>
                                                            4to<asp:TextBox ID="txtIdApplicationEducationEnrollCuarto" runat="server" Width="100px"></asp:TextBox>
                                                            5to<asp:TextBox ID="txtIdApplicationEducationEnrollQuinto" runat="server" Width="100px"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>
                                                        <table class="tablaFrame" style="width: 100%;">
                                                            <tr>
                                                                <td colspan="8" class="SubTitulo" style="text-align: left;">
                                                                    <asp:Label ID="Label15" runat="server" Text="Notas de Rendimiento y Notas de Secundaria"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <%--Ini:Christian Ramirez - REQ91569--%>
                                                            <tr runat="server" id ="trCuadroCompetencias" visible ="false">
                                                                <td colspan="10">
                                                                    <img src="../Images/ConversionVigesimal.png" />
                                                                </td>
                                                            </tr>
                                                            <%--Fin:Christian Ramirez - REQ91569--%>
                                                            <tr>
                                                                <td style="width: 1%;">&nbsp;</td>
                                                                <td style="width: 15%;"></td>
                                                                <td style="width: 27%; padding: 15px 0px; text-align: center;">
                                                                    <asp:Label ID="lblGrado11" runat="server" Text="Label" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 1%;">&nbsp;</td>
                                                                <td style="width: 27%; padding: 15px 0px; text-align: center;">
                                                                    <asp:Label ID="lblGrado22" runat="server" Text="Label" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 1%;">&nbsp;</td>
                                                                <td style="width: 27%; padding: 15px 0px; text-align: center;">
                                                                    <asp:Label ID="lblGrado33" runat="server" Text="Label" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td style="width: 1%;">&nbsp;</td>
                                                            </tr>
                                                            <%--Ini:Christian Ramirez - REQ91569--%>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td style="border-right: 1px solid #1B2869; vertical-align:top;">
                                                                    <div runat="server" id="divNumericoTercero" visible="false">
                                                                        <%--3er año--%>
                                                                        <table style="width: 100%;">
                                                                        <% if (Session["ModPostulacion"].ToString() != "41")
                                                                            { %>
                                                                            <tr runat="server" id="trOrdenMeritoTercero">
                                                                                <td style="width: 27%;">
                                                                                    <table class="tablaInterna" align="center">
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px; width: 150px;">
                                                                                                <asp:Label ID="Label16" runat="server" Text="Orden de Mérito:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtOrdenMeritoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="3" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image54" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="trNroAlumnosTercero">
                                                                                <td style="width: 27%;">
                                                                                    <table class="tablaInterna" align="center">
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px; width: 150px;">
                                                                                                <asp:Label ID="Label17" runat="server" Text="Nro. Alumnos en su Promoción:"
                                                                                                    CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNroAlmunosTercero" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="3" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image55" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        <% } %>
                                                                            <tr>
                                                                                <td style="width: 27%;">
                                                                                    <table class="tablaInterna" align="center">
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px; width: 150px;">
                                                                                                <asp:Label ID="Label18" runat="server" Text="Matemática:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaMateTercero" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image56" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 27%;">
                                                                                    <table class="tablaInterna" align="center">
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px; width: 150px;">
                                                                                                <asp:Label ID="Label19" runat="server" Text="Comunicación:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaLengTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                    Width="50px" MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image57" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:Button ID="btnEditarNotasTerceroNumerico" runat="server" Text="Editar"
                                                                                        CssClass="btnLimpiar" OnClick="btnEditarNotasTerceroNumerico_Click" Enabled="false" Visible="false"/>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <div id="divLetraTercero" runat="server" visible="false">
                                                                        <table runat="server" id="trLetraTercero" style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 1%;"></td>
                                                                                            <td style="width: 98%;"></td>
                                                                                            <td style="width: 1%;"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td class="MsgAlerta">(CNE = Compentencia No Evaluada)</td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Ciencia y Tecnología (CT)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Diseña y construye soluciones tecnológicas para resolver 
                                                                                                                    problemas.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CT01CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CT01Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Explica el mundo físico basándose en conocimientos sobre los seres vivos , materia y energía
                                                                                                                    , biodiversidad.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CT02CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CT02Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Indaga mediante métodos científicos para construir sus conocimientos.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CT03CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CT03Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Ciencias Sociales (CS)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Construye interpretaciones históricas.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                                <asp:TextBox ID="CS01CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CS01Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Gestiona responsablemente el espacio y el ambiente.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CS02CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CS02Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Gestiona responsablemente los recursos económicos.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CS03CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CS03Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Comunicación en lengua materna (CL)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Escribe diversos tipos de texto.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CL01CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CL01Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Lee diversos tipos de textos escritos</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CL02CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CL02Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Se comunica oralmente.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CL03CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CL03Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Matemática (MA)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Resuelve problemas de cantidad.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA01CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA01Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Resuelve problemas de forma y movimiento.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA02CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA02Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Resuelve problemas de gestión de datos e incertidumbre.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                                <asp:TextBox ID="MA03CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA03Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Resuelve problemas de regularidad equivalencia y 
                                                                                                                        cambio.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA04CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA04Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <%--Ini:Christian Ramirez - REQ95070--%>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Competencias Transversales (CTR)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Gestiona su aprendizaje de manera autónoma.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CTR01CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CTR01Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu>
                                                                                                                    <li><label>Se desenvuelve en los entornos virtuales generados por las 
                                                                                                                        TIC.</label></li>
                                                                                                                </lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CTR02CodigoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="10px"  Visible="false" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CTR02Tercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                                Width="50px" MaxLength="20" Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <%--Fin:Christian Ramirez - REQ95070--%>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:Button ID="btnEditarNotasTerceroLetra" runat="server" Text="Editar"
                                                                                        CssClass="btnLimpiar" OnClick="btnEditarNotasTerceroLetra_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                                <td></td>
                                                                <td style="border-right: 1px solid #1B2869; vertical-align: top;">
                                                                    <div runat="server" id="divNumericoCuarto" visible="false">
                                                                        <%--4to año--%>
                                                                        <table style="width: 100%;">
                                                                            <% if (Session["ModPostulacion"].ToString() != "41")
                                                                                { %>
                                                                            <tr runat="server" id="trOrdenMeritoCuarto">
                                                                                <td>
                                                                                    <table class="tablaInterna" align="center">
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px; width: 150px;">
                                                                                                <asp:Label ID="Label20" runat="server" Text="Orden de Mérito:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtOrdenMeritoCuarto" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="3" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image58" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server" id="trNroAlumnosCuarto">
                                                                                <td>
                                                                                    <table class="tablaInterna" align="center">
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px; width: 150px;">
                                                                                                <asp:Label ID="Label21" runat="server" Text="Nro. Alumnos en su Promoción:"
                                                                                                    CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNroAlmunosCuarto" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                    Width="50px" MaxLength="3" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image59" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <% } %>
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="tablaInterna" align="center">
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px; width: 150px;">
                                                                                                <asp:Label ID="Label22" runat="server" Text="Matemática:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaMateCuarto" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image60" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="tablaInterna" align="center">
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px; width: 150px;">
                                                                                                <asp:Label ID="Label23" runat="server" Text="Comunicación:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaLengCuarto" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image61" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: center;">
                                                                                    <asp:Button ID="btnEditarNotasCuartoNumerico" runat="server" Text="Editar"
                                                                                        CssClass="btnLimpiar" OnClick="btnEditarNotasCuartoNumerico_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <div id="divLetraCuarto" runat="server" visible="false">
                                                                        <table runat="server" id="trLetraCuarto" style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 1%;"></td>
                                                                                            <td style="width: 98%;"></td>
                                                                                            <td style="width: 1%;"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td class="MsgAlerta">(CNE = Compentencia No Evaluada)</td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Ciencia y Tecnología (CT)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Diseña y construye soluciones tecnológicas para resolverproblemas.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CT01CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CT01Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Explica el mundo físico basándose en conocimientos sobre losseres vivos , materia y energía, biodiversidad.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CT02CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CT02Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Indaga mediante métodos científicos para construir sus conocimientos.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CT03CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CT03Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Ciencias Sociales (CS)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Construye interpretaciones históricas.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CS01CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CS01Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Gestiona responsablemente el espacio y el ambiente.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CS02CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CS02Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu> <li><label>Gestiona responsablemente los recursos económicos.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CS03CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CS03Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Comunicación en lengua materna (CL)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Escribe diversos tipos de texto.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CL01CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CL01Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Lee diversos tipos de textos escritos</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CL02CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CL02Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Se comunica oralmente.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CL03CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CL03Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Matemática (MA)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Resuelve problemas de cantidad.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA01CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA01Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Resuelve problemas de forma y movimiento.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA02CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA02Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Resuelve problemas de gestión de datos e incertidumbre.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA03CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA03Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Resuelve problemas de regularidad equivalencia y cambio.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA04CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA04Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <%--Ini:Christian Ramirez - REQ95070--%>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Competencias Transversales (CTR)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Gestiona su aprendizaje de manera autónoma.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CTR01CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CTR01Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Se desenvuelve en los entornos virtuales generados por lasTIC.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CTR02CodigoCuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CTR02Cuarto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <%--Fin:Christian Ramirez - REQ95070--%>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:Button ID="btnEditarNotasCuartoLetra" runat="server" Text="Editar" CssClass="btnLimpiar" OnClick="btnEditarNotasCuartoLetra_Click" Enabled="false" Visible="false"/>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                                <td></td>
                                                                <td style="vertical-align:top;">
                                                                    <div runat="server" id="divNumericoQuinto" visible="false">
                                                                        <%--5to año--%>
                                                                        <table style="width: 100%;">
                                                                            <% if (Session["ModPostulacion"].ToString() != "41")
                                                                            { %>
                                                                                <tr runat="server" id="trOrdenMeritoQuinto">
                                                                                    <td>
                                                                                        <table class="tablaInterna" align="center">
                                                                                            <tr>
                                                                                                <td style="padding-top: 10px; width: 150px;">
                                                                                                    <asp:Label ID="Label24" runat="server" Text="Orden de Mérito:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtOrdenMeritoQuinto" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                        MaxLength="3" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr runat="server" id="trNroAlumnosQuinto">
                                                                                    <td>
                                                                                        <table class="tablaInterna" align="center">
                                                                                            <tr>
                                                                                                <td style="padding-top: 10px; width: 150px;">
                                                                                                    <asp:Label ID="Label25" runat="server" Text="Nro. Alumnos en su Promoción:"
                                                                                                        CssClass="TextoEtiqueta"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtNroAlmunosQuinto" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                        MaxLength="3" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            <% } %>
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="tablaInterna" align="center">
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px; width: 150px;">
                                                                                                <asp:Label ID="Label26" runat="server" Text="Matemática:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaMateQuinto" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                    Width="50px" MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="tablaInterna" align="center">
                                                                                        <tr>
                                                                                            <td style="padding-top: 10px; width: 150px;">
                                                                                                <asp:Label ID="Label27" runat="server" Text="Comunicación:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaLengQuinto" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:Button ID="btnEditarNotasQuintoNumerico" runat="server" Text="Editar" 
                                                                                        CssClass="btnLimpiar" OnClick="btnEditarNotasQuintoNumerico_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <div id="divLetraQuinto" runat="server" visible="false">
                                                                        <table runat="server" id="trLetraQuinto" style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 1%;"></td>
                                                                                            <td style="width: 98%;"></td>
                                                                                            <td style="width: 1%;"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td class="MsgAlerta">(CNE = Compentencia No Evaluada)</td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Ciencia y Tecnología (CT)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Diseña y construye soluciones tecnológicas para resolver problemas.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CT01CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CT01Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Explica el mundo físico basándose en conocimientos sobre los seres vivos , materia y energía, biodiversidad.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CT02CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CT02Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Indaga mediante métodos científicos para construir sus conocimientos.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CT03CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CT03Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Ciencias Sociales (CS)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu> <li><label>Construye interpretaciones históricas.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CS01CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CS01Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Gestiona responsablemente el espacio y el ambiente.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CS02CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CS02Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Gestiona responsablemente los recursos económicos.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CS03CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CS03Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Comunicación en lengua materna (CL)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Escribe diversos tipos de texto.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CL01CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CL01Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Lee diversos tipos de textos escritos</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CL02CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CL02Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Se comunica oralmente.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CL03CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CL03Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Matemática (MA)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Resuelve problemas de cantidad.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA01CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA01Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Resuelve problemas de forma y movimiento.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA02CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA02Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Resuelve problemas de gestión de datos e incertidumbre.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA03CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA03Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Resuelve problemas de regularidad equivalencia y cambio.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="MA04CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="MA04Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <%--Ini:Christian Ramirez - REQ95070--%>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td style="padding: 10px 0px;">
                                                                                                <label class="TextoEtiqueta">Competencias Transversales (CTR)</label>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Gestiona su aprendizaje de manera autónoma.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CTR01CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CTR01Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 80%; padding-left: 15px;">
                                                                                                            <lu><li><label>Se desenvuelve en los entornos virtuales generados por las TIC.</label></li></lu>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:TextBox ID="CTR02CodigoQuinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="10px" Visible="false"
                                                                                                                Enabled="false" Height="25px"></asp:TextBox>
                                                                                                            <asp:TextBox ID="CTR02Quinto" runat="server"
                                                                                                                CssClass="txtCajaTexto Deshabilitado" Width="50px" MaxLength="20"
                                                                                                                Style="text-align: center" Enabled="false" Height="25px"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <%--Fin:Christian Ramirez - REQ95070--%>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:Button ID="btnEditarNotasQuintoLetra" runat="server" Text="Editar" CssClass="btnLimpiar" OnClick="btnEditarNotasQuintoLetra_Click" />
                                                                                </td>
                                                                            </tr>

                                                                        
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <%--Fin:Christian Ramirez - REQ91569--%>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>                                
                                <td style="text-align: right;" colspan="4">
                                    <asp:ImageButton ID="imgGuardarDetalleCompRend" runat="server" ImageUrl="~/Images/Buttons/btnActualizar_Preform.png" OnClick="imgGuardarDetalleCompRend_Click" OnClientClick="return ValidarDetalleCompetencias()" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label28" runat="server" CssClass="tdTextNormal" Text="("></asp:Label>
                                    <asp:Image ID="Image62" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                    <asp:Label ID="Label29" runat="server" CssClass="tdTextNormal" Text=")  "></asp:Label>
                                    <asp:Label ID="Label30" runat="server" CssClass="tdTextNormal" Text="Campos Obligatorios"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                 <td>
                                     <%--Ini:Christian Ramirez - REQ91569--%>
                                     <asp:Button ID="btnMostrarPopupTipoCalificacionNone" Style="display: none;" runat="server" />
                                     <asp:Button ID="btnCerrarPopupTipoCalificacionNone" Style="display: none;" runat="server" />
                                     <ajax:ModalPopupExtender ID="mpePopupTipoCalificacion" runat="server" 
                                         TargetControlID="btnMostrarPopupTipoCalificacionNone" PopupControlID="pnlPopupTipoCalificacion" 
                                         PopupDragHandleControlID="pnlPopupTipoCalificacion" BackgroundCssClass="modalBackground"
                                         CancelControlID="btnCerrarPopupTipoCalificacionNone" Enabled="True" />
                                     <asp:Panel ID="pnlPopupTipoCalificacion" runat="server" CssClass="popup-content" Style="width:100%;">
                                         <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Always" runat="server">
                                             <ContentTemplate>
                                                 <div class="popup-main">
                                                     <div class="popup-header" style="background-color: #2a7cba">
                                                         <h4>
                                                             <asp:Label runat="server" ID="lblPopupTipoCalificacionHeader"></asp:Label>
                                                         </h4>
                                                     </div>
                                                     <div class="popup-content">
                                                         <div>
                                                             <table class="tablaFrame" style="width: 100%;">
                                                                 <tr>
                                                                     <td class="SubTitulo">
                                                                         <asp:Label ID="Label31" runat="server" Text="Notas de Rendimiento y Notas de Secundaria ">
                                                                         </asp:Label>
                                                                         <asp:Label runat="server" ID="lblPopupTipoCalificacionGrado"></asp:Label>
                                                                     </td>
                                                                 </tr>
                                                                 <tr runat="server" id="trPopupAvisoLetra" visible="false">
                                                                     <td>
                                                                         <table style="width:100%">
                                                                             <tr>
                                                                                 <td class="MsgAlerta" style="padding:10px;">(CNE = Compentencia No Evaluada)</td>
                                                                             </tr>
                                                                             <tr>
                                                                                <td colspan="10">
                                                                                    <img src="../Images/ConversionVigesimal.png" width="320" height="150"/>
                                                                                </td>
                                                                            </tr>
                                                                         </table>
                                                                     </td>
                                                                 </tr>
                                                             </table>
                                                         </div>
                                                         <div id="divNotasNumerico" runat="server">
                                                             <table class="tablaFrame" style="width: 100%;">
                                                                 <tr>
                                                                     <td>
                                                                         <table style="width: 100%;">
                                                                             <tr>
                                                                                 <td style="width: 1%;"></td>
                                                                                 <td style="width: 30%;"></td>
                                                                                 <td style="width: 68%;"></td>
                                                                                 <td style="width: 1%;"></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td>
                                                                                     <label class="TextoEtiqueta">Orden de Mérito:</label>
                                                                                 </td>
                                                                                 <td style="text-align: center; padding-top: 15px;">
                                                                                     <asp:TextBox ID="txtOrdenMeritoPopup" runat="server" CssClass="txtCajaTexto" MaxLength="3"
                                                                                         onkeypress="return soloNumeros(this);" Style="text-align: center" Width="80px"></asp:TextBox>
                                                                                     <asp:Image ID="Image63" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td>
                                                                                     <label class="TextoEtiqueta">Nro. Alumnos en su Promoción:</label>
                                                                                 </td>
                                                                                 <td style="text-align: center">
                                                                                     <asp:TextBox ID="txtNroAlmunosPopup" runat="server" CssClass="txtCajaTexto" Width="80px"
                                                                                         onkeypress="return soloNumeros(this);" MaxLength="3" Style="text-align: center"></asp:TextBox>
                                                                                     <asp:Image ID="Image64" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td>
                                                                                     <label class="TextoEtiqueta">Matemática:</label>
                                                                                 </td>
                                                                                 <td style="text-align: center">
                                                                                     <asp:DropDownList ID="ddlNotaMatePopup" runat="server" CssClass="txtTextoCombo" Width="100px"
                                                                                         AutoPostBack="true">
                                                                                     </asp:DropDownList>
                                                                                     <asp:Image ID="Image65" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td>
                                                                                     <label class="TextoEtiqueta">Comunicación:</label>
                                                                                 </td>
                                                                                 <td style="text-align: center">
                                                                                     <asp:DropDownList ID="ddlNotaLengPopup" runat="server" CssClass="txtTextoCombo" Width="100px"
                                                                                         AutoPostBack="true">
                                                                                     </asp:DropDownList>
                                                                                     <asp:Image ID="Image66" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                         </table>
                                                                     </td>
                                                                 </tr>
                                                             </table>
                                                         </div>
                                                         <div id="divNotasLetra" runat="server">
                                                             <table class="tablaFrame" style="width: 100%;">
                                                                 <tr>
                                                                     <td>
                                                                         <table runat="server" id="tbNotasLetra" style="width: 100%;">
                                                                             <tr>
                                                                                 <td style="width: 1%;"></td>
                                                                                 <td style="width: 98%;"></td>
                                                                                 <td style="width: 1%;"></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td style="padding: 10px 0px;">
                                                                                     <label class="TextoEtiqueta">Ciencia y Tecnología (CT)</label>
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td>
                                                                                     <table style="width: 100%">
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                 <li><label id="lblCompetencia1Popup">Diseña y construye soluciones tecnológicas para resolver 
                                                                                                 problemas.</label></li>
                                                                                             </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="CT01Popup" AutoPostBack="false" Style="width: 100px; margin: 0px">
                                                                                                 </asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                         <tr>
                                                                                             <td style="padding-left: 15px;">
                                                                                                 <lu>
                                                                                                 <li><label id="lblCompetencia2Popup"
                                                                                                 >Explica el mundo físico basándose en conocimientos sobre los seres vivos , materia y energía
                                                                                                 , biodiversidad.</label></li>
                                                                                             </lu>
                                                                                             </td>
                                                                                             <td>
                                                                                                 <asp:DropDownList runat="server" ID="CT02Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                         <tr>
                                                                                             <td style="padding-left: 15px;">
                                                                                                 <lu>
                                                                                                 <li><label id="lblCompetencia3Popup">Indaga mediante métodos científicos para construir
                                                                                                 sus conocimientos.</label></li>
                                                                                             </lu>
                                                                                             </td>
                                                                                             <td>
                                                                                                 <asp:DropDownList runat="server" ID="CT03Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                     </table>
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td style="padding: 10px 0px;">
                                                                                     <label class="TextoEtiqueta">Ciencias Sociales (CS)</label>
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td>
                                                                                     <table style="width: 100%">
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                     <li><label id="lblCompetencia4Popup">Construye interpretaciones históricas.</label></li>
                                                                                                 </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="CS01Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                <li><label id="lblCompetencia5Popup">Gestiona responsablemente el espacio y el ambiente.</label></li>
                                                                                              </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="CS02Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif"></td>
                                                                                         </tr>
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                     <li><label id="lblCompetencia6Popup">Gestiona responsablemente los recursos económicos.</label></li>
                                                                                                 </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="CS03Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                     </table>
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td style="padding: 10px 0px;">
                                                                                     <label class="TextoEtiqueta">Comunicación en lengua materna (CL)</label>
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td>
                                                                                     <table style="width: 100%">
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                     <li><label id="lblCompetencia9Popup">Escribe diversos tipos de texto.</label></li>
                                                                                                 </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="CL01Popup" Style="width: 100px; margin: 0px" AutoPostBack="false" ClientIDMode="Static">
                                                                                                 </asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                     <li><label id="lblCompetencia10Popup">Lee diversos tipos de textos escritos</label></li>
                                                                                                 </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="CL02Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                     <li><label id="lblCompetencia11Popup">Se comunica oralmente.</label></li>
                                                                                                 </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="CL03Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                     </table>
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td style="padding: 10px 0px;">
                                                                                     <label class="TextoEtiqueta">Matemática (MA)</label>
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td>
                                                                                     <table style="width: 100%">
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                <li><label id="lblCompetencia12Popup">Resuelve problemas de cantidad.</label></li>
                                                                                             </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="MA01Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                <li><label id="lblCompetencia13Popup">Resuelve problemas de forma y movimiento.</label></li>
                                                                                             </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="MA02Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                     <li><label id="lblCompetencia14Popup">Resuelve problemas de gestión de datos e incertidumbre.</label></li>
                                                                                                 </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="MA03Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                     <li><label id="lblCompetencia15Popup">Resuelve problemas de regularidad equivalencia y 
                                                                                                         cambio.</label></li>
                                                                                                 </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="MA04Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                     </table>
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <%--Ini:Christian Ramirez - REQ95070--%>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td style="padding: 10px 0px;">
                                                                                     <label class="TextoEtiqueta">Competencias Transversales (CTR)</label>
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td></td>
                                                                                 <td>
                                                                                     <table style="width: 100%">
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                 <li><label id="lblCompetencia7Popup">Gestiona su aprendizaje de manera autónoma.</label></li>
                                                                                             </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="CTR01Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                         <tr>
                                                                                             <td style="width: 80%; padding-left: 15px;">
                                                                                                 <lu>
                                                                                                 <li><label id="lblCompetencia8Popup">Se desenvuelve en los entornos virtuales generados por las 
                                                                                                     TIC.</label></li>
                                                                                             </lu>
                                                                                             </td>
                                                                                             <td style="width: 20%">
                                                                                                 <asp:DropDownList runat="server" ID="CTR02Popup" AutoPostBack="false" Style="width: 100px; margin: 0px"></asp:DropDownList>
                                                                                                 <img src="../Images/ico_Required.gif">
                                                                                             </td>
                                                                                         </tr>
                                                                                     </table>
                                                                                 </td>
                                                                                 <td></td>
                                                                             </tr>
                                                                             <%--Fin:Christian Ramirez - REQ95070--%>
                                                                         </table>
                                                                     </td>
                                                                 </tr>
                                                             </table>
                                                         </div>
                                                     </div>
                                                     <div class="popup-footer">
                                                         <table style="width:100%">
                                                             <tr>
                                                                 <td style="width:20%">
                                                                     <asp:LinkButton ID="btnGuardarPopupTipoCalificacion" runat="server"
                                                                        Text="Guardar" OnClick="btnGuardarPopupTipoCalificacion_Click"></asp:LinkButton>
                                                                 </td>
                                                                 <td  style="width:20%">
                                                                     <asp:LinkButton ID="btnCerrarPopupTipoCalificacion" runat="server"
                                                                         OnClick="btnCerrarPopupTipoCalificacion_Click" Text="Cerrar"></asp:LinkButton>
                                                                 </td>
                                                             </tr>
                                                         </table>
                                                     </div>
                                                 </div>
                                             </ContentTemplate>
                                             <Triggers>
                                                 <asp:AsyncPostBackTrigger ControlID="ddlTipoCalificacionTercero" EventName="SelectedIndexChanged" />
                                                 <asp:AsyncPostBackTrigger ControlID="ddlTipoCalificacionCuarto" EventName="SelectedIndexChanged" />
                                                 <asp:AsyncPostBackTrigger ControlID="ddlTipoCalificacionQuinto" EventName="SelectedIndexChanged" />
                                             </Triggers>
                                         </asp:UpdatePanel>
                                     </asp:Panel>
                                     <%--Fin:Christian Ramirez - REQ91569--%>
                                 </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <%-- FIN: Detalle Competencias --%>
                </td>
            </tr>
            <%-- FIN: JC.DelgadoV [RQ103950] Observaciones Pre Formalización --%>
            <tr>
                <td>
                    <asp:Panel ID="pnlArchivos" runat="server">
                        <table style="width:100%" class="tablaInterna">
                            <tr>
                                <td class="SubTitulo">
                                    <asp:Label ID="lblTitulo" runat="server" Text="Carga los siguientes documentos"></asp:Label>
                                </td>                                
                            </tr>                            
                            <tr>
                                <td style="padding-top: 18px;">
                                    <table style="width: 100%;" class="tablaInterna">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upResultado" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gvDocumentos" runat="server" AutoGenerateColumns="False" CssClass="Grilla" EmptyDataText="No se encontraron registros..." OnRowDataBound="gvDocumentos_RowDataBound" Width="100%">
                                                            <HeaderStyle CssClass="HeaderGrilla" />
                                                            <AlternatingRowStyle CssClass="AlternateItemGrilla" />
                                                            <RowStyle CssClass="ItemGrilla" />
                                                            <PagerStyle CssClass="PagerGrilla" />
                                                            <Columns>
                                                                <asp:BoundField />
                                                                <asp:TemplateField HeaderText="Modelo" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigo" runat="server" Style="display: none;" Text='<%# Bind("CodDoc") %>'></asp:Label>
                                                                        <asp:Label ID="lblApplicationId" runat="server" Style="display: none;" Text='<%# Bind("APPLICATION_ID") %>' />
                                                                        <asp:HyperLink ID="hlkDocumento" runat="server" ImageUrl="~/Images/ico_Documento.png" ToolTip="Descargar plantilla del documento" CssClass="tdTextLink">[hlkDocumento]</asp:HyperLink>
                                                                        <asp:Label ID="lblIdDocumento" runat="server" Style="display: none;" Text='<%# Bind("ApplicationAttachmentId") %>' />
                                                                        <asp:Label ID="lblDescEstado" runat="server" Style="display: none;" Text='<%# Bind("Estado") %>' />
                                                                        <asp:Label ID="lblDescripcion" runat="server" Style="display: none;" Text='<%# Bind("Descripcion") %>' />
                                                                        <asp:Label ID="lblObservacion" runat="server" Style="display: none;" Text='<%# Bind("Observacion") %>' />
                                                                        <asp:Label ID="lblGovernmentId" runat="server" Style="display: none;" Text='<%# Bind("GovernmentId") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Descripción de Documentos" DataField="DESCRIPCION" Visible="False">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Descripción de Documentos">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text="Label" CssClass="ItemGrilla"></asp:Label>
                                                                        <asp:Label ID="Label5" runat="server" Text="Label" CssClass="MsgAlerta" Visible="false"></asp:Label>                                                                        
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <%--<asp:BoundField HeaderText="Estado" DataField="Estado" />--%>
                                                                <asp:TemplateField HeaderText="Estado">
                                                                    <ItemTemplate>
                                                                        <table class="tablaInterna" style="width: 5%;">
                                                                            <tr>
                                                                                <td style="text-align: left; white-space:nowrap">
                                                                                    <asp:Label ID="lblEstado" runat="server" Text="Label"></asp:Label>                                                                                    
                                                                                </td>
                                                                                <td>&nbsp;</td>
                                                                                <td style="text-align: center;">
                                                                                    <asp:Image ID="imgEstado" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cargar">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbCargarDoc" runat="server" CausesValidation="false" CssClass="detalleCarga" ImageUrl="~/Images/ico_Upload.png" Height="16px" Width="16px" OnClick="imbCargarDoc_OnClick" />
                                                                        <asp:ImageButton ID="imgDelDoc" runat="server" CausesValidation="false" CssClass="detalleCarga" ImageUrl="~/Images/ico_Delete.png" Height="16px" Width="16px" OnClick="imbDeleteDoc_OnClick" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ver">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbDescargaDoc" runat="server" CausesValidation="false" CssClass="detalleCarga" ImageUrl="~/Images/ico_DownLoad.png" Height="16px" Width="16px" OnClick="imbDescargaDoc_OnClick" />
                                                                        
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    </EditItemTemplate>
                                                                    <ItemStyle Width="5%" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField ControlStyle-Width="0">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text="Label" CssClass="ItemGrilla dni" style="color:transparent" ></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                    </EditItemTemplate>
                                                                    <ItemStyle Width="0" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <input id="hCodDocumento" name="hCodDocumento" runat="server" type="hidden" />
                                                        <input id="hIdDocumento" name="hIdDocumento" runat="server" type="hidden" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="gvDocumentos" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; padding: 10px 0px;">
                                                <table style="width: auto;" border="0" align="center">
                                                    <tr>
                                                        <td style="width: 50%;">
                                                            <asp:Label ID="lblMensajeCarreras" runat="server" CssClass="tdTextoDetalle">Tipo de documentos soportados:</asp:Label>
                                                        </td>
                                                        <td style="width: 16%;">
                                                            <asp:Image ID="imgJPG" runat="server" ImageUrl="~/Images/Ico_JPG.png" Height="32px" Width="32px" />
                                                        </td>
                                                        <td style="width: 16%;">
                                                            <asp:Image ID="imgPdf" runat="server" ImageUrl="~/Images/Ico_PDF.png" Height="32px" Width="32px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <table style="width: 70%;" class="grillaAdm" border="1" align="center">
                                                    <tr>
                                                        <td style="text-align: center;" class="TablaDetalle LetraTablaDetalle" colspan="2">Leyenda</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 32px;" class="TablaDetalle LetraTablaDetalle">Imagen
                                                        </td>
                                                        <td style="text-align: center;" class="TablaDetalle LetraTablaDetalle">Descripción
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; height: 25px;">
                                                            <asp:ImageButton ID="ImageButton2" runat="server"
                                                                ImageUrl="~/Images/ico_Upload.png" ToolTip="En revisión" />
                                                        </td>
                                                        <td>Subir documento.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; height: 25px;">
                                                            <asp:ImageButton ID="imgLeyEstadoDocFAEPG" runat="server"
                                                                ImageUrl="~/Images/icoEnRevision.png" ToolTip="En revisión" />
                                                        </td>
                                                        <td>Documento se encuentra en proceso de revisión.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; height: 25px;">
                                                            <asp:ImageButton ID="imgLeyEstadoDocOKEPG" runat="server" ImageUrl="~/Images/icoObservado.png" ToolTip="Observado" />
                                                        </td>
                                                        <td>Documento fue observado. Postulante debe actualizarlo.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; height: 25px;">
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icoAprobado.png" ToolTip="Aprobado" />
                                                        </td>
                                                        <td>Documento cumple con los requisitos solicitados por admisión.
                                                        </td>
                                                    </tr>

                                                    <%--                                            <tr>
                                                <td align="center">
                                                    <asp:ImageButton ID="imgLeyDescargarEPG" runat="server" ImageUrl="~/Imagenes/Admision V2/download.png"  ToolTip="Descargar archivo" onclick="imgDescargarEPG_Click" />
                                                </td>
                                                <td>
                                                    Descargar documento enviado.
                                                </td>
                                            </tr>--%>
                                                    <%--
                                            <tr>
                                                <td align="center">
                                                    <asp:ImageButton ID="imgLeyPlantillaEPG" runat="server" ImageUrl="~/Imagenes/plantillaEPG.png"  ToolTip="Ver Plantilla" />
                                                </td>
                                                <td>
                                                    Descarga documentos de ejemplos antes de subir el archivo en la documentación
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="center">
                                                    <asp:ImageButton ID="imgLeyUploadEPG" runat="server" ImageUrl="~/Imagenes/uploadEPG.png"  ToolTip="Subir Archivo" />
                                                </td>
                                                <td>
                                                    Permite adjuntar archivos a las documentación
                                                </td>
                                            </tr>
                                                    --%>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Button ID="btnCargaDocumentos" Style="display: none;" runat="server" />
                                    <asp:Button ID="btnCancelarDocumentos" Style="display: none;" runat="server" />
                                    <ajax:ModalPopupExtender ID="mpeCargaDocumentos" runat="server" TargetControlID="btnCargaDocumentos"
                                        PopupControlID="pnlupCargaDocumentos" PopupDragHandleControlID="pnlupCargaDocumentos" BackgroundCssClass="modalBackground"
                                        CancelControlID="btnCancelarDocumentos" Enabled="True" />
                                    <asp:Panel ID="pnlupCargaDocumentos" runat="server" CssClass="modalPopup" Width="80%" Style="display: block;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="upCargaDocumentos" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <table style="width: 100%;" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td class="Titulo">
                                                                                    <asp:Label ID="Label7" runat="server" Text="Carga de Documentos"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width: 100%;" class="tablaFrame">
                                                                                        <tr style="height: 40px;">
                                                                                            <td>
                                                                                                <asp:Label ID="lblDocumento" runat="server" Text="Documento"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 40px;">
                                                                                            <td>
                                                                                                <asp:Label ID="Label8" runat="server" CssClass="tdTextNormal" Text="Seleccione el archivo que desea adjuntar.&lt;strong&gt;Tamaño máximo permitido para el archivo 2 MB."></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <table style="width: 100%;">
                                                                                                    <tr>
                                                                                                        <td style="width: 95%;">
                                                                                                            <asp:FileUpload ID="fuDocumento" runat="server" Width="100%" />
                                                                                                        </td>
                                                                                                        <td style="width: 5%;">
                                                                                                            <asp:RequiredFieldValidator ID="rfvDocumento" runat="server" ControlToValidate="fuDocumento"
                                                                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione achivo a cargar" InitialValue=""
                                                                                                                SetFocusOnError="true" ValidationGroup="registraDocumentos" Text="*" Style="display: inline"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblError" runat="server" CssClass="MsgAlertaIncompleto" Text="Error" Visible="False"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center" class="auto-style1">
                                                                                                <table style="width: 25%; text-align: center" align="center">
                                                                                                    <tr style="height: 40px;">
                                                                                                        <td>
                                                                                                            <asp:Button ID="btnAdjuntar" runat="server" Text="Guardar" OnClick="btnAdjuntar_Click" Width="80px" ValidationGroup="registraDocumentos" />
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Width="80px" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <table style="width: auto;" border="0" align="center">
                                                                                                    <tr style="height: 60px;">
                                                                                                        <td style="width: 50%;">
                                                                                                            <asp:Label ID="Label9" runat="server" CssClass="tdTextoDetalle">Tipo de documentos soportados:</asp:Label>
                                                                                                        </td>
                                                                                                        <td style="width: 16%;">
                                                                                                            <asp:Image ID="imgJPGPopUp" runat="server" ImageUrl="~/Images/Ico_JPG.png" />
                                                                                                        </td>
                                                                                                        <td style="width: 16%;">
                                                                                                            <asp:Image ID="imgPDFPopUp" runat="server" ImageUrl="~/Images/Ico_PDF.png" />
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
                                                                                    <asp:ValidationSummary ID="vlsRegistraDocumentos" runat="server" ShowMessageBox="true" ShowSummary="false"
                                                                                        ValidationGroup="registraDocumentos" CssClass="MsgAlertaIncompleto" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnAdjuntar" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlDatosFinales" runat="server">
                        <table style="width:100%" class="tablaInterna">
                            <tr>
                                <td class="SubTitulo" colspan="5">
                                    <asp:Label ID="lblDatosFinales" runat="server" Text="Datos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 1%;"></td>
                                <td style="padding-top: 10px;" colspan="4">
                                    <span class="TextoEtiqueta">
                                        Cambio de Carrera (Este cambio se puede realizar por única vez en este proceso de formalización y luego se puede realizar
                                        mediante trámite vigente en Servicios Académicos, realiza el cambio solo si estás seguro)
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 1%;"></td>
                                <td style="padding-top: 9px;" colspan="4">
                                    <asp:DropDownList ID="ddlCarrera" runat="server" CssClass="txtTextoCombo"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 1%;"></td>
                                <td style="width: 20%;">
                                    <asp:Label ID="lblSeguroRenta" runat="server" Text="Seguro de renta estudiantil:" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                                <td style="width: 75%; padding-top: 18px;">
                                    <asp:DropDownList ID="ddlSeguroRenta" runat="server" CssClass="txtTextoCombo" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">-- Seleccionar --</asp:ListItem>
                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 1%;" class="requerido">
                                    <asp:Image ID="Image37" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td style="width: 1%;">
                                    <asp:RequiredFieldValidator ID="rfvSeguroRenta" runat="server" ControlToValidate="ddlSeguroRenta"
                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Coloque el código de su seguro" InitialValue="0"
                                        SetFocusOnError="true" ValidationGroup="registraDatosFinales" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 1%;"></td>
                                <td style="width: 20%; text-align: justify;">
                                    <asp:Label ID="Label10" runat="server" Text="No voy a estudiar en el ciclo actual, por lo que solicito reservar mi vacante para el siguiente ciclo: " CssClass="TextoEtiqueta"></asp:Label>
                                    <span class="TextoEtiqueta" style="color:red;">(Al seleccionar la opción de reserva de vacante, se notificará a la Dirección de Servicios Académicos y de Registro, quienes luego les estarán informando sobre el trámite formal).</span>
                                </td>
                                <td style="width: 75%; padding-top: 18px;">
                                    <asp:DropDownList ID="ddlReservaMatricula" runat="server" CssClass="txtTextoCombo" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">-- Seleccionar --</asp:ListItem>
                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 1%;" class="requerido">
                                    <asp:Image ID="Image38" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td style="width: 1%;">
                                    <asp:RequiredFieldValidator ID="rfvReservaMatricula" runat="server" ControlToValidate="ddlReservaMatricula"
                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Escoja una opción" InitialValue="0"
                                        SetFocusOnError="true" ValidationGroup="registraDatosFinales" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 1%;"></td>
                                <td style="width: 20%;">
                                    <asp:Label ID="lblDeportitaDestacado" runat="server" Text="Deportista Destacado:" CssClass="TextoEtiqueta"></asp:Label>
                                </td>
                                <td style="width: 75%; padding-top: 18px;">
                                    <asp:DropDownList ID="ddlDeportistaDestacado" runat="server" CssClass="txtTextoCombo" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">-- Seleccionar --</asp:ListItem>
                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 1%;" class="requerido">
                                    <asp:Image ID="Image39" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                </td>
                                <td style="width: 1%;">
                                    <asp:RequiredFieldValidator ID="rfvDeportistaDestacado" runat="server" ControlToValidate="ddlDeportistaDestacado"
                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Escoja una opción" InitialValue="0"
                                        SetFocusOnError="true" ValidationGroup="registraDatosFinales" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>                            
                            <tr id="trPregConvalidar" runat="server" style="display:none">
                                <td style="width: 1%;"></td>
                                <td style="padding-top: 10px;" colspan="4">
                                    <span class="TextoEtiqueta">
                                        ¿Convalidará los siguientes cursos?
                                    </span>
                                </td>
                            </tr>
                            <tr id="trChLstConvalidar" runat="server" style="display:none">
                                <td style="width: 1%;"></td>
                                <td style="padding-top: 9px;" colspan="4">
                                    <asp:CheckBoxList id="chLstCursosConv" runat="server" CssClass="TextoEtiqueta" ></asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr id="trPregExoneratorios" runat="server" style="display:none">
                                <td style="width: 1%;"></td>
                                <td style="padding-top: 10px;" colspan="4">
                                    <span class="TextoEtiqueta">
                                        ¿Realizará los siguientes exámenes exoneratorios? <br />
                                        (Solo podrá seleccionar los exámenes dependiendo de los cursos que vaya a convalidar).
                                    </span>
                                </td>
                            </tr>
                            <tr id="trChLstExoneratorios" runat="server" style="display:none">
                                <td style="width: 1%;"></td>
                                <td style="padding-top: 9px;" colspan="4">
                                    <asp:CheckBoxList id="chLstExamenes" runat="server" CssClass="TextoEtiqueta"></asp:CheckBoxList>
                                </td>
                            </tr>
                            <%-- INICIO: JC.DelgadoV [RQ103036] Encuesta Retorno a clases (class: encuestaRC)--%>
                            <tr class="encuestaRGC">
                                <td class="SubTitulo" colspan="5">
                                    <asp:Label ID="Label11" runat="server" Text="Declaración de intención de retorno a clases en modalidad online o semipresencial"></asp:Label>
                                </td>
                            </tr>
                            <tr class="encuestaRGC">
                                <td style="width: 1%;"></td>
                                <td style="padding-top: 10px; text-align: justify;" colspan="4">
                                    <span class="TextoEtiquetaEncuesta">
                                        Estimadas y estimados seleccionados: <br /> <br />
                                        La información vertida en el siguiente acápite será tomada como insumo principal para el diseño de horarios según los aforos permitidos para el proceso de matrícula 2022-I, 
                                        le solicitamos completar esta información con especial cuidado considerando su situación y preferencia actual. En el caso de los seleccionados menores de edad se recomienda completar esta información en compañía de sus padres y/o apoderados. <br /> <br />
                                    </span>
                                </td>
                            </tr>
                            
                            <tr class="encuestaRGC">
                                <td colspan=" 4">
                                    <table style="width: 100%;" class="tablaInterna">
                                        <%-- Pregunta 1 --%>
                                        <tr class="encuestaRGC">
                                            <td style="width: 1%;"></td>
                                            <td style="padding-top: 10px;" colspan="4">
                                                <span id="encuestaRGC_P1" runat="server" class="TextoEtiquetaEncuesta"></span>
                                            </td>
                                        </tr>
                                        <tr class="encuestaRGC">
                                            <td style="width: 1%;"></td>
                                            <td style="width: 90%;padding-top: 9px;">
                                                <asp:DropDownList ID="ddlEncuestaRGC_R1" runat="server" CssClass="txtTextoCombo" DataValueField="codigo" DataTextField="descripcion"></asp:DropDownList>
                                            </td>
                                            <td style="width: 1%;" class="requerido">
                                                <asp:Image ID="Image40" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                            </td>
                                            <td style="width: 1%;">
                                                <%--<asp:RequiredFieldValidator ID="rfvEncuestaRGC" runat="server" ControlToValidate="ddlEncuestaRGC_R1"
                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione una alternativa para la pregunta 1" InitialValue="0"
                                                    SetFocusOnError="true" ValidationGroup="registraDatosFinales" Text="*"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <%-- Pregunta 2 --%>
                                        <tr class="encuestaRGC">
                                            <td style="width: 1%;"></td>
                                            <td style="padding-top: 10px; text-align: justify;" colspan="4">
                                                <span id="encuestaRGC_P2" runat="server" class="TextoEtiquetaEncuesta"></span>
                                            </td>
                                        </tr>
                                        <tr class="encuestaRGC">
                                            <td style="width: 1%;"></td>
                                            <td style="width: 90%;padding-top: 9px;">
                                                <asp:DropDownList ID="ddlEncuestaRGC_R2" runat="server" CssClass="txtTextoCombo" DataValueField="codigo" DataTextField="descripcion"></asp:DropDownList>
                                            </td>
                                            <td style="width: 1%;" class="requerido">
                                                <asp:Image ID="Image41" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                            </td>
                                            <td style="width: 1%;">
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEncuestaRGC_R2"
                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione una alternativa para la pregunta 2" InitialValue="0"
                                                    SetFocusOnError="true" ValidationGroup="registraDatosFinales" Text="*"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <%-- Pregunta 3 --%>
                                        <tr class="encuestaRGC">
                                            <td style="width: 1%;"></td>
                                            <td style="padding-top: 10px; text-align: justify;" colspan="4">
                                                <span id="encuestaRGC_P3" runat="server" class="TextoEtiquetaEncuesta"></span>
                                            </td>
                                        </tr>
                                        <tr class="encuestaRGC">
                                            <td style="width: 1%;"></td>
                                            <td style="width: 90%;padding-top: 9px;">
                                                <%--<asp:UpdatePanel ID="UpdatePanel7" runat="server" >
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlEncuestaRGC_R3" runat="server" CssClass="txtTextoCombo" DataValueField="codigo" DataTextField="descripcion"
                                                    OnSelectedIndexChanged="ddlEncuestaRGC_R3_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>--%>

                                                <asp:DropDownList ID="ddlEncuestaRGC_R3" runat="server" CssClass="txtTextoCombo" DataValueField="codigo" DataTextField="descripcion"
                                                    OnSelectedIndexChanged="ddlEncuestaRGC_R3_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </td>
                                            <td style="width: 1%;" class="requerido">
                                                <asp:Image ID="Image42" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                            </td>
                                            <td style="width: 1%;">
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEncuestaRGC_R3"
                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione una alternativa para la pregunta 3" InitialValue="0"
                                                    SetFocusOnError="true" ValidationGroup="registraDatosFinales" Text="*"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <%-- Pregunta 4 --%>
                                        <tr class="encuestaRGC">
                                            <td style="width: 1%;"></td>
                                            <td style="padding-top: 10px; text-align: justify;" colspan="4">
                                                <span id="encuestaRGC_P4" runat="server" class="TextoEtiquetaEncuesta"></span>
                                            </td>
                                        </tr>
                                        <tr class="encuestaRGC">
                                            <td style="width: 1%;"></td>
                                            <td style="width: 90%;padding-top: 9px;">
                                                <asp:TextBox ID="txtEncuestaRGC_R4" runat="server" CssClass="txtCajaTexto" MaxLength="500" onkeydown="return (event.keyCode!=13);" Width="97%"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%;" class="requerido">
                                                <asp:Image ID="Image43" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                            </td>
                                            <td style="width: 1%;">
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEncuestaRGC_R4"
                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Es necesario responder la pregunta 4" InitialValue=""
                                                    SetFocusOnError="true" ValidationGroup="registraDatosFinales" Text="*"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <%-- Pregunta 5 --%>
                                        <tr class="encuestaRGC" id="trP5" runat="server" visible="false">
                                            <td style="width: 1%;"></td>
                                            <td style="padding-top: 10px; text-align: justify;" colspan="4">
                                                <span id="encuestaRGC_P5" runat="server" class="TextoEtiquetaEncuesta"></span>
                                            </td>
                                        </tr>
                                        <tr class="encuestaRGC" id="trA5" runat="server" visible="false">
                                            <td style="width: 1%;"></td>
                                            <td style="width: 90%;padding-top: 9px;">
                                                <asp:DropDownList ID="ddlEncuestaRGC_R5" runat="server" CssClass="txtTextoCombo" DataValueField="codigo" DataTextField="descripcion"></asp:DropDownList>
                                            </td>
                                            <td style="width: 1%;" class="requerido">
                                                <asp:Image ID="Image44" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                            </td>
                                            <td style="width: 1%;">
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlEncuestaRGC_R5"
                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione una alternativa para la pregunta 5" InitialValue="0"
                                                    SetFocusOnError="true" ValidationGroup="registraDatosFinales" Text="*"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <%-- Pregunta 6 --%>
                                        <tr class="encuestaRGC" id="trP6" runat="server" visible="false">
                                            <td style="width: 1%;"></td>
                                            <td style="padding-top: 10px; text-align: justify;" colspan="4">
                                                <span id="encuestaRGC_P6" runat="server" class="TextoEtiquetaEncuesta"></span>
                                            </td>
                                        </tr>
                                        <tr class="encuestaRGC" id="trA6" runat="server" visible="false">
                                            <td style="width: 1%;"></td>
                                            <td style="width: 90%;padding-top: 9px;" >
                                                <asp:DropDownList ID="ddlEncuestaRGC_R6" runat="server" CssClass="txtTextoCombo" DataValueField="codigo" DataTextField="descripcion"></asp:DropDownList>
                                            </td>
                                            <td style="width: 1%;" class="requerido">
                                                <asp:Image ID="Image45" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                            </td>
                                            <td style="width: 1%;">
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlEncuestaRGC_R6"
                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione una alternativa para la pregunta 6" InitialValue="0"
                                                    SetFocusOnError="true" ValidationGroup="registraDatosFinales" Text="*"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>           
                                    </table>
                                </td>
                            </tr>
                                             
                            <%-- FIN: JC.DelgadoV [RQ103036] Encuesta Retorno a clases (class: encuestaRC)--%>
                            <tr>
                                <td class="SubTitulo" colspan="5">
                                    <asp:Label ID="Label12" runat="server" Text="Autorización de uso de datos"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 1%;"></td>
                                <td style="padding-top: 9px;" colspan="4">
                                    <asp:CheckBox id="chkUsoDatos" runat="server" CssClass="TextoEtiqueta" Text ="Autoriza uso de datos."></asp:CheckBox>
                                </td>
                            </tr>                            
                            <tr>
                                <td style="width: 1%;"></td>
                                <td style="padding-top: 9px;" colspan="4">
                                    <asp:CheckBox id="chkUsoDatosTerceros" runat="server" CssClass="TextoEtiqueta" Text ="Autoriza uso de datos con terceros."></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>                                
                                <td style="text-align: right;" colspan="5">
                                    <asp:ImageButton ID="btnGuardarDatosFinales" runat="server" ImageUrl="~/Images/Buttons/btnGuardar_Preform.png" OnClick="imgGuardarDatosFinales_Click" ValidationGroup="registraDatosFinales" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 1%;"></td>
                                <td style="padding-top: 10px;" colspan="4">
                                    <span class="TextoEtiqueta" style="color:red">
                                        *Debe enviar su expediente para que la Dirección de Admisión pueda revisarlo.
                                        <br />
                                        *Al hacer clic en enviar expediente ya no podrá realizar cambio alguno (excepto volver a subir los documentos que sean OBSERVADOS).
                                    </span>
                                </td>
                            </tr>
                            <tr>                                
                                <td style="text-align: right;" colspan="5">
                                    <asp:ImageButton ID="btnEnviarExpedientes" runat="server" ImageUrl="~/Images/Buttons/btnEnviarExpediente.png" OnClick="btnEnviarExpedientes_Click" OnClientClick="return confirmarFormalizacion()"/>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlMensajeCierreFormalizacion" runat="server" Visible ="false">
                        <table style="width: 100%;" class="tablaInterna">
                            <tr>
                                <td colspan="2" style="text-align:right; width: 50%;">
                                    <asp:Label ID="lblAvisoFinFormalizacion" runat="server" Text="La formalización finalizó: " CssClass="TextoEtiqueta" ></asp:Label>
                                </td>
                                <td colspan="2" style="text-align:left">
                                    <asp:Label ID="lblFechaFinFormalizacion" runat="server" CssClass="tdTextNormal"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr style="text-align: justify;">
                <td>
                    <br />
                    <asp:Label ID="lblMensajeInformativoInf" runat="server" CssClass="tdTextNormal"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <%--<asp:ImageButton ID="imgBtnEnviar" runat="server" ImageUrl="~/Images/Buttons/btnGuardar_Preformalizacion.png" OnClick="imgBtnEnviar_Click" ValidationGroup="registraHorario" />--%>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnMostrarError" Style="display: none;" runat="server" />
                    <asp:Button ID="btnCancelarError" Style="display: none;" runat="server" />
                    <ajax:ModalPopupExtender ID="mpeMostrarError" runat="server" TargetControlID="btnMostrarError"
                        PopupControlID="pnlMostrarError" PopupDragHandleControlID="pnlMostrarError" BackgroundCssClass="modalBackground"
                        CancelControlID="btnCancelarError" Enabled="True" />
                    <asp:Panel ID="pnlMostrarError" runat="server" CssClass="popup-content" Width="50%" Style="display: block;">
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
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnSalir_Click">Cerrar</asp:LinkButton>
                                    </div>
                                </div>
                            </ContentTemplate>
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

    <div id="divPopupMensaje" class="popup">
        <div class="popup-contenido">
            <div class="popup-cabecera-mensaje">
                ¡Ayuda!
            </div>
            <div class="popup-contenido-mensaje">
                <img alt="" src="../Images/dni_ubigeo.jpg" />
            </div>
            <div class="popup-footer-mensaje">
                <input id="btnCerrar" type="button" value="Cerrar" class="popup-footer-mensaje-cerrar" />
            </div>
        </div>
    </div>
    
    <script>
        $(document).ready(function () {
            var estadoExpediente = $('#lblEstadoFormalizacion').text();

            if (estadoExpediente == 'EXPEDIENTE ENVIADO') {
                deshabilitarExamenes();
            } else {
                comprobarExamen();
            }

            /*Ini:Christian Ramirez - REQ91569*/
            Sys.UI.Point = function Sys$UI$Point(x, y) {

                x = Math.round(x);
                y = Math.round(y);

                var e = Function._validateParams(arguments, [
                    { name: "x", type: Number, integer: true },
                    { name: "y", type: Number, integer: true }
                ]);
                if (e) throw e;
                this.x = x;
                this.y = y;
            }
            /*Fin:Christian Ramirez - REQ91569*/

            //INI: JC.DelgadoV[RQ103950] Observaciones Pre Formalización
            $("#ddlAnioLectivoTercero").change(function () {
                var $valor = $(this)
                if ($valor.val() > $("#ddlAnioLectivoCuarto").val()) {
                    $("#ddlAnioLectivoTercero").focus();
                    $valor.css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                    //alert("El 3er año debe ser menor al 4to y 5to año de secundaria");
                    $("#lblmessage").html("El 3er año debe ser menor al 4to y 5to año de secundaria.")
                    $find("mpeMostrarError").show();
                } else {
                    $valor.css("border-color", "")
                    if ($("#ddlAnioLectivoCuarto").val() > $valor.val()) {
                        $("#ddlAnioLectivoCuarto").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
                    }
                }
            })

            $("#ddlAnioLectivoCuarto").change(function () {
                var $valor = $(this)
                if ($valor.val() > $("#ddlAnioLectivoQuinto").val() ||
                    $valor.val() < $("#ddlAnioLectivoTercero").val()) {
                    $("#ddlAnioLectivoCuarto").focus();
                    $valor.css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                    //alert("El 4to año debe ser menor al 5to año y mayor al 3er año de secundaria");
                    $("#lblmessage").html("El 4to año debe ser menor al 5to año y mayor al 3er año de secundaria.")
                    $find("mpeMostrarError").show();
                } else {
                    $valor.css("border-color", "")
                    if ($("#ddlAnioLectivoTercero").val() < $valor.val()) {
                        $("#ddlAnioLectivoTercero").css("border-color", "")
                    }
                    if ($("#ddlAnioLectivoQuinto").val() > $valor.val()) {
                        $("#ddlAnioLectivoQuinto").css("border-color", "")
                    }
                }
            })

            $("#ddlAnioLectivoQuinto").change(function () {
                var $valor = $(this)
                if ($valor.val() < $("#ddlAnioLectivoCuarto").val()) {
                    $("#ddlAnioLectivoQuinto").focus();
                    $valor.css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                    //alert("El 5to año debe ser mayor al 3er o 4to año de secundaria");
                    $("#lblmessage").html("El 5to año debe ser mayor al 3er o 4to año de secundaria.")
                    $find("mpeMostrarError").show();
                } else {
                    $valor.css("border-color", "")
                    if ($("#ddlAnioLectivoCuarto").val() < $valor.val()) {
                        $("#ddlAnioLectivoCuarto").css("border-color", "")
                    }
                }
            })
            //FIN: JC.DelgadoV[RQ103950] Observaciones Pre Formalización
        })
        function confirmarFormalizacion() {
            var respuesta = false;
            var flagDocumentos = true;
            var flagCantidadCompetencias5to = true;
            var flagDetalleCompetencias5to = true;
            var situacionAcademica = '<%= Session["SituacionAcademica"].ToString() %>';

            //Validando Datos Personales
            Page_ClientValidate('registraDatoPersonal');
            if (!Page_ClientValidate('registraDatoPersonal')) {
                return false;
            }
            //**************************

            //Validando Datos Familiares
            Page_ClientValidate('registraPadres');
            if (!Page_ClientValidate('registraPadres')) {
                return false;
            }
            //**************************

            //Validando Archivos
            $('#gvDocumentos > tbody > tr').each(function (i) {
                var docOpcional = false;
                if (i > 0) {
                    var estado = $(this).find('td').eq(2).find('table').eq('0').find('tbody').eq(0).find('tr').eq(0).find('td').eq(0).find('span')[0].innerText;
                    var descripcion = ($(this).find('td').eq(1)[0].innerText).toUpperCase();

                    if (descripcion.indexOf('OPCIONAL') != -1) {
                        docOpcional = true;
                    }                    

                    if (estado == 'Pendiente' && docOpcional == false) {
                        flagDocumentos = false;
                    }
                }                
            })
            //**************************

            //Validando Datos Finales
            Page_ClientValidate('registraDatosFinales');
            if (!Page_ClientValidate('registraDatosFinales')) {
                return false;
            }
            //**************************

            console.log(situacionAcademica);

            if (situacionAcademica != "34") {
                //Validando Cantidad Competencias
                if ($('#txtTotalADQuinto').val() == '' ||
                    $('#txtTotalAQuinto').val() == '' ||
                    $('#txtTotalBQuinto').val() == '' ||
                    $('#txtTotalCQuinto').val() == '' ||
                    $('#txtTotalCompetenciasQuinto').val() == '') {

                    flagCantidadCompetencias5to = false;
                    $('#txtTipoNota5').focus();
                    alert('Es necesario colocar la cantidad de competencias de 5to.');
                }
                //**************************

                //Validando Detalle Competencias

                if ($('#ddlColegioQuinto').val() == '0') {

                    flagDetalleCompetencias5to = false;
                    $('#ddlColegioQuinto').focus();
                    alert('Es necesario colocar el colegio de 5to.');
                }

                if ($('#ddlAnioLectivoQuinto').val() == '0') {

                    flagDetalleCompetencias5to = false;
                    $('#ddlAnioLectivoQuinto').focus();
                    alert('Es necesario colocar año en el que curso 5to.');
                }

                if ($('#ddlTipoCalificacionQuinto').val() == '0') {

                    flagDetalleCompetencias5to = false;
                    $('#ddlTipoCalificacionQuinto').focus();
                    alert('Es necesario colocar el tipo de calificación de 5to.');
                }

                if ($('#ddlTipoCalificacionQuinto').val() != '0') {
                    flagDetalleCompetencias5to = ValidarDetalleCompetencias();
                }

            //**************************
            }

            if (!flagDocumentos) {
                respuesta = false;
                alert('Es necesario subir todos los documentos solicitados para continuar con la formalización.');
            }
            else {
                respuesta = true;
            }

            if (situacionAcademica == "34") {
                flagCantidadCompetencias5to = true;
                flagDetalleCompetencias5to = true;
            }

            if (!flagDocumentos || !flagCantidadCompetencias5to || !flagDetalleCompetencias5to) {
                respuesta = false;
            } else {
                respuesta = true;
            }

            return respuesta;            
        }

        //Validando Convalidación y Exámenes exoneratorios
        //$('#chLstCursosConv_0').change(function () { comprobarExamen(chLstCursosConv_0) });
        //$('#chLstCursosConv_1').change(function () { comprobarExamen(chLstCursosConv_1) });
        $('#chLstCursosConv_0').change(function () { comprobarExamen() });
        $('#chLstCursosConv_1').change(function () { comprobarExamen() });

        function comprobarExamen() {
            //Deshabilitando los exámenes
            deshabilitarExamenes();

            //Habilitando según convalidación
            if ($('#chLstCursosConv_0').is(':checked')) {
                $('#chLstExamenes_0').removeAttr('disabled');
            }

            if ($('#chLstCursosConv_1').is(':checked')) {
                $('#chLstExamenes_1').removeAttr('disabled');
            }

            if ($('#chLstCursosConv_0').is(':checked') && $('#chLstCursosConv_1').is(':checked')) {
                $('#chLstExamenes_2').removeAttr('disabled');
            }

            //Quitando check según los cursos de convalidación seleccionados
            if (!$('#chLstCursosConv_0').is(':checked')) {
                $('#chLstExamenes_0').prop('checked', false);
                $('#chLstExamenes_2').prop('checked', false);
            }

            if (!$('#chLstCursosConv_1').is(':checked')) {
                $('#chLstExamenes_1').prop('checked', false);
                $('#chLstExamenes_2').prop('checked', false);
            }
        }

        function deshabilitarExamenes() {
            $('#chLstExamenes_0').attr('disabled', true);
            $('#chLstExamenes_1').attr('disabled', true);
            $('#chLstExamenes_2').attr('disabled', true);
        }

        //INI: JC.DelgadoV[RQ103950] Observaciones Pre Formalización
        function valOrdMeritoCantAlmunTercero() {
            if (parseInt($("#txtOrdenMeritoTercero").val()) > parseInt($("#txtNroAlmunosTercero").val())) {
                $("#txtOrdenMeritoTercero").focus();
                $("#txtOrdenMeritoTercero").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                //alert("El orden de mérito no pude ser mayor al número de alumnos en el tercer año");
                $("#lblmessage").html("El orden de mérito no pude ser mayor al número de alumnos en el tercer año.")
                $find("mpeMostrarError").show();
            }
            else {
                $("#txtOrdenMeritoTercero").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
            }
        };

        function valOrdMeritoCantAlmunCuarto() {
            if (parseInt($("#txtOrdenMeritoCuarto").val()) > parseInt($("#txtNroAlmunosCuarto").val())) {
                $("#txtOrdenMeritoCuarto").focus();
                $("#txtOrdenMeritoCuarto").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                //alert("El orden de mérito no pude ser mayor al número de alumnos en el cuarto año");
                $("#lblmessage").html("El orden de mérito no pude ser mayor al número de alumnos en el cuarto año.")
                $find("mpeMostrarError").show();
            }
            else {
                $("#txtOrdenMeritoCuarto").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
            }
        };

        function valOrdMeritoCantAlmunQuinto() {
            if (parseInt($("#txtOrdenMeritoQuinto").val()) > parseInt($("#txtNroAlmunosQuinto").val())) {
                $("#txtOrdenMeritoQuinto").focus();
                $("#txtOrdenMeritoQuinto").css("border-color", "aliceblue").css("border-top-width", "2px").css("border-right-width", "2px").css("border-bottom-width", "2px").css("border-left-width", "2px")
                //alert("El orden de mérito no pude ser mayor al número de alumnos en el quinto año");
                $("#lblmessage").html("El orden de mérito no pude ser mayor al número de alumnos en el quinto año.")
                $find("mpeMostrarError").show();
            }
            else {
                $("#txtOrdenMeritoQuinto").css("border-color", "").css("border-top-width", "").css("border-right-width", "").css("border-bottom-width", "").css("border-left-width", "")
            }
        };

        $('.txtCajaTexto').click(function () {
            $(this).focus().select();
        })

        function ValidarDetalleCompetencias() {
            resultado = true;

            if ($('#ddlColegioQuinto').val() == "0") {

                $("#lblmessage").html("Es necesario seleccionar el colegio de quinto.")
                $find("mpeMostrarError").show();
                $('#ddlColegioQuinto').focus();

                resultado = false;
            }

            if ($('#ddlAnioLectivoQuinto').val() == "0") {

                $("#lblmessage").html("Es necesario seleccionar el año que curso quinto.")
                $find("mpeMostrarError").show();
                $('#ddlAnioLectivoQuinto').focus();

                resultado = false;
            }

            if ($('#ddlTipoCalificacionQuinto').val() == "0") {

                $("#lblmessage").html("Es necesario seleccionar el tipo de calificación de quinto.")
                $find("mpeMostrarError").show();
                $('#ddlTipoCalificacionQuinto').focus();

                resultado = false;
            }

            if ($('#CT01Quinto').val() == "" ||
                $('#CT02Quinto').val() == "" ||
                $('#CT03Quinto').val() == "" ||
                $('#CS01Quinto').val() == "" ||
                $('#CS02Quinto').val() == "" ||
                $('#CS03Quinto').val() == "" ||
                $('#CL01Quinto').val() == "" ||
                $('#CL02Quinto').val() == "" ||
                $('#CL03Quinto').val() == "" ||
                $('#MA01Quinto').val() == "" ||
                $('#MA02Quinto').val() == "" ||
                $('#MA03Quinto').val() == "" ||
                $('#MA04Quinto').val() == "" ||
                $('#CTR01Quinto').val() == "" ||
                $('#CTR02Quinto').val() == ""
               ) {

                $("#lblmessage").html("Es necesario colocar el detalle de las competencias de quinto.")
                $find("mpeMostrarError").show();
                $('#btnEditarNotasQuintoLetra').focus();

                resultado = false;
            }

            return resultado;
        }

        function soloNumerosFormalizacion(txt) {
            if (event.keyCode <= 47 || event.keyCode > 57) {
                event.returnValue = false;
            }
        }

        //FIN: JC.DelgadoV[RQ103950] Observaciones Pre Formalización

    </script>
    <script src="../JavaScript/Webforms/frm20_FormalizaIng.js"></script>
</body>
</html>
