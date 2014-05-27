/*global angular,_*/
(function (angular) {
  'use strict';

  function SignalsEditCtrl(
    $scope,
    $state,
    $stateParams,
    SignalData,
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
            return SignalData
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
    'SignalData',
    'signalData'
  ];

  SignalsEditCtrl.$resolve = {
    signalData: [
      '$stateParams',
      'SignalData',
      function ($stateParams, SignalData) {
        return SignalData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('mosv').controller('SignalsEditCtrl', SignalsEditCtrl);
}(angular));
