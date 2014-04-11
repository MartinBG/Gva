/*global angular*/
(function (angular) {
  'use strict';

  function DocumentTrainingsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentTraining,
    personDocumentTraining,
    selectedPublisher
  ) {
    $scope.save = function () {
      return $scope.newDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.newDocumentTrainingForm.$valid) {
            return PersonDocumentTraining
              .save({ id: $stateParams.id }, $scope.personDocumentTraining)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentTrainings.search');
              });
          }
        });
    };
    $scope.personDocumentTraining = personDocumentTraining;
    $scope.personDocumentTraining.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentTraining.part.documentPublisher;
    $scope.choosePublisher = function () {
      return $state.go('root.persons.view.documentTrainings.new.choosePublisher');
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentTrainings.search');
    };
  }

  DocumentTrainingsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentTraining',
    'personDocumentTraining',
    'selectedPublisher'
  ];

  DocumentTrainingsNewCtrl.$resolve = {
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

  angular.module('gva').controller('DocumentTrainingsNewCtrl', DocumentTrainingsNewCtrl);
}(angular));
