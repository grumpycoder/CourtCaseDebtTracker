//court-edit.component.js
(function () {
    var module = angular.module('app');

    function controller($http) {
        var $ctrl = this;

        $ctrl.title = 'Court Manager';
        $ctrl.subTitle = 'Court';

        $ctrl.$onInit = function () {
            console.log('court edit init', $ctrl);
            if ($ctrl.resolve) {
                $ctrl.court = angular.copy($ctrl.resolve.court);
            }
        }

        $ctrl.cancel = function () {
            $ctrl.dismiss();
        }

        $ctrl.save = function () {
            console.log('save', $ctrl.court);
            $http.put('api/court', $ctrl.court).then(function (r) {
                $ctrl.modalInstance.close($ctrl.court);
            });
        }


    }

    module.component('courtEdit', {
        templateUrl: 'app/courts/court-edit.component.html',
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