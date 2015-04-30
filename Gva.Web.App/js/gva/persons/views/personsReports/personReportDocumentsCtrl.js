/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportDocumentsCtrl(
    $scope,
    $state,
    $stateParams,
    documents
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

    $scope.documents = documents;

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
    'documents'
  ];

  PersonReportDocumentsCtrl.$resolve = {
    documents: [
      '$stateParams',
      'PersonsReports',
      function ($stateParams, PersonsReports) {
        return PersonsReports.getDocuments($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonReportDocumentsCtrl', PersonReportDocumentsCtrl);
}(angular, _));
