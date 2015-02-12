angular.module('main').directive('kGridHideDetails', ['jQuery', function (jQuery) {
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
                    hidden = !hidden;
                    if (hide) {
                        element.addClass("kendo-grid-hide-details");
                    } else {
                        element.removeClass("kendo-grid-hide-details");
                    }
                }
            }, true);
        });
    }
    return {
        restrict: 'A',
        link: link
    };
}]);