$(document).ready(function() {
    InicializaDocumento();
});

$.extend({
    getUrlVars: function() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },

    getUrlVar: function(name) {
        return $.getUrlVars()[name];
    }
});

function InicializaDocumento() {
    //$('#txtNombre').focus();
}


function OcultaLightBox() {
    self.parent.tb_remove();
}

function registrarDocumento(msje) {
    alert(msje);
    self.parent.tb_remove();
    parent.ActualizarGrilla();
}

