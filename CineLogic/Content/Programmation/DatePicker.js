$(document).ready(function () {
    //================================================================================
    // SELECTION DE LA DATE
    //================================================================================

    //  Définir les options pour le datepicker.
    var options = {
        todayBtn: 'linked',
        language: 'fr',
        orientation: 'bottom left',
        daysOfWeekHighlighted: '0,6'
    };

    //  Créer le datepicker.
    $("#date-picker").datepicker(options);
});