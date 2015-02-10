angular.module('users').controller('usersTableCtrl', ['$scope', 'users', 'kendo', function ($scope, users, kendo) {
    $scope.usersGridOptions = {
        columns: [
            { field: "firstName", title: "First name" },
            { field: "lastName", title: "Last name" },
            {
                field: "modules", title: "Modules",
                template: "#=moduleList#"
            }
        ]
    };

    users.query().$promise.then(function (data) {
        for (var i = 0; i < data.length; i++) {
            var user = data[i];
            user.moduleList = user.modules
                .filter(function(x) { return x.permission != "Prohibit"; })
                .map(function (x) { return x.name; })
                .join(", ");
        }

        $scope.array = new kendo.data.ObservableArray(data);
        $scope.usersGridOptions.dataSource = new kendo.data.DataSource({
            data: $scope.array
        });
    });

    //$scope.array[0].set("firstName", "1TB");
}]);