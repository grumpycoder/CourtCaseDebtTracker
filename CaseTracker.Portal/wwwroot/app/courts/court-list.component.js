//court-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal, $confirm) {
        var $ctrl = this;

        $ctrl.title = 'Court Manager';
        $ctrl.subTitle = 'Courts';
        $ctrl.isBusy = false;

        $ctrl.$onInit = function () {
            console.log('court list init');
            $ctrl.isBusy = true;
            $http.get('api/court/list').then(function (r) {
                $ctrl.courts = r.data;
            }).finally(function () {
                $ctrl.isBusy = false;
            });
        }

        $ctrl.openModal = function (court) {
            console.log('selected', court);
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
                console.log('updated', result);
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
                    console.log('delete court', court);
                }).catch(function (err) {
                    console.log('Oops. Something went wrong', err);
                });
            });

        }
    }

    module.component('courtList', {
        templateUrl: 'app/courts/court-list.component.html',
        controller: ['$http', '$uibModal', '$confirm', controller]
    });
})();