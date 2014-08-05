﻿/*global angular*/
(function (angular) {
  'use strict';

  function ChooseAppTypesModalCtrl(
    $scope,
    $modalInstance,
    Nomenclatures,
    appTypes
  ) {
    $scope.appTypes = appTypes;

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.chooseAppType = function (appType) {
      return $modalInstance.close(appType);
    };
  }

  ChooseAppTypesModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Nomenclatures',
    'appTypes'
  ];

  ChooseAppTypesModalCtrl.$resolve = {
    appTypes: [
      'Nomenclatures',
      'scModalParams',
      function (Nomenclatures, scModalParams) {
        return Nomenclatures.query({
          alias: 'applicationTypes',
          caseTypeAlias: scModalParams.caseType.alias
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseAppTypesModalCtrl', ChooseAppTypesModalCtrl);
}(angular));
