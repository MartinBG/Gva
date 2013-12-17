/*global angular*/
(function (angular) {
  'use strict';
  angular.module('users', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'navigation',
    'users.templates',
    'l10n',
    'l10n-tools'
  ]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'users',
        title: 'Потребители',
        url: '/users?username&fullname&showActive',
        parent: 'root',
        'abstract': true
      })
      .state({
        name: 'users.search',
        parent: 'users',
        url: '',
        views: {
          'pageView@root': {
            templateUrl: 'users/templates/search.html',
            controller: 'UsersSearchCtrl'
          }
        }
      })
      .state({
        name: 'users.new',
        title: 'Нов потребител',
        parent: 'users',
        url: '/new',
        views: {
          'pageView@root': {
            templateUrl: 'users/templates/edit.html',
            controller: 'UsersEditCtrl'
          }
        }
      })
      .state({
        name: 'users.edit',
        title: 'Редакция',
        parent: 'users',
        url: '/:userId',
        views: {
          'pageView@root': {
            templateUrl: 'users/templates/edit.html',
            controller: 'UsersEditCtrl'
          }
        }
      });
  }]);
}(angular));
