/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocSelectCtrl(
    $scope,
    $state,
    $stateParams,
    docs,
    selectedDoc
    ) {
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

    $scope.docs = docs.documents;

    $scope.search = function () {
      return $state.go($state.current, {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        regUri: $scope.filters.regUri,
        docName: $scope.filters.docName,
        docTypeId: $scope.filters.docTypeId,
        docStatusId: $scope.filters.docStatusId,
        corrs: $scope.filters.corrs,
        units: $scope.filters.units,
        stamp: new Date().getTime()
      });
    };

    $scope.selectDoc = function (result) {
      selectedDoc.push(result);
      return $state.go('^');
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.viewDoc = function (result) {
      return $state.go('root.docs.edit.view', { id: result.docId });
    };
  }

  DocSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'docs',
    'selectedDoc'
  ];

  DocSelectCtrl.$resolve = {
    docs: [
      '$stateParams',
      'Applications',
      function ($stateParams, Applications) {
        return Applications.notLinkedDocs($stateParams).$promise.then(function (docs) {
          return docs;
        });
      }
    ]
  };

  angular.module('gva').controller('DocSelectCtrl', DocSelectCtrl);
}(angular, _));
