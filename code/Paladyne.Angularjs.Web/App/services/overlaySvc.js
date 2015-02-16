angular.module('main').factory('overlay', ['jQuery', function (jQuery) {
    var overlaySelector = "#loading-overlay";
    function showOverlay() {
        var overlay = jQuery(overlaySelector);
        var counter = overlay.data("counter");
        if (counter == null) {
            counter = 0;
        }

        counter++;
        overlay.data("counter", counter);
        overlay.show();
    }
    function hideOverlay() {
        var overlay = jQuery(overlaySelector);
        var counter = overlay.data("counter");
        if (counter) {
            counter--;
            overlay.data("counter", counter);
        }

        overlay.hide();
    }

    jQuery(document).ajaxSend(function () {
        showOverlay();
    });

    jQuery(document).ajaxComplete(function () {
        hideOverlay();
    });

    var service = {
        show: function () {
            showOverlay();
        },
        hide: function () {
            hideOverlay();
        },
        wrap: function (promise) {
            service.show();
            promise.finally(service.hide);
            return promise;
        }
    };

    return service;
}]);