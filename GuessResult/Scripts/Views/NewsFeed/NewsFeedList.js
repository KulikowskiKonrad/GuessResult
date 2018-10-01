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
            $http.post("/api/ApiNewsFeed/SaveNewsFeed",
                {
                    Id: $scope.editedNewsFeed.Id,
                    Content: $scope.poleTekstowe,
                    InsertDate: $scope.editedNewsFeed.InsertDate,
                    InsertUserEmail: $scope.editedNewsFeed.InsertUserEmail
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

    $scope.remove = function (element) {
        $http.post("/api/ApiNewsFeed/Remove?id=" + element.Id)
        //$scope.listaElementow.splice($.inArray(element, $scope.listaElementow), 1);


        $scope.loadNewsFeedList();
    }
}]);





