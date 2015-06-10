/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportLicencesCtrl(
    $scope,
    $state,
    $stateParams,
    l10n,
    PersonsReports,
    docs
  ) {
    $scope.columnsOptions = [{
        id: 'lin', text: l10n.get('persons.reportLicences.lin')
      }, {
        id: 'uin', text: l10n.get('persons.reportLicences.uin')
      }, {
        id: 'licenceTypeName', text: l10n.get('persons.reportLicences.licenceTypeName')
      }, {
        id: 'licenceCode', text: l10n.get('persons.reportLicences.licenceCode')
      }, {
        id: 'names', text: l10n.get('persons.reportLicences.names')
      }, {
        id: 'fromDate', text: l10n.get('persons.reportLicences.fromDate')
      }, {
        id: 'toDate', text: l10n.get('persons.reportLicences.toDate')
      }, {
        id: 'firstIssueDate', text: l10n.get('persons.reportLicences.firstIssueDate')
      }, {
        id: 'licenceAction', text: l10n.get('persons.reportLicences.licenceAction')
      }, {
        id: 'limitations', text: l10n.get('persons.reportLicences.limitations')
      }, {
        id: 'stampNumber', text: l10n.get('persons.reportLicences.stampNumber')
      }];

    $scope.filters = {
      fromDate: null,
      toDate: null,
      licenceTypeId: null,
      lin: null,
      limit: 10
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
      var pageSize = $scope.filters.limit || 10;
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
        limit: $scope.filters.limit || 10,
        sortBy: $scope.filters.sortBy ? $scope.filters.sortBy.id : null
      });
    };
  }

  PersonReportLicencesCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'l10n',
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
