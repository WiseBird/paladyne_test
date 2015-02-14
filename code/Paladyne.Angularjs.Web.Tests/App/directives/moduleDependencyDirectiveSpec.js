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

/// <reference path="../../../paladyne.angularjs.web/app/directives/moduledependencydirective.js" />

describe('moduleDependencyDirectiveSpec', function () {
    var provide;

    beforeEach(function () {
        module('main');

        module(function ($provide) {
            provide = $provide;
        });
    });

    //https://github.com/ocombe/ocLazyLoad/issues/111#issuecomment-69868719
    it('tests', inject(function ($compile, $rootScope) {
        var mockCompile = jasmine.createSpy('$compile').and.callFake($compile);
        provide.constant('$compile', mockCompile);

        var $scope = $rootScope.$new();
        $scope.module = {
            canSee: false,
            loaded: false,
            load: jasmine.createSpy('load')
        }



        //should load module on rights change
        mockCompile("<div pt-module-dependency='module'></div>")($scope);


        $scope.module.canSee = true;
        $scope.$digest();


        expect($scope.module.load).toHaveBeenCalled();



        //should recompile on module load
        mockCompile.calls.reset();


        $scope.module.loaded = true;
        $scope.$digest();


        expect(mockCompile).toHaveBeenCalled();
    }));
});
