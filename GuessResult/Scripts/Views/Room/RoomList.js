var app = angular.module('RR', []);
app.controller('RoomListCtrl', ["$scope", "$http", function ($scope, $http) {
    $scope.loadList = function () {
        $http.get("/api/ApiRoom/GetAll")
            .then(function (res, status, xhr) {
                $scope.rooms = res.data;
            });
    }
    $scope.loadList();

    $scope.delete = function (roomId) {
        swal({
            title: 'Na pewno chcesz to usunąć?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Tak,chcę to usunąć!',
            cancelButtonText: 'Anuluj'
        }).then(function () {
            $http.delete("/api/ApiRoom/Delete/?id=" + roomId)
                .then(function (response) {
                    $scope.loadList();
                })
                .catch(function (data, status) {
                    swal({
                        title: data.data,
                        type: 'error',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok!',
                    });
                });
        })
    }

    $scope.edit = function (roomId) {
        $scope.editedRoom = null;

        if (roomId != null) {
            for (let i = 0; i < $scope.rooms.length; i++) {
                if ($scope.rooms[i].Id == roomId) {
                    $scope.editedRoom = angular.copy($scope.rooms[i]);
                }
            }
        }
        $('#modalRoomDetails').modal();
    }

    $scope.cancelEdit = function () {
        $('#modalRoomDetails').modal('hide');
    }

    $scope.saveDetails = function () {
        angular.forEach($scope.formSaveRoomDetails.$error, function (field) {
            angular.forEach(field, function (errorField) {
                errorField.$setTouched();
            })
        });

        if ($scope.formSaveRoomDetails.$valid) {
            $http.post("/api/ApiRoom/SaveDetails",
                {
                    Id: $scope.editedRoom.Id,
                    Name: $scope.editedRoom.Name,
                    Details: $scope.editedRoom.Details
                })
                .then(function (response) {
                    $scope.editedRoom = null;
                    $('#modalRoomDetails').modal('hide');
                    $scope.loadList();
                })
                .catch(function (data, status) {
                    swal({
                        title: data.data,
                        type: 'error',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok!',
                    });
                });
        }
    }


    //$scope.registerUser = function () {
    //    angular.forEach($scope.formRegisterUser.$error, function (field) {
    //        angular.forEach(field, function (errorField) {
    //            errorField.$setTouched();
    //        })
    //    });

    //    if ($scope.formRegisterUser.$valid) {
    //        $http.post("/api/ApiUser/RegisterUser",
    //            {
    //                "Email": $scope.Email,
    //                "Password": $scope.Password,
    //                "ConfirmPassword": $scope.ConfirmPassword
    //            })
    //            .then(function (response) {
    //                swal({
    //                    title: 'Użytkownik zarejestrowany',
    //                    type: 'success',
    //                    confirmButtonColor: '#3085d6',
    //                    confirmButtonText: 'Ok!',
    //                }).then(function () {
    //                    location.href = '/User/Login';
    //                });
    //            })
    //            .catch(function (data, status) {
    //                swal({
    //                    title: data.data,
    //                    type: 'error',
    //                    confirmButtonColor: '#3085d6',
    //                    confirmButtonText: 'Ok!',
    //                });
    //            });
    //    }
}]);
