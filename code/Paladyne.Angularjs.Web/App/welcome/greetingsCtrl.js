angular.module('welcome').controller('greetingsCtrl', function ($scope, authInfo) {
    $scope.authInfo = authInfo;
});