angular.module('main').config(['$httpProvider', '$provide', function ($httpProvider, $provide) {
    $provide.factory('addAuthTokenInterceptor', ['authInfo', function (authInfo) {
        return {
            request: function (config) {
                if (authInfo.token) {
                    config.headers = config.headers || {};
                    config.headers.Authorization = 'Bearer ' + authInfo.token;
                }
                return config;
            }
        }
    }]);

    $httpProvider.interceptors.push('addAuthTokenInterceptor');
}]);

angular.module('main').run(['authInfo', 'jQuery', function (authInfo, jQuery) {
    jQuery.ajaxSetup({
        beforeSend: function (req) {
            if (authInfo.token) {
                req.setRequestHeader('Authorization', 'Bearer ' + authInfo.token);
            }
        }
    });
}]);