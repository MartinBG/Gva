/*global angular*/
(function (angular) {
  'use strict';

  function NewTrainingModalCtrl(
    $scope,
    $modalInstance,
    $stateParams,
    namedModal,
    PersonDocumentTrainings,
    personDocumentTraining
  ) {
    $scope.form = {};
    $scope.personDocumentTraining = personDocumentTraining;

    $scope.choosePublisher = function () {
      var modalInstance = namedModal.open('choosePublisher');

      modalInstance.result.then(function (publisherName) {
        $scope.personDocumentTraining.part.documentPublisher = publisherName;
      });

      return modalInstance.opened;
    };

    $scope.save = function () {
      return $scope.form.newDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentTrainingForm.$valid) {
            return PersonDocumentTrainings
              .save({ id: $stateParams.id }, $scope.personDocumentTraining)
              .$promise
              .then(function (savedTraining) {
                $modalInstance.close(savedTraining.partIndex);
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
    'namedModal',
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
