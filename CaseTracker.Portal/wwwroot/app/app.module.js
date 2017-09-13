(function () {
    'use strict';

    var module = angular.module('app', [
        //third party modules
        'ui.bootstrap'
    ]);

    module.run(function () {
        console.log('Application Started');
    })
})();