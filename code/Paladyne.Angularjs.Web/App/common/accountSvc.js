angular.module('main').factory('account', ['$rootScope', '$http', 'popup', 'auth', function ($rootScope, $http, popup, auth) {
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
            }).error(function (errors) {
                console.log(arguments);
                if (Array.isArray(errors)) {
                    for (var i = 0; i < errors.length; i++) {
                        var error = errors[i];
                        popup.error(error);
                    }
                } else {
                    popup.error("Error during user register");
                }
            });
        }
    };
}]);