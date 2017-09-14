//court-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http, $modal) {
        var $ctrl = this;

        $ctrl.title = 'Court Manager';
        $ctrl.subTitle = 'Courts';

        $ctrl.$onInit = function () {
            console.log('court list init');
            $http.get('api/court/list').then(function (r) {
                console.log('r', r);
                $ctrl.courts = r.data;
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
                angular.extend(court, result);
            }, function (reason) {});
        }
    }

    module.component('courtList', {
        templateUrl: 'app/courts/court-list.component.html',
        controller: ['$http', '$uibModal', controller]
    });
})();