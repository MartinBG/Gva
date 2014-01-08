/*global angular*/
(function (angular) {
  'use strict';
  angular.module('app', [
    'ng',
    'ui.router',
    'ui.select2',
    'navigation',
    'scaffolding',
    'users',
    'gva',
    'ems',
    'l10n',
    'l10n-tools',
    'l10nTexts_bg-bg',
    'directive-tests'
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
      $urlRouterProvider.otherwise('/users');

      navigationConfigProvider
        .addItem({ text: 'Персонал', items: [
          { text: 'Физически лица', state: 'persons.search' },
          { text: 'Ново физическо лице', state: 'persons.new' },
          { text: 'Лицензи', url: '/licenses' },
          { text: 'Квалификации', url: '/qualifications' },
          { text: 'Медицински', url: '/medical' }
        ]})
        .addItem({ text: 'Организации', url: '/organizations', items: [
          { text: 'Удостоверения', url: '/licenses' },
          { text: 'Одити', url: '/licenses' },
          { text: 'Надзор', url: '/licenses' },
          { text: 'Ръководен Персонал', url: '/licenses' }
        ]})
        .addItem({text: 'Документи', icon: 'glyphicon-folder-open', state: 'docs.search', items: [
          { text: 'Нов документ', state: 'docs.new' }
        ]})
        .addItem({text: 'Кореспонденти', icon: 'glyphicon-user', state: 'corrs.search', items: [
          { text: 'Нов кореспондент', state: 'corrs.new' }
        ]})
        .addItem({text: 'Възд. Средства', url: '/aircraft', items: [
          { text: 'Удостоверения', url: '/licenses' },
          { text: 'Инспекции', url: '/licenses' }
        ]})
        .addItem({ text: 'Админ', icon: 'glyphicon-wrench', items: [
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
        .showBreadcrumbBar(true);
    }
  ]).run(['l10n', '$rootScope', function (l10n, $rootScope) {
    $rootScope.l10n = l10n;
  }]);
}(angular));
