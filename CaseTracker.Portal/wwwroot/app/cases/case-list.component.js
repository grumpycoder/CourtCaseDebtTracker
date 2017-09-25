//case-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal, ngToast) {
        var $ctrl = this;
        var pageSizeDefault = 10;

        $ctrl.title = 'Case Manager';
        $ctrl.subTitle = 'Cases';
        $ctrl.isBusy = false;

        $ctrl.searchModel = {
            page: 1,
            pageSize: pageSizeDefault,
            orderBy: 'id',
            orderDirection: 'desc'
        };

        $ctrl.$onInit = function () {
            console.log('case list init');
            $ctrl.isBusy = true;
        }

        $ctrl.search = function (tableState) {
            tableStateRef = tableState;
            $ctrl.isBusy = true;

            if (typeof (tableState.sort.predicate) !== "undefined") {
                $ctrl.searchModel.orderBy = tableState.sort.predicate;
                $ctrl.searchModel.orderDirection = tableState.sort.reverse ? 'desc' : 'asc';
            }

            $http.get('api/case/list', {
                params: $ctrl.searchModel
            }).then(function (r) {
                $ctrl.cases = r.data.results;
                $ctrl.searchModel = r.data;
                delete $ctrl.searchModel.results;
            }).finally(function () {
                $ctrl.isBusy = false;
            });
        }

        $ctrl.paged = function paged() {
            $ctrl.search(tableStateRef);
        };

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