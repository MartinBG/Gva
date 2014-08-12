/*global angular*/
(function (angular) {
  'use strict';

  function StatusesNewCtrl(
    $scope,
    $stateParams,
    $state,
    PersonStatuses,
    status
  ) {
    $scope.status = status;

    $scope.cancel = function () {
      return $state.go('root.persons.view.statuses.search', { id: $stateParams.id });
    };

    $scope.save = function () {
      return $scope.personStatusForm.$validate()
        .then(function () {
          if ($scope.personStatusForm.$valid) {
            return PersonStatuses
              .save({ id: $stateParams.id }, $scope.status)
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
    'PersonStatuses',
    'status'
  ];

  StatusesNewCtrl.$resolve = {
    status: [
      '$stateParams',
      'PersonStatuses',
      function ($stateParams, PersonStatuses) {
        return PersonStatuses.newStatus({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('StatusesNewCtrl', StatusesNewCtrl);
}(angular));
