/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseTrainingCtrl(
    $state,
    $stateParams,
    $scope,
    trainings
  ) {
    if (!($state.payload && $state.payload.selectedTrainings)) {
      $state.go('^');
      return;
    }

    $scope.selectedTrainings = [];

    $scope.trainings = _.filter(trainings, function (training) {
      return !_.contains($state.payload.selectedTrainings, training.partIndex);
    });

    $scope.addTrainings = function () {
      return $state.go('^', {}, {}, {
        selectedTrainings: _.pluck($scope.selectedTrainings, 'partIndex')
      });
    };

    $scope.goBack = function () {
      return $state.go('^');
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

  ChooseTrainingCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'trainings'
  ];

  ChooseTrainingCtrl.$resolve = {
    trainings: [
      '$stateParams',
      'PersonDocumentTrainings',
      function ($stateParams, PersonDocumentTrainings) {
        return PersonDocumentTrainings.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseTrainingCtrl', ChooseTrainingCtrl);
}(angular, _, $));
