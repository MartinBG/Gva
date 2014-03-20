/*global angular*/
(function (angular) {
  'use strict';

  function DocumentTrainingsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentTraining,
    personDocumentTraining,
    selectedPublisher
  ) {
    $scope.isEdit = true;
    $scope.personDocumentTraining = personDocumentTraining;
    $scope.personDocumentTraining.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentTraining.part.documentPublisher;

    $scope.save = function () {
      $scope.editDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.editDocumentTrainingForm.$valid) {
            return PersonDocumentTraining
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentTraining)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentTrainings.search');
              });
          }
        });
    };

    $scope.choosePublisher = function () {
      return $state.go('root.persons.view.documentTrainings.edit.choosePublisher');
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentTrainings.search');
    };
  }

  DocumentTrainingsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentTraining',
    'personDocumentTraining',
    'selectedPublisher'
  ];

  DocumentTrainingsEditCtrl.$resolve = {
    personDocumentTraining: [
      '$stateParams',
      'PersonDocumentTraining',
      function ($stateParams, PersonDocumentTraining) {
        return PersonDocumentTraining.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentTrainingsEditCtrl', DocumentTrainingsEditCtrl);
}(angular));
