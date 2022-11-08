<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm21_ModalidadColegioPostula_BackUp.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm21_ModalidadColegioPostula" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>UP - Admisión Pregrado</title>

    <link href="../Styles/Estilos.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/font-awesome.min.css" rel="stylesheet" />
    <link href="../Styles/css/paginador.css" rel="stylesheet" />

    <script src="../Styles/js/tooltip.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validate.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery.validationEngine-en.js" type="text/javascript"></script>
    <script src="../JavaScript/jquery-ui.js" type="text/javascript"></script>
    <script src="../JavaScript/json2.js" type="text/javascript"></script>
    <script src="../JavaScript/JS.js" type="text/javascript"></script>
    <script src="../JavaScript/thickbox.js" type="text/javascript"></script>

    <style type="text/css">
        .auto-style4 {
            height: 110px;
        }

        .auto-style5 {
            width: 50%;
            height: 110px;
        }
        .auto-style6 {
            width: 3%;
            height: 30px;
        }
        .auto-style8 {
            width: 2%;
            height: 30px;
        }
        .auto-style9 {
            height: 18px;
        }
        </style>

    <script type="text/javascript">
        let colegioCar = "";
        let colegioCoar = "";
        let colegioBahillerato = "";
        let colegioProvincia = "";

        /*Ini:Christian Ramirez - REQ91569*/
        function LimpiarModalidades() {
            var trModadlidadPostulacion = document.getElementById("trModadlidadPostulacion");
            if (trModadlidadPostulacion !== null)trModadlidadPostulacion.remove();
        }
        /*Fin:Christian Ramirez - REQ91569*/

        function ValidarColegio(codModular, anioAcademico) {

            /*INI:CHRISTIAN RAMIREZ - REQ90692*/
            if (sessionStorage.getItem("codModular") != null)
                codModular = sessionStorage.getItem("codModular");
            /*FIN:CHRISTIAN RAMIREZ - REQ90692*/

            PageMethods.getTipoColegio(codModular, anioAcademico, function (rpta) {
                if (rpta !== null) {

                    let tiposColegio = rpta.split('|');
                    let ntiposColegio = tiposColegio.length;

                    let modalidades = document.querySelectorAll("table[id='rblModalidad']");
                    if (modalidades.length > 0) {
                        modalidades = document.querySelectorAll("table[id='rblModalidad']")[0].querySelectorAll("tr");
                    }

                    let nModalidades = modalidades.length;
                    let codModalidad;

                    if (rpta !== "") {
                        for (var i = 0; i < ntiposColegio; i++) {
                            if (tiposColegio[i] == "CAR") colegioCar = "CAR";
                            if (tiposColegio[i] == "COAR") colegioCoar = "COAR";
                            if (tiposColegio[i] == "BCHIN") colegioBahillerato = "BCHIN"
                        }

                        for (var i = 0; i < nModalidades; i++) {
                            modalidades[i].setAttribute("style", "display:block");
                        }

                        //-> Todos los colegios deben ver las modalidades de Selectiva y Regular
                        if (colegioCar === "" && colegioBahillerato === "" && colegioCoar === "") {
                            for (var i = 0; i < nModalidades; i++) {
                                codModalidad = modalidades[i].querySelector("input[type='radio']").value;
                                if (codModalidad === "49") modalidades[i].setAttribute("style", "display:none"); //Excelencia Académica
                                if (codModalidad === "58") modalidades[i].setAttribute("style", "display:none"); //BI Mitad Superior
                                if (codModalidad === "57") modalidades[i].setAttribute("style", "display:none"); //BI Quinto Superior
                                if (codModalidad === "59") modalidades[i].setAttribute("style", "display:none"); //BI Excelencia
                                if (codModalidad === "60") modalidades[i].setAttribute("style", "display:none"); //BI Selectiva
                            }
                        }
                        //->    Solo los colegios CAR y no Bachillerato deben mostrar las modalidades de Excelencia
                        //      , Selectiva y Regular No va COAR || colegioCoar !== ""
                        else if ((colegioCar !== "") && colegioBahillerato === "") {
                            for (var i = 0; i < nModalidades; i++) {
                                codModalidad = modalidades[i].querySelector("input[type='radio']").value;
                                if (codModalidad === "58") modalidades[i].setAttribute("style", "display:none"); //BI Mitad Superior
                                if (codModalidad === "57") modalidades[i].setAttribute("style", "display:none"); //BI Quinto Superior
                                if (codModalidad === "59") modalidades[i].setAttribute("style", "display:none"); //BI Excelencia
                                if (codModalidad === "60") modalidades[i].setAttribute("style", "display:none"); //BI Selectiva
                            }
                        }
                        //->    Solo los colegios No CAR y Bachillerato deben mostrar las modalidades de Quinto Bachillerato
                        //      , Medio Bachillerato, Selectiva y Regular//&& colegioCoar === ""
                        else if (colegioBahillerato !== "" && (colegioCar === "")) {
                            for (var i = 0; i < nModalidades; i++) {
                                codModalidad = modalidades[i].querySelector("input[type='radio']").value;
                                if (codModalidad === "49") modalidades[i].setAttribute("style", "display:none"); //Excelencia Académica
                                if (codModalidad === "59") modalidades[i].setAttribute("style", "display:none"); //BI Excelencia
                            }
                        }
                    } else {
                        //-> Todos los colegios deben ver las modalidades de Selectiva y Regular
                        for (var i = 0; i < nModalidades; i++) {
                            codModalidad = modalidades[i].querySelector("input[type='radio']").value;
                            if (codModalidad === "49") modalidades[i].setAttribute("style", "display:none"); //Excelencia Académica
                            if (codModalidad === "58") modalidades[i].setAttribute("style", "display:none"); //BI Mitad Superior
                            if (codModalidad === "57") modalidades[i].setAttribute("style", "display:none"); //BI Quinto Superior
                            if (codModalidad === "59") modalidades[i].setAttribute("style", "display:none"); //BI Excelencia
                            if (codModalidad === "60") modalidades[i].setAttribute("style", "display:none"); //BI Selectiva
                        }
                    }
                }
            });
        }

        $("document").ready(function () {
            
            $("#txtColegio1").autocomplete({
                source: function (request, response) {

                    PageMethods.getColegios(request.term,
                        function (data) {
                            var colegios = (typeof data) == 'string' ? eval('(' + data + ')') : data;

                            response($.map(colegios, function (item) {
                                return {
                                    label: item.NombreInstitucion.toUpperCase(),
                                    value: item.NombreInstitucion.toUpperCase(),
                                    id: item.IdEducacion,
                                    IdEducacion: item.IdEducacion,
                                    NombreInstitucion: item.NombreInstitucion.toUpperCase(),
                                    Direccion: item.Direccion.toUpperCase(),
                                    CiudadInstitucion: item.CiudadInstitucion.toUpperCase(),
                                    DepartamentoDes: item.DepartamentoDes.toUpperCase(),
                                    ModCode: item.ModCode
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

                    if (hddAplicaValidacion.value === "1") {
                        /*INI:CHRISTIAN RAMIREZ - REQ90692*/
                        sessionStorage.setItem('codModular', ui.item.ModCode);
                        //ValidarColegio(sessionStorage.getItem("codModular"));/*Se comenta:Christian Ramirez - REQ91569*/
                        /*FIN:CHRISTIAN RAMIREZ - REQ90692*/

                        /*Ini:Christian Ramirez - REQ91569*/
                        var txtCodigoModular1 = document.getElementById("txtCodigoModular1").value;
                        if (txtCodigoModular1 !== "") {
                            document.getElementById("trBotonOpcion").setAttribute("style", "display:block");
                            LimpiarModalidades();
                        }
                        /*Fin:Christian Ramirez - REQ91569*/
                    }
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
                                    label: item.NombreInstitucion.toUpperCase(),
                                    value: item.NombreInstitucion.toUpperCase(),
                                    id: item.IdEducacion,
                                    IdEducacion: item.IdEducacion,
                                    NombreInstitucion: item.NombreInstitucion.toUpperCase(),
                                    Direccion: item.Direccion.toUpperCase(),
                                    CiudadInstitucion: item.CiudadInstitucion.toUpperCase(),
                                    DepartamentoDes: item.DepartamentoDes.toUpperCase(),
                                    ModCode: item.ModCode
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
                                    label: item.NombreInstitucion.toUpperCase(),
                                    value: item.NombreInstitucion.toUpperCase(),
                                    id: item.IdEducacion,
                                    IdEducacion: item.IdEducacion,
                                    NombreInstitucion: item.NombreInstitucion.toUpperCase(),
                                    Direccion: item.Direccion.toUpperCase(),
                                    CiudadInstitucion: item.CiudadInstitucion.toUpperCase(),
                                    DepartamentoDes: item.DepartamentoDes.toUpperCase(),
                                    ModCode: item.ModCode
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
        <div>
            <input runat="server" id="hddCodModular" type="hidden"/>
            <input runat="server" id="hddAplicaValidacion" type="hidden" value="1"/>
        </div>
        <div>
            <table style="width: 100%; margin: auto;">
                <tr>
                    <td>
                        <table style="width: 100%; visibility: hidden">
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
                        <%--colegio procedencia--%>
                        <div class="adm-box">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <table style="width: 100%;" class="SubTitulo tablaInterna ">
                                            <tr class="SubTitulo">
                                                <td style="width: 70%;" class="SubTitulo">
                                                    <asp:Label ID="lblTitulo" runat="server" Text="Información de Colegio de Procedencia"></asp:Label>
                                                </td>
                                                <td style="width: 20%;">
                                                    <asp:Label ID="lblPasos" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 10%; text-align: right;">
                                                    <asp:ImageButton ID="imgBtnVideo" runat="server" ImageUrl="~/Images/Buttons/btnVideo.png" 
                                                        OnClick="imgBtnVideo_Click" ToolTip="Ver video" Visible="False" Height="32px" Width="32px" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:Label ID="lblMensajeColegio" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr id="trColegioProcedencia" runat="server">
                                    <td>
                                        <table style="width: 100%;" runat="server">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;" class="tablaFrame">
                                                        <tr>
                                                            <td style="width: 3%; height: 30px;"></td>
                                                            <td style="width: 15%;">
                                                                <asp:Label ID="lblEgresado" runat="server" Text="Situación Académica:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 25%; text-align: left; padding-top: 5px;">
                                                                <asp:DropDownList ID="ddlSitAcademica" runat="server" CssClass="txtTextoCombo" 
                                                                    OnSelectedIndexChanged="ddlSitAcademica_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            </td>
                                                            <td style="width: 1%;" class="requerido">
                                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                            </td>
                                                            <td style="width: 1%;">
                                                                <asp:RequiredFieldValidator ID="rfvEgresado" runat="server" ControlToValidate="ddlSitAcademica"
                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija su situación académica" InitialValue="0"
                                                                    SetFocusOnError="true" ValidationGroup="registraModalidad" Text="*">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 15%;">
                                                                <asp:Label ID="lblPrimNom8" runat="server" Text="¿En cuántos colegios estudiaste de 3ero a 5to secundaria?:" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="width: 25%; text-align: left; padding-top: 5px;">
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
                                                                <asp:RequiredFieldValidator ID="rfvNroColegios" runat="server" ControlToValidate="ddlNroColegios" 
                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija el numero de colegios."
                                                                    InitialValue="0" SetFocusOnError="true" Text="*" ValidationGroup="registraModalidad">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tablaFrameDesc" style="background: rgba(51,122,183,0.3); padding: 5px; font-size: 11px; border-radius: 10px;">
                                                    <div class="box-icon">
                                                        <i class="fa fa-info-circle"></i>
                                                    </div>
                                                    <div class="box-content">
                                                        <asp:Label ID="lblExplicacion1" runat="server" Text="Nota:" CssClass="tdTextoDetalle" Font-Bold="True"></asp:Label><br />
                                                        <asp:Label ID="lblExplicacion" runat="server" Text="Considerar que se deberán registrar los colegios de 5to a 3ero de secundaria (en ese orden), solo en caso de haber estudiado en más de un colegio durante los últimos tres años.<br><br> La búsqueda del colegio puede demorar unos segundos, por favor, espere a que el sistema le muestre la lista de opciones." CssClass="tdTextoDetalle"></asp:Label>
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
                                                                                <asp:Label ID="lblColegio1" runat="server" Text="Ingresa tu colegio actual (en curso) o del que egresaste"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="BtnElimina1" runat="server" CssClass="btnQuitar" OnClick="btnElimina1_Click" 
                                                                                    Text="Quitar Colegio" ToolTip="Eliminar Datos de Colegio" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="lblBuscarColegio" runat="server" Text="Colegio:" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="padding-top: 10px; width: 90%;">
                                                                    <asp:TextBox ID="txtColegio1" runat="server" CssClass="txtCajaTexto txtSensitive" placeholder="Ejemplo: Trilce Lima" 
                                                                        MaxLength="100"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 1%;" class="requerido">
                                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="rfvColegio" runat="server" ControlToValidate="txtCodColegio1" 
                                                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese nombre de colegio." InitialValue="" SetFocusOnError="true"
                                                                        Text="Colegio incorrectamente ingresado. La búsqueda del colegio puede demorar unos segundos, por favor, espere a que el sistema le muestre la lista de opciones y luego verifique que los campos de código modular y dirección estén completos. "
                                                                        ForeColor="Red" ValidationGroup="registraModalidad" style="font-size:11px;"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCodigoModular" runat="server" Text="Código Modular:" CssClass="TextoEtiqueta" Width="110px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCodigoModular1" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 1%;" class="requerido"></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDireccionColegio" runat="server" Text="Dirección:" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDireccionColegio1" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="100"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 1%;" class="requerido"></td>
                                                                <td>
                                                                    <div style="display: none;">
                                                                        <asp:TextBox ID="txtCodColegio1" runat="server" Width="100px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtCodColegio2" runat="server" Width="100px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtCodColegio3" runat="server" Width="100px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtIdEducacion1" runat="server" Width="100px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtIdEducacion2" runat="server" Width="100px"></asp:TextBox>
                                                                        <asp:TextBox ID="txtIdEducacion3" runat="server" Width="100px"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
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
                                                                                <asp:Label ID="lblColegio2" runat="server" Text="Otro Colegio (4to o 3ero de secundaria)"></asp:Label>
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
                                                                    <asp:RequiredFieldValidator ID="rfvColegio2" runat="server" ControlToValidate="txtCodColegio2" CssClass="MsgAlertaIncompleto" 
                                                                        ErrorMessage="Ingrese nombre de colegio." InitialValue="" SetFocusOnError="true"
                                                                        Text="Colegio incorrectamente ingresado. La búsqueda del colegio puede demorar unos segundos, por favor, espere a que el sistema le muestre la lista de opciones y luego verifique que los campos de código modular y dirección estén completos."
                                                                        ForeColor="Red" ValidationGroup="registraModalidad" style="font-size:11px;"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCodigoModular2" runat="server" Text="Código Modular:" CssClass="TextoEtiqueta" Width="110px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCodigoModular2" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 1%;" class="requerido"></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDireccionColegio2" runat="server" Text="Dirección:" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDireccionColegio2" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="100"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 1%;" class="requerido"></td>
                                                                <td></td>
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
                                                                                <asp:Label ID="lblColegio3" runat="server" Text="Otro Colegio (3ero de Secundaria)"></asp:Label>
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
                                                                    <asp:RequiredFieldValidator ID="rfvColegio3" runat="server" ControlToValidate="txtCodColegio3" CssClass="MsgAlertaIncompleto" 
                                                                        ErrorMessage="Ingrese nombre de colegio." InitialValue="" SetFocusOnError="true"
                                                                        Text="Colegio incorrectamente ingresado. La búsqueda del colegio puede demorar unos segundos, por favor, espere a que el sistema le muestre la lista de opciones y luego verifique que los campos de código modular y dirección estén completos. "
                                                                        ForeColor="Red" ValidationGroup="registraModalidad" style="font-size:11px;"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCodigoModular3" runat="server" Text="Código Modular:" CssClass="TextoEtiqueta" Width="110px"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:TextBox ID="txtCodigoModular3" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="20"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 1%;" class="requerido"></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDireccionColegio3" runat="server" Text="Dirección:" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDireccionColegio3" runat="server" CssClass="txtCajaTexto Deshabilitado" MaxLength="100"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 1%;" class="requerido"></td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%--Ini:Christian Ramirez - REQ91569--%>
                        <%--Notas de Compentencias--%>
                        <div class="adm-box">
                            <table style="width: 100%;">
                                <tr id="trCompetencias" runat="server" style="display:none;">
                                    <td>
                                        <table style="width: 100%;" class="tablaFrame">
                                            <tr>
                                                <td class="SubTitulo" colspan="4">
                                                    <table style="width: 100%;" class="tablaInterna">
                                                        <tr>
                                                            <td>
                                                                <label>Ingresa las cantidades de tus competencias de 4to de secundaria:</label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="padding-top:5px;">
                                                    <asp:Image runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    <span id="lblMensajeCantidadCompentenciaAD01" runat="server" style="color:red; font-size:11px; font-family: Tahoma;">
                                                        Campos obligatorios, si hay algún campo vacío se tomará 
                                                        como valor cero</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 8%;">
                                                    <asp:Label runat="server" Text="¿Cuántas (AD) obtuviste?" CssClass="TextoEtiqueta"></asp:Label>
                                                </td>
                                                <td style="width:42%; padding-top: 10px;">
                                                    <asp:TextBox ID="txtAds" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                                        placeholder="Digite valor numérico" onkeypress="return soloNumeros(this);"></asp:TextBox>
                                                </td>
                                                <td style="width: 8%;">
                                                    <asp:Label runat="server" Text="¿Cuántas (A) obtuviste?" CssClass="TextoEtiqueta"></asp:Label>
                                                </td>
                                                <td style="width:42%; padding-top: 10px;">
                                                    <asp:TextBox ID="txtAs" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                                        placeholder="Digite valor numérico" onkeypress="return soloNumeros(this);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" Text="¿Cuántas (B) obtuviste?" CssClass="TextoEtiqueta"></asp:Label>
                                                </td>
                                                <td style="padding-top: 10px;">
                                                    <asp:TextBox ID="txtBs" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                                        placeholder="Digite valor numérico" onkeypress="return soloNumeros(this);"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" Text="¿Cuántas (C) obtuviste?" CssClass="TextoEtiqueta"></asp:Label>
                                                </td>
                                                <td style="padding-top: 10px;">
                                                    <asp:TextBox ID="txtCs" runat="server" CssClass="txtCajaTexto" MaxLength="3" Text="0"
                                                        placeholder="Digite valor numérico" onkeypress="return soloNumeros(this);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" Text="¿Total de competencias?" CssClass="TextoEtiqueta"></asp:Label>
                                                </td>
                                                <td style="padding-top: 10px;">
                                                     <asp:TextBox ID="txtTotalCompetencia" runat="server" CssClass="txtCajaTexto" MaxLength="4" Text="0"
                                                         placeholder="Digite valor numérico" onkeypress="return soloNumeros(this);"></asp:TextBox>
                                                </td>
                                                <td>
                                                     <asp:Button ID="btnEditarCantidadCompetencia" runat="server" Text="Editar" CssClass="btnLimpiar" OnClick="btnEditarCantidadCompetencia_Click"/>
                                                </td>
                                                <td>
                                                    <span id="spMensajeBotonEditar" runat="server" style="color:red; font-size:11px; font-family: Tahoma;">
                                                       Para modificar las competencias, debe dar click en el botón "Editar"</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="adm-box">
                            <table style="width: 100%;">
                                <tr id="trInformacionModalidad" runat="server" style="display:none;">
                                    <td class="tablaFrameDesc" style="background: rgba(51,122,183,0.3); padding: 5px; font-size: 11px; border-radius: 10px;">
                                        <div class="box-icon">
                                            <i class="fa fa-info"></i>
                                        </div>
                                        <div class="box-content">
                                            <asp:Label ID="Label3" runat="server" Text="Modalidad Postulación:" CssClass="tdTextoDetalle" Font-Bold="True"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblInformacionModalidad" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                            <%--Text="Ingrese su colegio actual para que pueda seleccionar la modalidad de postulación"--%>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%--Modalidad postulacion--%>
                        <div class="adm-box">
                            <table style="width: 100%;">
                                <tr class="SubTitulo">
                                    <td class="SubTitulo" style="width: 70%;">
                                        <asp:Label ID="Label4" runat="server" Text="Modalidad de Postulación"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trCamposObligatorios" runat="server">
                                    <td style="text-align: left;" class="auto-style9">
                                        <asp:Image ID="Image18" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                        <asp:Label ID="lblMensajeModalidadPostulacion01" runat="server" Text="Campos Obligatorios"
                                            Style="color: red; font-size: 11px; font-family: Tahoma;"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trBotonOpcion" runat="server">
                                    <td style="padding: 8px 0px;">
                                        <asp:Button ID="btnAgregarModalidad" runat="server" Text="Agregar Modalidad" CssClass="btnAgregar"
                                            OnClick="btnAgregarModalidad_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:UpdatePanel runat="server" ID="upAnioAcademico" UpdateMode="Always">
                            <ContentTemplate>
                                <div id="divModalidadPostulacion" class="adm-box" runat="server">
                                    <table style="width: 100%;">
                                        <tr >
                                            <td>
                                                <asp:Label ID="lblMensajeAvisoModalidad" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                <asp:Label ID="lblTituloMensajeDB" runat="server" CssClass="TextoEtiqueta" Text="Selecciona la modalidad:" Visible="false"></asp:Label>
                                                <asp:Label ID="lblMensajeDB" runat="server" CssClass="tdTextoDetalle" Visible="false"></asp:Label>
                                            </td>
                                            <tr>
                                                <td>
                                                    <table class="tablaFrame" style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <table id="trModadlidadPostulacion" runat="server" class="tablaFrame"  style="display:none;">
                                                                                <tr>
                                                                                    <td style="width: 3%; height: 30px;"></td>
                                                                                    <td class="SubTitulo" style="width: 50%; padding-top: 15px;">
                                                                                        <asp:Label ID="lblModalidadPostulacion" runat="server" Text="Modalidades de admisión"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 2%;"></td>
                                                                                    <td style="width: 3%;"></td>
                                                                                    <td class="SubTitulo" style="width: 40%; padding-top: 15px;">
                                                                                        <asp:Label ID="lblCarreraTitulo" runat="server" Text="Carreras"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 2%;"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="auto-style4"></td>
                                                                                    <td class="auto-style5" style="text-align: left; vertical-align: middle;">
                                                                                        <asp:Label ID="lbltextPeriodo" runat="server" CssClass="TextoEtiqueta" Text="Selecciona el periodo al cual vas a postular:"></asp:Label>
                                                                                        <asp:RadioButtonList ID="rblAnioAca" runat="server" AutoPostBack="true" CssClass="radioButtonList" 
                                                                                            OnSelectedIndexChanged="rblAnioAca_SelectedIndexChanged">
                                                                                        </asp:RadioButtonList>
                                                                                        <asp:Label ID="lblMsjePeriodo" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                                                        <br />
                                                                                        <asp:Label ID="lblTextModa" runat="server" CssClass="TextoEtiqueta" Text="Selecciona la modalidad:"></asp:Label>
                                                                                        <asp:RadioButtonList ID="rblModalidad" runat="server" AutoPostBack="true" CssClass="radioButtonList" 
                                                                                            OnSelectedIndexChanged="rblModalidad_SelectedIndexChanged">
                                                                                        </asp:RadioButtonList>
                                                                                        <asp:Label ID="lblMensajeModalidad" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                                                    </td>
                                                                                    <td class="auto-style4" style="text-align: left; vertical-align: bottom;">
                                                                                        <br />
                                                                                        <br />
                                                                                        <br />
                                                                                        <asp:RequiredFieldValidator ID="rfvModalidad" runat="server" ControlToValidate="rblModalidad"
                                                                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Elija una modalidad de postulación." InitialValue=""
                                                                                            SetFocusOnError="true" ValidationGroup="registraModalidad" Text="*"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td class="auto-style4" style="text-align: left"></td> 
                                                                                    <td class="auto-style5" style="text-align: left; vertical-align: middle; padding-top: 10px;">
                                                                                        <asp:RadioButtonList ID="rblCarrera" runat="server" CssClass="radioButtonList" OnSelectedIndexChanged="rblCarrera_SelectedIndexChanged" 
                                                                                            AutoPostBack="true">
                                                                                        </asp:RadioButtonList>
                                                                                        <asp:Label ID="lblMensajeCarreras" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                                                    </td>
                                                                                    <td class="auto-style4" style="text-align: left">
                                                                                        <asp:RequiredFieldValidator ID="rfvCarrera" runat="server" ControlToValidate="rblCarrera"
                                                                                        CssClass="MsgAlertaIncompleto" ErrorMessage="Elija una carrera." InitialValue=""
                                                                                        SetFocusOnError="true" ValidationGroup="registraModalidad" Text="*"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <%--Tipos de evaluacion--%>
                                                        <tr id="trTipoEvaluacion" runat="server" style="display: none">
                                                            <td>
                                                                <table class="tablaFrame" style="width: 100%;">
                                                                    <tr>
                                                                        <td class="auto-style6"></td>
                                                                        <td class="SubTitulo" style="width: 50%; padding-top: 15px;">
                                                                            <asp:Label ID="lblTipoEvaluacion" runat="server" Text="Tipo de Evaluación"></asp:Label>
                                                                        </td>
                                                                        <td class="auto-style8"></td>
                                                                        <td class="auto-style6"></td>
                                                                        <td class="SubTitulo" style="width: 50%; padding-top: 15px;">
                                                                            <asp:Label ID="lblBeca" runat="server" Text="Beca"></asp:Label>
                                                                        </td>
                                                                        <td class="auto-style8">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td style="text-align: left; vertical-align: middle; width: 50%;">
                                                                            <asp:Label ID="lblMsjeEvaluacion" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                                            <asp:RadioButtonList ID="rblEvaluacion" runat="server" CssClass="radioButtonList">
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td class="auto-style4" style="text-align: left; vertical-align: bottom;">
                                                                            <br />
                                                                           <%-- <% if (Session["ModPostulacion"].ToString() != "42" && Session["ModPostulacion"].ToString() != "52")
                                                                                {
                                                                            %>
                                                                            <asp:RequiredFieldValidator ID="rfdTipoEvaluacion" runat="server" ControlToValidate="rblEvaluacion"
                                                                                CssClass="MsgAlertaIncompleto" ErrorMessage="Elija un tipo de evaluación." InitialValue=""
                                                                                SetFocusOnError="true" ValidationGroup="registraModalidad" Text="*"></asp:RequiredFieldValidator>
                                                                            <% } %>--%>
                                                                        </td>
                                                                        <td style="text-align: left"></td>
                                                                        <td style="text-align: left; vertical-align: middle; width: 50%;">
                                                                            <asp:Label ID="lblMsjeBeca" runat="server" CssClass="tdTextoDetalle"></asp:Label>
                                                                            <asp:RadioButtonList ID="rblBeca" runat="server" CssClass="radioButtonList">
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td colspan="4" style="text-align: justify">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 1px;"></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <%--mensajes de aviso--%>
                                                        <tr>
                                                            <td style="text-align: justify; padding-bottom: 5px;">
                                                                <% if (Session["ModPostulacion"] != null) { %>
                                                                <div id="DescripcionMod" runat="server" class="tablaFrameDesc2">
                                                                    <div class="box-icon">
                                                                        <i class="fa fa-info-circle"></i>
                                                                    </div>
                                                                    <div class="box-content">
                                                                        <asp:Label ID="lblDescripcionModalidad" runat="server" CssClass="tdTextoDetalle2"></asp:Label>
                                                                        <% if (Session["ModPostulacion"].ToString() == "49") { %>
                                                                        <asp:Label ID="lblDescripcionDetalle0" runat="server" CssClass="tdTextoDetalle2"></asp:Label>
                                                                        <asp:Label ID="lblDescripcionDetalle" runat="server" CssClass="tdTextoDetalle2"> <span class="mensaje-alerta"></span></asp:Label>
                                                                        <% } %>
                                                                    </div>
                                                                </div>
                                                                <% } %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: justify;"><% if (Session["ModPostulacion"] != null)
                                                               {
                                                            %>
                                                                <div id="Div1" runat="server" class="tablaFrameDesc2">
                                                                    <div class="box-icon">
                                                                        <i class="fa fa-info-circle"></i>
                                                                    </div>
                                                                    <div class="box-content">
                                                                        <asp:Label ID="lblAviso" runat="server" CssClass="tdTextoDetalle2"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <% } %></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <div id="divBotones" class="adm-box" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <table class="controles" style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/Images/Buttons/btnPrev.png" ToolTip="Página anterior" OnClick="imgBtnBack_Click" />
                                                        </td>
                                                        <td></td>
                                                        <td style="text-align: right">
                                                            <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" 
                                                                ToolTip="Página Siguiente" OnClick="imgBtnNext_Click"
                                                                ValidationGroup="registraModalidad" Style="height: 36px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--Fin:Chritian Ramirez - REQ91569--%>
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
                            } catch (e) { ; }

                            function ApplicationLoadHandler(sender, args) {
                                try {
                                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                                    if (!prm.get_isInAsyncPostBack()) {
                                        prm.add_initializeRequest(initRequest);
                                        prm.add_endRequest(endRequest);
                                    }
                                } catch (e) { ; }
                            }

                            function initRequest(sender, args) {
                                try {
                                    var pop = $find("ModalPopupExtender1");
                                    pop.show();

                                } catch (e) { ; }
                            }

                            function endRequest(sender, args) {
                                try {
                                    var pop = $find("ModalPopupExtender1");
                                    pop.hide();
                                } catch (e) { ; }
                            }
                        </script>
                    </td>
                </tr>
            </table>
        </div>
		<%--Ini:Chritian Ramirez - REQ91569--%>
        <div>
            <ajax:ModalPopupExtender ID="mpeAviso" runat="server" TargetControlID="main" BackgroundCssClass="modalBackground"
                CancelControlID="btnCerrarPopuAviso" PopupControlID="divPopupAviso" Enabled="True"/>
            <div id="divPopupAviso">
                <div class="popup-main">
                    <div class="popup-header" style="background-color: #2a7cba">
                        <h4>¡Aviso!</h4>
                    </div>
                    <div class="popup-content">
                        <p>
                            <asp:Label runat="server" ID="lblContenidoPopupAviso"></asp:Label></p>
                    </div>
                    <div class="popup-footer">
                        <asp:LinkButton ID="btnCerrarPopuAviso" runat="server" OnClick="btnCerrarPopuAviso_Click">Cerrar</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
		<%--Fin:Chritian Ramirez - REQ91569--%>
    </form>
    <script type="text/javascript">
        var UrlAcion = {
            UrlGetStepsAdmision: 'frm21_ModalidadColegioPostula.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
    <script type="text/javascript" src="../JavaScript/Webforms/frm21_ModalidadColegioPostula.js"></script>
</body>
</html>
