//court-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal, $confirm, ngToast) {
        var $ctrl = this;
        var pageSizeDefault = 10;

        $ctrl.title = 'Court Manager';
        $ctrl.subTitle = 'Courts';
        $ctrl.isBusy = false;

        $ctrl.searchModel = {
            page: 1,
            pageSize: pageSizeDefault,
            orderBy: 'id',
            orderDirection: 'asc'
        };

        $ctrl.$onInit = function () {
            console.log('court list init');
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

            $http.get('api/court/list', {
                params: $ctrl.searchModel
            }).then(function (r) {
                $ctrl.courts = r.data.results;
                $ctrl.searchModel = r.data;
                delete $ctrl.searchModel.results;
                console.log('count', $ctrl.courts);
            }).finally(function () {
                $ctrl.isBusy = false;
            });
        }

        $ctrl.paged = function paged() {
            $ctrl.search(tableStateRef);
        };

        $ctrl.openModal = function (court) {
            $modal.open({
                component: 'courtEdit',
                bindings: {
                    modalInstance: "<"
                },
                resolve: {
                    court: court
                },
                size: 'md'
            }).result.then(function (result) {
                if (court !== undefined) {
                    angular.extend(court, result);
                } else {
                    $ctrl.courts.unshift(result);
                }
            }, function (reason) {});
        }

        $ctrl.delete = function (court) {
            $confirm({
                title: 'Delete',
                text: 'Are you sure you want to delete ' + court.name + '?'
            }).then(function () {
                $http.delete('/api/court/' + court.id).then(function (r) {
                    var idx = $ctrl.courts.indexOf(court);
                    $ctrl.courts.splice(idx, 1);
                    var myToastMsg = ngToast.success({
                        content: 'Deleted court ' + court.name,
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

    module.component('courtList', {
        templateUrl: 'app/courts/court-list.component.html',
        controller: ['$http', '$uibModal', '$confirm', 'ngToast', controller]
    });
})();