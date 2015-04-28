/*global angular*/
(function (angular) {
  'use strict';

  function CheckOfForeignerModalCtrl(
    $scope,
    $modalInstance,
    scModalParams
  ) {
    $scope.form = {};
    $scope.model = scModalParams.check || {};

    $scope.add = function () {
      return $scope.form.newDocumentCheckForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentCheckForm.$valid) {
            return $modalInstance.close($scope.model);
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  CheckOfForeignerModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams'
  ];

  angular.module('gva').controller('CheckOfForeignerModalCtrl', CheckOfForeignerModalCtrl);
}(angular));
