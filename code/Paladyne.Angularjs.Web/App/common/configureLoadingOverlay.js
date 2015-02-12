(function () {
    var overlaySelector = "#loading-overlay";
    function showOverlay(jQuery) {
        var overlay = jQuery(overlaySelector);
        var counter = overlay.data("counter");
        if (counter == null) {
            counter = 0;
        }

        counter++;
        overlay.data("counter", counter);
        overlay.show();
    }
    function hideOverlay(jQuery) {
        var overlay = jQuery(overlaySelector);
        var counter = overlay.data("counter");
        if (counter) {
            counter--;
            overlay.data("counter", counter);
        }

        overlay.hide();
    }

    angular.module('main').config(['$httpProvider', '$provide', function($httpProvider, $provide) {
            $provide.factory('loadingOverlayInterceptor', ['$q', 'jQuery', function($q, jQuery) {
                    return {
                        'request': function (config) {
                            showOverlay(jQuery);
                            return config;
                        },
                        'requestError': function (rejection) {
                            hideOverlay(jQuery);
                            return $q.reject(rejection);
                        },
                        'response': function (response) {
                            hideOverlay(jQuery);
                            return response;
                        },
                        'responseError': function (rejection) {
                            hideOverlay(jQuery);
                            return $q.reject(rejection);
                        }
                    };
                }
            ]);

            $httpProvider.interceptors.push('loadingOverlayInterceptor');
        }
    ]);

    angular.module('main').run(['jQuery', function(jQuery) {
            jQuery(document).ajaxSend(function() {
                showOverlay(jQuery);
            });

            jQuery(document).ajaxComplete(function() {
                hideOverlay(jQuery);
            });
        }
    ]);

})();