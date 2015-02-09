angular.module('main').factory('authInfo', function () {
    return {
        isAuthenticated: false,
        token: "",
        userId: null,
        userName: null
    };
});