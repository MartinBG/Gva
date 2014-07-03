/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AppDocSelectCtrl(
    $scope,
    $state,
    $stateParams,
    Admissions,
    selectDoc) {
    $scope.filters = {
      csFromDate: null,
      csToDate: null,
      csRegUri: null,
      csDocName: null,
      csDocTypeId: null,
      csDocStatusId: null,
      csCorrs: null,
      csUnits: null,
      csIsChosen: null
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
      isChosen: $stateParams.csIsChosen
    };

    Admissions.getDocs(stateParams).$promise.then(function (docs) {
      $scope.docs = docs.documents;
      $scope.docCount = docs.documentCount;
    });

    $scope.search = function () {
      $state.go($state.current, {
        csFromDate: $scope.filters.csFromDate,
        csToDate: $scope.filters.csToDate,
        csRegUri: $scope.filters.csRegUri,
        csDocName: $scope.filters.csDocName,
        csDocTypeId: $scope.filters.csDocTypeId,
        csDocStatusId: $scope.filters.csDocStatusId,
        csCorrs: $scope.filters.csCorrs,
        csUnits: $scope.filters.csUnits,
        csIsChosen: $scope.filters.csIsChosen
      }, { reload: true });
    };

    $scope.selectDoc = function (result) {
      selectDoc.push({
        docId: result.docId,
        regUri: result.regUri || '',
        docTypeName: result.docTypeName,
        docSubject: result.docSubject,
        type: $stateParams.type
      });
      return $state.go('^');
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  AppDocSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Admissions',
    'selectDoc'];

  angular.module('mosv').controller('AppDocSelectCtrl', AppDocSelectCtrl);
}(angular, _));
