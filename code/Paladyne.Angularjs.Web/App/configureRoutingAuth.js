angular.module('main').run(['$rootScope', '$location', '$route', 'auth', 'modules', '$injector', function ($rootScope, $location, $route, auth, modules, $injector) {
    function onUnauthRoute() {
        $location.path("/");
    }

    var triedAuth = false;
    // Checks user rights for route
    $rootScope.$on("$routeChangeStart", function (event, next) {
        if (!next.$$route.canAccess) {
            return;
        }

        if (!$injector.invoke(next.$$route.canAccess)) {
            event.preventDefault();
            // We will suspend user on unaccessible page until auth is retrieved.
            if (triedAuth) {
                onUnauthRoute();
            }
        }
    });

    // Watching for current user rights, on change checks if user still has access to current page.
    $rootScope.$watch(function () {
        return modules.array.map(function (module) { return module.canSee; });
    }, function () {
        if (!$route.current || !$route.current.$$route.canAccess) {
            return;
        }

        if (!$injector.invoke($route.current.$$route.canAccess)) {
            onUnauthRoute();
        }
    }, true);

    auth.tryAuth().finally(function () {
        triedAuth = true;

        // Rerender current page with correct rights
        $route.reload();
    });
}]);