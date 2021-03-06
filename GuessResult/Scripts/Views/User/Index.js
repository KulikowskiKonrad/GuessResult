﻿var app = angular.module('calendarDemoApp', ['ui.calendar', 'ui.bootstrap']);

app.controller('UserIndexCtrl', ["$scope", "$http", "$compile", "uiCalendarConfig", function ($scope, $http, $compile, uiCalendarConfig) {
    $scope.events = [];

    $scope.loadCalendarItems = function () {
        let roomId = ($scope.filteredRoom != null ? $scope.filteredRoom.Id : '');
        $http.get("/api/ApiReservation/GetCalendarItems?roomId=" + roomId)
            .then(function (res, status, xhr) {
                $scope.events.splice(0);
                for (let i = 0; i < res.data.length; i++) {
                    $scope.events.push({
                        title: res.data[i].Title,
                        start: moment(res.data[i].Date).toDate()
                    });
                }
            });
    }
    $scope.loadCalendarItems();

    $scope.alertOnEventClick = function (date, jsEvent, view) {
        location.href = '/Reservation/ReservationList';
    };

    $scope.uiConfig = {
        calendar: {
            height: 450,
            editable: true,
            header: {
                left: 'title',
                center: '',
                right: 'today prev,next'
            },
            eventClick: $scope.alertOnEventClick,
            eventRender: $scope.eventRender,
            locale: 'pl',
            allDayDefault: true
        }
    };
    $scope.eventSources = [$scope.events];

    $scope.$watchCollection('filteredRoom', function () {
        $scope.loadCalendarItems();
    });

    $scope.loadRoomList = function () {
        $http.get("/api/ApiRoom/GetAll")
            .then(function (res, status, xhr) {
                $scope.rooms = res.data;
            });
    }
    $scope.loadRoomList();
}]);
