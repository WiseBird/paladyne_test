angular.module('main').config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.when('/', { templateUrl: "/App/pages/index.html" });
    $routeProvider.when('/management', { templateUrl: '/App/pages/management.html' });
    $routeProvider.otherwise({ redirectTo: '/' });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);

angular.module('main').run(['$rootScope', '$location', '$route', 'authInfo', 'modules', function ($rootScope, $location, $route, authInfo, modules) {
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