﻿/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('gva', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    // @ifndef DEBUG
    'gva.templates',
    // @endif
    'common',
    'l10n',
    'l10n-tools',
    'scrollto'
  ]).config(['scaffoldingProvider', function (scaffoldingProvider) {
    scaffoldingProvider.form({
      name: 'gvaApplicationDocument',
      templateUrl: 'js/gva/applications/forms/applicationDocument.html'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationSelectPerson',
      templateUrl: 'js/gva/applications/forms/selectPerson.html'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationSelectOrganization',
      templateUrl: 'js/gva/applications/forms/selectOrganization.html'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationSelectAircraft',
      templateUrl: 'js/gva/applications/forms/selectAircraft.html'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationSelectAirport',
      templateUrl: 'js/gva/applications/forms/selectAirport.html'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationSelectEquipment',
      templateUrl: 'js/gva/applications/forms/selectEquipment.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonData',
      templateUrl: 'js/gva/persons/forms/personData.html',
      controller: 'PersonDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonAddress',
      templateUrl: 'js/gva/persons/forms/personAddress.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentId',
      templateUrl: 'js/gva/persons/forms/personDocumentId.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentEducation',
      templateUrl: 'js/gva/persons/forms/personDocumentEducation.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonStatus',
      templateUrl: 'js/gva/persons/forms/personStatus.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonApplication',
      templateUrl: 'js/gva/persons/forms/personApplication.html',
      controller: 'PersonApplicationCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentMedical',
      templateUrl: 'js/gva/persons/forms/personDocumentMedical.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentEmployment',
      templateUrl: 'js/gva/persons/forms/personDocumentEmployment.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentCheck',
      templateUrl: 'js/gva/persons/forms/personDocumentCheck.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentTraining',
      templateUrl: 'js/gva/persons/forms/personDocumentTraining.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentOther',
      templateUrl: 'js/gva/persons/forms/personDocumentOther.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonFlyingExperience',
      templateUrl: 'js/gva/persons/forms/personFlyingExperience.html'
    });
    scaffoldingProvider.form({
      name: 'gvaRatingEdition',
      templateUrl: 'js/gva/persons/forms/personRatingEdition.html'
    });
    scaffoldingProvider.form({
      name: 'gvaRating',
      templateUrl: 'js/gva/persons/forms/personRating.html'
    });
    scaffoldingProvider.form({
      name: 'gvaLicence',
      templateUrl: 'js/gva/persons/forms/personLicence.html',
      controller: 'PersonLicenceCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaLicenceEdition',
      templateUrl: 'js/gva/persons/forms/personLicenceEdition.html',
      controller: 'PersonLicenceEditionCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentExam',
      templateUrl: 'js/gva/persons/forms/personDocumentExam.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonExam',
      templateUrl: 'js/gva/persons/forms/personExam.html',
      controller: 'PersonExamCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRegisterView',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertRegView.html',
      controller: 'AircraftCertRegViewCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertSmodView',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertSmodView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertMarkView',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertMarkView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertAirworthinessView',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertAirworthinessView.html',
      controller: 'AircraftCertAirworthinessViewCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertNoiseView',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertNoiseView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertPermittoflyView',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertPermitToFlyView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRadioView',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertRadioView.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertSmod',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertSmod.html',
      controller: 'AircraftCertSmodCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertMark',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertMark.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertAirworthiness',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertAirworthiness.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertAirworthinessFm',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertAirworthinessFM.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAirworthinessAmendment',
      templateUrl: 'js/gva/aircrafts/forms/aircraftAirworthinessAmendment.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAirworthinessReview',
      templateUrl: 'js/gva/aircrafts/forms/aircraftAirworthinessReview.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertPermit',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertPermitToFly.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertNoise',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertNoise.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertNoiseFm',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertNoiseFM.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRadio',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertRadio.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertReg',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertReg.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRegFm',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertRegFM.html',
      controller: 'AircraftCertRegFMCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRegDeregFm',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertRegDeregFM.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftData',
      templateUrl: 'js/gva/aircrafts/forms/aircraftData.html',
      controller: 'AircraftDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDataApex',
      templateUrl: 'js/gva/aircrafts/forms/aircraftDataApex.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentDebt',
      templateUrl: 'js/gva/aircrafts/forms/aircraftDocumentDebt.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentDebtFm',
      templateUrl: 'js/gva/aircrafts/forms/aircraftDocumentDebtFM.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentOther',
      templateUrl: 'js/gva/aircrafts/forms/aircraftDocumentOther.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentOccurrence',
      templateUrl: 'js/gva/aircrafts/forms/aircraftDocumentOccurrence.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftMaintenance',
      templateUrl: 'js/gva/aircrafts/forms/aircrafttMaintenance.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentOwner',
      templateUrl: 'js/gva/aircrafts/forms/aircraftDocumentOwner.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftPart',
      templateUrl: 'js/gva/aircrafts/forms/aircraftPart.html'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationData',
      templateUrl: 'js/gva/organizations/forms/organizationData.html',
      controller: 'OrganizationDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationAddress',
      templateUrl: 'js/gva/organizations/forms/organizationAddress.html'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationCertAirportOperator',
      templateUrl: 'js/gva/organizations/forms/organizationCertAirportOperator.html',
      controller: 'OrganizationCertAirportOperatorCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationCertificate',
      templateUrl: 'js/gva/organizations/forms/organizationCertificate.html',
      controller: 'OrganizationCertificateCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationAuditplan',
      templateUrl: 'js/gva/organizations/forms/organizationAuditplan.html'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationStaffManagement',
      templateUrl: 'js/gva/organizations/forms/organizationStaffManagement.html'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationStaffExaminer',
      templateUrl: 'js/gva/organizations/forms/organizationStaffExaminer.html'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationDocumentOther',
      templateUrl: 'js/gva/organizations/forms/organizationDocumentOther.html'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationEquipment',
      templateUrl: 'js/gva/organizations/forms/organizationEquipment.html',
      controller: 'OrganizationEquipmentCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationApproval',
      templateUrl: 'js/gva/organizations/forms/organizationApproval.html'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationAmendment',
      templateUrl: 'js/gva/organizations/forms/organizationAmendment.html',
      controller: 'OrganizationAmendmentCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationRecommendation',
      templateUrl: 'js/gva/organizations/forms/organizationRecommendation.html',
      controller: 'OrganizationRecommendationCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationRecommendationAuditor',
      templateUrl: 'js/gva/organizations/forms/organizationRecommendationAuditor.html',
      controller: 'OrganizationRecommendationAuditorCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationRegister',
      templateUrl: 'js/gva/organizations/forms/organizationRegister.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAirportData',
      templateUrl: 'js/gva/airports/forms/airportData.html',
      controller: 'AirportDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAirportDocumentOther',
      templateUrl: 'js/gva/airports/forms/airportDocumentOther.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAirportDocumentOwner',
      templateUrl: 'js/gva/airports/forms/airportDocumentOwner.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAirportCertOperational',
      templateUrl: 'js/gva/airports/forms/airportCertOperational.html',
      controller: 'AirportCertOperationalCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaCommonScannedDocument',
      templateUrl: 'js/gva/common/forms/commonScannedDocument.html',
      controller: 'CommonScannedDocCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaCommonInspection',
      templateUrl: 'js/gva/common/forms/commonInspection.html',
      controller: 'CommonInspectionCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaCommonAuditDetail',
      templateUrl: 'js/gva/common/forms/commonAuditDetail.html',
      controller: 'CommonAuditDetailCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaCommonDisparity',
      templateUrl: 'js/gva/common/forms/commonDisparity.html',
      controller: 'CommonDisparityCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaCommonDocumentApplication',
      templateUrl: 'js/gva/common/forms/commonDocumentApplication.html',
      controller: 'CommonDocumentApplicationCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaEquipmentData',
      templateUrl: 'js/gva/equipments/forms/equipmentData.html'
    });
    scaffoldingProvider.form({
      name: 'gvaEquipmentDocumentOther',
      templateUrl: 'js/gva/equipments/forms/equipmentDocumentOther.html'
    });
    scaffoldingProvider.form({
      name: 'gvaEquipmentDocumentOwner',
      templateUrl: 'js/gva/equipments/forms/equipmentDocumentOwner.html'
    });
    scaffoldingProvider.form({
      name: 'gvaEquipmentCertOperational',
      templateUrl: 'js/gva/equipments/forms/equipmentCertOperational.html',
      controller: 'EquipmentCertOperationalCtrl'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.applications'                                  , '/applications?filter'                                                                                                                                                                                             ])
      .state(['root.applications.search'                           , '?fromDate&toDate&personLin&aircraftIcao&organizationUin'                                 , ['@root'                  , 'js/gva/applications/views/applicationsSearch.html'        , 'ApplicationsSearchCtrl'        ]])
      .state(['root.applications.new'                              , '/new'                                                                                    , ['@root'                  , 'js/gva/applications/views/applicationsNew.html'           , 'ApplicationsNewCtrl'           ]])
      .state(['root.applications.new.personSelect'                 , '/personSelect?exact&lin&uin&names&licences&ratings&organization'                         , ['@root'                  , 'js/gva/applications/views/common/personSelect.html'       , 'PersonSelectCtrl'              ]])
      .state(['root.applications.new.personNew'                    , '/personNew'                                                                              , ['@root'                  , 'js/gva/applications/views/common/personNew.html'          , 'PersonNewCtrl'                 ]])
      .state(['root.applications.new.organizationSelect'           , '/organizationSelect?name&CAO&valid&organizationType&dateValidTo&dateCAOValidTo'          , ['@root'                  , 'js/gva/applications/views/common/organizationSelect.html' , 'OrganizationSelectCtrl'        ]])
      .state(['root.applications.new.organizationNew'              , '/organizationNew'                                                                        , ['@root'                  , 'js/gva/applications/views/common/organizationNew.html'    , 'OrganizationNewCtrl'           ]])
      .state(['root.applications.new.aircraftSelect'               , '/aircraftSelect?manSN&model&icao'                                                        , ['@root'                  , 'js/gva/applications/views/common/aircraftSelect.html'     , 'AircraftSelectCtrl'            ]])
      .state(['root.applications.new.aircraftNew'                  , '/aircraftNew'                                                                            , ['@root'                  , 'js/gva/applications/views/common/aircraftNew.html'        , 'AircraftNewCtrl'               ]])
      .state(['root.applications.new.airportSelect'                , '/airportSelect?name&icao'                                                                , ['@root'                  , 'js/gva/applications/views/common/airportSelect.html'      , 'AirportSelectCtrl'             ]])
      .state(['root.applications.new.airportNew'                   , '/airportNew'                                                                             , ['@root'                  , 'js/gva/applications/views/common/airportNew.html'         , 'AirportNewCtrl'                ]])
      .state(['root.applications.new.equipmentSelect'              , '/equipmentSelect?name'                                                                   , ['@root'                  , 'js/gva/applications/views/common/equipmentSelect.html'    , 'EquipmentSelectCtrl'           ]])
      .state(['root.applications.new.equipmentNew'                 , '/equipmentNew'                                                                           , ['@root'                  , 'js/gva/applications/views/common/equipmentNew.html'       , 'EquipmentNewCtrl'              ]])
      .state(['root.applications.new.corrSelect'                   , '/corrSelect?displayName&email&stamp'                                                     , ['@root'                  , 'js/gva/applications/views/common/corrSelect.html'         , 'CorrSelectCtrl'                ]])
      .state(['root.applications.new.corrNew'                      , '/corrNew'                                                                                , ['@root'                  , 'js/gva/applications/views/common/corrNew.html'            , 'CorrNewCtrl'                   ]])
      .state(['root.applications.link'                             , '/link'                                                                                   , ['@root'                  , 'js/gva/applications/views/applicationsLink.html'          , 'ApplicationsLinkCtrl'          ]])
      .state(['root.applications.link.docSelect'                   , '/docSelect?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units&stamp'       , ['@root'                  , 'js/gva/applications/views/common/docSelect.html'          , 'DocSelectCtrl'                 ]])
      .state(['root.applications.link.personSelect'                , '/personSelect?exact&lin&uin&names&licences&ratings&organization&stamp'                   , ['@root'                  , 'js/gva/applications/views/common/personSelect.html'       , 'PersonSelectCtrl'              ]])
      .state(['root.applications.link.personNew'                   , '/personNew'                                                                              , ['@root'                  , 'js/gva/applications/views/common/personNew.html'          , 'PersonNewCtrl'                 ]])
      .state(['root.applications.link.organizationSelect'          , '/organizationSelect?name&CAO&valid&organizationType&dateValidTo&dateCAOValidTo&uin&stamp', ['@root'                  , 'js/gva/applications/views/common/organizationSelect.html' , 'OrganizationSelectCtrl'        ]])
      .state(['root.applications.link.organizationNew'             , '/organizationNew'                                                                        , ['@root'                  , 'js/gva/applications/views/common/organizationNew.html'    , 'OrganizationNewCtrl'           ]])
      .state(['root.applications.link.aircraftSelect'              , '/aircraftSelect?manSN&model&icao&stamp'                                                  , ['@root'                  , 'js/gva/applications/views/common/aircraftSelect.html'     , 'AircraftSelectCtrl'            ]])
      .state(['root.applications.link.aircraftNew'                 , '/aircraftNew'                                                                            , ['@root'                  , 'js/gva/applications/views/common/aircraftNew.html'        , 'AircraftNewCtrl'               ]])
      .state(['root.applications.link.airportSelect'               , '/airportSelect?name&icao&stamp'                                                          , ['@root'                  , 'js/gva/applications/views/common/airportSelect.html'      , 'AirportSelectCtrl'             ]])
      .state(['root.applications.link.airportNew'                  , '/airportNew'                                                                             , ['@root'                  , 'js/gva/applications/views/common/airportNew.html'         , 'AirportNewCtrl'                ]])
      .state(['root.applications.link.equipmentSelect'             , '/equipmentSelect?name&stamp'                                                             , ['@root'                  , 'js/gva/applications/views/common/equipmentSelect.html'    , 'EquipmentSelectCtrl'           ]])
      .state(['root.applications.link.equipmentNew'                , '/equipmentNew'                                                                           , ['@root'                  , 'js/gva/applications/views/common/equipmentNew.html'       , 'EquipmentNewCtrl'              ]])
      .state(['root.applications.edit'                             , '/:id'                                                                                    , ['@root'                  , 'js/gva/applications/views/applicationsEdit.html'          , 'ApplicationsEditCtrl'          ]])
      .state(['root.applications.edit.case'                        , '/case'                                                                                   , ['@root.applications.edit', 'js/gva/applications/views/applicationsEditCase.html'      , 'ApplicationsEditCaseCtrl'      ]])
      .state(['root.applications.edit.case.newFile'                , '/newFile?docId&docFileId'                                                                , ['@root.applications.edit', 'js/gva/applications/views/applicationsEditNewFile.html'   , 'ApplicationsEditNewFileCtrl'   ]])
      .state(['root.applications.edit.case.newDocFile'             , '/newDocFile?docId'                                                                       , ['@root.applications.edit', 'js/gva/applications/views/applicationsEditNewDocFile.html', 'ApplicationsEditNewDocFileCtrl']])
      .state(['root.applications.edit.case.childDoc'               , '/childDoc?parentDocId'                                                                   , ['@root.applications.edit', 'js/gva/applications/views/applicationsEditChildDoc.html'  , 'ApplicationsEditChildDocCtrl'  ]])
      .state(['root.applications.edit.case.addPart'                , '/addPart?docId&docFileId&setPartAlias'                                                   , ['@root.applications.edit', 'js/gva/applications/views/applicationsEditAddPart.html'   , 'ApplicationsEditAddPartCtrl'   ]])
      .state(['root.applications.edit.case.addPart.choosePublisher', '/choosepublisher?text&publisherTypeAlias'                                                , ['@root.applications.edit', 'js/gva/common/views/publishers/choosePublisher.html'      , 'ChoosePublisherCtrl'           ]])
      .state(['root.applications.edit.case.linkPart'               , '/linkPart?docFileId'                                                                     , ['@root.applications.edit', 'js/gva/applications/views/applicationsEditLinkPart.html'  , 'ApplicationsEditLinkPartCtrl'  ]]);
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.persons'                                            , '/persons?exact&lin&uin&names&licences&ratings&organization'                                                                                                              ])
      .state(['root.persons.search'                                     , ''                                           , ['@root'             , 'js/gva/persons/views/personsSearch.html'                             , 'PersonsSearchCtrl'            ]])
      .state(['root.persons.new'                                        , '/new'                                       , ['@root'             , 'js/gva/persons/views/personsNew.html'                                , 'PersonsNewCtrl'               ]])
      .state(['root.persons.view'                                       , '/:id?caseTypeId&appId&filter'               , ['@root'             , 'js/gva/persons/views/personsView.html'                               , 'PersonsViewCtrl'              ]])
      .state(['root.persons.view.edit'                                  , '/personData'                                , ['@root'             , 'js/gva/persons/views/personData/personDataEdit.html'                 , 'PersonDataEditCtrl'           ]])
      .state(['root.persons.view.addresses'                             , '/addresses'                                                                                                                                                              ])
      .state(['root.persons.view.addresses.search'                      , ''                                           , ['@root.persons.view', 'js/gva/persons/views/addresses/addrSearch.html'                      , 'AddressesSearchCtrl'          ]])
      .state(['root.persons.view.addresses.new'                         , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/addresses/addrNew.html'                         , 'AddressesNewCtrl'             ]])
      .state(['root.persons.view.addresses.edit'                        , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/addresses/addrEdit.html'                        , 'AddressesEditCtrl'            ]])
      .state(['root.persons.view.statuses'                              , '/statuses'                                                                                                                                                               ])
      .state(['root.persons.view.statuses.search'                       , ''                                           , ['@root.persons.view', 'js/gva/persons/views/statuses/statusesSearch.html'                   , 'StatusesSearchCtrl'           ]])
      .state(['root.persons.view.statuses.new'                          , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/statuses/statusesNew.html'                      , 'StatusesNewCtrl'              ]])
      .state(['root.persons.view.statuses.edit'                         , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/statuses/statusesEdit.html'                     , 'StatusesEditCtrl'             ]])
      .state(['root.persons.view.documentIds'                           , '/documentIds'                                                                                                                                                            ])
      .state(['root.persons.view.documentIds.search'                    , ''                                           , ['@root.persons.view', 'js/gva/persons/views/documentIds/idsSearch.html'                     , 'DocumentIdsSearchCtrl'        ]])
      .state(['root.persons.view.documentIds.new'                       , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/documentIds/idsNew.html'                        , 'DocumentIdsNewCtrl'           ]])
      .state(['root.persons.view.documentIds.edit'                      , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/documentIds/idsEdit.html'                       , 'DocumentIdsEditCtrl'          ]])
      .state(['root.persons.view.documentEducations'                    , '/documentEducations'                                                                                                                                                     ])
      .state(['root.persons.view.documentEducations.search'             , ''                                           , ['@root.persons.view', 'js/gva/persons/views/documentEducations/edusSearch.html'             , 'DocumentEducationsSearchCtrl' ]])
      .state(['root.persons.view.documentEducations.new'                , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/documentEducations/edusNew.html'                , 'DocumentEducationsNewCtrl'    ]])
      .state(['root.persons.view.documentEducations.edit'               , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/documentEducations/edusEdit.html'               , 'DocumentEducationsEditCtrl'   ]])
      .state(['root.persons.view.licences'                                 , '/licences'                                                                                                                                                      ])
      .state(['root.persons.view.licences.search'                          , ''                                          , ['@root.persons.view', 'js/gva/persons/views/licences/licencesSearch.html'          , 'LicencesSearchCtrl'           ]])
      .state(['root.persons.view.licences.new'                             , '/new'                                      , ['@root.persons.view', 'js/gva/persons/views/licences/licencesNew.html'             , 'LicencesNewCtrl'              ]])
      .state(['root.persons.view.licences.new.chooseRating'                , '/chooseRating'                             , ['@root.persons.view', 'js/gva/persons/views/ratings/chooseRating.html'             , 'ChooseRatingCtrl'             ]])
      .state(['root.persons.view.licences.new.newRating'                   , '/newRating'                                , ['@root.persons.view', 'js/gva/persons/views/ratings/ratingsNew.html'               , 'AddRatingCtrl'                ]])
      .state(['root.persons.view.licences.new.chooseMedical'               , '/chooseMedical'                            , ['@root.persons.view', 'js/gva/persons/views/documentMedicals/chooseMedical.html'   , 'ChooseMedicalCtrl'            ]])
      .state(['root.persons.view.licences.new.newMedical'                  , '/newMedical'                               , ['@root.persons.view', 'js/gva/persons/views/documentMedicals/medsNew.html'         , 'AddMedicalCtrl'               ]])
      .state(['root.persons.view.licences.new.chooseTraining'              , '/chooseTraining'                           , ['@root.persons.view', 'js/gva/persons/views/documentTrainings/chooseTraining.html' , 'ChooseTrainingCtrl'           ]])
      .state(['root.persons.view.licences.new.newTraining'                 , '/newTraining'                              , ['@root.persons.view', 'js/gva/persons/views/documentTrainings/trainingsNew.html'   , 'AddTrainingCtrl'              ]])
      .state(['root.persons.view.licences.new.newTraining.choosePublisher' , '/choosepublisher?text&publisherTypeAlias'  , ['@root.persons.view', 'js/gva/common/views/publishers/choosePublisher.html'        , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.licences.new.chooseExam'                  , '/chooseExam'                               , ['@root.persons.view', 'js/gva/persons/views/documentExams/chooseExam.html'         , 'ChooseExamCtrl'               ]])
      .state(['root.persons.view.licences.new.newExam'                     , '/newExam'                                  , ['@root.persons.view', 'js/gva/persons/views/documentExams/examsNew.html'           , 'AddExamCtrl'                  ]])
      .state(['root.persons.view.licences.new.chooseLicence'               , '/chooseLicence'                            , ['@root.persons.view', 'js/gva/persons/views/licences/chooseLicence.html'           , 'ChooseLicenceCtrl'            ]])
      .state(['root.persons.view.licences.edit'                            , '/:ind'                                     , ['@root.persons.view', 'js/gva/persons/views/licences/licencesEdit.html'            , 'LicencesEditCtrl'             ]])
      .state(['root.persons.view.licences.edit.chooseRating'               , '/chooseRating'                             , ['@root.persons.view', 'js/gva/persons/views/ratings/chooseRating.html'             , 'ChooseRatingCtrl'             ]])
      .state(['root.persons.view.licences.edit.newRating'                  , '/newRating'                                , ['@root.persons.view', 'js/gva/persons/views/ratings/ratingsNew.html'               , 'AddRatingCtrl'                ]])
      .state(['root.persons.view.licences.edit.chooseMedical'              , '/chooseMedical'                            , ['@root.persons.view', 'js/gva/persons/views/documentMedicals/chooseMedical.html'   , 'ChooseMedicalCtrl'            ]])
      .state(['root.persons.view.licences.edit.newMedical'                 , '/newMedical'                               , ['@root.persons.view', 'js/gva/persons/views/documentMedicals/medsNew.html'         , 'AddMedicalCtrl'               ]])
      .state(['root.persons.view.licences.edit.chooseTraining'             , '/chooseTraining'                           , ['@root.persons.view', 'js/gva/persons/views/documentTrainings/chooseTraining.html' , 'ChooseTrainingCtrl'           ]])
      .state(['root.persons.view.licences.edit.newTraining'                , '/newTraining'                              , ['@root.persons.view', 'js/gva/persons/views/documentTrainings/trainingsNew.html'   , 'AddTrainingCtrl'              ]])
      .state(['root.persons.view.licences.edit.newTraining.choosePublisher', '/choosepublisher?text&publisherTypeAlias'  , ['@root.persons.view', 'js/gva/common/views/publishers/choosePublisher.html'        , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.licences.edit.chooseExam'                 , '/chooseExam'                               , ['@root.persons.view', 'js/gva/persons/views/documentExams/chooseExam.html'         , 'ChooseExamCtrl'               ]])
      .state(['root.persons.view.licences.edit.newExam'                    , '/newExam'                                  , ['@root.persons.view', 'js/gva/persons/views/documentExams/examsNew.html'           , 'AddExamCtrl'                  ]])
      .state(['root.persons.view.licences.edit.chooseLicence'              , '/chooseLicence'                            , ['@root.persons.view', 'js/gva/persons/views/licences/chooseLicence.html'           , 'ChooseLicenceCtrl'            ]])
      .state(['root.persons.view.checks'                                , '/checks'                                                                                                                                                                 ])
      .state(['root.persons.view.checks.search'                         , ''                                           , ['@root.persons.view', 'js/gva/persons/views/documentChecks/checksSearch.html'               , 'DocumentChecksSearchCtrl'     ]])
      .state(['root.persons.view.checks.new'                            , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/documentChecks/checksNew.html'                  , 'DocumentChecksNewCtrl'        ]])
      .state(['root.persons.view.checks.new.choosePublisher'            , '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'js/gva/common/views/publishers/choosePublisher.html'                 , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.checks.edit'                           , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/documentChecks/checksEdit.html'                 , 'DocumentChecksEditCtrl'       ]])
      .state(['root.persons.view.checks.edit.choosePublisher'           , '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'js/gva/common/views/publishers/choosePublisher.html'                 , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.employments'                           , '/employments'                                                                                                                                                            ])
      .state(['root.persons.view.employments.search'                    , ''                                           , ['@root.persons.view', 'js/gva/persons/views/documentEmployments/emplsSearch.html'           , 'DocumentEmploymentsSearchCtrl']])
      .state(['root.persons.view.employments.new'                       , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/documentEmployments/emplsNew.html'              , 'DocumentEmploymentsNewCtrl'   ]])
      .state(['root.persons.view.employments.edit'                      , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/documentEmployments/emplsEdit.html'             , 'DocumentEmploymentsEditCtrl'  ]])
      .state(['root.persons.view.medicals'                              , '/medicals'                                                                                                                                                               ])
      .state(['root.persons.view.medicals.search'                       , ''                                           , ['@root.persons.view', 'js/gva/persons/views/documentMedicals/medsSearch.html'               , 'DocumentMedicalsSearchCtrl'   ]])
      .state(['root.persons.view.medicals.new'                          , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/documentMedicals/medsNew.html'                  , 'DocumentMedicalsNewCtrl'      ]])
      .state(['root.persons.view.medicals.edit'                         , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/documentMedicals/medsEdit.html'                 , 'DocumentMedicalsEditCtrl'     ]])
      .state(['root.persons.view.documentTrainings'                     , '/documentTrainings'                                                                                                                                                      ])
      .state(['root.persons.view.documentTrainings.search'              , ''                                           , ['@root.persons.view', 'js/gva/persons/views/documentTrainings/trainingsSearch.html'         , 'DocumentTrainingsSearchCtrl'  ]])
      .state(['root.persons.view.documentTrainings.new'                 , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/documentTrainings/trainingsNew.html'            , 'DocumentTrainingsNewCtrl'     ]])
      .state(['root.persons.view.documentTrainings.new.choosePublisher' , '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'js/gva/common/views/publishers/choosePublisher.html'                 , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.documentTrainings.edit'                , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/documentTrainings/trainingsEdit.html'           , 'DocumentTrainingsEditCtrl'    ]])
      .state(['root.persons.view.documentTrainings.edit.choosePublisher', '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'js/gva/common/views/publishers/choosePublisher.html'                 , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.flyingExperiences'                     , '/flyingExperiences'                                                                                                                                                      ])
      .state(['root.persons.view.flyingExperiences.search'              , ''                                           , ['@root.persons.view', 'js/gva/persons/views/flyingExperiences/flyExpsSearch.html'           , 'FlyingExperiencesSearchCtrl'  ]])
      .state(['root.persons.view.flyingExperiences.new'                 , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/flyingExperiences/flyExpsNew.html'              , 'FlyingExperiencesNewCtrl'     ]])
      .state(['root.persons.view.flyingExperiences.edit'                , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/flyingExperiences/flyExpsEdit.html'             , 'FlyingExperiencesEditCtrl'    ]])
      .state(['root.persons.view.inventory'                             , '/inventory'                                 , ['@root.persons.view', 'js/gva/persons/views/inventory/inventorySearch.html'                 , 'InventorySearchCtrl'          ]])
      .state(['root.persons.view.ratings'                               , '/ratings'                                                                                                                                                                ])
      .state(['root.persons.view.ratings.search'                        , ''                                           , ['@root.persons.view', 'js/gva/persons/views/ratings/ratingsSearch.html'                     , 'RatingsSearchCtrl'            ]])
      .state(['root.persons.view.ratings.new'                           , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/ratings/ratingsNew.html'                        , 'RatingsNewCtrl'               ]])
      .state(['root.persons.view.ratings.edit'                          , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/ratings/ratingsEdit.html'                       , 'RatingsEditCtrl'              ]])
      .state(['root.persons.view.exams'                                 , '/exams'                                                                                                                                            ])
      .state(['root.persons.view.exams.search'                          , ''                                           , ['@root.persons.view', 'js/gva/persons/views/documentExams/examsSearch.html', 'DocumentExamsSearchCtrl' ]])
      .state(['root.persons.view.exams.new'                             , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/documentExams/examsNew.html'   , 'DocumentExamsNewCtrl'    ]])
      .state(['root.persons.view.exams.edit'                            , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/documentExams/examsEdit.html'  , 'DocumentExamsEditCtrl'   ]])
      .state(['root.persons.view.examASs'                               , '/examASs'                                                                                                                                                                ])
      .state(['root.persons.view.examASs.search'                        , ''                                           , ['@root.persons.view', 'js/gva/persons/views/exams/examsSearch.html'                         , 'ExamsSearchCtrl'              ]])
      .state(['root.persons.view.examASs.new'                           , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/exams/examsNew.html'                            , 'ExamsNewCtrl'                 ]])
      .state(['root.persons.view.examASs.edit'                          , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/exams/examsEdit.html'                           , 'ExamsEditCtrl'                ]])
      .state(['root.persons.view.documentOthers'                        , '/documentOthers'                                                                                                                                                          ])
      .state(['root.persons.view.documentOthers.search'                 , ''                                           , ['@root.persons.view', 'js/gva/persons/views/documentOthers/othersSearch.html'               , 'DocumentOthersSearchCtrl'     ]])
      .state(['root.persons.view.documentOthers.new'                    , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/documentOthers/othersNew.html'                  , 'DocumentOthersNewCtrl'        ]])
      .state(['root.persons.view.documentOthers.new.choosePublisher'    , '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'js/gva/common/views/publishers/choosePublisher.html'                 , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.documentOthers.edit'                   , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/documentOthers/othersEdit.html'                 , 'DocumentOthersEditCtrl'       ]])
      .state(['root.persons.view.documentOthers.edit.choosePublisher'   , '/choosepublisher?text&publisherTypeAlias'   , ['@root.persons.view', 'js/gva/common/views/publishers/choosePublisher.html'                 , 'ChoosePublisherCtrl'          ]])
      .state(['root.persons.view.documentApplications'                  , '/documentApplications'                                                                                                                                                    ])
      .state(['root.persons.view.documentApplications.search'           , ''                                           , ['@root.persons.view', 'js/gva/persons/views/documentApplications/docApplicationsSearch.html', 'DocApplicationsSearchCtrl'    ]])
      .state(['root.persons.view.documentApplications.new'              , '/new'                                       , ['@root.persons.view', 'js/gva/persons/views/documentApplications/docApplicationsNew.html'   , 'DocApplicationsNewCtrl'       ]])
      .state(['root.persons.view.documentApplications.edit'             , '/:ind'                                      , ['@root.persons.view', 'js/gva/persons/views/documentApplications/docApplicationsEdit.html'  , 'DocApplicationsEditCtrl'      ]]);
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.aircrafts'                                          , '/aircrafts?mark&manSN&model&airCategory&aircraftProducer'                                                                                                                       ])
      .state(['root.aircrafts.search'                                   , ''                                           , ['@root'               , 'js/gva/aircrafts/views/aircraftsSearch.html'                            , 'AircraftsSearchCtrl'            ]])
      .state(['root.aircrafts.new'                                      , '/new'                                       , ['@root'               , 'js/gva/aircrafts/views/aircraftsNew.html'                               , 'AircraftsNewCtrl'               ]])
      .state(['root.aircrafts.newWizzard'                               , '/newWizzard'                                , ['@root'               , 'js/gva/aircrafts/views/aircraftsNewWizzard.html'                        , 'AircraftsNewWizzardCtrl'        ]])
      .state(['root.aircrafts.view'                                     , '/:id?appId&filter'                          , ['@root'               , 'js/gva/aircrafts/views/aircraftsView.html'                              , 'AircraftsViewCtrl'              ]])
      .state(['root.aircrafts.view.edit'                                , '/aircraftData'                              , ['@root'               , 'js/gva/aircrafts/views/aircraftData/aircraftDataEdit.html'              , 'AircraftDataEditCtrl'           ]])
      .state(['root.aircrafts.newApex'                                  , '/newApex'                                   , ['@root'               , 'js/gva/aircrafts/views/aircraftsApexNew.html'                           , 'AircraftsApexNewCtrl'           ]])
      .state(['root.aircrafts.view.editApex'                            , '/aircraftDataApex'                          , ['@root'               , 'js/gva/aircrafts/views/aircraftDataApex/aircraftDataEdit.html'          , 'AircraftDataApexEditCtrl'       ]])
      .state(['root.aircrafts.view.currentReg'                          , '/current/:ind'                              , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegs/regsView.html'                          , 'CertRegsViewCtrl'               ]])
      .state(['root.aircrafts.view.regs'                                , '/regs'                                                                                                                                                                          ])
      .state(['root.aircrafts.view.regs.search'                         , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegs/regsSearch.html'                        , 'CertRegsSearchCtrl'             ]])
      .state(['root.aircrafts.view.regs.new'                            , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegs/regsNew.html'                           , 'CertRegsNewCtrl'                ]])
      .state(['root.aircrafts.view.regs.edit'                           , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegs/regsEdit.html'                          , 'CertRegsEditCtrl'               ]])
      .state(['root.aircrafts.view.regsFM'                              , '/regsFM'                                                                                                                                                                        ])
      .state(['root.aircrafts.view.regsFM.search'                       , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsSearch.html'                      , 'CertRegsFMSearchCtrl'           ]])
      .state(['root.aircrafts.view.regsFM.new'                          , '/new?oldInd'                                , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsNew.html'                         , 'CertRegsFMNewCtrl'              ]])
      .state(['root.aircrafts.view.regsFM.newWizzard'                   , '/newWizzard?oldInd'                         , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsNewWizzard.html'                  , 'CertRegsFMNewWizzardCtrl'       ]])
      .state(['root.aircrafts.view.regsFM.edit'                         , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsEdit.html'                        , 'CertRegsFMEditCtrl'             ]])
      .state(['root.aircrafts.view.regsFM.dereg'                        , '/dereg/:ind'                                , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsDereg.html'                       , 'CertRegsFMDeregCtrl'            ]])
      .state(['root.aircrafts.view.smods'                               , '/smods'                                                                                                                                                                         ])
      .state(['root.aircrafts.view.smods.search'                        , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certSmods/smodsSearch.html'                      , 'CertSmodsSearchCtrl'            ]])
      .state(['root.aircrafts.view.smods.new'                           , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certSmods/smodsNew.html'                         , 'CertSmodsNewCtrl'               ]])
      .state(['root.aircrafts.view.smods.edit'                          , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certSmods/smodsEdit.html'                        , 'CertSmodsEditCtrl'              ]])
      .state(['root.aircrafts.view.marks'                               , '/marks'                                                                                                                                                                         ])
      .state(['root.aircrafts.view.marks.search'                        , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certMarks/marksSearch.html'                      , 'CertMarksSearchCtrl'            ]])
      .state(['root.aircrafts.view.marks.new'                           , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certMarks/marksNew.html'                         , 'CertMarksNewCtrl'               ]])
      .state(['root.aircrafts.view.marks.edit'                          , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certMarks/marksEdit.html'                        , 'CertMarksEditCtrl'              ]])
      .state(['root.aircrafts.view.airworthinesses'                     , '/airworthinesses'                                                                                                                                                               ])
      .state(['root.aircrafts.view.airworthinesses.search'              , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certAirworthinesses/airworthinessesSearch.html'  , 'CertAirworthinessesSearchCtrl'  ]])
      .state(['root.aircrafts.view.airworthinesses.new'                 , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certAirworthinesses/airworthinessesNew.html'     , 'CertAirworthinessesNewCtrl'     ]])
      .state(['root.aircrafts.view.airworthinesses.edit'                , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certAirworthinesses/airworthinessesEdit.html'    , 'CertAirworthinessesEditCtrl'    ]])
      .state(['root.aircrafts.view.airworthinessesFM'                   , '/airworthinessesFM'                                                                                                                                                             ])
      .state(['root.aircrafts.view.airworthinessesFM.search'            , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certAirworthinessesFM/airworthinessesSearch.html', 'CertAirworthinessesFMSearchCtrl']])
      .state(['root.aircrafts.view.airworthinessesFM.new'               , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certAirworthinessesFM/airworthinessesNew.html'   , 'CertAirworthinessesFMNewCtrl'   ]])
      .state(['root.aircrafts.view.airworthinessesFM.edit'              , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certAirworthinessesFM/airworthinessesEdit.html'  , 'CertAirworthinessesFMEditCtrl'  ]])
      .state(['root.aircrafts.view.permits'                             , '/permits'                                                                                                                                                                       ])
      .state(['root.aircrafts.view.permits.search'                      , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certPermits/permitsSearch.html'                  , 'CertPermitsToFlySearchCtrl'     ]])
      .state(['root.aircrafts.view.permits.new'                         , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certPermits/permitsNew.html'                     , 'CertPermitsToFlyNewCtrl'        ]])
      .state(['root.aircrafts.view.permits.edit'                        , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certPermits/permitsEdit.html'                    , 'CertPermitsToFlyEditCtrl'       ]])
      .state(['root.aircrafts.view.noises'                              , '/noises'                                                                                                                                                                        ])
      .state(['root.aircrafts.view.noises.search'                       , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certNoises/noisesSearch.html'                    , 'CertNoisesSearchCtrl'           ]])
      .state(['root.aircrafts.view.noises.new'                          , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certNoises/noisesNew.html'                       , 'CertNoisesNewCtrl'              ]])
      .state(['root.aircrafts.view.noises.edit'                         , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certNoises/noisesEdit.html'                      , 'CertNoisesEditCtrl'             ]])
      .state(['root.aircrafts.view.noisesFM'                            , '/noisesFM'                                                                                                                                                                      ])
      .state(['root.aircrafts.view.noisesFM.search'                     , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certNoisesFM/noisesSearch.html'                  , 'CertNoisesFMSearchCtrl'         ]])
      .state(['root.aircrafts.view.noisesFM.new'                        , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certNoisesFM/noisesNew.html'                     , 'CertNoisesFMNewCtrl'            ]])
      .state(['root.aircrafts.view.noisesFM.edit'                       , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certNoisesFM/noisesEdit.html'                    , 'CertNoisesFMEditCtrl'           ]])
      .state(['root.aircrafts.view.radios'                              , '/radios'                                                                                                                                                                        ])
      .state(['root.aircrafts.view.radios.search'                       , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRadios/radiosSearch.html'                    , 'CertRadiosSearchCtrl'           ]])
      .state(['root.aircrafts.view.radios.new'                          , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRadios/radiosNew.html'                       , 'CertRadiosNewCtrl'              ]])
      .state(['root.aircrafts.view.radios.edit'                         , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRadios/radiosEdit.html'                      , 'CertRadiosEditCtrl'             ]])
      .state(['root.aircrafts.view.debts'                               , '/debts'                                                                                                                                                                         ])
      .state(['root.aircrafts.view.debts.search'                        , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/docDebts/debtsSearch.html'                       , 'DocDebtsSearchCtrl'             ]])
      .state(['root.aircrafts.view.debts.new'                           , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/docDebts/debtsNew.html'                          , 'DocDebtsNewCtrl'                ]])
      .state(['root.aircrafts.view.debts.edit'                          , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/docDebts/debtsEdit.html'                         , 'DocDebtsEditCtrl'               ]])
      .state(['root.aircrafts.view.debtsFM'                             , '/debtsFM'                                                                                                                                                                       ])
      .state(['root.aircrafts.view.debtsFM.search'                      , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/docDebtsFM/debtsSearch.html'                     , 'DocDebtsFMSearchCtrl'           ]])
      .state(['root.aircrafts.view.debtsFM.new'                         , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/docDebtsFM/debtsNew.html'                        , 'DocDebtsFMNewCtrl'              ]])
      .state(['root.aircrafts.view.debtsFM.edit'                        , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/docDebtsFM/debtsEdit.html'                       , 'DocDebtsFMEditCtrl'             ]])
      .state(['root.aircrafts.view.others'                              , '/others'                                                                                                                                                                        ])
      .state(['root.aircrafts.view.others.search'                       , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOthers/othersSearch.html'                , 'AircraftOthersSearchCtrl'       ]])
      .state(['root.aircrafts.view.others.new'                          , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOthers/othersNew.html'                   , 'AircraftOthersNewCtrl'          ]])
      .state(['root.aircrafts.view.others.edit'                         , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOthers/othersEdit.html'                  , 'AircraftOthersEditCtrl'         ]])
      .state(['root.aircrafts.view.inspections'                         , '/inspections'                                                                                                                                                                   ])
      .state(['root.aircrafts.view.inspections.search'                  , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/inspections/aircraftsInspectionsSearch.html'     , 'AircraftsInspectionsSearchCtrl' ]])
      .state(['root.aircrafts.view.inspections.new'                     , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/inspections/aircraftsInspectionsNew.html'        , 'AircraftsInspectionsNewCtrl'    ]])
      .state(['root.aircrafts.view.inspections.edit'                    , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/inspections/aircraftsInspectionsEdit.html'       , 'AircraftsInspectionsEditCtrl'   ]])
      .state(['root.aircrafts.view.occurrences'                         , '/documentOccurrences'                                                                                                                                                           ])
      .state(['root.aircrafts.view.occurrences.search'                  , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOccurrences/docOccurrencesSearch.html'   , 'DocOccurrencesSearchCtrl'       ]])
      .state(['root.aircrafts.view.occurrences.new'                     , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOccurrences/docOccurrencesNew.html'      , 'DocOccurrencesNewCtrl'          ]])
      .state(['root.aircrafts.view.occurrences.edit'                    , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOccurrences/docOccurrencesEdit.html'     , 'DocOccurrencesEditCtrl'         ]])
      .state(['root.aircrafts.view.maintenances'                        , '/maintenances'                                                                                                                                                                  ])
      .state(['root.aircrafts.view.maintenances.search'                 , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/aircraftMaintenances/maintenancesSearch.html'    , 'MaintenancesSearchCtrl'         ]])
      .state(['root.aircrafts.view.maintenances.new'                    , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/aircraftMaintenances/maintenancesNew.html'       , 'MaintenancesNewCtrl'            ]])
      .state(['root.aircrafts.view.maintenances.edit'                   , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/aircraftMaintenances/maintenancesEdit.html'      , 'MaintenancesEditCtrl'           ]])
      .state(['root.aircrafts.view.owners'                              , '/owners'                                                                                                                                                                        ])
      .state(['root.aircrafts.view.owners.search'                       , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOwners/ownersSearch.html'                , 'DocumentOwnersSearchCtrl'       ]])
      .state(['root.aircrafts.view.owners.new'                          , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOwners/ownersNew.html'                   , 'DocumentOwnersNewCtrl'          ]])
      .state(['root.aircrafts.view.owners.edit'                         , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOwners/ownersEdit.html'                  , 'DocumentOwnersEditCtrl'         ]])
      .state(['root.aircrafts.view.parts'                               , '/parts'                                                                                                                                                                         ])
      .state(['root.aircrafts.view.parts.search'                        , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/parts/partsSearch.html'                          , 'PartsSearchCtrl'                ]])
      .state(['root.aircrafts.view.parts.new'                           , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/parts/partsNew.html'                             , 'PartsNewCtrl'                   ]])
      .state(['root.aircrafts.view.parts.edit'                          , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/parts/partsEdit.html'                            , 'PartsEditCtrl'                  ]])
      .state(['root.aircrafts.view.applications'                        , '/applications'                                                                                                                                                                  ])
      .state(['root.aircrafts.view.applications.search'                 , ''                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentApplications/docApplicationsSearch.html' , 'AircraftApplicationsSearchCtrl' ]])
      .state(['root.aircrafts.view.applications.new'                    , '/new'                                       , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentApplications/docApplicationsNew.html'    , 'AircraftApplicationsNewCtrl'    ]])
      .state(['root.aircrafts.view.applications.edit'                   , '/:ind'                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentApplications/docApplicationsEdit.html'   , 'AircraftApplicationsEditCtrl'   ]])
      .state(['root.aircrafts.view.inventory'                           , '/inventory'                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/inventory/inventorySearch.html'                  , 'AircraftInventorySearchCtrl'    ]]);
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.organizations'                                                               , '/organizations?name&CAO&valid&organizationType&dateValidTo&dateCAOValidTo&caseTypeId'])
      .state(['root.organizations.search'                                                        , ''                                           , ['@root'                   , 'js/gva/organizations/views/organizationsSearch.html'                                                                    , 'OrganizationsSearchCtrl'                           ]])
      .state(['root.organizations.new'                                                           , '/new'                                       , ['@root'                   , 'js/gva/organizations/views/organizationsNew.html'                                                                       , 'OrganizationsNewCtrl'                              ]])
      .state(['root.organizations.view'                                                          , '/:id?appId&filter'                          , ['@root'                   , 'js/gva/organizations/views/organizationsView.html'                                                                      , 'OrganizationsViewCtrl'                             ]])
      .state(['root.organizations.view.edit'                                                     , '/organizationData'                          , ['@root'                   , 'js/gva/organizations/views/organizationData/organizationDataEdit.html'                                                  , 'OrganizationDataEditCtrl'                          ]])
      .state(['root.organizations.view.addresses'                                                , '/addresses'                                                                                                                                                                                                                                            ])
      .state(['root.organizations.view.addresses.search'                                         , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/addresses/addrSearch.html'                                                                   , 'OrganizationAddressesSearchCtrl'                   ]])
      .state(['root.organizations.view.addresses.new'                                            , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/addresses/addrNew.html'                                                                      , 'OrganizationAddressesNewCtrl'                      ]])
      .state(['root.organizations.view.addresses.edit'                                           , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/addresses/addrEdit.html'                                                                     , 'OrganizationAddressesEditCtrl'                     ]])
      .state(['root.organizations.view.certAirportOperators'                                     , '/certAirportOperators'                                                                                                                                                                                                                                 ])
      .state(['root.organizations.view.certAirportOperators.search'                              , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certAirportOperators/certAirportOperatorsSearch.html'                                        , 'CertAirportOperatorsSearchCtrl'                    ]])
      .state(['root.organizations.view.certAirportOperators.new'                                 , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certAirportOperators/certAirportOperatorsNew.html'                                           , 'CertAirportOperatorsNewCtrl'                       ]])
      .state(['root.organizations.view.certAirportOperators.new.chooseDocuments'                 , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.certAirportOperators.edit'                                , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certAirportOperators/certAirportOperatorsEdit.html'                                          , 'CertAirportOperatorsEditCtrl'                      ]])
      .state(['root.organizations.view.certAirportOperators.edit.chooseDocuments'                , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.certAirOperators'                                         , '/certAirOperators'                                                                                                                                                                                                                                     ])
      .state(['root.organizations.view.certAirOperators.search'                                  , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certAirOperators/certAirOperatorsSearch.html'                                                , 'CertAirOperatorsSearchCtrl'                        ]])
      .state(['root.organizations.view.certAirOperators.new'                                     , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certAirOperators/certAirOperatorsNew.html'                                                   , 'CertAirOperatorsNewCtrl'                           ]])
      .state(['root.organizations.view.certAirOperators.new.chooseDocuments'                     , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.certAirOperators.edit'                                    , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certAirOperators/certAirOperatorsEdit.html'                                                  , 'CertAirOperatorsEditCtrl'                          ]])
      .state(['root.organizations.view.certAirOperators.edit.chooseDocuments'                    , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.certAirNavigationServiceDeliverers'                       , '/certAirNavigationServiceDeliverers'                                                                                                                                                                                                                    ])
      .state(['root.organizations.view.certAirNavigationServiceDeliverers.search'                , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certAirNavigationServiceDeliverers/certAirNavigationServiceDeliverersSearch.html'            , 'CertAirNavigationServiceDeliverersSearchCtrl'      ]])
      .state(['root.organizations.view.certAirNavigationServiceDeliverers.new'                   , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certAirNavigationServiceDeliverers/certAirNavigationServiceDeliverersNew.html'               , 'CertAirNavigationServiceDeliverersNewCtrl'         ]])
      .state(['root.organizations.view.certAirNavigationServiceDeliverers.new.chooseDocuments'   , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.certAirNavigationServiceDeliverers.edit'                  , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certAirNavigationServiceDeliverers/certAirNavigationServiceDeliverersEdit.html'              , 'CertAirNavigationServiceDeliverersEditCtrl'        ]])
      .state(['root.organizations.view.certAirNavigationServiceDeliverers.edit.chooseDocuments'  , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.certAirCarriers'                                          , '/certAirCarriers'                                                                                                                                                                                                                                       ])
      .state(['root.organizations.view.certAirCarriers.search'                                   , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certAirCarriers/certAirCarriersSearch.html'                                                  , 'CertAirCarriersSearchCtrl'                         ]])
      .state(['root.organizations.view.certAirCarriers.new'                                      , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certAirCarriers/certAirCarriersNew.html'                                                     , 'CertAirCarriersNewCtrl'                            ]])
      .state(['root.organizations.view.certAirCarriers.new.chooseDocuments'                      , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.certAirCarriers.edit'                                     , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certAirCarriers/certAirCarriersEdit.html'                                                    , 'CertAirCarriersEditCtrl'                           ]])
      .state(['root.organizations.view.certAirCarriers.edit.chooseDocuments'                     , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.auditplans'                                               , '/auditplans'                                                                                                                                                                                                                                            ])
      .state(['root.organizations.view.auditplans.search'                                        , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/auditplans/auditplansSearch.html'                                                            , 'AuditplansSearchCtrl'                              ]])
      .state(['root.organizations.view.auditplans.new'                                           , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/auditplans/auditplansNew.html'                                                               , 'AuditplansNewCtrl'                                 ]])
      .state(['root.organizations.view.auditplans.edit'                                          , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/auditplans/auditplansEdit.html'                                                              , 'AuditplansEditCtrl'                                ]])
      .state(['root.organizations.view.staffManagement'                                          , '/staffManagement'                                                                                                                                                                                                                                       ])
      .state(['root.organizations.view.staffManagement.search'                                   , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/staffManagement/staffManagementSearch.html'                                                  , 'StaffManagementSearchCtrl'                         ]])
      .state(['root.organizations.view.staffManagement.new'                                      , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/staffManagement/staffManagementNew.html'                                                     , 'StaffManagementNewCtrl'                            ]])
      .state(['root.organizations.view.staffManagement.new.chooseEmployment'                     , '/chooseEmployment'                          , ['@root.organizations.view', 'js/gva/organizations/views/staffManagement/chooseEmployment.html'                                                       , 'ChooseEmploymentCtrl'                              ]])
      .state(['root.organizations.view.staffManagement.edit'                                     , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/staffManagement/staffManagementEdit.html'                                                    , 'StaffManagementEditCtrl'                           ]])
      .state(['root.organizations.view.staffManagement.edit.chooseEmployment'                    , '/chooseEmployment'                          , ['@root.organizations.view', 'js/gva/organizations/views/staffManagement/chooseEmployment.html'                                                       , 'ChooseEmploymentCtrl'                              ]])
      .state(['root.organizations.view.documentOthers'                                           , '/documentOthers'                                                                                                                                                                                                                                        ])
      .state(['root.organizations.view.documentOthers.search'                                    , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/documentOthers/othersSearch.html'                                                            , 'OrganizationDocOthersSearchCtrl'                   ]])
      .state(['root.organizations.view.documentOthers.new'                                       , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/documentOthers/othersNew.html'                                                               , 'OrganizationDocOthersNewCtrl'                      ]])
      .state(['root.organizations.view.documentOthers.new.choosePublisher'                       , '/choosepublisher?text&publisherTypeAlias'   , ['@root.organizations.view', 'js/gva/common/views/publishers/choosePublisher.html'                                                                    , 'ChoosePublisherCtrl'                               ]])
      .state(['root.organizations.view.documentOthers.edit'                                      , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/documentOthers/othersEdit.html'                                                              , 'OrganizationDocOthersEditCtrl'                     ]])
      .state(['root.organizations.view.documentOthers.edit.choosePublisher'                      , '/choosepublisher?text&publisherTypeAlias'   , ['@root.organizations.view', 'js/gva/common/views/publishers/choosePublisher.html'                                                                    , 'ChoosePublisherCtrl'                               ]])
      .state(['root.organizations.view.certGroundServiceOperators'                               , '/certGroundServiceOperators'                                                                                                                                                                                                                            ])
      .state(['root.organizations.view.certGroundServiceOperators.search'                        , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperators/certGroundServiceOperatorsSearch.html'                            , 'CertGroundServiceOperatorsSearchCtrl'              ]])
      .state(['root.organizations.view.certGroundServiceOperators.new'                           , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperators/certGroundServiceOperatorsNew.html'                               , 'CertGroundServiceOperatorsNewCtrl'                 ]])
      .state(['root.organizations.view.certGroundServiceOperators.new.chooseDocuments'           , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.certGroundServiceOperators.edit'                          , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperators/certGroundServiceOperatorsEdit.html'                              , 'CertGroundServiceOperatorsEditCtrl'                ]])
      .state(['root.organizations.view.certGroundServiceOperators.edit.chooseDocuments'          , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.groundServiceOperatorsSnoOperational'                     , '/groundServiceOperatorsSnoOperational'                                                                                                                                                                                                                  ])
      .state(['root.organizations.view.groundServiceOperatorsSnoOperational.search'              , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperatorsSnoOperational/certGroundServiceOperatorsSnoOperationalSearch.html', 'CertGroundServiceOperatorsSnoOperationalSearchCtrl']])
      .state(['root.organizations.view.groundServiceOperatorsSnoOperational.new'                 , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperatorsSnoOperational/certGroundServiceOperatorsSnoOperationalNew.html'   , 'CertGroundServiceOperatorsSnoOperationalNewCtrl'   ]])
      .state(['root.organizations.view.groundServiceOperatorsSnoOperational.new.chooseDocuments' , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.groundServiceOperatorsSnoOperational.edit'                , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperatorsSnoOperational/certGroundServiceOperatorsSnoOperationalEdit.html'  , 'CertGroundServiceOperatorsSnoOperationalEditCtrl'  ]])
      .state(['root.organizations.view.groundServiceOperatorsSnoOperational.edit.chooseDocuments', '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html', 'OrganizationsChooseDocumentsCtrl']])
      .state(['root.organizations.view.inspections'                                              , '/inspections'                                                                                                                                                                                                                                           ])
      .state(['root.organizations.view.inspections.search'                                       , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/inspections/organizationsInspectionsSearch.html'                                             , 'OrganizationsInspectionsSearchCtrl'                ]])
      .state(['root.organizations.view.inspections.new'                                          , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/inspections/organizationsInspectionsNew.html'                                                , 'OrganizationsInspectionsNewCtrl'                   ]])
      .state(['root.organizations.view.inspections.edit'                                         , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/inspections/organizationsInspectionsEdit.html'                                               , 'OrganizationsInspectionsEditCtrl'                  ]])
      .state(['root.organizations.view.approvals'                                                , '/approvals'                                                                                                                                                                                                                                             ])
      .state(['root.organizations.view.approvals.search'                                         , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/approvals/approvalsSearch.html'                                                              , 'ApprovalsSearchCtrl'                               ]])
      .state(['root.organizations.view.approvals.new'                                            , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/approvals/approvalsNew.html'                                                                 , 'ApprovalsNewCtrl'                                  ]])
      .state(['root.organizations.view.approvals.new.chooseDocuments'                            , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.approvals.new.chooseLimitation'                          , '/chooseLimitation?index&limitationAlias'     , ['@root.organizations.view', 'js/gva/organizations/views/chooseLimitation/chooseLimitation.html'                                                      , 'ChooseLimitationCtrl'                              ]])
      .state(['root.organizations.view.approvals.edit'                                           , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/approvals/approvalsEdit.html'                                                                , 'ApprovalsEditCtrl'                                 ]])
      .state(['root.organizations.view.approvals.edit.chooseDocuments'                           , '/chooseDocuments?documentTypes'             , ['@root.organizations.view', 'js/gva/organizations/views/chooseDocuments/organizationsChooseDocuments.html'                                           , 'OrganizationsChooseDocumentsCtrl'                  ]])
      .state(['root.organizations.view.approvals.edit.chooseLimitation'                          , '/chooseLimitation?index&limitationAlias'    , ['@root.organizations.view', 'js/gva/organizations/views/chooseLimitation/chooseLimitation.html'                                                      , 'ChooseLimitationCtrl'                              ]])
      .state(['root.organizations.view.staffExaminers'                                           , '/staffExaminers'                                                                                                                                                                                                                                        ])
      .state(['root.organizations.view.staffExaminers.search'                                    , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/staffExaminers/staffExaminersSearch.html'                                                    , 'StaffExaminersSearchCtrl'                          ]])
      .state(['root.organizations.view.staffExaminers.new'                                       , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/staffExaminers/staffExaminersNew.html'                                                       , 'StaffExaminersNewCtrl'                             ]])
      .state(['root.organizations.view.staffExaminers.edit'                                      , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/staffExaminers/staffExaminersEdit.html'                                                      , 'StaffExaminersEditCtrl'                            ]])
      .state(['root.organizations.view.regAirportOperators'                                      , '/regAirportOperators'                                                                                                                                                                                                                                   ])
      .state(['root.organizations.view.regAirportOperators.search'                               , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/regAirportOperators/regAirportOperatorsSearch.html'                                          , 'RegAirportOperatorsSearchCtrl'                     ]])
      .state(['root.organizations.view.regAirportOperators.new'                                  , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/regAirportOperators/regAirportOperatorsNew.html'                                             , 'RegAirportOperatorsNewCtrl'                        ]])
      .state(['root.organizations.view.regAirportOperators.edit'                                 , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/regAirportOperators/regAirportOperatorsEdit.html'                                            , 'RegAirportOperatorsEditCtrl'                       ]])
      .state(['root.organizations.view.regGroundServiceOperators'                                , '/regGroundServiceOperators'                                                                                                                                                                                                                             ])
      .state(['root.organizations.view.regGroundServiceOperators.search'                         , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/regGroundServiceOperators/regGroundServiceOperatorsSearch.html'                              , 'RegGroundServiceOperatorsSearchCtrl'               ]])
      .state(['root.organizations.view.regGroundServiceOperators.new'                            , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/regGroundServiceOperators/regGroundServiceOperatorsNew.html'                                 , 'RegGroundServiceOperatorsNewCtrl'                  ]])
      .state(['root.organizations.view.regGroundServiceOperators.edit'                           , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/regGroundServiceOperators/regGroundServiceOperatorsEdit.html'                                , 'RegGroundServiceOperatorsEditCtrl'                 ]])
      .state(['root.organizations.view.recommendations'                                          , '/recommendations'                                                                                                                                                                                                                                      ])
      .state(['root.organizations.view.recommendations.search'                                   , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/recommendations/recommendationsSearch.html'                                                  , 'RecommendationsSearchCtrl'                         ]])
      .state(['root.organizations.view.recommendations.new'                                      , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/recommendations/recommendationsNew.html'                                                     , 'RecommendationsNewCtrl'                            ]])
      .state(['root.organizations.view.recommendations.new.chooseAudits'                         , '/chooseAudits'                              , ['@root.organizations.view', 'js/gva/organizations/views/recommendations/audits/chooseAudits.html'                                                    , 'ChooseAuditsCtrl'                                  ]])
      .state(['root.organizations.view.recommendations.new.editDisparity'                        , '/:childInd'                                 , ['@root.organizations.view', 'js/gva/organizations/views/inspections/organizationsInspectionsEdit.html'                                               , 'OrganizationsInspectionsEditCtrl'                  ]])
      .state(['root.organizations.view.recommendations.edit'                                     , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/recommendations/recommendationsEdit.html'                                                    , 'RecommendationsEditCtrl'                           ]])
      .state(['root.organizations.view.recommendations.edit.chooseAudits'                        , '/chooseAudits'                              , ['@root.organizations.view', 'js/gva/organizations/views/recommendations/audits/chooseAudits.html'                                                    , 'ChooseAuditsCtrl'                                  ]])
      .state(['root.organizations.view.recommendations.edit.editDisparity'                       , '/:childInd'                                 , ['@root.organizations.view', 'js/gva/organizations/views/inspections/organizationsInspectionsEdit.html'                                               , 'OrganizationsInspectionsEditCtrl'                  ]])
      .state(['root.organizations.view.documentApplications'                                     , '/documentApplications'                                                                                                                                                                                                                                  ])
      .state(['root.organizations.view.documentApplications.search'                              , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/documentApplications/organizationsDocApplicationsSearch.html'                                , 'OrganizationsDocApplicationsSearchCtrl'            ]])
      .state(['root.organizations.view.documentApplications.new'                                 , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/documentApplications/organizationsDocApplicationsNew.html'                                   , 'OrganizationsDocApplicationsNewCtrl'               ]])
      .state(['root.organizations.view.documentApplications.edit'                                , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/documentApplications/organizationsDocApplicationsEdit.html'                                  , 'OrganizationsDocApplicationsEditCtrl'              ]])
      .state(['root.organizations.view.inventory'                                                , '/inventory'                                 , ['@root.organizations.view', 'js/gva/organizations/views/inventory/organizationsInventorySearch.html'                                                 , 'OrganizationsInventorySearchCtrl'                  ]]);

  }]).config(['$stateProvider', function ($stateProvider) {
      $stateProvider
      .state(['root.airports'                                   , '/airports?name&icao'                                                                                                                                                        ])
      .state(['root.airports.search'                            , ''                                         , ['@root'              , 'js/gva/airports/views/airportsSearch.html'                             , 'AirportsSearchCtrl'            ]])
      .state(['root.airports.new'                               , '/new'                                     , ['@root'              , 'js/gva/airports/views/airportsNew.html'                                , 'AirportsNewCtrl'               ]])
      .state(['root.airports.view'                              , '/:id?appId&filter'                        , ['@root'              , 'js/gva/airports/views/airportsView.html'                               , 'AirportsViewCtrl'              ]])
      .state(['root.airports.view.edit'                         , '/airportData'                             , ['@root'              , 'js/gva/airports/views/airportData/airportDataEdit.html'                , 'AirportDataEditCtrl'           ]])
      .state(['root.airports.view.others'                       , '/others'                                                                                                                                                                     ])
      .state(['root.airports.view.others.search'                , ''                                         , ['@root.airports.view', 'js/gva/airports/views/documentOthers/othersSearch.html'                 , 'AirportOthersSearchCtrl'       ]])
      .state(['root.airports.view.others.new'                   , '/new'                                     , ['@root.airports.view', 'js/gva/airports/views/documentOthers/othersNew.html'                    , 'AirportOthersNewCtrl'          ]])
      .state(['root.airports.view.others.edit'                  , '/:ind'                                    , ['@root.airports.view', 'js/gva/airports/views/documentOthers/othersEdit.html'                   , 'AirportOthersEditCtrl'         ]])
      .state(['root.airports.view.owners'                       , '/owners'                                                                                                                                                                    ])
      .state(['root.airports.view.owners.search'                , ''                                         , ['@root.airports.view', 'js/gva/airports/views/documentOwners/ownersSearch.html'                 , 'AirportOwnersSearchCtrl'       ]])
      .state(['root.airports.view.owners.new'                   , '/new'                                     , ['@root.airports.view', 'js/gva/airports/views/documentOwners/ownersNew.html'                    , 'AirportOwnersNewCtrl'          ]])
      .state(['root.airports.view.owners.edit'                  , '/:ind'                                    , ['@root.airports.view', 'js/gva/airports/views/documentOwners/ownersEdit.html'                   , 'AirportOwnersEditCtrl'         ]])
      .state(['root.airports.view.opers'                        , '/opers'                                                                                                                                                                      ])
      .state(['root.airports.view.opers.search'                 , ''                                         , ['@root.airports.view', 'js/gva/airports/views/certOpers/opersSearch.html'                       , 'AirportOpersSearchCtrl'        ]])
      .state(['root.airports.view.opers.new'                    , '/new'                                     , ['@root.airports.view', 'js/gva/airports/views/certOpers/opersNew.html'                          , 'AirportOpersNewCtrl'           ]])
      .state(['root.airports.view.opers.new.chooseDocuments'    , '/chooseDocuments?documentTypes'           , ['@root.airports.view', 'js/gva/airports/views/chooseDocuments/chooseDocuments.html'             , 'AirportsChooseDocumentsCtrl'   ]])
      .state(['root.airports.view.opers.edit'                   , '/:ind'                                    , ['@root.airports.view', 'js/gva/airports/views/certOpers/opersEdit.html'                         , 'AirportOpersEditCtrl'          ]])
      .state(['root.airports.view.opers.edit.chooseDocuments'   , '/chooseDocuments?documentTypes'           , ['@root.airports.view', 'js/gva/airports/views/chooseDocuments/chooseDocuments.html'             , 'AirportsChooseDocumentsCtrl'   ]])
      .state(['root.airports.view.applications'                 , '/applications'                                                                                                                                                              ])
      .state(['root.airports.view.applications.search'          , ''                                         , ['@root.airports.view', 'js/gva/airports/views/documentApplications/docApplicationsSearch.html'  , 'AirportApplicationsSearchCtrl' ]])
      .state(['root.airports.view.applications.new'             , '/new'                                     , ['@root.airports.view', 'js/gva/airports/views/documentApplications/docApplicationsNew.html'     , 'AirportApplicationsNewCtrl'    ]])
      .state(['root.airports.view.applications.edit'            , '/:ind'                                    , ['@root.airports.view', 'js/gva/airports/views/documentApplications/docApplicationsEdit.html'    , 'AirportApplicationsEditCtrl'   ]])
      .state(['root.airports.view.inspections'                  , '/inspections'                                                                                                                                                               ])
      .state(['root.airports.view.inspections.search'           , ''                                         , ['@root.airports.view', 'js/gva/airports/views/inspections/airportsInspectionsSearch.html'       , 'AirportsInspectionsSearchCtrl' ]])
      .state(['root.airports.view.inspections.new'              , '/new'                                     , ['@root.airports.view', 'js/gva/airports/views/inspections/airportsInspectionsNew.html'          , 'AirportsInspectionsNewCtrl'    ]])
      .state(['root.airports.view.inspections.edit'             , '/:ind'                                    , ['@root.airports.view', 'js/gva/airports/views/inspections/airportsInspectionsEdit.html'         , 'AirportsInspectionsEditCtrl'   ]])
      .state(['root.airports.view.inventory'                    , '/inventory'                               , ['@root.airports.view', 'js/gva/airports/views/inventory/inventorySearch.html'                   , 'AirportInventorySearchCtrl'    ]]);
    }]).config(['$stateProvider', function ($stateProvider) {
      $stateProvider
      .state(['root.equipments'                                 , '/equipments?name'                                                                                                                                                                ])
      .state(['root.equipments.search'                          , ''                                         , ['@root'                , 'js/gva/equipments/views/equipmentsSearch.html'                             , 'EquipmentsSearchCtrl'           ]])
      .state(['root.equipments.new'                             , '/new'                                     , ['@root'                , 'js/gva/equipments/views/equipmentsNew.html'                                , 'EquipmentsNewCtrl'              ]])
      .state(['root.equipments.view'                            , '/:id?appId&filter'                        , ['@root'                , 'js/gva/equipments/views/equipmentsView.html'                               , 'EquipmentsViewCtrl'             ]])
      .state(['root.equipments.view.edit'                       , '/equipmentData'                           , ['@root'                , 'js/gva/equipments/views/equipmentData/equipmentDataEdit.html'              , 'EquipmentDataEditCtrl'          ]])
      .state(['root.equipments.view.others'                     , '/others'                                                                                                                                                                         ])
      .state(['root.equipments.view.others.search'              , ''                                         , ['@root.equipments.view', 'js/gva/equipments/views/documentOthers/othersSearch.html'                 , 'EquipmentOthersSearchCtrl'      ]])
      .state(['root.equipments.view.others.new'                 , '/new'                                     , ['@root.equipments.view', 'js/gva/equipments/views/documentOthers/othersNew.html'                    , 'EquipmentOthersNewCtrl'         ]])
      .state(['root.equipments.view.others.edit'                , '/:ind'                                    , ['@root.equipments.view', 'js/gva/equipments/views/documentOthers/othersEdit.html'                   , 'EquipmentOthersEditCtrl'        ]])
      .state(['root.equipments.view.owners'                     , '/owners'                                                                                                                                                                         ])
      .state(['root.equipments.view.owners.search'              , ''                                         , ['@root.equipments.view', 'js/gva/equipments/views/documentOwners/ownersSearch.html'                 , 'EquipmentOwnersSearchCtrl'      ]])
      .state(['root.equipments.view.owners.new'                 , '/new'                                     , ['@root.equipments.view', 'js/gva/equipments/views/documentOwners/ownersNew.html'                    , 'EquipmentOwnersNewCtrl'         ]])
      .state(['root.equipments.view.owners.edit'                , '/:ind'                                    , ['@root.equipments.view', 'js/gva/equipments/views/documentOwners/ownersEdit.html'                   , 'EquipmentOwnersEditCtrl'        ]])
      .state(['root.equipments.view.opers'                      , '/opers'                                                                                                                                                                          ])
      .state(['root.equipments.view.opers.search'               , ''                                         , ['@root.equipments.view', 'js/gva/equipments/views/certOpers/opersSearch.html'                       , 'EquipmentOpersSearchCtrl'       ]])
      .state(['root.equipments.view.opers.new'                  , '/new'                                     , ['@root.equipments.view', 'js/gva/equipments/views/certOpers/opersNew.html'                          , 'EquipmentOpersNewCtrl'          ]])
      .state(['root.equipments.view.opers.new.chooseDocuments'  , '/chooseDocuments?documentTypes'           , ['@root.equipments.view', 'js/gva/equipments/views/chooseDocuments/chooseDocuments.html'             , 'EquipmentsChooseDocumentsCtrl'  ]])
      .state(['root.equipments.view.opers.edit'                 , '/:ind'                                    , ['@root.equipments.view', 'js/gva/equipments/views/certOpers/opersEdit.html'                         , 'EquipmentOpersEditCtrl'         ]])
      .state(['root.equipments.view.opers.edit.chooseDocuments' , '/chooseDocuments?documentTypes'           , ['@root.equipments.view', 'js/gva/equipments/views/chooseDocuments/chooseDocuments.html'             , 'EquipmentsChooseDocumentsCtrl'  ]])
      .state(['root.equipments.view.applications'               , '/applications'                                                                                                                                                                   ])
      .state(['root.equipments.view.applications.search'        , ''                                         , ['@root.equipments.view', 'js/gva/equipments/views/documentApplications/docApplicationsSearch.html'  , 'EquipmentApplicationsSearchCtrl']])
      .state(['root.equipments.view.applications.new'           , '/new'                                     , ['@root.equipments.view', 'js/gva/equipments/views/documentApplications/docApplicationsNew.html'     , 'EquipmentApplicationsNewCtrl'   ]])
      .state(['root.equipments.view.applications.edit'          , '/:ind'                                    , ['@root.equipments.view', 'js/gva/equipments/views/documentApplications/docApplicationsEdit.html'    , 'EquipmentApplicationsEditCtrl'  ]])
      .state(['root.equipments.view.inspections'                , '/inspections'                                                                                                                                                                    ])
      .state(['root.equipments.view.inspections.search'         , ''                                         , ['@root.equipments.view', 'js/gva/equipments/views/inspections/equipmentsInspectionsSearch.html'     , 'EquipmentsInspectionsSearchCtrl']])
      .state(['root.equipments.view.inspections.new'            , '/new'                                     , ['@root.equipments.view', 'js/gva/equipments/views/inspections/equipmentsInspectionsNew.html'        , 'EquipmentsInspectionsNewCtrl'   ]])
      .state(['root.equipments.view.inspections.edit'           , '/:ind'                                    , ['@root.equipments.view', 'js/gva/equipments/views/inspections/equipmentsInspectionsEdit.html'       , 'EquipmentsInspectionsEditCtrl'  ]])
      .state(['root.equipments.view.inventory'                  , '/inventory'                               , ['@root.equipments.view', 'js/gva/equipments/views/inventory/inventorySearch.html'                   , 'EquipmentInventorySearchCtrl'   ]]);
    }]);
}(angular));
