angular.module('welcome').factory('welcomeData', function ($http, popup) {
    return {
        load: function () {
            return $http.get('/api/welcome').error(function () {
                console.log(arguments);
                popup.error("Error while retrieving welcome data");
            });
        }
    };
});