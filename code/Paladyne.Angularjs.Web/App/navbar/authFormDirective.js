angular.module('main').directive('ptNavbarAuthForm', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/navbar/authForm.html',
        controller: 'navbarAuthFormCtrl',
        scope: true
    }
});