/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReferenceLicencesCtrl(
    $scope,
    $state,
    $stateParams,
    licences
  ) {
    $scope.filters = {
      fromDate: null,
      toDate: null,
      licenceTypeId: null,
      licenceActionId: null,
      lin: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.licences = licences;

    $scope.search = function () {
      return $state.go('root.personsReferences.licences', {
        fromDate: $scope.filters.fromDate,
        toDate: $scope.filters.toDate,
        licenceTypeId: $scope.filters.licenceTypeId,
        licenceActionId: $scope.filters.licenceActionId,
        lin: $scope.filters.lin
      });
    };
  }

  PersonReferenceLicencesCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'licences'
  ];

  PersonReferenceLicencesCtrl.$resolve = {
    licences: [
      '$stateParams',
      'PersonReferences',
      function ($stateParams, PersonReferences) {
        return PersonReferences.getLicences($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonReferenceLicencesCtrl', PersonReferenceLicencesCtrl);
}(angular, _));
