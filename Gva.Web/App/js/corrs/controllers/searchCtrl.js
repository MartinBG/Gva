/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CorrsSearchCtrl($scope, $state, $stateParams, Corr) {
    $scope.filters = {
      corrUin: null,
      corrEmail: null
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
        corrUin: $scope.filters.corrUin,
        corrEmail: $scope.filters.corrEmail
      });
    };

    Corr.query($stateParams).$promise.then(function (corrs) {
      $scope.corrs = corrs.map(function (corr) {
        //var roles = '';
        //for (var i = 0; i < user.roles.length; i++) {
        //  roles += user.roles[i].name + ', ';
        //}
        //roles = roles.substring(0, roles.length - 2);

        return {
          corrId: corr.corrId,
          displayName: corr.displayName,
          email: corr.email,
          corrType: corr.corrType
        };
      });
    });

    $scope.editCorr = function (corr) {
      $state.go('corrs.edit', { corrId: corr.corrId });
    };
  }
  
  CorrsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'Corr'];

  angular.module('corrs').controller('CorrsSearchCtrl', CorrsSearchCtrl);
}(angular, _));
