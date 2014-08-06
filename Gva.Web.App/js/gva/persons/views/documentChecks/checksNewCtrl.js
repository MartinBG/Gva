/*global angular*/
(function (angular) {
  'use strict';

  function DocumentChecksNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentChecks,
    personDocumentCheck
  ) {
    $scope.personDocumentCheck = personDocumentCheck;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newDocumentCheckForm.$validate()
        .then(function () {
          if ($scope.newDocumentCheckForm.$valid) {
            return PersonDocumentChecks
              .save({ id: $stateParams.id }, $scope.personDocumentCheck)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.checks.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.checks.search');
    };
  }

  DocumentChecksNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentChecks',
    'personDocumentCheck'
  ];
  DocumentChecksNewCtrl.$resolve = {
    personDocumentCheck: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ isAdded: true, applications: [application] }]
          };
        }
        else {
          return {
            part: {},
            files: []
          };
        }
      }
    ]
  };

  angular.module('gva').controller('DocumentChecksNewCtrl', DocumentChecksNewCtrl);
}(angular));
