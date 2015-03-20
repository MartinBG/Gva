/*global angular*/
(function (angular) {
  'use strict';

  function EditUnitModalCtrl($scope, $modalInstance, scModalParams,
    ClassificationsResource,
    ClassificationPermissionsResource) {
    //$scope.model = unitsModel;

    ClassificationsResource.get();
    ClassificationPermissionsResource.get();

    $scope.paramFromOutside = scModalParams.type;//parentId;

    $scope.test = 'i am the controller';

    $scope.returnVal = function () {
        $modalInstance.close('data from modal');
    }

    $scope.cancel = function () {
      $modalInstance.close(null);
    };
  }

  EditUnitModalCtrl.$inject = ['$scope', '$modalInstance', 'scModalParams', 'ClassificationsResource', 'ClassificationPermissionsResource'];

  angular.module('common').controller('EditUnitModalCtrl', EditUnitModalCtrl);
}(angular));
