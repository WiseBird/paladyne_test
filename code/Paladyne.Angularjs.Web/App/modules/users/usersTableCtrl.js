angular.module('users').controller('usersTableCtrl', ['$scope', 'modules', 'users', 'permissions', 'authInfo', 'errorHandler', function($scope, modules, users, permissions, authInfo, errorHandler) {
    $scope.authInfo = authInfo;
    $scope.modules = modules;
    $scope.module = modules.users;
    $scope.permissions = permissions;
    if (!$scope.module.canSee && !$scope.module.canEdit) {
        return;
    }

    $scope.usersGridOptions = {
        columns: [
            { field: "firstName", title: "First name" },
            { field: "lastName", title: "Last name" },
            {
                field: "modules",
                title: "Modules",
                template: "<span ng-repeat='module in dataItem.modules' ng-if='module.permission != permissions.prohibit'><span ng-if='$index != 0'>, </span>{{ modules[module.id].name }}</span>"
            }
        ],
        dataSource: {
            transport: {
                read: $scope.module.url
            },
            error: errorHandler
        }
    };

    $scope.save = function(user) {
        users.save(user).success(function() {
            $scope.grid.refresh();
        });
    }
}]);