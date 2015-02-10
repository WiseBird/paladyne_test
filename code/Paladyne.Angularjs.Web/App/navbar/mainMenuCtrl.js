angular.module('main').controller('navbarMainMenuCtrl', ['$scope', 'modules', function ($scope, modules) {
    $scope.modules = modules;
}]);