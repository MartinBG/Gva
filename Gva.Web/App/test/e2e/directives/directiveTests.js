/*global angular*/
(function (angular) {
  'use strict';
  angular.module('directive-tests', [
    'directive-tests.templates'
  ]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'directive-tests.input',
        parent: 'root',
        url: '/test/input',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-x/templates/scInput.html',
            controller: 'directive-tests.ScInputCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.select',
        parent: 'root',
        url: '/test/select',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-x/templates/scSelect.html',
            controller: 'directive-tests.ScSelectCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.nomenclature',
        parent: 'root',
        url: '/test/nom',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-x/templates/scNomenclature.html',
            controller: 'directive-tests.ScNomenclatureCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.search',
        parent: 'root',
        url: '/test/search',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-search/templates/scSearch.html',
            controller: 'directive-tests.ScSearchCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.files',
        parent: 'root',
        url: '/test/files',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-files/templates/scFiles.html',
            controller: 'directive-tests.ScFilesCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.datatable',
        parent: 'root',
        url: '/test/datatable',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-datatable/templates/scDatatable.html',
            controller: 'directive-tests.ScDatatableCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.datatable.column',
        parent: 'root',
        url: '/test/datatable/column',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-datatable/templates/scColumn.html',
            controller: 'directive-tests.ScDatatableCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.promiseState',
        parent: 'root',
        url: '/test/promise',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-promise-state/templates/scPromiseState.html',
            controller: 'directive-tests.ScPromiseStateCtrl'
          }
        }
      });
  }]);
}(angular));
