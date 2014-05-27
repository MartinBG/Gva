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

    $scope.viewAdmission = function (Admission) {
      return $state.go('root.admissions.edit', { id: Admission.id });
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
      'Admission',
      function ($stateParams, Admission) {
        return Admission.query($stateParams).$promise;
      }
    ]
  };

  angular.module('mosv').controller('AdmissionsSearchCtrl', AdmissionsSearchCtrl);
}(angular, _));