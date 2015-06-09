/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportRatingsCtrl(
    $scope,
    $state,
    $stateParams,
    PersonsReports,
    docs
  ) {
    $scope.filters = {
      fromDate: null,
      toDate: null,
      ratingClassId: null,
      lin: null,
      limit: 10
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.docs = docs;
    $scope.ratingsCount = docs.ratingsCount;

    $scope.getRatings = function (page) {
      var params = {set: $stateParams.set};
      var pageSize = $scope.filters.limit || 10;
      _.assign(params, $scope.filters);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return PersonsReports.getRatings(params).$promise;
    };

    $scope.search = function () {
      return $state.go('root.personsReports.ratings', {
        fromDatePeriodFrom: $scope.filters.fromDatePeriodFrom,
        fromDatePeriodTo: $scope.filters.fromDatePeriodTo,
        toDatePeriodFrom: $scope.filters.toDatePeriodFrom,
        toDatePeriodTo: $scope.filters.toDatePeriodTo,
        ratingClassId: $scope.filters.ratingClassId,
        authorizationId: $scope.filters.authorizationId,
        aircraftTypeCategoryId: $scope.filters.aircraftTypeCategoryId,
        lin: $scope.filters.lin,
        limitationId: $scope.filters.limitationId,
        limit: $scope.filters.limit || 10
      });
    };
  }

  PersonReportRatingsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonsReports',
    'docs'
  ];

  PersonReportRatingsCtrl.$resolve = {
    docs: [
      '$stateParams',
      'PersonsReports',
      function ($stateParams, PersonsReports) {
        return PersonsReports.getRatings($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonReportRatingsCtrl', PersonReportRatingsCtrl);
}(angular, _));
