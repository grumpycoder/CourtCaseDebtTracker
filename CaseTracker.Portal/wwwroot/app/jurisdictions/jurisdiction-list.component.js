//jurisdiction-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal, $confirm, ngToast) {
        var $ctrl = this;
        var pageSizeDefault = 10;

        $ctrl.title = 'Jurisdiction Manager';
        $ctrl.subTitle = 'Jurisdictions';
        $ctrl.isBusy = false;

        $ctrl.searchModel = {
            page: 1,
            pageSize: pageSizeDefault,
            orderBy: 'id',
            orderDirection: 'asc'
        };

        $ctrl.$onInit = function () {
            console.log('jurisdiction list init');
            $ctrl.isBusy = true;
        }

        $ctrl.search = function (tableState) {
            tableStateRef = tableState;
            $ctrl.isBusy = true;

            if (typeof (tableState.sort.predicate) !== "undefined") {
                $ctrl.searchModel.orderBy = tableState.sort.predicate;
                $ctrl.searchModel.orderDirection = tableState.sort.reverse ? 'desc' : 'asc';
            }

            console.log('search', $ctrl.searchModel);

            $http.get('api/jurisdiction/list', {
                params: $ctrl.searchModel
            }).then(function (r) {
                $ctrl.jurisdictions = r.data.results;
                $ctrl.searchModel = r.data;
                delete $ctrl.searchModel.results;
            }).finally(function () {
                $ctrl.isBusy = false;
            });
        }

        $ctrl.paged = function paged() {
            $ctrl.search(tableStateRef);
        };

        $ctrl.openModal = function (jurisdiction) {
            $modal.open({
                component: 'jurisdictionEdit',
                bindings: {
                    modalInstance: "<"
                },
                resolve: {
                    jurisdiction: jurisdiction
                },
                size: 'md'
            }).result.then(function (result) {
                if (jurisdiction === undefined) {
                    $ctrl.jurisdictions.unshift(result);
                } else {
                    angular.extend(jurisdiction, result);
                }
            }, function (reason) {});
        }

        $ctrl.delete = function (jurisdiction) {
            $confirm({
                title: 'Delete',
                text: 'Are you sure you want to delete ' + jurisdiction.name + '?'
            }).then(function () {
                $http.delete('/api/jurisdiction/' + jurisdiction.id).then(function (r) {
                    var idx = $ctrl.jurisdictions.indexOf(jurisdiction);
                    $ctrl.jurisdictions.splice(idx, 1);
                    var myToastMsg = ngToast.success({
                        content: 'Deleted jurisdiction ' + jurisdiction.name,
                        dismissButton: true
                    });
                }).catch(function (err) {
                    console.log('Oops. Something went wrong', err);
                    var myToastMsg = ngToast.danger({
                        content: err.data,
                        dismissButton: true
                    });
                });
            });
        }

    }

    module.component('jurisdictionList', {
        templateUrl: 'app/jurisdictions/jurisdiction-list.component.html',
        controller: ['$http', '$uibModal', '$confirm', 'ngToast', controller]
    });
})();