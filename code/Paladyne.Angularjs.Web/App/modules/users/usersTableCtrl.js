angular.module('users').controller('usersTableCtrl', ['$scope', 'modules', 'users', 'permissions', 'authInfo', 'errorHandler', function($scope, modules, users, permissions, authInfo, errorHandler) {
    $scope.authInfo = authInfo;
    $scope.modules = modules;
    $scope.module = modules.users;
    $scope.permissions = permissions;
    if (!$scope.module.canSee) {
        return;
    }

    $scope.usersGridOptions = {
        columns: [
            { field: "firstName", title: "First name" },
            { field: "lastName", title: "Last name" },
            {
                field: "modules",
                title: "Modules",
                template: "<span class='commas-list-item' ng-repeat='module in dataItem.modules' ng-if='module.permission != permissions.prohibit'>{{ modules[module.id].name }}</span>"
            }
        ],
        dataSource: {
            transport: {
                read: $scope.module.url
            },
            schema: {
                parse: function(userList) {
                    for (var i = 0; i < userList.length; i++) {
                        var user = userList[i];
                        user.forEdit = angular.copy(user);
                    }
                    return userList;
                }
            },
            error: errorHandler
        },
        detailExpand: function (e) {
            if ($scope.expandedRow && $scope.expandedRow[0] == e.masterRow[0]) {
                return;
            }

            collapseExpanded();
            $scope.expandedRow = e.masterRow;
        }
    };

    $scope.save = function (user) {var u = user;
        users.save(user.forEdit).success(function() {
            copyUserData(user.forEdit, user);

            $scope.grid.refresh();
        });
    }

    $scope.cancelEdit = function(user) {
        copyUserData(user, user.forEdit);
        collapseExpanded();
    }

    function copyUserData(from, to) {
        to.firstName = from.firstName;
        to.lastName = from.lastName;
        for (var i = 0; i < to.modules.length; i++) {
            to.modules[i].name = from.modules[i].name;
            to.modules[i].permission = from.modules[i].permission;
        }
    }

    function collapseExpanded() {
        if ($scope.expandedRow) {
            $scope.grid.collapseRow($scope.expandedRow);
        }
    }
}]);