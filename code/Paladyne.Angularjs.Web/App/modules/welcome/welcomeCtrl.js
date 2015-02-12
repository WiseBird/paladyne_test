angular.module('welcome').controller('welcomeCtrl', ['$scope', 'welcomeData', 'modules', function ($scope, welcomeData, modules) {
    $scope.module = modules.welcome;
    if (!$scope.module.canSee) {
        return;
    }

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
}]);