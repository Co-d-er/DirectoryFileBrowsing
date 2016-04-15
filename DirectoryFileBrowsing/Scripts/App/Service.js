/// <reference path="Libraries/angular.min.js" />
/// <reference path="App/Module.js" />

app.service("MyService", function ($http) {
    
    this.getStartPage = function () {
        return $http.get("/api/Value/GetStartPage");
    };

    this.getFolders = function (data) {
        var config = {
            params: data
        };
        return $http.get("/api/Value/GetFolders", config);
    };

    this.getFoldersOfDisk = function (data) {
        var config = {
            params: data
        };
        return $http.get("/api/Value/GetFoldersOfDisk", config);
    };

    
})