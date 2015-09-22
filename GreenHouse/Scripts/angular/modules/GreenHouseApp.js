angular.module('GreenHouseApp', []);

angular.module('GreenHouseApp', []).filter('roomsFilter', function () {
    return function (rooms, wifi) {
        var filteredRooms = [];

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

        return filteredRooms;
    };
});