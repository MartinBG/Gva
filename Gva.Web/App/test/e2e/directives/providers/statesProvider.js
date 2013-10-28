(function (angular) {
  'use strict';

  function StatesProvider($stateProvider, navigationStatesProvider) {
    this.states = {
      'scInputTest': {
        name: 'directive-tests.text',
        parent: navigationStatesProvider.states.root,
        url: '/test/input',
        views: {
          'pageView': {
            templateUrl: '../test/e2e/directives/templates/scInput.html',
            controller: 'directive-tests.ScInputCtrl'
          }
        }
      }
    };

    $stateProvider
      .state(this.states.scInputTest);
  }

  StatesProvider.$inject = ['$stateProvider', 'navigation.StatesProvider'];

  StatesProvider.prototype.$get = function () {
    return this.states;
  };

  angular.module('directive-tests').provider('directive-tests.States', StatesProvider);
}(angular));