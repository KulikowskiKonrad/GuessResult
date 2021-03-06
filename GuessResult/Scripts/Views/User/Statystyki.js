﻿var app = angular.module("myApp", ["ng-fusioncharts"]);

app.controller('MyController', ["$scope", "$http", function ($scope, $http) {
    $scope.effectivityFilter = '1';
    $scope.effectivityDataSource = {
        "chart": {
            "caption": "Skuteczność typów",
            //"subcaption": "Drurzyna gospodarzy",
            "showvalues": "1",
            "showpercentintooltip": "0",
            //"numberprefix": "$",
            //"enablemultislicing": "1",
            "theme": "fusion",
        },
        "data": []
    };

    $scope.$watchCollection('effectivityFilter', function () {
        $scope.loadEffectivityData();
    });

    $scope.loadEffectivityData = function () {
        //let filterEventStatus = ($scope.filterEventStatus != null ? $scope.filterEventStatus.Id : '')
        //let filterOnlyMyEvents = ($scope.filterOnlyMyEvents != null ? $scope.filterOnlyMyEvents : false);
        $http.get("/api/ApiEvent/GetUserEffectivityData?effectivityFilter=" + $scope.effectivityFilter)
            .then(function (resultGetData) {
                $scope.effectivityDataSource.data = resultGetData.data;
            }, function () {
                swal({
                    title: 'Wystapił błąd!',
                    type: 'error',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok!',
                });
            });
    }
    $scope.loadEffectivityData();


    $scope.effectivityTotalDataSource = {
        "chart": {
            "caption": "Całkowita skuteczność",
            //"subcaption": "Drurzyna gospodarzy",
            "showvalues": "1",
            "showpercentintooltip": "0",
            //"numberprefix": "$",
            //"enablemultislicing": "1",
            "theme": "fusion",
        },
        "data": []
    };
    $scope.loadEffectivityTotalData = function () {
        $http.get("/api/ApiEvent/GetUserEffectivityData")
            .then(function (resultGetData) {
                $scope.effectivityTotalDataSource.data = resultGetData.data;
            }, function () {
                swal({
                    title: 'Wystapił błąd!',
                    type: 'error',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok!',
                });
            });
    }
    $scope.loadEffectivityTotalData();
}]);