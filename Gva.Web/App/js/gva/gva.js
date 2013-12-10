/*global angular*/
(function (angular) {
  'use strict';
  angular.module('gva', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'gva.templates',
    'navigation',
    'l10n',
    'l10n-tools'
  ]).config(['scaffoldingProvider', function (scaffoldingProvider) {
    scaffoldingProvider.form({
      name: 'gvaPersonData',
      templateUrl: 'gva/persons/forms/personData.html',
      controller: 'PersonDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonAddress',
      templateUrl: 'gva/persons/forms/personAddress.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentId',
      templateUrl: 'gva/persons/forms/personDocumentId.html'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'persons',
        title: 'Физически лица',
        parent: 'root',
        url: '/persons',
        'abstract': true
      })
      .state({
        name: 'persons.search',
        parent: 'persons',
        url: '',
        views: {
          'pageView@root': {
            templateUrl: 'gva/persons/views/personsSearch.html',
            controller: 'PersonsSearchCtrl'
          }
        }
      })
      .state({
        name: 'persons.new',
        title: 'Ново физическо лице',
        parent: 'persons',
        url: '/new',
        views: {
          'pageView@root': {
            templateUrl: 'gva/persons/views/personsNew.html',
            controller: 'PersonsNewCtrl'
          }
        }
      })
      .state({
        name: 'persons.view',
        title: 'Лично досие',
        parent: 'persons',
        url: '/:id',
        views: {
          'pageView@root': {
            templateUrl: 'gva/persons/views/personsView.html',
            controller: 'PersonsViewCtrl'
          }
        }
      })
      .state({
        name: 'persons.addresses',
        title: 'Адреси',
        parent: 'persons.view',
        url: '/addresses',
        'abstract': true
      })
      .state({
        name: 'persons.addresses.search',
        parent: 'persons.addresses',
        url: '',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/addressesSearch.html',
            controller: 'AddressesSearchCtrl'
          }
        }
      })
      .state({
        name: 'persons.addresses.new',
        title: 'Нов адрес',
        parent: 'persons.addresses',
        url: '/new',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/addressesNew.html',
            controller: 'AddressesNewCtrl'
          }
        }
      })
      .state({
        name: 'persons.addresses.edit',
        title: 'Редакция на адрес',
        parent: 'persons.addresses',
        url: '/:ind',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/addressesEdit.html',
            controller: 'AddressesEditCtrl'
          }
        }
      })
      .state({
        name: 'persons.statuses',
        title: 'Състояния',
        parent: 'persons.view',
        url: '/statuses',
        'abstract': true
      })
      .state({
        name: 'persons.statuses.search',
        parent: 'persons.statuses',
        url: '',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/statusesSearch.html',
            controller: 'StatusesSearchCtrl'
          }
        }
      })
      .state({
        name: 'persons.statuses.new',
        title: 'Ново състояние',
        parent: 'persons.statuses',
        url: '/new',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/statusesNew.html',
            controller: 'StatusesNewCtrl'
          }
        }
      })
      .state({
        name: 'persons.statuses.edit',
        title: 'Редакция на състояние',
        parent: 'persons.statuses',
        url: '/:ind',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/statusesEdit.html',
            controller: 'StatusesEditCtrl'
          }
        }
      });
  }]);
}(angular));
