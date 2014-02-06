/*global angular*/
(function (angular) {
  'use strict';
  angular.module('app', [
    'ng',
    'ui.router',
    'ui.select2',
    'common',
    'scaffolding',
    'gva',
    'ems',
    'l10n',
    'l10n-tools'
  ]).config([
    '$urlRouterProvider',
    '$stateProvider',
    '$locationProvider',
    'NavigationConfigProvider',
    function (
      $urlRouterProvider,
      $stateProvider,
      $locationProvider,
      navigationConfigProvider
    ) {
      $locationProvider.html5Mode(false);
      $urlRouterProvider.otherwise('/persons');

      navigationConfigProvider
        .addItem({
          text: 'Документи',
          icon: 'glyphicon glyphicon-folder-open',
          state: 'docs/search',
          items: [
            { text: 'Нов документ', state: 'docs/new' },
            { text: 'Кореспонденти', state: 'corrs/search' },
            { text: 'Нов кореспондент', state: 'corrs/new' }
          ]
        })
        .addItem({ text: 'ЛАП', icon: 'fa fa-users', items: [
          { text: 'Физически лица', state: 'persons.search' },
          { text: 'Ново физическо лице', state: 'persons.new' },
          { text: 'Лицензи', url: '/licences' },
          { text: 'Квалификации', url: '/qualifications' },
          { text: 'Медицински', url: '/medical' },
          { text: 'Заявления', state: 'applications/search' },
          { text: 'Ново заявление', state: 'applications/new/doc' },
          { text: 'Свържи заявление', state: 'applications/link/common' }
        ]})
        .addItem({text: 'ВС', icon: 'glyphicon glyphicon-plane', url: '/aircraft', items: [
          { text: 'Удостоверения', url: '/licences' },
          { text: 'Инспекции', url: '/licences' }
        ]})
        .addItem({ text: 'Админ', icon: 'glyphicon glyphicon-wrench', items: [
          { text: 'Потребители', state: 'users.search' }
        ]})
        .addItem({ text: 'Помощ', items: [
          {
            text: 'Ръководство на потребителя',
            url: '/docs/kr_help.pdf',
            newTab: true
          }
        ]})
        .setUserFullName('Администратор')
        .setUserHasPassword(true)
        .showBreadcrumbBar(true)
        .setBreadcrumbBarHomeState('persons');
    }
  ]).run(['l10n', '$rootScope', function (l10n, $rootScope) {
    $rootScope.l10n = l10n;
  }]);
}(angular));
