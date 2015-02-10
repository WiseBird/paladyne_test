angular.module('main', ['ngRoute', 'oc.lazyLoad']);

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

angular.module('main').config([
    '$routeProvider', function ($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: function () {
                var injector = angular.element('*[ng-app]').injector();
                var authInfo = injector.get('authInfo');
                var modules = injector.get('modules');

                if (authInfo.isAuthenticated) {
                    return modules.welcome.view;
                }

                return undefined;
            }, resolve: {
                load: ['authInfo', 'modules', function (authInfo, modules) {
                    if (!authInfo.isAuthenticated) {
                        return undefined;
                    }

                    return modules.welcome.load();
                }]
            }
        });
        $routeProvider.otherwise({ redirectTo: '/' });
    }
]);

angular.module('main').value('toastr', toastr);
angular.module('main').value('require', require);

angular.module('main').run(['$rootScope', '$location', '$route', 'authInfo', function ($rootScope, $location, $route, authInfo) {
    $rootScope.$on("$routeChangeStart", function (event, next) {
        if (!authInfo.isAuthenticated) {
            $location.path("/");
        }
    });

    $rootScope.$on("auth:logged_in", function () {
        $route.reload();
    });
}])