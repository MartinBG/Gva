/*global angular,_*/
(function (angular) {
  'use strict';

  function AdmissionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AdmissionData,
    admission
  ) {
    var originalAdmission = _.cloneDeep(admission);
    $scope.admission = admission;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.admission = _.cloneDeep(originalAdmission);
    };

    $scope.save = function () {
      return $scope.editAdmissionForm.$validate()
        .then(function () {
          if ($scope.editAdmissionForm.$valid) {
            return AdmissionData
              .save({ id: $stateParams.id }, $scope.admission )
              .$promise
              .then(function () {
                return $state.go('root.admissions.search');
              });
          }
        });
    };
  }

  AdmissionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AdmissionData',
    'admission'
  ];

  AdmissionsEditCtrl.$resolve = {
    admission: [
      '$stateParams',
      'AdmissionData',
      function ($stateParams, AdmissionData) {
        return AdmissionData.get({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('mosv').controller('AdmissionsEditCtrl', AdmissionsEditCtrl);
}(angular, _));
