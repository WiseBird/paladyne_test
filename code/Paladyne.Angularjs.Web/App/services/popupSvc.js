angular.module('main').factory('popup', ['toastr', function (toastr) {
    return {
        success: function (msg) {
            toastr.success(msg);
        },
        error: function (msg) {
            toastr.error(msg);
        }
    }
}])