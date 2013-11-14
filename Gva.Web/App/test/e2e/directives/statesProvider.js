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
      }
    };

    $stateProvider
      .state(this.states.scInputTest)
      .state(this.states.scSelectTest)
      .state(this.states.scSearchTest)
      .state(this.states.scFilesTest);
  }

  StatesProvider.$inject = ['$stateProvider', 'navigation.StatesProvider'];

  StatesProvider.prototype.$get = function () {
    return this.states;
  };

  angular.module('directive-tests').provider('directive-tests.States', StatesProvider);
}(angular));