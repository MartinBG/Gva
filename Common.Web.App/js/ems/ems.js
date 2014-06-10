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
    // @ifndef DEBUG
    'ems.templates',
    // @endif
    'l10n',
    'l10n-tools'
  ]).config(['scaffoldingProvider', function (scaffoldingProvider) {
    scaffoldingProvider.form({
      name: 'emsCorrData',
      templateUrl: 'js/ems/corrs/forms/corrData.html',
      controller: 'CorrsDataCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocNewCommon',
      templateUrl: 'js/ems/docs/forms/docNew/docNewCommon.html',
      controller: 'DocNewCommonCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocInfo',
      templateUrl: 'js/ems/docs/forms/docInfo.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocIncoming',
      templateUrl: 'js/ems/docs/forms/docView/docIncoming.html',
      controller: 'DocIncomingCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocInternal',
      templateUrl: 'js/ems/docs/forms/docView/docInternal.html',
      controller: 'DocInternalCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocInternalOutgoing',
      templateUrl: 'js/ems/docs/forms/docView/docInternalOutgoing.html',
      controller: 'DocInternalOutgoingCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocOutgoing',
      templateUrl: 'js/ems/docs/forms/docView/docOutgoing.html',
      controller: 'DocOutgoingCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewRemark',
      templateUrl: 'js/ems/docs/forms/docView/remark.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewResolution',
      templateUrl: 'js/ems/docs/forms/docView/resolution.html',
      controller: 'ResolutionCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewTask',
      templateUrl: 'js/ems/docs/forms/docView/task.html',
      controller: 'TaskCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocFilesEdit',
      templateUrl: 'js/ems/docs/forms/docView/docFilesEdit.html',
      controller: 'DocFilesEditCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocFilesView',
      templateUrl: 'js/ems/docs/forms/docView/docFilesView.html',
      controller: 'DocFilesViewCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocClassification',
      templateUrl: 'js/ems/docs/forms/docView/docClassification.html',
      controller: 'DocClassificationCtrl'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.docs'                                     , '/docs?filter&fromDate&toDate&regUri&docName&docTypeId&docStatusId&hideRead&isCase&corrs&units&ds&hasLot'                                                                                                      ])
      .state(['root.docs.search'                              , ''                                                                                                     , ['@root'           , 'js/ems/docs/views/docsSearch.html'                       ,'DocsSearchCtrl'        ]])
      .state(['root.docs.new'                                 , '/new?parentDocId'                                                                                     , ['@root'           , 'js/ems/docs/views/docsNew.html'                          ,'DocsNewCtrl'           ]])
      .state(['root.docs.new.caseSelect'                      , '/caseSelect?csFromDate&csToDate&csRegUri&csDocName&csDocTypeId&csDocStatusId&csCorrs&csUnits&csIsCase', ['@root'           , 'js/ems/docs/views/caseSelect.html'                       ,'CaseSelectCtrl'        ]])
      .state(['root.docs.news'                                , '/news'                                                                                                , ['@root'           , 'js/ems/docs/views/docsNews.html'                         ,'DocsNewsCtrl'          ]])
      .state(['root.docs.edit'                                , '/:id'                                                                                                 , ['@root'           , 'js/ems/docs/views/docsEdit.html'                         ,'DocsEditCtrl'          ]])
      .state(['root.docs.edit.view'                           , '/view'                                                                                                , ['@root.docs.edit' , 'js/ems/docs/views/docsView.html'                         ,'DocsViewCtrl'          ]])
      .state(['root.docs.edit.view.selectCorr'                , '/selectCorr?displayName&email&stamp'                                                                  , ['@root.docs.edit' , 'js/ems/docs/views/selectCorrView.html'                   ,'SelectCorrViewCtrl'    ]])
      .state(['root.docs.edit.view.newCorr'                   , '/new'                                                                                                 , ['@root.docs.edit' , 'js/ems/corrs/views/corrNew.html'                         ,'DocsCorrNewCtrl'       ]])
      .state(['root.docs.edit.view.selectUnit'                , '/selectUnit?name&stamp'                                                                               , ['@root.docs.edit' , 'js/ems/docs/views/selectUnitView.html'                   ,'SelectUnitViewCtrl'    ]])
      .state(['root.docs.edit.workflows'                      , '/workflows'                                                                                           , ['@root.docs.edit' , 'js/ems/docs/views/docsWorkflows.html'                    ,'DocsWorkflowsCtrl'     ]])
      .state(['root.docs.edit.workflows.signRequest'          , '/signRequest'                                                                                         , ['@root.docs.edit' , 'js/ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'   ]])
      .state(['root.docs.edit.workflows.discussRequest'       , '/discussRequest'                                                                                      , ['@root.docs.edit' , 'js/ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'   ]])
      .state(['root.docs.edit.workflows.approvalRequest'      , '/approvalRequest'                                                                                     , ['@root.docs.edit' , 'js/ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'   ]])
      .state(['root.docs.edit.workflows.registrationRequest'  , '/registrationRequest'                                                                                 , ['@root.docs.edit' , 'js/ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'   ]])
      .state(['root.docs.edit.workflows.signConfirm'          , '/signConfirm'                                                                                         , ['@root.docs.edit' , 'js/ems/docs/views/workflowConfirm.html'                  ,'WorkflowConfirmCtrl'   ]])
      .state(['root.docs.edit.workflows.discussConfirm'       , '/discussConfirm'                                                                                      , ['@root.docs.edit' , 'js/ems/docs/views/workflowConfirm.html'                  ,'WorkflowConfirmCtrl'   ]])
      .state(['root.docs.edit.workflows.approvalConfirm'      , '/approvalConfirm'                                                                                     , ['@root.docs.edit' , 'js/ems/docs/views/workflowConfirm.html'                  ,'WorkflowConfirmCtrl'   ]])
      .state(['root.docs.edit.stages'                         , '/stages'                                                                                              , ['@root.docs.edit' , 'js/ems/docs/views/docsStages.html'                       ,'DocsStagesCtrl'        ]])
      .state(['root.docs.edit.stages.next'                    , '/next'                                                                                                , ['@root.docs.edit' , 'js/ems/docs/views/nextStage.html'                        ,'NextStageCtrl'         ]])
      .state(['root.docs.edit.stages.edit'                    , '/edit'                                                                                                , ['@root.docs.edit' , 'js/ems/docs/views/editStage.html'                        ,'EditStageCtrl'         ]])
      .state(['root.docs.edit.stages.end'                     , '/end'                                                                                                 , ['@root.docs.edit' , 'js/ems/docs/views/endStage.html'                         ,'EndStageCtrl'          ]])
      .state(['root.docs.edit.case'                           , '/case'                                                                                                , ['@root.docs.edit' , 'js/ems/docs/views/docsCase.html'                         ,'DocsCaseCtrl'          ]])
      .state(['root.docs.edit.case.linkApp'                   , '/linkApp'                                                                                             , ['@root.docs.edit' , 'js/gva/applications/views/docsLinkApp.html'              ,'DocsLinkAppCtrl'       ]])
      .state(['root.docs.edit.case.linkApp.personSelect'      , '/personSelect?exact&lin&uin&names&licences&ratings&organization&stamp'                                , ['@root'           , 'js/gva/applications/views/common/personSelect.html'      ,'PersonSelectCtrl'      ]])
      .state(['root.docs.edit.case.linkApp.personNew'         , '/personNew'                                                                                           , ['@root'           , 'js/gva/applications/views/common/personNew.html'         ,'PersonNewCtrl'         ]])
      .state(['root.docs.edit.case.linkApp.organizationSelect', '/organizationSelect?name&CAO&valid&organizationType&dateValidTo&dateCAOValidTo&uin&stamp'             , ['@root'           , 'js/gva/applications/views/common/organizationSelect.html','OrganizationSelectCtrl']])
      .state(['root.docs.edit.case.linkApp.organizationNew'   , '/organizationNew'                                                                                     , ['@root'           , 'js/gva/applications/views/common/organizationNew.html'   ,'OrganizationNewCtrl'   ]])
      .state(['root.docs.edit.case.linkApp.aircraftSelect'    , '/aircraftSelect?manSN&model&icao&stamp'                                                               , ['@root'           , 'js/gva/applications/views/common/aircraftSelect.html'    ,'AircraftSelectCtrl'    ]])
      .state(['root.docs.edit.case.linkApp.aircraftNew'       , '/aircraftNew'                                                                                         , ['@root'           , 'js/gva/applications/views/common/aircraftNew.html'       ,'AircraftNewCtrl'       ]])
      .state(['root.docs.edit.case.linkApp.airportSelect'     , '/airportSelect?name&icao&stamp'                                                                       , ['@root'           , 'js/gva/applications/views/common/airportSelect.html'     ,'AirportSelectCtrl'     ]])
      .state(['root.docs.edit.case.linkApp.airportNew'        , '/airportNew'                                                                                          , ['@root'           , 'js/gva/applications/views/common/airportNew.html'        ,'AirportNewCtrl'        ]])
      .state(['root.docs.edit.case.linkApp.equipmentSelect'   , '/equipmentSelect?name&stamp'                                                                          , ['@root'           , 'js/gva/applications/views/common/equipmentSelect.html'   ,'EquipmentSelectCtrl'   ]])
      .state(['root.docs.edit.case.linkApp.equipmentNew'      , '/equipmentNew'                                                                                        , ['@root'           , 'js/gva/applications/views/common/equipmentNew.html'      ,'EquipmentNewCtrl'      ]])
      .state(['root.docs.edit.case.casePart'                  , '/casePart'                                                                                            , ['@root.docs.edit' , 'js/ems/docs/views/editCasePart.html'                     ,'EditCasePartCtrl'      ]])
      .state(['root.docs.edit.case.docType'                   , '/docType'                                                                                             , ['@root.docs.edit' , 'js/ems/docs/views/editDocType.html'                      ,'EditDocTypeCtrl'       ]])
      .state(['root.docs.edit.case.docType.selectUnit'        , '/selectUnit?name'                                                                                     , ['@root.docs.edit' , 'js/ems/docs/views/selectUnitView.html'                   ,'SelectUnitViewCtrl'    ]])
      .state(['root.docs.edit.case.manualRegister'            , '/manualRegister'                                                                                      , ['@root.docs.edit' , 'js/ems/docs/views/manualRegister.html'                   ,'ManualRegisterCtrl'    ]])
      .state(['root.docs.edit.case.caseFinish'                , '/caseFinish'                                                                                          , ['@root.docs.edit' , 'js/ems/docs/views/caseFinish.html'                       ,'CaseFinishCtrl'        ]])
      .state(['root.docs.edit.case.changeDocParent'           , '/changeDocParent'                                                                                     , ['@root.docs.edit' , 'js/ems/docs/views/changeDocParent.html'                  ,'ChangeDocParentCtrl'   ]])
      .state(['root.corrs'                                    , '/corrs?displayName&correspondentEmail&limit&offset'                                                                                                                                                              ])
      .state(['root.corrs.search'                             , ''                                                                                                     , ['@root'           , 'js/ems/corrs/views/corrSearch.html'                      ,'CorrsSearchCtrl'       ]])
      .state(['root.corrs.new'                                , '/new'                                                                                                 , ['@root'           , 'js/ems/corrs/views/corrNew.html'                         ,'CorrsNewCtrl'          ]])
      .state(['root.corrs.edit'                               , '/:id'                                                                                                 , ['@root'           , 'js/ems/corrs/views/corrEdit.html'                        ,'CorrsEditCtrl'         ]]);
  }]);
}(angular));
