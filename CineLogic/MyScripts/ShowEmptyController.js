//The controller has dependency upon the Service and ShareData
app.controller('ShowEmptyController', function ($scope, $location, SinglePageCRUDService, ShareData) {

    loadRecords();

    //Function to Load all Employees Records.   
    function loadRecords() {
        var promiseGetCinema = SinglePageCRUDService.getCinema();

        promiseGetCinema.then(function (pl) { $scope.Cinema = pl.data },
            function (errorPl) {
                $scope.error = 'failure loading Cinema', errorPl;
            });
    }


    //Method to route to the addemployee
    $scope.addCinema = function () {
        $location.path("/addempty");
    }

    //Method to route to the editEmployee
    //The EmpNo passed to this method is further set to the ShareData.
    //This value can then be used to communicate across the Controllers
    $scope.editCinema = function (CinemaID) {
        ShareData.value = CinemaID;
        $location.path("/editempty");
    }

    //Method to route to the deleteEmployee
    //The EmpNo passed to this method is further set to the ShareData.
    //This value can then be used to communicate across the Controllers
    $scope.deleteCinema = function (CinemaID) {
        ShareData.value = CinemaID;
        $location.path("/deleteempty");
    }
});