/*global angular*/
(function (angular) {
  'use strict';

  function NewCorrModalCtrl(
    $modalInstance,
    $scope,
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
    'Corrs',
    'corr'
  ];

  NewCorrModalCtrl.$resolve = {
    corr: ['Integration','scModalParams',
      function (Integration, scModalParams) {
        return Integration.convertLotToCorrespondent({
          lotId: scModalParams.lotId
        })
          .$promise
          .then(function (corr) {
            return corr;
          });
      }
    ]
  };

  angular.module('ems').controller('NewCorrModalCtrl', NewCorrModalCtrl);
}(angular));
