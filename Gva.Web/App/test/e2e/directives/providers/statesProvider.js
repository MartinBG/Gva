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
            templateUrl: '../test/e2e/directives/templates/scInput.html',
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
            templateUrl: '../test/e2e/directives/templates/scSelect.html',
            controller: 'directive-tests.ScSelectCtrl'
          }
        }
      }
    };

    $stateProvider
      .state(this.states.scInputTest)
      .state(this.states.scSelectTest);
  }

  StatesProvider.$inject = ['$stateProvider', 'navigation.StatesProvider'];

  StatesProvider.prototype.$get = function () {
    return this.states;
  };

  angular.module('directive-tests').provider('directive-tests.States', StatesProvider);
}(angular));