
window.onload = function () {
    document.getElementById('txtColegio1').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtColegio2').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtColegio3').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtDireccionColegio1').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtDireccionColegio2').onkeyup = function () { textoMayusculas(this) }
    document.getElementById('txtDireccionColegio3').onkeyup = function () { textoMayusculas(this) }
}

function textoMayusculas(texto) {
    texto.value = texto.value.toUpperCase();
}