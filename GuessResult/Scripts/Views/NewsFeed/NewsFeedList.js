var app = angular.module('NewsFeed', []);
app.controller('NewsFeedCtrl', ["$scope", "$http", function ($scope, $http) {

    $scope.commentList = [];

    $scope.newsFeedId = null;
    $scope.loadNewsFeedCommentList = function (newsFeedId) {
        $scope.newsFeedId = newsFeedId;
        $http.get("/api/ApiNewsFeedComment/GetNewsFeedComments?newsFeedId=" + $scope.newsFeedId)
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
        if ($scope.comentContent != "") {
            $http.post("/api/ApiNewsFeedComment/SaveNewsFeedComment",
                {
                    NewsFeedId: $scope.newsFeedId,
                    Content: $scope.commentContent,
                })
                .then(function (response) {
                    $scope.commentContent = '';
                    $scope.loadNewsFeedList();
                    $scope.loadNewsFeedCommentList($scope.newsFeedId);
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

            //let data = new FormData();
            //if ($('#newsFeedFile').val() != '') {
            //    data.append('file', $('#newsFeedFile')[0].files[0]);
            //}

            //$http({
            //    method: 'POST',
            //    url: "/api/ApiNewsFeed/SaveNewsFeed",
            //    //headers: { 'Content-Type': undefined },
            //    data: {
            //        Id: $scope.editedNewsFeed.Id,
            //        Content: $scope.poleTekstowe,
            //    },
            //})

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

    $scope.likeNewsFeed = function (id, isLikedByCurrentUser) {
        if (!isLikedByCurrentUser) {
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
    }


    //$scope.commentCountNewsFeed = function (id) {
    //    $http.post("/api/ApiNewsFeed/Comment", {
    //        id: id
    //    }).then(function (result) {
    //        $scope.loadNewsFeedList();
    //    }, function () {
    //        swal({
    //            title: 'Wystapił błąd!',
    //            type: 'error',
    //            confirmButtonColor: '#3085d6',
    //            confirmButtonText: 'Ok!',
    //        });
    //    });
    //}
    //$scope.showCommentNewsFeed = function (id) {
    //    $('#modalNewsFeedComment').modal();
    //}


    //$scope.showCommentNewsFeed = function (id) {
    //    $http.post("/api/ApiNewsFeedComment/Comment", {
    //        id: id
    //    }).then(function (result) {
    //        $('#modalNewsFeedComment').modal();
    //    }, function () {
    //        swal({
    //            title: 'Wystapił błąd!',
    //            type: 'error',
    //            confirmButtonColor: '#3085d6',
    //            confirmButtonText: 'Ok!',
    //        });
    //    });
    //}

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

    $scope.addNewsFeedFile = function () {
        $('#newsFeedFile').click();
    }

    $('#newsFeedFile').change(function () {
        $scope.newsFeedFileName = $(this)[0].files[0].name;
        $scope.$apply();
    });
}]);

function calculateDivNewsFeedListHeight() {
    $('#divNewsFeedList').css('max-height', $(window).height() - $('#divNewsFeedList').offset().top - ($('footer').height() + 30));
}

function calculateDivNewsFeedCommentListHeight() {
    $('#divNewsFeedCommentList').css('max-height', $(window).height() - $('#divNewsFeedList').offset().top - ($('footer').height() + 30));
}

$(document).ready(function () {
    calculateDivNewsFeedListHeight();
    calculateDivNewsFeedCommentListHeight()
    $(window).resize(function () {
        calculateDivNewsFeedListHeight();
        calculateDivNewsFeedCommentListHeight()
    });
});