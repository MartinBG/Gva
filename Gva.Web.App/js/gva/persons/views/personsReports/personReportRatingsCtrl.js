/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportRatingsCtrl(
    $scope,
    $state,
    $stateParams,
    l10n,
    PersonsReports,
    docs
  ) {
    $scope.columnsOptions = [{
        id: 'lin', text: l10n.get('persons.reportRatings.lin')
      }, {
        id: 'fromDate', text: l10n.get('persons.reportRatings.fromDate')
      }, {
        id: 'toDate', text: l10n.get('persons.reportRatings.toDate')
      }, {
        id: 'firstIssueDate', text: l10n.get('persons.reportRatings.firstIssueDate')
      }, {
        id: 'personRatingLevel', text: l10n.get('persons.reportRatings.personRatingLevel')
      }, {
        id: 'ratingTypes', text: l10n.get('persons.reportRatings.ratingTypes')
      }, {
        id: 'locationIndicator', text: l10n.get('persons.reportRatings.locationIndicator')
      }, {
        id: 'sector', text: l10n.get('persons.reportRatings.sector')
      }, {
        id: 'authorizationCode', text: l10n.get('persons.reportRatings.authorizationCode')
      }, {
        id: 'limitations', text: l10n.get('persons.reportRatings.limitations')
      }];
   
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
        limit: $scope.filters.limit || 10,
        sortBy: $scope.filters.sortBy ? $scope.filters.sortBy.id : null
      });
    };
  }

  PersonReportRatingsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'l10n',
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
