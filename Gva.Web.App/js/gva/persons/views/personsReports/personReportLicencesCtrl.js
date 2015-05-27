/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportLicencesCtrl(
    $scope,
    $state,
    $stateParams,
    PersonsReports,
    docs
  ) {
    var itemsPerPage = $stateParams.limit;
    $scope.filters = {
      fromDate: null,
      toDate: null,
      licenceTypeId: null,
      lin: null,
      itemsPerPage: itemsPerPage ? 
        {id: $stateParams.limit, text: $stateParams.limit} :
        {id: 10, text: '10'}
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.docs = docs;
    $scope.licencesCount = docs.licencesCount;

    $scope.getLicences = function (page) {
      var params = {set: $stateParams.set};
      var pageSize = $scope.filters.itemsPerPage.id;
      _.assign(params, $scope.filters);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return PersonsReports.getLicences(params).$promise;
    };

    $scope.search = function () {
      return $state.go('root.personsReports.licences', {
        fromDatePeriodFrom: $scope.filters.fromDatePeriodFrom,
        fromDatePeriodTo: $scope.filters.fromDatePeriodTo,
        toDatePeriodFrom: $scope.filters.toDatePeriodFrom,
        toDatePeriodTo: $scope.filters.toDatePeriodTo,
        licenceTypeId: $scope.filters.licenceTypeId,
        licenceActionId: $scope.filters.licenceActionId,
        lin: $scope.filters.lin,
        limitationId: $scope.filters.limitationId,
        limit: $scope.filters.itemsPerPage ?
          $scope.filters.itemsPerPage.id : 10
      });
    };
  }

  PersonReportLicencesCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonsReports',
    'docs'
  ];

  PersonReportLicencesCtrl.$resolve = {
    docs: [
      '$stateParams',
      'PersonsReports',
      function ($stateParams, PersonsReports) {
        return PersonsReports.getLicences($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonReportLicencesCtrl', PersonReportLicencesCtrl);
}(angular, _));
