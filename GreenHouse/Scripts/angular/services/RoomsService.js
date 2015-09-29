(function () {
    'use strict';

    angular.module('GreenHouseApp').service("roomsService", roomsService);

    function roomsService($http) {
        // Return public API
        return ({
            getRooms: getRooms,
            addNewRoom: addNewRoom
        });

        function getRooms() {
            var request = $http({
                method: "GET",
                url: '/Daily/GetRooms'
            });

            return request;
        }

        function addNewRoom(newRoom) {
            var request = $http({
                method: "POST",
                url: '/Daily/AddNewRoom',
                data: newRoom
            });

            return request;
        }
    }

    roomsService.$inject = ["$http"];
})()