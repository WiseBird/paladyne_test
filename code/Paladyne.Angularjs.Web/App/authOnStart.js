angular.module('main').run(['auth', function (auth) {
    auth.tryAuth();
}]);