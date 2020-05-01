$(document).ready(function () {

    //  Chercher les cinemas disponibles.
    $.get('@Url.Action("Cinemas")', null, function (data) {
        $.each(data, function (i, item) {
            $("#cinema-select").append(
                '<option value="' + item.CinemaID + '">' + item.Nom + '</option>'
            );
            getSalles();
        });
    }
    );

    //  Chercher les salles correspondant à le cinéma selectionné.
    function getSalles() {

        var cinemaID = $("#cinema-select").val();

        $.get(
            '@Url.Action("Salles")',
            { cinemaID: cinemaID },
            function (data) {
                $("#salle-select").empty();
                $.each(data, function (i, item) {
                    $("#salle-select").append(
                        '<option value="' + item.SalleID + '">' + item.Nom + '</option>'
                    );
                });
            }
        );
    }

    //  Mettre à jour les salles chaque fois que le cinéma est changé.
    $("#cinema-select").change(function () {
        getSalles();
    });

    //  Créer le datepicker.
    var options = {
        todayBtn: 'linked',
        language: 'fr',
        orientation: 'bottom left',
        daysOfWeekHighlighted: '0,6'
    };
    $("#date-picker").datepicker(options);

    //  Ajouter une nouvelle séance.
    $("#create-seance").click(function () {
        console.log("Create clicked.");

        if ($("#date-picker").datepicker('getDate') == null) {
            console.log("Alert");
            $("#alert-date").show();
        }
        else {
            $("#alert-date").hide();

            $("#conf-salle").html($("#cinema-select option:selected").text() + " - " + $("#salle-select option:selected").text());
            $("#conf-titre").html($('input[name = "titre"]').val());
            $("#conf-debut").html(new Date($("#date-picker").datepicker('getDate').setMinutes(60 * parseInt($('input[name="debut-hours"]').val()) + parseInt($('input[name="debut-mins"]').val()))));
            $("#conf-fin").html(new Date($("#date-picker").datepicker('getDate').setMinutes(60 * parseInt($('input[name="fin-hours"]').val()) + parseInt($('input[name="fin-mins"]').val()))));
            $("#confirmationModal").modal('show');
        }
    });


    $("#btn-confirmed").click(function () {

        $("#confirmationModal").modal('hide');

        console.log($('input[name = "titre"]').val());
        console.log($('#salle-select').val());
        console.log($("#date-picker").datepicker('getDate'));
        console.log(new Date($("#date-picker").datepicker('getDate').setMinutes(60 * parseInt($('input[name="debut-hours"]').val()) + parseInt($('input[name="debut-mins"]').val()))));
        console.log(new Date($("#date-picker").datepicker('getDate').setMinutes(60 * parseInt($('input[name="fin-hours"]').val()) + parseInt($('input[name="fin-mins"]').val()))));


        var seanceData = {
            titre: $('input[name = "titre"]').val(),
            salleID: $('#salle-select').val(),
            heureDebut: new Date($("#date-picker").datepicker('getDate').setMinutes(60 * parseInt($('input[name="debut-hours"]').val()) + parseInt($('input[name="debut-mins"]').val()))),
            heureFin: new Date($("#date-picker").datepicker('getDate').setMinutes(60 * parseInt($('input[name="fin-hours"]').val()) + parseInt($('input[name="fin-mins"]').val()))),
        }

        $.ajax({
            type: 'POST',
            url: '@Url.Action("Create")',
            dataType: 'json',
            data: '{ seance: ' + JSON.stringify(seanceData) + '}',
            contentType: 'application/json; charset=utf-8',
            success: function () {
                console.log("Post success.");
            },
            error: function (e) {
                console.log(e);
            }
        })
    });

    //  Créer le calendrier des séances.
    var events = [];

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: ['timeGrid', 'bootstrap'],
        defaultView: 'timeGridWeek',
        themeSystem: 'bootstrap',
        locale: 'fr',
        allDaySlot: false,
        minTime: "08:00:00",
        maxTime: "24:00:00",
        events: events,
        height: "parent",
        eventTimeFormat: { // like '14:30:00'
            hour: '2-digit',
            minute: '2-digit',
            hour12: false
        },
        eventClick: function (info) {
            console.log(info.event);
        }
    });

    calendar.render();

    $("#refresh-events").click(function () {
        calendar.getEventSources()[0].remove();

        var salleID = $('#salle-select').val();

        var jqxhr = $.get('@Url.Action("Seances")', { salleID: salleID }, function (data) {
            var events = [];
            $.each(data, function (i, item) {
                console.log(item);
                events.push({
                    id: item.SeanceID,
                    title: item.Titre + (item.ContenuTitre == null ? "" : item.ContenuTitre),
                    start: item.HeureDebut,
                    end: item.HeureFin,
                    backgroundColor: item.ContenuTitre == null ? 'primary' : '#5CB85C',
                    borderColor: item.ContenuTitre == null ? 'primary' : '#5CB85C',
                    eventTitle: item.Titre,
                    contenuTitre: item.ContenuTitre
                });
            });
            calendar.addEventSource(events);
        });
    });

    $("#datechanger").click(function () {
        console.log($("#date-picker").datepicker('getDate'));
        calendar.gotoDate($("#date-picker").datepicker('getDate'));
    });
})
    .ajaxStart(function () {
        console.log("ajax start");
        $("#loading").show();
    })
    .ajaxStop(function () {
        console.log("ajax stop");
        $("#loading").hide();
    });

