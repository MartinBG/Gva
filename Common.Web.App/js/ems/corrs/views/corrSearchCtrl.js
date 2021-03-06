﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CorrsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    corrs
  ) {
    $scope.corrs = corrs.correspondents;
    $scope.corrCount = corrs.correspondentCount;

    $scope.filters = {
      displayName: null,
      correspondentEmail: null
    };

    _.forOwn($stateParams, function(value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.newCorr = function newCorr() {
      return $state.go('root.corrs.new');
    };

    $scope.search = function search() {
      return $state.go($state.current, {
        displayName: $scope.filters.displayName,
        correspondentEmail: $scope.filters.correspondentEmail
      }, { reload: true });
    };

    $scope.editCorr = function editCorr(corr) {
      return $state.go('root.corrs.edit', { id: corr.correspondentId });
    };
  }

  CorrsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'corrs'
  ];

  CorrsSearchCtrl.$resolve = {
    corrs: [
      '$stateParams',
      'Corrs',
      function resolveCorrs($stateParams, Corrs) {
        return Corrs.get($stateParams).$promise;
      }
    ]
  };

  angular.module('ems').controller('CorrsSearchCtrl', CorrsSearchCtrl);
}(angular, _));
