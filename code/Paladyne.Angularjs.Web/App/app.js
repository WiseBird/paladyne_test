angular.module('main', ['ngRoute', 'ngResource', 'oc.lazyLoad', 'kendo.directives']);

angular.module('main').value('toastr', toastr);
angular.module('main').value('kendo', kendo);
angular.module('main').value('jQuery', jQuery);