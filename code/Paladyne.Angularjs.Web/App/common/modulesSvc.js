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

    var service = {
        welcome: new Module('welcome', [
            '/App/modules/welcome/welcome.js',
            '/App/modules/welcome/dataSrv.js',
            '/App/modules/welcome/welcomeCtrl.js',
            '/App/modules/welcome/welcomeDirective.js'
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

        hasAccessToManagement: false,
        setModuleName: function(id, name) {
            if (!service[id]) {
                return;
            }

            service[id].name = name;
        },
        setModules: function(mdls) {
            for (var i = 0; i < mdls.length; i++) {
                var mdl = mdls[i];

                if (!service[mdl.id]) {
                    continue;
                }

                service[mdl.id].name = mdl.name;
                service[mdl.id].canSee = mdl.permission != permissions.prohibit;
                service[mdl.id].canEdit = mdl.permission == permissions.edit;
            }

            updateModulesAcessProperties();
        }
    };
    var modules = [service.welcome, service.users, service.userModules];

    function updateModulesAcessProperties() {
        service.hasAccessToManagement = service.users.canSee || service.userModules.canSee;
    }
    updateModulesAcessProperties();

    $rootScope.$on("auth:logged_in", function (event, data) {
        service.setModules(data.modules);
    });
    $rootScope.$on("auth:logged_out", function () {
        for (var i = 0; i < modules.length; i++) {
            var module = modules[i];

            module.name = module.id;
            module.canSee = false;
            module.canEdit = false;
        }

        updateModulesAcessProperties();
    });

    return service;
}])