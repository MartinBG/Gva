/*global angular*/
(function (angular) {
  'use strict';

  function SelectUserModalCtrl($scope,
    $modalInstance,
    scModalParams,
    UnitUsersResource) {

    var unitId = scModalParams.unitId;

    $scope.model = UnitUsersResource.query();

    $scope.selectUser = function (userId) {
      return UnitUsersResource.save({ id: unitId, userId: userId })
        .$promise.then(function () {
          return $modalInstance.close(true);
        });
    };

    $scope.cancel = function () {
      return $modalInstance.close(null);
    };
  }

  SelectUserModalCtrl.$inject = ['$scope', '$modalInstance', 'scModalParams', 'UnitUsersResource'];

  angular.module('common').controller('SelectUserModalCtrl', SelectUserModalCtrl);
}(angular));
