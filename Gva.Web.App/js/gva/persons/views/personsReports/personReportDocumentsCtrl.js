/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportDocumentsCtrl(
    $scope,
    $state,
    $stateParams,
    l10n,
    PersonsReports,
    docs
  ) {
    $scope.columnsOptions = [{
        id: 'lin', text: l10n.get('persons.reportDocuments.lin')
      }, {
        id: 'role', text: l10n.get('persons.reportDocuments.role')
      }, {
        id: 'type', text: l10n.get('persons.reportDocuments.type')
      }, {
        id: 'number', text: l10n.get('persons.reportDocuments.docNumber')
      }, {
        id: 'publisher', text: l10n.get('persons.reportDocuments.publisher')
      }, {
        id: 'limitations', text: l10n.get('persons.reportDocuments.limitations')
      }, {
        id: 'valid', text: l10n.get('persons.reportDocuments.valid')
      }, {
        id: 'fromDate', text: l10n.get('persons.reportDocuments.fromDate')
      }, {
        id: 'toDate', text: l10n.get('persons.reportDocuments.toDate')
      }, {
        id: 'medClass', text: l10n.get('persons.reportDocuments.medClass')
      }];

    $scope.filters = {
      roleId: null,
      fromDate: null,
      toDate: null,
      typeId: null,
      lin: null,
      limit: 10,
      sortBy: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });


    $scope.docs = docs;
    $scope.documentsCount = docs.documentsCount;

    $scope.getDocuments = function (page) {
      var params = {set: $stateParams.set};
      var pageSize = $scope.filters.limit || 10;
      _.assign(params, $scope.filters);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return PersonsReports.getDocuments(params).$promise;
    };

    $scope.search = function () {
      return $state.go('root.personsReports.documents', {
        roleId: $scope.filters.roleId,
        fromDatePeriodFrom: $scope.filters.fromDatePeriodFrom,
        fromDatePeriodTo: $scope.filters.fromDatePeriodTo,
        toDatePeriodFrom: $scope.filters.toDatePeriodFrom,
        toDatePeriodTo: $scope.filters.toDatePeriodTo,
        typeId: $scope.filters.typeId,
        lin: $scope.filters.lin,
        medClassId: $scope.filters.medClassId,
        limitationId: $scope.filters.limitationId,
        docNumber: $scope.filters.docNumber,
        publisher: $scope.filters.publisher,
        limit: $scope.filters.limit || 10,
        sortBy: $scope.filters.sortBy ? $scope.filters.sortBy.id : null
      });
    };
  }

  PersonReportDocumentsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'l10n',
    'PersonsReports',
    'docs'
  ];

  PersonReportDocumentsCtrl.$resolve = {
    docs: [
      '$stateParams',
      'PersonsReports',
      function ($stateParams, PersonsReports) {
        return PersonsReports.getDocuments($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonReportDocumentsCtrl', PersonReportDocumentsCtrl);
}(angular, _));
