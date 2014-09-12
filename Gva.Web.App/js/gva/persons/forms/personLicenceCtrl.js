/*global angular*/
(function (angular) {
  'use strict';

  function PersonLicenceCtrl(
    $scope,
    l10n,
    PersonLicences,
    scFormParams
  ) {
    $scope.isNew = scFormParams.isNew;

    if ($scope.isNew) {
      $scope.$watch('model.licenceType', function () {
        if ($scope.model.licenceType) {
          PersonLicences.lastLicenceNumber({
            id: scFormParams.lotId,
            licenceTypeCode: $scope.model.licenceType.code
          }).$promise
            .then(function (lastLicenceNumber) {
              if (lastLicenceNumber.number === null) {
                $scope.lastLicenceNumber = l10n.get('statusTexts.noSuchLastLicenceNumber');
              }
              else {
                $scope.lastLicenceNumber = lastLicenceNumber.number;
              }
            });
        }
        else {
          $scope.lastLicenceNumber = null;
        }
      });
    }

    $scope.changedStaffType = function () {
      $scope.model.fcl = null;
    };

  }

  PersonLicenceCtrl.$inject = [
    '$scope',
    'l10n',
    'PersonLicences',
    'scFormParams'
  ];

  angular.module('gva').controller('PersonLicenceCtrl', PersonLicenceCtrl);
}(angular));
