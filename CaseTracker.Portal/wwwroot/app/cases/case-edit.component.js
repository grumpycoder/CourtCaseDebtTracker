//case-edit.component.js
(function () {
    var module = angular.module('app');

    function controller($http, uibDateParser) {
        var $ctrl = this;
        $ctrl.title = 'Update Case';
        $ctrl.dateFormat = "MM/DD/YYYY";

        $ctrl.$onInit = function () {
            console.log('case edit init');
            if ($ctrl.resolve) {
                $ctrl.case = angular.copy($ctrl.resolve.case);
                //$ctrl.case.dateFiled = new Date($ctrl.case.dateFiled);
                console.log('case', $ctrl.case);
            }
            if ($ctrl.case === undefined) {
                $ctrl.title = 'New Case';
            }
        }

        $ctrl.cancel = function () {
            $ctrl.dismiss();
        }

        $ctrl.save = function () {
            console.log('save', $ctrl.case);
            debugger;
            //$ctrl.case.dateFiled = $ctrl.case.dateFiled
            if ($ctrl.case.id !== undefined) {
                $http.put('/api/case', $ctrl.case).then(function (r) {
                    $ctrl.modalInstance.close($ctrl.case);
                });
            } else {
                $http.post('/api/case', $ctrl.case).then(function (r) {
                    $ctrl.modalInstance.close(r.data);
                });
            }
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