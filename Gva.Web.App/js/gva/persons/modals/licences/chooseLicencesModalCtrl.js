/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseLicencesModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    licences
  ) {
    $scope.selectedLicences = [];

    $scope.licences = _.filter(licences, function (licence) {
      return !_.contains(scModalParams.includedLicences, licence.partIndex);
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
    'scModalParams',
    'licences'
  ];

  ChooseLicencesModalCtrl.$resolve = {
    licences: [
      'PersonLicences',
      'scModalParams',
      function (PersonLicences, scModalParams) {
        return PersonLicences.query({ id: scModalParams.lotId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseLicencesModalCtrl', ChooseLicencesModalCtrl);
}(angular, _, $));
