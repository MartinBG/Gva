/*global angular*/
(function (angular) {
  'use strict';

  function AdmissionsNewCtrl($scope, $state, Admission, admission) {
    $scope.admission = admission;

    $scope.save = function () {
      return $scope.newAdmissionForm.$validate()
      .then(function () {
        if ($scope.newAdmissionForm.$valid) {
          return Admission.save($scope.admission).$promise
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

  AdmissionsNewCtrl.$inject = ['$scope', '$state', 'Admission', 'admission'];

  AdmissionsNewCtrl.$resolve = {
    admission: function () {
      return {
        admissionData: {}
      };
    }
  };

  angular.module('mosv').controller('AdmissionsNewCtrl', AdmissionsNewCtrl);
}(angular));