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
    $scope.lotId = scFormParams.lotId;
    $scope.caseTypeId = scFormParams.caseTypeId;

    if ($scope.isNew) {
      $scope.$watch('model.part.licenceType', function () {
        if ($scope.model.part.licenceType) {
          PersonLicences.lastLicenceNumber({
            id: scFormParams.lotId,
            licenceTypeCode: $scope.model.part.licenceType.code
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
  }

  PersonLicenceCtrl.$inject = [
    '$scope',
    'l10n',
    'PersonLicences',
    'scFormParams'
  ];

  angular.module('gva').controller('PersonLicenceCtrl', PersonLicenceCtrl);
}(angular));
