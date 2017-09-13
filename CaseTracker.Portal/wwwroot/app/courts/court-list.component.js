//court-list.component.js
(function () {
    var module = angular.module('app');

    function controller($http) {
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

    }

    module.component('courtList', {
        templateUrl: 'app/courts/court-list.component.html',
        controller: ['$http', controller]
    });
})();