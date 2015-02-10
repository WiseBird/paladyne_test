angular.module('main').controller('navbarAuthFormCtrl', ['$scope', 'authInfo', 'auth', function ($scope, authInfo, auth) {
    $scope.authInfo = authInfo;

    $scope.data = {
        username: "",
        password: ""
    };

    $scope.submit = function () {
        auth.login($scope.data.username, $scope.data.password);
    }
}]);