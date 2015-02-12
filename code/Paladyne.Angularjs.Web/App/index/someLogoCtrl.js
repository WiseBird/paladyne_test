angular.module('main').controller('someLogoCtrl', ['$scope', 'authInfo', function ($scope, authInfo) {
    $scope.authInfo = authInfo;
}]);