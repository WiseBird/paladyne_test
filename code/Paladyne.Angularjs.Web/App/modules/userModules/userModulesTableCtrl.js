angular.module('userModules').controller('userModulesTableCtrl', ['$scope', 'modules', 'errorHandler', 'popup', 'kendo', function ($scope, modules, errorHandler, popup, kendo) {
    $scope.module = modules.userModules;
    if (!$scope.module.canSee) {
        return;
    }

    function getIdentityUrl(data) {
        return $scope.module.url + "/" + data.id;
    }

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: $scope.module.url,
            update: {
                url: getIdentityUrl,
                type: 'PUT',
                dataType: 'json',
                contentType: "application/json"
            },
            parameterMap: function(data) {
                return JSON.stringify(data);
            }
        },
        schema: {
            model: {
                id: "id",
                fields: {
                    id: { editable: false, nullable: false },
                    name: { validation: { required: true } },
                    granter: { editable: false }
                }
            }
        },
        sync: function (e) {
            modules.setModuleName($scope.savingModel.id, $scope.savingModel.name);
            popup.success("User module successfully updated");
            $scope.$apply();
        },
        error: errorHandler
    });

    $scope.userModulesGridOptions = {
        columns: [
            { field: "name", title: "Name" },
            { field: "granter", title: "Granter" },
            { command: ["edit"], title: "&nbsp;", width: "250px" }
        ],
        editable: "inline",
        dataSource: dataSource,
        save: function (e) {
            $scope.savingModel = e.model;
        }
    };

    $scope.$watch(function() {
        return modules.array.map(function(module) { return module.canSee; });
    }, function () {
        if ($scope.module.canSee) {
            dataSource.read();
        }
    }, true);
}]);