var app = angular.module("myApp", ["ng-fusioncharts"]);

app.controller('MyController', function ($scope) {
    $scope.myDataSource = {
        "chart": {
            "caption": "Twoje najczęstsze przewidywania",
            "subcaption": "Drużyna gospodarzy",
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
});