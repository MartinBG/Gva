(function (angular) {
  'use strict';

  function StatesProvider($stateProvider, navigationStatesProvider) {
    this.states = {
      'search': {
        name: 'users.search',
        parent: navigationStatesProvider.states.root,
        url: '/users?username&fullname&showActive',
        views: {
          'pageView': {
            templateUrl: 'users/templates/search.html',
            controller: 'users.SearchCtrl'
          }
        }
      },
      'newUser': {
        name: 'users.newUser',
        parent: navigationStatesProvider.states.root,
        url: '/users/new',
        views: {
          'pageView': {
            templateUrl: 'users/templates/edit.html',
            controller: 'users.EditCtrl'
          }
        }
      },
      'edit': {
        name: 'users.edit',
        parent: navigationStatesProvider.states.root,
        url: '/users/:userId',
        views: {
          'pageView': {
            templateUrl: 'users/templates/edit.html',
            controller: 'users.EditCtrl'
          }
        }
      }
    };

    $stateProvider
      .state(this.states.search)
      .state(this.states.newUser)
      .state(this.states.edit);
  }

  StatesProvider.$inject = ['$stateProvider', 'navigation.StatesProvider'];

  StatesProvider.prototype.$get = function () {
    return this.states;
  };

  angular.module('users').provider('users.States', StatesProvider);
}(angular));