<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cuwControlFecha.ascx.cs" Inherits="UPAdmPre.WEB.usrctrl.cuwControlFecha" %>
<table border="0" cellpadding="0" cellspacing="2">
    <tr>
        <td style="vertical-align: middle;">
            <asp:TextBox ID="txtTextoFecha" CssClass="txtCajaTexto" runat="server" Width="60px"></asp:TextBox>
        </td>
        <td style="vertical-align: middle;">
            <asp:ImageButton ID="imgCalendario" runat="server" ImageUrl="~/Images/Buttons/btnCalendar.gif" />
        </td>
        <td style="vertical-align: middle;">
            <asp:RequiredFieldValidator ID="rfvFechaControl" CssClass="MsgAlertaIncompleto" ControlToValidate="txtTextoFecha"
                Display="Dynamic" Text="*" ErrorMessage="Ingrese Fecha." runat="server"
                Width="100%" />
            <ajax:maskededitvalidator ID="mevTextoFecha" runat="server" CssClass="MsgAlertaIncompleto" ControlExtender="meeTextoFecha"
                ControlToValidate="txtTextoFecha" IsValidEmpty="True" Display="Dynamic" MaximumValueMessage=""
                MinimumValueMessage="" TooltipMessage="" Text="" Width="100px" EmptyValueMessage="Seleccione una fecha"
                InvalidValueMessage="Fecha incorrecta" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                MinimumValue="01/01/1900" MinimumValueBlurredText="Fecha incorrecta"
                MaximumValue="01/01/2100" MaximumValueBlurredMessage="Fecha incorrecta" />
        </td>
        <td>
            <ajax:maskededitextender ID="meeTextoFecha" runat="server" AcceptNegative="Left"
                DisplayMoney="Left" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                TargetControlID="txtTextoFecha" CultureName="es-PE" AutoComplete="true" ErrorTooltipEnabled="True" />
            <ajax:calendarextender ID="clxCalendario" TargetControlID="txtTextoFecha" Format="dd/MM/yyyy"
                PopupButtonID="imgCalendario" runat="server" />
        </td>
    </tr>
</table>
