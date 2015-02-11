angular.module('userModules').controller('userModulesTableCtrl', ['$scope', 'modules', 'errorHandler', function ($scope, modules, errorHandler) {
    $scope.module = modules.userModules;
    if (!$scope.module.canSee && !$scope.module.canEdit) {
        return;
    }

    function getIdentityUrl(data) {
        return $scope.module.url + "/" + data.id;
    }

    $scope.userModulesGridOptions = {
        columns: [
            { field: "name", title: "Name" },
            { field: "granter", title: "Granter" },
            { command: ["edit"], title: "&nbsp;", width: "250px" }
        ],
        editable: "inline",
        dataSource: {
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
        },
        save: function (e) {
            var model = e.model;
            modules.setModuleName(model.id, model.name);
        }
    };
}]);