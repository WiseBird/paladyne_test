angular.module('main').controller('indexManagementCtrl', ['$scope', '$route', 'modules', function ($scope, $route, modules) {
    $scope.$on("modules:changed", function() {
        if (!modules.hasAccessToManagement) {
            $route.reload();
        }
    });
}]);