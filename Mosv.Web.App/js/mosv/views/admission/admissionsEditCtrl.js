/*global angular,_*/
(function (angular) {
  'use strict';

  function AdmissionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    Admissions,
    AdmissionsData,
    admission,
    selectDoc
  ) {
    var originalAdmission = _.cloneDeep(admission.partData);

    $scope.admission = admission.partData;
    $scope.data = admission.data;
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
              .save({ id: $stateParams.id }, $scope.admission)
              .$promise
              .then(function () {
                return $state.go('root.admissions.search');
              });
          }
        });
    };

    $scope.connectToDoc = function () {
      return $state.go('root.admissions.edit.docSelect');
    };

    $scope.disconnectDoc = function () {
      $scope.data.applicationDocId = undefined;

      return $scope.fastSave();
    };

    $scope.loadData = function () {
      return Admissions
        .loadData({ id: $stateParams.id }, {})
        .$promise
        .then(function (data) {
          return $state.go('root.admissions.edit', { id: data.id }, { reload: true });
        });
    };

    $scope.fastSave = function () {
      return Admissions
        .fastSave({ id: $stateParams.id }, $scope.data)
        .$promise
        .then(function (data) {
          return $state.go('root.admissions.edit', { id: data.id }, { reload: true });
        });
    };

    if (selectDoc.length > 0) {
      var sd = selectDoc.pop();

      $scope.data.applicationDocId = sd.docId;

      return $scope.fastSave();
    }
  }

  AdmissionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Admissions',
    'AdmissionsData',
    'admission',
    'selectDoc'
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
    ],
    selectDoc: [function () {
      return [];
    }]
  };

  angular.module('mosv').controller('AdmissionsEditCtrl', AdmissionsEditCtrl);
}(angular, _));
