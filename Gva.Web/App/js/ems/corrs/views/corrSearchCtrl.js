/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CorrsSearchCtrl($scope, $state, $stateParams, Corr) {
    $scope.filters = {
      displayName: null,
      email: null
    };

    _.forOwn($stateParams, function(value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.newCorr = function () {
      $state.go('corrs.new');
    };

    $scope.search = function () {
      $state.go('corrs.search', {
        displayName: $scope.filters.displayName,
        email: $scope.filters.email
      });
    };

    Corr.query($stateParams).$promise.then(function (corrs) {
      $scope.corrs = corrs.map(function (corr) {
        return {
          corrId: corr.corrId,
          displayName: corr.displayName,
          email: corr.email,
          correspondentType: corr.correspondentType
        };
      });
    });

    $scope.editCorr = function (corr) {
      $state.go('corrs.edit', { corrId: corr.corrId });
    };
  }
  
  CorrsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'Corr'];

  angular.module('ems').controller('CorrsSearchCtrl', CorrsSearchCtrl);
}(angular, _));
