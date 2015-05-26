/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AppDocSelectCtrl(
    $scope,
    $state,
    $stateParams,
    Aops,
    docs,
    selectDoc) {
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
      csIsChosen: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.search = function () {
      $state.go('root.apps.edit.docSelect', {
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

    $scope.getDocs = function (page, pageSize) {
      var params = {};

      _.assign(params, $stateParams);
      _.assign(params, {
        offset: (page - 1) * pageSize,
        limit: pageSize
      });

      return Aops.getDocs(params).$promise;
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
    'Aops',
    'docs',
    'selectDoc'];

  AppDocSelectCtrl.$resolve = {
    docs: [
      '$stateParams',
      'Aops',
      function resolveDocs($stateParams, Aops) {
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

        return Aops.getDocs(stateParams).$promise;
      }
    ]
  };

  angular.module('aop').controller('AppDocSelectCtrl', AppDocSelectCtrl);
}(angular, _));
