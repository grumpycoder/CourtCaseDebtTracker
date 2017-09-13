//jurisdiction-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http) {
        var $ctrl = this;

        $ctrl.title = 'Jurisdiction Manager';
        $ctrl.subTitle = 'Jurisdictions';

        $ctrl.$onInit = function () {
            console.log('jurisdiction list init');
            $http.get('api/jurisdiction/list').then(function (r) {
                console.log('r', r);
                $ctrl.jurisdictions = r.data;
            });
        }

    }

    module.component('jurisdictionList', {
        templateUrl: 'app/jurisdictions/jurisdiction-list.component.html',
        controller: ['$http', controller]
    });
})();