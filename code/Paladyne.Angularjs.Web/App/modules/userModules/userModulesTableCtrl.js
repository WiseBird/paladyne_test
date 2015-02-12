angular.module('userModules').controller('userModulesTableCtrl', ['$scope', 'modules', 'errorHandler', 'popup', function ($scope, modules, errorHandler, popup) {
    $scope.module = modules.userModules;

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
            popup.success("User module successfully updated");
            var model = e.model;
            modules.setModuleName(model.id, model.name);
        }
    };
}]);