/*global angular*/
(function (angular) {
  'use strict';

  function DocumentTrainingsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentTraining,
    trainings
  ) {
    $scope.documentTrainings = trainings;

    $scope.editDocumentTraining = function (documentTraining) {
      return $state.go('root.persons.view.documentTrainings.edit',
        {
          id: $stateParams.id,
          ind: documentTraining.partIndex
        });
    };

    $scope.deleteDocumentTraining = function (documentTraining) {
      return PersonDocumentTraining.remove({ id: $stateParams.id, ind: documentTraining.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newDocumentTraining = function () {
      return $state.go('root.persons.view.documentTrainings.new');
    };
  }

  DocumentTrainingsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentTraining',
    'trainings'
  ];

  DocumentTrainingsSearchCtrl.$resolve = {
    trainings: [
      '$stateParams',
      'PersonDocumentTraining',
      function ($stateParams, PersonDocumentTraining) {
        return PersonDocumentTraining.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentTrainingsSearchCtrl', DocumentTrainingsSearchCtrl);
}(angular));
