var app = angular.module("myApp", ["ng-fusioncharts"]);

app.controller('MyController', ["$scope", "$http", function ($scope, $http) {
    debugger;
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
            });
    }
    $scope.loadTopUsersData();

    //$scope.myDataSource = {
    //    "chart": {
    //        "caption": "Najlepsi użytkownicy",
    //        "xaxisname": "Użytkownicy",
    //        "yaxisname": "Statystyki",
    //        "numbersuffix": "%",
    //        "theme": "candy"
    //    },
    //    "data": [
    //        {
    //            "label": "Venezuela",
    //            "value": "100"
    //        },
    //        {
    //            "label": "Saudi",
    //            "value": "100"
    //        },
    //        {
    //            "label": "Canada",
    //            "value": "100"
    //        },
    //        {
    //            "label": "Iran",
    //            "value": "80"
    //        },
    //        {
    //            "label": "Russia",
    //            "value": "45"
    //        },
    //        {
    //            "label": "UAE",
    //            "value": "100"
    //        },
    //        {
    //            "label": "US",
    //            "value": "30"
    //        },
    //        {
    //            "label": "China",
    //            "value": "30"
    //        }
    //    ]
    //};
}]);