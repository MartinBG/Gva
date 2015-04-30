/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportRatingsCtrl(
    $scope,
    $state,
    $stateParams,
    ratings
  ) {
    $scope.filters = {
      fromDate: null,
      toDate: null,
      ratingClassId: null,
      lin: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.ratings = ratings;

    $scope.search = function () {
      return $state.go('root.personsReports.ratings', {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        ratingClassId: $scope.filters.ratingClassId,
        authorizationId: $scope.filters.authorizationId,
        aircraftTypeCategoryId: $scope.filters.aircraftTypeCategoryId,
        lin: $scope.filters.lin
      });
    };
  }

  PersonReportRatingsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'ratings'
  ];

  PersonReportRatingsCtrl.$resolve = {
    ratings: [
      '$stateParams',
      'PersonsReports',
      function ($stateParams, PersonsReports) {
        return PersonsReports.getRatings($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonReportRatingsCtrl', PersonReportRatingsCtrl);
}(angular, _));
