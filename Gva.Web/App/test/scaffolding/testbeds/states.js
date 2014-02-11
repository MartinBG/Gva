/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('scaffolding').config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.scaffoldingTestbed'             , '/test'                                                                                                        ])
      .state(['root.scaffoldingTestbed.input'       , '/input'    , ['@root', '../test/scaffolding/testbeds/scInputTestbed.html'       , 'ScInputTestbedCtrl'       ]])
      .state(['root.scaffoldingTestbed.select'      , '/select'   , ['@root', '../test/scaffolding/testbeds/scSelectTestbed.html'      , 'ScSelectTestbedCtrl'      ]])
      .state(['root.scaffoldingTestbed.nomenclature', '/nom'      , ['@root', '../test/scaffolding/testbeds/scNomenclatureTestbed.html', 'ScNomenclatureTestbedCtrl']])
      .state(['root.scaffoldingTestbed.search'      , '/search'   , ['@root', '../test/scaffolding/testbeds/scSearchTestbed.html'      , 'ScSearchTestbedCtrl'      ]])
      .state(['root.scaffoldingTestbed.files'       , '/files'    , ['@root', '../test/scaffolding/testbeds/scFilesTestbed.html'       , 'ScFilesTestbedCtrl'       ]])
      .state(['root.scaffoldingTestbed.datatable'   , '/datatable', ['@root', '../test/scaffolding/testbeds/scDatatableTestbed.html'   , 'ScDatatableTestbedCtrl'   ]])
      .state(['root.scaffoldingTestbed.promiseState', '/promise'  , ['@root', '../test/scaffolding/testbeds/scPromiseStateTestbed.html', 'ScPromiseStateTestbedCtrl']])
      .state(['root.scaffoldingTestbed.button'      , '/button'   , ['@root', '../test/gva/directives/testbeds/gvaButtonTestbed.html'  , 'GvaButtonTestbedCtrl'     ]]);
  }]);
}(angular));
