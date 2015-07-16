/*global angular*/
(function (angular) {
  'use strict';

  function LicenceStatusesModalCtrl(
    $scope,
    $modalInstance,
    PersonLicences,
    scModalParams,
    statusModel
  ) {
    $scope.form = {};
    $scope.statusModel = statusModel;
    $scope.licence = scModalParams.licence;

    $scope.save = function () {
      return $scope.form.licenceStatusesForm.$validate().then(function () {
        if ($scope.form.licenceStatusesForm.$valid) {
          if (!$scope.licence.statuses) {
            $scope.licence.statuses = [];
          }
          $scope.licence.statuses.push(statusModel);
          $scope.licence.valid = statusModel.valid;

          return PersonLicences.updateLicenceStatus({
            id: scModalParams.personId,
            ind: scModalParams.licenceInd
          },
          statusModel).$promise.then(function () {
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
    'scModalParams',
    'statusModel'
  ];

  LicenceStatusesModalCtrl.$resolve = {
    statusModel: [
      'scModalParams',
      'PersonLicences',
      function (scModalParams, PersonLicences) {
        return PersonLicences.newLicenceStatus({
          id: scModalParams.personId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicenceStatusesModalCtrl', LicenceStatusesModalCtrl);
}(angular));