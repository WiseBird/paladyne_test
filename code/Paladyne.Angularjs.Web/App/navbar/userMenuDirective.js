angular.module('main').directive('ptNavbarUserMenu', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/navbar/userMenu.html',
        controller: 'navbarUserMenuCtrl'
    }
});