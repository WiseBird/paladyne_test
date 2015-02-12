/// <reference path="../../../paladyne.angularjs.web/scripts/jquery-1.10.2.js" />
/// <reference path="../../../paladyne.angularjs.web/scripts/toastr.js" />
/// <reference path="../../../paladyne.angularjs.web/scripts/angular.js" />
/// <reference path="../../../paladyne.angularjs.web/scripts/angular-resource.js" />
/// <reference path="../../../paladyne.angularjs.web/scripts/angular-route.js" />
/// <reference path="../../../paladyne.angularjs.web/scripts/oclazyload.js" />
/// <reference path="../../../paladyne.angularjs.web/scripts/angular-mocks.js" />

/// <reference path="../../../paladyne.angularjs.web/scripts/kendo/kendo.all.min.js" />
/// <reference path="../../../paladyne.angularjs.web/scripts/kendo/kendo.angular.min.js" />

/// <reference path="../../../paladyne.angularjs.web/app/app.js" />

/// <reference path="../../../paladyne.angularjs.web/app/pages/managementCtrl.js" />

describe('managementCtrl', function () {
    var $controller,
        $route;

    beforeEach(function () {
        module('main');

        inject(function (_$controller_, _$route_) {
            $controller = _$controller_;
            $route = _$route_;
        });
    });

    it('should reload route when access to page is lost', function () {
        var $scope = {
            $watch: function(f1, f2) {
                f2(false);
            }
        };

        spyOn($route, 'reload');

        $controller('managementCtrl', { $scope: $scope, $route: $route, modules: {} });

        expect($route.reload).toHaveBeenCalled();
    });
});
