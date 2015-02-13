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

/// <reference path="../../../paladyne.angularjs.web/app/common/permissionssrv.js" />
/// <reference path="../../../paladyne.angularjs.web/app/common/modulessvc.js" />

/// <reference path="../../../paladyne.angularjs.web/app/pages/managementCtrl.js" />

describe('managementCtrl', function () {
    var $controller,
        $route,
        $scope,
        modules;

    beforeEach(function () {
        module('main');

        inject(function (_$controller_, _$route_, _$rootScope_, _modules_) {
            $controller = _$controller_;
            $route = _$route_;
            $scope = _$rootScope_.$new();
            modules = _modules_;
        });
    });

    it('should reload route when access to page is lost', function () {
        spyOn($route, 'reload');

        $controller('managementCtrl', { $scope: $scope, $route: $route });


        modules.hasAccessToManagement = false;
        $scope.$apply();


        expect($route.reload).toHaveBeenCalled();
    });
});
