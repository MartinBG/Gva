/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentTrainingsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentTrainings,
    personDocumentTraining,
    selectedPublisher
  ) {
    var originalTraining = _.cloneDeep(personDocumentTraining);

    $scope.personDocumentTraining = personDocumentTraining;
    $scope.personDocumentTraining.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentTraining.part.documentPublisher;
    $scope.editMode = null;
    $scope.backFromChild = false;

    if ($state.previous && $state.previous.includes[$state.current.name]) {
      $scope.backFromChild = true;
    }

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

    $scope.choosePublisher = function () {
      return $state.go('root.persons.view.documentTrainings.edit.choosePublisher');
    };

    $scope.deleteTraining = function () {
      return PersonDocumentTrainings.remove({
        id: $stateParams.id,
        ind: personDocumentTraining.partIndex
      }).$promise.then(function () {
        return $state.go('root.persons.view.documentTrainings.search');
      });
    };
  }

  DocumentTrainingsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentTrainings',
    'personDocumentTraining',
    'selectedPublisher'
  ];

  DocumentTrainingsEditCtrl.$resolve = {
    personDocumentTraining: [
      '$stateParams',
      'PersonDocumentTrainings',
      function ($stateParams, PersonDocumentTrainings) {
        return PersonDocumentTrainings.get($stateParams).$promise;
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentTrainingsEditCtrl', DocumentTrainingsEditCtrl);
}(angular));
