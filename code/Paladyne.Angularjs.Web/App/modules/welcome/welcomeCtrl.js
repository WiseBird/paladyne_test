angular.module('welcome').controller('welcomeCtrl', function ($scope, authInfo) {
    $scope.authInfo = authInfo;
});