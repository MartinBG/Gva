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
    'l10n-tools',
    'xeditable'
  ])

  .config(['scModalProvider', function (scModalProvider) {
      //jscs:disable disallowSpaceBeforeBinaryOperators, disallowSpacesInsideArrayBrackets, maximumLineLength
    scModalProvider
    .modal('editUnitModal', 'js/ems/units/editUnitModal.html', 'EditUnitModalCtrl')
    .modal('selectUserModal', 'js/ems/units/selectUserModal.html', 'SelectUserModalCtrl');
  }])

  .config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root'                                    , null                    , ['@'                          , 'js/common/root/views/root.html'                           , 'RootCtrl'                    ]])
      .state(['root.users'                              , '/users?username&fullname&showActive'                                                                                                                 ])
      .state(['root.users.search'                       , ''                      , ['@root'                      , 'js/common/users/views/search.html'                        , 'UsersSearchCtrl'             ]])
      .state(['root.users.new'                          , '/new'                  , ['@root'                      , 'js/common/users/views/edit.html'                          , 'UsersEditCtrl'               ]])
      .state(['root.users.edit'                         , '/:userId'              , ['@root'                      , 'js/common/users/views/edit.html'                          , 'UsersEditCtrl'               ]])
      //.state(['root.nomenclatures'                      , '/nomenclatures'                                                                                                                                      ])
      //.state(['root.nomenclatures.search'               , ''                      , ['@root'                      , 'js/common/nomenclatures/views/nomenclatures.html'         , 'NomenclaturesCtrl'           ]])
      //.state(['root.nomenclatures.edit'                 , '/:id'                  , ['@root'                      , 'js/common/nomenclatures/views/nomenclaturesEdit.html'     , 'NomenclaturesEditCtrl'       ]])
      //.state(['root.nomenclatures.search.values'        , '/:nomId/values'        , ['@root.nomenclatures.search' , 'js/common/nomenclatures/views/nomenclatureValues.html'    , 'NomenclatureValuesCtrl'      ]])
      //.state(['root.nomenclatures.search.values.edit', '/:id', ['@root.nomenclatures.search', 'js/common/nomenclatures/views/nomenclatureValuesEdit.html', 'NomenclaturevaluesEditCtrl'                        ]])
      .state(['root.units'                              , '/units'                , ['@root'                      , 'js/ems/units/unitsView.html'                              , 'UnitsCtrl'                   ]])
      .state(['root.docNomenclatures', '/docNomenclatures?category', ['@root', 'js/common/nomenclatures/docNomenclaturesView.html']])
      .state(['root.docNomenclatures.docTypes'          , '/docTypes', ['@root.docNomenclatures', 'js/common/nomenclatures/docTypeView.html', 'DocNomenclatureGenericController']])
      .state(['root.docNomenclatures.docTypeGroups'     , '/docTypeGroups', ['@root.docNomenclatures', 'js/common/nomenclatures/docTypeGroupView.html', 'DocNomenclatureGenericController']]);
  }]);
}(angular));
