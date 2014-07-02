/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChangeDocParentCtrl(
    $scope,
    $state,
    $stateParams,
    $sce,
    Docs,
    docs,
    doc
  ) {
    $scope.docs = docs.documents;
    $scope.docCount = docs.documentCount;
    $scope.msg = $sce.trustAsHtml(docs.msg);

    $scope.filters = {
      fromDate: null,
      toDate: null,
      regUri: null,
      docName: null,
      docTypeId: null,
      docStatusId: null,
      corrs: null,
      units: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        if (param === 'corrs' || param === 'units') {
          $scope.filters[param] = value.split(',');
        } else {
          $scope.filters[param] = value;
        }
      }
    });

    $scope.search = function () {
      return $state.go('root.docs.edit.case.changeDocParent', {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        regUri: $scope.filters.regUri,
        docName: $scope.filters.docName,
        docTypeId: $scope.filters.docTypeId,
        docStatusId: $scope.filters.docStatusId,
        corrs: $scope.filters.corrs,
        units: $scope.filters.units
      }, { reload: true });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.select = function (newDocId) {
      return Docs.changeDocParent({
        id: doc.docId,
        newDocId: newDocId
      }, {}).$promise.then(function () {
        return $state.go('^', { id: doc.docId }, { reload: true });
      });
    };
  }

  ChangeDocParentCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$sce',
    'Docs',
    'docs',
    'doc'
  ];

  ChangeDocParentCtrl.$resolve = {
    docs: [
      '$stateParams',
      'Docs',
      function resolveDocs($stateParams, Docs) {
        return Docs.getDocsForChange($stateParams).$promise;
      }
    ]
  };

  angular.module('ems').controller('ChangeDocParentCtrl', ChangeDocParentCtrl);
}(angular, _));
