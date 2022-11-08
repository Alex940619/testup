var txtEmailRelFam1;
var txtEmailRelFam2;
var txtEmailRelFam3;
var divPopupAlerta;


window.onload = function () {
    divPopupAlerta = document.getElementById('divPopupAlerta');

    document.getElementById('btnCerrar2').onclick = function () {
        divPopupAlerta.style.display = 'none';
    };

    /*Ini: Christian Ramirez - GIIT[Caso 48793] - 20180731*/
    var txtNomRelFam1 = document.getElementById('txtNomRelFam1');
    var txtApeRelFam1 = document.getElementById('txtApeRelFam1');
    var txtNomRelFam2 = document.getElementById('txtNomRelFam2');
    var txtApeRelFam2 = document.getElementById('txtApeRelFam2');
    var txtNomRelFam3 = document.getElementById('txtNomRelFam3');
    var txtApeRelFam3 = document.getElementById('txtApeRelFam3');

    if (txtNomRelFam1 != null) document.getElementById('txtNomRelFam1').onkeyup = function () { textoMayusculas(this) }
    if (txtApeRelFam1 != null) document.getElementById('txtApeRelFam1').onkeyup = function () { textoMayusculas(this) }
    if (txtNomRelFam2 != null) document.getElementById('txtNomRelFam2').onkeyup = function () { textoMayusculas(this) }
    if (txtApeRelFam2 != null) document.getElementById('txtApeRelFam2').onkeyup = function () { textoMayusculas(this) }
    if (txtNomRelFam3 != null) document.getElementById('txtNomRelFam3').onkeyup = function () { textoMayusculas(this) }
    if (txtApeRelFam3 != null) document.getElementById('txtApeRelFam3').onkeyup = function () { textoMayusculas(this) }
   
    txtEmailRelFam1 = document.getElementById('txtEmailRelFam1');
    if (txtEmailRelFam1 != null) {
        txtEmailRelFam1.onblur = function () {
            if (txtEmailRelFam1.value != '') { validarEmail(this); }
        }
    }

    txtEmailRelFam2 = document.getElementById('txtEmailRelFam2');
    if (txtEmailRelFam2 != null) {
        txtEmailRelFam2.onblur = function () {
            if (txtEmailRelFam2.value != '') { validarEmail(this); }
        }
    }

    txtEmailRelFam3 = document.getElementById('txtEmailRelFam3');
    if (txtEmailRelFam2 != null) {
        txtEmailRelFam3.onblur = function () {
            if (txtEmailRelFam3.value != '') { validarEmail(this); }
        }
    }
    /*Fin: Christian Ramirez - GIIT[Caso 48793] - 20180731*/

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