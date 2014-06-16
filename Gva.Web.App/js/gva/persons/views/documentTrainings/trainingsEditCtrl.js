/*global angular,_*/
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

    $scope.deleteTraining = function () {
      return PersonDocumentTraining.remove({
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
    'PersonDocumentTraining',
    'personDocumentTraining',
    'selectedPublisher'
  ];

  DocumentTrainingsEditCtrl.$resolve = {
    personDocumentTraining: [
      '$stateParams',
      'PersonDocumentTraining',
      function ($stateParams, PersonDocumentTraining) {
        return PersonDocumentTraining.get($stateParams).$promise;
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentTrainingsEditCtrl', DocumentTrainingsEditCtrl);
}(angular));
