﻿/*jshint maxlen:false*/
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
  ]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.docs'                     , '/docs?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units&docIds&hasLot'                                    ])
      .state(['root.docs.search'              , ''                             , ['@root'          , 'ems/docs/views/docsSearch.html'         , 'DocsSearchCtrl'         ]])
      .state(['root.docs.new'                 , '/new?parentDocId'             , ['@root'          , 'ems/docs/views/docsNew.html'            , 'DocsNewCtrl'            ]])
      .state(['root.docs.edit'                , '/:docId'                      , ['@root'          , 'ems/docs/views/docsEdit.html'           , 'DocsEditCtrl'           ]])
      .state(['root.docs.edit.addressing'     , '/address'                     , ['@root.docs.edit', 'ems/docs/views/docsAddressing.html'     , 'DocsAddressingCtrl'     ]])
      .state(['root.docs.edit.chooseCorr'     , '/choosecorr?displayName&email', ['@root.docs.edit', 'ems/docs/views/chooseCorrView.html'     , 'ChooseCorrViewCtrl'     ]])
      .state(['root.docs.edit.chooseUnit'     , '/chooseunit?name'             , ['@root.docs.edit', 'ems/docs/views/chooseUnitView.html'     , 'ChooseUnitViewCtrl'     ]])
      .state(['root.docs.edit.content'        , '/content'                     , ['@root.docs.edit', 'ems/docs/views/docsContent.html'        , 'DocsContentCtrl'        ]])
      .state(['root.docs.edit.workflows'      , '/workflows'                   , ['@root.docs.edit', 'ems/docs/views/docsWorkflows.html'      , 'DocsWorkflowsCtrl'      ]])
      .state(['root.docs.edit.stages'         , '/stages'                      , ['@root.docs.edit', 'ems/docs/views/docsStages.html'         , 'DocsStagesCtrl'         ]])
      .state(['root.docs.edit.case'           , '/case'                        , ['@root.docs.edit', 'ems/docs/views/docsCase.html'           , 'DocsCaseCtrl'           ]])
      .state(['root.docs.edit.classifications', '/classifications'             , ['@root.docs.edit', 'ems/docs/views/docsClassifications.html', 'DocsClassificationsCtrl']])
      .state(['root.corrs'                    , '/corrs?displayName&email'                                                                                                ])
      .state(['root.corrs.search'             , ''                             , ['@root'          , 'ems/corrs/views/corrSearch.html'        , 'CorrsSearchCtrl'        ]])
      .state(['root.corrs.new'                , '/new'                         , ['@root'          , 'ems/corrs/views/corrEdit.html'          , 'CorrsEditCtrl'          ]])
      .state(['root.corrs.edit'               , '/:corrId'                     , ['@root'          , 'ems/corrs/views/corrEdit.html'          , 'CorrsEditCtrl'          ]]);
  }]);
}(angular));
