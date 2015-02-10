angular.module('main').controller('navbarUserMenuCtrl', ['$scope', 'authInfo', 'auth', function ($scope, authInfo, auth) {
    $scope.authInfo = authInfo;

    $scope.logout = function() {
        auth.logout();
    }
}]);