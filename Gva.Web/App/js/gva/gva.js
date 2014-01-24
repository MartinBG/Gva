/*global angular*/
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
  }])
    .config(['$stateProvider', function ($stateProvider) {
      $stateProvider
        .state({
          name: 'applications',
          title: 'Заявления',
          url: '/applications',
          parent: 'root',
          'abstract': true
        })
        .state({
          name: 'applications/search',
          parent: 'applications',
          url: '',
          views: {
            'pageView@root': {
              templateUrl: 'gva/applications/views/applicationsSearch.html',
              controller: 'ApplicationsSearchCtrl'
            }
          }
        })
        // new application
        .state({
          name: 'applications/new',
          parent: 'applications',
          url: '/new',
          views: {
            'pageView@root': {
              templateUrl: 'gva/applications/views/applicationsNew.html',
              controller: 'ApplicationsNewCtrl'
            }
          }
        })
        .state({
          name: 'applications/new/doc',
          title: 'Ново заявление',
          parent: 'applications/new',
          url: '/doc',
          views: {
            'applicationsNewView': {
              templateUrl: 'gva/applications/views/applicationsNewDoc.html',
              controller: 'ApplicationsNewDocCtrl'
            }
          }
        })
        .state({
          name: 'applications/new/personChoose',
          title: 'Избер на заявител',
          parent: 'applications/new',
          url: '/personChoose?exact&lin&uin&names&licences&ratings&organization',
          views: {
            'applicationsNewView': {
              templateUrl: 'gva/applications/views/personChoose.html',
              controller: 'PersonChooseCtrl'
            }
          }
        })
        .state({
          name: 'applications/new/personNew',
          title: 'Нов заявител',
          parent: 'applications/new',
          url: '/personNew',
          views: {
            'applicationsNewView': {
              templateUrl: 'gva/applications/views/personNew.html',
              controller: 'PersonNewCtrl'
            }
          }
        })
        // link application
        .state({
          name: 'applications/link',
          parent: 'applications',
          url: '/link',
          views: {
            'pageView@root': {
              templateUrl: 'gva/applications/views/applicationsLink.html',
              controller: 'ApplicationsLinkCtrl'
            }
          }
        })
        .state({
          name: 'applications/link/common',
          title: 'Свържи заявление',
          parent: 'applications/link',
          url: '/common',
          views: {
            'linkView@applications/link': {
              templateUrl: 'gva/applications/views/applicationsLinkCommon.html',
              controller: 'ApplicationsLinkCommonCtrl'
            }
          }
        })
        .state({
          name: 'applications/link/docChoose',
          title: 'Избери Док',
          parent: 'applications/link',
          url: '/docChoose',
          views: {
            'linkView@applications/link': {
              templateUrl: 'gva/applications/views/docChoose.html',
              controller: 'DocChooseCtrl'
            }
          }
        })
        .state({
          name: 'applications/link/personChoose',
          title: 'Избери Персон',
          parent: 'applications/link',
          url: '/personChoose',
          views: {
            'linkView@applications/link': {
              templateUrl: 'gva/applications/views/personChoose.html',
              controller: 'PersonChooseCtrl'
            }
          }
        })
        .state({
          name: 'applications/link/personNew',
          title: 'Нов Персон',
          parent: 'applications/link',
          url: '/personNew',
          views: {
            'linkView@applications/link': {
              templateUrl: 'gva/applications/views/personNew.html',
              controller: 'PersonNewCtrl'
            }
          }
        })
        //applications edit
        .state({
          name: 'applications/edit',
          title: 'Редакция',
          parent: 'applications',
          url: '/:id',
          views: {
            'pageView@root': {
              templateUrl: 'gva/applications/views/applicationsEdit.html',
              controller: 'ApplicationsEditCtrl'
            }
          }
        })
        .state({
          name: 'applications/edit/case',
          title: 'Преписка',
          parent: 'applications/edit',
          url: '/case',
          views: {
            'detailView@applications/edit': {
              templateUrl: 'gva/applications/views/applicationsEditCase.html',
              controller: 'ApplicationsEditCaseCtrl'
            }
          }
        })
        .state({
          name: 'applications/edit/quals',
          title: 'Квалификации',
          parent: 'applications/edit',
          url: '/quals',
          views: {
            'detailView@applications/edit': {
              templateUrl: 'gva/applications/views/applicationsEditQuals.html',
              controller: 'ApplicationsEditQualsCtrl'
            }
          }
        })
        .state({
          name: 'applications/edit/licenses',
          title: 'Лицензи',
          parent: 'applications/edit',
          url: '/licenses',
          views: {
            'detailView@applications/edit': {
              templateUrl: 'gva/applications/views/applicationsEditLicenses.html',
              controller: 'ApplicationsEditLicensesCtrl'
            }
          }
        })
        .state({
          name: 'applications/edit/newfile',
          title: 'Нов файл',
          parent: 'applications/edit',
          url: '/newfile',
          views: {
            'detailView@applications/edit': {
              templateUrl: 'gva/applications/views/applicationsEditNewFile.html',
              controller: 'ApplicationsEditNewFileCtrl'
            }
          }
        })
        .state({
          name: 'applications/edit/addpart',
          title: 'Добавяне',
          parent: 'applications/edit',
          url: '/addpart',
          views: {
            'detailView@applications/edit': {
              templateUrl: 'gva/applications/views/applicationsEditAddPart.html',
              controller: 'ApplicationsEditAddPartCtrl'
            }
          }
        })
        .state({
          name: 'applications/edit/linkpart',
          title: 'Свързване',
          parent: 'applications/edit',
          url: '/linkpart',
          views: {
            'detailView@applications/edit': {
              templateUrl: 'gva/applications/views/applicationsEditLinkPart.html',
              controller: 'ApplicationsEditLinkPartCtrl'
            }
          }
        })


      ;
    }])
    .config(['$stateProvider', function ($stateProvider) {
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
        });
    }]);
}(angular));
