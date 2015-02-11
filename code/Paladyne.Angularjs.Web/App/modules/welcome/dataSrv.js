angular.module('welcome').factory('welcomeData', ['$http', 'errorHandler', function ($http, errorHandler) {
    return {
        load: function () {
            return $http.get('/api/welcome').error(errorHandler);
        }
    };
}]);