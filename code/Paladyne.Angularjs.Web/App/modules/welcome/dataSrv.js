angular.module('welcome').factory('welcomeData', ['$http', 'popup', function ($http, popup) {
    return {
        load: function () {
            return $http.get('/api/welcome').error(function () {
                console.log(arguments);
                popup.error("Error while retrieving welcome data");
            });
        }
    };
}]);