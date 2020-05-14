app.controller("DeleteEmptyController", function ($scope, $location, ShareData, SinglePageCRUDService) {

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

    //The delete method used to make HTTP DELETE call to the WEB API to update the record
    $scope.delete = function () {
        var promiseDeleteCinema = SinglePageCRUDService.delete(ShareData.value);

        promiseDeleteCinema.then(function (pl) {
            $location.path("/showempty");
        },
            function (errorPl) {
                $scope.error = 'failure loading Empty', errorPl;
            });
    };

});