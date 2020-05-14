app.controller('AddEmpController', function ($scope, SinglePageCRUDService) {
    $scope.CinemaID = 0;
    //The Save scope method used to define the Employee object and 
    //post the Employee information to the server by making call to the Service
    $scope.save = function () {
        var Cinema = {
            CinemaID: $scope.CinemaID,
            Nom: $scope.Nom,
            Adresse: $scope.Adresse,
            EnExploitation: $scope.EnExploitation,
            ResponsableID: $scope.ResponsableID,
            Programmateur: $scope.Programmateur
        };

        var promisePost = SinglePageCRUDService.post(Cinema);


        promisePost.then(function (pl) {
            $scope.CinemaID = pl.data.CinemaID;
            alert("EmpNo " + pl.data.CinemaID);
        },
            function (errorPl) {
                $scope.error = 'failure loading Employee', errorPl;
            });

    };
});