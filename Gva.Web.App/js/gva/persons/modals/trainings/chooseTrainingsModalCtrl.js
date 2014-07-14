/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseTrainingsModalCtrl(
    $scope,
    $modalInstance,
    trainings,
    includedTrainings
  ) {
    $scope.selectedTrainings = [];

    $scope.trainings = _.filter(trainings, function (training) {
      return !_.contains(includedTrainings, training.partIndex);
    });

    $scope.addTrainings = function () {
      return $modalInstance.close(_.pluck($scope.selectedTrainings, 'partIndex'));
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectTraining = function (event, training) {
      if ($(event.target).is(':checked')) {
        $scope.selectedTrainings.push(training);
      }
      else {
        $scope.selectedTrainings = _.without($scope.selectedTrainings, training);
      }
    };
  }

  ChooseTrainingsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'trainings',
    'includedTrainings'
  ];

  ChooseTrainingsModalCtrl.$resolve = {
    trainings: [
      '$stateParams',
      'PersonDocumentTrainings',
      function ($stateParams, PersonDocumentTrainings) {
        return PersonDocumentTrainings.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseTrainingsModalCtrl', ChooseTrainingsModalCtrl);
}(angular, _, $));
