/*global angular*/
(function (angular) {
  'use strict';
  angular.module('directive-tests', [
    'directive-tests.templates'
  ]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'directive-tests',
        parent: 'root',
        url: '/test',
        'abstract': true
      })
      .state({
        name: 'directive-tests.input',
        url: '/input',
        views: {
          'pageView@root': {
            templateUrl: '../test/e2e/directives/sc-x/templates/scInput.html',
            controller: 'directive-tests.ScInputCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.select',
        url: '/select',
        views: {
          'pageView@root': {
            templateUrl: '../test/e2e/directives/sc-x/templates/scSelect.html',
            controller: 'directive-tests.ScSelectCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.nomenclature',
        url: '/nom',
        views: {
          'pageView@root': {
            templateUrl: '../test/e2e/directives/sc-x/templates/scNomenclature.html',
            controller: 'directive-tests.ScNomenclatureCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.search',
        url: '/search',
        views: {
          'pageView@root': {
            templateUrl: '../test/e2e/directives/sc-search/templates/scSearch.html',
            controller: 'directive-tests.ScSearchCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.files',
        url: '/files',
        views: {
          'pageView@root': {
            templateUrl: '../test/e2e/directives/sc-files/templates/scFiles.html',
            controller: 'directive-tests.ScFilesCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.datatable',
        url: '/datatable',
        views: {
          'pageView@root': {
            templateUrl: '../test/e2e/directives/sc-datatable/templates/scDatatable.html',
            controller: 'directive-tests.ScDatatableCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.column',
        url: '/column',
        views: {
          'pageView@root': {
            templateUrl: '../test/e2e/directives/sc-datatable/templates/scColumn.html',
            controller: 'directive-tests.ScDatatableCtrl'
          }
        }
      })
      .state({
        name: 'directive-tests.promiseState',
        url: '/promise',
        views: {
          'pageView@root': {
            templateUrl: '../test/e2e/directives/sc-promise-state/templates/scPromiseState.html',
            controller: 'directive-tests.ScPromiseStateCtrl'
          }
        }
      });
  }]);
}(angular));
