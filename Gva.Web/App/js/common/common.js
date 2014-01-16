/*global angular*/
(function (angular) {
  'use strict';
  angular.module('common', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'common.templates',
    'l10n',
    'l10n-tools'
  ]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'root',
        views: {
          'rootView': {
            templateUrl: 'common/navigation/views/navbar.html'
          }
        }
      })
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
            templateUrl: 'common/users/views/search.html',
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
            templateUrl: 'common/users/views/edit.html',
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
            templateUrl: 'common/users/views/edit.html',
            controller: 'UsersEditCtrl'
          }
        }
      });
  }]);
}(angular));
