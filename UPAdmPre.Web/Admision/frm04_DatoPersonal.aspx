<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm04_DatoPersonal.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm04_DatoPersonal" Culture="es-PE" %>

<%@ Register Src="../UserControl/cuwControlFecha.ascx" TagName="cuwControlFecha" TagPrefix="ucd" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP - Admisión Pregrado</title>
    <link href="../Styles/Utilitarios.css" rel="stylesheet" /> <%--Se agrega: Christian Ramirez[GIIT] Caso43692 - 20180423--%>
    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/font-awesome.min.css" rel="stylesheet" />
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery-ui.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validate.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine-en.js" type="text/javascript"></script>
    <script src="../JavaScript/json2.js" type="text/javascript"></script>
    <script src="../JavaScript/JS.js" type="text/javascript"></script>
    <script src="../JavaScript/thickbox.js" type="text/javascript"></script>
    <link href="../Styles/css/paginador.css" rel="stylesheet" />
    <script type="text/javascript" src="../Styles/js/tooltip.js"></script>
    <style type="text/css">
        .auto-style2 {
            width: 1%;
        }
    </style>
    <script type="text/javascript">
        $("document").ready(function () {

            // validate signup form on keyup and submit
            $("#frm04_DatoPersonal").validate({
                rules: {
                    txtEmail1: {
                        required: false,
                        email: true
                    },
                    txtEmail2: {
                        required: false,
                        email: true
                    },
                },
                messages: {
                    txtEmail1: "Debe ingresar un correo válido.",
                    txtEmail2: "Debe ingresar un correo válido."
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
    <form id="frm04_DatoPersonal" runat="server">
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
                        <table style="width: 100%;" class="tablaInterna">
                            <tr>
                                <td class="SubTitulo">
                                    <table style="width: 100%;" class="tablaInterna">
                                        <tr>
                                            <td style="width: 70%;">
                                                <asp:Label ID="lblTitulo" runat="server" Text="Información Personal"></asp:Label>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:Label ID="lblPasos" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 10%;text-align: right;">
                                                <asp:ImageButton ID="imgBtnVideo" runat="server" ImageUrl="~/Images/Buttons/btnVideo.png" OnClick="imgBtnVideo_Click" ToolTip="Ver video" Visible="False" Height="32px" Width="32px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;" class="tablaInterna">
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="tablaInterna">
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
                                                    <%--Ini: Christian Ramirez[GIIT] Caso43692 - 20180423--%>
                                                    <tr runat="server" id="trUbigeo"> <%--Se modifica: Christian Ramirez - GIIT [Caso 47019] - 20180627--%>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" CssClass="TextoEtiqueta" Text="Ubigeo de Nacimiento:"></asp:Label>
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
                                                                        <%--Ini: Christian Ramirez GIIT [Caso45607] - 20180531--%>
                                                                        <span id="spnUbigeoRequerido" style="display:none;">
                                                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                        </span>
                                                                        <%--Fin: Christian Ramirez GIIT [Caso45607] - 20180531--%>
                                                                    </td>
                                                                    <td style="width: 5px;">
                                                                        <%--Ini: Christian Ramirez GIIT [Caso45607] - 20180531--%>
                                                                        <%--<asp:RequiredFieldValidator ID="rfvUbigeoNacimiento" runat="server" ControlToValidate="txtUbigeoNacimiento"
                                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese su Codigo de Ubigeo de Nacimiento." InitialValue=""
                                                                            SetFocusOnError="true" ValidationGroup="registraDatoPersonal" Text="*"></asp:RequiredFieldValidator>--%>
                                                                        <span id="spnUbigeoRequeridoIcono" style="display:none;">
                                                                            <img src="../Images/ico_Alerta.gif" />
                                                                        </span>
                                                                        <%--Fin: Christian Ramirez GIIT [Caso45607] - 20180531--%>
                                                                    </td>
                                                                    <%--Ini:: Christian Ramirez - GIIT [Caso 48662] - 20180730--%>
                                                                    <td>
                                                                        <span id="spnUbigeo" style="color:red; display:none;">
                                                                            Debe ingresar los 6 digitos del ubigeo
                                                                        </span>
                                                                    </td>
                                                                    <%--Fin:: Christian Ramirez - GIIT [Caso 48662] - 20180730--%>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <%--Fin: Christian Ramirez[GIIT] Caso43692 - 20180423--%>
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
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="SubTitulo">
                                    <asp:Label ID="Label2" runat="server" Text="Datos de Contacto"></asp:Label>
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
                                <td style="text-align: left;">
                                    <asp:Label ID="Label4" runat="server" CssClass="tdTextNormal" Text="("></asp:Label>
                                    <asp:Image ID="Image18" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                    <asp:Label ID="Label5" runat="server" CssClass="tdTextNormal" Text=")  "></asp:Label>
                                    <asp:Label ID="Label3" runat="server" CssClass="tdTextNormal" Text="Campos Obligatorios"></asp:Label>
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
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" ValidationGroup="registraDatoPersonal" />
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
                                            <asp:Label runat="server" ID="lblmessage" CssClass="tdTextNormalAlerta"></asp:Label>
                                        </p>
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
    <%--Ini: Christian Ramirez[GIIT] Caso43692 - 20180423--%>
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
            UrlGetStepsAdmision: 'frm04_DatoPersonal.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
    <script src="../JavaScript/Webforms/frm04_DatoPersonal.js"></script> <%--Se agrega: Christian Ramirez[GIIT] Caso43692 - 20180423--%>
</body>
</html>
