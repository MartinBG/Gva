(function (angular) {
  'use strict';

  function StatesProvider($stateProvider, navigationStatesProvider) {
    var users = {
        name: 'users',
        title: 'Потребители',
        url: '/users?username&fullname&showActive',
        parent: navigationStatesProvider.states.root,
        'abstract': true,
        views: {
          'pageView': {
            template: '<div ui-view="pageView"></div>'
          }
        }
      };

    this.states = {
      'users': users,
      'search': {
        name: 'users.search',
        parent: users,
        url: '',
        views: {
          'pageView': {
            templateUrl: 'users/templates/search.html',
            controller: 'users.SearchCtrl'
          }
        }
      },
      'newUser': {
        name: 'users.newUser',
        title: 'Нов потребител',
        parent: users,
        url: '/new',
        views: {
          'pageView': {
            templateUrl: 'users/templates/edit.html',
            controller: 'users.EditCtrl'
          }
        }
      },
      'edit': {
        name: 'users.edit',
        title: 'Редакция',
        parent: users,
        url: '/:userId',
        views: {
          'pageView': {
            templateUrl: 'users/templates/edit.html',
            controller: 'users.EditCtrl'
          }
        }
      }
    };

    $stateProvider
    .state(this.states.users)
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