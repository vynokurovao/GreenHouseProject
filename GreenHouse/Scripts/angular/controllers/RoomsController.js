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

        $scope.updateRooms = function (rooms) {
            var filteredRooms = [];
            var wifi = $('#wi-fi').is(':checked');
            var projector = $('#proj').is(':checked');
            var monitor = $('#mon').is(':checked');
            var microphone = $('#mic').is(':checked');

            if (wifi) {
                angular.forEach(rooms, function (room) {
                    if (room.wifi) {
                        filteredRooms.push(room);
                    }
                });
            }
            else {
                angular.forEach(rooms, function (room) {
                    filteredRooms.push(room);
                })
            }

            var nfilteredRooms = [];
            if (projector) {
                angular.forEach(filteredRooms, function (room) {
                    if (room.projector) {
                        nfilteredRooms.push(room);
                    }
                });
            }
            else {
                angular.forEach(filteredRooms, function (room) {
                    nfilteredRooms.push(room);
                })
            }

            var nnfilteredRooms = [];
            if (monitor) {
                angular.forEach(nfilteredRooms, function (room) {
                    if (room.monitor) {
                        nnfilteredRooms.push(room);
                    }
                })
            }
            else {
                angular.forEach(nfilteredRooms, function (room) {
                    nnfilteredRooms.push(room);
                })
            }

            var nnnfilteredRooms = [];
            if (microphone) {
                angular.forEach(nnfilteredRooms, function (room) {
                    if (room.microphone) {
                        nnnfilteredRooms.push(room);
                    }
                })
            }
            else {
                angular.forEach(nnfilteredRooms, function (room) {
                    nnnfilteredRooms.push(room);
                })
            }

            $scope.filteredRooms = nnnfilteredRooms;
        };

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