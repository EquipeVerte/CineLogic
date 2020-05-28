$(document)
    //  Afficher l'icone "loading" pendant que l'ajax s'execute.
    .ajaxStart(function () {
        $(".loading").show();
    })
    .ajaxStop(function () {
        $(".loading").hide();
    });