angular.module('userModules').controller('userModulesTableCtrl', ['$scope', 'userModules', 'kendo', function ($scope, userModules, kendo) {
    $scope.userModulesGridOptions = {
        columns: [
            { field: "name", title: "Name" },
            { field: "granter", title: "Granter" }
        ]
    };

    userModules.query().$promise.then(function (data) {
        $scope.array = new kendo.data.ObservableArray(data);
        $scope.userModulesGridOptions.dataSource = new kendo.data.DataSource({
            data: $scope.array
        });
    });
}]);