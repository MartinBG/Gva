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
