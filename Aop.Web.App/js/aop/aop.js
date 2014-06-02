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
    'ems.templates',
    // @endif
    'l10n',
    'l10n-tools'
  ]).config(['scaffoldingProvider', function (scaffoldingProvider) {
    scaffoldingProvider.form({
      name: 'emsDocFileChecklist',
      templateUrl: 'js/ems/docs/forms/editableFile/checklist.html',
      controller: 'ChecklistCtrl'
    });
    scaffoldingProvider.form({
      name: 'emsEditableFile',
      templateUrl: 'js/ems/docs/forms/editableFile/editableFile.html',
      controller: 'EditableFileCtrl'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root.apps'                                     , '/aop/app?limit&offset'                                                                                                                                                                                    ])
      .state(['root.apps.search'                              , ''                                                                                                     , ['@root'           , 'js/aop/views/appSearch.html'                         ,'AppsSearchCtrl'        ]])
      .state(['root.apps.edit'                                , '/:id'                                                                                                 , ['@root'           , 'js/aop/views/appEdit.html'                           ,'AppsEditCtrl'          ]])
      .state(['root.apps.edit.docSelect'                      , '/caseSelect?csFromDate&csToDate&csRegUri&csDocName&csDocTypeId&csDocStatusId&csCorrs&csUnits&csIsCase', ['@root'           , 'js/ems/docs/views/caseSelect.html'                       ,'AppDocSelectCtrl'      ]])
      .state(['root.apps.edit.newAopEmployer'                 , '/newAopEmployer'                                                                                      , ['@root'           , 'js/aop/views/appEmployerNew.html'                    ,'AppEmployerNewCtrl'    ]]);
  }]);
}(angular));
