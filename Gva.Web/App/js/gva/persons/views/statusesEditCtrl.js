/*global angular*/
(function (angular) {
  'use strict';

  function StatusesEditCtrl($scope, $stateParams, $state, PersonStatus) {
    PersonStatus.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
      .then(function (status) {
        $scope.status = status;
      });

    $scope.cancel = function () {
      return $state.go('persons.statuses.search', { id: $stateParams.id });
    };

    $scope.save = function () {
      $scope.personStatusForm.$validate()
        .then(function () {
          if ($scope.personStatusForm.$valid) {
            return PersonStatus
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.status).$promise
              .then(function () {
                return $state.go('persons.statuses.search', { id: $stateParams.id });
              });
          }
        });
    };
  }

  StatusesEditCtrl.$inject = ['$scope', '$stateParams', '$state', 'PersonStatus'];

  angular.module('gva').controller('StatusesEditCtrl', StatusesEditCtrl);
}(angular));
