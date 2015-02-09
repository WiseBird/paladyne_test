angular.module('main').directive('ptNavbarAuthForm', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/common/navbarAuthForm.html',
        controller: 'navbarAuthFormCtrl'
    }
});