/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('scaffolding').config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'scaffoldingTestbed/button',
        url: '/button',
        parent: 'scaffoldingTestbed',
        views: {
          'pageView@root': {
            templateUrl: '../test/gva/directives/testbeds/gvaButtonTestbed.html',
            controller: 'GvaButtonTestbedCtrl'
          }
        }
      });
  }]);
}(angular));
