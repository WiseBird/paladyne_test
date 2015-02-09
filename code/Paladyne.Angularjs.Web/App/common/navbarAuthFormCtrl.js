angular.module('main').controller('navbarAuthFormCtrl', function ($scope, authInfo, auth) {
    $scope.authInfo = authInfo;

    $scope.data = {
        username: "",
        password: ""
    };

    $scope.submit = function () {
        auth.authenticate($scope.data.username, $scope.data.password);
    }
});