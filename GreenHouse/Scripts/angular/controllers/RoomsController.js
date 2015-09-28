(function () {
    'use strict';

    angular.module("GreenHouseApp").controller("RoomsController", roomsCtrl);

    function roomsCtrl($scope, $filter, roomsService) {
        $scope.top = 50;
        $scope.left = 530;
        $scope.top1 = 50;
        $scope.left1 = 330;

        roomsService.getRooms().then(function (response) {
            $scope.rooms = response.data;
            $scope.filteredRooms = $scope.rooms;
        });

        $scope.mapCapacity = function (sliderValue) {
            switch (sliderValue) {
                case 0:
                    return 0;
                case 1:
                    return 30;
                case 2:
                    return 70;
                case 3:
                    return 100;
                case 4:
                    return 200;
            }
        }

        $scope.checkRoom = function (room, constraints) {
            if (room.places >= constraints.capacity)
                return false;

            if (constraints.wifi && !room.wifi)
                return false;

            if (constraints.projector && !room.projector)
                return false;

            if (constraints.monitor && !room.monitor)
                return false;

            if (constraints.microphone && !room.microphone)
                return false;

            return true;
        }

        $scope.updateRooms = function (rooms) {
            var filteredRooms = [];

            var constraints = {
                capacity : $scope.mapCapacity( parseInt($('#capacity').val())),
                wifi : $('#wi-fi').is(':checked'),
                projector : $('#proj').is(':checked'),
                monitor : $('#mon').is(':checked'),
                microphone : $('#mic').is(':checked')
            }

            angular.forEach(rooms, function (room) {
                if ($scope.checkRoom(room, constraints)) {
                    filteredRooms.push(room);
                }
            })
            $scope.filteredRooms = filteredRooms;
        };

        $scope.sliderMoveLeft = function () {
            var currentValue = parseInt($('#capacity').val());
            if (currentValue > 0) {
                $('#capacity').val(currentValue - 1);
            }
            $scope.updateRooms($scope.rooms);
        };

        $scope.sliderMoveRight = function () {
            var currentValue = parseInt($('#capacity').val());
            if (currentValue < 4) {
                $('#capacity').val(currentValue + 1);
            }
            $scope.updateRooms($scope.rooms);
        };

        $scope.selectedRoom = null;

        $scope.selectRoom = function (roomNumber) {
            var contains = false;
            angular.forEach($scope.filteredRooms, function(room) {
                if (room.number == roomNumber) {
                    contains = true;
                }
            })
            if (contains === true) {
                $scope.selectedRoom = $filter('filter')($scope.rooms, { number: roomNumber })[0];
            }
            else {
                return;
            }
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

        $scope.selectedNewRoom = null;

        $scope.addNewRoom = function (roomNumber) {
            var contains = false;
            angular.forEach($scope.filteredRooms, function (room) {
                if (room.number == roomNumber) {
                    contains = true;
                }
            })
            if (contains === false) {
                $scope.selectedNewRoom = roomNumber;
                $('#newRoomInfo').removeClass('hidden');
                var roomId = "#Room" + roomNumber;
                var newRoomId = "newRoom" + roomNumber;
                var roomClass = "add" + roomNumber;
                var curroom = $(roomId);
                curroom.attr('id', newRoomId);
                curroom.addClass(roomClass);
            }
            else {
                return;
            }
        }

    }

    roomsCtrl.$inject= ['$scope', '$filter', 'roomsService'];
})()