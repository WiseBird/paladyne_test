/// <reference path="../../paladyne.angularjs.web/scripts/jquery-1.10.2.js" />
/// <reference path="../../paladyne.angularjs.web/scripts/toastr.js" />
/// <reference path="../../paladyne.angularjs.web/scripts/angular.js" />
/// <reference path="../../paladyne.angularjs.web/scripts/angular-resource.js" />
/// <reference path="../../paladyne.angularjs.web/scripts/angular-route.js" />
/// <reference path="../../paladyne.angularjs.web/scripts/oclazyload.js" />
/// <reference path="../../paladyne.angularjs.web/scripts/angular-mocks.js" />

/// <reference path="../../paladyne.angularjs.web/scripts/kendo/kendo.all.min.js" />
/// <reference path="../../paladyne.angularjs.web/scripts/kendo/kendo.angular.min.js" />

/// <reference path="../../paladyne.angularjs.web/app/app.js" />
/// <reference path="../../paladyne.angularjs.web/app/configureroutingauth.js" />

/// <reference path="../../paladyne.angularjs.web/app/services/permissionssrv.js" />
/// <reference path="../../paladyne.angularjs.web/app/services/modulessvc.js" />
/// <reference path="../../paladyne.angularjs.web/app/services/authinfo.js" />

describe('routingAuthSpec', function () {
    var privateRouteCanAccessFunc = null;

    var publicPath = "/public";
    var privatePath = "/private";
    beforeEach(function () {
        privateRouteCanAccessFunc = jasmine.createSpy('dummy').and.returnValue(false);

        module('ngRoute', function ($routeProvider) {
            $routeProvider.when("/", {});
            $routeProvider.when(publicPath, {});
            $routeProvider.when(privatePath, {
                canAccess: privateRouteCanAccessFunc
            });
        });
    });

    beforeEach(function () {
        module('main');

        module(function ($provide) {
            var $q = window.angular.injector(['ng']).get('$q');
            $provide.value('auth', {
                tryAuth: function () {
                    return $q.defer().promise;
                }
            });
        });
    });

    it('should call canAccess', inject(function ($rootScope, $route, $location) {
        $location.path(privatePath);


        $rootScope.$apply();


        expect(privateRouteCanAccessFunc).toHaveBeenCalled();
    }));
});
