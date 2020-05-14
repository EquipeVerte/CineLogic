app.service("SinglePageCRUDService", function ($http) {

    //Function to Read All Employees
    this.getCinema = function () {
        return $http.get("/api/CinemasAPI");
    };

    //Function to Read Employee based upon id
    this.getCinema = function (id) {
        return $http.get("/api/CinemasAPI/" + id);
    };

    //Function to create new Employee
    this.post = function (Cinema) {
        var request = $http({
            method: "post",
            url: "/api/CinemasAPI",
            data: Cinema
        });
        return request;
    };

    //Function  to Edit Employee based upon id 
    this.put = function (id, Cinema) {
        var request = $http({
            method: "put",
            url: "/api/CinemasAPI/" + id,
            data: Cinema
        });
        return request;
    };

    //Function to Delete Employee based upon id
    this.delete = function (id) {
        var request = $http({
            method: "delete",
            url: "/api/CinemasAPI/" + id
        });
        return request;
    };
});