/*global angular*/
(function (angular) {
  'use strict';

  function AdmissionsNewCtrl($scope, $state, Admissions, admission) {
    $scope.admission = admission;

    $scope.save = function () {
      return $scope.newAdmissionForm.$validate()
      .then(function () {
        if ($scope.newAdmissionForm.$valid) {
          return Admissions.save($scope.admission).$promise
            .then(function () {
              return $state.go('root.admissions.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.admissions.search');
    };
  }

  AdmissionsNewCtrl.$inject = ['$scope', '$state', 'Admissions', 'admission'];

  AdmissionsNewCtrl.$resolve = {
    admission: function () {
      return {
        admissionData: {}
      };
    }
  };

  angular.module('mosv').controller('AdmissionsNewCtrl', AdmissionsNewCtrl);
}(angular));