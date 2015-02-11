angular.module('users').factory('users', ['$http', 'modules', 'popup', function ($http, modules, popup) {
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
            }).error(function (errors) {
                console.log(arguments);
                if (Array.isArray(errors)) {
                    for (var i = 0; i < errors.length; i++) {
                        var error = errors[i];
                        popup.error(error);
                    }
                } else {
                    popup.error("Error during user save");
                }
            });
        }
    }
}]);