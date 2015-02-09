angular.module('welcome').controller('welcomeCtrl', function ($scope, welcomeData) {
    $scope.data = {
        ready: false,
        userName: null,
        lastLogin: null
    };
    welcomeData.load().success(function(data) {
        $scope.data.userName = data.userName;
        $scope.data.lastLogin = data.lastLogin;
        $scope.data.ready = true;
    });
});