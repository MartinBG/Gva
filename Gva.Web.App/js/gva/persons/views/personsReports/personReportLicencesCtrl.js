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
    $scope.filters = {
      fromDate: null,
      toDate: null,
      licenceTypeId: null,
      lin: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.docs = docs.licences;
    $scope.documentsCount = docs.licencesCount;

    $scope.getLicences = function (page, pageSize) {
      var params = {set: $stateParams.set};

      _.assign(params, $scope.filters);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return PersonsReports.getLicences(params).$promise;
    };

    $scope.search = function () {
      return $state.go('root.personsReports.licences', {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        licenceTypeId: $scope.filters.licenceTypeId,
        licenceActionId: $scope.filters.licenceActionId,
        lin: $scope.filters.lin
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
