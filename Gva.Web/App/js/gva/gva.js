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
      .state(['root.applications.new.personSelect'    , '/personSelect?exact&lin&uin&names&licences&ratings&organization'                          , ['@root'                  , 'gva/applications/views/applicationsPersonSelect.html', 'ApplicationsPersonSelectCtrl']])
      .state(['root.applications.new.personNew'       , '/personNew'                                                                               , ['@root'                  , 'gva/applications/views/applicationsPersonNew.html'   , 'ApplicationsPersonNewCtrl'   ]])
      .state(['root.applications.link'                , '/link'                                                                                    , ['@root'                  , 'gva/applications/views/applicationsLink.html'        , 'ApplicationsLinkCtrl'        ]])
      .state(['root.applications.link.docSelect'      , '/docSelect?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units&docIds&hasLot', ['@root'                  , 'gva/applications/views/applicationsDocSelect.html'   , 'ApplicationsDocSelectCtrl'   ]])
      .state(['root.applications.link.personSelect'   , '/personSelect?exact&lin&uin&names&licences&ratings&organization'                          , ['@root'                  , 'gva/applications/views/applicationsPersonSelect.html', 'ApplicationsPersonSelectCtrl']])
      .state(['root.applications.link.personNew'      , '/personNew'                                                                               , ['@root'                  , 'gva/applications/views/applicationsPersonNew.html'   , 'ApplicationsPersonNewCtrl'   ]])
      .state(['root.applications.edit'                , '/:id'                                                                                     , ['@root'                  , 'gva/applications/views/applicationsEdit.html'        , 'ApplicationsEditCtrl'        ]])
      .state(['root.applications.edit.case'           , '/case'                                                                                    , ['@root.applications.edit', 'gva/applications/views/applicationsEditCase.html'    , 'ApplicationsEditCaseCtrl'    ]])
      .state(['root.applications.edit.quals'          , '/quals'                                                                                   , ['@root.applications.edit', 'gva/applications/views/applicationsEditQuals.html'   , 'ApplicationsEditQualsCtrl'   ]])
      .state(['root.applications.edit.licenses'       , '/licenses'                                                                                , ['@root.applications.edit', 'gva/applications/views/applicationsEditLicenses.html', 'ApplicationsEditLicensesCtrl']])
      .state(['root.applications.edit.newFile'        , '/newFile?isLinkNew&currentDocId&docFileKey&docFileName'                                   , ['@root.applications.edit', 'gva/applications/views/applicationsEditNewFile.html' , 'ApplicationsEditNewFileCtrl' ]])
      .state(['root.applications.edit.addPart'        , '/addPart?isLinkNew&currentDocId&docFileKey&docFileName&docPartTypeAlias'                  , ['@root.applications.edit', 'gva/applications/views/applicationsEditAddPart.html' , 'ApplicationsEditAddPartCtrl' ]])
      .state(['root.applications.edit.linkPart'       , '/linkPart?currentDocId&docFileKey&docFileName'                                            , ['@root.applications.edit', 'gva/applications/views/applicationsEditLinkPart.html', 'ApplicationsEditLinkPartCtrl']]);
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.persons'                               , '/persons?exact&lin&uin&names&licences&ratings&organization'                                                                             ])
      .state(['root.persons.search'                        , ''                   , ['@root'             , 'gva/persons/views/personsSearch.html'                    , 'PersonsSearchCtrl'            ]])
      .state(['root.persons.new'                           , '/new'               , ['@root'             , 'gva/persons/views/personsNew.html'                       , 'PersonsNewCtrl'               ]])
      .state(['root.persons.view'                          , '/:id'               , ['@root'             , 'gva/persons/views/personsView.html'                      , 'PersonsViewCtrl'              ]])
      .state(['root.persons.view.edit'                     , '/personData'        , ['@root'             , 'gva/persons/views/personData/personDataEdit.html'        , 'PersonDataEditCtrl'           ]])
      .state(['root.persons.view.addresses'                , '/addresses'                                                                                                                             ])
      .state(['root.persons.view.addresses.search'         , ''                   , ['@root.persons.view', 'gva/persons/views/addresses/addrSearch.html'             , 'AddressesSearchCtrl'          ]])
      .state(['root.persons.view.addresses.new'            , '/new'               , ['@root.persons.view', 'gva/persons/views/addresses/addrNew.html'                , 'AddressesNewCtrl'             ]])
      .state(['root.persons.view.addresses.edit'           , '/:ind'              , ['@root.persons.view', 'gva/persons/views/addresses/addrEdit.html'               , 'AddressesEditCtrl'            ]])
      .state(['root.persons.view.statuses'                 , '/statuses'                                                                                                                              ])
      .state(['root.persons.view.statuses.search'          , ''                   , ['@root.persons.view', 'gva/persons/views/statuses/statusesSearch.html'          , 'StatusesSearchCtrl'           ]])
      .state(['root.persons.view.statuses.new'             , '/new'               , ['@root.persons.view', 'gva/persons/views/statuses/statusesNew.html'             , 'StatusesNewCtrl'              ]])
      .state(['root.persons.view.statuses.edit'            , '/:ind'              , ['@root.persons.view', 'gva/persons/views/statuses/statusesEdit.html'            , 'StatusesEditCtrl'             ]])
      .state(['root.persons.view.documentIds'              , '/documentIds'                                                                                                                           ])
      .state(['root.persons.view.documentIds.search'       , ''                   , ['@root.persons.view', 'gva/persons/views/documentIds/idsSearch.html'            , 'DocumentIdsSearchCtrl'        ]])
      .state(['root.persons.view.documentIds.new'          , '/new'               , ['@root.persons.view', 'gva/persons/views/documentIds/idsNew.html'               , 'DocumentIdsNewCtrl'           ]])
      .state(['root.persons.view.documentIds.edit'         , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentIds/idsEdit.html'              , 'DocumentIdsEditCtrl'          ]])
      .state(['root.persons.view.documentEducations'       , '/documentEducations'                                                                                                                    ])
      .state(['root.persons.view.documentEducations.search', ''                   , ['@root.persons.view', 'gva/persons/views/documentEducations/edusSearch.html'    , 'DocumentEducationsSearchCtrl' ]])
      .state(['root.persons.view.documentEducations.new'   , '/new'               , ['@root.persons.view', 'gva/persons/views/documentEducations/edusNew.html'       , 'DocumentEducationsNewCtrl'    ]])
      .state(['root.persons.view.documentEducations.edit'  , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentEducations/edusEdit.html'      , 'DocumentEducationsEditCtrl'   ]])
      .state(['root.persons.view.licences'                 , '/licences'                                                                                                                              ])
      .state(['root.persons.view.licences.search'          , ''                   , ['@root.persons.view', 'gva/persons/views/licences/licencesSearch.html'          , 'LicencesSearchCtrl'           ]])
      .state(['root.persons.view.checks'                   , '/checks'                                                                                                                                ])
      .state(['root.persons.view.checks.search'            , ''                   , ['@root.persons.view', 'gva/persons/views/documentChecks/checksSearch.html'      , 'DocumentChecksSearchCtrl'     ]])
      .state(['root.persons.view.checks.new'               , '/new'               , ['@root.persons.view', 'gva/persons/views/documentChecks/checksNew.html'         , 'DocumentChecksNewCtrl'        ]])
      .state(['root.persons.view.checks.edit'              , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentChecks/checksEdit.html'        , 'DocumentChecksEditCtrl'       ]])
      .state(['root.persons.view.employments'              , '/employments'                                                                                                                           ])
      .state(['root.persons.view.employments.search'       , ''                   , ['@root.persons.view', 'gva/persons/views/documentEmployments/emplsSearch.html'  , 'DocumentEmploymentsSearchCtrl']])
      .state(['root.persons.view.employments.new'          , '/new'               , ['@root.persons.view', 'gva/persons/views/documentEmployments/emplsNew.html'     , 'DocumentEmploymentsNewCtrl'   ]])
      .state(['root.persons.view.employments.edit'         , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentEmployments/emplsEdit.html'    , 'DocumentEmploymentsEditCtrl'  ]])
      .state(['root.persons.view.medicals'                 , '/medicals'                                                                                                                              ])
      .state(['root.persons.view.medicals.search'          , ''                   , ['@root.persons.view', 'gva/persons/views/documentMedicals/medsSearch.html'      , 'DocumentMedicalsSearchCtrl'   ]])
      .state(['root.persons.view.medicals.new'             , '/new'               , ['@root.persons.view', 'gva/persons/views/documentMedicals/medsNew.html'         , 'DocumentMedicalsNewCtrl'      ]])
      .state(['root.persons.view.medicals.edit'            , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentMedicals/medsEdit.html'        , 'DocumentMedicalsEditCtrl'     ]])
      .state(['root.persons.view.documentTrainings'        , '/documentTrainings'                                                                                                                     ])
      .state(['root.persons.view.documentTrainings.search' , ''                   , ['@root.persons.view', 'gva/persons/views/documentTrainings/trainingsSearch.html', 'DocumentTrainingsSearchCtrl'  ]])
      .state(['root.persons.view.documentTrainings.new'    , '/new'               , ['@root.persons.view', 'gva/persons/views/documentTrainings/trainingsNew.html'   , 'DocumentTrainingsNewCtrl'     ]])
      .state(['root.persons.view.documentTrainings.edit'   , '/:ind'              , ['@root.persons.view', 'gva/persons/views/documentTrainings/trainingsEdit.html'  , 'DocumentTrainingsEditCtrl'    ]])
      .state(['root.persons.view.flyingExperiences'        , '/flyingExperiences'                                                                                                                     ])
      .state(['root.persons.view.flyingExperiences.search' , ''                   , ['@root.persons.view', 'gva/persons/views/flyingExperiences/flyExpsSearch.html'  , 'FlyingExperiencesSearchCtrl'  ]])
      .state(['root.persons.view.flyingExperiences.new'    , '/new'               , ['@root.persons.view', 'gva/persons/views/flyingExperiences/flyExpsNew.html'     , 'FlyingExperiencesNewCtrl'     ]])
      .state(['root.persons.view.flyingExperiences.edit'   , '/:ind'              , ['@root.persons.view', 'gva/persons/views/flyingExperiences/flyExpsEdit.html'    , 'FlyingExperiencesEditCtrl'    ]])
      .state(['root.persons.view.inventory'                , '/inventory'         , ['@root.persons.view', 'gva/persons/views/inventory/inventorySearch.html'        , 'InventorySearchCtrl'          ]])
      .state(['root.persons.view.ratings'                  , '/ratings'                                                                                                                               ])
      .state(['root.persons.view.ratings.search'           , ''                   , ['@root.persons.view', 'gva/persons/views/ratings/ratingsSearch.html'            , 'RatingsSearchCtrl'            ]])
      .state(['root.persons.view.ratings.new'              , '/new'               , ['@root.persons.view', 'gva/persons/views/ratings/ratingsNew.html'               , 'RatingsNewCtrl'               ]])
      .state(['root.persons.view.editions'                 , '/:ind/editions'                                                                                                                         ])
      .state(['root.persons.view.editions.search'          , ''                   , ['@root.persons.view', 'gva/persons/views/ratings/editions/editionsSearch.html'  , 'ЕditionsSearchCtrl'           ]])
      .state(['root.persons.view.editions.new'             , '/new'               , ['@root.persons.view', 'gva/persons/views/ratings/editions/editionsNew.html'     , 'EditionsNewCtrl'              ]])
      .state(['root.persons.view.editions.edit'            , '/:childInd'         , ['@root.persons.view', 'gva/persons/views/ratings/editions/editionsEdit.html'    , 'EditionsEditCtrl'             ]]);
  }]);
}(angular));
