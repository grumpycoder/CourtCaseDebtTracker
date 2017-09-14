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

        $ctrl.addLitigant = function (type) {
            if ($ctrl.plaintiff !== undefined) {
                console.log('add plaintiff', $ctrl.plaintiff.name);
                $ctrl.plaintiff.caseId = $ctrl.case.id;
                $ctrl.plaintiff.type = 'plaintiff';

                $http.post('/api/case/' + $ctrl.case.id + '/litigant', $ctrl.plaintiff)
                    .then(function (r) {
                        $ctrl.case.plaintiffs.unshift($ctrl.plaintiff);
                        $ctrl.plaintiff = undefined;
                    });
            }
            if ($ctrl.defendant !== undefined) {
                $ctrl.defendant.caseId = $ctrl.case.id;
                $ctrl.defendant.type = 'defendant';

                console.log('add defendant', $ctrl.defendant);
                $http.post('/api/case/' + $ctrl.case.id + '/litigant', $ctrl.defendant)
                    .then(function (r) {
                        $ctrl.case.defendants.unshift($ctrl.defendant);
                        $ctrl.defendant = undefined;
                    });
            }
        }


    }

    module.component('caseDetail', {
        templateUrl: '/app/cases/case-detail.component.html',
        controller: ['$http', '$uibModal', controller]
    });
})();