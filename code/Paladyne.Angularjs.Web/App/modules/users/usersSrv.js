angular.module('users').factory('users', ['$http', 'modules', 'errorHandler', function ($http, modules, errorHandler) {
    return {
        save: function (user) {
            $http.put(modules.users.url + '/' + user.id, user).error(errorHandler).success(function() {
                modules.setModules(user.modules);
            });
        }
    }
}]);