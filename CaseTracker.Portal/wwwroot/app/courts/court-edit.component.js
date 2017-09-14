//court-edit.component.js
(function () {
    var module = angular.module('app');

    function controller($http) {
        var $ctrl = this;

        $ctrl.title = 'Court Manager';
        $ctrl.subTitle = 'Court';

        $ctrl.$onInit = function () {
            console.log('court edit init', $ctrl);
            $http.get('api/jurisdiction/list').then(function (r) {
                $ctrl.jurisdictions = r.data;
                console.log('jurisdictions', $ctrl.jurisdictions);
            });

            if ($ctrl.resolve) {
                $ctrl.court = angular.copy($ctrl.resolve.court);
            }
        }

        $ctrl.cancel = function () {
            $ctrl.dismiss();
        }

        $ctrl.save = function () {
            console.log('save', $ctrl.court);
            if ($ctrl.court.id !== undefined) {
                console.log('updating court', $ctrl.court);
                $http.put('api/court', $ctrl.court).then(function (r) {
                    $ctrl.modalInstance.close($ctrl.court);
                });
            } else {
                console.log('adding new court', $ctrl.court);
                $http.post('api/court', $ctrl.court).then(function (r) {
                    $ctrl.modalInstance.close($ctrl.court);
                });
            }
        }

    }

    module.component('courtEdit', {
        templateUrl: '/app/courts/court-edit.component.html',
        controller: ['$http', controller],
        bindings: {
            court: '<',
            resolve: '<',
            close: '&',
            dismiss: '&',
            modalInstance: '<'
        }
    });
})();