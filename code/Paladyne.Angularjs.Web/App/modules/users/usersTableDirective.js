angular.module('users').directive('ptUsersTable', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/modules/users/usersTable.html',
        controller: 'usersTableCtrl'
    }
});