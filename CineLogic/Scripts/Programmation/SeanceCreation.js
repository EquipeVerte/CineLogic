$(document).ready(function () {
    //================================================================================
    // CRÉATION DE LA SÉANCE
    //================================================================================

    //  Confirmer l'ajout d'une nouvelle séance.
    $("#create-seance").click(function () {
        //  Cacher les alerts s'ils sont affichés déjà.
        $("#alert-seance-container").hide();
        $("#success-seance-container").hide();

        if ($('#multi').is(':not(:checked)'))
            $('#fin-date').datepicker('setDate', $('#debut-date').datepicker('getDate'));

        //  Vérifier q'une date est sélectionné.
        if ($("#date-picker").datepicker('getDate') == null) {
            $("#alert-date").show();
        }
        else if ($('#multi').is(':checked') && ($("#debut-date").datepicker('getDate') == null ||
            $("#fin-date").datepicker('getDate') == null) ||
            $("#fin-date").datepicker('getDate') < $("#debut-date").datepicker('getDate')) {
            $("#alert-duree").show();
        }
        else {
            //  Cacher l'alert si une date est selectionné.
            $("#alert-date").hide();
            $("#alert-duree").hide();

            //  Remplir les informations sur la séance à créé dans le modal de confirmation.

            $("#conf-salle").html($("#cinema-select option:selected").text() + " - " + $("#salle-select option:selected").text());
            $("#conf-titre").html($('input[name = "titre"]').val());
            $("#conf-debut").html(new Date($("#date-picker")
                .datepicker('getDate')
                .setMinutes(
                    60 * parseInt($('input[name="debut-hours"]').val()) +
                    parseInt($('input[name="debut-mins"]').val() == "" ? "0" : $('input[name="debut-mins"]').val())))
            );
            $("#conf-fin").html(new Date($("#date-picker")
                .datepicker('getDate')
                .setMinutes(
                    60 * parseInt($('input[name="fin-hours"]').val()) +
                    parseInt($('input[name="fin-mins"]').val() == "" ? "0" : $('input[name="fin-mins"]').val())))
            );

            if ($('#multi').is(':checked')) {
                $("#nbSeances").html($('#type-freq').val() == "Jour" ?
                    Math.floor((Math.abs($('#debut-date').datepicker('getDate') - $('#fin-date').datepicker('getDate')) / 86400000) / parseInt($('#chiffre-freq').val())) + 1
                    : parseInt((countCertainDays(getJoursSem(), $('#debut-date').datepicker('getDate'), $('#fin-date').datepicker('getDate')) / parseInt($('#chiffre-freq').val())) + 1));
                $("#conf-debut-texte").html("Début de script");
                $("#conf-debut").html(new Date($("#debut-date").datepicker('getDate')
                    .setMinutes(
                        60 * parseInt($('input[name="debut-hours"]').val()) +
                        parseInt($('input[name="debut-mins"]').val() == "" ? "0" : $('input[name="debut-mins"]').val()))));
                $("#conf-fin-texte").html("Fin de script");
                $("#conf-fin").html(new Date($("#fin-date").datepicker('getDate')
                    .setMinutes(
                        60 * parseInt($('input[name="fin-hours"]').val()) +
                        parseInt($('input[name="fin-mins"]').val() == "" ? "0" : $('input[name="fin-mins"]').val()))));
            }

            //  Afficher le modal et attendre confirmation.
            $("#confirmationModal").modal('show');
        }
    });

    //  Ajout confirmé.
    $("#btn-confirmed").click(function () {
        $("#btn-confirmed").prop("disabled", true);
        //  Remplir l'objet des données.
        var seanceData = {
            titre: $('input[name = "titre"]').val(),
            salleID: $('#salle-select').val(),
            heureDebut: new Date($("#date-picker")
                .datepicker('getDate')
                .setMinutes(
                    60 * parseInt($('input[name="debut-hours"]').val()) +
                    parseInt($('input[name="debut-mins"]').val() == "" ? "0" : $('input[name="debut-mins"]').val()))
            ),
            heureFin: new Date($("#date-picker")
                .datepicker('getDate')
                .setMinutes(
                    60 * parseInt($('input[name="fin-hours"]').val()) +
                    parseInt($('input[name="fin-mins"]').val() == "" ? "0" : $('input[name="fin-mins"]').val()))
            ),
            multiples: $('#multi').is(':checked'),
            chiffreFreq: parseInt($('#chiffre-freq').val()) == "" ? "1" : $('#chiffre-freq').val(),
            typeFreq: $('#type-freq').val(),
            joursSem: getJoursSem(),
            dateDebutHeureDebut: new Date($('#debut-date').datepicker('getDate')
                .setMinutes(
                    60 * parseInt($('input[name="debut-hours"]').val()) +
                    parseInt($('input[name="debut-mins"]').val() == "" ? "0" : $('input[name="debut-mins"]').val()))
            ),
            dateDebutHeureFin: new Date($('#debut-date').datepicker('getDate')
                .setMinutes(
                    60 * parseInt($('input[name="fin-hours"]').val()) +
                    parseInt($('input[name="fin-mins"]').val() == "" ? "0" : $('input[name="fin-mins"]').val()))
            ),
            dateFin: new Date($('#fin-date').datepicker('getDate')
                .setMinutes(
                    60 * parseInt($('input[name="debut-hours"]').val()) +
                    parseInt($('input[name="debut-mins"]').val() == "" ? "0" : $('input[name="debut-mins"]').val()))
            ),
            nbSeances: $('#type-freq').val() == "Jour" ?
                Math.floor((Math.abs($('#debut-date').datepicker('getDate') - $('#fin-date').datepicker('getDate')) / 86400000) / parseInt($('#chiffre-freq').val())) + 1
                : parseInt(countCertainDays(getJoursSem(), $('#debut-date').datepicker('getDate'), $('#fin-date').datepicker('getDate')))
        }

        //  Executer l'Ajax Post.
        $.ajax({
            type: 'POST',
            url: dictURLs["CreateSeance"],
            dataType: 'json',
            data: '{ seance: ' + JSON.stringify(seanceData) + '}',
            contentType: 'application/json; charset=utf-8',
            success: function () {
                console.log("Post success.");
                $(".unsaved-alert").show();
                $("#confirmationModal").modal('hide');
                $("#alert-seance-container").hide();

                //  Montrer l'alert success et cacher l'alert d'erreur.
                $("#success-seance-container").show();
                $("#alert-seance-container").hide();

                //  Rafraichir les séances.
                refreshEvents();

                //  Réactiver le bouton confirmation.
                $("#btn-confirmed").prop("disabled", false);
            },
            error: function (e) {
                console.log(e);
                $("#alert-seance-container").show();
                $("#alert-seance").empty();
                //  Afficher les erreurs.
                $.each(e.responseJSON, function (i, item) {
                    $("#alert-seance").append("<b>" + dictVarNames[item.Key] + " : </b>");
                    $("#alert-seance").append($.each(item.Errors, function (i2, item2) {
                        return item2;
                    }) + "<br/>");
                });
                $("#confirmationModal").modal('hide');
                $("#btn-confirmed").prop("disabled", false);
            }
        })
    });

    $('.alert .close').on("click", function (e) {
        e.stopPropagation();
        e.preventDefault();
        $(this).parent().hide();
    });



    function getJoursSem() {
        jours = [];
        $('#jours-semaine').find('input[type = checkbox]:checked').each(function (i, item) {
            jours.push(item.id.slice(4));
        });
        return jours;
    }

    function countCertainDays(days, d0, d1) {
        var ndays = 1 + Math.round((d1 - d0) / (24 * 3600 * 1000));
        var sum = function (a, b) {
            return a + Math.floor((ndays + (d0.getDay() + 6 - b) % 7) / 7);
        };
        return days.reduce(sum, 0);
    }
});