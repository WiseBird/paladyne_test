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
        $routeProvider.when('/', {
            templateUrl: function () {
                var injector = angular.element('*[ng-app]').injector();
                var authInfo = injector.get('authInfo');
                var modules = injector.get('modules');

                if (authInfo.isAuthenticated) {
                    return modules.welcome.view;
                }

                return "/App/index/someLogo.html";
            }, resolve: {
                load: ['authInfo', 'modules', function (authInfo, modules) {
                    if (!authInfo.isAuthenticated) {
                        return undefined;
                    }

                    return modules.welcome.load();
                }]
            }
        });
        $routeProvider.when('/management', {
            templateUrl: '/App/index/management.html',
            resolve: {
                load: ['authInfo', 'modules', '$q', function (authInfo, modules, $q) {
                    if (!authInfo.isAuthenticated) {
                        return undefined;
                    }

                    var promises = [];
                    if (modules.users.canSee) {
                        promises.push(modules.users.load());
                    }
                    if (modules.userModules.canSee) {
                        promises.push(modules.userModules.load());
                    }

                    return $q.all(promises);
                }]
            }
        });
        $routeProvider.otherwise({ redirectTo: '/' });

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    }
]);

angular.module('main').run(['$rootScope', '$location', '$route', 'authInfo', function ($rootScope, $location, $route, authInfo) {
    $rootScope.$on("$routeChangeStart", function (event, next) {
        var headintToTheRoot = next.$$route && (next.$$route.originalPath == "/");
        if (!authInfo.isAuthenticated && !headintToTheRoot) {
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