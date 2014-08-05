/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChooseCorrModalCtrl(
    $modalInstance,
    $scope,
    Corrs,
    scModalParams,
    corrs
  ) {
    $scope.corrs = corrs.correspondents;

    $scope.filters = {
      displayName: scModalParams.corr.displayName,
      email: scModalParams.corr.email
    };

    $scope.selectedCorrs = _.map(scModalParams.selectedCorrs, function (corr) {
      return corr.nomValueId;
    });

    $scope.search = function () {
      return Corrs.get({
        displayName: $scope.filters.displayName,
        correspondentEmail: $scope.filters.email
      }).$promise.then(function (corrs) {
        $scope.corrs = corrs.correspondents;
      });
    };

    $scope.selectCorr = function (corr) {
      var nomItem = {
        nomValueId: corr.correspondentId,
        name: corr.displayName
      };

      return $modalInstance.close(nomItem);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ChooseCorrModalCtrl.$inject = [
    '$modalInstance',
    '$scope',
    'Corrs',
    'scModalParams',
    'corrs'
  ];

  ChooseCorrModalCtrl.$resolve = {
    corrs: [
      'Corrs',
      'scModalParams',
      function (Corrs, scModalParams) {
        return Corrs.get({
          displayName: scModalParams.corr.displayName,
          email: scModalParams.corr.email
        }).$promise;
      }
    ]
  };

  angular.module('ems').controller('ChooseCorrModalCtrl', ChooseCorrModalCtrl);
}(angular, _));
