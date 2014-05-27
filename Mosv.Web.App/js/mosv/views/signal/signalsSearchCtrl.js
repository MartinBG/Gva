/*global angular, _*/
(function (angular, _) {
  'use strict';

  function SignalsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    signals
  ) {

    $scope.filters = {
      incomingNumber: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.signals = signals;

    $scope.search = function () {
      $state.go('root.signals.search', {
        incomingNumber: $scope.filters.incomingNumber,
        incomingLot: $scope.filters.incomingLot,
        applicant: $scope.filters.applicant,
        incomingDate: $scope.filters.incomingDate,
        violation: $scope.filters.violation
      });
    };

    $scope.newSignal = function () {
      return $state.go('root.signals.new');
    };

    $scope.viewSignal = function (signal) {
      return $state.go('root.signals.edit', { id: signal.id });
    };
  }

  SignalsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'signals'
  ];

  SignalsSearchCtrl.$resolve = {
    signals: [
      '$stateParams',
      'Signal',
      function ($stateParams, Signal) {
        return Signal.query($stateParams).$promise;
      }
    ]
  };

  angular.module('mosv').controller('SignalsSearchCtrl', SignalsSearchCtrl);
}(angular, _));