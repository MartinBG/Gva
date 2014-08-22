/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    $sce,
    Docs,
    docs
  ) {
    $scope.docs = docs.documents;
    $scope.docCount = docs.documentCount;
    $scope.msg = $sce.trustAsHtml(docs.msg);

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
      ds: null,
      hasLot: null
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
        ds: undefined,
        hasLot: $scope.filters.hasLot
      }, { reload: true });
    };

    $scope.viewDoc = function (doc) {
      return $state.go('root.docs.edit.view', { id: doc.docId });
    };

    $scope.newDoc = function () {
      return $state.go('root.docs.new');
    };

    $scope.getDocs = function (page, pageSize) {
      var params = {};

      _.assign(params, $stateParams);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return Docs.get(params).$promise;
    };
  }

  DocsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    '$sce',
    'Docs',
    'docs'
  ];

  //? this resolve is useless as the scDatatable2 will make the call?
  DocsSearchCtrl.$resolve = {
    docs: [
      '$stateParams',
      'Docs',
      function resolveDocs($stateParams, Docs) {
        return Docs.get($stateParams).$promise;
      }
    ]
  };

  angular.module('ems').controller('DocsSearchCtrl', DocsSearchCtrl);
}(angular, _));
