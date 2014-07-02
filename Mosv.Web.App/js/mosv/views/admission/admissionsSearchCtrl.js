/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AdmissionsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    admissions
  ) {

    $scope.filters = {
      incomingNumber: null,
      lot: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.admissions = admissions;

    $scope.search = function () {
      $state.go('root.admissions.search', {
        incomingNumber: $scope.filters.incomingNumber,
        incomingLot: $scope.filters.incomingLot,
        applicant: $scope.filters.applicant,
        incomingDate: $scope.filters.incomingDate
      });
    };

    $scope.newAdmission = function () {
      return $state.go('root.admissions.new');
    };

    $scope.viewAdmission = function (admission) {
      return $state.go('root.admissions.edit', { id: admission.id });
    };
  }

  AdmissionsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'admissions'
  ];

  AdmissionsSearchCtrl.$resolve = {
    admissions: [
      '$stateParams',
      'Admissions',
      function ($stateParams, Admissions) {
        return Admissions.query($stateParams).$promise;
      }
    ]
  };

  angular.module('mosv').controller('AdmissionsSearchCtrl', AdmissionsSearchCtrl);
}(angular, _));