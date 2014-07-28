/*global angular*/
(function (angular) {
  'use strict';

  function NewTrainingModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentTrainings,
    personDocumentTraining,
    lotId
  ) {
    $scope.form = {};
    $scope.personDocumentTraining = personDocumentTraining;

    $scope.save = function () {
      return $scope.form.newDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentTrainingForm.$valid) {
            return PersonDocumentTrainings
              .save({ id: lotId }, $scope.personDocumentTraining)
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
    'personDocumentTraining',
    'lotId'
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
