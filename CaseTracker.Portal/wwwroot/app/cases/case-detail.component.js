//case-detail.component.js
(function () {
    var module = angular.module('app');

    function controller($http) {
        var $ctrl = this;

        $ctrl.title = 'Case Manager';
        $ctrl.subTitle = 'Case';

        $ctrl.$onInit = function () {
            console.log('case detail init');
            var id = location.pathname.split('/')[location.pathname.split('/').length - 1];
            console.log('id', id);
            $http.get('/api/case/' + id).then(function (r) {
                $ctrl.case = r.data;
                console.log('case', $ctrl.case);
            });
        }

    }

    module.component('caseDetail', {
        templateUrl: '/app/cases/case-detail.component.html',
        controller: ['$http', controller]
    });
})();