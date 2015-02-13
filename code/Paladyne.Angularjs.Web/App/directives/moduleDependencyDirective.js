angular.module('main').directive('ptModuleDependency', ['$compile', function ($compile) {
    function link(scope, element, attrs) {
        var module = scope.$eval(attrs['ptModuleDependency']);
        if (module.loaded) {
            return;
        }

        var unwatchLoaded = scope.$watch(function() {
            return module.loaded;
        }, function(loaded) {
            if (!loaded) {
                return;
            }

            unwatchLoaded();
            $compile(element)(scope);
        });

        var unwatchCanSee = scope.$watch(function () {
            return module.canSee;
        }, function (canSee) {
            if (!canSee) {
                return;
            }

            unwatchCanSee();
            module.load();
        });
    }
    return {
        restrict: 'A',
        link: link
    };
}]);