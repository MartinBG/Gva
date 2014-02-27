/*global angular, _*/
(function (angular, _) {
  'use strict';

  function StatusesSearchCtrl(
    $scope,
    $stateParams,
    $state,
    PersonStatus,
    statuses
  ) {
    $scope.statuses = statuses;

    $scope.editStatus = function (status) {
      return $state.go('root.persons.view.statuses.edit', {
        id: $stateParams.id,
        ind: status.partIndex
      });
    };

    $scope.deleteStatus = function (status) {
      return PersonStatus
        .remove({ id: $stateParams.id, ind: status.partIndex }).$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newStatus = function () {
      return $state.go('root.persons.view.statuses.new');
    };
  }

  StatusesSearchCtrl.$inject = [
    '$scope',
    '$stateParams',
    '$state',
    'PersonStatus',
    'statuses'
  ];

  StatusesSearchCtrl.$resolve = {
    statuses: [
      '$stateParams',
      'PersonStatus',
      function ($stateParams, PersonStatus) {
        return PersonStatus.query($stateParams).$promise
        .then(function (statuses) {
          return _(statuses)
          .forEach(function (s) {
            s.part.isActive = new Date(s.part.documentDateValidTo) > new Date();
          })
          .value();
        });
      }
    ]
  };

  angular.module('gva').controller('StatusesSearchCtrl', StatusesSearchCtrl);
}(angular, _));
