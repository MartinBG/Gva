/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsDocSelectCtrl($scope, $state, $stateParams, Doc, selectedDoc) {
    $scope.filters = {
      fromDate: null,
      toDate: null,
      regUri: null,
      docName: null,
      docTypeId: null,
      docStatusId: null,
      corrs: null,
      units: null,
      docIds: null,
      hasLot: false
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
      $state.go('root.applications.link.docSelect', {
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

    $scope.selectDoc = function (result) {
      selectedDoc.push({
        docId: result.docId,
        regUri: result.regUri,
        docTypeName: result.docTypeName,
        docStatusName: result.docStatusName
      });
      return $state.go('^');
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  ApplicationsDocSelectCtrl.$inject = ['$scope', '$state', '$stateParams', 'Doc', 'selectedDoc'];

  angular.module('gva').controller('ApplicationsDocSelectCtrl', ApplicationsDocSelectCtrl);
}(angular, _));
