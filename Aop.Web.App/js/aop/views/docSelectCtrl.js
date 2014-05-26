/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AppDocSelectCtrl(
    $scope,
    $state,
    $stateParams,
    Doc,
    selectDoc) {
    $scope.filters = {
      csFromDate: null,
      csToDate: null,
      csRegUri: null,
      csDocName: null,
      csDocTypeId: null,
      csDocStatusId: null,
      csCorrs: null,
      csUnits: null
      //csIsCase: false
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    var stateParams = {
      filter: 'all',
      fromDate: $stateParams.csFromDate,
      toDate: $stateParams.csToDate,
      regUri: $stateParams.csRegUri,
      docName: $stateParams.csDocName,
      docTypeId: $stateParams.csDocTypeId,
      docStatusId: $stateParams.csDocStatusId,
      corrs: $stateParams.csCorrs,
      units: $stateParams.csUnits
      //isCase: false
    };

    Doc.get(stateParams).$promise.then(function (docs) {
      $scope.docs = docs.documents;
      $scope.docCount = docs.documentCount;
    });

    //aop filtrite dali shte rabotqt?
    $scope.search = function () {
      $state.go('root.apps.edit.docSelect', {
        filter: 'all',
        csFromDate: $scope.filters.csFromDate,
        csToDate: $scope.filters.csToDate,
        csRegUri: $scope.filters.csRegUri,
        csDocName: $scope.filters.csDocName,
        csDocTypeId: $scope.filters.csDocTypeId,
        csDocStatusId: $scope.filters.csDocStatusId,
        csCorrs: $scope.filters.csCorrs,
        csUnits: $scope.filters.csUnits
        //csIsCase: false
      }, { reload: true });
    };

    $scope.selectDoc = function (result) {
      selectDoc.push({
        docId: result.docId,
        regUri: result.regUri || '',
        docTypeName: result.docTypeName,
        docSubject: result.docSubject,
        type: $state.payload.type
      });
      return $state.go('^');
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  AppDocSelectCtrl.$inject = ['$scope', '$state', '$stateParams', 'Doc', 'selectDoc'];

  angular.module('aop').controller('AppDocSelectCtrl', AppDocSelectCtrl);
}(angular, _));
