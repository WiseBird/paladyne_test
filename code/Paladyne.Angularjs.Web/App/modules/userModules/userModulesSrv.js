angular.module('userModules').factory('userModules', ['$resource', function ($resource) {
    return $resource('/api/userModules/:id', { id: "@id" });
}]);