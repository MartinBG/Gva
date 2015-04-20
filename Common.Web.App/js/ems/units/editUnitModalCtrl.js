/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EditUnitModalCtrl(
    $scope,
    $state,
    $modalInstance,
    scModalParams,
    UnitsResource) {

    $scope.form = {};

    if (scModalParams.unitType === 'Department') {
      if (scModalParams.parentId) {
        $scope.unitTypes = [
          { value: 'Department', name: 'Организация' },
          { value: 'Position', name: 'Длъжност' }
        ];
      } else {
        $scope.unitTypes = [
          { value: 'Department', name: 'Организация' },          
        ];
      }

    } else if (scModalParams.unitType === 'Employee') {
      $scope.unitTypes = [
        { value: 'Employee', name: 'Служител' }
      ];
    }

    $scope.isEditMode = scModalParams.isEditMode;

    if ($scope.isEditMode) {

      $scope.unitTypes = [
        { value: 'Department', name: 'Организация' },
        { value: 'Position', name: 'Длъжност' },
        { value: 'Employee', name: 'Служител' }
      ];

      $scope.selectedItem = _.find($scope.unitTypes, function (item) {
        return item.value === scModalParams.unit.type;
      });

      $scope.model = scModalParams.unit;
    } else {

      $scope.selectedItem = $scope.unitTypes[0];

      $scope.model = {
        name: '',
        parentUnitId: scModalParams.parentId,
        type: $scope.selectedItem.value,
        classifications: []
      };
    }

    $scope.addClassification = function () {
      $scope.model.classifications.push({});
    };

    $scope.removeClassification = function (permission) {
      _.pull($scope.model.classifications, permission);
    };

    $scope.validatePermissionsNotRepeated = function () {
      var array = $scope.model.classifications;

      for (var i = 0; i < array.length; i++) {
        var item = array[i];
        for (var j = 0; j < array.length; j++) {
          if (i === j) {
            continue;
          }
          var innerItem = array[j];
          if (item.classificationId === innerItem.classificationId &&
            item.classificationPermissionId === innerItem.classificationPermissionId) {
            return false;
          }
        }
      }
      return true;
    };

    $scope.save = function () {
      return $scope.form.unitForm.$validate().then(function () {

        if ($scope.form.unitForm.$valid) {
          if ($scope.isEditMode) {
            return $scope.model.$save().then(function () {
              return $modalInstance.close(true);
            });
          }
          else {
            return UnitsResource.save($scope.model)
              .$promise.then(function () {
                return $modalInstance.close(true);
              });
          }
        }
      });
    };

    $scope.cancel = function () {
      $modalInstance.close();
    };
  }

  EditUnitModalCtrl.$inject = ['$scope',
    '$state', '$modalInstance', 'scModalParams', 'UnitsResource'];

  angular.module('common').controller('EditUnitModalCtrl', EditUnitModalCtrl);
}(angular, _));
