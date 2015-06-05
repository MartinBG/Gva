/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportDocumentsCtrl(
    $scope,
    $state,
    $stateParams,
    PersonsReports,
    docs
  ) {
    var itemsPerPage = $stateParams.limit;

    $scope.filters = {
      roleId: null,
      fromDate: null,
      toDate: null,
      typeId: null,
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
    $scope.documentsCount = docs.documentsCount;

    $scope.getDocuments = function (page) {
      var params = {set: $stateParams.set};
      var pageSize = $scope.filters.itemsPerPage.id;

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
        limit: $scope.filters.itemsPerPage ?
          $scope.filters.itemsPerPage.id : 10
      });
    };
  }

  PersonReportDocumentsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
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
