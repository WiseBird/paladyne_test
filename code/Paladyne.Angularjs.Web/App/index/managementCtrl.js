angular.module('main').controller('indexManagementCtrl', ['$scope', '$route', '$compile', 'modules', function ($scope, $route, $compile, modules) {
    $scope.$on("modules:changed", function() {
        if (!modules.hasAccessToManagement) {
            $route.reload();
        } else if (modules.userModules.canSee && !modules.userModules.loaded) {
            modules.userModules.load().then(function() {
                var userModulesView = angular.element(document.querySelector('pt-user-modules-table'));
                $compile(userModulesView)($scope);
            });
        }
    });
}]);