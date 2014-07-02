/*global angular,_*/
(function (angular) {
  'use strict';

  function SignalsEditCtrl(
    $scope,
    $state,
    $stateParams,
    SignalsData,
    signalData
  ) {
    var originalSignalData = _.cloneDeep(signalData);

    $scope.signalData = signalData;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.signalData = _.cloneDeep(originalSignalData);
    };

    $scope.save = function () {
      return $scope.editSignalForm.$validate()
        .then(function () {
          if ($scope.editSignalForm.$valid) {
            return SignalsData
              .save({ id: $stateParams.id }, $scope.signalData)
              .$promise
              .then(function () {
                return $state.transitionTo('root.signals.edit', $stateParams, { reload: true });
              });
          }
        });
    };
  }

  SignalsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'SignalsData',
    'signalData'
  ];

  SignalsEditCtrl.$resolve = {
    signalData: [
      '$stateParams',
      'SignalsData',
      function ($stateParams, SignalsData) {
        return SignalsData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('mosv').controller('SignalsEditCtrl', SignalsEditCtrl);
}(angular));
