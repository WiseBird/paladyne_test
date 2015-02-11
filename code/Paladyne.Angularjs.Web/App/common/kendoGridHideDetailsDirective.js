angular.module('main').directive('kGridHideDetails', function () {
    function link(scope, element, attrs) {
        var hide = scope.$eval(attrs['kGridHideDetails']);
        if (!hide) {
            return;
        }

        element.addClass('kendo-grid-hide-details');
    }

    return {
        link: link
    };
});