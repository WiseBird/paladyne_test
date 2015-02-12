angular.module('main').controller('managementCtrl', ['$scope', '$route', 'modules', function ($scope, $route, modules) {
    $scope.modules = modules;
    $scope.$watch(function() {
        return modules.hasAccessToManagement;
    }, function(hasAccessToManagement) {
        if (!hasAccessToManagement) {
            $route.reload();
        }
    });
}]);