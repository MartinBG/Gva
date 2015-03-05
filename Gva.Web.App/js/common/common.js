/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('common', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    // @ifndef DEBUG
    'common.templates',
    // @endif
    'l10n',
    'l10n-tools'
  ]).config(['scModalProvider', function (scModalProvider) {
      //jscs:disable disallowSpaceBeforeBinaryOperators, disallowSpacesInsideArrayBrackets, maximumLineLength
    scModalProvider
     .modal('changePassword', 'js/common/users/modals/changePasswordModal.html', 'ChangePasswordModalCtrl', 'xsm');
  }]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root'                                           , null                    , ['@'                         , 'js/common/root/views/root.html'                           , 'RootCtrl'                    ]])
      .state(['root.users'                                     , '/users?username&fullname&showActive'                                                                                                                ])
      .state(['root.users.search'                              , ''                      , ['@root'                     , 'js/common/users/views/search.html'                        , 'UsersSearchCtrl'             ]])
      .state(['root.users.new'                                 , '/new'                  , ['@root'                     , 'js/common/users/views/edit.html'                          , 'UsersEditCtrl'               ]])
      .state(['root.users.edit'                                , '/:userId'              , ['@root'                     , 'js/common/users/views/edit.html'                          , 'UsersEditCtrl'               ]])
      .state(['root.nomenclatures'                             , '/nomenclatures'                                                                                                                                     ])
      .state(['root.nomenclatures.search'                      , '?category&alias'       , ['@root'                     , 'js/common/nomenclatures/views/nomenclatures.html'         , 'NomenclaturesCtrl'           ]])
      .state(['root.nomenclatures.search.licences'             , '/licences'             , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.ratings'              , '/ratings'              , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.system'               , '/system'               , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.airports'             , '/airports'             , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.equipments'           , '/equipments'           , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.persons'              , '/persons'              , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.aircrafts'            , '/aircrafts'            , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.orgCommon'            , '/orgCommon'            , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.org145mf'             , '/org145mf'             , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.org147'               , '/org147'               , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.orgReport'            , '/orgreport'            , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/categoryNoms.html'          , 'CategoryNomsCtrl'            ]])
      .state(['root.nomenclatures.search.values'               , '/:nomId/values'        , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/nomenclatureValues.html'    , 'NomenclatureValuesCtrl'      ]])
      .state(['root.nomenclatures.search.values.edit'          , '/:id'                  , ['@root.nomenclatures.search', 'js/common/nomenclatures/views/nomenclatureValuesEdit.html', 'NomenclaturevaluesEditCtrl'  ]]);
  }]);
}(angular));
