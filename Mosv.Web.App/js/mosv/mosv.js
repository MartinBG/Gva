/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('mosv', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    // @ifndef DEBUG
    'mosv.templates',
    // @endif
    'common',
    'l10n',
    'l10n-tools',
    'scrollto'
  ]).config(['scaffoldingProvider', function (scaffoldingProvider) {
    scaffoldingProvider.form({
      name: 'mosvSignal',
      templateUrl: 'js/mosv/forms/signal.html'
    });
    scaffoldingProvider.form({
      name: 'mosvSuggestion',
      templateUrl: 'js/mosv/forms/suggestion.html'
    });
    scaffoldingProvider.form({
      name: 'mosvAdmission',
      templateUrl: 'js/mosv/forms/admission.html'
    });
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
    .state(['root.signals'           , '/signals?exact&incomingLot&uin&incomingNumber&incomingDate&applicant&institution&violation'       ])
    .state(['root.signals.search'    , ''    , ['@root', 'js/mosv/views/signal/signalsSearch.html'        , 'SignalsSearchCtrl'           ]])
    .state(['root.signals.new'       , '/new', ['@root', 'js/mosv/views/signal/signalsNew.html'           , 'SignalsNewCtrl'              ]])
    .state(['root.signals.edit'      , '/:id', ['@root', 'js/mosv/views/signal/signalsEdit.html'          , 'SignalsEditCtrl'             ]])

    .state(['root.admissions'        , '/admissions?incomingNumber&incomingLot&applicant&incomingDate&applicantType'                      ])
    .state(['root.admissions.search' , ''    , ['@root', 'js/mosv/views/admission/admissionsSearch.html'  , 'AdmissionsSearchCtrl'        ]])
    .state(['root.admissions.new'    , '/new', ['@root', 'js/mosv/views/admission/admissionsNew.html'     , 'AdmissionsNewCtrl'           ]])
    .state(['root.admissions.edit'   , '/:id', ['@root', 'js/mosv/views/admission/admissionsEdit.html'    , 'AdmissionsEditCtrl'          ]])

    .state(['root.suggestions'       , '/suggestions?incomingNumber&incomingLot&applicant&incomingDateFrom&incomingDate'                  ])
    .state(['root.suggestions.search', ''    , ['@root', 'js/mosv/views/suggestion/suggestionsSearch.html', 'SuggestionsSearchCtrl'       ]])
    .state(['root.suggestions.new'   , '/new', ['@root', 'js/mosv/views/suggestion/suggestionsNew.html'   , 'SuggestionsNewCtrl'          ]])
    .state(['root.suggestions.edit'  , '/:id', ['@root', 'js/mosv/views/suggestion/suggestionsEdit.html'  , 'SuggestionEditCtrl'          ]]);
  }]);
}(angular));
