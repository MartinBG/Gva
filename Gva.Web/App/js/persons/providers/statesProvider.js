/*global angular*/
(function (angular) {
  'use strict';

  function StatesProvider($stateProvider, navigationStatesProvider) {
    this.states = {
      'newPerson': {
        name: 'persons.new',
        title: 'Ново физическо лице',
        parent: navigationStatesProvider.states.root,
        url: '/persons/new',
        views: {
          'pageView': {
            templateUrl: 'persons/views/new/new.html',
            controller: 'persons.NewCtrl'
          }
        }
      }
    };

    $stateProvider
      .state(this.states.newPerson);
  }

  StatesProvider.$inject = ['$stateProvider', 'navigation.StatesProvider'];

  StatesProvider.prototype.$get = function () {
    return this.states;
  };

  angular.module('persons').provider('persons.States', StatesProvider);
}(angular));