//case-edit.component.js
(function () {
    var module = angular.module('app');

    function controller($http, uibDateParser) {
        var $ctrl = this;
        $ctrl.title = 'Update Case';
        $ctrl.dateFormat = "MM/DD/YYYY";
        $ctrl.dateOptions = {
            minDate: '1940-01-01'
        };

        $ctrl.$onInit = function () {
            console.log('case edit init');
            if ($ctrl.resolve) {
                $ctrl.case = angular.copy($ctrl.resolve.case);

                var isValidDate = moment($ctrl.resolve.case.dateFiled).isValid();
                if (moment($ctrl.resolve.case.dateFiled).isValid()) {
                    $ctrl.case.dateFiled = new Date($ctrl.case.dateFiled);
                } else {
                    $ctrl.case.dateFiled = new Date();
                }
            }
            if ($ctrl.case === undefined) {
                $ctrl.title = 'New Case';
            }

            $http.get('/api/court/list').then(function (r) {
                $ctrl.courts = r.data;
            })
        }

        $ctrl.cancel = function () {
            $ctrl.dismiss();
        }

        $ctrl.save = function () {
            if ($ctrl.case.id !== undefined) {
                $http.put('/api/case', $ctrl.case).then(function (r) {
                    $ctrl.modalInstance.close(r.data);
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