angular.module('main').controller('indexSomeLogoCtrl', ['$scope', 'authInfo', function ($scope, authInfo) {
    $scope.authInfo = authInfo;
}]);