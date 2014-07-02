/*global angular,_*/
(function (angular) {
  'use strict';

  function AdmissionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AdmissionsData,
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
            return AdmissionsData
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
    'AdmissionsData',
    'admission'
  ];

  AdmissionsEditCtrl.$resolve = {
    admission: [
      '$stateParams',
      'AdmissionsData',
      function ($stateParams, AdmissionsData) {
        return AdmissionsData.get({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('mosv').controller('AdmissionsEditCtrl', AdmissionsEditCtrl);
}(angular, _));
