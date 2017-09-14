//case-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http) {
        var $ctrl = this;

        $ctrl.title = 'Case Manager';
        $ctrl.subTitle = 'Cases';

        $ctrl.$onInit = function () {
            console.log('case list init');
            $http.get('api/case/list').then(function (r) {
                console.log('r', r);
                $ctrl.cases = r.data;
            });
        }

        $ctrl.gotoDetails = function (id) {
            console.log('goto', id);
        }

    }

    module.component('caseList', {
        templateUrl: 'app/cases/case-list.component.html',
        controller: ['$http', controller]
    });
})();