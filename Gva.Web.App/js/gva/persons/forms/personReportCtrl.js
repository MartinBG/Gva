/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonReportCtrl($scope, Persons, scModal, scMessage, scFormParams) {
    $scope.includedChecks = [];
    if (!scFormParams.isNew && $scope.model.part.includedChecks.length) {
      Persons
        .getChecksForReport({
          checks: $scope.model.part.includedChecks
        })
        .$promise
        .then(function (checks) {
          $scope.includedChecks = checks;
        });
    }

    $scope.addCheck = function () {
      var modalInstance = scModal.open('chooseChecksForReport', {
        includedChecks: $scope.model.part.includedChecks,
        publisherNames: scFormParams.names
      });

      $scope.model.part.includedChecks = $scope.model.part.includedChecks || [];
      modalInstance.result.then(function (selectedChecks) {
        $scope.includedChecks = $scope.includedChecks.concat(selectedChecks);
        $scope.model.part.includedChecks = _.pluck(selectedChecks, 'partId');
      });

      return modalInstance.opened;
    };

    $scope.removeCheck = function (check) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedChecks = _.without($scope.includedChecks, check);

            _.remove($scope.model.part.includedChecks,
              function(includedCheck) {
                return check.partId === includedCheck;
              });
          }
        });
    };
  }

  PersonReportCtrl.$inject = [
    '$scope',
    'Persons',
    'scModal',
    'scMessage',
    'scFormParams'
  ];


  angular.module('gva').controller('PersonReportCtrl', PersonReportCtrl);
}(angular, _));
