/*jshint maxlen:false*/
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
    'scaffolding',
    'l10n',
    'l10n-tools',
    'scrollto'
  ]).config(['scaffoldingProvider', function (scaffoldingProvider) {
    scaffoldingProvider.form({
      name: 'gvaApplicationDocument',
      templateUrl: 'js/gva/applications/forms/applicationDocument.html',
      controller: 'AppDocumentCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationExamSystData',
      templateUrl: 'js/gva/applications/forms/appExSystData.html',
      controller: 'AppExSystDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaCommonSelectPerson',
      templateUrl: 'js/gva/common/forms/commonSelectPerson.html',
      controller: 'CommonSelectPersonCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationStage',
      templateUrl: 'js/gva/applications/forms/applicationStage.html'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationSelectOrganization',
      templateUrl: 'js/gva/applications/forms/applicationSelectOrganization.html',
      controller: 'AppSelectOrganizationCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationSelectAircraft',
      templateUrl: 'js/gva/applications/forms/applicationSelectAircraft.html',
      controller: 'AppSelectAircraftCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationSelectAirport',
      templateUrl: 'js/gva/applications/forms/applicationSelectAirport.html',
      controller: 'AppSelectAirportCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaApplicationSelectEquipment',
      templateUrl: 'js/gva/applications/forms/applicationSelectEquipment.html',
      controller: 'AppSelectEquipmentCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonData',
      templateUrl: 'js/gva/persons/forms/personData.html',
      controller: 'PersonDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaInspectorData',
      templateUrl: 'js/gva/persons/forms/inspectorData.html',
      controller: 'InspDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaExaminerData',
      templateUrl: 'js/gva/persons/forms/examinerData.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonReport',
      templateUrl: 'js/gva/persons/forms/personReport.html',
      controller: 'PersonReportCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonAddress',
      templateUrl: 'js/gva/persons/forms/personAddress.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentId',
      templateUrl: 'js/gva/persons/forms/personDocumentId.html',
      controller: 'PersonDocIdCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentEducation',
      templateUrl: 'js/gva/persons/forms/personDocumentEducation.html',
      controller: 'PersonDocumentEducationCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonStatus',
      templateUrl: 'js/gva/persons/forms/personStatus.html',
      controller: 'PersonStatusCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentMedical',
      templateUrl: 'js/gva/persons/forms/personDocumentMedical.html',
      controller: 'PersonDocumentMedicalCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentEmployment',
      templateUrl: 'js/gva/persons/forms/personDocumentEmployment.html',
      controller: 'PersonDocumentEmploymentCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentLangCert',
      templateUrl: 'js/gva/persons/forms/personDocumentLangCert.html',
      controller: 'PersonDocumentLangCertCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonCommonDoc',
      templateUrl: 'js/gva/persons/forms/personCommonDocument.html',
      controller: 'PersonCommonDocCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonCommonDocClassification',
      templateUrl: 'js/gva/persons/forms/personCommonDocClassification.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentCheck',
      templateUrl: 'js/gva/persons/forms/personDocumentCheck.html',
      controller: 'PersonDocumentCheckCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentTraining',
      templateUrl: 'js/gva/persons/forms/personDocumentTraining.html',
      controller: 'PersonDocumentTrainingCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonDocumentOther',
      templateUrl: 'js/gva/persons/forms/personDocumentOther.html',
      controller: 'PersonDocumentOtherCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonFlyingExperience',
      templateUrl: 'js/gva/persons/forms/personFlyingExperience.html',
      controller: 'PersonFlyingExperienceCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaRatingEdition',
      templateUrl: 'js/gva/persons/forms/personRatingEdition.html',
      controller: 'PersonRatingEditionCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaRating',
      templateUrl: 'js/gva/persons/forms/personRating.html',
      controller: 'PersonRatingCtrl'
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
      name: 'gvaLicenceStatus',
      templateUrl: 'js/gva/persons/forms/personLicenceStatus.html'
    });
    scaffoldingProvider.form({
      name: 'gvaQualificationState',
      templateUrl: 'js/gva/persons/forms/qualificationfState.html'
    });
    scaffoldingProvider.form({
      name: 'gvaPersonExam',
      templateUrl: 'js/gva/persons/forms/personExam.html',
      controller: 'PersonExamCtrl'
    });
      scaffoldingProvider.form({
      name: 'gvaAircraftNewWizzard',
      templateUrl: 'js/gva/aircrafts/forms/aircraftNewWizzard.html',
      controller: 'AircraftNewWizzardCtrl'
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
      name: 'gvaAircraftCertAirworthinessView',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertAirworthinessView.html',
      controller: 'AircraftCertAirworthinessViewCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertNoiseView',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertNoiseView.html'
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
      name: 'gvaAircraftCertAirworthiness',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertAirworthiness.html',
      controller: 'AircraftCertAirworthinessCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertAirworthinessForm15',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertAirworthinessForm15.html',
      controller: 'AircraftCertAirworthinessForm15Ctrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertNoise',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertNoise.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRadio',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertRadio.html'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRegFm',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertRegFM.html',
      controller: 'AircraftCertRegFMCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftCertRegDeregFm',
      templateUrl: 'js/gva/aircrafts/forms/aircraftCertRegDeregFM.html',
      controller: 'AircraftCertRegDeregFMCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftData',
      templateUrl: 'js/gva/aircrafts/forms/aircraftData.html',
      controller: 'AircraftDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentDebtFm',
      templateUrl: 'js/gva/aircrafts/forms/aircraftDocumentDebtFM.html',
      controller: 'AircraftDocumentDebtFMCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaAircraftDocumentDebtClose',
      templateUrl: 'js/gva/aircrafts/forms/aircraftDocumentDebtClose.html'
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
      name: 'gvaAircraftDocumentOwner',
      templateUrl: 'js/gva/aircrafts/forms/aircraftDocumentOwner.html'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationData',
      templateUrl: 'js/gva/organizations/forms/organizationData.html'
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
      name: 'gvaOrganizationStaffManagement',
      templateUrl: 'js/gva/organizations/forms/organizationStaffManagement.html',
      controller: 'OrgStaffManagementCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationAwExaminer',
      templateUrl: 'js/gva/organizations/forms/organizationAwExaminer.html',
      controller: 'OrganizationAwExaminerCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationDocumentOther',
      templateUrl: 'js/gva/organizations/forms/organizationDocumentOther.html',
      controller: 'OrgDocumentOtherCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationEquipment',
      templateUrl: 'js/gva/organizations/forms/organizationEquipment.html',
      controller: 'OrganizationEquipmentCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaOrganizationApproval',
      templateUrl: 'js/gva/organizations/forms/organizationApproval.html',
      controller: 'OrganizationApprovalCtrl'
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
      name: 'gvaCommonInspectionData',
      templateUrl: 'js/gva/common/forms/commonInspectionData.html',
      controller: 'CommonInspectionDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaCommonInspectionDetails',
      templateUrl: 'js/gva/common/forms/commonInspectionDetails.html',
      controller: 'CommonInspectionDetailsCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaCommonDocumentApplication',
      templateUrl: 'js/gva/common/forms/commonDocumentApplication.html',
      controller: 'CommonDocumentApplicationCtrl'
    });
    scaffoldingProvider.form({
      name: 'gvaCommonInspectors',
      templateUrl: 'js/gva/common/forms/commonInspectors.html',
      controller: 'CommonInspectorsCtrl'
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
    scaffoldingProvider.form({
      name: 'gvaAircraftInspector',
      templateUrl: 'js/gva/aircrafts/forms/aircraftInspector.html',
      controller: 'AircraftInspectorCtrl'
    });
  }]).config(['scModalProvider', function (scModalProvider) {
    scModalProvider
     .modal('choosePublisher' , 'js/gva/common/modals/publishers/choosePublisherModal.html'  , 'ChoosePublisherModalCtrl' )
     .modal('choosePerson'    , 'js/gva/common/modals/persons/choosePersonModal.html'        , 'ChoosePersonModalCtrl'    )
     .modal('choosePersons'   , 'js/gva/common/modals/persons/choosePersonsModal.html'       , 'ChoosePersonsModalCtrl'   )
     .modal('chooseAppExams'  , 'js/gva/common/modals/persons/chooseAppExamsModal.html'      , 'ChooseAppExamsModalCtrl'  )
     .modal('newPerson'       , 'js/gva/common/modals/persons/newPersonModal.html'           , 'NewPersonModalCtrl'       )
     .modal('chooseInspectors', 'js/gva/common/modals/inspectors/chooseInspectorsModal.html' , 'ChooseInspectorsModalCtrl')
     .modal('editDisparity'   , 'js/gva/common/modals/disparities/editDisparityModal.html'   , 'EditDisparityModalCtrl'   )
     .modal('viewApplication' , 'js/gva/common/modals/applications/viewApplicationModal.html', 'ViewApplicationModalCtrl' )
     .modal('linkApplication' , 'js/gva/common/modals/integration/linkAppModal.html'         , 'LinkAppModalCtrl'         );
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.applications'                                  , '/applications?set'                                                                                                                                                                                                        ])
      .state(['root.applications.search'                           , '?fromDate&toDate&personLin&aircraftIcao&organizationUin&stageId&inspectorId&applicationTypeId', ['@root'                  , 'js/gva/applications/views/applicationsSearch.html'             , 'ApplicationsSearchCtrl'        ]])
      .state(['root.applications.new'                              , '/new?lotId'                                                                                   , ['@root'                  , 'js/gva/applications/views/applicationsNew.html'                , 'ApplicationsNewCtrl'           ]])
      .state(['root.applications.edit'                             , '/:id'                                                                                         , ['@root'                  , 'js/gva/applications/views/applicationsEdit.html'               , 'ApplicationsEditCtrl'          ]])
      .state(['root.applications.edit.data'                        , '/:lotId/document/:ind'                                                                        , ['@root.applications.edit', 'js/gva/applications/views/applicationsEditPart.html'           , 'AppEditPartCtrl'               ]])
      .state(['root.applications.edit.case'                        , '/case'                                                                                        , ['@root.applications.edit', 'js/gva/applications/views/case/applicationsEditCase.html'      , 'ApplicationsEditCaseCtrl'      ]])
      .state(['root.applications.edit.case.newFile'                , '/newFile?docId&docFileId&setPartAlias'                                                        , ['@root.applications.edit', 'js/gva/applications/views/case/applicationsEditNewFile.html'   , 'ApplicationsEditNewFileCtrl'   ]])
      .state(['root.applications.edit.case.newDocFile'             , '/newDocFile?docId'                                                                            , ['@root.applications.edit', 'js/gva/applications/views/case/applicationsEditNewDocFile.html', 'ApplicationsEditNewDocFileCtrl']])
      .state(['root.applications.edit.case.childDoc'               , '/childDoc?parentDocId'                                                                        , ['@root.applications.edit', 'js/gva/applications/views/case/applicationsEditChildDoc.html'  , 'ApplicationsEditChildDocCtrl'  ]])
      .state(['root.applications.edit.case.addPart'                , '/addPart?docId&docFileId&setPartAlias&caseTypeId&lotId'                                       , ['@root.applications.edit', 'js/gva/applications/views/case/applicationsEditAddPart.html'   , 'ApplicationsEditAddPartCtrl'   ]])
      .state(['root.applications.edit.case.linkPart'               , '/linkPart?docFileId'                                                                          , ['@root.applications.edit', 'js/gva/applications/views/case/applicationsEditLinkPart.html'  , 'ApplicationsEditLinkPartCtrl'  ]])
      .state(['root.applications.edit.stages'                      , '/stages'                                                                                      , ['@root.applications.edit', 'js/gva/applications/views/stages/applicationsEditStages.html'  , 'ApplicationsEditStagesCtrl'    ]])
      .state(['root.applications.edit.examSyst'                    , '/:lotId/examSyst/:ind'                                                                        , ['@root.applications.edit', 'js/gva/applications/views/appExamSyst/appExamSyst.html'        , 'AppExamSystCtrl'               ]]);
  }]).config(['scModalProvider', function (scModalProvider) {
    scModalProvider
     .modal('chooseAppType'        , 'js/gva/applications/modals/applicationTypes/chooseAppTypesModal.html'    , 'ChooseAppTypesModalCtrl'            )
     .modal('addCaseTypes'         , 'js/gva/applications/modals/persons/addCaseTypesModal.html'               , 'AddCaseTypesModalCtrl'      , 'xs-2')
     .modal('chooseOrganization'   , 'js/gva/applications/modals/organizations/chooseOrganizationModal.html'   , 'ChooseOrganizationModalCtrl'        )
     .modal('newOrganization'      , 'js/gva/applications/modals/organizations/newOrganizationModal.html'      , 'NewOrganizationModalCtrl'           )
     .modal('chooseAircraft'       , 'js/gva/applications/modals/aircrafts/chooseAircraftModal.html'           , 'ChooseAircraftModalCtrl'            )
     .modal('newAircraft'          , 'js/gva/applications/modals/aircrafts/newAircraftModal.html'              , 'NewAircraftModalCtrl'               )
     .modal('chooseAirport'        , 'js/gva/applications/modals/airports/chooseAirportModal.html'             , 'ChooseAirportModalCtrl'             )
     .modal('newAirport'           , 'js/gva/applications/modals/airports/newAirportModal.html'                , 'NewAirportModalCtrl'                )
     .modal('chooseEquipment'      , 'js/gva/applications/modals/equipments/chooseEquipmentModal.html'         , 'ChooseEquipmentModalCtrl'           )
     .modal('newEquipment'         , 'js/gva/applications/modals/equipments/newEquipmentModal.html'            , 'NewEquipmentModalCtrl'              )
     .modal('chooseDoc'            , 'js/gva/applications/modals/docs/chooseDocModal.html'                     , 'ChooseDocModalCtrl'                 )
     .modal('editDocStage'         , 'js/gva/applications/modals/stages/editDocStageModal.html'                , 'EditDocStageModalCtrl'              )
     .modal('endDocStage'          , 'js/gva/applications/modals/stages/endDocStageModal.html'                 , 'EndDocStageModalCtrl'               )
     .modal('nextDocStage'         , 'js/gva/applications/modals/stages/nextDocStageModal.html'                , 'NextDocStageModalCtrl'              )
     .modal('newAppStage'          , 'js/gva/applications/modals/stages/newAppStageModal.html'                 , 'NewAppStageModalCtrl'               )
     .modal('editAppStage'         , 'js/gva/applications/modals/stages/editAppStageModal.html'                , 'EditAppStageModalCtrl'              )
     .modal('appExSystChooseExams' , 'js/gva/applications/modals/examSyst/addExamsModal.html'                  , 'AddExamsModalCtrl'          , 'xs-2');
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.persons'                                               , '/persons?exact&lin&uin&names&licences&ratings&organization&caseType'                                                                                                                                                                                                                      ])
      .state(['root.persons.search'                                        , ''                                                                                              , ['@root'                                            , 'js/gva/persons/views/personsSearch.html'                                                     , 'PersonsSearchCtrl'               ]])
      .state(['root.persons.new'                                           , '/new'                                                                                          , ['@root'                                            , 'js/gva/persons/views/personsNew.html'                                                        , 'PersonsNewCtrl'                  ]])
      .state(['root.persons.securityExam'                                  , '/securityExam'                                                                                 , ['@root'                                            , 'js/gva/persons/views/exams/securityExamBatch.html'                                           , 'SecurityExamBatchCtrl'           ]])
      .state(['root.persons.securityExam.part'                             , '/:id'                                                                                          , ['@root.persons.securityExam'                       , 'js/gva/persons/views/exams/securityExamBatchPart.html'                                       , 'SecurityExamBatchPartCtrl'       ]])
      .state(['root.persons.view'                                          , '/:id?caseTypeId&appId&set'                                                                     , ['@root'                                            , 'js/gva/persons/views/personsView.html'                                                       , 'PersonsViewCtrl'                 ]])
      .state(['root.persons.view.edit'                                     , '/personInfo'                                                                                   , ['@root'                                            , 'js/gva/persons/views/personInfo/personInfoEdit.html'                                         , 'PersonInfoEditCtrl'              ]])
      .state(['root.persons.view.reports'                                  , '/reports'                                                                                                                                                                                                                                                                                 ])
      .state(['root.persons.view.reports.search'                           , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/reports/reportsSearch.html'                                             , 'ReportsSearchCtrl'               ]])
      .state(['root.persons.view.reports.new'                              , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/reports/reportsNew.html'                                                , 'ReportsNewCtrl'                  ]])
      .state(['root.persons.view.reports.edit'                             , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/reports/reportsEdit.html'                                               , 'ReportsEditCtrl'                 ]])
      .state(['root.persons.view.addresses'                                , '/addresses'                                                                                                                                                                                                                                                                               ])
      .state(['root.persons.view.addresses.search'                         , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/addresses/addrSearch.html'                                              , 'AddressesSearchCtrl'             ]])
      .state(['root.persons.view.addresses.new'                            , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/addresses/addrNew.html'                                                 , 'AddressesNewCtrl'                ]])
      .state(['root.persons.view.addresses.edit'                           , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/addresses/addrEdit.html'                                                , 'AddressesEditCtrl'               ]])
      .state(['root.persons.view.statuses'                                 , '/statuses'                                                                                                                                                                                                                                                                                ])
      .state(['root.persons.view.statuses.search'                          , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/statuses/statusesSearch.html'                                           , 'StatusesSearchCtrl'              ]])
      .state(['root.persons.view.statuses.new'                             , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/statuses/statusesNew.html'                                              , 'StatusesNewCtrl'                 ]])
      .state(['root.persons.view.statuses.edit'                            , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/statuses/statusesEdit.html'                                             , 'StatusesEditCtrl'                ]])
      .state(['root.persons.view.documentIds'                              , '/documentIds'                                                                                                                                                                                                                                                                             ])
      .state(['root.persons.view.documentIds.search'                       , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/documentIds/idsSearch.html'                                             , 'DocumentIdsSearchCtrl'           ]])
      .state(['root.persons.view.documentIds.new'                          , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/documentIds/idsNew.html'                                                , 'DocumentIdsNewCtrl'              ]])
      .state(['root.persons.view.documentIds.edit'                         , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/documentIds/idsEdit.html'                                               , 'DocumentIdsEditCtrl'             ]])
      .state(['root.persons.view.documentEducations'                       , '/documentEducations'                                                                                                                                                                                                                                                                      ])
      .state(['root.persons.view.documentEducations.search'                , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/documentEducations/edusSearch.html'                                     , 'DocumentEducationsSearchCtrl'    ]])
      .state(['root.persons.view.documentEducations.new'                   , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/documentEducations/edusNew.html'                                        , 'DocumentEducationsNewCtrl'       ]])
      .state(['root.persons.view.documentEducations.edit'                  , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/documentEducations/edusEdit.html'                                       , 'DocumentEducationsEditCtrl'      ]])
      .state(['root.persons.view.licences'                                 , '/licences'                                                                                                                                                                                                                                                                                ])
      .state(['root.persons.view.licences.search'                          , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/licences/licencesSearch.html'                                           , 'LicencesSearchCtrl'              ]])
      .state(['root.persons.view.licences.new'                             , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/licences/licencesNew.html'                                              , 'LicencesNewCtrl'                 ]])
      .state(['root.persons.view.licences.view'                            , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/licences/licencesView.html'                                             , 'LicencesViewCtrl'                ]])
      .state(['root.persons.view.licences.view.editions'                   , '/licenceEditions'                                                                                                                                                                                                                                                                         ])
      .state(['root.persons.view.licences.view.editions.new'               , '/new'                                                                                          , ['@root.persons.view.licences.view'                 , 'js/gva/persons/views/licences/licenceEditions/licenceEditionsNew.html'                       , 'LicenceEditionsNewCtrl'          ]])
      .state(['root.persons.view.licences.view.editions.edit'              , '/:index'                                                                                       , ['@root.persons.view.licences.view'                 , 'js/gva/persons/views/licences/licenceEditions/licenceEditionsEdit.html'                      , 'LicenceEditionsEditCtrl'         ]])
      .state(['root.persons.view.licences.view.editions.edit.ratings'      , '/ratings?licenceTypeCode'                                                                      , ['@root.persons.view.licences.view.editions.edit'   , 'js/gva/persons/views/licences/licenceEditions/licenceEditionsEditRatings.html'               , 'LicenceEditionsEditRatingsCtrl'  ]])
      .state(['root.persons.view.licences.view.editions.edit.exams'        , '/exams'                                                                                        , ['@root.persons.view.licences.view.editions.edit'   , 'js/gva/persons/views/licences/licenceEditions/licenceEditionsEditExams.html'                 , 'LicenceEditionsEditExamsCtrl'    ]])
      .state(['root.persons.view.licences.view.editions.edit.langCerts'    , '/langCerts'                                                                                    , ['@root.persons.view.licences.view.editions.edit'   , 'js/gva/persons/views/licences/licenceEditions/licenceEditionsEditLangCerts.html'             , 'LicenceEditionsEditLangCertsCtrl']])
      .state(['root.persons.view.licences.view.editions.edit.trainings'    , '/trainings'                                                                                    , ['@root.persons.view.licences.view.editions.edit'   , 'js/gva/persons/views/licences/licenceEditions/licenceEditionsEditTrainings.html'             , 'LicenceEditionsEditTrainingsCtrl']])
      .state(['root.persons.view.licences.view.editions.edit.checks'       , '/checks'                                                                                       , ['@root.persons.view.licences.view.editions.edit'   , 'js/gva/persons/views/licences/licenceEditions/licenceEditionsEditChecks.html'                , 'LicenceEditionsEditChecksCtrl'   ]])
      .state(['root.persons.view.licences.view.editions.edit.medicals'     , '/medicals'                                                                                     , ['@root.persons.view.licences.view.editions.edit'   , 'js/gva/persons/views/licences/licenceEditions/licenceEditionsEditMedicals.html'              , 'LicenceEditionsEditMedicalsCtrl' ]])
      .state(['root.persons.view.licences.view.editions.edit.licences'     , '/licences'                                                                                     , ['@root.persons.view.licences.view.editions.edit'   , 'js/gva/persons/views/licences/licenceEditions/licenceEditionsEditLicences.html'              , 'LicenceEditionsEditLicencesCtrl' ]])
      .state(['root.persons.view.checks'                                   , '/checks'                                                                                                                                                                                                                                                                                  ])
      .state(['root.persons.view.checks.search'                            , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/documentChecks/checksSearch.html'                                       , 'DocumentChecksSearchCtrl'        ]])
      .state(['root.persons.view.checks.new'                               , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/documentChecks/checksNew.html'                                          , 'DocumentChecksNewCtrl'           ]])
      .state(['root.persons.view.checks.edit'                              , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/documentChecks/checksEdit.html'                                         , 'DocumentChecksEditCtrl'          ]])
      .state(['root.persons.view.employments'                              , '/employments'                                                                                                                                                                                                                                                                             ])
      .state(['root.persons.view.employments.search'                       , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/documentEmployments/emplsSearch.html'                                   , 'DocumentEmploymentsSearchCtrl'   ]])
      .state(['root.persons.view.employments.new'                          , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/documentEmployments/emplsNew.html'                                      , 'DocumentEmploymentsNewCtrl'      ]])
      .state(['root.persons.view.employments.edit'                         , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/documentEmployments/emplsEdit.html'                                     , 'DocumentEmploymentsEditCtrl'     ]])
      .state(['root.persons.view.medicals'                                 , '/medicals'                                                                                                                                                                                                                                                                                ])
      .state(['root.persons.view.medicals.search'                          , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/documentMedicals/medsSearch.html'                                       , 'DocumentMedicalsSearchCtrl'      ]])
      .state(['root.persons.view.medicals.new'                             , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/documentMedicals/medsNew.html'                                          , 'DocumentMedicalsNewCtrl'         ]])
      .state(['root.persons.view.medicals.edit'                            , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/documentMedicals/medsEdit.html'                                         , 'DocumentMedicalsEditCtrl'        ]])
      .state(['root.persons.view.documentTrainings'                        , '/documentTrainings'                                                                                                                                                                                                                                                                       ])
      .state(['root.persons.view.documentTrainings.search'                 , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/documentTrainings/trainingsSearch.html'                                 , 'DocumentTrainingsSearchCtrl'     ]])
      .state(['root.persons.view.documentTrainings.new'                    , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/documentTrainings/trainingsNew.html'                                    , 'DocumentTrainingsNewCtrl'        ]])
      .state(['root.persons.view.documentTrainings.edit'                   , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/documentTrainings/trainingsEdit.html'                                   , 'DocumentTrainingsEditCtrl'       ]])
      .state(['root.persons.view.documentLangCerts'                        , '/documentLangCerts'                                                                                                                                                                                                                                                                       ])
      .state(['root.persons.view.documentLangCerts.search'                 , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/documentLangCerts/langCertsSearch.html'                                 , 'DocumentLangCertsSearchCtrl'     ]])
      .state(['root.persons.view.documentLangCerts.new'                    , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/documentLangCerts/langCertsNew.html'                                    , 'DocumentLangCertsNewCtrl'        ]])
      .state(['root.persons.view.documentLangCerts.edit'                   , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/documentLangCerts/langCertsEdit.html'                                   , 'DocumentLangCertsEditCtrl'       ]])
      .state(['root.persons.view.flyingExperiences'                        , '/flyingExperiences'                                                                                                                                                                                                                                                                       ])
      .state(['root.persons.view.flyingExperiences.search'                 , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/flyingExperiences/flyExpsSearch.html'                                   , 'FlyingExperiencesSearchCtrl'     ]])
      .state(['root.persons.view.flyingExperiences.new'                    , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/flyingExperiences/flyExpsNew.html'                                      , 'FlyingExperiencesNewCtrl'        ]])
      .state(['root.persons.view.flyingExperiences.edit'                   , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/flyingExperiences/flyExpsEdit.html'                                     , 'FlyingExperiencesEditCtrl'       ]])
      .state(['root.persons.view.inventory'                                , '/inventory'                                                                                    , ['@root.persons.view'                               , 'js/gva/persons/views/inventory/inventorySearch.html'                                         , 'InventorySearchCtrl'             ]])
      .state(['root.persons.view.ratings'                                  , '/ratings'                                                                                                                                                                                                                                                                                 ])
      .state(['root.persons.view.ratings.search'                           , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/ratings/ratingsSearch.html'                                             , 'RatingsSearchCtrl'               ]])
      .state(['root.persons.view.ratings.new'                              , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/ratings/ratingsNew.html'                                                , 'RatingsNewCtrl'                  ]])
      .state(['root.persons.view.ratings.edit'                             , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/ratings/ratingsEdit.html'                                               , 'RatingsEditCtrl'                 ]])
      .state(['root.persons.view.ratings.edit.editions'                    , '/ratingEditions'                                                                                                                                                                                                                                                                          ])
      .state(['root.persons.view.ratings.edit.editions.new'                , '/new'                                                                                          , ['@root.persons.view.ratings.edit'                  , 'js/gva/persons/views/ratings/ratingEditions/ratingEditionsNew.html'                          , 'RatingEditionsNewCtrl'           ]])
      .state(['root.persons.view.ratings.edit.editions.edit'               , '/:index'                                                                                       , ['@root.persons.view.ratings.edit'                  , 'js/gva/persons/views/ratings/ratingEditions/ratingEditionsEdit.html'                         , 'RatingEditionsEditCtrl'          ]])
      .state(['root.persons.view.examASs'                                  , '/examASs'                                                                                                                                                                                                                                                                                 ])
      .state(['root.persons.view.examASs.search'                           , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/exams/examsSearch.html'                                                 , 'ExamsSearchCtrl'                 ]])
      .state(['root.persons.view.examASs.new'                              , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/exams/examsNew.html'                                                    , 'ExamsNewCtrl'                    ]])
      .state(['root.persons.view.examASs.edit'                             , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/exams/examsEdit.html'                                                   , 'ExamsEditCtrl'                   ]])
      .state(['root.persons.view.documentOthers'                           , '/documentOthers'                                                                                                                                                                                                                                                                          ])
      .state(['root.persons.view.documentOthers.search'                    , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/documentOthers/othersSearch.html'                                       , 'DocumentOthersSearchCtrl'        ]])
      .state(['root.persons.view.documentOthers.new'                       , '/new'                                                                                          , ['@root.persons.view'                               , 'js/gva/persons/views/documentOthers/othersNew.html'                                          , 'DocumentOthersNewCtrl'           ]])
      .state(['root.persons.view.documentOthers.edit'                      , '/:ind'                                                                                         , ['@root.persons.view'                               , 'js/gva/persons/views/documentOthers/othersEdit.html'                                         , 'DocumentOthersEditCtrl'          ]])
      .state(['root.persons.view.documentApplications'                     , '/documentApplications'                                                                                                                                                                                                                                                                    ])
      .state(['root.persons.view.documentApplications.search'              , ''                                                                                              , ['@root.persons.view'                               , 'js/gva/persons/views/documentApplications/docApplicationsSearch.html'                        , 'DocApplicationsSearchCtrl'       ]])
      .state(['root.persons.view.examinationSystem'                        , '/examSyst'                                                                                     , ['@root.persons.view'                               , 'js/gva/persons/views/personExamSyst/personExamSyst.html'                                     , 'PersonExamSystCtrl'              ]])
      .state(['root.printableDocs'                                         , '/printableDocs?lin&uin&names&licenceType&licenceAction'                                        , ['@root'                                            , 'js/gva/persons/views/printableDocs/printableDocsSearch.html'                                 , 'PrintableDocsSearchCtrl'         ]])
      .state(['root.stampedDocuments'                                      , '/stampedDocuments?lin&uin&names&stampNumber'                                                   , ['@root'                                            , 'js/gva/persons/views/stampedDocuments/stampedDocuments.html'                                 , 'StampedDocumentsCtrl'            ]])
      .state(['root.export'                                                , '/export'                                                                                       , ['@root'                                            , 'js/gva/persons/views/export/exportView.html'                                                                                     ]])
      .state(['root.export.personsData'                                    , '/personsData'                                                                                  , ['@root.export'                                     , 'js/gva/persons/views/export/exportPersonsData.html'                                          , 'ExportPersonsDataCtrl'           ]])
      .state(['root.export.examsData'                                      , '/examsData'                                                                                    , ['@root.export'                                     , 'js/gva/persons/views/export/exportExamsData.html'                                            , 'ExportExamsDataCtrl'             ]])
      .state(['root.examinationSystem'                                     , '/examinationSystem'                                                                            , ['@root'                                            , 'js/gva/persons/views/examinationSystem/examinationSystem.html'                               , 'ExaminationSystemCtrl'           ]])
      .state(['root.examinationSystem.qualifications'                      , '/qualifications'                                                                               , ['@root.examinationSystem'                          , 'js/gva/persons/views/examinationSystem/qualifications.html'                                  , 'QualificationsCtrl'              ]])
      .state(['root.examinationSystem.examinees'                           , '/examinees'                                                                                    , ['@root.examinationSystem'                          , 'js/gva/persons/views/examinationSystem/exSystExaminees.html'                                 , 'ExSystExamineesCtrl'             ]])
      .state(['root.examinationSystem.exams'                               , '/exams'                                                                                        , ['@root.examinationSystem'                          , 'js/gva/persons/views/examinationSystem/exSystExams.html'                                     , 'ExSystExamsCtrl'                 ]])
      .state(['root.examinationSystem.certCampaigns'                       , '/certCampaigns'                                                                                , ['@root.examinationSystem'                          , 'js/gva/persons/views/examinationSystem/certCampaigns.html'                                   , 'CertCampaignsCtrl'               ]])
      .state(['root.examinationSystem.certPaths'                           , '/certPaths'                                                                                    , ['@root.examinationSystem'                          , 'js/gva/persons/views/examinationSystem/certPaths.html'                                       , 'CertPathsCtrl'                   ]])
      .state(['root.personsReports'                                        , '/personsReports'                                                                               , ['@root'                                            , 'js/gva/persons/views/personsReports/personReportView.html'                                                                       ]])
      .state(['root.personsReports.documents'                              , '/documents?typeId&documentRole&fromDate&toDate&lin'                                        , ['@root.personsReports'                             , 'js/gva/persons/views/personsReports/personReportDocuments.html'                              , 'PersonReportDocumentsCtrl'       ]])
      .state(['root.personsReports.licences'                               , '/licences?lin&licenceTypeId&fromDate&toDate&licenceActionId'                                   , ['@root.personsReports'                             , 'js/gva/persons/views/personsReports/personReportLicences.html'                               , 'PersonReportLicencesCtrl'        ]])
      .state(['root.personsReports.ratings'                                , '/ratings?fromDate&toDate&ratingTypeId&ratingClassId&authorizationId&aircraftTypeCategoryId&lin', ['@root.personsReports'                             , 'js/gva/persons/views/personsReports/personReportRatings.html'                                , 'PersonReportRatingsCtrl'         ]]);
  }]).config(['scModalProvider', function (scModalProvider) {
    scModalProvider
    .modal('chooseTrainings'       , 'js/gva/persons/modals/trainings/chooseTrainingsModal.html'      , 'ChooseTrainingsModalCtrl'           )
    .modal('newTraining'           , 'js/gva/persons/modals/trainings/newTrainingModal.html'          , 'NewTrainingModalCtrl'               )
    .modal('chooseChecks'          , 'js/gva/persons/modals/checks/chooseChecksModal.html'            , 'ChooseChecksModalCtrl'              )
    .modal('newCheck'              , 'js/gva/persons/modals/checks/newCheckModal.html'                , 'NewCheckModalCtrl'                  )
    .modal('checkOfForeigner'      , 'js/gva/persons/modals/checks/checkOfForeignerModal.html'        , 'CheckOfForeignerModalCtrl'          )
    .modal('chooseRatings'         , 'js/gva/persons/modals/ratings/chooseRatingsModal.html'          , 'ChooseRatingsModalCtrl'             )
    .modal('newRating'             , 'js/gva/persons/modals/ratings/newRatingModal.html'              , 'NewRatingModalCtrl'                 )
    .modal('chooseMedicals'        , 'js/gva/persons/modals/medicals/chooseMedicalsModal.html'        , 'ChooseMedicalsModalCtrl'            )
    .modal('newMedical'            , 'js/gva/persons/modals/medicals/newMedicalModal.html'            , 'NewMedicalModalCtrl'                )
    .modal('chooseExams'           , 'js/gva/persons/modals/exams/chooseExamsModal.html'              , 'ChooseExamsModalCtrl'               )
    .modal('newExam'               , 'js/gva/persons/modals/exams/newExamModal.html'                  , 'NewExamModalCtrl'                   )
    .modal('chooseLangCerts'       , 'js/gva/persons/modals/langCerts/chooseLangCertsModal.html'      , 'ChooseLangCertsModalCtrl'           )
    .modal('newLangCert'           , 'js/gva/persons/modals/langCerts/newLangCertModal.html'          , 'NewLangCertModalCtrl'               )
    .modal('chooseLicences'        , 'js/gva/persons/modals/licences/chooseLicencesModal.html'        , 'ChooseLicencesModalCtrl'            )
    .modal('licenceStatuses'       , 'js/gva/persons/modals/licences/licenceStatusesModal.html'       , 'LicenceStatusesModalCtrl'           )
    .modal('langLevelEntries'      , 'js/gva/persons/modals/langCerts/langLevelEntriesModal.html'     , 'LangLevelEntriesModalCtrl'          )
    .modal('licenceEditionDoc'     , 'js/gva/persons/modals/licences/licenceEditionDocModal.html'     , 'LicenceEditionDocModalCtrl'         )
    .modal('printLicence'          , 'js/gva/persons/modals/licences/printLicenceModal.html'          , 'PrintLicenceModalCtrl'      , 'xs-2')
    .modal('printRatingEdition'    , 'js/gva/persons/modals/licences/printRatingEditionModal.html'    , 'PrintRatingEditionModalCtrl', 'xs-2')
    .modal('changeCaseType'        , 'js/gva/persons/modals/caseTypes/changeCaseTypeModal.html'       , 'ChangeCaseTypeModalCtrl'    , 'xs-2')
    .modal('newQualificationState' , 'js/gva/persons/modals/qualificationStates/newQlfStateModal.html', 'NewQlfStateModalCtrl'       , 'xs-2');
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.aircrafts'                                          , '/aircrafts?mark&manSN&modelAlt&airCategory&aircraftProducer'                                                                                                                                    ])
      .state(['root.aircrafts.search'                                   , ''                                                      , ['@root'               , 'js/gva/aircrafts/views/aircraftsSearch.html'                            , 'AircraftsSearchCtrl'             ]])
      .state(['root.aircrafts.registrations'                            , '/registrations?registerId&certNumber&actNumber&regMark', ['@root'               , 'js/gva/aircrafts/views/aircraftsRegistrationsSearch.html'               , 'AircraftsRegistrationsSearchCtrl']])
      .state(['root.aircrafts.invalidActNumbers'                        , '/invalidActNumbers'                                    , ['@root'               , 'js/gva/aircrafts/views/invalidActNumbersSearch.html'                    , 'InvalidActNumbersSearchCtrl'     ]])
      .state(['root.aircrafts.new'                                      , '/new'                                                  , ['@root'               , 'js/gva/aircrafts/views/aircraftsNew.html'                               , 'AircraftsNewCtrl'                ]])
      .state(['root.aircrafts.newWizzard'                               , '/newWizzard'                                           , ['@root'               , 'js/gva/aircrafts/views/aircraftsNewWizzard.html'                        , 'AircraftsNewWizzardCtrl'         ]])
      .state(['root.aircrafts.view'                                     , '/:id?appId&set'                                        , ['@root'               , 'js/gva/aircrafts/views/aircraftsView.html'                              , 'AircraftsViewCtrl'               ]])
      .state(['root.aircrafts.view.edit'                                , '/aircraftData'                                         , ['@root'               , 'js/gva/aircrafts/views/aircraftData/aircraftDataEdit.html'              , 'AircraftDataEditCtrl'            ]])
      .state(['root.aircrafts.view.currentReg'                          , '/current/:ind'                                         , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsView.html'                        , 'CertRegsViewCtrl'                ]])
      .state(['root.aircrafts.view.regsFM'                              , '/regsFM'                                                                                                                                                                                        ])
      .state(['root.aircrafts.view.regsFM.search'                       , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsSearch.html'                      , 'CertRegsFMSearchCtrl'            ]])
      .state(['root.aircrafts.view.regsFM.new'                          , '/new?oldInd'                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsNew.html'                         , 'CertRegsFMNewCtrl'               ]])
      .state(['root.aircrafts.view.regsFM.newWizzard'                   , '/newWizzard'                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsNewWizzard.html'                  , 'CertRegsFMNewWizzardCtrl'        ]])
      .state(['root.aircrafts.view.regsFM.edit'                         , '/:ind'                                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsEdit.html'                        , 'CertRegsFMEditCtrl'              ]])
      .state(['root.aircrafts.view.regsFM.dereg'                        , '/dereg/:ind'                                           , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRegsFM/regsDereg.html'                       , 'CertRegsFMDeregCtrl'             ]])
      .state(['root.aircrafts.view.smods'                               , '/smods'                                                                                                                                                                                         ])
      .state(['root.aircrafts.view.smods.search'                        , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certSmods/smodsSearch.html'                      , 'CertSmodsSearchCtrl'             ]])
      .state(['root.aircrafts.view.smods.new'                           , '/new'                                                  , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certSmods/smodsNew.html'                         , 'CertSmodsNewCtrl'                ]])
      .state(['root.aircrafts.view.smods.edit'                          , '/:ind'                                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certSmods/smodsEdit.html'                        , 'CertSmodsEditCtrl'               ]])
      .state(['root.aircrafts.view.airworthinessesFM'                   , '/airworthinessesFM'                                                                                                                                                                             ])
      .state(['root.aircrafts.view.airworthinessesFM.search'            , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certAirworthinessesFM/airworthinessesSearch.html', 'CertAirworthinessesFMSearchCtrl' ]])
      .state(['root.aircrafts.view.airworthinessesFM.new'               , '/new'                                                  , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certAirworthinessesFM/airworthinessesNew.html'   , 'CertAirworthinessesFMNewCtrl'    ]])
      .state(['root.aircrafts.view.airworthinessesFM.edit'              , '/:ind'                                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certAirworthinessesFM/airworthinessesEdit.html'  , 'CertAirworthinessesFMEditCtrl'   ]])
      .state(['root.aircrafts.view.noises'                              , '/noises'                                                                                                                                                                                        ])
      .state(['root.aircrafts.view.noises.search'                       , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certNoises/noisesSearch.html'                    , 'CertNoisesSearchCtrl'            ]])
      .state(['root.aircrafts.view.noises.new'                          , '/new'                                                  , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certNoises/noisesNew.html'                       , 'CertNoisesNewCtrl'               ]])
      .state(['root.aircrafts.view.noises.edit'                         , '/:ind'                                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certNoises/noisesEdit.html'                      , 'CertNoisesEditCtrl'              ]])
      .state(['root.aircrafts.view.radios'                              , '/radios'                                                                                                                                                                                        ])
      .state(['root.aircrafts.view.radios.search'                       , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRadios/radiosSearch.html'                    , 'CertRadiosSearchCtrl'            ]])
      .state(['root.aircrafts.view.radios.new'                          , '/new'                                                  , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRadios/radiosNew.html'                       , 'CertRadiosNewCtrl'               ]])
      .state(['root.aircrafts.view.radios.edit'                         , '/:ind'                                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/certRadios/radiosEdit.html'                      , 'CertRadiosEditCtrl'              ]])
      .state(['root.aircrafts.view.debtsFM'                             , '/debtsFM'                                                                                                                                                                                       ])
      .state(['root.aircrafts.view.debtsFM.search'                      , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/docDebtsFM/debtsSearch.html'                     , 'DocDebtsFMSearchCtrl'            ]])
      .state(['root.aircrafts.view.debtsFM.new'                         , '/new'                                                  , ['@root.aircrafts.view', 'js/gva/aircrafts/views/docDebtsFM/debtsNew.html'                        , 'DocDebtsFMNewCtrl'               ]])
      .state(['root.aircrafts.view.debtsFM.edit'                        , '/:ind'                                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/docDebtsFM/debtsEdit.html'                       , 'DocDebtsFMEditCtrl'              ]])
      .state(['root.aircrafts.view.others'                              , '/others'                                                                                                                                                                                        ])
      .state(['root.aircrafts.view.others.search'                       , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOthers/othersSearch.html'                , 'AircraftOthersSearchCtrl'        ]])
      .state(['root.aircrafts.view.others.new'                          , '/new'                                                  , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOthers/othersNew.html'                   , 'AircraftOthersNewCtrl'           ]])
      .state(['root.aircrafts.view.others.edit'                         , '/:ind'                                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOthers/othersEdit.html'                  , 'AircraftOthersEditCtrl'          ]])
      .state(['root.aircrafts.view.inspections'                         , '/inspections'                                                                                                                                                                                   ])
      .state(['root.aircrafts.view.inspections.search'                  , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/inspections/aircraftsInspectionsSearch.html'     , 'AircraftsInspectionsSearchCtrl'  ]])
      .state(['root.aircrafts.view.inspections.new'                     , '/new'                                                  , ['@root.aircrafts.view', 'js/gva/aircrafts/views/inspections/aircraftsInspectionsNew.html'        , 'AircraftsInspectionsNewCtrl'     ]])
      .state(['root.aircrafts.view.inspections.edit'                    , '/:ind'                                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/inspections/aircraftsInspectionsEdit.html'       , 'AircraftsInspectionsEditCtrl'    ]])
      .state(['root.aircrafts.view.occurrences'                         , '/documentOccurrences'                                                                                                                                                                           ])
      .state(['root.aircrafts.view.occurrences.search'                  , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOccurrences/docOccurrencesSearch.html'   , 'DocOccurrencesSearchCtrl'        ]])
      .state(['root.aircrafts.view.occurrences.new'                     , '/new'                                                  , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOccurrences/docOccurrencesNew.html'      , 'DocOccurrencesNewCtrl'           ]])
      .state(['root.aircrafts.view.occurrences.edit'                    , '/:ind'                                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOccurrences/docOccurrencesEdit.html'     , 'DocOccurrencesEditCtrl'          ]])
      .state(['root.aircrafts.view.owners'                              , '/owners'                                                                                                                                                                                        ])
      .state(['root.aircrafts.view.owners.search'                       , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOwners/ownersSearch.html'                , 'DocumentOwnersSearchCtrl'        ]])
      .state(['root.aircrafts.view.owners.new'                          , '/new'                                                  , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOwners/ownersNew.html'                   , 'DocumentOwnersNewCtrl'           ]])
      .state(['root.aircrafts.view.owners.edit'                         , '/:ind'                                                 , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentOwners/ownersEdit.html'                  , 'DocumentOwnersEditCtrl'          ]])
      .state(['root.aircrafts.view.applications'                        , '/applications'                                                                                                                                                                                  ])
      .state(['root.aircrafts.view.applications.search'                 , ''                                                      , ['@root.aircrafts.view', 'js/gva/aircrafts/views/documentApplications/docApplicationsSearch.html' , 'AircraftApplicationsSearchCtrl'  ]])
      .state(['root.aircrafts.view.inventory'                           , '/inventory'                                            , ['@root.aircrafts.view', 'js/gva/aircrafts/views/inventory/inventorySearch.html'                  , 'AircraftInventorySearchCtrl'     ]]);
  }])
    .config(['scModalProvider', function (scModalProvider) {
    scModalProvider
    .modal('printAirworthiness'     , 'js/gva/aircrafts/modals/airworthinesses/printAirworthinessModal.html' , 'PrintAirworthinessModalCtrl' , 'xs-2')
    .modal('printExportCert'        , 'js/gva/aircrafts/modals/dereg/printExportCertModal.html'              , 'PrintExportCertModalCtrl'    , 'xs-2');
  }])
    .config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.organizations'                                                               , '/organizations?organizationName&cao&valid&organizationType&dateValidTo&dateCaoValidTo&caseTypeId'])
      .state(['root.organizations.search'                                                        , ''                                           , ['@root'                   , 'js/gva/organizations/views/organizationsSearch.html'                                                                    , 'OrganizationsSearchCtrl'                           ]])
      .state(['root.organizations.new'                                                           , '/new'                                       , ['@root'                   , 'js/gva/organizations/views/organizationsNew.html'                                                                       , 'OrganizationsNewCtrl'                              ]])
      .state(['root.organizations.view'                                                          , '/:id?appId&set'                             , ['@root'                   , 'js/gva/organizations/views/organizationsView.html'                                                                      , 'OrganizationsViewCtrl'                             ]])
      .state(['root.organizations.view.edit'                                                     , '/organizationData'                          , ['@root'                   , 'js/gva/organizations/views/organizationData/organizationDataEdit.html'                                                  , 'OrganizationDataEditCtrl'                          ]])
      .state(['root.organizations.view.addresses'                                                , '/addresses'                                                                                                                                                                                                                                                ])
      .state(['root.organizations.view.addresses.search'                                         , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/addresses/addrSearch.html'                                                                   , 'OrganizationAddressesSearchCtrl'                   ]])
      .state(['root.organizations.view.addresses.new'                                            , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/addresses/addrNew.html'                                                                      , 'OrganizationAddressesNewCtrl'                      ]])
      .state(['root.organizations.view.addresses.edit'                                           , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/addresses/addrEdit.html'                                                                     , 'OrganizationAddressesEditCtrl'                     ]])
      .state(['root.organizations.view.certAirportOperators'                                     , '/certAirportOperators'                                                                                                                                                                                                                                     ])
      .state(['root.organizations.view.certAirportOperators.search'                              , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certAirportOperators/certAirportOperatorsSearch.html'                                        , 'CertAirportOperatorsSearchCtrl'                    ]])
      .state(['root.organizations.view.certAirportOperators.new'                                 , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certAirportOperators/certAirportOperatorsNew.html'                                           , 'CertAirportOperatorsNewCtrl'                       ]])
      .state(['root.organizations.view.certAirportOperators.edit'                                , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certAirportOperators/certAirportOperatorsEdit.html'                                          , 'CertAirportOperatorsEditCtrl'                      ]])
      .state(['root.organizations.view.certAirOperators'                                         , '/certAirOperators'                                                                                                                                                                                                                                         ])
      .state(['root.organizations.view.certAirOperators.search'                                  , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certAirOperators/certAirOperatorsSearch.html'                                                , 'CertAirOperatorsSearchCtrl'                        ]])
      .state(['root.organizations.view.certAirOperators.new'                                     , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certAirOperators/certAirOperatorsNew.html'                                                   , 'CertAirOperatorsNewCtrl'                           ]])
      .state(['root.organizations.view.certAirOperators.edit'                                    , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certAirOperators/certAirOperatorsEdit.html'                                                  , 'CertAirOperatorsEditCtrl'                          ]])
      .state(['root.organizations.view.certAirNavigationServiceDeliverers'                       , '/certAirNavigationServiceDeliverers'                                                                                                                                                                                                                       ])
      .state(['root.organizations.view.certAirNavigationServiceDeliverers.search'                , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certAirNavigationServiceDeliverers/certAirNavigationServiceDeliverersSearch.html'            , 'CertAirNavigationServiceDeliverersSearchCtrl'      ]])
      .state(['root.organizations.view.certAirNavigationServiceDeliverers.new'                   , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certAirNavigationServiceDeliverers/certAirNavigationServiceDeliverersNew.html'               , 'CertAirNavigationServiceDeliverersNewCtrl'         ]])
      .state(['root.organizations.view.certAirNavigationServiceDeliverers.edit'                  , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certAirNavigationServiceDeliverers/certAirNavigationServiceDeliverersEdit.html'              , 'CertAirNavigationServiceDeliverersEditCtrl'        ]])
      .state(['root.organizations.view.certAirCarriers'                                          , '/certAirCarriers'                                                                                                                                                                                                                                          ])
      .state(['root.organizations.view.certAirCarriers.search'                                   , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certAirCarriers/certAirCarriersSearch.html'                                                  , 'CertAirCarriersSearchCtrl'                         ]])
      .state(['root.organizations.view.certAirCarriers.new'                                      , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certAirCarriers/certAirCarriersNew.html'                                                     , 'CertAirCarriersNewCtrl'                            ]])
      .state(['root.organizations.view.certAirCarriers.edit'                                     , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certAirCarriers/certAirCarriersEdit.html'                                                    , 'CertAirCarriersEditCtrl'                           ]])
      .state(['root.organizations.view.staffManagement'                                          , '/staffManagement'                                                                                                                                                                                                                                          ])
      .state(['root.organizations.view.staffManagement.search'                                   , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/staffManagement/staffManagementSearch.html'                                                  , 'StaffManagementSearchCtrl'                         ]])
      .state(['root.organizations.view.staffManagement.new'                                      , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/staffManagement/staffManagementNew.html'                                                     , 'StaffManagementNewCtrl'                            ]])
      .state(['root.organizations.view.staffManagement.edit'                                     , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/staffManagement/staffManagementEdit.html'                                                    , 'StaffManagementEditCtrl'                           ]])
      .state(['root.organizations.view.documentOthers'                                           , '/documentOthers'                                                                                                                                                                                                                                           ])
      .state(['root.organizations.view.documentOthers.search'                                    , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/documentOthers/othersSearch.html'                                                            , 'OrganizationDocOthersSearchCtrl'                   ]])
      .state(['root.organizations.view.documentOthers.new'                                       , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/documentOthers/othersNew.html'                                                               , 'OrganizationDocOthersNewCtrl'                      ]])
      .state(['root.organizations.view.documentOthers.edit'                                      , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/documentOthers/othersEdit.html'                                                              , 'OrganizationDocOthersEditCtrl'                     ]])
      .state(['root.organizations.view.certGroundServiceOperators'                               , '/certGroundServiceOperators'                                                                                                                                                                                                                               ])
      .state(['root.organizations.view.certGroundServiceOperators.search'                        , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperators/certGroundServiceOperatorsSearch.html'                            , 'CertGroundServiceOperatorsSearchCtrl'              ]])
      .state(['root.organizations.view.certGroundServiceOperators.new'                           , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperators/certGroundServiceOperatorsNew.html'                               , 'CertGroundServiceOperatorsNewCtrl'                 ]])
      .state(['root.organizations.view.certGroundServiceOperators.edit'                          , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperators/certGroundServiceOperatorsEdit.html'                              , 'CertGroundServiceOperatorsEditCtrl'                ]])
      .state(['root.organizations.view.groundServiceOperatorsSnoOperational'                     , '/groundServiceOperatorsSnoOperational'                                                                                                                                                                                                                     ])
      .state(['root.organizations.view.groundServiceOperatorsSnoOperational.search'              , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperatorsSnoOperational/certGroundServiceOperatorsSnoOperationalSearch.html', 'CertGroundServiceOperatorsSnoOperationalSearchCtrl']])
      .state(['root.organizations.view.groundServiceOperatorsSnoOperational.new'                 , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperatorsSnoOperational/certGroundServiceOperatorsSnoOperationalNew.html'   , 'CertGroundServiceOperatorsSnoOperationalNewCtrl'   ]])
      .state(['root.organizations.view.groundServiceOperatorsSnoOperational.edit'                , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/certGroundServiceOperatorsSnoOperational/certGroundServiceOperatorsSnoOperationalEdit.html'  , 'CertGroundServiceOperatorsSnoOperationalEditCtrl'  ]])
      .state(['root.organizations.view.inspections'                                              , '/inspections'                                                                                                                                                                                                                                              ])
      .state(['root.organizations.view.inspections.search'                                       , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/inspections/organizationsInspectionsSearch.html'                                             , 'OrganizationsInspectionsSearchCtrl'                ]])
      .state(['root.organizations.view.inspections.new'                                          , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/inspections/organizationsInspectionsNew.html'                                                , 'OrganizationsInspectionsNewCtrl'                   ]])
      .state(['root.organizations.view.inspections.edit'                                         , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/inspections/organizationsInspectionsEdit.html'                                               , 'OrganizationsInspectionsEditCtrl'                  ]])
      .state(['root.organizations.view.approvals'                                                , '/approvals'                                                                                                                                                                                                                                               ])
      .state(['root.organizations.view.approvals.search'                                         , ''                                           , ['@root.organizations.view'                 , 'js/gva/organizations/views/approvals/approvalsSearch.html'                                             , 'ApprovalsSearchCtrl'                               ]])
      .state(['root.organizations.view.approvals.new'                                            , '/new'                                       , ['@root.organizations.view'                 , 'js/gva/organizations/views/approvals/approvalsNew.html'                                                , 'ApprovalsNewCtrl'                                  ]])
      .state(['root.organizations.view.approvals.edit'                                           , '/:ind'                                      , ['@root.organizations.view'                 , 'js/gva/organizations/views/approvals/approvalsEdit.html'                                               , 'ApprovalsEditCtrl'                                 ]])
      .state(['root.organizations.view.approvals.edit.amendments'                                , '/approvalAmendments'                                                                                                                                                                                                                                       ])
      .state(['root.organizations.view.approvals.edit.amendments.new'                            , '/new'                                       , ['@root.organizations.view.approvals.edit'  , 'js/gva/organizations/views/approvals/amendments/approvalAmendmentsNew.html'                            , 'ApprovalAmendmentsNewCtrl'                         ]])
      .state(['root.organizations.view.approvals.edit.amendments.edit'                           , '/:index'                                    , ['@root.organizations.view.approvals.edit'  , 'js/gva/organizations/views/approvals/amendments/approvalAmendmentsEdit.html'                           , 'ApprovalAmendmentsEditCtrl'                        ]])
      .state(['root.organizations.view.awExaminers'                                              , '/awExaminers'                                                                                                                                                                                                                                              ])
      .state(['root.organizations.view.awExaminers.search'                                       , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/awExaminers/awExaminersSearch.html'                                                          , 'AwExaminersSearchCtrl'                             ]])
      .state(['root.organizations.view.awExaminers.new'                                          , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/awExaminers/awExaminersNew.html'                                                             , 'AwExaminersNewCtrl'                                ]])
      .state(['root.organizations.view.awExaminers.edit'                                         , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/awExaminers/awExaminersEdit.html'                                                            , 'AwExaminersEditCtrl'                               ]])
      .state(['root.organizations.view.recommendations'                                          , '/recommendations'                                                                                                                                                                                                                                          ])
      .state(['root.organizations.view.recommendations.search'                                   , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/recommendations/recommendationsSearch.html'                                                  , 'RecommendationsSearchCtrl'                         ]])
      .state(['root.organizations.view.recommendations.new'                                      , '/new'                                       , ['@root.organizations.view', 'js/gva/organizations/views/recommendations/recommendationsNew.html'                                                     , 'RecommendationsNewCtrl'                            ]])
      .state(['root.organizations.view.recommendations.edit'                                     , '/:ind'                                      , ['@root.organizations.view', 'js/gva/organizations/views/recommendations/recommendationsEdit.html'                                                    , 'RecommendationsEditCtrl'                           ]])
      .state(['root.organizations.view.documentApplications'                                     , '/documentApplications'                                                                                                                                                                                                                                     ])
      .state(['root.organizations.view.documentApplications.search'                              , ''                                           , ['@root.organizations.view', 'js/gva/organizations/views/documentApplications/organizationsDocApplicationsSearch.html'                                , 'OrganizationsDocApplicationsSearchCtrl'            ]])
      .state(['root.organizations.view.inventory'                                                , '/inventory'                                 , ['@root.organizations.view', 'js/gva/organizations/views/inventory/organizationsInventorySearch.html'                                                 , 'OrganizationsInventorySearchCtrl'                  ]]);
  }]).config(['scModalProvider', function (scModalProvider) {
      scModalProvider
      .modal('chooseOrganizationDocs', 'js/gva/organizations/modals/documents/chooseDocumentsModal.html'    , 'ChooseOrgDocsModalCtrl'    )
      .modal('chooseInspections'     , 'js/gva/organizations/modals/inspections/chooseInspectionsModal.html', 'ChooseInspectionsModalCtrl')
      .modal('chooseLimitation'      , 'js/gva/organizations/modals/limitations/chooseLimitationModal.html' , 'ChooseLimitationModalCtrl' )
      .modal('chooseEmployment'      , 'js/gva/organizations/modals/employments/chooseEmploymentModal.html' , 'ChooseEmploymentModalCtrl' );
  }]).config(['$stateProvider', function ($stateProvider) {
      $stateProvider
      .state(['root.airports'                                   , '/airports?name&icao'                                                                                                                                                            ])
      .state(['root.airports.search'                            , ''                                         , ['@root'              , 'js/gva/airports/views/airportsSearch.html'                             , 'AirportsSearchCtrl'             ]])
      .state(['root.airports.new'                               , '/new'                                     , ['@root'              , 'js/gva/airports/views/airportsNew.html'                                , 'AirportsNewCtrl'                ]])
      .state(['root.airports.view'                              , '/:id?appId&set'                           , ['@root'              , 'js/gva/airports/views/airportsView.html'                               , 'AirportsViewCtrl'               ]])
      .state(['root.airports.view.edit'                         , '/airportData'                             , ['@root'              , 'js/gva/airports/views/airportData/airportDataEdit.html'                , 'AirportDataEditCtrl'            ]])
      .state(['root.airports.view.others'                       , '/others'                                                                                                                                                                        ])
      .state(['root.airports.view.others.search'                , ''                                         , ['@root.airports.view', 'js/gva/airports/views/documentOthers/othersSearch.html'                 , 'AirportOthersSearchCtrl'       ]])
      .state(['root.airports.view.others.new'                   , '/new'                                     , ['@root.airports.view', 'js/gva/airports/views/documentOthers/othersNew.html'                    , 'AirportOthersNewCtrl'          ]])
      .state(['root.airports.view.others.edit'                  , '/:ind'                                    , ['@root.airports.view', 'js/gva/airports/views/documentOthers/othersEdit.html'                   , 'AirportOthersEditCtrl'         ]])
      .state(['root.airports.view.owners'                       , '/owners'                                                                                                                                                                        ])
      .state(['root.airports.view.owners.search'                , ''                                         , ['@root.airports.view', 'js/gva/airports/views/documentOwners/ownersSearch.html'                 , 'AirportOwnersSearchCtrl'       ]])
      .state(['root.airports.view.owners.new'                   , '/new'                                     , ['@root.airports.view', 'js/gva/airports/views/documentOwners/ownersNew.html'                    , 'AirportOwnersNewCtrl'          ]])
      .state(['root.airports.view.owners.edit'                  , '/:ind'                                    , ['@root.airports.view', 'js/gva/airports/views/documentOwners/ownersEdit.html'                   , 'AirportOwnersEditCtrl'         ]])
      .state(['root.airports.view.opers'                        , '/opers'                                                                                                                                                                         ])
      .state(['root.airports.view.opers.search'                 , ''                                         , ['@root.airports.view', 'js/gva/airports/views/certOpers/opersSearch.html'                       , 'AirportOpersSearchCtrl'        ]])
      .state(['root.airports.view.opers.new'                    , '/new'                                     , ['@root.airports.view', 'js/gva/airports/views/certOpers/opersNew.html'                          , 'AirportOpersNewCtrl'           ]])
      .state(['root.airports.view.opers.edit'                   , '/:ind'                                    , ['@root.airports.view', 'js/gva/airports/views/certOpers/opersEdit.html'                         , 'AirportOpersEditCtrl'          ]])
      .state(['root.airports.view.applications'                 , '/applications'                                                                                                                                                                  ])
      .state(['root.airports.view.applications.search'          , ''                                         , ['@root.airports.view', 'js/gva/airports/views/documentApplications/docApplicationsSearch.html'  , 'AirportApplicationsSearchCtrl' ]])
      .state(['root.airports.view.inspections'                  , '/inspections'                                                                                                                                                                   ])
      .state(['root.airports.view.inspections.search'           , ''                                         , ['@root.airports.view', 'js/gva/airports/views/inspections/airportsInspectionsSearch.html'       , 'AirportsInspectionsSearchCtrl' ]])
      .state(['root.airports.view.inspections.new'              , '/new'                                     , ['@root.airports.view', 'js/gva/airports/views/inspections/airportsInspectionsNew.html'          , 'AirportsInspectionsNewCtrl'    ]])
      .state(['root.airports.view.inspections.edit'             , '/:ind'                                    , ['@root.airports.view', 'js/gva/airports/views/inspections/airportsInspectionsEdit.html'         , 'AirportsInspectionsEditCtrl'   ]])
      .state(['root.airports.view.inventory'                    , '/inventory'                               , ['@root.airports.view', 'js/gva/airports/views/inventory/inventorySearch.html'                   , 'AirportInventorySearchCtrl'    ]]);
    }]).config(['scModalProvider', function (scModalProvider) {
      scModalProvider
      .modal('chooseAirportsDocs', 'js/gva/airports/modals/documents/chooseDocumentsModal.html', 'ChooseAirportDocsModalCtrl');
    }]).config(['$stateProvider', function ($stateProvider) {
      $stateProvider
      .state(['root.equipments'                                 , '/equipments?name'                                                                                                                                                                    ])
      .state(['root.equipments.search'                          , ''                                         , ['@root'                , 'js/gva/equipments/views/equipmentsSearch.html'                             , 'EquipmentsSearchCtrl'          ]])
      .state(['root.equipments.new'                             , '/new'                                     , ['@root'                , 'js/gva/equipments/views/equipmentsNew.html'                                , 'EquipmentsNewCtrl'             ]])
      .state(['root.equipments.view'                            , '/:id?appId&set'                           , ['@root'                , 'js/gva/equipments/views/equipmentsView.html'                               , 'EquipmentsViewCtrl'            ]])
      .state(['root.equipments.view.edit'                       , '/equipmentData'                           , ['@root'                , 'js/gva/equipments/views/equipmentData/equipmentDataEdit.html'              , 'EquipmentDataEditCtrl'         ]])
      .state(['root.equipments.view.others'                     , '/others'                                                                                                                                                                             ])
      .state(['root.equipments.view.others.search'              , ''                                         , ['@root.equipments.view', 'js/gva/equipments/views/documentOthers/othersSearch.html'                 , 'EquipmentOthersSearchCtrl'      ]])
      .state(['root.equipments.view.others.new'                 , '/new'                                     , ['@root.equipments.view', 'js/gva/equipments/views/documentOthers/othersNew.html'                    , 'EquipmentOthersNewCtrl'         ]])
      .state(['root.equipments.view.others.edit'                , '/:ind'                                    , ['@root.equipments.view', 'js/gva/equipments/views/documentOthers/othersEdit.html'                   , 'EquipmentOthersEditCtrl'        ]])
      .state(['root.equipments.view.owners'                     , '/owners'                                                                                                                                                                             ])
      .state(['root.equipments.view.owners.search'              , ''                                         , ['@root.equipments.view', 'js/gva/equipments/views/documentOwners/ownersSearch.html'                 , 'EquipmentOwnersSearchCtrl'      ]])
      .state(['root.equipments.view.owners.new'                 , '/new'                                     , ['@root.equipments.view', 'js/gva/equipments/views/documentOwners/ownersNew.html'                    , 'EquipmentOwnersNewCtrl'         ]])
      .state(['root.equipments.view.owners.edit'                , '/:ind'                                    , ['@root.equipments.view', 'js/gva/equipments/views/documentOwners/ownersEdit.html'                   , 'EquipmentOwnersEditCtrl'        ]])
      .state(['root.equipments.view.opers'                      , '/opers'                                                                                                                                                                              ])
      .state(['root.equipments.view.opers.search'               , ''                                         , ['@root.equipments.view', 'js/gva/equipments/views/certOpers/opersSearch.html'                       , 'EquipmentOpersSearchCtrl'       ]])
      .state(['root.equipments.view.opers.new'                  , '/new'                                     , ['@root.equipments.view', 'js/gva/equipments/views/certOpers/opersNew.html'                          , 'EquipmentOpersNewCtrl'          ]])
      .state(['root.equipments.view.opers.edit'                 , '/:ind'                                    , ['@root.equipments.view', 'js/gva/equipments/views/certOpers/opersEdit.html'                         , 'EquipmentOpersEditCtrl'         ]])
      .state(['root.equipments.view.applications'               , '/applications'                                                                                                                                                                       ])
      .state(['root.equipments.view.applications.search'        , ''                                         , ['@root.equipments.view', 'js/gva/equipments/views/documentApplications/docApplicationsSearch.html'  , 'EquipmentApplicationsSearchCtrl']])
      .state(['root.equipments.view.inspections'                , '/inspections'                                                                                                                                                                        ])
      .state(['root.equipments.view.inspections.search'         , ''                                         , ['@root.equipments.view', 'js/gva/equipments/views/inspections/equipmentsInspectionsSearch.html'     , 'EquipmentsInspectionsSearchCtrl']])
      .state(['root.equipments.view.inspections.new'            , '/new'                                     , ['@root.equipments.view', 'js/gva/equipments/views/inspections/equipmentsInspectionsNew.html'        , 'EquipmentsInspectionsNewCtrl'   ]])
      .state(['root.equipments.view.inspections.edit'           , '/:ind'                                    , ['@root.equipments.view', 'js/gva/equipments/views/inspections/equipmentsInspectionsEdit.html'       , 'EquipmentsInspectionsEditCtrl'  ]])
      .state(['root.equipments.view.inventory'                  , '/inventory'                               , ['@root.equipments.view', 'js/gva/equipments/views/inventory/inventorySearch.html'                   , 'EquipmentInventorySearchCtrl'   ]]);
    }]).config(['scModalProvider', function (scModalProvider) {
    scModalProvider
    .modal('chooseEquipmentsDocs', 'js/gva/equipments/modals/documents/chooseDocumentsModal.html', 'ChooseEquipmentsDocsModalCtrl');
  }]);
}(angular));
