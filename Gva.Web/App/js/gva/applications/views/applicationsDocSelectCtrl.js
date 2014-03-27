/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsDocSelectCtrl($scope, $state, $stateParams, Application, selectedDoc) {
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

    Application.notLinkedDocs($stateParams).$promise.then(function (docs) {
      $scope.docs = docs.documents;
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
        units: $scope.filters.units
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

  ApplicationsDocSelectCtrl.$inject = [
    '$scope', '$state', '$stateParams', 'Application', 'selectedDoc'
  ];

  angular.module('gva').controller('ApplicationsDocSelectCtrl', ApplicationsDocSelectCtrl);
}(angular, _));
