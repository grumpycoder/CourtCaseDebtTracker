//jurisdiction-edit.component.js
(function () {
    var module = angular.module('app');

    function controller($http) {
        var $ctrl = this;

        $ctrl.title = 'Update Jurisdiction';

        $ctrl.$onInit = function () {
            console.log('jurisdiction edit init', $ctrl);
            if ($ctrl.resolve) {
                $ctrl.jurisdiction = angular.copy($ctrl.resolve.jurisdiction);
            }
            if ($ctrl.jurisdiction === undefined) {
                $ctrl.title = 'New Jurisdiction'
            }
        }

        $ctrl.cancel = function () {
            $ctrl.dismiss();
        }

        $ctrl.save = function () {
            console.log('save', $ctrl.jurisdiction);
            if ($ctrl.jurisdiction.id !== undefined) {
                $http.put('api/jurisdiction', $ctrl.jurisdiction).then(function (r) {
                    $ctrl.modalInstance.close($ctrl.jurisdiction);
                });
            } else {
                $http.post('api/jurisdiction', $ctrl.jurisdiction).then(function (r) {
                    $ctrl.modalInstance.close($ctrl.jurisdiction);
                });
            }
        }


    }

    module.component('jurisdictionEdit', {
        templateUrl: 'app/jurisdictions/jurisdiction-edit.component.html',
        controller: ['$http', controller],
        bindings: {
            jurisdiction: '<',
            resolve: '<',
            close: '&',
            dismiss: '&',
            modalInstance: '<'
        }
    });
})();