<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm16_TerminosyCondiciones.aspx.cs" Inherits="UPAdmPre.Web.Admision.frm16_TerminosyCondiciones" Culture="es-PE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
        function Abrir_ventana(pagina) {
            var opciones = "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=yes, width=720, height=365, top=185, left=450";
            window.open(pagina, "", opciones);
        }
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
                                <asp:Label ID="lblPantallaAnterior" runat="server" Text="Referencias Personales"></asp:Label>
                            </td>
                            <td class="CabeceraNoSeleccionada" style="width: 33%;">
                                <asp:Label ID="lblPantallaVigente" runat="server" Text="Documentos Requeridos"></asp:Label>
                            </td>
                            <td class="CabeceraSeleccionada" style="width: 33%;">
                                <asp:Label ID="lblPantallaSiguiente" runat="server" Text="Terminos y Condiciones"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" class="tablaFrame">
                        <tr>
                            <td class="SubTitulo">
                                <table style="width: 100%;" class="tablaInterna">
                                    <tr>
                                        <td style="width: 70%;">
                                            <asp:Label ID="lblTitulo" runat="server" Text="Consideraciones"></asp:Label>
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:Label ID="lblPasos" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 10%;text-align: right">
                                            <asp:ImageButton ID="imgBtnVideo" runat="server" ImageUrl="~/Images/Buttons/btnVideo.png" OnClick="imgBtnVideo_Click" ToolTip="Ver video" Visible="False" />
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;" class="tablaFrame">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 1%; padding-top: 18px;">
                                                        <asp:CheckBox ID="chkConsideraciones" runat="server" CssClass="tdTextNormal" OnCheckedChanged="chkConsideraciones_CheckedChanged" AutoPostBack="true" />
                                                    </td>
                                                    <td style="padding-top: 18px; padding-bottom: 6px;">
                                                        <asp:Label ID="lblConsideraciones" runat="server" CssClass="tdTextNormal"></asp:Label>
                                                        <a href="javascript:Abrir_ventana('Facilidades.htm')" class="tdTextNormal">(ver política UP)</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <% if (chkConsideraciones.Checked == true)
                                        {  %>
                                    <tr>
                                        <td>
                                            <table style="width: 100%;" class="tablaInterna">
                                                <tr style="border-bottom: 1px solid #337ab7;">
                                                    <td style="width: 2%; vertical-align: top;">
                                                        <asp:Image ID="Image1" runat="server" Height="30px" ImageUrl="~/Images/discapacidad.png" Width="30px" />
                                                    </td>
                                                    <td style="width: 98%; padding-bottom: 0px;">
                                                        <asp:TextBox ID="txtConsideracion" runat="server" CssClass="txtCajaTexto" Height="50px" TextMode="MultiLine" Width="95%" placeholder="Indicar requerimiento-discapacidad" MaxLength="60"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvCondiciones" runat="server" ControlToValidate="txtConsideracion"
                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Ingrese las consideraciones" InitialValue=""
                                                            SetFocusOnError="true" ValidationGroup="registraPadres" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <% } %>
                                    <tr>
                                        <td style="text-align: left;"></td>
                                    </tr>                                 
                                    <tr>
                                        <td style="text-align: left; padding-top: 10px;"><b>Consentimiento interesados</b>
                                            <asp:TextBox ID="txtConsentimiento" runat="server" CssClass="txtCajaTexto" Height="150px" TextMode="MultiLine" Width="92%" placeholder="Indicar requerimiento-discapacidad" ReadOnly="True" style="text-align:justify;">De conformidad con la Ley N° 29733 (en adelante, la Ley) y el Decreto Supremo N° 003-2013-JUS (en adelante, el Reglamento), el titular de los datos personales (en adelante, el titular) autoriza, mediante su firma o cualquier otra forma de aceptación expresa automatizada, el tratamiento de los datos personales (incluido imagen y voz) que facilite a la Universidad del Pacífico (en adelante, la Universidad), por cualquier medio físico o electrónico, de acuerdo a las finalidades descritas a continuación.

La Universidad, con domicilio en Jr. General Sánchez Cerro N° 2141, distrito de Jesús María, de conformidad con la Ley, el Reglamento y normas conexas, declara ser la titular del Banco de Datos Personales que almacena los datos que registre en las diversas plataformas o sitios web de titularidad de la Universidad, y que se encuentra inscrito ante la Autoridad Nacional de Protección de Datos Personales. Asimismo, la Universidad informa que los destinatarios de los datos personales serán las oficinas de la Admisión, Servicios Académicos, Marketing, y cualquier unidad académica o administrativa de la Universidad, la cual conservará los datos personales por el plazo máximo de sesenta (60) años o hasta que sean modificados, dependiendo de la naturaleza de los mismos; con las siguientes finalidades principales: gestión académica; gestión administrativa; gestión comercial; gestión y acciones de cobranza; gestión de iniciativas empresariales y crowdfunding; gestión de la Red Alumni, de la bolsa de trabajo de la Universidad, y verificación de referencias académicas; gestión de procedimientos disciplinarios; gestión de clientes y potenciales clientes; actividades de investigación; ofrecimiento y prestación de servicios educativos u otros ofrecidos por la Universidad, cualquiera de sus dependencias, o terceros con quienes se hubieran celebrados contratos o convenios; atención y seguimiento al proceso de postulación y admisión; atención de consultas sobre los procesos de admisión, carreras profesionales, pensiones de enseñanza, financiamiento, o de cualquier tipo; recopilación de información para determinar la escala de pensiones; registro de postulantes; publicación de resultados de los procesos de admisión en sus diferentes modalidades; diseño, ejecución y análisis de encuestas; atención de consultas, quejas, reclamos; registro y actualización de los datos de alumnos y egresados; evaluación socioeconómica para la asignación de escala de pensiones, becas o cualquier tipo de financiamiento educativo proporcionado por la Universidad u otra institución que hubiese suscrito un convenio con la Universidad; evaluación del cumplimiento de requisitos para acceder a cualquier beneficio proporcionado por la Universidad; diseño, ejecución y análisis de encuestas; acreditación académica o de procesos internos, ante instituciones nacionales e internacionales, públicas y privadas; obtención de grados académicos, títulos profesionales, certificaciones y constancias; elaboración y entrega de fotocheck, carné universitario, u otro instrumento de identificación física o digital; entrega y recojo de materiales académicos, documentos, premios y otros artículos que resulten necesarios; elaboración y entrega de cartas de recomendación; invitaciones a eventos, talleres, charlas o cualquier otro que organice la Universidad; atención en el tópico de la Universidad; ejecución de actividades de telemarketing, mediante sistemas de llamadas telefónicas envío de mensajes de texto a celular, mensajería instantánea (WhatsApp), correos electrónicos (individuales o masivos) o medios electrónicos, para promover productos y servicios que ofrece la Universidad; coordinaciones para el desarrollo de eventos co-organizados con personas naturales o jurídicas distintas a la Universidad; elaboración y actualización del directorio de la comunidad universitaria; cumplimiento de los requerimientos que efectúe cualquier entidad pública, por mandato legal; cumplimiento de cualquier finalidad directa o indirecta vinculada a su condición de estudiante o egresado de la Universidad. 

En función a ello, y de acuerdo a lo estipulado en el artículo 58° del Código de Protección y Defensa del Consumidor, el titular autoriza que la Universidad remita información al titular de los datos personales, sobre las carreras de pregrado que ofrece la Universidad, programas académicos de postgrado, educación ejecutiva e idiomas, encuestas de satisfacción y mejora del servicio educativo, eventos académicos, artísticos, culturales y de entretenimiento organizados por la Universidad o cualquiera de sus dependencias, para lo cual se utilizará la vía postal, telefónica, correos electrónicos, medio electrónicos o cualquier otro medio de comunicación.

El alumno autoriza a la Universidad para que comparta, ceda, encargue o transfiera estos datos a terceros con las siguientes finalidades adicionales:

-	Realizar actividades de telemarketing, mediante sistemas de llamado telefónico, envío de mensajes de texto a celular, correos electrónicos (individuales o masivos) o medios electrónicos, para promover productos y servicios que ofrece la Universidad.
-	Mantener actualizados los datos de los titulares.
-	Realizar evaluaciones financieras con el objetivo de asegurar la continuidad de estudios de sus estudiantes, en los niveles de pregrado, postgrado, educación ejecutiva o en cualquiera de los servicios educativos que el usuario esté cursando en la Universidad.
-	Validar datos en procesos de selección de personal.
-	Realizar actividades de reparto (courier o delivery), para el envío y recojo de documentos, artículos, premios u otros materiales.
-	Coordinar el desarrollo adecuado de eventos co-organizados con personas naturales o jurídicas distintas a la Universidad. 
-	Gestionar riesgos y acciones de cobranza de las deudas que pueda tener el titular con cualquiera de las dependencias o unidades de la Universidad.

La Universidad garantiza que cualquier transferencia, cesión o encargo de datos personales se sujetará a lo previsto por la Ley y la Universidad procurará que estos no se vean afectados por cualquier uso indebido. A su vez, la Universidad podrá transferir los datos personales recopilados a los co-organizadores de los programas académicos, conferencias o eventos –presenciales o virtuales- en los que participe el titular, siempre que cumplan con las disposiciones previstas para la adecuada protección de datos personales.

Asimismo, el titular autoriza que las imágenes en fotografía o video que sean entregadas por el titular o recopiladas por la Universidad, en la sesión de bienvenida, semana internacional, o en cualquier evento presencial o virtual que sea organizado por la Universidad, puedan ser publicadas en la página web, redes sociales u otros medios digitales de la Universidad, y utilizarlas para la promoción de las actividades y eventos que organice. 

En caso que el titular de los datos personales sea mayor de catorce años y menor de dieciocho, declara que lo descrito en este documento es comprensible; y, en función a ello, otorga su consentimiento para el uso y tratamiento de sus datos personales, de conformidad con las finalidades descritas anteriormente.

Si, por cualquier circunstancia, el titular no completa el proceso de admisión o no es admitido en el proceso al que postula, el presente consentimiento no surtirá efectos y la Universidad almacenará la data registrada con fines de contacto para los programas académicos de pregrado y postgrado, educación ejecutiva e idiomas, encuestas de satisfacción y mejora del servicio, eventos académicos, artísticos, culturales y de entretenimiento organizados por la Universidad. 

En caso que el titular desee ejercer sus derechos de acceso, cancelación, oposición, revocatoria de consentimiento, modificación o cualquier otro, podrá recurrir a Jr. General Luis Sánchez Cerro N° 2141, distrito de Jesús María, o escribir revocatoria.datos.personales@up.edu.pe. La Universidad tiene la obligación de informar los procedimientos para hacer valer los derechos mencionados anteriormente.

Se pone en conocimiento de los titulares que los formularios, mediante los cuales otorguen sus datos personales, incluyen preguntas obligatorias y facultativas, las cuales podrán ser identificadas en cada formulario. Las consecuencias de la concesión de datos personales, faculta a la Universidad a utilizarlos de acuerdo a las finalidades señaladas en este documento. La negativa para el tratamiento de los datos personales imposibilita a la Universidad a incluirlos en su base de datos, la cual remite información instantánea y actualizada respecto a las actividades que organiza la Universidad, ofertas de programas académicos de pregrado (carreras), postgrado (maestrías), educación ejecutiva (extensión), idiomas, encuestas de satisfacción y mejora del servicio educativo, eventos académicos, artísticos, culturales y de entretenimiento, así como otras actividades relacionadas a la Universidad o sus dependencias.
                                                
<%--De conformidad con la Ley N° 29733 (en adelante, la Ley) y el Decreto Supremo N° 003-2013-JUS (en adelante, el Reglamento), el usuario autoriza, mediante su firma o cualquier otra forma de aceptación expresa automatizada, el tratamiento de los datos personales de cualquier índole (los mismos que incluyen imagen y voz) que facilite a la Universidad del Pacífico (en adelante, la Universidad), por cualquier medio físico o electrónico, de acuerdo a las finalidades descritas a continuación.

La Universidad, con domicilio en Jr. Gral. Luis Sánchez Cerro 2141, distrito de Jesús María, fomenta y respeta el uso de los datos personales; asimismo, declara ser la titular del Banco de Datos Personales e informa que los destinatarios de los datos personales serán las oficinas de Marketing, Emprende UP, Admisión, Servicios Académicos, Red Alumni, y cualquier otra unidad académica o administrativa de la Universidad, la cual conservará los datos personales permanentemente o hasta que sean modificados dependiendo de la naturaleza de los mismos; con la finalidad de utilizarlos en gestiones académicas, institucionales, administrativas y comerciales, así como procesar y manejar información para el adecuado desarrollo de la prestación de servicios y cubrir las necesidades de sus interesados.

En función a ello, y de acuerdo a lo estipulado en el artículo 58° del Código de Protección y Defensa del Consumidor, el usuario autoriza que la Universidad remita información al titular de los datos personales, sobre las carreras de pregrado que ofrece la Universidad, programas académicos de postgrado, educación ejecutiva e idiomas, encuestas de satisfacción y mejora del servicio educativo, eventos académicos, artísticos, culturales y de entretenimiento organizados por la Universidad o cualquiera de sus dependencias, para lo cual se utilizará la vía postal, telefónica, correos electrónicos, medio electrónicos o cualquier otro medio de comunicación. Además, el usuario autoriza a la Universidad para que realice, por sus propios medios, o comparta, ceda o transfiera estos datos a terceros; a fin de realizar actividades de telemarketing, mediante sistemas de llamado telefónico, envío de mensajes de texto a celular, correos electrónicos postulantes (individuales o masivos) o medio electrónicos, para promover productos y servicios; así como, mantener actualizados los datos de los titulares, bajo la garantía de que la Universidad procurará que estos no se vean afectados por cualquier uso indebido.

En caso el usuario desee ejercer sus derechos de acceso, cancelación, oposición, revocatoria de consentimiento, modificación o cualquier otro, podrá recurrir a la oficina de Data Intelligence, la misma que se encuentra ubicada en Calle Inca Rípac N° 395, distrito de Jesús María, o escribir a revocatoria.postulantes.interesados@up.edu.pe. Esta oficina tiene la obligación de informar los procedimientos para hacer valer los derechos mencionados anteriormente.

En caso que el titular de los datos personales sea menor de edad entre catorce y dieciocho años, declara que lo descrito en este documento es comprensible; y, en función a ello, otorga su consentimiento para el uso y tratamiento de sus datos personales, de conformidad con las finalidades descritas anteriormente.

Se pone en conocimiento de los usuarios que los formularios, mediante los cuales otorguen sus datos personales, incluyen preguntas obligatorias y facultativas, las cuales podrán ser identificadas en cada formulario. Las consecuencias de la concesión de datos personales, faculta a la Universidad a utilizarlos de acuerdo a las finalidades señaladas en el párrafo anterior. La negativa en la entrega de los datos personales del usuario imposibilita a la Universidad a incluirlos en su base de datos que remite información instantánea y actualizada respecto a programas académicos de pregrado (carreras), postgrado (maestrías), educación ejecutiva (extensión) e idiomas, encuestas de satisfacción y mejora del servicio educativo, eventos académicos, artísticos, culturales y de entretenimiento, así como otras actividades relacionadas a la Universidad o sus dependencias.--%>

                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td colspan ="4">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            <table class="tablaInterna">
                                                <tr>
                                                    <td style="text-align: left;">
                                                        <asp:RadioButtonList ID="rblAutorizacion" runat="server" CssClass="radioButtonList" RepeatDirection="Vertical">
                                                            <asp:ListItem Text="" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>                                                     
                                                    <td style="text-align: left; height: 35px;">
                                                        <asp:Label ID="lblAutorizacion" runat="server" Text="Autorizo a la Universidad del Pacífico utilizar mis datos personales para los fines mencionados.</br>No autorizo a la Universidad del Pacífico utilizar mis datos personales para los fines mencionados." CssClass="tdTextNormal"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvAutorizacion" runat="server" ControlToValidate="rblAutorizacion"
                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione si autoriza o no el uso de datos" InitialValue=""
                                                            SetFocusOnError="true" ValidationGroup="registraAutorizaciones" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        <asp:RadioButtonList ID="rblautorizacionTercer" runat="server" CssClass="radioButtonList" RepeatDirection="Vertical">
                                                            <asp:ListItem Text="" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    
                                                    <td style="text-align: left; height: 35px;">
                                                        <asp:Label ID="LblAutorizacionTercer" runat="server" Text="Autorizo a que la Universidad comparta, ceda o transfiera mis datos personales a terceros.</br>No autorizo a que la Universidad comparta, ceda o transfiera mis datos personales a terceros." CssClass="tdTextNormal"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvautorizaciontercer" runat="server" ControlToValidate="rblautorizacionTercer"
                                                            CssClass="MsgAlertaIncompleto" ErrorMessage="Seleccione si autoriza o no el uso de datos" InitialValue=""
                                                            SetFocusOnError="true" ValidationGroup="registraAutorizaciones" Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                    
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>     
                                    <tr>
                                        <td colspan ="4">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            <table class="tablaInterna">
                                                <tr>
                                                    <td style="padding-left: 15px">
                                                        <asp:CheckBox ID="chkTerminosCondiciones" runat="server" OnCheckedChanged="chkTerminosCondiciones_CheckedChanged" AutoPostBack="true"/>                                                        
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTerminosCondiciones" runat="server" Text="Declaro haber leído los términos y condiciones detallados en el prospecto de admisión vigente de la campaña correspondiente." CssClass="tdTextNormal"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAlertaTerminosCondiciones" runat="server" Text="Es necesario confirmar la lectura y aceptación de los términos y condiciones." CssClass="MsgAlertaIncompleto2" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            <table class="tablaInterna">
                                                <tr>
                                                    <td style="padding-left: 15px">
                                                        <asp:CheckBox ID="chkMayor14ConsentimientoDatPer" runat="server" OnCheckedChanged="chkMayor14ConsentimientoDatPer_CheckedChanged" AutoPostBack="true"/>                                                        
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMayor14ConsentimientoDatPer" runat="server" Text="Declaro que soy mayor de 14 años y comprendo los términos del consentimiento de datos personales (al marcar esta opción, el apoderado legal no deberá marcar la siguiente casilla)" CssClass="tdTextNormal"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAlertaMayor14ConsentimientoDatPer" runat="server" Text="Es necesario confirmar que es mayor de 14 años y comprende los términos del consentimiento de datos personales o que es el apoderado legal del titular de los datos personales." CssClass="MsgAlertaIncompleto2" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            <table class="tablaInterna">
                                                <tr>
                                                    <td style="padding-left: 15px">
                                                        <asp:CheckBox ID="chkApoderadoLegalTitularDatPer" runat="server" OnCheckedChanged="chkApoderadoLegalTitularDatPer_CheckedChanged" AutoPostBack="true"/>                                                        
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblApoderadoLegalTitularDatPer" runat="server" Text="Declaro que soy el apoderado legal del titular de los datos personales (al marcar esta opción, el postulante no deberá marcar la casilla anterior)." CssClass="tdTextNormal"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAlertaApoderadoLegalTitularDatPer" runat="server" Text="Es necesario confirmar que es el apoderado legal del titular de los datos personales o que es mayor de 14 años y comprende los términos del consentimiento de datos personales." CssClass="MsgAlertaIncompleto2" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/ico_Required.gif" />
                    &nbsp;<asp:Label ID="Label5" runat="server" CssClass="tdTextNormal" Text="Campos Obligatorios"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 13px; color: Red; font-weight: bold; text-align: right; height: 30px;">
                    <asp:Label ID="lblMsgIncompleto" runat="server" Text="La inscripción será considerada exitosa si los documentos necesarios para la inscripicón <i>online</i> han sido aprobados y se ha confirmado el pago del derecho de inscripción.
<br>A partir de este momento tu inscripción entrará en proceso de revisión." Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="controles" style="width: 100%;">
                        <tr>
                            <td style="width: 219px;">
                                <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/Images/Buttons/btnPrev.png" ToolTip="Página anterior" OnClick="imgBtnBack_Click" />
                            </td>
                            <td style="width: 281px;"></td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="imgBtnGuardar" runat="server" ImageUrl="~/Images/Buttons/btnGuardar.png" OnClick="imgBtnGuardar_Click" ValidationGroup="registraAutorizaciones" />
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr >
                <td>
                    <table style="width:100%;text-align:center">
                    <tr>
                            <td>
                                <asp:ImageButton ID="imgBtnEnviar" runat="server" ImageUrl="~/Images/Buttons/btnEnabled.png" OnClick="imgBtnEnviar_Click" ValidationGroup="registraAutorizaciones" Visible="false"/>
                            </td>                            
                        </tr>
                </table>
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
            UrlGetStepsAdmision: 'frm16_TerminosyCondiciones.aspx/GetStepsAdmision'
        }
    </script>
    <script type="text/javascript" src="../Helper/UtilStepAdmision.js"></script>
</body>
</html>
