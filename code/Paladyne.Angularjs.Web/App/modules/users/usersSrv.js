angular.module('users').factory('users', ['$http', 'modules', 'errorHandler', 'authInfo', function ($http, modules, errorHandler, authInfo) {
    return {
        save: function (user) {
            $http.put(modules.users.url + '/' + user.id, user).error(errorHandler).success(function () {
                if (user.id == authInfo.userId) {
                    modules.setModules(user.modules);
                }
            });
        }
    }
}]);