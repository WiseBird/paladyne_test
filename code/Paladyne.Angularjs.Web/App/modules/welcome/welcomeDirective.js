angular.module('welcome').directive('ptWelcome', function () {
    return {
        restrict: 'E',
        replace: false,
        templateUrl: '/App/modules/welcome/welcome.html',
        controller: 'welcomeCtrl',
        scope: true
    }
});