
var refreshEvents;

$(document).ready(function () {
    //================================================================================
    // CALENDRIER
    //================================================================================

    var events = [];

    var calendarEl = document.getElementById('calendar');

    //  Définir les options pour le calendrier.
    var calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: ['timeGrid', 'interaction', 'bootstrap'],
        defaultView: 'timeGridWeek',
        themeSystem: 'bootstrap',
        locale: 'fr',
        allDaySlot: false,
        minTime: "06:00:00",
        maxTime: "24:00:00",
        events: events,
        height: "parent",
        eventTimeFormat: { // like '14:30:00'
            hour: '2-digit',
            minute: '2-digit',
            hour12: false
        },
        editable: true,
        eventOverlap: false,
        eventDrop: function (info) {
            timesChanged(info);
        },
        eventResize: function (info) {
            timesChanged(info);
        }
    });



    function timesChanged(info) {

        var data = {
            seanceID: info.event.id,
            heureDebut: new Date(info.event.start),
            heureFin: new Date(info.event.end)
        };

        console.log(data);

        $.ajax({
            type: 'POST',
            url: dictURLs["UpdateSeanceTimes"],
            dataType: 'json',
            data: '{seanceVM: ' + JSON.stringify(data) + '}',
            contentType: 'application/json; charset=utf-8',
            success: function () {
                console.log("Post success.");
                animateSuccess();
            },
            error: function (e) {
                console.log(e);
                info.revert();
                animateFailure();
            }
        })
    }

    function animateSuccess() {
        console.log("Success");
    };

    function animateFailure() {
        console.log("Fail");
    };

    //  Créer le calendrier
    calendar.render();

    //  Mettre à jour les événements quand la salle est changé.
    $("#salle-select").change(function () {
        refreshEvents();
    });

    //  Flag pour empecher la fonction de refresh d'executer plus q'une fois.
    var eventsRefreshing = false;

    //  Fonction pour mettre à jour les evenements.
    refreshEvents = function () {
        if (eventsRefreshing) return;
        eventsRefreshing = true;

        //  Vider les événements.
        $.each(calendar.getEventSources(), function (i, item) {
            calendar.getEventSources()[i].remove();
        });

        //  Chercher l'ID de la salle.
        var salleID = $('#salle-select').val();

        //  Empecher la salle d'être changé pendant le mise à jour.
        $('#salle-select').prop('disabled', true);

        var jqxhr = $.get(dictURLs["GetSeances"], { salleID: salleID }, function (data) {
            var events = [];
            //  Ajouter chaque evenement dans le tableau des evenements.
            $.each(data, function (i, item) {
                events.push({
                    id: item.SeanceID,
                    url: dictURLs["EditSeance"] + '/' + item.SeanceID,
                    title: item.Titre + (item.ContenuTitre == null ? "" : " - " + item.ContenuTitre),
                    start: item.HeureDebut,
                    end: item.HeureFin,
                    backgroundColor: item.ContenuTitre == null ? 'primary' : '#5CB85C',
                    borderColor: item.ContenuTitre == null ? 'primary' : '#5CB85C',
                    eventTitle: item.Titre,
                    contenuTitre: item.ContenuTitre
                });
            });
            //  Ajouter les evenements à le calendrier.
            calendar.addEventSource(events);

            //  Permettre le changement de la salle.
            eventsRefreshing = false;
            $('#salle-select').prop('disabled', false);
        });
    };

    //  Fonction pour changer la position du calendrier.
    $("#datechanger").click(function () {
        if ($("#date-picker").datepicker('getDate') != null) {
            calendar.gotoDate($("#date-picker").datepicker('getDate'));
        }
    });
});
