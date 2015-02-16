angular.module('main').factory('permissions', function () {
    return {
        prohibit: "Prohibit",
        see: "See",
        edit: "Edit",
        array: ["Prohibit", "See", "Edit"]
    };
})