angular.module('main').controller('navbarRegisterFormCtrl', ['$scope', 'authInfo', 'auth', function ($scope, authInfo, auth) {
    $scope.authInfo = authInfo;

    $scope.data = {
        userName: "",
        password: "",
        firstName: "",
        lastName: "",
    };

    $scope.submit = function () {
        auth.register($scope.data.userName, $scope.data.password, $scope.data.firstName, $scope.data.lastName);
    }
}]);