/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('scaffolding').config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.scaffoldingTestbed.gvabutton', '/gvabutton', ['@root', '../test/gva/directives/testbeds/gvaButtonTestbed.html', 'GvaButtonTestbedCtrl']])
      .state(['root.scaffoldingTestbed.gvatabs', '/gvatabs', ['@root', '../test/gva/directives/testbeds/gvaTabsTestbed.html', 'GvaTabsTestbedCtrl']])
      .state(['root.scaffoldingTestbed.gvatabs.tab1', '/tab1', ['@root.scaffoldingTestbed.gvatabs']])
      .state(['root.scaffoldingTestbed.gvatabs.tab2', '/tab2', ['@root.scaffoldingTestbed.gvatabs']])
      .state(['root.scaffoldingTestbed.gvatabs.subtab1', '/subtab1', ['@root.scaffoldingTestbed.gvatabs']])
      .state(['root.scaffoldingTestbed.gvatabs.subtab2', '/subtab2', ['@root.scaffoldingTestbed.gvatabs']])
      .state(['root.scaffoldingTestbed.gvatabs.subtab3', '/subtab3', ['@root.scaffoldingTestbed.gvatabs']]);
  }]);
}(angular));
