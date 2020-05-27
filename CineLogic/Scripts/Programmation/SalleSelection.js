$(document).ready(function () {
    //================================================================================
    // SELECTION DE LA SALLE
    //================================================================================

    //  Chercher les cinemas disponibles pour remplir le select.
    //  Cette fonction est executé une fois au chargement de la page.
    $.get(dictURLs["GetCinemas"], null, function (data) {
        //  Remplir le select.
        $.each(data, function (i, item) {
            $("#cinema-select").append(
                '<option value="' + item.CinemaID + '">' + item.Nom + '</option>'
            );
            getSalles();
        });
    });

    //  Chercher les salles correspondant à le cinéma selectionné pour remplir le select.
    //  Cette function est executé après le select des cinemas est rempli et chaque fois que la valeur de ce select est changé.
    function getSalles() {
        //  Get données nécessaires.
        var cinemaID = $("#cinema-select").val();

        //  Executer l'ajax.
        $.get(
            dictURLs["GetSalles"],
            { cinemaID: cinemaID },
            function (data) {
                //  Remplir le select.
                $("#salle-select").empty();
                $.each(data, function (i, item) {
                    $("#salle-select").append(
                        '<option value="' + item.SalleID + '">' + item.Nom + '</option>'
                    );
                });
                refreshEvents();
            }
        );
    }

    //  Mettre à jour les salles chaque fois que le cinéma est changé.
    $("#cinema-select").change(function () {
        getSalles();
    });
});