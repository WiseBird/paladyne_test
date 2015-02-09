angular.module('main').factory('modules', function ($q, $ocLazyLoad, $rootScope, authInfo) {
    function module(id, files) {
        this.id = id;
        this.name = id;

        this.view = 'App/modules/' + id + '/' + id + '.html';

        this.files = files.slice();
        this.files.unshift('/App/modules/' + id + '/' + id + '.js');

        this.loaded = false;
        this.canSee = false;
        this.canEdit = false;
    }
    module.prototype.load = function() {
        if (this.loaded) {
            var defer = $q.defer();
            defer.resolve();
            return defer;
        }

        var self = this;
        return $ocLazyLoad.load({
            serie: true,
            name: this.id,
            files: this.files
        }).then(function() {
            self.loaded = true;
        });
    }

    $rootScope.$on("auth:logged_in", function (event, data) {
        for (var i = 0; i < data.modules.length; i++) {
            var mdl = data.modules[i];

            if (!modules[mdl.id]) {
                continue;
            }

            modules[mdl.id].name = mdl.name;
            modules[mdl.id].canSee = mdl.permission != "Prohibit";
            modules[mdl.id].canEdit = mdl.permission == "Edit";
        }

        $rootScope.$broadcast("modules:changed");
    });

    var modules = {
        welcome: new module('welcome', [
            '/App/modules/welcome/welcome.js',
            '/App/modules/welcome/welcomeCtrl.js'
        ]),
        userMng: new module('userMng', []),
        moduleList: new module('moduleList', []),


        hasAccessToManagement: function() {
            if (!authInfo.isAuthenticated) {
                return false;
            }

            var userMngModule = authInfo.modules["userMng"];
            if (userMngModule && userMngModule.permission != "Prohibit") {
                return true;
            }

            var moduleListModule = authInfo.modules["moduleList"];
            if (moduleListModule && moduleListModule.permission != "Prohibit") {
                return true;
            }

            return false;
        }
    };
    return modules;
})