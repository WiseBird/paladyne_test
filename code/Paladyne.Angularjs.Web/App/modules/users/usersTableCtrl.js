angular.module('users').controller('usersTableCtrl', ['$scope', 'users', function ($scope, users) {
    $scope.users = [];
    users.query().$promise.then(function (data) {
        $scope.users = data;
    });
}]);