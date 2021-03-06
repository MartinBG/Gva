﻿/*global angular*/
(function (angular) {
  'use strict';

  function DocumentTrainingsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentTrainings,
    personDocumentTraining
  ) {
    $scope.personDocumentTraining = personDocumentTraining;
    $scope.lotId = $stateParams.id;
    $scope.appId = $stateParams.appId;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newDocumentTrainingForm.$validate()
        .then(function () {
          if ($scope.newDocumentTrainingForm.$valid) {
            return PersonDocumentTrainings
              .save({ id: $stateParams.id }, $scope.personDocumentTraining)
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

  DocumentTrainingsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentTrainings',
    'personDocumentTraining'
  ];

  DocumentTrainingsNewCtrl.$resolve = {
    personDocumentTraining: [
      '$stateParams',
      'PersonDocumentTrainings',
      function ($stateParams, PersonDocumentTrainings) {
        return PersonDocumentTrainings.newTraining({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentTrainingsNewCtrl', DocumentTrainingsNewCtrl);
}(angular));
