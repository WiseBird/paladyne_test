angular.module('main').controller('navbarMainMenuCtrl', function ($rootScope, $scope, authInfo, modules) {
    $scope.data = {};
    $rootScope.$on("modules:changed", function () {
        updateScope();
    });

    updateScope();

    function updateScope() {
        $scope.data.canAccessManagement = modules.userMng.canSee || modules.moduleList.canSee;
    }
});