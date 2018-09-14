var app = angular.module("myApp", ["ng-fusioncharts"]);

app.controller('MyController', ["$scope", "$http", function ($scope, $http) {
    $scope.effectivityData = [];
    $scope.myDataSource = {
        "chart": {
            "caption": "Skuteczność typów",
            //"subcaption": "Drużyna gospodarzy",
            "showvalues": "1",
            "showpercentintooltip": "0",
            "numberprefix": "$",
            "enablemultislicing": "1",
            "theme": "fusion"
        },
        "data": []
        //[
        //    {
        //        "label": "Wygrana",
        //        "value": "300000"
        //    },
        //    {
        //        "label": "Przegrana",
        //        "value": "230000"
        //    },
        //    {
        //        "label": "Remis",
        //        "value": "20000"
        //    }
        //]
    };

    $scope.$watchCollection('effectivityFilter', function () {
        $scope.loadEffectivityData();
    });

    $scope.loadEffectivityData = function () {
        //let filterEventStatus = ($scope.filterEventStatus != null ? $scope.filterEventStatus.Id : '')
        //let filterOnlyMyEvents = ($scope.filterOnlyMyEvents != null ? $scope.filterOnlyMyEvents : false);
        $http.get("/api/ApiEvent/GetUserEffectivityData?effectivityFilter=" + $scope.effectivityFilter)
            .then(function (resultGetData) {
                $scope.myDataSource.data = resultGetData.data;
            });
    }
    $scope.loadEffectivityData();

    $scope.myDataSourceAwayTeam = {
        "chart": {
            "caption": "Twoje najczęstsze przewidywania",
            "subcaption": "Drużyna przeciwna",
            "showvalues": "1",
            "showpercentintooltip": "0",
            "numberprefix": "$",
            "enablemultislicing": "1",
            "theme": "fusion"
        },
        "data": [
            {
                "label": "Wygrana",
                "value": "300000"
            },
            {
                "label": "Przegrana",
                "value": "230000"
            },
            {
                "label": "Remis",
                "value": "20000"
            }
        ]
    };
}]);