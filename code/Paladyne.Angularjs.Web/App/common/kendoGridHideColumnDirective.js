angular.module('main').directive('kGridHideColumn', function (jQuery) {
    function link(scope, element, attrs) {
        var grid = null;
        var hidden = false;

        var unwatch = scope.$watch(function() {
            return jQuery(element).data("kendoGrid");
        }, function(val) {
            if (!val) {
                return;
            }

            grid = val;
            unwatch();

            scope.$watch(attrs['kGridHideColumn'], function (options) {
                if (hidden != options.hide) {
                    if (options.hide) {
                        grid.hideColumn(options.column);
                    } else {
                        grid.showColumn(options.column);
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