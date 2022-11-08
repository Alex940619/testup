(function () {

    let txtColegio1 = document.getElementById("txtColegio1");
    let txtColegio2 = document.getElementById("txtColegio2");
    let txtColegio3 = document.getElementById("txtColegio3");

    let txtDireccionColegio1 = document.getElementById("txtDireccionColegio1");
    let txtDireccionColegio2 = document.getElementById("txtDireccionColegio2");
    let txtDireccionColegio3 = document.getElementById("txtDireccionColegio3");

    if (txtColegio1 !== null) txtColegio1.onkeyup = function () { textoMayusculas(this); };
    if (txtColegio2 !== null) txtColegio2.onkeyup = function () { textoMayusculas(this); };
    if (txtColegio3 !== null) txtColegio3.onkeyup = function () { textoMayusculas(this); };

    if (txtDireccionColegio1 !== null) txtDireccionColegio1.onkeyup = function () { textoMayusculas(this); };
    if (txtDireccionColegio2 !== null) txtDireccionColegio2.onkeyup = function () { textoMayusculas(this); };
    if (txtDireccionColegio3 !== null) txtDireccionColegio3.onkeyup = function () { textoMayusculas(this); };

    function textoMayusculas(texto) {
        texto.value = texto.value.toUpperCase();
    }

}());