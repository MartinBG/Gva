/*global angular*/
(function (angular) {
  'use strict';

  function EditUnitModalCtrl($scope, $modalInstance, scModalParams,
    ClassificationsResource,
    ClassificationPermissionsResource) {

    if (scModalParams.unitType === 'Department') {
      $scope.unitTypes = [
        { value: 'Department', name: 'Организация' },
        { value: 'Position', name: 'Длъжност' }
      ];
    } else if (scModalParams.unitType === 'Employee') {
      $scope.unitTypes = [
        { value: 'Employee', name: 'Служител' }
      ];
    }
    $scope.selectedItem = $scope.unitTypes[0];

    $scope.model = {
      name: '',
      parentId: scModalParams.parentId,
      type: $scope.selectedItem.value,
      classifications: []
    };

    $scope.classifications = [];

    ClassificationsResource.query().$promise.then(function (classifications) {
      $scope.classifications = classifications;
    });

    ClassificationPermissionsResource.query().$promise.then(function (classificationPermissions) {
      $scope.classificationPermissions = classificationPermissions;
    });


    $scope.addClassification = function () {
      $scope.model.classifications.push({});
    };

    $scope.returnVal = function () {
      $modalInstance.close($scope.model);
    };

    $scope.cancel = function () {
      $modalInstance.close(null);
    };
  }

  EditUnitModalCtrl.$inject = ['$scope', '$modalInstance', 'scModalParams', 'ClassificationsResource', 'ClassificationPermissionsResource'];

  angular.module('common').controller('EditUnitModalCtrl', EditUnitModalCtrl);
}(angular));
