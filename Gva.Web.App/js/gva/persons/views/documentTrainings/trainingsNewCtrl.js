/*global angular*/
(function (angular) {
  'use strict';

  function DocumentTrainingsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentTrainings,
    personDocumentTraining
  ) {
    $scope.personDocumentTraining = personDocumentTraining;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.newDocumentTrainingForm.$valid) {
            return PersonDocumentTrainings
              .save({ id: $stateParams.id }, $scope.personDocumentTraining)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentTrainings.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentTrainings.search');
    };
  }

  DocumentTrainingsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentTrainings',
    'personDocumentTraining'
  ];

  DocumentTrainingsNewCtrl.$resolve = {
    personDocumentTraining: [
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

  angular.module('gva').controller('DocumentTrainingsNewCtrl', DocumentTrainingsNewCtrl);
}(angular));
