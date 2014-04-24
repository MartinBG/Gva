/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('ems', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'common',
    'ems.templates',
    'l10n',
    'l10n-tools'
  ]).config(['scaffoldingProvider', function (scaffoldingProvider) {
    scaffoldingProvider.form({
      name: 'emsCorrData',
      templateUrl: 'ems/corrs/forms/corrData.html',
      controller: 'CorrsDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocNewCommon',
      templateUrl: 'ems/docs/forms/docNew/docNewCommon.html',
      controller: 'DocNewCommonCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocInfo',
      templateUrl: 'ems/docs/forms/docInfo.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocIncoming',
      templateUrl: 'ems/docs/forms/docView/docIncoming.html',
      controller: 'DocIncomingCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocInternal',
      templateUrl: 'ems/docs/forms/docView/docInternal.html',
      controller: 'DocInternalCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocInternalOutgoing',
      templateUrl: 'ems/docs/forms/docView/docInternalOutgoing.html',
      controller: 'DocInternalOutgoingCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocOutgoing',
      templateUrl: 'ems/docs/forms/docView/docOutgoing.html',
      controller: 'DocOutgoingCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewRemark',
      templateUrl: 'ems/docs/forms/docView/remark.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewResolution',
      templateUrl: 'ems/docs/forms/docView/resolution.html',
      controller: 'ResolutionCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewTask',
      templateUrl: 'ems/docs/forms/docView/task.html',
      controller: 'TaskCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocFilesEdit',
      templateUrl: 'ems/docs/forms/docView/docFilesEdit.html',
      controller: 'DocFilesEditCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocFilesView',
      templateUrl: 'ems/docs/forms/docView/docFilesView.html',
      controller: 'DocFilesViewCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocClassification',
      templateUrl: 'ems/docs/forms/docView/docClassification.html',
      controller: 'DocClassificationCtrl'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.docs'                                     , '/docs?filter&fromDate&toDate&regUri&docName&docTypeId&docStatusId&hideRead&isCase&corrs&units&ds&hasLot'                                                                                                      ])
      .state(['root.docs.search'                              , ''                                                                                                     , ['@root'           , 'ems/docs/views/docsSearch.html'                       ,'DocsSearchCtrl'        ]])
      .state(['root.docs.new'                                 , '/new?parentDocId'                                                                                     , ['@root'           , 'ems/docs/views/docsNew.html'                          ,'DocsNewCtrl'           ]])
      .state(['root.docs.new.caseSelect'                      , '/caseSelect?csFromDate&csToDate&csRegUri&csDocName&csDocTypeId&csDocStatusId&csCorrs&csUnits&csIsCase', ['@root'           , 'ems/docs/views/caseSelect.html'                       ,'CaseSelectCtrl'        ]])
      .state(['root.docs.news'                                , '/news'                                                                                                , ['@root'           , 'ems/docs/views/docsNews.html'                         ,'DocsNewsCtrl'          ]])
      .state(['root.docs.edit'                                , '/:id'                                                                                                 , ['@root'           , 'ems/docs/views/docsEdit.html'                         ,'DocsEditCtrl'          ]])
      .state(['root.docs.edit.view'                           , '/view'                                                                                                , ['@root.docs.edit' , 'ems/docs/views/docsView.html'                         ,'DocsViewCtrl'          ]])
      .state(['root.docs.edit.view.selectCorr'                , '/selectCorr?displayName&email&stamp'                                                                  , ['@root.docs.edit' , 'ems/docs/views/selectCorrView.html'                   ,'SelectCorrViewCtrl'    ]])
      .state(['root.docs.edit.view.selectUnit'                , '/selectUnit?name&stamp'                                                                               , ['@root.docs.edit' , 'ems/docs/views/selectUnitView.html'                   ,'SelectUnitViewCtrl'    ]])
      .state(['root.docs.edit.workflows'                      , '/workflows'                                                                                           , ['@root.docs.edit' , 'ems/docs/views/docsWorkflows.html'                    ,'DocsWorkflowsCtrl'     ]])
      .state(['root.docs.edit.workflows.signRequest'          , '/signRequest'                                                                                         , ['@root.docs.edit' , 'ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'   ]])
      .state(['root.docs.edit.workflows.discussRequest'       , '/discussRequest'                                                                                      , ['@root.docs.edit' , 'ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'   ]])
      .state(['root.docs.edit.workflows.approvalRequest'      , '/approvalRequest'                                                                                     , ['@root.docs.edit' , 'ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'   ]])
      .state(['root.docs.edit.workflows.registrationRequest'  , '/registrationRequest'                                                                                 , ['@root.docs.edit' , 'ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'   ]])
      .state(['root.docs.edit.workflows.signConfirm'          , '/signConfirm'                                                                                         , ['@root.docs.edit' , 'ems/docs/views/workflowConfirm.html'                  ,'WorkflowConfirmCtrl'   ]])
      .state(['root.docs.edit.workflows.discussConfirm'       , '/discussConfirm'                                                                                      , ['@root.docs.edit' , 'ems/docs/views/workflowConfirm.html'                  ,'WorkflowConfirmCtrl'   ]])
      .state(['root.docs.edit.workflows.approvalConfirm'      , '/approvalConfirm'                                                                                     , ['@root.docs.edit' , 'ems/docs/views/workflowConfirm.html'                  ,'WorkflowConfirmCtrl'   ]])
      .state(['root.docs.edit.stages'                         , '/stages'                                                                                              , ['@root.docs.edit' , 'ems/docs/views/docsStages.html'                       ,'DocsStagesCtrl'        ]])
      .state(['root.docs.edit.stages.next'                    , '/next'                                                                                                , ['@root.docs.edit' , 'ems/docs/views/nextStage.html'                        ,'NextStageCtrl'         ]])
      .state(['root.docs.edit.stages.edit'                    , '/edit'                                                                                                , ['@root.docs.edit' , 'ems/docs/views/editStage.html'                        ,'EditStageCtrl'         ]])
      .state(['root.docs.edit.stages.end'                     , '/end'                                                                                                 , ['@root.docs.edit' , 'ems/docs/views/endStage.html'                         ,'EndStageCtrl'          ]])
      .state(['root.docs.edit.case'                           , '/case'                                                                                                , ['@root.docs.edit' , 'ems/docs/views/docsCase.html'                         ,'DocsCaseCtrl'          ]])
      .state(['root.docs.edit.case.linkApp'                   , '/linkApp'                                                                                             , ['@root.docs.edit' , 'gva/applications/views/docsLinkApp.html'              ,'DocsLinkAppCtrl'       ]])
      .state(['root.docs.edit.case.linkApp.personSelect'      , '/personSelect?exact&lin&uin&names&licences&ratings&organization&stamp'                                , ['@root'           , 'gva/applications/views/common/personSelect.html'      ,'PersonSelectCtrl'      ]])
      .state(['root.docs.edit.case.linkApp.personNew'         , '/personNew'                                                                                           , ['@root'           , 'gva/applications/views/common/personNew.html'         ,'PersonNewCtrl'         ]])
      .state(['root.docs.edit.case.linkApp.organizationSelect', '/organizationSelect?name&CAO&valid&organizationType&dateValidTo&dateCAOValidTo&uin&stamp'             , ['@root'           , 'gva/applications/views/common/organizationSelect.html','OrganizationSelectCtrl']])
      .state(['root.docs.edit.case.linkApp.organizationNew'   , '/organizationNew'                                                                                     , ['@root'           , 'gva/applications/views/common/organizationNew.html'   ,'OrganizationNewCtrl'   ]])
      .state(['root.docs.edit.case.linkApp.aircraftSelect'    , '/aircraftSelect?manSN&model&icao&stamp'                                                               , ['@root'           , 'gva/applications/views/common/aircraftSelect.html'    ,'AircraftSelectCtrl'    ]])
      .state(['root.docs.edit.case.linkApp.aircraftNew'       , '/aircraftNew'                                                                                         , ['@root'           , 'gva/applications/views/common/aircraftNew.html'       ,'AircraftNewCtrl'       ]])
      .state(['root.docs.edit.case.linkApp.airportSelect'     , '/airportSelect?name&icao&stamp'                                                                       , ['@root'           , 'gva/applications/views/common/airportSelect.html'     ,'AirportSelectCtrl'     ]])
      .state(['root.docs.edit.case.linkApp.airportNew'        , '/airportNew'                                                                                          , ['@root'           , 'gva/applications/views/common/airportNew.html'        ,'AirportNewCtrl'        ]])
      .state(['root.docs.edit.case.linkApp.equipmentSelect'   , '/equipmentSelect?name&stamp'                                                                          , ['@root'           , 'gva/applications/views/common/equipmentSelect.html'   ,'EquipmentSelectCtrl'   ]])
      .state(['root.docs.edit.case.linkApp.equipmentNew'      , '/equipmentNew'                                                                                        , ['@root'           , 'gva/applications/views/common/equipmentNew.html'      ,'EquipmentNewCtrl'      ]])
      .state(['root.docs.edit.case.casePart'                  , '/casePart'                                                                                            , ['@root.docs.edit' , 'ems/docs/views/editCasePart.html'                     ,'EditCasePartCtrl'      ]])
      .state(['root.docs.edit.case.docType'                   , '/docType'                                                                                             , ['@root.docs.edit' , 'ems/docs/views/editDocType.html'                      ,'EditDocTypeCtrl'       ]])
      .state(['root.docs.edit.case.docType.selectUnit'        , '/selectUnit?name'                                                                                     , ['@root.docs.edit' , 'ems/docs/views/selectUnitView.html'                   ,'SelectUnitViewCtrl'    ]])
      .state(['root.corrs'                                    , '/corrs?displayName&correspondentEmail&limit&offset'                                                                                                                                                           ])
      .state(['root.corrs.search'                             , ''                                                                                                     , ['@root'           , 'ems/corrs/views/corrSearch.html'                      ,'CorrsSearchCtrl'       ]])
      .state(['root.corrs.new'                                , '/new'                                                                                                 , ['@root'           , 'ems/corrs/views/corrNew.html'                         ,'CorrsNewCtrl'          ]])
      .state(['root.corrs.edit'                               , '/:id'                                                                                                 , ['@root'           , 'ems/corrs/views/corrEdit.html'                        ,'CorrsEditCtrl'         ]]);
  }]);
}(angular));
