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
      name: 'gvaPersonDocumentEducation',
      templateUrl: 'gva/persons/forms/personDocumentEducation.html',
      controller: 'PersonDocumentEducationCtrl'
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
      templateUrl: 'gva/persons/forms/personApplication.html',
      controller: 'PersonApplicationCtrl'
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
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentTraining',
      templateUrl: 'gva/persons/forms/personDocumentTraining.html',
      controller: 'PersonDocumentTrainingCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonFlyingExperience',
      templateUrl: 'gva/persons/forms/personFlyingExperience.html',
      controller: 'PersonFlyingExperienceCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaRatingEdition',
      templateUrl: 'gva/persons/forms/personRatingEdition.html',
      controller: 'PersonRatingEditionCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaRating',
      templateUrl: 'gva/persons/forms/personRating.html'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
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
            'applicationsLinkView': {
              templateUrl: 'gva/applications/views/applicationsLinkCommon.html',
              controller: 'ApplicationsLinkCommonCtrl'
            }
          }
        })
        .state({
          name: 'applications/link/docChoose',
          title: 'Избор на документ',
          parent: 'applications/link',
          url: '/docChoose',
          views: {
            'applicationsLinkView': {
              templateUrl: 'gva/applications/views/docChoose.html',
              controller: 'DocChooseCtrl'
            }
          }
        })
        .state({
          name: 'applications/link/personChoose',
          title: 'Избер на заявител',
          parent: 'applications/link',
          url: '/personChoose',
          views: {
            'applicationsLinkView': {
              templateUrl: 'gva/applications/views/personChoose.html',
              controller: 'PersonChooseCtrl'
            }
          }
        })
        .state({
          name: 'applications/link/personNew',
          title: 'Нов заявител',
          parent: 'applications/link',
          url: '/personNew',
          views: {
            'applicationsLinkView': {
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
          title: 'Нов документ',
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
          name: 'persons.documentEducations',
          title: 'Образования',
          parent: 'persons.view',
          url: '/documentEducations',
          'abstract': true
        })
        .state({
          name: 'persons.documentEducations.search',
          parent: 'persons.documentEducations',
          url: '',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/documentEducationsSearch.html',
              controller: 'DocumentEducationsSearchCtrl'
            }
          }
        })
        .state({
          name: 'persons.documentEducations.new',
          title: 'Ново образование',
          parent: 'persons.documentEducations',
          url: '/new',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/documentEducationsNew.html',
              controller: 'DocumentEducationsNewCtrl'
            }
          }
        })
        .state({
          name: 'persons.documentEducations.edit',
          title: 'Редакция на образование',
          parent: 'persons.documentEducations',
          url: '/:ind',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/documentEducationsEdit.html',
              controller: 'DocumentEducationsEditCtrl'
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
        })
        .state({
          name: 'persons.documentTrainings',
          title: 'Обучение',
          parent: 'persons.view',
          url: '/documentTrainings',
          'abstract': true
        })
        .state({
          name: 'persons.documentTrainings.search',
          parent: 'persons.documentTrainings',
          url: '',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/documentTrainingsSearch.html',
              controller: 'DocumentTrainingsSearchCtrl'
            }
          }
        })
        .state({
          name: 'persons.documentTrainings.new',
          title: 'Нов документ за самоличност',
          parent: 'persons.documentTrainings',
          url: '/new',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/documentTrainingsNew.html',
              controller: 'DocumentTrainingsNewCtrl'
            }
          }
        })
        .state({
          name: 'persons.documentTrainings.edit',
          title: 'Редакция на документ за самоличност',
          parent: 'persons.documentTrainings',
          url: '/:ind',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/documentTrainingsEdit.html',
              controller: 'DocumentTrainingsEditCtrl'
            }
          }
        })
        .state({
          name: 'persons.flyingExperiences',
          title: 'Летателен/практически опит',
          parent: 'persons.view',
          url: '/flyingExperiences',
          'abstract': true
        })
        .state({
          name: 'persons.flyingExperiences.search',
          parent: 'persons.flyingExperiences',
          url: '',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/flyingExperiencesSearch.html',
              controller: 'FlyingExperiencesSearchCtrl'
            }
          }
        })
        .state({
          name: 'persons.flyingExperiences.new',
          title: 'Нов летателен/практически опит',
          parent: 'persons.flyingExperiences',
          url: '/new',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/flyingExperiencesNew.html',
              controller: 'FlyingExperiencesNewCtrl'
            }
          }
        })
        .state({
          name: 'persons.flyingExperiences.edit',
          title: 'Редакция на летателен/практически опит',
          parent: 'persons.flyingExperiences',
          url: '/:ind',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/flyingExperiencesEdit.html',
              controller: 'FlyingExperiencesEditCtrl'
            }
          }
        })
        .state({
          name: 'persons.inventory',
          parent: 'persons.view',
          url: '/inventory',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/inventorySearch.html',
              controller: 'InventorySearchCtrl'
            }
          }
        })
        .state({
          name: 'persons.ratings',
          title: 'Класове',
          parent: 'persons.view',
          url: '/ratings',
          'abstract': true
        })
        .state({
          name: 'persons.ratings.search',
          parent: 'persons.ratings',
          url: '',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/ratingsSearch.html',
              controller: 'RatingsSearchCtrl'
            }
          }
        })
        .state({
          name: 'persons.ratings.new',
          title: 'Нов клас',
          parent: 'persons.ratings',
          url: '/new',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/ratingsNew.html',
              controller: 'RatingsNewCtrl'
            }
          }
        })
        .state({
          name: 'persons.editions',
          title: 'Вписвания/Потвърждения',
          parent: 'persons.ratings',
          url: '/:ind/editions',
          'abstract': true
        })
        .state({
          name: 'persons.editions.search',
          parent: 'persons.editions',
          url: '',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/editionsSearch.html',
              controller: 'ЕditionsSearchCtrl'
            }
          }
        })
        .state({
          name: 'persons.editions.new',
          title: 'Ново вписване/потвърждение',
          parent: 'persons.editions',
          url: '/new',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/editionsNew.html',
              controller: 'EditionsNewCtrl'
            }
          }
        })
        .state({
          name: 'persons.editions.edit',
          title: 'Редакция на вписване/потвърждение',
          parent: 'persons.editions',
          url: '/:childInd',
          views: {
            'detailView@persons.view': {
              templateUrl: 'gva/persons/views/editionsEdit.html',
              controller: 'EditionsEditCtrl'
            }
          }
        });
    }]);
}(angular));
