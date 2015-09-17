(function () {
    'use strict';

    angular.module("GreenHouseApp").controller("RoomsController", roomsCtrl);

    function roomsCtrl($scope, $filter, roomsService) {
        $scope.top = 50;
        $scope.left = 530;

        roomsService.getRooms().then(function (response) {
            $scope.rooms = response.data;
        });

        $scope.selectedRoom = null;

        $scope.selectRoom = function (roomNumber) {
            $scope.selectedRoom = $filter('filter')($scope.rooms, { number: roomNumber })[0];
        }

        $scope.getClass = function (room) {
            if (room.places < 30)
            {
                return "room" + room.number + "_30";
            }
            else if (room.places < 70)
            {
                return "room" + room.number + "_70";
            }
            else if (room.places < 100)
            {
                return "room" + room.number + "_100";
            }
            else
            {
                return "room" + room.number + "_200";
            }
        }
    }
})()