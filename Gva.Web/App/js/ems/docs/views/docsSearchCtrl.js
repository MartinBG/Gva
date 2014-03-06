/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    Doc,
    docs
  ) {
    $scope.docs = docs;

    $scope.filters = {
      filter: null,
      fromDate: null,
      toDate: null,
      regUri: null,
      docName: null,
      docTypeId: null,
      docStatusId: null,
      corrs: null,
      units: null,
      docIds: null,
      hasLot: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.search = function () {
      return $state.go('root.docs.search', {
        filter: $scope.filters.filter,
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        regUri: $scope.filters.regUri,
        docName: $scope.filters.docName,
        docTypeId: $scope.filters.docTypeId,
        docStatusId: $scope.filters.docStatusId,
        corrs: $scope.filters.corrs,
        units: $scope.filters.units,
        docIds: $scope.filters.docIds,
        hasLot: $scope.filters.hasLot
      });
    };

    $scope.viewDoc = function (doc) {
      return $state.go('root.docs.edit.view', { docId: doc.docId });
    };

    $scope.newDoc = function () {
      return $state.go('root.docs.new');
    };
  }

  DocsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Doc',
    'docs'
  ];

  DocsSearchCtrl.$resolve = {
    docs: [
      '$stateParams',
      'Doc',
      function resolveDocs($stateParams, Doc) {
        return Doc.query($stateParams).$promise;
      }
    ]
  };

  angular.module('ems').controller('DocsSearchCtrl', DocsSearchCtrl);
}(angular, _));
