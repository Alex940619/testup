var spnAyuda;
var divPopupMensaje;
var divPopupAlerta;
var txtUbigeoNacimiento;
var spnPopupMensaje;
var txtEmail2;
var txtEmail1;


window.onload = function () {
    divPopupMensaje = document.getElementById('divPopupMensaje');
    divPopupAlerta = document.getElementById('divPopupAlerta');
    spnPopupMensaje = document.getElementById('spnPopupMensaje');

    btnCerrar2 = document.getElementById('btnCerrar2');
    btnCerrar2.onclick = function () {
        divPopupAlerta.style.display = 'none';
    }

    btnCerrar = document.getElementById('btnCerrar');
    btnCerrar.onclick = function () {
        divPopupMensaje.style.display = 'none';
    }

    /*Ini: Christian Ramirez - GIIT [Caso 47019] - 20180627*/
    var trUbigeo = document.getElementById('trUbigeo');
    if (trUbigeo != null) {

        spnAyuda = document.getElementById('spnAyuda');
        spnAyuda.onclick = function () {
            divPopupMensaje.style.display = 'block';
        }

        txtUbigeoNacimiento = document.getElementById('txtUbigeoNacimiento').onkeypress = function () {
            return soloNumeros(event);
        }

        txtUbigeoNacimiento = document.getElementById('txtUbigeoNacimiento').onblur = function () {
            validarUbigeoNacimiento(this);
        }
    }
    /*Fin: Christian Ramirez - GIIT [Caso 47019] - 20180627*/

    document.getElementById('txtPrimNombre').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtSegNombre').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtApePaterno').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtApeMaterno').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtDireccion').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtDireccionCompleta').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtReferencia').onkeyup = function () { textoMayusculas(this) }

    txtEmail1 = document.getElementById('txtEmail1')
    txtEmail1.onblur = function () {
        if (txtEmail1.value != '') {
            validarEmail(this);
        }
    }

    txtEmail2 = document.getElementById('txtEmail2');
    txtEmail2.onblur = function () {
        if (txtEmail2.value != '') {
            validarEmail(this);
        }
    }

    /*Ini: Christian Ramirez GIIT [Caso45607] - 20180531*/
    document.getElementById('ddlTipoDocumento').onchange = function () {
        validarTipoDocumento(this)
    }
    /*Fin: Christian Ramirez GIIT [Caso45607] - 20180531*/
}

function soloNumeros(e) {
    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57) {
        return false;
    }
}

function textoMayusculas(texto) {
    texto.value = texto.value.toUpperCase();
}

function validarEmail(texto) {
    var valor = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i.test(texto.value);
    if (!valor) {
        texto.focus();
        spnPopupMensaje.innerHTML = 'El formato del correo no es correcto';
        divPopupAlerta.style.display = 'block';
    }
}

function validarUbigeoNacimiento(texto) {
    /*Ini: Christian Ramirez GIIT [Caso45607] - 20180531*/
    var seleccion = document.getElementById('ddlTipoDocumento').selectedIndex;
    if (seleccion == 1) {
        var ubigeo = texto.value.length;
        if (ubigeo < 6) {
            /*Ini:: Christian Ramirez - GIIT [Caso 48662] - 20180730*/
            document.getElementById('spnUbigeo').style.display = 'block';
            /*Fin:: Christian Ramirez - GIIT [Caso 48662] - 20180730*/
        } else {
            document.getElementById('spnUbigeo').style.display = 'none'; /*Se agrega:: Christian Ramirez - GIIT [Caso 48662] - 20180730*/
        }
    } else {
        document.getElementById('spnUbigeo').style.display = 'none'; /*Se agrega:: Christian Ramirez - GIIT [Caso 48662] - 20180730*/
    }
    /*Fin: Christian Ramirez GIIT [Caso45607] - 20180531*/
}

/*Ini: Christian Ramirez GIIT [Caso45607] - 20180531*/
function validarTipoDocumento(e) {
    document.getElementById('spnUbigeoRequeridoIcono').style.display = 'none';
    var seleccion = e.selectedIndex;
    var spnUbigeoRequerido = document.getElementById('spnUbigeoRequerido');

    if (seleccion == 1) {
        spnUbigeoRequerido.style.display = 'block';
        document.getElementById('spnUbigeo').style.display = 'block'; /*Se agrega:: Christian Ramirez - GIIT [Caso 48662] - 20180730*/
    }
    else {
        spnUbigeoRequerido.style.display = 'none';
        document.getElementById('spnUbigeo').style.display = 'none'; /*Se agrega:: Christian Ramirez - GIIT [Caso 48662] - 20180730*/
    }
}
/*Fin: Christian Ramirez GIIT [Caso45607] - 20180531*/