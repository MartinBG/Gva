/*global angular*/
(function (angular) {
  'use strict';

  function SignalsNewCtrl($scope, $state, Signal, signal) {
    $scope.signal = signal;

    $scope.save = function () {
      return $scope.newSignalForm.$validate()
      .then(function () {
        if ($scope.newSignalForm.$valid) {
          return Signal.save($scope.signal).$promise
            .then(function () {
              return $state.go('root.signals.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.signals.search');
    };
  }

  SignalsNewCtrl.$inject = ['$scope', '$state', 'Signal', 'signal'];

  SignalsNewCtrl.$resolve = {
    signal: function () {
      return {
        signalData: {}
      };
    }
  };

  angular.module('mosv').controller('SignalsNewCtrl', SignalsNewCtrl);
}(angular));