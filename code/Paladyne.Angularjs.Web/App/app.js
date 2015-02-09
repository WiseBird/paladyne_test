angular.module('main', ['welcome']);

angular.module('main').config(function ($httpProvider, $provide) {
    $provide.factory('addAuthTokenInterceptor', function (authInfo) {
        return {
            request: function (config) {
                if (config.method == "POST") {
                    config.data = config.data || {};
                    config.data.token = authInfo.token;
                }
                return config;
            }
        }
    });

    $httpProvider.interceptors.push('addAuthTokenInterceptor');
});

angular.module('main').value('toastr', toastr);