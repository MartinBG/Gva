(function (angular) {
  'use strict';
  angular.module('app', [
    'ng',
    'ui.router',
    'ui.select2',
    'navigation',
    'scaffolding',
    'users',
    'l10n',
    'l10n-tools',
    'l10nTexts_en-bg'
  ]).config([
    '$urlRouterProvider',
    '$stateProvider',
    '$locationProvider',
    'navigation.NavigationConfigProvider',
    'users.StatesProvider',
    function (
      $urlRouterProvider,
      $stateProvider,
      $locationProvider,
      navigationConfigProvider,
      usersStatesProvider
    ) {
      $locationProvider.html5Mode(false);
      $urlRouterProvider.otherwise('/users');

      navigationConfigProvider
        .addItem({ text: 'Персонал', items: [
          { text: 'Физически лица', url: '/personel' },
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
        .addItem({ text: 'Възд. Средства', url: '/aircraft', items: [
          { text: 'Удостоверения', url: '/licenses' },
          { text: 'Инспекции', url: '/licenses' }
        ]})
        .addItem({ text: 'Админ', icon: 'glyphicon-wrench', items: [
          { text: 'Потребители', state: usersStatesProvider.states.search }
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
  ]);
}(angular));
