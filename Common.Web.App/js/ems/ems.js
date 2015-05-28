/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('ems', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'textAngular',
    'common',
    // @ifndef DEBUG
    'ems.templates',
    // @endif
    'scaffolding',
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
      templateUrl: 'js/ems/docs/forms/docView/docClassification.html'
    });
    scaffoldingProvider.form({
      name: 'emsRemovingIrregularity',
      templateUrl: 'js/ems/docs/forms/removingIrregularity/removingIrregularity.html',
      controller: 'RemovingIrregularityCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsIndividualActRefusal',
      templateUrl: 'js/ems/docs/forms/individualActRefusal/individualActRefusal.html',
      controller: 'IndividualActRefusalCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsConsiderationRefusal',
      templateUrl: 'js/ems/docs/forms/considerationRefusal/considerationRefusal.html',
      controller: 'ConsiderationRefusalCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsReceiptNotAcknowledge',
      templateUrl: 'js/ems/docs/forms/receiptNotAcknowledge/receiptNotAcknowledge.html',
      controller: 'ReceiptNotAcknowledgeCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsReceiptAcknowledge',
      templateUrl: 'js/ems/docs/forms/receiptAcknowledge/receiptAcknowledge.html',
      controller: 'ReceiptAcknowledgeCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsCompetenceTransfer',
      templateUrl: 'js/ems/docs/forms/competenceTransfer/competenceTransfer.html',
      controller: 'CompetenceTransferCtrl'
    });
  }]).config(['scModalProvider', function (scModalProvider) {
    scModalProvider
     .modal('chooseUnit', 'js/ems/docs/modals/chooseUnitModal.html', 'ChooseUnitModalCtrl')
     .modal('chooseCorr', 'js/ems/docs/modals/chooseCorrModal.html', 'ChooseCorrModalCtrl')
     .modal('newCorr', 'js/ems/docs/modals/newCorrModal.html', 'NewCorrModalCtrl')
     .modal('sendTransferDoc', 'js/ems/docs/modals/sendTransferDocModal.html', 'SendTransferDocModalCtrl');
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.docs'                                     , '/docs?filter&fromDate&toDate&regUri&docName&docTypeId&docStatusId&hideRead&isCase&corrs&units&ds&hasLot'                                                                                                           ])
      .state(['root.docs.search'                              , ''                                                                                                     , ['@root'           , 'js/ems/docs/views/docsSearch.html'                       ,'DocsSearchCtrl'           ]])
      .state(['root.docs.new'                                 , '/new?parentDocId&eDoc'                                                                                , ['@root'           , 'js/ems/docs/views/docsNew.html'                          ,'DocsNewCtrl'              ]])
      .state(['root.docs.new.caseSelect'                      , '/caseSelect?csFromDate&csToDate&csRegUri&csDocName&csDocTypeId&csDocStatusId&csCorrs&csUnits&csIsCase', ['@root'           , 'js/ems/docs/views/caseSelect.html'                       ,'CaseSelectCtrl'           ]])
      .state(['root.docs.news'                                , '/news'                                                                                                , ['@root'           , 'js/ems/docs/views/docsNews.html'                         ,'DocsNewsCtrl'             ]])
      .state(['root.docs.edit'                                , '/:id'                                                                                                 , ['@root'           , 'js/ems/docs/views/docsEdit.html'                         ,'DocsEditCtrl'             ]])
      .state(['root.docs.edit.view'                           , '/view'                                                                                                , ['@root.docs.edit' , 'js/ems/docs/views/docsView.html'                         ,'DocsViewCtrl'             ]])
      .state(['root.docs.edit.workflows'                      , '/workflows'                                                                                           , ['@root.docs.edit' , 'js/ems/docs/views/docsWorkflows.html'                    ,'DocsWorkflowsCtrl'        ]])
      .state(['root.docs.edit.workflows.signRequest'          , '/signRequest'                                                                                         , ['@root.docs.edit' , 'js/ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'      ]])
      .state(['root.docs.edit.workflows.discussRequest'       , '/discussRequest'                                                                                      , ['@root.docs.edit' , 'js/ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'      ]])
      .state(['root.docs.edit.workflows.approvalRequest'      , '/approvalRequest'                                                                                     , ['@root.docs.edit' , 'js/ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'      ]])
      .state(['root.docs.edit.workflows.registrationRequest'  , '/registrationRequest'                                                                                 , ['@root.docs.edit' , 'js/ems/docs/views/workflowRequest.html'                  ,'WorkflowRequestCtrl'      ]])
      .state(['root.docs.edit.workflows.signConfirm'          , '/signConfirm'                                                                                         , ['@root.docs.edit' , 'js/ems/docs/views/workflowConfirm.html'                  ,'WorkflowConfirmCtrl'      ]])
      .state(['root.docs.edit.workflows.discussConfirm'       , '/discussConfirm'                                                                                      , ['@root.docs.edit' , 'js/ems/docs/views/workflowConfirm.html'                  ,'WorkflowConfirmCtrl'      ]])
      .state(['root.docs.edit.workflows.approvalConfirm'      , '/approvalConfirm'                                                                                     , ['@root.docs.edit' , 'js/ems/docs/views/workflowConfirm.html'                  ,'WorkflowConfirmCtrl'      ]])
      .state(['root.docs.edit.stages'                         , '/stages'                                                                                              , ['@root.docs.edit' , 'js/ems/docs/views/docsStages.html'                       ,'DocsStagesCtrl'           ]])
      .state(['root.docs.edit.stages.next'                    , '/next'                                                                                                , ['@root.docs.edit' , 'js/ems/docs/views/nextStage.html'                        ,'NextStageCtrl'            ]])
      .state(['root.docs.edit.stages.edit'                    , '/edit'                                                                                                , ['@root.docs.edit' , 'js/ems/docs/views/editStage.html'                        ,'EditStageCtrl'            ]])
      .state(['root.docs.edit.stages.end'                     , '/end'                                                                                                 , ['@root.docs.edit' , 'js/ems/docs/views/endStage.html'                         ,'EndStageCtrl'             ]])
      .state(['root.docs.edit.case'                           , '/case'                                                                                                , ['@root.docs.edit' , 'js/ems/docs/views/docsCase.html'                         ,'DocsCaseCtrl'             ]])
      .state(['root.docs.edit.case.casePart'                  , '/casePart'                                                                                            , ['@root.docs.edit' , 'js/ems/docs/views/editCasePart.html'                     ,'EditCasePartCtrl'         ]])
      .state(['root.docs.edit.case.docType'                   , '/docType'                                                                                             , ['@root.docs.edit' , 'js/ems/docs/views/editDocType.html'                      ,'EditDocTypeCtrl'          ]])
      .state(['root.docs.edit.case.manualRegister'            , '/manualRegister'                                                                                      , ['@root.docs.edit' , 'js/ems/docs/views/manualRegister.html'                   ,'ManualRegisterCtrl'       ]])
      .state(['root.docs.edit.case.caseFinish'                , '/caseFinish'                                                                                          , ['@root.docs.edit' , 'js/ems/docs/views/caseFinish.html'                       ,'CaseFinishCtrl'           ]])
      .state(['root.docs.edit.case.changeDocParent'           , '/changeDocParent'                                                                                     , ['@root.docs.edit' , 'js/ems/docs/views/changeDocParent.html'                  ,'ChangeDocParentCtrl'      ]])
      .state(['root.docs.edit.case.sendEmail'                 , '/sendEmail'                                                                                           , ['@root.docs.edit' , 'js/ems/docs/views/sendEmail.html'                        ,'SendEmailCtrl'            ]])
      .state(['root.docs.edit.case.docClassification'         , '/docClassification'                                                                                   , ['@root.docs.edit' , 'js/ems/docs/views/editDocClassification.html'            ,'EditDocClassificationCtrl']])
      .state(['root.corrs'                                    , '/corrs?displayName&correspondentEmail&limit&offset'                                                                                                                                                                ])
      .state(['root.corrs.search'                             , ''                                                                                                     , ['@root'           , 'js/ems/corrs/views/corrSearch.html'                      ,'CorrsSearchCtrl'          ]])
      .state(['root.corrs.new'                                , '/new'                                                                                                 , ['@root'           , 'js/ems/corrs/views/corrNew.html'                         ,'CorrsNewCtrl'             ]])
      .state(['root.corrs.edit'                               , '/:id'                                                                                                 , ['@root'           , 'js/ems/corrs/views/corrEdit.html'                        ,'CorrsEditCtrl'            ]]);
  }]).config(['$provide', function($provide){
    // this demonstrates how to register a new tool and add it to the default toolbar
    $provide.decorator('taOptions', ['$delegate', function(taOptions){
      // $delegate is the taOptions we are decorating
      // here we override the default toolbars and classes specified in taOptions.
      taOptions.toolbar = [
          ['h1', 'h2', 'h3', 'h4', 'h5', 'h6', 'p', 'quote'],
          ['bold', 'italics', 'underline'],
          ['ul', 'ol'],
          ['justifyLeft', 'justifyCenter', 'justifyRight'],
          ['redo', 'undo', 'clear']
      ];
      taOptions.classes = {
        focussed: 'focussed',
        toolbar: 'btn-toolbar',
        toolbarGroup: 'btn-group btn-group-sm',
        toolbarButton: 'btn btn-default',
        toolbarButtonActive: 'active',
        disabled: 'disabled',
        textEditor: 'form-control',
        htmlEditor: 'form-control'
      };
      return taOptions;
    }]);
  }]);
}(angular));
