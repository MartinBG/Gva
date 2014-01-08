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
        name: 'docs/search',
        parent: 'docs',
        url: '',
        views: {
          'pageView@root': {
            templateUrl: 'ems/docs/views/docsSearch.html',
            controller: 'DocsSearchCtrl'
          }
        }
      })
      .state({
        name: 'docs/edit',
        title: 'Редакция',
        parent: 'docs',
        url: '/:docId',
        views: {
          'pageView@root': {
            templateUrl: 'ems/docs/views/docEdit.html',
            controller: 'DocsEditCtrl'
          }
        }
      })
      .state({
        name: 'docs/edit/addressing',
        title: 'Адресати',
        parent: 'docs/edit',
        url: '/addresses',
        views: {
          'detailView@docs/edit': {
            templateUrl: 'ems/docs/views/docAddressing.html',
            controller: 'DocAddressingCtrl'
          }
        }
      })
      .state({
        name: 'docs/edit/content',
        title: 'Прикачени файлове',
        parent: 'docs/edit',
        url: '/content',
        views: {
          'detailView@docs/edit': {
            templateUrl: 'ems/docs/views/docContent.html',
            controller: 'DocContentCtrl'
          }
        }
      })
      .state({
        name: 'docs/edit/workflows',
        title: 'Управление',
        parent: 'docs/edit',
        url: '/workflows',
        views: {
          'detailView@docs/edit': {
            templateUrl: 'ems/docs/views/docWorkflows.html',
            controller: 'DocWorkflowsCtrl'
          }
        }
      })
      .state({
        name: 'docs/edit/stages',
        title: 'Етапи',
        parent: 'docs/edit',
        url: '/stages',
        views: {
          'detailView@docs/edit': {
            templateUrl: 'ems/docs/views/docStages.html',
            controller: 'DocStagesCtrl'
          }
        }
      })
      .state({
        name: 'docs/edit/case',
        title: 'Преписка',
        parent: 'docs/edit',
        url: '/case',
        views: {
          'detailView@docs/edit': {
            templateUrl: 'ems/docs/views/docCase.html',
            controller: 'DocCaseCtrl'
          }
        }
      })
      .state({
        name: 'docs/edit/classifications',
        title: 'Класификация',
        parent: 'docs/edit',
        url: '/classifications',
        views: {
          'detailView@docs/edit': {
            templateUrl: 'ems/docs/views/docClassifications.html',
            controller: 'DocClassificationsCtrl'
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
        name: 'corrs/search',
        parent: 'corrs',
        url: '',
        views: {
          'pageView@root': {
            templateUrl: 'ems/corrs/views/corrSearch.html',
            controller: 'CorrsSearchCtrl'
          }
        }
      })
      .state({
        name: 'corrs/new',
        title: 'Нов кореспондент',
        parent: 'corrs',
        url: '/new',
        views: {
          'pageView@root': {
            templateUrl: 'ems/corrs/views/corrEdit.html',
            controller: 'CorrsEditCtrl'
          }
        }
      })
      .state({
        name: 'corrs/edit',
        title: 'Редакция',
        parent: 'corrs',
        url: '/:corrId',
        views: {
          'pageView@root': {
            templateUrl: 'ems/corrs/views/corrEdit.html',
            controller: 'CorrsEditCtrl'
          }
        }
      });
  }]);
}(angular));
