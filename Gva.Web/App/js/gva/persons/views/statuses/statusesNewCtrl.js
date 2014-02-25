/*global angular*/
(function (angular) {
  'use strict';

  function StatusesNewCtrl(
    $scope,
    $stateParams,
    $state,
    PersonStatus,
    status
  ) {
    $scope.status = status;

    $scope.cancel = function () {
      return $state.go('root.persons.view.statuses.search', { id: $stateParams.id });
    };

    $scope.save = function () {
      $scope.personStatusForm.$validate()
        .then(function () {
          if ($scope.personStatusForm.$valid) {
            return PersonStatus.save({ id: $stateParams.id }, $scope.status)
              .$promise
              .then(function () {
                $state.go('root.persons.view.statuses.search', { id: $stateParams.id });
              });
          }
        });
    };
  }

  StatusesNewCtrl.$inject = [
    '$scope',
    '$stateParams',
    '$state',
    'PersonStatus',
    'status'
  ];

  StatusesNewCtrl.$resolve = {
    status: function () {
      return {};
    }
  };

  angular.module('gva').controller('StatusesNewCtrl', StatusesNewCtrl);
}(angular));
