var app = angular.module('NewsFeed', []);
app.controller('NewsFeedCtrl', ["$scope", "$http", function ($scope, $http) {

    $scope.listaElementow = [];
    $scope.addNewsFeedToList = function () {
        if ($scope.poleTekstowe != "") {
            $scope.listaElementow.push({
                tresc: $scope.poleTekstowe
            })
        }
    }
    //$scope.usun = function (element) {
    //    $scope.listaElementow.splice($.inArray(element, $scope.listaElementow), 1);
    //}
}]);





