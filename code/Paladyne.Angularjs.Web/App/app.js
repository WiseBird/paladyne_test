angular.module('main', ['ngRoute', 'ngResource', 'oc.lazyLoad', 'kendo.directives']);

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

angular.module('main').config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider.when('/', { templateUrl: "/App/pages/index.html" });
        $routeProvider.when('/management', { templateUrl: '/App/pages/management.html' });
        $routeProvider.otherwise({ redirectTo: '/' });

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    }
]);

angular.module('main').run(['$rootScope', '$location', '$route', 'authInfo', 'modules', 'jQuery', function ($rootScope, $location, $route, authInfo, modules, jQuery) {
    jQuery.ajaxSetup({
        beforeSend: function (req) {
            if (authInfo.token) {
                req.setRequestHeader('Authorization', 'Bearer ' + authInfo.token);
            }
        }
    });

    $rootScope.$on("$routeChangeStart", function (event, next) {
        var headintToTheRoot = next.$$route && (next.$$route.originalPath == "/");
        var headintToTheManagement = next.$$route && (next.$$route.originalPath == "/management");

        if (!authInfo.isAuthenticated && !headintToTheRoot) {
            event.preventDefault();
            $location.path("/");
        } else if (!modules.hasAccessToManagement && headintToTheManagement) {
            event.preventDefault();
            $location.path("/");
        }
    });

    $rootScope.$on("auth:logged_in", function () {
        $route.reload();
    });
    $rootScope.$on("auth:logged_out", function () {
        $route.reload();
    });
}]);

angular.module('main').value('toastr', toastr);
angular.module('main').value('kendo', kendo);
angular.module('main').value('jQuery', jQuery);