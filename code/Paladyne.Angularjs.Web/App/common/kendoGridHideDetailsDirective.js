angular.module('main').directive('kGridHideDetails', function (jQuery) {
    function link(scope, element, attrs) {
        var grid = null;
        var hidden = false;

        var unwatch = scope.$watch(function () {
            return jQuery(element).data("kendoGrid");
        }, function (val) {
            if (!val) {
                return;
            }

            unwatch();
            scope.$watch(attrs['kGridHideDetails'], function (hide) {
                if (hidden != hide) {
                    if (hide) {
                        element.addClass("kendo-grid-hide-details");
                    } else {
                        element.removeClass("kendo-grid-hide-details");
                    }
                    hidden = !hidden;
                }
            }, true);
        });
    }
    return {
        link: link
    };
});