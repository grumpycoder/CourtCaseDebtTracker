//case-edit.component.js
(function () {
    var module = angular.module('app');

    function controller($http) {
        var $ctrl = this;

        $ctrl.title = 'Case Manager';
        $ctrl.subTitle = 'Case';

        $ctrl.$onInit = function () {
            console.log('case edit init', $ctrl);
            if ($ctrl.resolve) {
                $ctrl.case = angular.copy($ctrl.resolve.case);
            }
        }

        $ctrl.cancel = function () {
            $ctrl.dismiss();
        }

        $ctrl.save = function () {
            console.log('save', $ctrl.case);
            $http.put('/api/case', $ctrl.case).then(function (r) {
                $ctrl.modalInstance.close($ctrl.case);
            });
        }


    }

    module.component('caseEdit', {
        templateUrl: '/app/cases/case-edit.component.html',
        controller: ['$http', controller],
        bindings: {
            case: '<',
            resolve: '<',
            close: '&',
            dismiss: '&',
            modalInstance: '<'
        }
    });
})();