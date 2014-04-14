/*global angular*/
(function (angular) {
  'use strict';

  function AddTrainingCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentTraining,
    personDocumentTraining,
    selectedPublisher
  ) {
    $scope.save = function () {
      $scope.newDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.newDocumentTrainingForm.$valid) {
            return PersonDocumentTraining
              .save({ id: $stateParams.id }, $scope.personDocumentTraining).$promise
              .then(function (savedTraining) {
                return $state.go('^', {}, {}, {
                  selectedTrainings: [savedTraining.partIndex]
                });
              });
          }
        });
    };
    $scope.personDocumentTraining = personDocumentTraining;
    $scope.personDocumentTraining.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentTraining.part.documentPublisher;
    $scope.choosePublisher = function () {
      return $state.go('.choosePublisher');
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  AddTrainingCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentTraining',
    'personDocumentTraining',
    'selectedPublisher'
  ];

  AddTrainingCtrl.$resolve = {
    personDocumentTraining: function () {
      return {
        part: {},
        files: []
      };
    },
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('AddTrainingCtrl', AddTrainingCtrl);
}(angular));
