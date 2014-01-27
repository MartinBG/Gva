﻿/*global angular*/
(function (angular) {
  'use strict';
  angular.module('gva', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'gva.templates',
    'common',
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
      templateUrl: 'gva/persons/forms/personDocumentId.html',
      controller: 'PersonDocumentIdCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonStatus',
      templateUrl: 'gva/persons/forms/personStatus.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonScannedDocument',
      templateUrl: 'gva/persons/forms/personScannedDocument.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonApplication',
      templateUrl: 'gva/persons/forms/personApplication.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentMedical',
      templateUrl: 'gva/persons/forms/personDocumentMedical.html',
      controller: 'PersonDocumentMedicalCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentEmployment',
      templateUrl: 'gva/persons/forms/personDocumentEmployment.html',
      controller: 'PersonDocumentEmploymentCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentCheck',
      templateUrl: 'gva/persons/forms/personDocumentCheck.html',
      controller: 'PersonDocumentCheckCtrl'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state({
        name: 'persons',
        title: 'Физически лица',
        parent: 'root',
        url: '/persons?exact&lin&uin&names&licences&ratings&organization',
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
        name: 'persons.edit',
        title: 'Редакция',
        parent: 'persons.view',
        url: '/personData',
        views: {
          'pageView@root': {
            templateUrl: 'gva/persons/views/personDataEdit.html',
            controller: 'PersonDataEditCtrl'
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
      })
      .state({
        name: 'persons.documentIds',
        title: 'Документи за самоличност',
        parent: 'persons.view',
        url: '/documentIds',
        'abstract': true
      })
      .state({
        name: 'persons.documentIds.search',
        parent: 'persons.documentIds',
        url: '',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentIdsSearch.html',
            controller: 'DocumentIdsSearchCtrl'
          }
        }
      })
      .state({
        name: 'persons.documentIds.new',
        title: 'Нов документ за самоличност',
        parent: 'persons.documentIds',
        url: '/new',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentIdsNew.html',
            controller: 'DocumentIdsNewCtrl'
          }
        }
      })
      .state({
        name: 'persons.documentIds.edit',
        title: 'Редакция на документ за самоличност',
        parent: 'persons.documentIds',
        url: '/:ind',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentIdsEdit.html',
            controller: 'DocumentIdsEditCtrl'
          }
        }
      })
      .state({
        name: 'persons.licences',
        title: 'Лицензи',
        parent: 'persons.view',
        url: '/licences',
        'abstract': true
      })
      .state({
        name: 'persons.licences.search',
        parent: 'persons.licences',
        url: '',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/licencesSearch.html',
            controller: 'LicencesSearchCtrl'
          }
        }
      })
      .state({
        name: 'persons.checks',
        title: 'Проверки',
        parent: 'persons.view',
        url: '/checks',
        'abstract': true
      })
      .state({
        name: 'persons.checks.search',
        parent: 'persons.checks',
        url: '',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentChecksSearch.html',
            controller: 'DocumentChecksSearchCtrl'
          }
        }
      })
      .state({
        name: 'persons.checks.new',
        title: 'Нова проверка',
        parent: 'persons.checks',
        url: '/new',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentChecksNew.html',
            controller: 'DocumentChecksNewCtrl'
          }
        }
      })
      .state({
        name: 'persons.checks.edit',
        title: 'Редакция на проверка',
        parent: 'persons.checks',
        url: '/:ind',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentChecksEdit.html',
            controller: 'DocumentChecksEditCtrl'
          }
        }
      }).state({
        name: 'persons.employments',
        title: 'Месторабота',
        parent: 'persons.view',
        url: '/employments',
        'abstract': true
      })
      .state({
        name: 'persons.employments.search',
        parent: 'persons.employments',
        url: '',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentEmploymentsSearch.html',
            controller: 'DocumentEmploymentsSearchCtrl'
          }
        }
      })
      .state({
        name: 'persons.employments.new',
        title: 'Новa месторабота',
        parent: 'persons.employments',
        url: '/new',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentEmploymentsNew.html',
            controller: 'DocumentEmploymentsNewCtrl'
          }
        }
      })
      .state({
        name: 'persons.employments.edit',
        title: 'Редакция на месторабота',
        parent: 'persons.employments',
        url: '/:ind',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentEmploymentsEdit.html',
            controller: 'DocumentEmploymentsEditCtrl'
          }
        }
      })
      .state({
        name: 'persons.medicals',
        title: 'Медицински',
        parent: 'persons.view',
        url: '/medicals',
        'abstract': true
      })
      .state({
        name: 'persons.medicals.search',
        parent: 'persons.medicals',
        url: '',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentMedicalsSearch.html',
            controller: 'DocumentMedicalsSearchCtrl'
          }
        }
      })
      .state({
        name: 'persons.medicals.new',
        title: 'Новo медицинско',
        parent: 'persons.medicals',
        url: '/new',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentMedicalsNew.html',
            controller: 'DocumentMedicalsNewCtrl'
          }
        }
      })
      .state({
        name: 'persons.medicals.edit',
        title: 'Редакция на медицинско',
        parent: 'persons.medicals',
        url: '/:ind',
        views: {
          'detailView@persons.view': {
            templateUrl: 'gva/persons/views/documentMedicalsEdit.html',
            controller: 'DocumentMedicalsEditCtrl'
          }
        }
      });
  }]);
}(angular));
