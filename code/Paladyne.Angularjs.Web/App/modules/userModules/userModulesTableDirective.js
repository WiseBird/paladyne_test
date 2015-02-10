angular.module('userModules').directive('ptUserModulesTable', function () {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App/modules/userModules/userModulesTable.html',
        controller: 'userModulesTableCtrl'
    }
});