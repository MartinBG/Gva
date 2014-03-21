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
    'l10n-tools',
    'scrollto'
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
      templateUrl: 'gva/persons/forms/personScannedDocument.html',
      controller: 'PersonScannedDocCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonApplication',
      templateUrl: 'gva/persons/forms/personApplication.html',
      controller: 'PersonApplicationCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentMedical',
      templateUrl: 'gva/persons/forms/personDocumentMedical.html'
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
      name: 'gvaPersonDocumentOther',
      templateUrl: 'gva/persons/forms/personDocumentOther.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonFlyingExperience',
      templateUrl: 'gva/persons/forms/personFlyingExperience.html'
    });
    scaffoldingProvider.form({
      name: 'gvaRatingEdition',
      templateUrl: 'gva/persons/forms/personRatingEdition.html'
    });
    scaffoldingProvider.form({
      name: 'gvaRating',
      templateUrl: 'gva/persons/forms/personRating.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentApplication',
      templateUrl: 'gva/persons/forms/personDocumentApplication.html',
      controller: 'PersonDocumentApplicationCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRegisterView',
      templateUrl: 'gva/aircrafts/forms/aircraftCertRegView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertSmodView',
      templateUrl: 'gva/aircrafts/forms/aircraftCertSmodView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertMarkView',
      templateUrl: 'gva/aircrafts/forms/aircraftCertMarkView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertAirworthinessView',
      templateUrl: 'gva/aircrafts/forms/aircraftCertAirworthinessView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertNoiseView',
      templateUrl: 'gva/aircrafts/forms/aircraftCertNoiseView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertPermittoflyView',
      templateUrl: 'gva/aircrafts/forms/aircraftCertPermitToFlyView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRadioView',
      templateUrl: 'gva/aircrafts/forms/aircraftCertRadioView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertSmod',
      templateUrl: 'gva/aircrafts/forms/aircraftCertSmod.html',
      controller: 'AircraftCertSmodCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertMark',
      templateUrl: 'gva/aircrafts/forms/aircraftCertMark.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertAirworthiness',
      templateUrl: 'gva/aircrafts/forms/aircraftCertAirworthiness.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertAirworthinessFm',
      templateUrl: 'gva/aircrafts/forms/aircraftCertAirworthinessFM.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertPermit',
      templateUrl: 'gva/aircrafts/forms/aircraftCertPermitToFly.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertNoise',
      templateUrl: 'gva/aircrafts/forms/aircraftCertNoise.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertNoiseFm',
      templateUrl: 'gva/aircrafts/forms/aircraftCertNoiseFM.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRadio',
      templateUrl: 'gva/aircrafts/forms/aircraftCertRadio.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertReg',
      templateUrl: 'gva/aircrafts/forms/aircraftCertReg.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRegFm',
      templateUrl: 'gva/aircrafts/forms/aircraftCertRegFM.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftData',
      templateUrl: 'gva/aircrafts/forms/aircraftData.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDataApex',
      templateUrl: 'gva/aircrafts/forms/aircraftDataApex.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentDebt',
      templateUrl: 'gva/aircrafts/forms/aircraftDocumentDebt.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentDebtFm',
      templateUrl: 'gva/aircrafts/forms/aircraftDocumentDebtFM.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentOther',
      templateUrl: 'gva/aircrafts/forms/aircraftDocumentOther.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftInspection',
      templateUrl: 'gva/aircrafts/forms/aircraftInspection.html',
      controller: 'AircraftInspectionCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftAuditDetail',
      templateUrl: 'gva/aircrafts/forms/aircraftAuditDetail.html',
      controller: 'AircraftAuditDetailCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentOccurrence',
      templateUrl: 'gva/aircrafts/forms/aircraftDocumentOccurrence.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftMaintenance',
      templateUrl: 'gva/aircrafts/forms/aircrafttMaintenance.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentOwner',
      templateUrl: 'gva/aircrafts/forms/aircraftDocumentOwner.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftPart',
      templateUrl: 'gva/aircrafts/forms/aircraftPart.html'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.applications'                                  , '/applications'                                                                                                                                                                                                                            ])
      .state(['root.applications.search'                           , '?fromDate&toDate&lin'                                                                                                , ['@root'                  , 'gva/applications/views/applicationsSearch.html'      , 'ApplicationsSearchCtrl'      ]])
      .state(['root.applications.new'                              , '/new'                                                                                                                , ['@root'                  , 'gva/applications/views/applicationsNew.html'         , 'ApplicationsNewCtrl'         ]])
      .state(['root.applications.new.personSelect'                 , '/personSelect?exact&lin&uin&names&licences&ratings&organization'                                                     , ['@root'                  , 'gva/applications/views/applicationsPersonSelect.html', 'ApplicationsPersonSelectCtrl']])
      .state(['root.applications.new.personNew'                    , '/personNew'                                                                                                          , ['@root'                  , 'gva/applications/views/applicationsPersonNew.html'   , 'ApplicationsPersonNewCtrl'   ]])
      .state(['root.applications.link'                             , '/link'                                                                                                               , ['@root'                  , 'gva/applications/views/applicationsLink.html'        , 'ApplicationsLinkCtrl'        ]])
      .state(['root.applications.link.docSelect'                   , '/docSelect?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units&docIds&hasLot'                           , ['@root'                  , 'gva/applications/views/applicationsDocSelect.html'   , 'ApplicationsDocSelectCtrl'   ]])
      .state(['root.applications.link.personSelect'                , '/personSelect?exact&lin&uin&names&licences&ratings&organization'                                                     , ['@root'                  , 'gva/applications/views/applicationsPersonSelect.html', 'ApplicationsPersonSelectCtrl']])
      .state(['root.applications.link.personNew'                   , '/personNew'                                                                                                          , ['@root'                  , 'gva/applications/views/applicationsPersonNew.html'   , 'ApplicationsPersonNewCtrl'   ]])
      .state(['root.applications.edit'                             , '/:id'                                                                                                                , ['@root'                  , 'gva/applications/views/applicationsEdit.html'        , 'ApplicationsEditCtrl'        ]])
      .state(['root.applications.edit.case'                        , '/case'                                                                                                               , ['@root.applications.edit', 'gva/applications/views/applicationsEditCase.html'    , 'ApplicationsEditCaseCtrl'    ]])
      .state(['root.applications.edit.quals'                       , '/quals'                                                                                                              , ['@root.applications.edit', 'gva/applications/views/applicationsEditQuals.html'   , 'ApplicationsEditQualsCtrl'   ]])
      .state(['root.applications.edit.licenses'                    , '/licenses'                                                                                                           , ['@root.applications.edit', 'gva/applications/views/applicationsEditLicenses.html', 'ApplicationsEditLicensesCtrl']])
      .state(['root.applications.edit.case.newFile'                , '/newFile?isLinkNew&currentDocId&docFileKey&docFileName'                                                              , ['@root.applications.edit', 'gva/applications/views/applicationsEditNewFile.html' , 'ApplicationsEditNewFileCtrl' ]])
      .state(['root.applications.edit.case.addPart'                , '/addPart?isLinkNew&currentDocId&docFileKey&docFileName&docPartTypeAlias'                                             , ['@root.applications.edit', 'gva/applications/views/applicationsEditAddPart.html' , 'ApplicationsEditAddPartCtrl' ]])
      .state(['root.applications.edit.case.addPart.choosePublisher', '/choosepublisher?text&publisherTypeAlias'                                                                            , ['@root.applications.edit', 'gva/persons/views/publishers/choosePublisher.html'   , 'ChoosePublisherCtrl'         ]])
      .state(['root.applications.edit.case.linkPart'               , '/linkPart?currentDocId&docFileKey&docFileName'                                                                       , ['@root.applications.edit', 'gva/applications/views/applicationsEditLinkPart.html', 'ApplicationsEditLinkPartCtrl']]);
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.persons'                                            , '/persons?exact&lin&uin&names&licences&ratings&organization'                                                                                                              ])
      .state(['root.persons.search'                                     , ''                                           , ['@root'             , 'gva/persons/views/personsSearch.html'                             , 'PersonsSearchCtrl'            ]])
      .state(['root.persons.new'                                        , '/new'                                       , ['@root'             , 'gva/persons/views/personsNew.html'                                , 'PersonsNewCtrl'               ]])
      .state(['root.persons.view'                                       , '/:id'                                       , ['@root'             , 'gva/persons/views/personsView.html'                               , 'PersonsViewCtrl'              ]])
      .state(['root.persons.view.edit'                                  , '/personData'                                , ['@root'             , 'gva/persons/views/personData/personDataEdit.html'                 , 'PersonDataEditCtrl'           ]])
      .state(['root.persons.view.addresses'                             , '/addresses'                                                                                                                                                              ])
      .state(['root.persons.view.addresses.search'                      , ''                                           , ['@root.persons.view', 'gva/persons/views/addresses/addrSearch.html'                      , 'AddressesSearchCtrl'          ]])
      .state(['root.persons.view.addresses.new'                         , '/new'                                       , ['@root.persons.view', 'gva/persons/views/addresses/addrNew.html'                         , 'AddressesNewCtrl'             ]])
      .state(['root.persons.view.addresses.edit'                        , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/addresses/addrEdit.html'                        , 'AddressesEditCtrl'            ]])
      .state(['root.persons.view.statuses'                              , '/statuses'                                                                                                                                                               ])
      .state(['root.persons.view.statuses.search'                       , ''                                           , ['@root.persons.view', 'gva/persons/views/statuses/statusesSearch.html'                   , 'StatusesSearchCtrl'           ]])
      .state(['root.persons.view.statuses.new'                          , '/new'                                       , ['@root.persons.view', 'gva/persons/views/statuses/statusesNew.html'                      , 'StatusesNewCtrl'              ]])
      .state(['root.persons.view.statuses.edit'                         , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/statuses/statusesEdit.html'                     , 'StatusesEditCtrl'             ]])
      .state(['root.persons.view.documentIds'                           , '/documentIds'                                                                                                                                                            ])
      .state(['root.persons.view.documentIds.search'                    , ''                                           , ['@root.persons.view', 'gva/persons/views/documentIds/idsSearch.html'                     , 'DocumentIdsSearchCtrl'        ]])
      .state(['root.persons.view.documentIds.new'                       , '/new'                                       , ['@root.persons.view', 'gva/persons/views/documentIds/idsNew.html'                        , 'DocumentIdsNewCtrl'           ]])
      .state(['root.persons.view.documentIds.edit'                      , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/documentIds/idsEdit.html'                       , 'DocumentIdsEditCtrl'          ]])
      .state(['root.persons.view.documentEducations'                    , '/documentEducations'                                                                                                                                                     ])
      .state(['root.persons.view.documentEducations.search'             , ''                                           , ['@root.persons.view', 'gva/persons/views/documentEducations/edusSearch.html'             , 'DocumentEducationsSearchCtrl' ]])
      .state(['root.persons.view.documentEducations.new'                , '/new'                                       , ['@root.persons.view', 'gva/persons/views/documentEducations/edusNew.html'                , 'DocumentEducationsNewCtrl'    ]])
      .state(['root.persons.view.documentEducations.edit'               , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/documentEducations/edusEdit.html'               , 'DocumentEducationsEditCtrl'   ]])
      .state(['root.persons.view.licences'                              , '/licences'                                                                                                                                                               ])
      .state(['root.persons.view.licences.search'                       , ''                                           , ['@root.persons.view', 'gva/persons/views/licences/licencesSearch.html'                   , 'LicencesSearchCtrl'           ]])
      .state(['root.persons.view.checks'                                , '/checks'                                                                                                                                                                 ])
      .state(['root.persons.view.checks.search'                         , ''                                           , ['@root.persons.view', 'gva/persons/views/documentChecks/checksSearch.html'               , 'DocumentChecksSearchCtrl'     ]])
      .state(['root.persons.view.checks.new'                            , '/new'                                       , ['@root.persons.view', 'gva/persons/views/documentChecks/checksNew.html'                  , 'DocumentChecksNewCtrl'        ]])
      .state(['root.persons.view.checks.new.choosePublisher'            , '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'gva/persons/views/publishers/choosePublisher.html'                , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.checks.edit'                           , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/documentChecks/checksEdit.html'                 , 'DocumentChecksEditCtrl'       ]])
      .state(['root.persons.view.checks.edit.choosePublisher'           , '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'gva/persons/views/publishers/choosePublisher.html'                , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.employments'                           , '/employments'                                                                                                                                                            ])
      .state(['root.persons.view.employments.search'                    , ''                                           , ['@root.persons.view', 'gva/persons/views/documentEmployments/emplsSearch.html'           , 'DocumentEmploymentsSearchCtrl']])
      .state(['root.persons.view.employments.new'                       , '/new'                                       , ['@root.persons.view', 'gva/persons/views/documentEmployments/emplsNew.html'              , 'DocumentEmploymentsNewCtrl'   ]])
      .state(['root.persons.view.employments.edit'                      , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/documentEmployments/emplsEdit.html'             , 'DocumentEmploymentsEditCtrl'  ]])
      .state(['root.persons.view.medicals'                              , '/medicals'                                                                                                                                                               ])
      .state(['root.persons.view.medicals.search'                       , ''                                           , ['@root.persons.view', 'gva/persons/views/documentMedicals/medsSearch.html'               , 'DocumentMedicalsSearchCtrl'   ]])
      .state(['root.persons.view.medicals.new'                          , '/new'                                       , ['@root.persons.view', 'gva/persons/views/documentMedicals/medsNew.html'                  , 'DocumentMedicalsNewCtrl'      ]])
      .state(['root.persons.view.medicals.edit'                         , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/documentMedicals/medsEdit.html'                 , 'DocumentMedicalsEditCtrl'     ]])
      .state(['root.persons.view.documentTrainings'                     , '/documentTrainings'                                                                                                                                                      ])
      .state(['root.persons.view.documentTrainings.search'              , ''                                           , ['@root.persons.view', 'gva/persons/views/documentTrainings/trainingsSearch.html'         , 'DocumentTrainingsSearchCtrl'  ]])
      .state(['root.persons.view.documentTrainings.new'                 , '/new'                                       , ['@root.persons.view', 'gva/persons/views/documentTrainings/trainingsNew.html'            , 'DocumentTrainingsNewCtrl'     ]])
      .state(['root.persons.view.documentTrainings.new.choosePublisher' , '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'gva/persons/views/publishers/choosePublisher.html'                , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.documentTrainings.edit'                , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/documentTrainings/trainingsEdit.html'           , 'DocumentTrainingsEditCtrl'    ]])
      .state(['root.persons.view.documentTrainings.edit.choosePublisher', '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'gva/persons/views/publishers/choosePublisher.html'                , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.flyingExperiences'                     , '/flyingExperiences'                                                                                                                                                      ])
      .state(['root.persons.view.flyingExperiences.search'              , ''                                           , ['@root.persons.view', 'gva/persons/views/flyingExperiences/flyExpsSearch.html'           , 'FlyingExperiencesSearchCtrl'  ]])
      .state(['root.persons.view.flyingExperiences.new'                 , '/new'                                       , ['@root.persons.view', 'gva/persons/views/flyingExperiences/flyExpsNew.html'              , 'FlyingExperiencesNewCtrl'     ]])
      .state(['root.persons.view.flyingExperiences.edit'                , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/flyingExperiences/flyExpsEdit.html'             , 'FlyingExperiencesEditCtrl'    ]])
      .state(['root.persons.view.inventory'                             , '/inventory'                                 , ['@root.persons.view', 'gva/persons/views/inventory/inventorySearch.html'                 , 'InventorySearchCtrl'          ]])
      .state(['root.persons.view.ratings'                               , '/ratings'                                                                                                                                                                ])
      .state(['root.persons.view.ratings.search'                        , ''                                           , ['@root.persons.view', 'gva/persons/views/ratings/ratingsSearch.html'                     , 'RatingsSearchCtrl'            ]])
      .state(['root.persons.view.ratings.new'                           , '/new'                                       , ['@root.persons.view', 'gva/persons/views/ratings/ratingsNew.html'                        , 'RatingsNewCtrl'               ]])
      .state(['root.persons.view.ratings.editions'                      , '/:ind/editions'                                                                                                                                                          ])
      .state(['root.persons.view.ratings.editions.search'               , ''                                           , ['@root.persons.view', 'gva/persons/views/ratings/editions/editionsSearch.html'           , 'EditionsSearchCtrl'           ]])
      .state(['root.persons.view.ratings.editions.new'                  , '/new'                                       , ['@root.persons.view', 'gva/persons/views/ratings/editions/editionsNew.html'              , 'EditionsNewCtrl'              ]])
      .state(['root.persons.view.ratings.editions.edit'                 , '/:childInd'                                 , ['@root.persons.view', 'gva/persons/views/ratings/editions/editionsEdit.html'             , 'EditionsEditCtrl'             ]])
      .state(['root.persons.view.documentOthers'                        , '/documentOthers'                                                                                                                                                          ])
      .state(['root.persons.view.documentOthers.search'                 , ''                                           , ['@root.persons.view', 'gva/persons/views/documentOthers/othersSearch.html'               , 'DocumentOthersSearchCtrl'     ]])
      .state(['root.persons.view.documentOthers.new'                    , '/new'                                       , ['@root.persons.view', 'gva/persons/views/documentOthers/othersNew.html'                  , 'DocumentOthersNewCtrl'        ]])
      .state(['root.persons.view.documentOthers.new.choosePublisher'    , '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'gva/persons/views/publishers/choosePublisher.html'                , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.documentOthers.edit'                   , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/documentOthers/othersEdit.html'                 , 'DocumentOthersEditCtrl'       ]])
      .state(['root.persons.view.documentOthers.edit.choosePublisher'   , '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'gva/persons/views/publishers/choosePublisher.html'                , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.documentApplications'                  , '/documentApplications'                                                                                                                                                    ])
      .state(['root.persons.view.documentApplications.search'           , ''                                           , ['@root.persons.view', 'gva/persons/views/documentApplications/docApplicationsSearch.html', 'DocApplicationsSearchCtrl'    ]])
      .state(['root.persons.view.documentApplications.new'              , '/new'                                       , ['@root.persons.view', 'gva/persons/views/documentApplications/docApplicationsNew.html'   , 'DocApplicationsNewCtrl'       ]])
      .state(['root.persons.view.documentApplications.edit'             , '/:ind'                                      , ['@root.persons.view', 'gva/persons/views/documentApplications/docApplicationsEdit.html'  , 'DocApplicationsEditCtrl'      ]]);
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.aircrafts'                                          , '/aircrafts?manSN&model&icao'                                                                                                                                                    ])
      .state(['root.aircrafts.search'                                   , ''                                           , ['@root'               , 'gva/aircrafts/views/aircraftsSearch.html'                            , 'AircraftsSearchCtrl'            ]])
      .state(['root.aircrafts.new'                                      , '/new'                                       , ['@root'               , 'gva/aircrafts/views/aircraftsNew.html'                               , 'AircraftsNewCtrl'               ]])
      .state(['root.aircrafts.view'                                     , '/:id'                                       , ['@root'               , 'gva/aircrafts/views/aircraftsView.html'                              , 'AircraftsViewCtrl'              ]])
      .state(['root.aircrafts.view.edit'                                , '/aircraftData'                              , ['@root'               , 'gva/aircrafts/views/aircraftData/aircraftDataEdit.html'              , 'AircraftDataEditCtrl'           ]])
      .state(['root.aircrafts.newApex'                                  , '/newApex'                                   , ['@root'               , 'gva/aircrafts/views/aircraftsApexNew.html'                           , 'AircraftsApexNewCtrl'           ]])
      .state(['root.aircrafts.view.editApex'                            , '/aircraftDataApex'                          , ['@root'               , 'gva/aircrafts/views/aircraftDataApex/aircraftDataEdit.html'          , 'AircraftDataApexEditCtrl'       ]])
      .state(['root.aircrafts.view.currentReg'                          , '/current/:ind'                              , ['@root.aircrafts.view', 'gva/aircrafts/views/certRegs/regsView.html'                          , 'CertRegsViewCtrl'               ]])
      .state(['root.aircrafts.view.regs'                                , '/regs'                                                                                                                                                                          ])
      .state(['root.aircrafts.view.regs.search'                         , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/certRegs/regsSearch.html'                        , 'CertRegsSearchCtrl'             ]])
      .state(['root.aircrafts.view.regs.new'                            , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/certRegs/regsNew.html'                           , 'CertRegsNewCtrl'                ]])
      .state(['root.aircrafts.view.regs.edit'                           , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/certRegs/regsEdit.html'                          , 'CertRegsEditCtrl'               ]])
      .state(['root.aircrafts.view.regsFM'                              , '/regsFM'                                                                                                                                                                        ])
      .state(['root.aircrafts.view.regsFM.search'                       , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/certRegsFM/regsSearch.html'                      , 'CertRegsFMSearchCtrl'           ]])
      .state(['root.aircrafts.view.regsFM.new'                          , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/certRegsFM/regsNew.html'                         , 'CertRegsFMNewCtrl'              ]])
      .state(['root.aircrafts.view.regsFM.edit'                         , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/certRegsFM/regsEdit.html'                        , 'CertRegsFMEditCtrl'             ]])
      .state(['root.aircrafts.view.smods'                               , '/smods'                                                                                                                                                                         ])
      .state(['root.aircrafts.view.smods.search'                        , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/certSmods/smodsSearch.html'                      , 'CertSmodsSearchCtrl'            ]])
      .state(['root.aircrafts.view.smods.new'                           , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/certSmods/smodsNew.html'                         , 'CertSmodsNewCtrl'               ]])
      .state(['root.aircrafts.view.smods.edit'                          , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/certSmods/smodsEdit.html'                        , 'CertSmodsEditCtrl'              ]])
      .state(['root.aircrafts.view.marks'                               , '/marks'                                                                                                                                                                         ])
      .state(['root.aircrafts.view.marks.search'                        , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/certMarks/marksSearch.html'                      , 'CertMarksSearchCtrl'            ]])
      .state(['root.aircrafts.view.marks.new'                           , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/certMarks/marksNew.html'                         , 'CertMarksNewCtrl'               ]])
      .state(['root.aircrafts.view.marks.edit'                          , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/certMarks/marksEdit.html'                        , 'CertMarksEditCtrl'              ]])
      .state(['root.aircrafts.view.airworthinesses'                     , '/airworthinesses'                                                                                                                                                               ])
      .state(['root.aircrafts.view.airworthinesses.search'              , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/certAirworthinesses/airworthinessesSearch.html'  , 'CertAirworthinessesSearchCtrl'  ]])
      .state(['root.aircrafts.view.airworthinesses.new'                 , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/certAirworthinesses/airworthinessesNew.html'     , 'CertAirworthinessesNewCtrl'     ]])
      .state(['root.aircrafts.view.airworthinesses.edit'                , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/certAirworthinesses/airworthinessesEdit.html'    , 'CertAirworthinessesEditCtrl'    ]])
      .state(['root.aircrafts.view.airworthinessesFM'                   , '/airworthinessesFM'                                                                                                                                                             ])
      .state(['root.aircrafts.view.airworthinessesFM.search'            , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/certAirworthinessesFM/airworthinessesSearch.html', 'CertAirworthinessesFMSearchCtrl']])
      .state(['root.aircrafts.view.airworthinessesFM.new'               , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/certAirworthinessesFM/airworthinessesNew.html'   , 'CertAirworthinessesFMNewCtrl'   ]])
      .state(['root.aircrafts.view.airworthinessesFM.edit'              , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/certAirworthinessesFM/airworthinessesEdit.html'  , 'CertAirworthinessesFMEditCtrl'  ]])
      .state(['root.aircrafts.view.permits'                             , '/permits'                                                                                                                                                                       ])
      .state(['root.aircrafts.view.permits.search'                      , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/certPermits/permitsSearch.html'                  , 'CertPermitsToFlySearchCtrl'     ]])
      .state(['root.aircrafts.view.permits.new'                         , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/certPermits/permitsNew.html'                     , 'CertPermitsToFlyNewCtrl'        ]])
      .state(['root.aircrafts.view.permits.edit'                        , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/certPermits/permitsEdit.html'                    , 'CertPermitsToFlyEditCtrl'       ]])
      .state(['root.aircrafts.view.noises'                              , '/noises'                                                                                                                                                                        ])
      .state(['root.aircrafts.view.noises.search'                       , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/certNoises/noisesSearch.html'                    , 'CertNoisesSearchCtrl'           ]])
      .state(['root.aircrafts.view.noises.new'                          , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/certNoises/noisesNew.html'                       , 'CertNoisesNewCtrl'              ]])
      .state(['root.aircrafts.view.noises.edit'                         , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/certNoises/noisesEdit.html'                      , 'CertNoisesEditCtrl'             ]])
      .state(['root.aircrafts.view.noisesFM'                            , '/noisesFM'                                                                                                                                                                      ])
      .state(['root.aircrafts.view.noisesFM.search'                     , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/certNoisesFM/noisesSearch.html'                  , 'CertNoisesFMSearchCtrl'         ]])
      .state(['root.aircrafts.view.noisesFM.new'                        , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/certNoisesFM/noisesNew.html'                     , 'CertNoisesFMNewCtrl'            ]])
      .state(['root.aircrafts.view.noisesFM.edit'                       , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/certNoisesFM/noisesEdit.html'                    , 'CertNoisesFMEditCtrl'           ]])
      .state(['root.aircrafts.view.radios'                              , '/radios'                                                                                                                                                                        ])
      .state(['root.aircrafts.view.radios.search'                       , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/certRadios/radiosSearch.html'                    , 'CertRadiosSearchCtrl'           ]])
      .state(['root.aircrafts.view.radios.new'                          , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/certRadios/radiosNew.html'                       , 'CertRadiosNewCtrl'              ]])
      .state(['root.aircrafts.view.radios.edit'                         , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/certRadios/radiosEdit.html'                      , 'CertRadiosEditCtrl'             ]])
      .state(['root.aircrafts.view.debts'                               , '/debts'                                                                                                                                                                         ])
      .state(['root.aircrafts.view.debts.search'                        , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/docDebts/debtsSearch.html'                       , 'DocDebtsSearchCtrl'             ]])
      .state(['root.aircrafts.view.debts.new'                           , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/docDebts/debtsNew.html'                          , 'DocDebtsNewCtrl'                ]])
      .state(['root.aircrafts.view.debts.edit'                          , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/docDebts/debtsEdit.html'                         , 'DocDebtsEditCtrl'               ]])
      .state(['root.aircrafts.view.debtsFM'                             , '/debtsFM'                                                                                                                                                                       ])
      .state(['root.aircrafts.view.debtsFM.search'                      , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/docDebtsFM/debtsSearch.html'                     , 'DocDebtsFMSearchCtrl'           ]])
      .state(['root.aircrafts.view.debtsFM.new'                         , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/docDebtsFM/debtsNew.html'                        , 'DocDebtsFMNewCtrl'              ]])
      .state(['root.aircrafts.view.debtsFM.edit'                        , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/docDebtsFM/debtsEdit.html'                       , 'DocDebtsFMEditCtrl'             ]])
      .state(['root.aircrafts.view.others'                              , '/others'                                                                                                                                                                        ])
      .state(['root.aircrafts.view.others.search'                       , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/documentOthers/othersSearch.html'                , 'AircraftOthersSearchCtrl'       ]])
      .state(['root.aircrafts.view.others.new'                          , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/documentOthers/othersNew.html'                   , 'AircraftOthersNewCtrl'          ]])
      .state(['root.aircrafts.view.others.edit'                         , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/documentOthers/othersEdit.html'                  , 'AircraftOthersEditCtrl'         ]])
      .state(['root.aircrafts.view.inspections'                         , '/inspections'                                                                                                                                                                   ])
      .state(['root.aircrafts.view.inspections.search'                  , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/inspections/inspectionsSearch.html'              , 'InspectionsSearchCtrl'          ]])
      .state(['root.aircrafts.view.inspections.new'                     , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/inspections/inspectionsNew.html'                 , 'InspectionsNewCtrl'             ]])
      .state(['root.aircrafts.view.inspections.edit'                    , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/inspections/inspectionsEdit.html'                , 'InspectionsEditCtrl'            ]])
      .state(['root.aircrafts.view.occurrences'                         , '/documentOccurrences'                                                                                                                                                           ])
      .state(['root.aircrafts.view.occurrences.search'                  , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/documentOccurrences/docOccurrencesSearch.html'   , 'DocOccurrencesSearchCtrl'       ]])
      .state(['root.aircrafts.view.occurrences.new'                     , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/documentOccurrences/docOccurrencesNew.html'      , 'DocOccurrencesNewCtrl'          ]])
      .state(['root.aircrafts.view.occurrences.edit'                    , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/documentOccurrences/docOccurrencesEdit.html'     , 'DocOccurrencesEditCtrl'         ]])
      .state(['root.aircrafts.view.maintenances'                        , '/maintenances'                                                                                                                                                                  ])
      .state(['root.aircrafts.view.maintenances.search'                 , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/aircraftMaintenances/maintenancesSearch.html'    , 'MaintenancesSearchCtrl'         ]])
      .state(['root.aircrafts.view.maintenances.new'                    , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/aircraftMaintenances/maintenancesNew.html'       , 'MaintenancesNewCtrl'            ]])
      .state(['root.aircrafts.view.maintenances.edit'                   , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/aircraftMaintenances/maintenancesEdit.html'      , 'MaintenancesEditCtrl'           ]])
      .state(['root.aircrafts.view.owners'                              , '/owners'                                                                                                                                                                        ])
      .state(['root.aircrafts.view.owners.search'                       , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/documentOwners/ownersSearch.html'                , 'DocumentOwnersSearchCtrl'       ]])
      .state(['root.aircrafts.view.owners.new'                          , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/documentOwners/ownersNew.html'                   , 'DocumentOwnersNewCtrl'          ]])
      .state(['root.aircrafts.view.owners.edit'                         , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/documentOwners/ownersEdit.html'                  , 'DocumentOwnersEditCtrl'         ]])
      .state(['root.aircrafts.view.parts'                               , '/parts'                                                                                                                                                                         ])
      .state(['root.aircrafts.view.parts.search'                        , ''                                           , ['@root.aircrafts.view', 'gva/aircrafts/views/parts/partsSearch.html'                          , 'PartsSearchCtrl'                ]])
      .state(['root.aircrafts.view.parts.new'                           , '/new'                                       , ['@root.aircrafts.view', 'gva/aircrafts/views/parts/partsNew.html'                             , 'PartsNewCtrl'                   ]])
      .state(['root.aircrafts.view.parts.edit'                          , '/:ind'                                      , ['@root.aircrafts.view', 'gva/aircrafts/views/parts/partsEdit.html'                            , 'PartsEditCtrl'                  ]]);
  }]);
}(angular));
