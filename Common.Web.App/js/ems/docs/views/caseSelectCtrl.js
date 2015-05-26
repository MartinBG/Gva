/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CaseSelectCtrl(
    $scope,
    $state,
    $stateParams,
    Docs,
    docs,
    parentDoc) {
    $scope.docs = docs;
    $scope.docCount = docs.documentCount;

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

    $scope.getDocs = function (page, pageSize) {
      var params = {};

      _.assign(params, $stateParams);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return Docs.get(params).$promise;
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

  CaseSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Docs',
    'docs',
    'parentDoc'
  ];

  CaseSelectCtrl.$resolve = {
    docs: [
      '$stateParams',
      'Docs',
      function resolveDocs($stateParams, Docs) {
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

        return Docs.get(stateParams).$promise;
      }
    ]
  };

  angular.module('ems').controller('CaseSelectCtrl', CaseSelectCtrl);
}(angular, _));
