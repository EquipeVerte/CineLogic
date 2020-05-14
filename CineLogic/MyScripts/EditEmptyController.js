app.controller("EditEmptyController", function ($scope, $location, ShareData, SinglePageCRUDService) {

    getCinema();
    function getCinema() {

        var promiseGetCinema = SinglePageCRUDService.getCinema(ShareData.value);


        promiseGetCinema.then(function (pl) {
            $scope.Cinema = pl.data;
        },
            function (errorPl) {
                $scope.error = 'failure loading Empty', errorPl;
            });
    }


    //The Save method used to make HTTP PUT call to the WEB API to update the record

    $scope.save = function () {
        var Cinema = {
            CinemaID: $scope.Cinema.CinemaID,
            Nom: $scope.Cinema.Nom,
            Adresse: $scope.Cinema.Adresse,
            EnExploitation: $scope.Cinema.EnExploitation,
            ResponsableID: $scope.Cinema.ResponsableID,
            Programmateur: $scope.Cinema.Programmateur
        };

        var promisePutCinema = SinglePageCRUDService.put($scope.Cinema.CinemaID, Cinema);
        promisePutCinema.then(function (pl) {
            $location.path("/showempty");
        },
            function (errorPl) {
                $scope.error = 'failure loading Empty', errorPl;
            });
    };
});