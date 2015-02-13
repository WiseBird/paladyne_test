angular.module('main', ['ngRoute', 'ngResource', 'oc.lazyLoad', 'kendo.directives']);

angular.module('main').value('toastr', toastr);
angular.module('main').value('kendo', kendo);
angular.module('main').value('jQuery', jQuery);

angular.module('main').config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.when('/', { templateUrl: "/App/pages/index.html" });
    $routeProvider.when('/management', {
        templateUrl: '/App/pages/management.html',
        canAccess: ['modules', function (modules) {
            return modules.hasAccessToManagement;
        }]
    });
    $routeProvider.otherwise({ redirectTo: '/' });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);