angular.module('users').factory('users', ['$http', 'modules', 'errorHandler', function ($http, modules, errorHandler) {
    return {
        save: function (user) {
            var data = {
                firstName: user.firstName,
                lastName: user.LastName,
                modules: []
            }

            for (var i = 0; i < user.modules.length; i++) {
                data.modules.push({
                    id: user.modules[i].id,
                    name: user.modules[i].name,
                    permission: user.modules[i].permission
                });
            }

            $http.put(modules.users.url + '/' + user.id, user).success(function () {
            }).error(errorHandler);
        }
    }
}]);