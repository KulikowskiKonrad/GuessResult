﻿var app = angular.module('GR', []);
app.controller('EventListCtrl', ["$scope", "$http", function ($scope, $http) {
    //$scope.loadList = function () {
    //    let eventId = ($scope.filteredEvent != null ? $scope.filteredEvent.Id : '');
    //    $http.get("/api/ApiEvent/GetAll")
    //        .then(function (res, status, xhr) {
    //            $scope.events = res.data;
    //        });
    //}
    //$scope.loadList();

    //$scope.$watchCollection('filteredEvent', function () {
    //    $scope.loadList();
    //});

    $scope.loadEventList = function () {
        $http.get("/api/ApiEvent/GetAll")
            .then(function (res, status, xhr) {
                $scope.events = res.data;
            });
    }
    $scope.loadEventList();

    //$scope.delete = function (eventId) {
    //    swal({
    //        title: 'Na pewno chcesz to usunąć?',
    //        type: 'warning',
    //        showCancelButton: true,
    //        confirmButtonColor: '#3085d6',
    //        cancelButtonColor: '#d33',
    //        confirmButtonText: 'Tak,chcę to usunąć!',
    //        cancelButtonText: 'Anuluj'
    //    }).then(function () {
    //        $http.delete("/api/ApiEvent/Delete/?id=" + eventId)
    //            .then(function (response) {
    //                $scope.loadList();
    //            })
    //            .catch(function (data, status) {
    //                swal({
    //                    title: data.data,
    //                    type: 'error',
    //                    confirmButtonColor: '#3085d6',
    //                    confirmButtonText: 'Ok!',
    //                });
    //            });
    //    })
    //}



    $scope.edit = function (eventId) {
        $scope.editedEvent = {};
        //$scope.selectedEvent = {};

        if (eventId != null) {
            for (let i = 0; i < $scope.singleEvent.length; i++) {
                if ($scope.events[i].Id == eventId) {
                    $scope.editedEvent = angular.copy($scope.events[i]);
                }
            }
        }
        $('#modalEventDetails').modal();
    }

    //$scope.cancelEdit = function () {
    //    $scope.editedEvent = null;
    //    $('#modalEventDetails').modal('hide');
    //}

    //$scope.saveDetails = function () {
    //    angular.forEach($scope.formSaveEventDetails.$error, function (field) {
    //        angular.forEach(field, function (errorField) {
    //            errorField.$setTouched();
    //        })
    //    });

    //    if ($scope.formSaveEventDetails.$valid) {
    //        $http.post("/api/ApiEvent/SaveEventDetails",
    //            {


    //                Id: $scope.editedEvent.Id,
    //                Name: $scope.editedEvent.Name,
    //                StartDate: $scope.editedEvent.StartDate,
    //                HomeTeamName: $scope.editedEvent.HomeTeamName,
    //                AwayTeamName: $scope.editedEvent.AwayTeamName
    //            })
    //            .then(function (response) {
    //                $scope.editEvent = null;
    //                $('#modalEventDetails').modal('hide');
    //                $scope.loadList();
    //            })
    //            .catch(function (data, status) {
    //                swal({
    //                    title: data.data.Message,
    //                    type: 'error',
    //                    confirmButtonColor: '#3085d6',
    //                    confirmButtonText: 'Ok!',
    //                });
    //            });
    //    }
    //}
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
    });