angular.module('users').controller('usersTableCtrl', ['$scope', 'modules', 'users', 'permissions', 'errorHandler', function($scope, modules, users, permissions, errorHandler) {
    $scope.modules = modules;
    $scope.module = modules.users;
    $scope.permissions = permissions.array;

    $scope.usersGridOptions = {
        columns: [
            { field: "firstName", title: "First name" },
            { field: "lastName", title: "Last name" },
            {
                field: "modules",
                title: "Modules",
                template:
                    "<span ng-repeat='module in dataItem.modules' ng-if='module.permission != permissions.prohibit'><span ng-if='$index != 0'>, </span>{{ modules[module.id].name }}</span>" +
                    ""
                /*template: function(user) {
                    return user.modules
                        .filter(function(x) { return x.permission != permissions.prohibit; })
                        .map(function(x) { return x.name; })
                        .join(", ");
                }*/
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
        users.save(user);
        $scope.grid.refresh();
    }
}]);