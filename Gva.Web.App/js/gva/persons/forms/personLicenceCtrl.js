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
      $scope.$watch('model.part.licenceTypeId', function () {
        if ($scope.model.part.licenceTypeId) {
          PersonLicences.lastLicenceNumber({
            id: $scope.lotId,
            licenceTypeId: $scope.model.part.licenceTypeId
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

     $scope.isUniqueLicenceNumber = function () {
      if($scope.model.part.licenceNumber && 
        $scope.model.part.licenceTypeId) {
          return PersonLicences
            .isUniqueLicenceNumber({
              id: $scope.lotId,
              licenceNumber: $scope.model.part.licenceNumber,
              licenceTypeId: $scope.model.part.licenceTypeId
            })
          .$promise
          .then(function (result) {
            return result.isUnique;
          });
      } else {
        return true;
      }
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
