angular.module('main').factory('modules', function ($q, $ocLazyLoad, authInfo) {
    function module(id, files) {
        this.id = id;

        this.view = 'App/modules/' + id + '/' + id + '.html';

        this.files = files.slice();
        this.files.unshift('/App/modules/' + id + '/' + id + '.js');

        this.loaded = false;
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

    return {
        welcome: new module('welcome', [
                '/App/modules/welcome/welcome.js',
                '/App/modules/welcome/welcomeCtrl.js'
        ]),
        hasAccessToManagement: function () {
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
    }
})