/*global angular*/
(function (angular) {
  'use strict';

  function ChooseAppTypesModalCtrl(
    $scope,
    $modalInstance,
    Nomenclatures,
    scModalParams,
    appTypes
  ) {
    $scope.appTypes = appTypes;
    $scope.searchParams = {
      code: null,
      alias: 'applicationTypes',
      caseTypeAlias: scModalParams.caseType.alias,
      name: null
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.search = function () {
      return Nomenclatures.query($scope.searchParams)
        .$promise.then(function (appTypes) {
          $scope.appTypes = appTypes;
        });
    };

    $scope.chooseAppType = function (appType) {
      return $modalInstance.close(appType);
    };
  }

  ChooseAppTypesModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Nomenclatures',
    'scModalParams',
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
