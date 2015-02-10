angular.module('main').directive('ptNavbar', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/navbar/navbar.html',
        controller: 'navbarCtrl'
    }
});