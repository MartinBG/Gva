/*global angular*/
(function (angular) {
  'use strict';

  function NewTrainingModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentTrainings,
    scModalParams,
    personDocumentTraining
  ) {
    $scope.form = {};
    $scope.personDocumentTraining = personDocumentTraining;
    $scope.lotId = scModalParams.lotId;
    $scope.caseTypeId = scModalParams.caseTypeId;

    $scope.save = function () {
      return $scope.form.newDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentTrainingForm.$valid) {
            return PersonDocumentTrainings
              .save({ id: $scope.lotId }, $scope.personDocumentTraining)
              .$promise
              .then(function (savedTraining) {
                return $modalInstance.close(savedTraining);
              });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewTrainingModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonDocumentTrainings',
    'scModalParams',
    'personDocumentTraining'
  ];

  NewTrainingModalCtrl.$resolve = {
    personDocumentTraining: [
      'PersonDocumentTrainings',
      'scModalParams',
      function (PersonDocumentTrainings, scModalParams) {
        return PersonDocumentTrainings.newTraining({
          id: scModalParams.lotId,
          appId: scModalParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('NewTrainingModalCtrl', NewTrainingModalCtrl);
}(angular));
