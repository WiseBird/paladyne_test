angular.module('main').factory('auth', function ($http, popup, authInfo) {
    return {
        authenticate: function (username, password) {
            var loginData = {
                username: username,
                password: password
            };

            $http.post('/account/login', loginData).success(function (data) {
                authInfo.isAuthenticated = true;
                authInfo.userId = data.userId;
                authInfo.token = data.token;
            }).error(function () {
                popup.error("Invalid login or password");
            });
        }
    };
});