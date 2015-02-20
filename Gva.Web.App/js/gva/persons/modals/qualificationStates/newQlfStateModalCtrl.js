/*global angular*/
(function (angular) {
  'use strict';

  function NewQlfStateModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    newState
  ) {
    $scope.form = {};
    $scope.newState = newState;

    $scope.save = function () {
      return $scope.form.newQlfStateForm.$validate()
        .then(function () {
          if ($scope.form.newQlfStateForm.$valid) {
            return $modalInstance.close({
              qualificationCode: $scope.newState.qualification.code,
              qualificationName: $scope.newState.qualification.name,
              fromDate: $scope.newState.fromDate,
              toDate: $scope.newState.toDate,
              stateMethod: $scope.newState.stateMethod,
              state: $scope.newState.state
            });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewQlfStateModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'newState'
  ];

  NewQlfStateModalCtrl.$resolve = {
    newState: [
      'PersonExamSystData',
      'scModalParams',
      function (PersonExamSystData, scModalParams) {
        return PersonExamSystData.newState({
          id: scModalParams.lotId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('NewQlfStateModalCtrl', NewQlfStateModalCtrl);
}(angular));
