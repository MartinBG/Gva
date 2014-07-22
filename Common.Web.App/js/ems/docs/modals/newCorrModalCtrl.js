/*global angular*/
(function (angular) {
  'use strict';

  function NewCorrModalCtrl(
    $modalInstance,
    $scope,
    $state,
    Corrs,
    corr
  ) {
    $scope.form = {};
    $scope.corr = corr;

    $scope.save = function save() {
      return $scope.form.corrForm.$validate().then(function () {
        if ($scope.form.corrForm.$valid) {
          return Corrs.save($scope.corr).$promise.then(function (corr) {
            var nomItem = {
              name: corr.obj.displayName,
              nomValueId: corr.correspondentId
            };

            return $modalInstance.close(nomItem);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewCorrModalCtrl.$inject = [
    '$modalInstance',
    '$scope',
    '$state',
    'Corrs',
    'corr'
  ];

  angular.module('ems').controller('NewCorrModalCtrl', NewCorrModalCtrl);
}(angular));
