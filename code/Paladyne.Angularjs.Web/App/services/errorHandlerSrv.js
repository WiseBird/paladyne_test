angular.module('main').factory('errorHandler', ['popup', function (popup) {
    function onErrors(errors) {
        for (var i = 0; i < errors.length; i++) {
            var error = errors[i];
            popup.error(error);
        }
    }

    return function() {
        if (!arguments.length) {
            popup.error("Server error");
            return;
        }

        if (Array.isArray(arguments[0])) {
            onErrors(arguments[0]);
            return;
        }

        if (arguments[0] && arguments[0].xhr) {
            if (arguments[0].errorThrown == "Unauthorized") {
                popup.error("Access denied");
                return;
            }
        }

        if (arguments[1] && arguments[1] == 401) {
            popup.error("Access denied");
            return;
        }

        if (typeof arguments[0] === 'string') {
            popup.error(arguments[0]);
            return;
        }

        popup.error("Server error");
        return;
    }
}])