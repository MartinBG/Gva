/*global angular*/
(function (angular) {
  'use strict';
  angular.module('ems', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'navigation',
    'ems.templates',
    'l10n',
    'l10n-tools'
  ]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'docs',
        title: 'Документи',
        url: '/docs',
        parent: 'root',
        'abstract': true
      })
      .state({
        name: 'docs.search',
        parent: 'docs',
        url: '',
        views: {
          'pageView@root': {
            templateUrl: 'ems/docs/forms/docSearch.html',
            controller: 'DocsSearchCtrl'
          }
        }
      })
      .state({
        name: 'docs.edit',
        title: 'Редакция',
        parent: 'docs',
        url: '/:docId',
        views: {
          'pageView@root': {
            templateUrl: 'ems/docs/forms/docEdit.html',
            controller: 'DocsEditCtrl'
          }
        }
      });
  }]).config(['$stateProvider', function ($stateProvider) {
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
            templateUrl: 'ems/corrs/forms/corrSearch.html',
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
            templateUrl: 'ems/corrs/forms/corrEdit.html',
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
            templateUrl: 'ems/corrs/forms/corrEdit.html',
            controller: 'CorrsEditCtrl'
          }
        }
      });
  }]);
}(angular));
