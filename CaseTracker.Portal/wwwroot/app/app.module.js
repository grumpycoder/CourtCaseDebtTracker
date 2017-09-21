(function () {
    'use strict';

    var module = angular.module('app', [
        //third party modules
        'ui.bootstrap',
        'ngSanitize',
        'ui.select',
        'angular-confirm'
    ]);

    module.run(function () {
        console.log('Application Started');
    })
})();