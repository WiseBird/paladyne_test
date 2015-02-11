angular.module('main').directive('ptNavbarRegisterForm', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/navbar/registerForm.html',
        controller: 'navbarRegisterFormCtrl',
        scope: true
    }
});