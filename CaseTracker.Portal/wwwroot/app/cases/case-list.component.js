//case-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal) {
        var $ctrl = this;

        $ctrl.title = 'Case Manager';
        $ctrl.subTitle = 'Cases';

        $ctrl.$onInit = function () {
            console.log('case list init');
            $http.get('api/case/list').then(function (r) {
                $ctrl.cases = r.data;
            });
        }

        $ctrl.openModal = function () {
            $modal.open({
                component: 'caseEdit',
                bindings: {
                    modalInstance: "<"
                },
                resolve: {
                    case: $ctrl.case
                },
                size: 'md'
            }).result.then(function (result) {
                console.log('result', result);
                $ctrl.cases.unshift(result);
            }, function (reason) {});
        }

    }

    module.component('caseList', {
        templateUrl: 'app/cases/case-list.component.html',
        controller: ['$http', '$uibModal', controller]
    });
})();