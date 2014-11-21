/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseChecksForReportModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    checks
  ) {
    $scope.selectedChecks = [];

    $scope.checks = _.filter(checks, function (check) {
      var checks = _.where(scModalParams.includedChecks, function (includedCheck) {
        return includedCheck === check.partId;
      });
      return checks.length === 0;
    });

    $scope.addChecks = function () {
      return $modalInstance.close($scope.selectedChecks);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
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

  ChooseChecksForReportModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'checks'
  ];

  ChooseChecksForReportModalCtrl.$resolve = {
    checks: [
      'Persons',
      'scModalParams',
      function (Persons, scModalParams) {
        return Persons
          .getChecksForReport({ publisherNames : scModalParams.publisherNames })
          .$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('ChooseChecksForReportModalCtrl', ChooseChecksForReportModalCtrl);
}(angular, _, $));
