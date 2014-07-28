/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentTrainingsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentTrainings,
    personDocumentTraining,
    scMessage
  ) {
    var originalTraining = _.cloneDeep(personDocumentTraining);

    $scope.personDocumentTraining = personDocumentTraining;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentTraining = _.cloneDeep(originalTraining);
    };

    $scope.save = function () {
      return $scope.editDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.editDocumentTrainingForm.$valid) {
            return PersonDocumentTrainings
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentTraining)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentTrainings.search');
              });
          }
        });
    };

    $scope.deleteTraining = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonDocumentTrainings.remove({
            id: $stateParams.id,
            ind: personDocumentTraining.partIndex
          }).$promise.then(function () {
            return $state.go('root.persons.view.documentTrainings.search');
          });
        }
      });
    };
  }

  DocumentTrainingsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentTrainings',
    'personDocumentTraining',
    'scMessage'
  ];

  DocumentTrainingsEditCtrl.$resolve = {
    personDocumentTraining: [
      '$stateParams',
      'PersonDocumentTrainings',
      function ($stateParams, PersonDocumentTrainings) {
        return PersonDocumentTrainings.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentTrainingsEditCtrl', DocumentTrainingsEditCtrl);
}(angular));
