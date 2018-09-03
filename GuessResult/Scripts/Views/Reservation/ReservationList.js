var app = angular.module('RR', []);
app.controller('ReservationListCtrl', ["$scope", "$http", function ($scope, $http) {
    $scope.loadList = function () {
        let roomId = ($scope.filteredRoom != null ? $scope.filteredRoom.Id : '');
        $http.get("/api/ApiReservation/GetAll?roomId=" + roomId)
            .then(function (res, status, xhr) {
                $scope.reservations = res.data;
            });
    }
    $scope.loadList();

    $scope.$watchCollection('filteredRoom', function () {
        $scope.loadList();
    });

    $scope.loadRoomList = function () {
        $http.get("/api/ApiRoom/GetAll")
            .then(function (res, status, xhr) {
                $scope.rooms = res.data;
            });
    }
    $scope.loadRoomList();

    $scope.delete = function (reservationId) {
        swal({
            title: 'Na pewno chcesz to usunąć?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Tak,chcę to usunąć!',
            cancelButtonText: 'Anuluj'
        }).then(function () {
            $http.delete("/api/ApiReservation/Delete/?id=" + reservationId)
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

    $scope.changeStatus = function (status, reservationId) {
        swal({
            title: 'Na pewno chcesz zmienić status?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Tak,chcę!',
            cancelButtonText: 'Anuluj'
        }).then(function () {
            $http.post("/api/ApiReservation/ChangeStatus", {
                Id: reservationId,
                Status: status
            })
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



    $scope.edit = function (reservationId) {
        $scope.editedReservation = {};
        $scope.selectedRoom = {};

        if (reservationId != null) {
            for (let i = 0; i < $scope.reservations.length; i++) {
                if ($scope.reservations[i].Id == reservationId) {
                    $scope.editedReservation = angular.copy($scope.reservations[i]);

                }
            }

            for (let i = 0; i < $scope.rooms.length; i++) {
                if ($scope.rooms[i].Id == $scope.editedReservation.RoomId) {
                    $scope.selectedRoom = $scope.rooms[i];
                }
            }
        }
        $('#modalReservationDetails').modal();
    }

    $scope.cancelEdit = function () {
        $scope.editedReservation = null;
        $('#modalReservationDetails').modal('hide');
    }

    $scope.saveDetails = function () {
        angular.forEach($scope.formSaveReservationDetails.$error, function (field) {
            angular.forEach(field, function (errorField) {
                errorField.$setTouched();
            })
        });

        if ($scope.formSaveReservationDetails.$valid) {
            $http.post("/api/ApiReservation/SaveReservationDetails",
                {
                    Id: $scope.editedReservation.Id,
                    Date: $scope.editedReservation.Date,
                    RRRoomId: $scope.selectedRoom.Id
                })
                .then(function (response) {
                    $scope.editReservation = null;
                    $('#modalReservationDetails').modal('hide');
                    $scope.loadList();
                })
                .catch(function (data, status) {
                    swal({
                        title: data.data.Message,
                        type: 'error',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok!',
                    });
                });
        }
    }
}])
    .directive('date', function (dateFilter) {
        return {
            require: 'ngModel',
            link: function (scope, elm, attrs, ctrl) {

                var dateFormat = attrs['date'] || 'yyyy-MM-dd';

                ctrl.$formatters.unshift(function (modelValue) {
                    return dateFilter(modelValue, dateFormat);
                });
            }
        };
    })
    ;