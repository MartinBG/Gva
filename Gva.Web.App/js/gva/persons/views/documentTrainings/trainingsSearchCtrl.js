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
