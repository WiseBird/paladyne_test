angular.module('users').factory('users', ['$http', 'modules', 'errorHandler', 'authInfo', 'popup', function ($http, modules, errorHandler, authInfo, popup) {
    return {
        save: function (user) {
            return $http.put(modules.users.url + '/' + user.id, user).error(errorHandler).success(function () {
                if (user.id == authInfo.userId) {
                    modules.setModules(user.modules);
                }
                popup.success("User successfully updated");
            });
        }
    }
}]);