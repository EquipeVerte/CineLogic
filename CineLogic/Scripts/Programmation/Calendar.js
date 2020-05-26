
var refreshEvents;

$(document).ready(function () {
    //================================================================================
    // CALENDRIER
    //================================================================================

    var events = [];

    var calendarEl = document.getElementById('calendar');

    $('body').append(
        '<div class="show" id="rmenu">' +
        '<div class="card">' +
        '<ul class="list-group">' +
        '<li id="rmenu-delete" class="list-group-item py-2 px-3">' +
        '<span><i class="far fa-trash-alt"></i> Supprimer</span>' +
        '</li>' +
        '<li id="rmenu-ajuster" class="list-group-item py-2 px-3">' +
        '<span><i class="far fa-clock"></i> Ajuster la durée</span>' +
        '</li>' +
        '<li id="rmenu-edit" class="list-group-item py-2 px-3">' +
        '<span><i class="far fa-edit"></i> Éditer</span>' +
        '</li>' +
        '</ul>' +
        '</div>' +
        '</div>'
    );

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
        snapDuration: '00:15:00',
        eventOverlap: false,
        eventDrop: function (info) {
            timesChanged(info);
        },
        eventResize: function (info) {
            timesChanged(info);
        },
        eventRender: function (info) {
            $(info.el).contextmenu(function (e) {
                e.preventDefault();
                console.log("Context Menu");
                $("#rmenu").show();
                $("#rmenu").css({ 'top': mouseY(event) + 'px' });
                $("#rmenu").css({ 'left': mouseX(event) + 'px' });
                $("#rmenu-delete").on('click', function () {
                    console.log("Delete");
                    console.log(info.event.id);
                    deleteEvent(info.event);
                });
                $("#rmenu-ajuster").on('click', function () {
                    console.log("Ajuster");
                    console.log(info.event.id);
                    adjustTimes(info.event);
                });
                $("#rmenu-edit").on('click', function () {
                    console.log("Edit");
                    console.log(info.event.id);
                    window.open(dictURLs["EditSeance"] + "/" + info.event.id);
                });

                window.event.returnValue = false;
            });
        }
    });

    //  Cacher le context menu quand le bouton gauche du souris est cliqué.
    $(document).bind("click", function (event) {
        $("#rmenu").hide();
    });

    //  Fonctions pour rétourner les positions du souris.
    function mouseX(evt) {
        if (evt.pageX) {
            return evt.pageX;
        } else if (evt.clientX) {
            return evt.clientX + (document.documentElement.scrollLeft ?
                document.documentElement.scrollLeft :
                document.body.scrollLeft);
        } else {
            return null;
        }
    }

    function mouseY(evt) {
        if (evt.pageY) {
            return evt.pageY;
        } else if (evt.clientY) {
            return evt.clientY + (document.documentElement.scrollTop ?
                document.documentElement.scrollTop :
                document.body.scrollTop);
        } else {
            return null;
        }
    }

    //  Supprimer un event par ajax.
    function deleteEvent(event) {
        $.ajax({
            type: 'POST',
            url: dictURLs["DeleteSeanceAjax"],
            dataType: 'json',
            data: '{seanceID: ' + event.id + '}',
            contentType: 'application/json; charset=utf-8',
            success: function () {
                console.log("Post success.");
                event.remove();
                $("#unsaved-alert").show();
                animateSuccess();
            },
            error: function (e) {
                alert("Suppression échoué!");
                animateFailure();
            }
        });
    }

    function adjustTimes(event) {
        $.ajax({
            type: 'POST',
            url: dictURLs["AdjustSeanceTimes"],
            dataType: 'json',
            data: '{seanceID: ' + event.id + '}',
            contentType: 'application/json; charset=utf-8',
            success: function () {
                console.log("Post success.");
                $("#unsaved-alert").show();
                refreshEvents();
                animateSuccess();
            },
            error: function (e) {
                alert("Ajustement échoué!");
                animateFailure();
            }
        });
    }

    //  Appelé quand les heures d'un séance sont changé par le calendrier.
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
                $("#unsaved-alert").show();
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
