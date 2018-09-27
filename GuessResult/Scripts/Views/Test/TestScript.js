var app = angular.module('Tt', []);
app.controller('TestCtrl', ["$scope", "$http", function ($scope, $http) {

    $scope.listaElementow = [];
    $scope.addElementToList = function () {
        if ($scope.poleTekstowe != "") {
            $scope.listaElementow.push({
                dataDodania: new Date(),
                tresc: $scope.poleTekstowe
            })
        }
    }
    $scope.usun = function (element) {
        $scope.listaElementow.splice($.inArray(element, $scope.listaElementow), 1);
    }
}]);





