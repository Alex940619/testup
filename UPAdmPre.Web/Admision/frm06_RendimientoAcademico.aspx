<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm06_RendimientoAcademico.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm06_RendimientoAcademico" Culture="es-PE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UP - Admisión Pregrado</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <%--<base target="_self" />--%>
</head>
<body>
    <div id="contentStepsAdmision"></div>
    <form id="form1" runat="server">
        <div id="main" runat="server">
            <ajax:ToolkitScriptManager ID="scr_principal" runat="Server" EnablePageMethods="True"></ajax:ToolkitScriptManager>
        </div>
        <table style="width: 100%;">
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
                                <td class="SubTitulo">
                                    <table style="width: 100%" class="tablaInterna">
                                        
                                        <tr>
                                            <td style="width: 40%;">
                                                <asp:Label ID="lblTitulo" runat="server" Text="Situación Académica Actual"></asp:Label>
                                            </td>
                                            <td style="width: 50%;">
                                                <asp:Label ID="lblPasos" runat="server"></asp:Label>
                                                 <div id="Div2" runat="server" class="tablaFrameDesc2" visible="false">
                                                    <div class="box-icon">
                                                        <i class="fa fa-info-circle"></i>
                                                    </div>
                                                    <div id="Div3"  runat="server"   class="box-content" visible="false">
                                                        <asp:Label ID="Label1" runat="server" CssClass="tdTextoDetalle2" Text=""></asp:Label>
                                                        <%--<a href="https://docs.google.com/forms/d/e/1FAIpQLSfSwTEE8LwavQg5RC0nspQaWX_SES9desFn6Y_hs4cqIhvpfw/viewform?gxids=7628" class="tdTextNormal" target="_blank">CLIC AQUÍ</a>--%>
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
                                                                                <asp:Image ID="Image42" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                <asp:Image ID="Image45" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                <asp:Image ID="Image43" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                <asp:Image ID="Image44" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                <asp:Image ID="Image46" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                    <%if (Session["SituacionAcademica"].ToString() == "63")
                                                                        { %>
                                                                            <label class="MsgAlerta" style="font-size: 9px;">
                                                                                Opcional, la información debe ser completada obligatoriamente durante el 
                                                                                            proceso de formalización.</label>
                                                                    <%  } %>
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
                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoCalificacionTercero"
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
                                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTipoCalificacionCuarto"
                                                                                    CssClass="MsgAlertaIncompleto" ErrorMessage="Elija un tipo de calificación" InitialValue="0"
                                                                                    SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td></td>
                                                                <td class="requerido">
                                                                    <table class="tablaInterna" align="center">
                                                                       <%-- <tr>
                                                                            <td>
                                                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                <label class="MsgAlerta" style="font-size:9px;"> Opcional, la información debe ser completada obligatoriamente durante el 
                                                                                    proceso de formalización.</label>
                                                                            </td>
                                                                        </tr>--%>
                                                                        <tr>
                                                                            <td style="padding-left: 25px;">
                                                                                <asp:DropDownList ID="ddlTipoCalificacionQuinto" runat="server" CssClass="txtTextoCombo"
                                                                                    Width="130px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCalificacionQuinto_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <% if(Session["SituacionAcademica"].ToString() == "63") 
                                                                                { %>
                                                                                    <td style="padding-left: 40px;"></td>
                                                                                    <td></td>
                                                                            <%  } 
                                                                                else { %>

                                                                                    <td class="requerido">
                                                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTipoCalificacionQuinto"
                                                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Elija un tipo de calificación" InitialValue="0"
                                                                                            SetFocusOnError="true" ValidationGroup="registraOrdenMerito" Text="*"></asp:RequiredFieldValidator>
                                                                                    </td>    

                                                                            <%  } %>
                                                                             
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
                                                                    <asp:Label ID="lblRendimientoAcademico" runat="server" Text="Notas de Rendimiento y Notas de Secundaria"></asp:Label>
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
                                                                                                <asp:Label ID="Label15" runat="server" Text="Orden de Mérito:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtOrdenMeritoTercero" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="3" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image17" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                                <asp:Label ID="Label16" runat="server" Text="Nro. Alumnos en su Promoción:"
                                                                                                    CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNroAlmunosTercero" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="3" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                                <asp:Label ID="Label17" runat="server" Text="Matemática:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaMateTercero" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image12" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                                <asp:Label ID="Label18" runat="server" Text="Comunicación:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaLengTercero" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                    Width="50px" MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:Button ID="btnEditarNotasTerceroNumerico" runat="server" Text="Editar"
                                                                                        CssClass="btnLimpiar" OnClick="btnEditarNotasTerceroNumerico_Click" />
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
                                                                                                <asp:Label ID="Label6" runat="server" Text="Orden de Mérito:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtOrdenMeritoCuarto" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="3" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                                <asp:Label ID="Label7" runat="server" Text="Nro. Alumnos en su Promoción:"
                                                                                                    CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNroAlmunosCuarto" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                    Width="50px" MaxLength="3" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                                <asp:Label ID="Label10" runat="server" Text="Matemática:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaMateCuarto" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                                <asp:Label ID="Label13" runat="server" Text="Comunicación:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaLengCuarto" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                        <table runat="server" id="trLetraCuarto" style="width: 100%; font-size: 12px;">
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
                                                                                                            <lu><li><label>Se desenvuelve en los entornos virtuales generados por las TIC.</label></li></lu>
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
                                                                                    <asp:Button ID="btnEditarNotasCuartoLetra" runat="server" Text="Editar" CssClass="btnLimpiar" OnClick="btnEditarNotasCuartoLetra_Click" />
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
                                                                                                <asp:Label ID="Label8" runat="server" Text="Orden de Mérito:" CssClass="TextoEtiqueta"></asp:Label>
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
                                                                                                <asp:Label ID="Label9" runat="server" Text="Nro. Alumnos en su Promoción:"
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
                                                                                                <asp:Label ID="Label11" runat="server" Text="Matemática:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaMateQuinto" runat="server" CssClass="txtCajaTexto Deshabilitado"
                                                                                                    Width="50px" MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                                                <asp:Label ID="Label14" runat="server" Text="Comunicación:" CssClass="TextoEtiqueta"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtNotaLengQuinto" runat="server" CssClass="txtCajaTexto Deshabilitado" Width="50px"
                                                                                                    MaxLength="20" Style="text-align: center" Enabled="false"></asp:TextBox>
                                                                                                <asp:Image ID="Image19" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                        <table runat="server" id="trLetraQuinto" style="width: 100%; font-size: 12px;">
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
                                                <asp:ImageButton ID="imgBtnNext" runat="server" ImageUrl="~/Images/Buttons/btnNext.png" 
                                                    ToolTip="Página Siguiente" OnClick="imgBtnNext_Click" ValidationGroup="registraOrdenMerito" />
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
                     <%--Ini:Christian Ramirez - REQ91569--%>
                     <asp:Button ID="btnMostrarPopupTipoCalificacionNone" Style="display: none;" runat="server" />
                     <asp:Button ID="btnCerrarPopupTipoCalificacionNone" Style="display: none;" runat="server" />
                     <ajax:ModalPopupExtender ID="mpePopupTipoCalificacion" runat="server" 
                         TargetControlID="btnMostrarPopupTipoCalificacionNone" PopupControlID="pnlPopupTipoCalificacion" 
                         PopupDragHandleControlID="pnlPopupTipoCalificacion" BackgroundCssClass="modalBackground"
                         CancelControlID="btnCerrarPopupTipoCalificacionNone" Enabled="True" />
                     <asp:Panel ID="pnlPopupTipoCalificacion" runat="server" CssClass="popup-content" Style="width:100%;">
                         <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
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
                                                         <asp:Label ID="Label2" runat="server" Text="Notas de Rendimiento y Notas de Secundaria ">
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
                                                             <tr runat="server" id="trOrdenMeritoPopup_PopUp">
                                                                 <td></td>
                                                                 <td>
                                                                     <label class="TextoEtiqueta">Orden de Mérito:</label>
                                                                 </td>
                                                                 <td style="text-align: center; padding-top: 15px;">
                                                                     <asp:TextBox ID="txtOrdenMeritoPopup" runat="server" CssClass="txtCajaTexto" MaxLength="3"
                                                                         onkeypress="return soloNumeros(this);" Style="text-align: center" Width="80px"></asp:TextBox>
                                                                     <asp:Image ID="Image13" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                                 </td>
                                                                 <td></td>
                                                             </tr>
                                                             <tr runat="server" id="trNroAlmunosPopup_PopUp">
                                                                 <td></td>
                                                                 <td>
                                                                     <label class="TextoEtiqueta">Nro. Alumnos en su Promoción:</label>
                                                                 </td>
                                                                 <td style="text-align: center">
                                                                     <asp:TextBox ID="txtNroAlmunosPopup" runat="server" CssClass="txtCajaTexto" Width="80px"
                                                                         onkeypress="return soloNumeros(this);" MaxLength="3" Style="text-align: center"></asp:TextBox>
                                                                     <asp:Image ID="Image14" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                     <asp:Image ID="Image15" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
                                                                     <asp:Image ID="Image16" runat="server" ImageUrl="~/Images/ico_Required.gif" />
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
            <tr>
                <td>
                    <%--Ini:Christian Ramirez - REQ91569--%>
                    <asp:Button ID="btnMostrarError" Style="display: none;" runat="server" />
                    <asp:Button ID="btnCancelarError" Style="display: none;" runat="server" />
                    <ajax:ModalPopupExtender ID="mpeMostrarError" runat="server" TargetControlID="btnMostrarError"
                        PopupControlID="pnlMostrarError" PopupDragHandleControlID="pnlMostrarError" BackgroundCssClass="modalBackground"
                        CancelControlID="btnCancelarError" Enabled="True" />
                    <asp:Panel ID="pnlMostrarError" runat="server" CssClass="popup-content" Width="50%">
                        <asp:UpdatePanel ID="upMostrarErrorDetalle" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div class="popup-main">
                                    <div class="popup-header" style="background-color:#82B366">
                                        <h4>¡ Alerta !</h4>
                                    </div>
                                    <div class="popup-content">
                                        <p>
                                            <asp:Label runat="server" ID="lblmessage" CssClass="tdTextNormalAlerta"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="popup-footer" style="border-top: 1px solid #82B366">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnSalir_Click" 
                                            style="background-color:#82B366; border-bottom:#82B366">Cerrar</asp:LinkButton>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgBtnNext" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <%--Fin:Christian Ramirez - REQ91569--%>
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
            UrlGetStepsAdmision: 'frm06_RendimientoAcademico.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
    <script type="text/javascript">
        $(function () {
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
        })
    </script>
    <script type="text/javascript">
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
    </script>
</body>
</html>
