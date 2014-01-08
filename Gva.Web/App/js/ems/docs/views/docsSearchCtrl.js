/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsSearchCtrl($scope, $state, $stateParams, Doc) {
    $scope.filters = {
      fromDate: null,
      toDate: null
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
      $state.go('docs.search', {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate
        //names: $scope.filters.names,
        //licences: $scope.filters.licences,
        //ratings: $scope.filters.ratings,
        //organization: $scope.filters.organization
      });
    };

    $scope.viewDoc = function (doc) {
      return $state.go('docs.edit', { id: doc.docId });
    };
  }

  DocsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'Doc'];

  angular.module('ems').controller('DocsSearchCtrl', DocsSearchCtrl);
}(angular, _));
