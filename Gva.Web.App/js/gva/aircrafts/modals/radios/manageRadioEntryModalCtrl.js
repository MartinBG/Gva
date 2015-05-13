/*global angular*/
(function (angular) {
  'use strict';

  function ManageRadioEntryModalCtrl(
    $scope,
    $modalInstance,
    scModalParams
  ) {
    $scope.form = {};
    $scope.model = scModalParams.entry || {};

    $scope.add = function () {
      return $scope.form.manageRadioEntryForm.$validate()
        .then(function () {
          if ($scope.form.manageRadioEntryForm.$valid) {
            return $modalInstance.close($scope.model);
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ManageRadioEntryModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams'
  ];

  angular.module('gva').controller('ManageRadioEntryModalCtrl', ManageRadioEntryModalCtrl);
}(angular));
