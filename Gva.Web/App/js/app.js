/*global angular*/
(function (angular) {
  'use strict';
  angular.module('app', [
    'ng',
    'ui.router',
    'ui.select2',
    'boot',
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
          state: 'root.docs.search',
          items: [
            { text: 'Нов документ', state: 'root.docs.new' },
            { text: 'Кореспонденти', state: 'root.corrs.search' },
            { text: 'Нов кореспондент', state: 'root.corrs.new' }
          ]
        })
        .addItem({ text: 'ЛАП', icon: 'fa fa-users', items: [
          { text: 'Физически лица', state: 'root.persons.search' },
          { text: 'Ново физическо лице', state: 'root.persons.new' },
          { text: 'Лицензи', url: '/licences' },
          { text: 'Квалификации', url: '/qualifications' },
          { text: 'Медицински', url: '/medical' },
          { text: 'Заявления', state: 'root.applications.search' },
          { text: 'Ново заявление', state: 'root.applications.new.doc' },
          { text: 'Свържи заявление', state: 'root.applications.link.common' }
        ]})
        .addItem({text: 'ВС', icon: 'glyphicon glyphicon-plane', url: '/aircraft', items: [
          { text: 'Удостоверения', url: '/licences' },
          { text: 'Инспекции', url: '/licences' }
        ]})
        .addItem({ text: 'Админ', icon: 'glyphicon glyphicon-wrench', items: [
          { text: 'Потребители', state: 'root.users.search' }
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
        .setBreadcrumbBarHomeState('root.persons');
    }
  ]).run(['l10n', '$rootScope', function (l10n, $rootScope) {
    $rootScope.l10n = l10n;
  }]);
}(angular));
