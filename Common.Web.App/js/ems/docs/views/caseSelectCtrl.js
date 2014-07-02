/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CaseSelectCtrl($scope, $state, $stateParams, Docs, parentDoc) {
    $scope.filters = {
      csFromDate: null,
      csToDate: null,
      csRegUri: null,
      csDocName: null,
      csDocTypeId: null,
      csDocStatusId: null,
      csCorrs: null,
      csUnits: null,
      csIsCase: true
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    var stateParams = {
      fromDate: $stateParams.csFromDate,
      toDate: $stateParams.csToDate,
      regUri: $stateParams.csRegUri,
      docName: $stateParams.csDocName,
      docTypeId: $stateParams.csDocTypeId,
      docStatusId: $stateParams.csDocStatusId,
      corrs: $stateParams.csCorrs,
      units: $stateParams.csUnits,
      isCase: true
    };

    Docs.get(stateParams).$promise.then(function (docs) {
      $scope.docs = docs.documents;
      $scope.docCount = docs.documentCount;
    });

    $scope.search = function () {
      $state.go('root.docs.new.caseSelect', {
        csFromDate: $scope.filters.csFromDate,
        csToDate: $scope.filters.csToDate,
        csRegUri: $scope.filters.csRegUri,
        csDocName: $scope.filters.csDocName,
        csDocTypeId: $scope.filters.csDocTypeId,
        csDocStatusId: $scope.filters.csDocStatusId,
        csCorrs: $scope.filters.csCorrs,
        csUnits: $scope.filters.csUnits,
        csIsCase: true
      }, { reload: true });
    };

    $scope.selectDoc = function (result) {
      parentDoc.push({
        docId: result.docId,
        regUri: result.regUri,
        docTypeName: result.docTypeName,
        docSubject: result.docSubject
      });
      return $state.go('^');
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  CaseSelectCtrl.$inject = ['$scope', '$state', '$stateParams', 'Docs', 'parentDoc'];

  angular.module('ems').controller('CaseSelectCtrl', CaseSelectCtrl);
}(angular, _));
