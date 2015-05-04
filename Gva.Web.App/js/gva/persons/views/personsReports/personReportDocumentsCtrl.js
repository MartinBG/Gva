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
    $scope.filters = {
      documentPart: null,
      fromDate: null,
      toDate: null,
      typeId: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.docs = docs.documents;
    $scope.documentsCount = docs.documentsCount;

    $scope.getDocuments = function (page, pageSize) {
      var params = {set: $stateParams.set};

      _.assign(params, $scope.filters);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return PersonsReports.getDocuments(params).$promise;
    };

    $scope.search = function () {
      return $state.go('root.personsReports.documents', {
        documentPart: $scope.filters.documentPart,
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        typeId: $scope.filters.typeId
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
