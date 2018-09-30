var app = angular.module('NewsFeed', []);
app.controller('NewsFeedCtrl', ["$scope", "$http", function ($scope, $http) {

    $scope.listaElementow = [];
    $scope.loadNewsFeedList = function () {
        $http.get("/api/ApiNewsFeed/GetAll")
            .then(function (resultGetData) {
                $scope.listaElementow = resultGetData.data;
            }, function () {
                swal({
                    title: 'Wystapił błąd!',
                    type: 'error',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok!',
                });
            });
    }
    $scope.loadNewsFeedList();


    $scope.addNewsFeedToList = function () {
        if ($scope.poleTekstowe != "") {
            $scope.editedNewsFeed = {};
            //$scope.listaElementow.push({
            //    tresc: $scope.poleTekstowe
            //});
            $http.post("/api/ApiNewsFeed/SaveNewsFeed",
                {
                    Id: $scope.editedNewsFeed.Id,
                    Content: $scope.poleTekstowe
                })
                .then(function (response) {
                    $scope.poleTekstowe = '';
                    $scope.editedNewsFeed = {};
                    $scope.loadNewsFeedList();
                })
                .catch(function (data, status) {
                    swal({
                        title: (data.data != '' ? data.data.Message : 'Wystąpił błąd'),
                        type: 'error',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok!',
                    });
                });

        }
    }
    //$scope.usun = function (element) {
    //    $scope.listaElementow.splice($.inArray(element, $scope.listaElementow), 1);
    //}
}]);





