/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportLicencesCtrl(
    $scope,
    $state,
    $stateParams,
    licences
  ) {
    $scope.filters = {
      fromDate: null,
      toDate: null,
      licenceTypeId: null,
      licenceActionId: null,
      lin: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.licences = licences;

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
    'licences'
  ];

  PersonReportLicencesCtrl.$resolve = {
    licences: [
      '$stateParams',
      'PersonsReports',
      function ($stateParams, PersonsReports) {
        return PersonsReports.getLicences($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonReportLicencesCtrl', PersonReportLicencesCtrl);
}(angular, _));
