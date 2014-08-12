/*global angular*/
(function (angular) {
  'use strict';

  function EditDisparityModalCtrl(
    $scope,
    $modalInstance,
    scModalParams
  ) {
      $scope.disparity = scModalParams.disparity;
      $scope.form = {};

      $scope.save = function () {
        return $scope.form.editDisparityForm.$validate()
        .then(function () {
          if ($scope.form.editDisparityForm.$valid) {
            return $modalInstance.close($scope.disparity);
          }
        });
      };

      $scope.cancel = function () {
        return $modalInstance.dismiss('cancel');
      };
  }

  EditDisparityModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams'
  ];

  angular.module('gva').controller('EditDisparityModalCtrl', EditDisparityModalCtrl);
}(angular));
