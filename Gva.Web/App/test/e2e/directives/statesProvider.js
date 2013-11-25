/*global angular*/
(function (angular) {
  'use strict';

  function StatesProvider($stateProvider, navigationStatesProvider) {
    this.states = {
      'scInputTest': {
        name: 'directive-tests.input',
        parent: navigationStatesProvider.states.root,
        url: '/test/input',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-x/templates/scInput.html',
            controller: 'directive-tests.ScInputCtrl'
          }
        }
      },
      'scSelectTest': {
        name: 'directive-tests.select',
        parent: navigationStatesProvider.states.root,
        url: '/test/select',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-x/templates/scSelect.html',
            controller: 'directive-tests.ScSelectCtrl'
          }
        }
      },
      'scNomenclatureTest': {
        name: 'directive-tests.nomenclature',
        parent: navigationStatesProvider.states.root,
        url: '/test/nom',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-x/templates/scNomenclature.html',
            controller: 'directive-tests.ScNomenclatureCtrl'
          }
        }
      },
      'scSearchTest': {
        name: 'directive-tests.search',
        parent: navigationStatesProvider.states.root,
        url: '/test/search',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-search/templates/scSearch.html',
            controller: 'directive-tests.ScSearchCtrl'
          }
        }
      },
      'scFilesTest': {
        name: 'directive-tests.files',
        parent: navigationStatesProvider.states.root,
        url: '/test/files',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-files/templates/scFiles.html',
            controller: 'directive-tests.ScFilesCtrl'
          }
        }
      },
      'scDatatableTest': {
        name: 'directive-tests.datatable',
        parent: navigationStatesProvider.states.root,
        url: '/test/datatable',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-datatable/templates/scDatatable.html',
            controller: 'directive-tests.ScDatatableCtrl'
          }
        }
      },
      'scColumnTest': {
        name: 'directive-tests.datatable.column',
        parent: navigationStatesProvider.states.root,
        url: '/test/datatable/column',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-datatable/templates/scColumn.html',
            controller: 'directive-tests.ScDatatableCtrl'
          }
        }
      },
      'scPromiseStateTest': {
        name: 'directive-tests.promiseState',
        parent: navigationStatesProvider.states.root,
        url: '/test/promise',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/sc-promise-state/templates/scPromiseState.html',
            controller: 'directive-tests.ScPromiseStateCtrl'
          }
        }
      }
    };

    $stateProvider
      .state(this.states.scInputTest)
      .state(this.states.scSelectTest)
      .state(this.states.scNomenclatureTest)
      .state(this.states.scSearchTest)
      .state(this.states.scFilesTest)
      .state(this.states.scDatatableTest)
      .state(this.states.scColumnTest)
      .state(this.states.scPromiseStateTest);
  }

  StatesProvider.$inject = ['$stateProvider', 'navigation.StatesProvider'];

  StatesProvider.prototype.$get = function () {
    return this.states;
  };

  angular.module('directive-tests').provider('directive-tests.States', StatesProvider);
}(angular));