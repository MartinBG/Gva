/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('aop', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    'common',
    // @ifndef DEBUG
    'aop.templates',
    // @endif
    'l10n',
    'l10n-tools'
  ]).config(['scaffoldingProvider', function (scaffoldingProvider) {
    scaffoldingProvider.form({
      name: 'emsChecklistContent',
      templateUrl: 'js/ems/docs/forms/checklist/checklistContent.html',
      controller: 'ChecklistContentCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsChecklist',
      templateUrl: 'js/ems/docs/forms/checklist/checklistHeader.html',
      controller: 'ChecklistHeaderCtrl'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.apps'                                     , '/aop/app?limit&offset'                                                                                                                                                                                           ])
      .state(['root.apps.search'                              , ''                                                                                                            , ['@root'           , 'js/aop/views/appSearch.html'                         ,'AppsSearchCtrl'      ]])
      .state(['root.apps.edit'                                , '/:id'                                                                                                        , ['@root'           , 'js/aop/views/appEdit.html'                           ,'AppsEditCtrl'        ]])
      .state(['root.apps.edit.docSelect'                      , '/docSelect?type&csFromDate&csToDate&csRegUri&csDocName&csDocTypeId&csDocStatusId&csCorrs&csUnits&csIsChosen' , ['@root'           , 'js/aop/views/docSelect.html'                         ,'AppDocSelectCtrl'    ]])
      .state(['root.apps.edit.newAopEmployer'                 , '/newAopEmployer'                                                                                             , ['@root'           , 'js/aop/views/appEmployerNew.html'                    ,'AppEmployerNewCtrl'  ]]);
  }]);
}(angular));
