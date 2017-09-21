//case-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal, ngToast) {
        var $ctrl = this;

        $ctrl.title = 'Case Manager';
        $ctrl.subTitle = 'Cases';
        $ctrl.isBusy = false;

        $ctrl.$onInit = function () {
            console.log('case list init');
            $ctrl.isBusy = true;
            $http.get('api/case/list').then(function (r) {
                $ctrl.cases = r.data;
                console.log('cases', $ctrl.cases);
            }).finally(function () {
                $ctrl.isBusy = false;
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
                var myToastMsg = ngToast.success({
                    content: 'Added case ' + result.caption,
                    dismissButton: true
                });
            }, function (reason) {});
        }

    }

    module.component('caseList', {
        templateUrl: 'app/cases/case-list.component.html',
        controller: ['$http', '$uibModal', 'ngToast', controller]
    });
})();