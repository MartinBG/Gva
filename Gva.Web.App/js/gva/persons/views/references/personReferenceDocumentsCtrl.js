/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReferenceDocumentsCtrl(
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
      return $state.go('root.personsReferences.documents', {
        documentPart: $scope.filters.documentPart,
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        typeId: $scope.filters.typeId
      });
    };
  }

  PersonReferenceDocumentsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'documents'
  ];

  PersonReferenceDocumentsCtrl.$resolve = {
    documents: [
      '$stateParams',
      'PersonReferences',
      function ($stateParams, PersonReferences) {
        return PersonReferences.getDocuments($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonReferenceDocumentsCtrl', PersonReferenceDocumentsCtrl);
}(angular, _));
