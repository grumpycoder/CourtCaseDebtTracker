//jurisdiction-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal) {
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

        $ctrl.selectJurisidiction = function (j) {
            console.log('select', j);
            $ctrl.selectedJurisidiction = j;
        }

        $ctrl.openModal = function () {
            console.log('selected', $ctrl.selectedJurisidiction);
            $modal.open({
                component: 'jurisdictionEdit',
                bindings: {
                    modalInstance: "<"
                },
                resolve: {
                    jurisdiction: $ctrl.selectedJurisidiction
                },
                size: 'md'
            }).result.then(function (result) {
                console.log('updated', result);
                angular.extend($ctrl.selectedJurisidiction, result);
            }, function (reason) {});
        }
    }

    module.component('jurisdictionList', {
        templateUrl: 'app/jurisdictions/jurisdiction-list.component.html',
        controller: ['$http', '$uibModal', controller]
    });
})();