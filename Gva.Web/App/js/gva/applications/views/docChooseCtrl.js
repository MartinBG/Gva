/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocChooseCtrl($scope, $state, $stateParams, Doc) {
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
      $state.go('root.applications.link.docChoose', {
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

    $scope.chooseDoc = function (doc) {
      $scope.$parent.doc = doc;

      return $state.go('root.applications.link.common');
    };

    $scope.cancel = function () {
      return $state.go('root.applications.link.common');
    };
  }

  DocChooseCtrl.$inject = ['$scope', '$state', '$stateParams', 'Doc'];

  angular.module('gva').controller('DocChooseCtrl', DocChooseCtrl);
}(angular, _));
