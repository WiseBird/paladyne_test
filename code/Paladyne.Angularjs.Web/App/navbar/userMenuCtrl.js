angular.module('main').controller('navbarUserMenuCtrl', function ($scope, authInfo, auth) {
    $scope.authInfo = authInfo;

    $scope.logout = function() {
        auth.logout();
    }
});