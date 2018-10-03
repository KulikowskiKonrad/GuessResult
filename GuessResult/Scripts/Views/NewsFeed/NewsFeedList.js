var app = angular.module('NewsFeed', []);
app.controller('NewsFeedCtrl', ["$scope", "$http", function ($scope, $http) {

    $scope.commentList = [];

    $scope.loadNewsFeedCommentList = function () {
        $http.get("/api/ApiNewsFeedComment/GetAllNewsFeedComment")
            .then(function (resultGetData) {
                $scope.commentList = resultGetData.data;
                $('#modalNewsFeedComment').modal();
            }, function () {
                swal({
                    title: 'Wystapił błąd!',
                    type: 'error',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok!',
                });
            });
    }
    $scope.loadNewsFeedCommentList();



    $scope.listaElementow = [];
    $scope.loadNewsFeedList = function () {
        $http.get("/api/ApiNewsFeed/GetAll")
            .then(function (resultGetData) {
                $scope.listaElementow = resultGetData.data;
                $('#modalNewsFeed').modal();
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


    $scope.addNewsFeedCommentToList = function () {
        if ($scope.text != "") {
            $scope.editedNewsFeed = {};
            $http.post("/api/ApiNewsFeedComment/SaveNewsFeedComment",
                {
                    Id: $scope.editedNewsFeed.Id,
                    Content: $scope.text,
                })
                .then(function (response) {
                    $scope.text = '';
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



    $scope.addNewsFeedToList = function () {
        if ($scope.poleTekstowe != "") {
            $scope.editedNewsFeed = {};
            $http.post("/api/ApiNewsFeed/SaveNewsFeed",
                {
                    Id: $scope.editedNewsFeed.Id,
                    Content: $scope.poleTekstowe,
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

    $scope.likeNewsFeed = function (id) {
        $http.post("/api/ApiNewsFeed/Like", {
            id: id
        }).then(function (result) {
            $scope.loadNewsFeedList();
        }, function () {
            swal({
                title: 'Wystapił błąd!',
                type: 'error',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok!',
            });
        });
    }

    $scope.remove = function (id) {


        swal({
            title: 'Na pewno chcesz to usunąć?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Tak,chcę to usunąć!',
            cancelButtonText: 'Anuluj'
        }).then(function () {
            $http.delete("/api/ApiNewsFeed/Remove?id=" + id)
                .then(function () {
                    $scope.loadNewsFeedList();
                }).catch(function (data, status) {
                    swal({
                        title: data.data,
                        type: 'error',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok!',
                    });
                });
        })
        //$scope.listaElementow.splice($.inArray(element, $scope.listaElementow), 1);


        $scope.loadNewsFeedList();
    }
}]);

function calculateDivNewsFeedListHeight() {
    $('#divNewsFeedList').css('max-height', $(window).height() - $('#divNewsFeedList').offset().top - ($('footer').height() + 30));
}

$(document).ready(function () {
    calculateDivNewsFeedListHeight();
    $(window).resize(function () {
        calculateDivNewsFeedListHeight();
    });
});