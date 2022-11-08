/******************************************************************************
'Descripcion :  Esta Funcion permite validar el input de numeros Enteros.
'*****************************************************************************/
function soloNumeros(txt) {
    if (event.keyCode <= 47 || event.keyCode > 57) {
        event.returnValue = false;
    }
}

// **************************************************************************************************
// BEGIN==INICIO. FUNCIONES Y VARIALES USADAS PARA CONFIGURAR LA GRILLA. AL HACER CLICK O AL MOVER EL RATON.
// **************************************************************************************************

var CssClassNombreItemGrillaXgYzk37H;
var ObjetoSelectClickAnteriorXgYzk37H;
var CssClassNombreAnteriorItemGrillaXgYzk37H;

function CambiarColorPasarMouseOver(ObjetoSelect) {
    ObjetoSelect.style.cursor = 'hand';
    if (ObjetoSelect != ObjetoSelectClickAnteriorXgYzk37H) {
        CssClassNombreItemGrillaXgYzk37H = ObjetoSelect.className;
        ObjetoSelect.className = "ItemGrillaPasarMouse";
    }
}

function CambiarColorPasarMouseOut(ObjetoSelect) {
    if (ObjetoSelect != ObjetoSelectClickAnteriorXgYzk37H) {
        ObjetoSelect.className = CssClassNombreItemGrillaXgYzk37H;
    }
}

function CambiarColorPasarMouseOverPublico(ObjetoSelect) {
    ObjetoSelect.style.cursor = 'hand';
    if (ObjetoSelect != ObjetoSelectClickAnteriorXgYzk37H) {
        CssClassNombreItemGrillaXgYzk37H = ObjetoSelect.className;
        ObjetoSelect.className = "ItemGrillaPasarMousePublico";
    }
}

function CambiarColorPasarMouseOutPublico(ObjetoSelect) {
    if (ObjetoSelect != ObjetoSelectClickAnteriorXgYzk37H) {
        ObjetoSelect.className = CssClassNombreItemGrillaXgYzk37H;
    }
}

function CambiarColorSeleccion(ObjetoSelect) {
    if (ObjetoSelectClickAnteriorXgYzk37H != null) {
        if (ObjetoSelect != ObjetoSelectClickAnteriorXgYzk37H) {
            ObjetoSelectClickAnteriorXgYzk37H.className = CssClassNombreAnteriorItemGrillaXgYzk37H;
            ObjetoSelectClickAnteriorXgYzk37H = ObjetoSelect;
            CssClassNombreAnteriorItemGrillaXgYzk37H = CssClassNombreItemGrillaXgYzk37H;
        }
    }
    else {
        ObjetoSelectClickAnteriorXgYzk37H = ObjetoSelect;
        CssClassNombreAnteriorItemGrillaXgYzk37H = CssClassNombreItemGrillaXgYzk37H;
    }
    ObjetoSelect.className = "ItemGrillaHacerClick";
}

function CambiarColorSeleccionPublico(ObjetoSelect) {
    if (ObjetoSelectClickAnteriorXgYzk37H != null) {
        if (ObjetoSelect != ObjetoSelectClickAnteriorXgYzk37H) {
            ObjetoSelectClickAnteriorXgYzk37H.className = CssClassNombreAnteriorItemGrillaXgYzk37H;
            ObjetoSelectClickAnteriorXgYzk37H = ObjetoSelect;
            CssClassNombreAnteriorItemGrillaXgYzk37H = CssClassNombreItemGrillaXgYzk37H;
        }
    }
    else {
        ObjetoSelectClickAnteriorXgYzk37H = ObjetoSelect;
        CssClassNombreAnteriorItemGrillaXgYzk37H = CssClassNombreItemGrillaXgYzk37H;
    }
    ObjetoSelect.className = "ItemGrillaHacerClickPublico";
}


// **************************************************************************************************
// END==FIN. FUNCIONES Y VARIALES USADAS PARA CONFIGURAR LA GRILLA. AL HACER CLICK O AL MOVER EL RATON.
// **************************************************************************************************
function MostrarDatosEnCajaTexto(ptxtObjeto, ptxtValor) {
    document.forms[0].elements[ptxtObjeto].value = ptxtValor;
    return;
}

function MostrarDatosEnCajaTextoNuevo(ptxtObjeto, ptxtObjetoBotonN, ptxtObjetoBotonM, ptxtValor) {

    if (ptxtValor == '') {
        document.forms[0].elements[ptxtObjeto].value = ptxtValor;
        document.forms[0].elements[ptxtObjetoBotonN].disabled = false;
        document.forms[0].elements[ptxtObjetoBotonM].disabled = true;
    }
    else {
        document.forms[0].elements[ptxtObjeto].value = ptxtValor;
        document.forms[0].elements[ptxtObjetoBotonN].disabled = true;
        document.forms[0].elements[ptxtObjetoBotonM].disabled = false;
    }
    return;
}

function VerificarSeleccionControl(NombreControl, Mensaje) {
    if (document.forms[0].elements[NombreControl].value == '') {
        alert(Mensaje);
        return false;
    }
    else {
        return true;
    }
}

function ConfimarAccionBotonPagRegistro(NombreIdControl, MensajeErrorSeleccionar, MensajeConfirmacionAccion) {
    if (document.forms[0].elements[NombreIdControl].value != '') {
        var blnresult = confirm(MensajeConfirmacionAccion);
        if (blnresult == false) {
            document.forms[0].elements[NombreIdControl].value = '';
        }
        return blnresult;
    }
    else {
        alert(MensajeErrorSeleccionar);
        return false;
    }
}

function ConfimarAccionRegistroEmail(ctl1, MensajeConfirmacion) {
    var val1 = document.forms[0].elements[ctl1].value;
    var mensaje = '';
    if (val1 == '') {
        mensaje = mensaje + '- Ingrese el tipo de campo.' + '\n';
    }
    if (mensaje == '') {
        return confirm(MensajeConfirmacion);
    }
    else {
        alert(mensaje);
        return false;
    }
}

function ConfimarEliminarRegistro(NombreIdControl, MensajeErrorSeleccionar, MensajeConfirmacionEliminar) {
    if (document.forms[0].elements[NombreIdControl].value != '') {
        return confirm(MensajeConfirmacionEliminar);
    }
    else {
        alert(MensajeErrorSeleccionar);
        return false;
    }
}

function ConfimarAccionRegistro(MensajeConfirmacion) {
    return confirm(MensajeConfirmacion);
}


function BloquearPostBackAlPrecionarTecla() {
    if (event.keyCode == 13) return false;
    //|| event.keyCode==9
}

function ConfigurarPaginaWebAlCargarla() {
    return false;
}

function ConfigurarPaginaWebAlDescargarla() {
    return false;
}

function trim(argvalue) {
    var tmpstr = ltrim(argvalue);
    return rtrim(tmpstr);
}

function rtrim(argvalue) {
    while (1) {
        if (argvalue.substring(argvalue.length - 1, argvalue.length) != " ")
            break;
        argvalue = argvalue.substring(0, argvalue.length - 1);
    }
    return argvalue;
}

function ltrim(argvalue) {
    while (1) {
        if (argvalue.substring(0, 1) != " ")
            break;
        argvalue = argvalue.substring(1, argvalue.length);
    }
    return argvalue;
}

function FP_ValidaEntero(idcontrol) {
    //strNameObj : Nombre de la caja de texto a validar.	
    //var Obj = document.all[strNameObj];
    var ch_Caracter = String.fromCharCode(window.event.keyCode);
    var intEncontrado = "0123456789".indexOf(ch_Caracter);
    if (intEncontrado == -1) {
        window.event.keyCode = 0;
    }
    else {
        window.event.keyCode = ch_Caracter.charCodeAt();
    }
}

function FP_ValidNumero(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return ((tecla > 47 && tecla < 58) || tecla == 46);
}
