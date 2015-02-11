angular.module('main').directive('ptNavbarMainMenu', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/navbar/mainMenu.html',
        controller: 'navbarMainMenuCtrl',
        scope: true
    }
});