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
      name: 'emsDocInfo',
      templateUrl: 'ems/docs/forms/docInfo.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocIncoming',
      templateUrl: 'ems/docs/forms/docView/docIncoming.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocInternal',
      templateUrl: 'ems/docs/forms/docView/docInternal.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocInternalOutgoing',
      templateUrl: 'ems/docs/forms/docView/docInternalOutgoing.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewDocOutgoing',
      templateUrl: 'ems/docs/forms/docView/docOutgoing.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewRemark',
      templateUrl: 'ems/docs/forms/docView/remark.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewResolution',
      templateUrl: 'ems/docs/forms/docView/resolution.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocViewTask',
      templateUrl: 'ems/docs/forms/docView/task.html'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.docs'                           , '/docs?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units&docIds&hasLot'                                      ])
      .state(['root.docs.search'                    , ''                             , ['@root'                , 'ems/docs/views/docsSearch.html'     , 'DocsSearchCtrl'         ]])
      .state(['root.docs.new'                       , '/new?parentDocId'             , ['@root'                , 'ems/docs/views/docsNew.html'        , 'DocsNewCtrl'            ]])
      .state(['root.docs.new.caseSelect'            , '/caseSelect?csFromDate&csToDate&csRegUri&csDocName&csDocTypeId&csDocStatusId&csCorrs&csUnits&csIsCase', ['@root'          , 'ems/docs/views/caseSelect.html'         , 'CaseSelectCtrl'         ]])
      .state(['root.docs.edit'                      , '/:docId'                      , ['@root'                , 'ems/docs/views/docsEdit.html'       , 'DocsEditCtrl'           ]])
      .state(['root.docs.edit.view'                 , '/view'                        , ['@root.docs.edit' , 'ems/docs/views/docsView.html'       , 'DocsViewCtrl'           ]])
      .state(['root.docs.edit.view.selectCorr'      , '/selectCorr?displayName&email', ['@root.docs.edit' , 'ems/docs/views/selectCorrView.html' , 'SelectCorrViewCtrl'     ]])
      .state(['root.docs.edit.view.selectUnit'      , '/selectUnit?name'             , ['@root.docs.edit' , 'ems/docs/views/selectUnitView.html' , 'SelectUnitViewCtrl'     ]])
      .state(['root.docs.edit.workflows'            , '/workflows'                   , ['@root.docs.edit' , 'ems/docs/views/docsWorkflows.html'  , 'DocsWorkflowsCtrl'      ]])
      .state(['root.docs.edit.stages'               , '/stages'                      , ['@root.docs.edit' , 'ems/docs/views/docsStages.html'     , 'DocsStagesCtrl'         ]])
      .state(['root.docs.edit.case'                 , '/case'                        , ['@root.docs.edit' , 'ems/docs/views/docsCase.html'       , 'DocsCaseCtrl'           ]])
      .state(['root.corrs'                          , '/corrs?displayName&email'                                                                                                  ])
      .state(['root.corrs.search'                   , ''                             , ['@root'                , 'ems/corrs/views/corrSearch.html'    , 'CorrsSearchCtrl'        ]])
      .state(['root.corrs.new'                      , '/new'                         , ['@root'                , 'ems/corrs/views/corrEdit.html'      , 'CorrsEditCtrl'          ]])
      .state(['root.corrs.edit'                     , '/:corrId'                     , ['@root'                , 'ems/corrs/views/corrEdit.html'      , 'CorrsEditCtrl'          ]]);
  }]);
}(angular));
