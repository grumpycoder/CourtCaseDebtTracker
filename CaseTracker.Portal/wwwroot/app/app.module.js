(function () {
    'use strict';

    var module = angular.module('app', [
        //third party modules
        'ui.bootstrap',
        'ngSanitize',
        'ui.select'
    ]);

    module.run(function () {
        console.log('Application Started');
    })
})();