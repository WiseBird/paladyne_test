angular.module('welcome').directive('ptGreetings', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/welcome/greetings.html',
        controller: 'greetingsCtrl'
    }
});