/*global angular*/
(function (angular) {
  'use strict';
  angular.module('ems', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'common',
    'ems.templates',
    'l10n',
    'l10n-tools'
  ]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'docs',
        title: 'Документи',
        url: '/docs?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units',
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
        name: 'docs/new',
        title: 'Нов документ',
        parent: 'docs',
        url: '/new',
        views: {
          'pageView@root': {
            templateUrl: 'ems/docs/views/docsNew.html',
            controller: 'DocsNewCtrl'
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
            templateUrl: 'ems/docs/views/docsEdit.html',
            controller: 'DocsEditCtrl'
          }
        }
      })
      .state({
        name: 'docs/edit/addressing',
        title: 'Адресати',
        parent: 'docs/edit',
        url: '/address',
        views: {
          'detailView@docs/edit': {
            templateUrl: 'ems/docs/views/docsAddressing.html',
            controller: 'DocsAddressingCtrl'
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
            templateUrl: 'ems/docs/views/docsContent.html',
            controller: 'DocsContentCtrl'
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
            templateUrl: 'ems/docs/views/docsWorkflows.html',
            controller: 'DocsWorkflowsCtrl'
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
            templateUrl: 'ems/docs/views/docsStages.html',
            controller: 'DocsStagesCtrl'
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
            templateUrl: 'ems/docs/views/docsCase.html',
            controller: 'DocsCaseCtrl'
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
            templateUrl: 'ems/docs/views/docsClassifications.html',
            controller: 'DocsClassificationsCtrl'
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
