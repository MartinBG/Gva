/*global angular, _*/
(function (angular, _) {
  'use strict';

  function StatusesSearchCtrl(
    $scope,
    $stateParams,
    $state,
    statuses
  ) {
    $scope.statuses = statuses;

    $scope.editStatus = function (status) {
      return $state.go('root.persons.view.statuses.edit', {
        id: $stateParams.id,
        ind: status.partIndex
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
    'statuses'
  ];

  StatusesSearchCtrl.$resolve = {
    statuses: [
      '$stateParams',
      'PersonStatuses',
      function ($stateParams, PersonStatuses) {
        return PersonStatuses.query($stateParams).$promise
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
