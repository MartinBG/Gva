/*global angular, _, moment*/
(function (angular, _, moment) {
  'use strict';

  function StatusesSearchCtrl(
    $scope,
    $stateParams,
    $state,
    statuses
  ) {
    $scope.statuses = statuses;

    $scope.isExpiringDocument = function(item) {
      var today = moment(new Date()),
          difference = moment(item.documentDateValidTo).diff(today, 'days');

      return 0 <= difference && difference <= 30;
    };

    $scope.isExpiredDocument = function(item) {
      return moment(new Date()).isAfter(item.documentDateValidTo);
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
            s.isActive = new Date(s.documentDateValidTo) > new Date();
          })
          .value();
        });
      }
    ]
  };

  angular.module('gva').controller('StatusesSearchCtrl', StatusesSearchCtrl);
}(angular, _, moment));
