/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsSearchCtrl($scope, $state, $stateParams, Doc) {
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
        $scope.filters[param] = value;
      }
    });

    Doc.query($stateParams).$promise.then(function (docs) {
      $scope.docs = docs;
    });

    $scope.search = function () {
      $state.go('docs/search', {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        regUri: $scope.filters.regUri,
        docName: $scope.filters.docName,
        docTypeId: $scope.filters.docTypeId,
        docStatusId: $scope.filters.docStatusId,
        corrs: $scope.filters.corrs,
        units: $scope.filters.units
      });
    };

    $scope.viewDoc = function (doc) {
      return $state.go('docs/edit/addressing', { docId: doc.docId });
    };

    $scope.newDoc = function () {
      return $state.go('docs/new');
    };
  }

  DocsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'Doc'];

  angular.module('ems').controller('DocsSearchCtrl', DocsSearchCtrl);
}(angular, _));
