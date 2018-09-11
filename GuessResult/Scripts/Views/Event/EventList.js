var app = angular.module('GR', []);
app.controller('EventListCtrl', ["$scope", "$http", function ($scope, $http) {

    $scope.eventStatusList = [
        {
            Id: 1,
            Name: 'Zakończony'
        },
        {
            Id: 2,
            Name: 'Przyszły'
        },
    ];
   // dodajesz + "&nowyParametr=" + $scope.coscos

    $scope.loadEventList = function () {
        let filterEventStatus = ($scope.filterEventStatus != null ? $scope.filterEventStatus.Id : '')
        let filterOnlyMyEvents = ($scope.filterOnlyMyEvents == true || $scope.filterOnlyMyEvents == false)
        $http.get("/api/ApiEvent/GetAll?filterEventStatus=" + filterEventStatus +"&filterOnlyMyEvents="+ filterOnlyMyEvents)
            .then(function (resultGetData) {
                $scope.events = resultGetData.data;
            });
    }
    //$scope.loadEventList();
    $scope.$watchCollection('filterEventStatus', function () {
        $scope.loadEventList();
    });

    $scope.$watchCollection('filterOnlyMyEvents', function () {
        $scope.loadEventList();
    });

    $scope.delete = function (eventId) {
        swal({
            title: 'Na pewno chcesz to usunąć?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Tak,chcę to usunąć!',
            cancelButtonText: 'Anuluj'
        }).then(function () {
            $http.delete("/api/ApiEvent/Delete/?id=" + eventId)
                .then(function (response) {
                    $scope.loadEventList();
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

    //debugger;
    $scope.edit = function (eventId) {
        $scope.editedEvent = {};
        //$scope.selectedEvent = {};
        if (eventId != null) {
            for (let i = 0; i < $scope.events.length; i++) {
                if ($scope.events[i].Id == eventId) {
                    $scope.editedEvent = angular.copy($scope.events[i]);
                }
            }
        }
        $('#modalEventDetails').modal();
    }


    $scope.cancelEdit = function () {
        $scope.editedEvent = null;
        $('#modalEventDetails').modal('hide');
    }

    $scope.SaveEventDetails = function () {
        //angular.forEach($scope.formSaveEventDetails.$error, function (field) {
        //    angular.forEach(field, function (errorField) {
        //        errorField.$setTouched();
        //    })
        //}
        //);

        if ($scope.formSaveEventDetails.$valid) {
            $http.post("/api/ApiEvent/SaveEventDetails",
                {
                    Id: $scope.editedEvent.Id,
                    Name: $scope.editedEvent.Name,
                    StartDate: $scope.editedEvent.StartDate,
                    HomeTeamName: $scope.editedEvent.HomeTeamName,
                    AwayTeamName: $scope.editedEvent.AwayTeamName,
                    HomeTeamScore: $scope.editedEvent.HomeTeamScore,
                    AwayTeamScore: $scope.editedEvent.AwayTeamScore
                })
                .then(function (response) {
                    $scope.editedEvent = null;
                    $('#modalEventDetails').modal('hide');
                    $scope.loadEventList();
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


    $scope.editUserEvent = function (eventId) {
        $http.get("/api/ApiUserEvent/GetByEventId?eventId=" + eventId)
            .then(function (resultGetData) {
                $scope.editedUserEvent = resultGetData.data;
                $('#modalUserEventDetails').modal();
            });
    }

    $scope.cancelUserEventEdit = function () {
        $scope.editedUserEvent = null;
        $('#modalUserEventDetails').modal('hide');
    }

    $scope.saveUserEventDetails = function () {
        //angular.forEach($scope.formSaveUserEventDetails.$error, function (field) {
        //    angular.forEach(field, function (errorField) {
        //        errorField.$setTouched();
        //    })
        //}
        //);

        if ($scope.formSaveUserEventDetails.$valid) {
            $http.post("/api/ApiUserEvent/SaveUserEventDetails",
                {
                    Id: $scope.editedUserEvent.Id,
                    //Name: $scope.editedUserEvent.Name,
                    StartDate: $scope.editedUserEvent.StartDate,
                    HomeTeamName: $scope.editedUserEvent.HomeTeamName,
                    AwayTeamName: $scope.editedUserEvent.AwayTeamName,
                    HomeTeamScore: $scope.editedUserEvent.HomeTeamScore,
                    AwayTeamScore: $scope.editedUserEvent.AwayTeamScore,
                    EventId: $scope.editedUserEvent.EventId
                    //UserId: $scope.editedUserEvent.UserId
                })
                .then(function (response) {
                    $scope.editedUserEvent = null;
                    $('#modalUserEventDetails').modal('hide');
                    $scope.loadEventList();
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
                var dateFormat = attrs['format'];// 'yyyy-MM-dd';

                ctrl.$formatters.unshift(function (modelValue) {
                    return dateFilter(modelValue, dateFormat);
                });
            }
        };
    })
    ;






