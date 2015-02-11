angular.module('main').controller('navbarRegisterFormCtrl', ['$scope', 'authInfo', 'account', function ($scope, authInfo, account) {
    $scope.authInfo = authInfo;

    $scope.data = {
        userName: "",
        password: "",
        firstName: "",
        lastName: "",
    };

    $scope.submit = function () {
        account.register($scope.data.userName, $scope.data.password, $scope.data.firstName, $scope.data.lastName);
    }
}]);