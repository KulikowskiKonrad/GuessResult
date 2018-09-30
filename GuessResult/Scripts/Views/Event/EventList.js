var app = angular.module('GR', ['datatables']);
app.controller('EventListCtrl', ["$scope", "$http", function ($scope, $http) {

    $scope.eventStatusList = [
        {
            Id: 2,
            Name: 'Przyszły'
        },
        {
            Id: 1,
            Name: 'Zakończony'
        },
    ];

    $scope.eventPredictionTypeList = [
        {
            Id: 1,
            Name: 'Dokładny wynik'
        },
        {
            Id: 2,
            Name: 'Ogólny wynik'
        },
    ];

    $scope.generalScoreTypeList = [
        {
            Id: 1,
            Name: 'Zwycięstwo gospodarzy'
        },
        {
            Id: 2,
            Name: 'Zwycięstwo gości'
        },
        {
            Id: 3,
            Name: 'Remis'
        },
    ];



    $scope.loadEventList = function () {
        let filterEventStatus = ($scope.filterEventStatus != null ? $scope.filterEventStatus.Id : '')
        let filterOnlyMyEvents = ($scope.filterOnlyMyEvents != null ? $scope.filterOnlyMyEvents : false);
        $http.get("/api/ApiEvent/GetAll?filterEventStatus=" + filterEventStatus + "&filterOnlyMyEvents=" + filterOnlyMyEvents)
            .then(function (resultGetData) {
                $scope.events = resultGetData.data;
            }, function () {
                swal({
                    title: 'Wystapił błąd!',
                    type: 'error',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok!',
                });
            });
    }
    $scope.loadEventList();
    $scope.$watchCollection('filterEventStatus', function () {
        $scope.loadEventList();
    });

    $scope.$watchCollection('filterOnlyMyEvents', function () {
        $scope.loadEventList();
    });

    $scope.editUserEvent = function (eventId) {
        $http.get("/api/ApiUserEvent/GetByEventId?eventId=" + eventId)
            .then(function (resultGetData) {
                $scope.editedUserEvent = resultGetData.data;
                for (let i = 0; i < $scope.eventPredictionTypeList.length; i++) {
                    if ($scope.eventPredictionTypeList[i].Id == $scope.editedUserEvent.EventPredictionType) {
                        $scope.editedUserEvent.EventPredictionType = $scope.eventPredictionTypeList[i];
                    }
                }

                if ($scope.editedUserEvent.GeneralScoreType != null) {
                    for (let i = 0; i < $scope.generalScoreTypeList.length; i++) {
                        if ($scope.generalScoreTypeList[i].Id == $scope.editedUserEvent.GeneralScoreType) {
                            $scope.editedUserEvent.GeneralScoreType = $scope.generalScoreTypeList[i];
                        }
                    }
                } else {
                    $scope.editedUserEvent.GeneralScoreType = $scope.generalScoreTypeList[0];
                }
                $('#modalUserEventDetails').modal();
            }, function () {
                swal({
                    title: 'Wystapił błąd!',
                    type: 'error',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok!',
                });
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
                    EventId: $scope.editedUserEvent.EventId,
                    EventPredictionType: $scope.editedUserEvent.EventPredictionType.Id,
                    GeneralScoreType: $scope.editedUserEvent.GeneralScoreType.Id,
                    //UserId: $scope.editedUserEvent.UserId
                })
                .then(function (response) {
                    $scope.editedUserEvent = null;
                    $('#modalUserEventDetails').modal('hide');
                    $scope.loadEventList();
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


function calculateDivEventListHeight() {
    $('#divEventList').css('max-height', $(window).height() - $('#divEventList').offset().top - ($('footer').height() + 30));
}

$(document).ready(function () {
    calculateDivEventListHeight();
    $(window).resize(function () {
        calculateDivEventListHeight();
    });
});




