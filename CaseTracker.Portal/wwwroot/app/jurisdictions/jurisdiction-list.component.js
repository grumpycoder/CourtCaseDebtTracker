//jurisdiction-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal, $confirm, ngToast) {
        var $ctrl = this;

        $ctrl.title = 'Jurisdiction Manager';
        $ctrl.subTitle = 'Jurisdictions';
        $ctrl.isBusy = false;

        $ctrl.$onInit = function () {
            console.log('jurisdiction list init');
            $ctrl.isBusy = true;
            $http.get('api/jurisdiction/list').then(function (r) {
                console.log('r', r);
                $ctrl.jurisdictions = r.data;
            }).finally(function () {
                $ctrl.isBusy = false;
            });
        }

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