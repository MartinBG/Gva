/*global angular*/
(function (angular) {
  'use strict';

  function DocumentTrainingsEditCtrl($scope, $state, $stateParams, PersonDocumentTraining) {
    $scope.isEdit = true;
    PersonDocumentTraining
    .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
    .then(function (documentTraining) {
      $scope.personDocumentTraining = documentTraining;
    });

    $scope.save = function () {
      $scope.personDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.personDocumentTrainingForm.$valid) {
            return PersonDocumentTraining
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentTraining)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentTrainings.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentTrainings.search');
    };
  }

  DocumentTrainingsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentTraining'
  ];

  angular.module('gva').controller('DocumentTrainingsEditCtrl', DocumentTrainingsEditCtrl);
}(angular));
