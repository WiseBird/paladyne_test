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
            popup.success("User module successfully updated");
            var model = e.model;
            modules.setModuleName(model.id, model.name);
        }
    };

    $scope.$watch(function() {
        return modules.array.map(function(module) { return module.canSee; });
    }, function() {
        dataSource.read();
    }, true);
}]);