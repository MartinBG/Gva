/*global angular*/
(function (angular) {
  'use strict';
  angular.module('corrs', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'navigation',
    'corrs.templates',
    'l10n',
    'l10n-tools'
  ]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'corrs',
        title: 'Кореспонденти',
        url: '/corrs?displayName&email',
        parent: 'root',
        'abstract': true
      })
      .state({
        name: 'corrs.search',
        parent: 'corrs',
        url: '',
        views: {
          'pageView@root': {
            templateUrl: 'corrs/templates/search.html',
            controller: 'CorrsSearchCtrl'
          }
        }
      })
      .state({
        name: 'corrs.new',
        title: 'Нов кореспондент',
        parent: 'corrs',
        url: '/new',
        views: {
          'pageView@root': {
            templateUrl: 'corrs/templates/edit.html',
            controller: 'CorrsEditCtrl'
          }
        }
      })
      .state({
        name: 'corrs.edit',
        title: 'Редакция',
        parent: 'corrs',
        url: '/:corrId',
        views: {
          'pageView@root': {
            templateUrl: 'corrs/templates/edit.html',
            controller: 'CorrsEditCtrl'
          }
        }
      });
  }]);
}(angular));
