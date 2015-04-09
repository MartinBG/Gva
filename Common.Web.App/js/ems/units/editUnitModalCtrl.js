/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EditUnitModalCtrl($scope, $modalInstance, scModalParams, UnitsResource) {
    $scope.form = {};

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
      permissions: []
    };

    $scope.addClassification = function () {
      $scope.model.permissions.push({});
    };

    $scope.removeClassification = function (permission) {
      _.pull($scope.model.permissions, permission);
    };

    $scope.returnVal = function () {
      $modalInstance.close($scope.model);
    };

    $scope.validatePermissionsNotRepeated = function () {
      var array = $scope.model.permissions;

      for (var i = 0; i < array.length; i++) {
        var item = array[i];
        for (var j = 0; j < array.length; j++) {
          if (i === j) continue;
          var innerItem = array[j];
          if (item.classificationId == innerItem.classificationId
            && item.classificationPermissionId == innerItem.classificationPermissionId) {
            return false;
          }
        }
      }
      return true;
    }

    $scope.save = function () {
      return $scope.form.unitForm.$validate().then(function () {

        $scope.model.classifications = $scope.model.permissions;

        if ($scope.form.unitForm.$valid) {

          UnitsResource.save($scope.model)
            .$promise.then(function () {
              //.then(function (result) {
              //    return $state.go('root.docs.edit.view', { id: result.docId });
            
          });
        }
      });
    };

    $scope.cancel = function () {
      $modalInstance.close(null);
    };
  }

  EditUnitModalCtrl.$inject = ['$scope', '$modalInstance', 'scModalParams', 'UnitsResource'];

  angular.module('common').controller('EditUnitModalCtrl', EditUnitModalCtrl);
}(angular, _));
