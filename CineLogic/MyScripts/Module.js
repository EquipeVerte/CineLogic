var app = angular.module("ApplicationModule", ["ngRoute"]);

//The Factory used to define the value to
//Communicate and pass data across controllers

app.factory("ShareData", function () {
    return { value: 0 }
});

//Defining Routing
app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.when('/showempty',
        {
            templateUrl: 'Empty/ShowEmpty',
            controller: 'ShowEmptyController'
        });
    $routeProvider.when('/addempty',
        {
            templateUrl: 'Empty/AddEmpty',
            controller: 'AddEmpController'
        });
    $routeProvider.when("/editempty",
        {
            templateUrl: 'Empty/EditEmpty',
            controller: 'EditEmptyController'
        });
    $routeProvider.when('/deleteempty',
        {
            templateUrl: 'Empty/DeleteEmpty',
            controller: 'DeleteEmptyController'
        });
    $routeProvider.otherwise(
        {
            redirectTo: '/'
        });
    // $locationProvider.html5Mode(true);
    $locationProvider.html5Mode(true).hashPrefix('!')
}]);