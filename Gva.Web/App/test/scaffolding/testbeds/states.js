/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('scaffolding').config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.scaffoldingTestbed'                 , '/test'                                                                                                          ])
      .state(['root.scaffoldingTestbed.input'           , '/input'      , ['@root', '../test/scaffolding/testbeds/scInputTestbed.html'       , 'ScInputTestbedCtrl'       ]])
      .state(['root.scaffoldingTestbed.select'          , '/select'     , ['@root', '../test/scaffolding/testbeds/scSelectTestbed.html'      , 'ScSelectTestbedCtrl'      ]])
      .state(['root.scaffoldingTestbed.nomenclature'    , '/nom'        , ['@root', '../test/scaffolding/testbeds/scNomenclatureTestbed.html', 'ScNomenclatureTestbedCtrl']])
      .state(['root.scaffoldingTestbed.search'          , '/search'     , ['@root', '../test/scaffolding/testbeds/scSearchTestbed.html'      , 'ScSearchTestbedCtrl'      ]])
      .state(['root.scaffoldingTestbed.files'           , '/files'      , ['@root', '../test/scaffolding/testbeds/scFilesTestbed.html'       , 'ScFilesTestbedCtrl'       ]])
      .state(['root.scaffoldingTestbed.datatable'       , '/datatable'  , ['@root', '../test/scaffolding/testbeds/scDatatableTestbed.html'   , 'ScDatatableTestbedCtrl'   ]])
      .state(['root.scaffoldingTestbed.promiseState'    , '/promise'    , ['@root', '../test/scaffolding/testbeds/scPromiseStateTestbed.html', 'ScPromiseStateTestbedCtrl']])
      .state(['root.scaffoldingTestbed.scbutton'        , '/scbutton'   , ['@root', '../test/scaffolding/testbeds/scButtonTestbed.html'      , 'ScButtonTestbedCtrl']])
      .state(['root.scaffoldingTestbed.sctabs'          , '/sctabs'     , ['@root', '../test/scaffolding/testbeds/scTabsTestbed.html'        , 'ScTabsTestbedCtrl']])
      .state(['root.scaffoldingTestbed.sctabs.tab1'     , '/tab1'       , ['@root.scaffoldingTestbed.sctabs']])
      .state(['root.scaffoldingTestbed.sctabs.tab2'     , '/tab2'       , ['@root.scaffoldingTestbed.sctabs']])
      .state(['root.scaffoldingTestbed.sctabs.subtab1'  , '/subtab1'    , ['@root.scaffoldingTestbed.sctabs']])
      .state(['root.scaffoldingTestbed.sctabs.subtab2'  , '/subtab2'    , ['@root.scaffoldingTestbed.sctabs']])
      .state(['root.scaffoldingTestbed.sctabs.subtab3'  , '/subtab3'    , ['@root.scaffoldingTestbed.sctabs']]);
  }]);
}(angular));
