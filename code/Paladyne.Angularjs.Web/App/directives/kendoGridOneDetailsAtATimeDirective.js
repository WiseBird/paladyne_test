angular.module('main').directive('kGridOneDetailsAtATime', ['jQuery', function (jQuery) {
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
            var expandedRow = null;

            function isRowExpanded(row) {
                if (expandedRow && expandedRow[0] == row[0]) {
                    return true;
                }
                return false;
            }

            grid.collapseDetails = function () {
                if (expandedRow) {
                    this.collapseRow(expandedRow);
                }
            }

            grid.bind("detailExpand", function (e) {
                if (isRowExpanded(e.masterRow)) {
                    return;
                }

                this.collapseDetails();

                var selectedRow = this.select();
                if (selectedRow && selectedRow[0] != e.masterRow[0]) {
                    this.select(e.masterRow);
                }

                expandedRow = e.masterRow;
            });
            grid.bind("detailCollapse", function () {
                expandedRow = null;
            });

            grid.bind("change", function () {
                var row = this.select();
                if (isRowExpanded(row)) {
                    this.collapseRow(row);
                } else {
                    this.expandRow(row);
                }
            });
        }
    }
    return {
        restrict: 'A',
        link: link
    };
}]);