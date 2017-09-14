//case-detail.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal) {
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

        $ctrl.openModal = function () {
            console.log('selected', $ctrl.case);
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
                console.log('updated', result);
                angular.extend($ctrl.case, result);
            }, function (reason) {});
        }

    }

    module.component('caseDetail', {
        templateUrl: '/app/cases/case-detail.component.html',
        controller: ['$http', '$uibModal', controller]
    });
})();