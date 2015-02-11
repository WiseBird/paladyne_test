angular.module('main').factory('modules', ['$q', '$ocLazyLoad', '$rootScope', 'permissions', function ($q, $ocLazyLoad, $rootScope, permissions) {
    function Module(id, files) {
        this.id = id;
        this.name = id;

        this.url = '/api/' + id;
        this.view = 'App/modules/' + id + '/' + id + '.html';

        this.files = files.slice();
        this.files.unshift('/App/modules/' + id + '/' + id + '.js');

        this.loaded = false;
        this.canSee = false;
        this.canEdit = false;
    }
    Module.prototype.load = function () {
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

    var modules = {
        welcome: new Module('welcome', [
            '/App/modules/welcome/welcome.js',
            '/App/modules/welcome/dataSrv.js',
            '/App/modules/welcome/welcomeCtrl.js'
        ]),
        users: new Module('users', [
            '/App/modules/users/usersSrv.js',
            '/App/modules/users/usersTableCtrl.js',
            '/App/modules/users/usersTableDirective.js'
        ]),
        userModules: new Module('userModules', [
            '/App/modules/userModules/userModulesTableCtrl.js',
            '/App/modules/userModules/userModulesTableDirective.js'
        ]),

        hasAccessToManagement: false
    };

    function updateModulesAcessProperties() {
        modules.hasAccessToManagement = modules.users.canSee || modules.userModules.canSee;
    }
    updateModulesAcessProperties();

    $rootScope.$on("auth:logged_in", function (event, data) {
        for (var i = 0; i < data.modules.length; i++) {
            var mdl = data.modules[i];

            if (!modules[mdl.id]) {
                continue;
            }

            modules[mdl.id].name = mdl.name;
            modules[mdl.id].canSee = mdl.permission != permissions.prohibit;
            modules[mdl.id].canEdit = mdl.permission == permissions.edit;
        }

        updateModulesAcessProperties();
        //$rootScope.$broadcast("modules:changed");
    });
    $rootScope.$on("auth:logged_out", function () {
        var keys = Object.keys(modules);
        for (var i = 0; i < keys.length; i++) {
            var key = keys[i];

            if (!(modules[key] instanceof Module)) {
                continue;
            }

            var module = modules[key];
            module.name = module.id;
            module.canSee = false;
            module.canEdit = false;
        }

        updateModulesAcessProperties();
        //$rootScope.$broadcast("modules:changed");
    });

    return modules;
}])