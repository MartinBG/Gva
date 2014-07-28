/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseLicencesModalCtrl(
    $scope,
    $modalInstance,
    licences,
    includedLicences
  ) {
    $scope.selectedLicences = [];

    $scope.licences = _.filter(licences, function (licence) {
      return !_.contains(includedLicences, licence.partIndex);
    });

    $scope.addLicences = function () {
      return $modalInstance.close($scope.selectedLicences);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectLicence = function (event, licence) {
      if ($(event.target).is(':checked')) {
        $scope.selectedLicences.push(licence);
      }
      else {
        $scope.selectedLicences = _.without($scope.selectedLicences, licence);
      }
    };
  }

  ChooseLicencesModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'licences',
    'includedLicences'
  ];

  angular.module('gva').controller('ChooseLicencesModalCtrl', ChooseLicencesModalCtrl);
}(angular, _, $));
