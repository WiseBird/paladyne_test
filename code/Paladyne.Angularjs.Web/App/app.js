angular.module('main', ['ngRoute', 'welcome']);

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

angular.module('main').config([
    '$routeProvider', function($routeProvider) {
        $routeProvider.when('/', { templateUrl: '/App/index/index.html', controller: 'indexCtrl' });
        $routeProvider.when('/Welcome', { templateUrl: '/App/welcome/welcome.html', controller: 'welcomeCtrl' });
        $routeProvider.otherwise({ redirectTo: '/' });
    }
]);

angular.module('main').value('toastr', toastr);

angular.module('main').run(function ($rootScope, $location, $route, authInfo) {
    $rootScope.$on("$routeChangeStart", function(event, next) {
        if (!authInfo.isAuthenticated) {
            $location.path("/");
        } else {
            if (next.$$route.originalPath == "/") {
                $location.path("/Welcome");
            }
        }
    });

    $rootScope.$on("auth:logged_in", function () {
        $route.reload();
    });
})