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
      }
    };

    $stateProvider
      .state(this.states.scInputTest)
      .state(this.states.scSelectTest)
      .state(this.states.scSearchTest)
      .state(this.states.scDatatableTest)
      .state(this.states.scColumnTest);
  }

  StatesProvider.$inject = ['$stateProvider', 'navigation.StatesProvider'];

  StatesProvider.prototype.$get = function () {
    return this.states;
  };

  angular.module('directive-tests').provider('directive-tests.States', StatesProvider);
}(angular));