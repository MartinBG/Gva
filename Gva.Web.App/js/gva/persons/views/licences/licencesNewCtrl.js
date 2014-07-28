/*global angular*/
(function (angular) {
  'use strict';

  function LicencesNewCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    PersonLicences,
    licence
  ) {
    $scope.licence = licence;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newLicenceForm.$validate().then(function () {
        if ($scope.newLicenceForm.$valid) {
          return PersonLicences
            .save({ id: $stateParams.id }, $scope.licence).$promise
            .then(function () {
              return $state.go('root.persons.view.licences.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.licences.search');
    };
  }

  LicencesNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$filter',
    'PersonLicences',
    'licence'
  ];

  LicencesNewCtrl.$resolve = {
    licence: [
      '$stateParams',
      'PersonLicences',
      function ($stateParams, PersonLicences) {
        return PersonLicences.init({ id: $stateParams.id, appId: $stateParams.appId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicencesNewCtrl', LicencesNewCtrl);
}(angular));
