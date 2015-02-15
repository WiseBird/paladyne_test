angular.module('main').factory('auth', ['$rootScope', '$http', 'authInfo', 'errorHandler', function ($rootScope, $http, authInfo, errorHandler) {
    function authOk(data) {
        authInfo.isAuthenticated = true;
        authInfo.userId = data.userId;
        authInfo.userName = data.userName;
        authInfo.token = data.token;

        $rootScope.$broadcast("auth:logged_in", data);
    }

    var service = {
        login: function (username, password) {
            var loginData = {
                username: username,
                password: password
            };

            $http.post('/account/login', loginData).success(function (data) {
                authOk(data);
            }).error(errorHandler);
        },
        tryAuth: function () {
            return $http.post('/account/token').success(function (data) {
                authOk(data);
            });
        },
        logout: function () {
            $http.post('/account/logout');

            authInfo.isAuthenticated = false;
            authInfo.userId = null;
            authInfo.userName = null;
            authInfo.token = null;

            $rootScope.$broadcast("auth:logged_out", authInfo);
        },
        register: function (userName, password, firstName, lastName) {
            var registerData = {
                userName: userName,
                password: password,
                firstName: firstName,
                lastName: lastName
            };

            $http.post('/account/register', registerData).success(function () {
                service.login(userName, password);
            }).error(errorHandler);
        }
    };

    return service;
}]);