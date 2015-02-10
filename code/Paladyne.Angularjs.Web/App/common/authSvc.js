angular.module('main').factory('auth', ['$rootScope', '$http', 'popup', 'authInfo', function ($rootScope, $http, popup, authInfo) {
    return {
        login: function (username, password) {
            var loginData = {
                username: username,
                password: password
            };

            $http.post('/account/login', loginData).success(function (data) {
                authInfo.isAuthenticated = true;
                authInfo.userId = data.userId;
                authInfo.userName = username;
                authInfo.token = data.token;

                $rootScope.$broadcast("auth:logged_in", data);
            }).error(function () {
                popup.error("Invalid login or password");
            });
        },
        logout: function() {
            authInfo.isAuthenticated = false;
            authInfo.userId = null;
            authInfo.userName = null;
            authInfo.token = null;

            $rootScope.$broadcast("auth:logged_out", authInfo);
        }
    };
}]);