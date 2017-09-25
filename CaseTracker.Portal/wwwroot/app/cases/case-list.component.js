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
            orderDirection: 'asc'
        };

        $ctrl.$onInit = function () {
            console.log('case list init');
            $ctrl.isBusy = true;
            $http.get('api/case/list').then(function (r) {
                $ctrl.cases = r.data;
            }).finally(function () {
                $ctrl.isBusy = false;
            });
        }

        $ctrl.search = function (tableState) {
            tableStateRef = tableState;
            $ctrl.isBusy = true;

            if (typeof (tableState.sort.predicate) !== "undefined") {
                $ctrl.searchModel.orderBy = tableState.sort.predicate;
                $ctrl.searchModel.orderDirection = tableState.sort.reverse ? 'desc' : 'asc';
            }

            console.log('search', $ctrl.searchModel);

            $http.get('api/case/list', {
                params: $ctrl.searchModel
            }).then(function (r) {
                $ctrl.cases = r.data.results;
                $ctrl.searchModel = r.data;
                delete $ctrl.searchModel.results;
                console.log('cases', $ctrl.cases);
                console.log('search', $ctrl.searchModel);
            }).finally(function () {
                $ctrl.isBusy = false;
            });

            // if ($ctrl.searchModel.suppress === null) $ctrl.searchModel.suppress = false;
            // return $http.get('api/mailer', { params: $ctrl.searchModel })
            //     .then(function (r) {
            //         $ctrl.mailers = r.data.results;
            //         $ctrl.searchModel = r.data;
            //         delete $ctrl.searchModel.results;
            //     }).catch(function (err) {
            //         console.log('Oops', err.data.message);
            //         toastr.error('Oops ' + err.data.message);
            //     }).finally(function () {
            //         $ctrl.isBusy = false;
            //     });
        }

        $ctrl.resetSearch = function () {
            $ctrl.searchModel = {
                page: 1,
                pageSize: pageSizeDefault,
                orderBy: 'id',
                orderDirection: 'asc'
            };
            $ctrl.search(tableStateRef);
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