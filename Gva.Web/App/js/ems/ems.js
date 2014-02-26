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
      name: 'emsDocAddressingDocIncoming',
      templateUrl: 'ems/docs/forms/docAddressing/docIncoming.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocAddressingDocInternal',
      templateUrl: 'ems/docs/forms/docAddressing/docInternal.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocAddressingDocInternalOutgoing',
      templateUrl: 'ems/docs/forms/docAddressing/docInternalOutgoing.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocAddressingDocOutgoing',
      templateUrl: 'ems/docs/forms/docAddressing/docOutgoing.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocAddressingRemark',
      templateUrl: 'ems/docs/forms/docAddressing/remark.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocAddressingResolution',
      templateUrl: 'ems/docs/forms/docAddressing/resolution.html'
    });
    scaffoldingProvider.form({
      name: 'emsDocAddressingTask',
      templateUrl: 'ems/docs/forms/docAddressing/task.html'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.docs'                           , '/docs?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units&docIds&hasLot'                                    ])
      .state(['root.docs.search'                    , ''                             , ['@root'          , 'ems/docs/views/docsSearch.html'         , 'DocsSearchCtrl'         ]])
      .state(['root.docs.new'                       , '/new?parentDocId'             , ['@root'          , 'ems/docs/views/docsNew.html'            , 'DocsNewCtrl'            ]])
      .state(['root.docs.edit'                      , '/:docId'                      , ['@root'          , 'ems/docs/views/docsEdit.html'           , 'DocsEditCtrl'           ]])
      .state(['root.docs.edit.addressing'           , '/address'                     , ['@root.docs.edit', 'ems/docs/views/docsAddressing.html'     , 'DocsAddressingCtrl'     ]])
      .state(['root.docs.edit.addressing.selectCorr', '/selectCorr?displayName&email', ['@root.docs.edit', 'ems/docs/views/selectCorrView.html'     , 'SelectCorrViewCtrl'     ]])
      .state(['root.docs.edit.addressing.selectUnit', '/selectUnit?name'             , ['@root.docs.edit', 'ems/docs/views/selectUnitView.html'     , 'SelectUnitViewCtrl'     ]])
      .state(['root.docs.edit.content'              , '/content'                     , ['@root.docs.edit', 'ems/docs/views/docsContent.html'        , 'DocsContentCtrl'        ]])
      .state(['root.docs.edit.workflows'            , '/workflows'                   , ['@root.docs.edit', 'ems/docs/views/docsWorkflows.html'      , 'DocsWorkflowsCtrl'      ]])
      .state(['root.docs.edit.stages'               , '/stages'                      , ['@root.docs.edit', 'ems/docs/views/docsStages.html'         , 'DocsStagesCtrl'         ]])
      .state(['root.docs.edit.case'                 , '/case'                        , ['@root.docs.edit', 'ems/docs/views/docsCase.html'           , 'DocsCaseCtrl'           ]])
      .state(['root.docs.edit.classifications'      , '/classifications'             , ['@root.docs.edit', 'ems/docs/views/docsClassifications.html', 'DocsClassificationsCtrl']])
      .state(['root.corrs'                          , '/corrs?displayName&email'                                                                                                ])
      .state(['root.corrs.search'                   , ''                             , ['@root'          , 'ems/corrs/views/corrSearch.html'        , 'CorrsSearchCtrl'        ]])
      .state(['root.corrs.new'                      , '/new'                         , ['@root'          , 'ems/corrs/views/corrEdit.html'          , 'CorrsEditCtrl'          ]])
      .state(['root.corrs.edit'                     , '/:corrId'                     , ['@root'          , 'ems/corrs/views/corrEdit.html'          , 'CorrsEditCtrl'          ]]);
  }]);
}(angular));
