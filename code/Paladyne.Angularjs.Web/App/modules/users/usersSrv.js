angular.module('users').factory('users', ['$resource', function ($resource) {
    return $resource('/api/users/:id', { id: "@id" });
}]);