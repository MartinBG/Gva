/*global angular*/
(function (angular) {
  'use strict';

  function NewTrainingModalCtrl(
    $scope,
    $modalInstance,
    $stateParams,
    PersonDocumentTrainings,
    personDocumentTraining
  ) {
    $scope.form = {};
    $scope.personDocumentTraining = personDocumentTraining;

    $scope.save = function () {
      return $scope.form.newDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentTrainingForm.$valid) {
            return PersonDocumentTrainings
              .save({ id: $stateParams.id }, $scope.personDocumentTraining)
              .$promise
              .then(function (savedTraining) {
                return $modalInstance.close(savedTraining.partIndex);
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
    '$stateParams',
    'PersonDocumentTrainings',
    'personDocumentTraining'
  ];

  NewTrainingModalCtrl.$resolve = {
    personDocumentTraining: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('NewTrainingModalCtrl', NewTrainingModalCtrl);
}(angular));
