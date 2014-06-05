/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseCheckCtrl(
    $state,
    $stateParams,
    $scope,
    checks
  ) {
    if (!($state.payload && $state.payload.selectedChecks)) {
      $state.go('^');
      return;
    }

    $scope.selectedChecks = [];

    $scope.checks = _.filter(checks, function (check) {
      return !_.contains($state.payload.selectedChecks, check.partIndex);
    });

    $scope.addChecks = function () {
      return $state.go('^', {}, {}, {
        selectedChecks: _.pluck($scope.selectedChecks, 'partIndex')
      });
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

    $scope.selectCheck = function (event, check) {
      if ($(event.target).is(':checked')) {
        $scope.selectedChecks.push(check);
      }
      else {
        $scope.selectedChecks = _.without($scope.selectedChecks, check);
      }
    };
  }

  ChooseCheckCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'checks'
  ];

  ChooseCheckCtrl.$resolve = {
    checks: [
      '$stateParams',
      'PersonDocumentCheck',
      function ($stateParams, PersonDocumentCheck) {
        return PersonDocumentCheck.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseCheckCtrl', ChooseCheckCtrl);
}(angular, _, $));
