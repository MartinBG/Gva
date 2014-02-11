/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('scaffolding').config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.scaffoldingTestbed.gvabutton', '/gvabutton', ['@root', '../test/gva/directives/testbeds/gvaButtonTestbed.html', 'GvaButtonTestbedCtrl']]);
  }]);
}(angular));
