angular.module('main').directive('kGridHideColumn', ['jQuery', function (jQuery) {
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
                    hidden = !hidden;
                    if (options.hide) {
                        grid.hideColumn(options.column);
                    } else {
                        grid.showColumn(options.column);
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