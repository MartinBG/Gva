/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportDocumentsCtrl(
    $scope,
    $state,
    $stateParams,
    PersonsReports,
    documentRoleOptions,
    docs
  ) {

    $scope.documentRoleOptions = documentRoleOptions;
    $scope.filters = {
      documentRole: null,
      fromDate: null,
      toDate: null,
      typeId: null,
      lin: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.docs = docs;
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
        documentRole: $scope.filters.documentRole ?
          $scope.filters.documentRole.text : null,
        fromDatePeriodFrom: $scope.filters.fromDatePeriodFrom,
        fromDatePeriodTo: $scope.filters.fromDatePeriodTo,
        toDatePeriodFrom: $scope.filters.toDatePeriodFrom,
        toDatePeriodTo: $scope.filters.toDatePeriodTo,
        typeId: $scope.filters.typeId,
        lin: $scope.filters.lin,
        limitationId: $scope.filters.limitationId,
        docNumber: $scope.filters.docNumber,
        publisher: $scope.filters.publisher
      });
    };
  }

  PersonReportDocumentsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonsReports',
    'documentRoleOptions',
    'docs'
  ];

  PersonReportDocumentsCtrl.$resolve = {
    docs: [
      '$stateParams',
      'PersonsReports',
      function ($stateParams, PersonsReports) {
        return PersonsReports.getDocuments($stateParams).$promise;
      }
    ],
    documentRoleOptions: [
      '$q',
      'Nomenclatures',
      function ($q, Nomenclatures) {
        return $q.all({
          roles: Nomenclatures.query({alias: 'documentRoles'}).$promise,
          parts: Nomenclatures.query({alias: 'personSetParts'}).$promise
        })
        .then(function (result) {
          var neccessarySetParts = [
            'personDocumentId',
            'personEducation',
            'personDocumentId',
            'personMedical',
            'personApplication',
            'personLicence'
          ];

          var parts = _.filter(result.parts, function (part) {
            return _.contains(neccessarySetParts, part.alias);
          });

          return _.map(_.union(result.roles, parts), function (option) {
            return { id: option.name, text: option.name };
          });
        });
      }
    ]
  };

  angular.module('gva').controller('PersonReportDocumentsCtrl', PersonReportDocumentsCtrl);
}(angular, _));
