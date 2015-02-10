angular.module('main').directive('ptIndexSomeLogo', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/index/someLogo.html',
        controller: 'indexSomeLogoCtrl'
    }
});