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
      lin: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.docs = docs.ratings;
    $scope.ratingsCount = docs.ratingsCount;

    $scope.getRatings = function (page, pageSize) {
      var params = {set: $stateParams.set};

      _.assign(params, $scope.filters);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return PersonsReports.getRatings(params).$promise;
    };

    $scope.search = function () {
      return $state.go('root.personsReports.ratings', {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        ratingClassId: $scope.filters.ratingClassId,
        authorizationId: $scope.filters.authorizationId,
        aircraftTypeCategoryId: $scope.filters.aircraftTypeCategoryId,
        lin: $scope.filters.lin,
        limitationId: $scope.filters.limitationId
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
