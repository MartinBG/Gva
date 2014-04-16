/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocSelectCtrl($scope, $state, $stateParams, Application, selectedDoc) {
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

    Application.notLinkedDocs($stateParams).$promise.then(function (docs) {
      $scope.docs = docs.documents;
    });

    $scope.search = function () {
      return $state.go($state.current, {
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

    $scope.selectDoc = function (result) {
      selectedDoc.push({
        docId: result.docId,
        regUri: result.regUri,
        docTypeName: result.docTypeName,
        docStatusName: result.docStatusName,
        isElectronic: result.isElectronic
      });
      return $state.go('^');
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  DocSelectCtrl.$inject = [
    '$scope', '$state', '$stateParams', 'Application', 'selectedDoc'
  ];

  angular.module('gva').controller('DocSelectCtrl', DocSelectCtrl);
}(angular, _));
