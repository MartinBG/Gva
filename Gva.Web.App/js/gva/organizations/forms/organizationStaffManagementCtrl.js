/*global angular*/
(function (angular) {
  'use strict';
  function OrgStaffManagementCtrl($scope, namedModal, scFormParams) {
    $scope.lotId = scFormParams.lotId;

    $scope.chooseEmployment = function () {
      var modalInstance = namedModal.open('chooseEmployment');

      modalInstance.result.then(function (employmentName) {
        $scope.model.part.position = employmentName;
      });
    };
  }

  OrgStaffManagementCtrl.$inject = ['$scope', 'namedModal', 'scFormParams'];

  angular.module('gva').controller('OrgStaffManagementCtrl', OrgStaffManagementCtrl);
}(angular));
