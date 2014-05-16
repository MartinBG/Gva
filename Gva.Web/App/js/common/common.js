/*jshint maxlen:false*/
/*global angular*/
(function (angular) {
  'use strict';
  angular.module('common', [
    'ng',
    'ngResource',
    'ui.router',
    'ui.bootstrap',
    //'common.templates',
    'l10n',
    'l10n-tools'
  ]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state(['root'             , null      , ['@'    , 'js/common/root/views/root.html'        , 'RootCtrl'       ]])
      .state(['root.users'       , '/users?username&fullname&showActive'                                          ])
      .state(['root.users.search', ''        , ['@root', 'js/common/users/views/search.html'     , 'UsersSearchCtrl']])
      .state(['root.users.new'   , '/new'    , ['@root', 'js/common/users/views/edit.html'       , 'UsersEditCtrl'  ]])
      .state(['root.users.edit'  , '/:userId', ['@root', 'js/common/users/views/edit.html'       , 'UsersEditCtrl'  ]]);
  }]);
}(angular));
