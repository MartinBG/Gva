/*global angular*/
(function (angular) {
  'use strict';

  function SignalsNewCtrl($scope, $state, Signals, signal) {
    $scope.signal = signal;

    $scope.save = function () {
      return $scope.newSignalForm.$validate()
      .then(function () {
        if ($scope.newSignalForm.$valid) {
          return Signals.save($scope.signal).$promise
            .then(function (data) {
              return $state.go('root.signals.edit', { id: data.id });
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.signals.search');
    };
  }

  SignalsNewCtrl.$inject = ['$scope', '$state', 'Signals', 'signal'];

  SignalsNewCtrl.$resolve = {
    signal: [
      'Signals',
      function (Signals) {
        return Signals.newSignal().$promise;
      }
    ]
  };

  angular.module('mosv').controller('SignalsNewCtrl', SignalsNewCtrl);
}(angular));
