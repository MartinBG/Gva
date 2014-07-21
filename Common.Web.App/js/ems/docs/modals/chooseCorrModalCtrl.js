/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChooseCorrModalCtrl(
    $modalInstance,
    $scope,
    Corrs,
    corrs,
    selectedCorrs
  ) {
    $scope.corrs = corrs.correspondents;

    $scope.filters = {
      displayName: null,
      email: null
    };
    
    $scope.selectedCorrs = _.map(selectedCorrs, function (corr) {
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
    'corrs',
    'selectedCorrs'
  ];

  angular.module('ems').controller('ChooseCorrModalCtrl', ChooseCorrModalCtrl);
}(angular, _));
