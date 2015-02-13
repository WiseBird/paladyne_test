angular.module('main').directive('kGridHideColumn', ['jQuery', function (jQuery) {
    function link(scope, element, attrs) {
        var unwatchGrid = scope.$watch(function () {
            return jQuery(element).data("kendoGrid");
        }, function (grid) {
            if (!grid) {
                return;
            }

            unwatchGrid();
            watchForSettings(grid);
        });

        function watchForSettings(grid) {
            var hidden = false;

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
        }
    }
    return {
        restrict: 'A',
        link: link
    };
}]);