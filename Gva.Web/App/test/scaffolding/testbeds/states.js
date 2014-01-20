/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('scaffolding').config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'scaffoldingTestbed',
        parent: 'root',
        url: '/test',
        'abstract': true
      })
      .state({
        name: 'scaffoldingTestbed/input',
        parent: 'scaffoldingTestbed',
        url: '/input',
        views: {
          'pageView@root': {
            templateUrl: '../test/scaffolding/testbeds/scInputTestbed.html',
            controller: 'ScInputTestbedCtrl'
          }
        }
      })
      .state({
        name: 'scaffoldingTestbed/select',
        parent: 'scaffoldingTestbed',
        url: '/select',
        views: {
          'pageView@root': {
            templateUrl: '../test/scaffolding/testbeds/scSelectTestbed.html',
            controller: 'ScSelectTestbedCtrl'
          }
        }
      })
      .state({
        name: 'scaffoldingTestbed/nomenclature',
        parent: 'scaffoldingTestbed',
        url: '/nom',
        views: {
          'pageView@root': {
            templateUrl: '../test/scaffolding/testbeds/scNomenclatureTestbed.html',
            controller: 'ScNomenclatureTestbedCtrl'
          }
        }
      })
      .state({
        name: 'scaffoldingTestbed/search',
        parent: 'scaffoldingTestbed',
        url: '/search',
        views: {
          'pageView@root': {
            templateUrl: '../test/scaffolding/testbeds/scSearchTestbed.html',
            controller: 'ScSearchTestbedCtrl'
          }
        }
      })
      .state({
        name: 'scaffoldingTestbed/files',
        parent: 'scaffoldingTestbed',
        url: '/files',
        views: {
          'pageView@root': {
            templateUrl: '../test/scaffolding/testbeds/scFilesTestbed.html',
            controller: 'ScFilesTestbedCtrl'
          }
        }
      })
      .state({
        name: 'scaffoldingTestbed/datatable',
        parent: 'scaffoldingTestbed',
        url: '/datatable',
        views: {
          'pageView@root': {
            templateUrl: '../test/scaffolding/testbeds/scDatatableTestbed.html',
            controller: 'ScDatatableTestbedCtrl'
          }
        }
      })
      .state({
        name: 'scaffoldingTestbed/promiseState',
        parent: 'scaffoldingTestbed',
        url: '/promise',
        views: {
          'pageView@root': {
            templateUrl: '../test/scaffolding/testbeds/scPromiseStateTestbed.html',
            controller: 'ScPromiseStateTestbedCtrl'
          }
        }
      });
  }]);
}(angular));
