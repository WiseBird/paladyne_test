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
/// <reference path="../../paladyne.angularjs.web/app/configureauthtokenheader.js" />

/// <reference path="../../paladyne.angularjs.web/app/services/authinfo.js" />

describe('authTokenHeaderSpec', function () {
    beforeEach(function () {
        module('main');
    });

    it('should include auth token', inject(function ($httpBackend, $http, authInfo) {
        authInfo.token = "AAA";
        $httpBackend.expectGET("/", function (headers) {
            return headers['Authorization'] == 'Bearer AAA';
        }).respond(201, '');


        $http.get("/");
        $httpBackend.flush();


        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    }));
});
