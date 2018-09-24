var app = angular.module("myApp", ["ng-fusioncharts"]);

app.controller('MyController', ["$scope", "$http", function ($scope, $http) {
    $scope.topUsersDataSource = {
        "chart": {
            "caption": "Najlepsi użytkownicy",
            "xaxisname": "Użytkownicy",
            "yaxisname": "Statystyki",
            "numbersuffix": "%",
            "theme": "candy"
        },
        "data": []
    };
    $scope.loadTopUsersData = function () {
        $http.get("/api/ApiEvent/GetTopUsersData")
            .then(function (resultGetData) {
                $scope.topUsersDataSource.data = resultGetData.data;
            }, function () {
                swal({
                    title: 'Wystapił błąd!',
                    type: 'error',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok!',
                });
            });
    }
    $scope.loadTopUsersData();
}]);