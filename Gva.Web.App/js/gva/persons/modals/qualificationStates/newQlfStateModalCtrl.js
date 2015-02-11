/*global angular*/
(function (angular) {
  'use strict';

  function NewQlfStateModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    PersonsExamSystData,
    newState
  ) {
    $scope.form = {};
    $scope.newState = newState;
    $scope.lotId = scModalParams.lotId;

    $scope.save = function () {
      return $scope.form.newQlfStateForm.$validate()
        .then(function () {
          if ($scope.form.newQlfStateForm.$valid) {
            return PersonsExamSystData
              .saveState({ id: $scope.lotId }, $scope.newState)
              .$promise
              .then(function () {
                return $modalInstance.close();
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
    'PersonsExamSystData',
    'newState'
  ];

  NewQlfStateModalCtrl.$resolve = {
    newState: [
      'PersonsExamSystData',
      'scModalParams',
      function (PersonsExamSystData, scModalParams) {
        return PersonsExamSystData.newState({
          id: scModalParams.lotId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('NewQlfStateModalCtrl', NewQlfStateModalCtrl);
}(angular));
