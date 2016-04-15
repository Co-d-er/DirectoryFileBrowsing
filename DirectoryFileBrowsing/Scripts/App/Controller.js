/// <reference path="Libraries/angular.min.js" />
/// <reference path="App/Module.js" />

app.controller('crudController', function ($scope, MyService) {

    load();

    //Function to load start page. 
    //Variable "$scope.loading" uses for showing gif animation while waiting response.
    function load() {
        $scope.loading = true;
        var promiseGet = MyService.getStartPage();

        promiseGet.then(function (pl) {
            $scope.Respond = pl.data;
            $scope.selected = pl.data.Disk;
            $scope.loading = false;
            $scope.isRoot = pl.data.IsRoot;
        },
              function (errorPl) {
                  $log.error('failure loading', errorPl);
              });
    };

    //Function to change folder
    $scope.changeFolder = function (currentPath, folder) {
        var params = {
            path: currentPath,
            dir: folder
        };      
        $scope.loading = true;
        var promise = MyService.getFolders(params);

        promise.then(function (pl) {
            $scope.Respond = pl.data;
            $scope.selected = pl.data.Disk;
            $scope.loading = false;
            $scope.isRoot = pl.data.IsRoot;
        },
              function (errorPl) {
                  $log.error('failure loading', errorPl);
              });
    }

    // Function to change disk
    $scope.changeDisk = function (selectedDisk) {
        var param = {
            disk: selectedDisk
        };
        $scope.loading = true;
        var promise = MyService.getFoldersOfDisk(param);

        promise.then(function (pl) {
            $scope.Respond = pl.data;
            $scope.selected = pl.data.Disk;
            $scope.loading = false;         
            $scope.isRoot = pl.data.IsRoot;
        },
             function (errorPl) {
                 $log.error('failure loading', errorPl);
             });
    };
});
