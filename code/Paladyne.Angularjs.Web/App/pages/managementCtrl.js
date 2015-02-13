angular.module('main').controller('managementCtrl', ['$scope', '$route', 'modules', function ($scope, $route, modules) {
    $scope.modules = modules;
}]);