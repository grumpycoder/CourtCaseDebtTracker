//case-detail.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal, uibDateParser, $confirm, ngToast) {
        var $ctrl = this;

        $ctrl.title = 'Case Manager';
        $ctrl.subTitle = 'Case';
        $ctrl.isBusy = false;

        $ctrl.$onInit = function () {
            console.log('case detail init');
            var id = location.pathname.split('/')[location.pathname.split('/').length - 1];
            $ctrl.isBusy = true;
            $http.get('/api/case/' + id).then(function (r) {
                $ctrl.case = r.data;
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
                        $ctrl.case.plaintiffs.unshift(r.data);
                        $ctrl.plaintiff = undefined;
                    });
            }
            if ($ctrl.defendant !== undefined) {
                $ctrl.defendant.caseId = $ctrl.case.id;
                $ctrl.defendant.type = 'defendant';

                console.log('add defendant', $ctrl.defendant);
                $http.post('/api/case/' + $ctrl.case.id + '/litigant', $ctrl.defendant)
                    .then(function (r) {
                        $ctrl.case.defendants.unshift(r.data);
                        $ctrl.defendant = undefined;
                    });
            }
        }

        $ctrl.deleteLitigant = function (item, type) {
            console.log('litigant', item);
            console.log('type', type);
            $http.delete('/api/case/' + $ctrl.case.id + '/litigant/' + item.id).then(function (r) {
                switch (type) {
                    case 'defendant':
                        var idx = $ctrl.case.defendants.indexOf(item);
                        $ctrl.case.defendants.splice(idx, 1);
                        break;
                    case 'plaintiff':
                        var idx = $ctrl.case.plaintiffs.indexOf(item);
                        $ctrl.case.plaintiffs.splice(idx, 1);
                        break;
                    default:
                        console.log('unknown litigant');
                }
            }).catch(function (err) {
                // toastr.error('Oops. Error deleting tax');
            }).finally(function () {
                // $ctrl.isBusy = false;
            });

        }

        $ctrl.delete = function () {
            $confirm({
                title: 'Delete',
                text: 'Are you sure you want to delete ' + $ctrl.case.caption + '?'
            }).then(function () {

                $http.delete('/api/case/' + $ctrl.case.id).then(function (r) {
                    var myToastMsg = ngToast.success({
                        content: 'Deleted case ' + $ctrl.case.caption,
                        dismissButton: true
                    });
                    window.location.href = '/';
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

    module.component('caseDetail', {
        templateUrl: '/app/cases/case-detail.component.html',
        controller: ['$http', '$uibModal', 'uibDateParser', '$confirm', 'ngToast', controller]
    });
})();