/*global angular*/
(function (angular) {
  'use strict';

  function CorrsEditCtrl(
    $q,
    $scope,
    $filter,
    $state,
    $stateParams,
    Corr
  ) {
    if ($stateParams.corrId) {
      $scope.isEdit = true;
      $scope.corr = Corr.get({ corrId: $stateParams.corrId });
    } else {
      $scope.isEdit = false;
      $scope.corr = new Corr();
      $scope.corr.$promise = $q.when($scope.corr);
    }
    $scope.saveClicked = false;

    $scope.save = function () {
      $scope.saveClicked = true;

      if ($scope.corrForm.$valid) {
        $scope.corr.$save().then(function () {
          $state.go('corrs.search');
        });
      }
    };

    $scope.cancel = function () {
      $state.go('corrs.search');
    };
  }

  CorrsEditCtrl.$inject = [
    '$q',
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Corr'
  ];

  angular.module('corrs').controller('CorrsEditCtrl', CorrsEditCtrl);
}(angular));
