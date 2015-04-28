/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseTrainingsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    trainings
  ) {
    $scope.selectedTrainings = [];

    $scope.trainings = _.filter(trainings, function (training) {
      return !_.contains(scModalParams.includedTrainings, training.partIndex);
    });

    $scope.addTrainings = function () {
      return $modalInstance.close($scope.selectedTrainings);
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
    'scModalParams',
    'trainings'
  ];

  ChooseTrainingsModalCtrl.$resolve = {
    trainings: [
      'PersonDocumentTrainings',
      'scModalParams',
      function (PersonDocumentTrainings, scModalParams) {
        return PersonDocumentTrainings.query({
          id: scModalParams.lotId
        })
         .$promise
         .then(function (trainings){
            return _.filter(trainings, function (training) {
              return training['case'].caseType.nomValueId === scModalParams.caseTypeId ||
                training['case'].caseType.alias === 'person';
            });
          });
      }
    ]
  };

  angular.module('gva').controller('ChooseTrainingsModalCtrl', ChooseTrainingsModalCtrl);
}(angular, _, $));
