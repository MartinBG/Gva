/*global angular*/
(function (angular) {
  'use strict';

  function DocumentTrainingsSearchCtrl(
    $scope,
    $state,
    $stateParams,
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

    $scope.newDocumentTraining = function () {
      return $state.go('root.persons.view.documentTrainings.new');
    };
  }

  DocumentTrainingsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'trainings'
  ];

  DocumentTrainingsSearchCtrl.$resolve = {
    trainings: [
      '$stateParams',
      'PersonDocumentTrainings',
      function ($stateParams, PersonDocumentTrainings) {
        return PersonDocumentTrainings.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentTrainingsSearchCtrl', DocumentTrainingsSearchCtrl);
}(angular));
