/*global angular*/
(function (angular) {
  'use strict';

  function StatusesNewCtrl($scope, $stateParams, $state, PersonStatus) {
    $scope.cancel = function () {
      return $state.go('persons.statuses.search', { id: $stateParams.id });
    };

    $scope.save = function () {
      return PersonStatus.save({ id: $stateParams.id }, $scope.status).$promise
        .then(function () {
          $state.go('persons.statuses.search', { id: $stateParams.id });
        });
    };
  }

  StatusesNewCtrl.$inject = ['$scope', '$stateParams', '$state', 'PersonStatus'];

  angular.module('gva').controller('StatusesNewCtrl', StatusesNewCtrl);
}(angular));
