﻿/*global angular,_*/
(function (angular) {
  'use strict';

  function AppEditPartCtrl(
    $scope,
    $state,
    $stateParams,
    Applications,
    application) {
    var originalApplication = _.cloneDeep(application);

    $scope.application = application;
    $scope.editMode = null;
    $scope.lotId = $stateParams.lotId;
    $scope.set = $stateParams.set;
    $scope.appId = $stateParams.id;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.application = _.cloneDeep(originalApplication);
    };

    $scope.save = function () {
      return $scope.editDocumentApplicationForm.$validate().then(function () {
        if ($scope.editDocumentApplicationForm.$valid) {
          return Applications.editAppPart({
            path: $scope.set + 'DocumentApplications',
            lotId: $scope.lotId,
            ind: $stateParams.ind
          },
          $scope.application).$promise.then(function () {
            $scope.editMode = null;
          });
        }
      });
    };
  }

  AppEditPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Applications',
    'application'
  ];

  AppEditPartCtrl.$resolve = {
    application: [
      '$stateParams',
      'Applications',
      function ($stateParams, Applications) {
        return Applications.getAppPart({
          path: $stateParams.set + 'DocumentApplications',
          lotId: $stateParams.lotId,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AppEditPartCtrl', AppEditPartCtrl);
}(angular));
