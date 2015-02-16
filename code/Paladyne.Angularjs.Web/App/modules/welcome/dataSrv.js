angular.module('welcome').factory('welcomeData', ['$http', 'errorHandler', 'overlay', function ($http, errorHandler, overlay) {
    return {
        load: function () {
            return overlay.wrap($http.get('/api/welcome').error(errorHandler));
        }
    };
}]);