(function (angular) {
  'use strict';
  angular.module('app', [
    'ng',
    'ui.router',
    'navigation',
    'users'
  ]).config([
    '$urlRouterProvider',
    '$stateProvider',
    '$locationProvider',
    'navigation.NavbarConfigProvider',
    'users.StatesProvider',
    function (
      $urlRouterProvider,
      $stateProvider,
      $locationProvider,
      navbarConfigProvider,
      usersStatesProvider
    ) {
      $locationProvider.html5Mode(false);
      $urlRouterProvider.otherwise('/users');
        
      navbarConfigProvider
        .addItem({ text: 'Регистър', url: '/search', icon: 'icon-search' })
        .addItem({
          text: 'Администриране',
          icon: 'icon-wrench',
          permissions: [ 'sys#admin' ],
          items: [
            { text: 'Потребители', state: usersStatesProvider.states.search },
            { text: 'Редакция', state: usersStatesProvider.states.edit },
            { text: 'Номенклатури', url: '/nomenclatures' }
          ]
        })
        .addItem({ text: 'Помощ', items: [
          {
            text: 'Ръководство на потребителя',
            url: '/docs/kr_help.pdf',
            newTab: true
          }
        ]})
        .setUserFullName("Администратор")
        .setUserHasPassword(true);
    }
  ]);
}(angular));
