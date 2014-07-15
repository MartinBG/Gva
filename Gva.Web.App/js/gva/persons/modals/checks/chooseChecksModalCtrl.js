/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseChecksModalCtrl(
    $scope,
    $modalInstance,
    checks,
    includedChecks
  ) {
    $scope.selectedChecks = [];

    $scope.checks = _.filter(checks, function (check) {
      return !_.contains(includedChecks, check.partIndex);
    });

    $scope.addChecks = function () {
      return $modalInstance.close($scope.selectedChecks);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectCheck = function (event, checkId) {
      if ($(event.target).is(':checked')) {
        $scope.selectedChecks.push(checkId);
      }
      else {
        $scope.selectedChecks = _.without($scope.selectedChecks, checkId);
      }
    };
  }

  ChooseChecksModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'checks',
    'includedChecks'
  ];

  ChooseChecksModalCtrl.$resolve = {
    checks: [
      '$stateParams',
      'PersonDocumentChecks',
      function ($stateParams, PersonDocumentChecks) {
        return PersonDocumentChecks.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseChecksModalCtrl', ChooseChecksModalCtrl);
}(angular, _, $));
