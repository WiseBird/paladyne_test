angular.module('main').factory('account', ['$rootScope', '$http', 'popup', 'auth', 'errorHandler', function ($rootScope, $http, popup, auth, errorHandler) {
    return {
        register: function (userName, password, firstName, lastName) {
            var registerData = {
                userName: userName,
                password: password,
                firstName: firstName,
                lastName: lastName
            };

            $http.post('/account/register', registerData).success(function () {
                auth.login(userName, password);
            }).error(errorHandler);
        }
    };
}]);