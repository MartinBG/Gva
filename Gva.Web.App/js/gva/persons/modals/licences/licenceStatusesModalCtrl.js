/*global angular*/
(function (angular) {
  'use strict';

  function LicenceStatusesModalCtrl(
    $scope,
    $modalInstance,
    PersonLicences,
    licence,
    personId,
    licenceInd,
    statusModel
  ) {
    $scope.form = {};
    $scope.statusModel = statusModel;
    $scope.licence = licence;

    $scope.save = function () {
      return $scope.form.licenceStatusesForm.$validate().then(function () {
        if ($scope.form.licenceStatusesForm.$valid) {
          if (!$scope.licence.part.statuses) {
            $scope.licence.part.statuses = [];
          }
          $scope.licence.part.statuses.push(statusModel);
          $scope.licence.part.valid = statusModel.valid;

          return PersonLicences
            .save({ id: personId, ind: licenceInd }, $scope.licence).$promise
            .then(function () {
              return $modalInstance.close();
            });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

  }

  LicenceStatusesModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonLicences',
    'licence',
    'personId',
    'licenceInd',
    'statusModel'
  ];

  LicenceStatusesModalCtrl.$resolve = {
    statusModel: function () {
      return {};
    }
  };

  angular.module('gva').controller('LicenceStatusesModalCtrl', LicenceStatusesModalCtrl);
}(angular));