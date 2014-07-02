/*global angular*/
(function (angular) {
  'use strict';

  function LicencesNewCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    PersonLicences,
    person,
    licence
  ) {
    $scope.person = person;
    $scope.licence = licence;

    $scope.backFromChild = false;

    if ($state.previous && $state.previous.includes[$state.current.name]) {
      $scope.backFromChild = true;
    }

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
    'person',
    'licence'
  ];

  LicencesNewCtrl.$resolve = {
    licence: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {
              editions: [{ applications: [application] }]
            }
          };
        }
        else {
          return {
            part: {
              editions: [{}]
            }
          };
        }
      }
    ]
  };

  angular.module('gva').controller('LicencesNewCtrl', LicencesNewCtrl);
}(angular));
