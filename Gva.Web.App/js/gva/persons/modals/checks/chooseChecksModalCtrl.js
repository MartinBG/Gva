/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseChecksModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    checks
  ) {
    $scope.selectedChecks = [];

    $scope.checks = _.filter(checks, function (check) {
      return !_.contains(scModalParams.includedChecks, check.partIndex);
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

  ChooseChecksModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'checks'
  ];

  ChooseChecksModalCtrl.$resolve = {
    checks: [
      'PersonDocumentChecks',
      'scModalParams',
      function (PersonDocumentChecks, scModalParams) {
        return PersonDocumentChecks.query({
          id: scModalParams.lotId,
          caseTypeId: scModalParams.caseTypeId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseChecksModalCtrl', ChooseChecksModalCtrl);
}(angular, _, $));
