angular.module('main').controller('indexCtrl', ['$scope', 'authInfo', 'modules', function ($scope, authInfo, modules) {
    $scope.authInfo = authInfo;
    $scope.modules = modules;
}]);