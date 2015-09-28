(function () {
    'use strict';

    angular.module('GreenHouseApp').service("roomsService", roomsService);

    function roomsService($http) {
        // Return public API
        return ({
            getRooms: getRooms
        });

        function getRooms() {
            var request = $http({
                method: "GET",
                url: '/Daily/GetRooms'
            });

            return request;
        }
    }

    roomsService.$inject = ["$http"];
})()