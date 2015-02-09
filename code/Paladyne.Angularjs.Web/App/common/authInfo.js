angular.module('main').factory('authInfo', function () {
    return {
        isAuthenticated: false,
        userId: null,
        token: ""
    };
});