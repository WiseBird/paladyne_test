angular.module('users').controller('usersTableCtrl', ['$scope', 'modules', 'users', 'permissions', function ($scope, modules, users, permissions) {
    $scope.module = modules.users;
    $scope.permissions = permissions.array;
    
    $scope.usersGridOptions = {
        columns: [
            { field: "firstName", title: "First name" },
            { field: "lastName", title: "Last name" },
            { field: "modules", title: "Modules", 
                template: function(user) {
                    return user.modules
                        .filter(function (x) { return x.permission != permissions.prohibit; })
                        .map(function (x) { return x.name; })
                        .join(", ");
                }
            }
        ],
        dataSource: {
            transport: {
                read: $scope.module.url
            }
        }
    };

    $scope.save = function(user) {
        users.save(user);
        $scope.grid.refresh();
    }
}]);