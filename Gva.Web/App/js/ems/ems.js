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
            templateUrl: 'ems/docs/views/docSearch.html',
            controller: 'DocsSearchCtrl'
          }
        }
      })
      .state({
        name: 'doc.edit',
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
        name: 'doc.addressing',
        title: 'Адресати',
        parent: 'doc.edit',
        url: '/addresses',
        views: {
          'detailView@doc.edit': {
            templateUrl: 'ems/docs/views/docAddressing.html',
            controller: 'DocAddressingCtrl'
          }
        }
      })
      .state({
        name: 'doc.content',
        title: 'Прикачени файлове',
        parent: 'doc.edit',
        url: '/content',
        views: {
          'detailView@doc.edit': {
            templateUrl: 'ems/docs/views/docContent.html',
            controller: 'DocContentCtrl'
          }
        }
      })
      .state({
        name: 'doc.workflows',
        title: 'Управление',
        parent: 'doc.edit',
        url: '/workflows',
        views: {
          'detailView@doc.edit': {
            templateUrl: 'ems/docs/views/docWorkflows.html',
            controller: 'DocWorkflowsCtrl'
          }
        }
      })
    .state({
      name: 'doc.stages',
      title: 'Етапи',
      parent: 'doc.edit',
      url: '/stages',
      views: {
        'detailView@doc.edit': {
          templateUrl: 'ems/docs/views/docStages.html',
          controller: 'DocStagesCtrl'
        }
      }
    })
    .state({
      name: 'doc.case',
      title: 'Преписка',
      parent: 'doc.edit',
      url: '/case',
      views: {
        'detailView@doc.edit': {
          templateUrl: 'ems/docs/views/docCase.html',
          controller: 'DocCaseCtrl'
        }
      }
    })
    .state({
      name: 'doc.classifications',
      title: 'Класификация',
      parent: 'doc.edit',
      url: '/classifications',
      views: {
        'detailView@doc.edit': {
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
        name: 'corrs.search',
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
        name: 'corrs.new',
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
        name: 'corrs.edit',
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
