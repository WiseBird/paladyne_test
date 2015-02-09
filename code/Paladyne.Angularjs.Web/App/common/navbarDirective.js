angular.module('main').directive('ptNavbar', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/common/navbar.html',
        controller: 'navbarCtrl'
    }
});