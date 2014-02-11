/*jshint maxlen:false*/
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
      templateUrl: 'gva/persons/forms/personDocumentId.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentEducation',
      templateUrl: 'gva/persons/forms/personDocumentEducation.html'
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
      templateUrl: 'gva/persons/forms/personDocumentEmployment.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentCheck',
      templateUrl: 'gva/persons/forms/personDocumentCheck.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentTraining',
      templateUrl: 'gva/persons/forms/personDocumentTraining.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonFlyingExperience',
      templateUrl: 'gva/persons/forms/personFlyingExperience.html'
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
      .state(['root.applications'                     , '/applications'                                                                                                                                                                                                 ])
      .state(['root.applications.search'              , '?fromDate&toDate&lin&regUri'                                                              , ['@root'                  , 'gva/applications/views/applicationsSearch.html'      , 'ApplicationsSearchCtrl'      ]])
      .state(['root.applications.new'                 , '/new'                                                                                     , ['@root'                  , 'gva/applications/views/applicationsNew.html'         , 'ApplicationsNewCtrl'         ]])
      .state(['root.applications.new.doc'             , '/doc'                                                                                     , ['@root.applications.new' , 'gva/applications/views/applicationsNewDoc.html'      , 'ApplicationsNewDocCtrl'      ]])
      .state(['root.applications.new.personChoose'    , '/personChoose?exact&lin&uin&names&licences&ratings&organization'                          , ['@root.applications.new' , 'gva/applications/views/personChoose.html'            , 'PersonChooseCtrl'            ]])
      .state(['root.applications.new.personNew'       , '/personNew'                                                                               , ['@root.applications.new' , 'gva/applications/views/personNew.html'               , 'PersonNewCtrl'               ]])
      .state(['root.applications.link'                , '/link'                                                                                    , ['@root'                  , 'gva/applications/views/applicationsLink.html'        , 'ApplicationsLinkCtrl'        ]])
      .state(['root.applications.link.common'         , '/common'                                                                                  , ['@root.applications.link', 'gva/applications/views/applicationsLinkCommon.html'  , 'ApplicationsLinkCommonCtrl'  ]])
      .state(['root.applications.link.docChoose'      , '/docChoose?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units&docIds&hasLot', ['@root.applications.link', 'gva/applications/views/docChoose.html'               , 'DocChooseCtrl'               ]])
      .state(['root.applications.link.personChoose'   , '/personChoose'                                                                            , ['@root.applications.link', 'gva/applications/views/personChoose.html'            , 'PersonChooseCtrl'            ]])
      .state(['root.applications.link.personNew'      , '/personNew'                                                                               , ['@root.applications.link', 'gva/applications/views/personNew.html'               , 'PersonNewCtrl'               ]])
      .state(['root.applications.edit'                , '/:id'                                                                                     , ['@root'                  , 'gva/applications/views/applicationsEdit.html'        , 'ApplicationsEditCtrl'        ]])
      .state(['root.applications.edit.case'           , '/case'                                                                                    , ['@root.applications.edit', 'gva/applications/views/applicationsEditCase.html'    , 'ApplicationsEditCaseCtrl'    ]])
      .state(['root.applications.edit.quals'          , '/quals'                                                                                   , ['@root.applications.edit', 'gva/applications/views/applicationsEditQuals.html'   , 'ApplicationsEditQualsCtrl'   ]])
      .state(['root.applications.edit.licenses'       , '/licenses'                                                                                , ['@root.applications.edit', 'gva/applications/views/applicationsEditLicenses.html', 'ApplicationsEditLicensesCtrl']])
      .state(['root.applications.edit.newfile'        , '/newfile'                                                                                 , ['@root.applications.edit', 'gva/applications/views/applicationsEditNewFile.html' , 'ApplicationsEditNewFileCtrl' ]])
      .state(['root.applications.edit.addpart'        , '/addpart'                                                                                 , ['@root.applications.edit', 'gva/applications/views/applicationsEditAddPart.html' , 'ApplicationsEditAddPartCtrl' ]])
      .state(['root.applications.edit.linkpart'       , '/linkpart'                                                                                , ['@root.applications.edit', 'gva/applications/views/applicationsEditLinkPart.html', 'ApplicationsEditLinkPartCtrl']]);
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.persons'                               , '/persons?exact&lin&uin&names&licences&ratings&organization'                                                                      ])
      .state(['root.persons.search'                        , ''                   , ['@root'             , 'gva/persons/views/personsSearch.html'            , 'PersonsSearchCtrl'            ]])
      .state(['root.persons.new'                           , '/new'               , ['@root'             , 'gva/persons/views/personsNew.html'               , 'PersonsNewCtrl'               ]])
      .state(['root.persons.view'                          , '/:id'               , ['@root'             , 'gva/persons/views/personsView.html'              , 'PersonsViewCtrl'              ]])
      .state(['root.persons.view.edit'                     , '/personData'        , ['@root'             , 'gva/persons/views/personDataEdit.html'           , 'PersonDataEditCtrl'           ]])
      .state(['root.persons.view.addresses'                , '/addresses'                                                                                                                      ])
      .state(['root.persons.view.addresses.search'         , ''                   , ['@root.persons.view', 'gva/persons/views/addressesSearch.html'          , 'AddressesSearchCtrl'          ]])
      .state(['root.persons.view.addresses.new'            , '/new'               , ['@root.persons.view', 'gva/persons/views/addressesNew.html'             , 'AddressesNewCtrl'             ]])
      .state(['root.persons.view.addresses.edit'           , '/:ind'              , ['@root.persons.view', 'gva/persons/views/addressesEdit.html'            , 'AddressesEditCtrl'            ]])
      .state(['root.persons.view.statuses'                 , '/statuses'                                                                                                                       ])
      .state(['root.persons.view.statuses.search'          , ''                   , ['@root.persons.view', 'gva/persons/views/statusesSearch.html'           , 'StatusesSearchCtrl'           ]])
      .state(['root.persons.view.statuses.new'             , '/new'               , ['@root.persons.view', 'gva/persons/views/statusesNew.html'              , 'StatusesNewCtrl'              ]])
      .state(['root.persons.view.statuses.edit'            , '/:ind'              , ['@root.persons.view', 'gva/persons/views/statusesEdit.html'             , 'StatusesEditCtrl'             ]])
      .state(['root.persons.view.documentIds'              , '/documentIds'                                                                                                                    ])
      .state(['root.persons.view.documentIds.search'       , ''                   , ['@root.persons.view', 'gva/persons/views/documentIdsSearch.html'        , 'DocumentIdsSearchCtrl'        ]])
      .state(['root.persons.view.documentIds.new'          , '/new'               , ['@root.persons.view', 'gva/persons/views/documentIdsNew.html'           , 'DocumentIdsNewCtrl'           ]])
      .state(['root.persons.view.documentIds.edit'         , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentIdsEdit.html'          , 'DocumentIdsEditCtrl'          ]])
      .state(['root.persons.view.documentEducations'       , '/documentEducations'                                                                                                             ])
      .state(['root.persons.view.documentEducations.search', ''                   , ['@root.persons.view', 'gva/persons/views/documentEducationsSearch.html' , 'DocumentEducationsSearchCtrl' ]])
      .state(['root.persons.view.documentEducations.new'   , '/new'               , ['@root.persons.view', 'gva/persons/views/documentEducationsNew.html'    , 'DocumentEducationsNewCtrl'    ]])
      .state(['root.persons.view.documentEducations.edit'  , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentEducationsEdit.html'   , 'DocumentEducationsEditCtrl'   ]])
      .state(['root.persons.view.licences'                 , '/licences'                                                                                                                       ])
      .state(['root.persons.view.licences.search'          , ''                   , ['@root.persons.view', 'gva/persons/views/licencesSearch.html'           , 'LicencesSearchCtrl'           ]])
      .state(['root.persons.view.checks'                   , '/checks'                                                                                                                         ])
      .state(['root.persons.view.checks.search'            , ''                   , ['@root.persons.view', 'gva/persons/views/documentChecksSearch.html'     , 'DocumentChecksSearchCtrl'     ]])
      .state(['root.persons.view.checks.new'               , '/new'               , ['@root.persons.view', 'gva/persons/views/documentChecksNew.html'        , 'DocumentChecksNewCtrl'        ]])
      .state(['root.persons.view.checks.edit'              , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentChecksEdit.html'       , 'DocumentChecksEditCtrl'       ]])
      .state(['root.persons.view.employments'              , '/employments'                                                                                                                    ])
      .state(['root.persons.view.employments.search'       , ''                   , ['@root.persons.view', 'gva/persons/views/documentEmploymentsSearch.html', 'DocumentEmploymentsSearchCtrl']])
      .state(['root.persons.view.employments.new'          , '/new'               , ['@root.persons.view', 'gva/persons/views/documentEmploymentsNew.html'   , 'DocumentEmploymentsNewCtrl'   ]])
      .state(['root.persons.view.employments.edit'         , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentEmploymentsEdit.html'  , 'DocumentEmploymentsEditCtrl'  ]])
      .state(['root.persons.view.medicals'                 , '/medicals'                                                                                                                       ])
      .state(['root.persons.view.medicals.search'          , ''                   , ['@root.persons.view', 'gva/persons/views/documentMedicalsSearch.html'   , 'DocumentMedicalsSearchCtrl'   ]])
      .state(['root.persons.view.medicals.new'             , '/new'               , ['@root.persons.view', 'gva/persons/views/documentMedicalsNew.html'      , 'DocumentMedicalsNewCtrl'      ]])
      .state(['root.persons.view.medicals.edit'            , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentMedicalsEdit.html'     , 'DocumentMedicalsEditCtrl'     ]])
      .state(['root.persons.view.documentTrainings'        , '/documentTrainings'                                                                                                              ])
      .state(['root.persons.view.documentTrainings.search' , ''                   , ['@root.persons.view', 'gva/persons/views/documentTrainingsSearch.html'  , 'DocumentTrainingsSearchCtrl'  ]])
      .state(['root.persons.view.documentTrainings.new'    , '/new'               , ['@root.persons.view', 'gva/persons/views/documentTrainingsNew.html'     , 'DocumentTrainingsNewCtrl'     ]])
      .state(['root.persons.view.documentTrainings.edit'   , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentTrainingsEdit.html'    , 'DocumentTrainingsEditCtrl'    ]])
      .state(['root.persons.view.flyingExperiences'        , '/flyingExperiences'                                                                                                              ])
      .state(['root.persons.view.flyingExperiences.search' , ''                   , ['@root.persons.view', 'gva/persons/views/flyingExperiencesSearch.html'  , 'FlyingExperiencesSearchCtrl'  ]])
      .state(['root.persons.view.flyingExperiences.new'    , '/new'               , ['@root.persons.view', 'gva/persons/views/flyingExperiencesNew.html'     , 'FlyingExperiencesNewCtrl'     ]])
      .state(['root.persons.view.flyingExperiences.edit'   , '/:ind'              , ['@root.persons.view', 'gva/persons/views/flyingExperiencesEdit.html'    , 'FlyingExperiencesEditCtrl'    ]])
      .state(['root.persons.view.inventory'                , '/inventory'         , ['@root.persons.view', 'gva/persons/views/inventorySearch.html'          , 'InventorySearchCtrl'          ]])
      .state(['root.persons.view.ratings'                  , '/ratings'                                                                                                                        ])
      .state(['root.persons.view.ratings.search'           , ''                   , ['@root.persons.view', 'gva/persons/views/ratingsSearch.html'            , 'RatingsSearchCtrl'            ]])
      .state(['root.persons.view.ratings.new'              , '/new'               , ['@root.persons.view', 'gva/persons/views/ratingsNew.html'               , 'RatingsNewCtrl'               ]])
      .state(['root.persons.view.editions'                 , '/:ind/editions'                                                                                                                  ])
      .state(['root.persons.view.editions.search'          , ''                   , ['@root.persons.view', 'gva/persons/views/editionsSearch.html'           , 'ЕditionsSearchCtrl'           ]])
      .state(['root.persons.view.editions.new'             , '/new'               , ['@root.persons.view', 'gva/persons/views/editionsNew.html'              , 'EditionsNewCtrl'              ]])
      .state(['root.persons.view.editions.edit'            , '/:childInd'         , ['@root.persons.view', 'gva/persons/views/editionsEdit.html'             , 'EditionsEditCtrl'             ]]);
  }]);
}(angular));
