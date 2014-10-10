/*global angular*/
(function (angular) {
  'use strict';
  function OrgStaffManagementCtrl($scope, scModal, scFormParams) {
    $scope.lotId = scFormParams.lotId;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;

    $scope.chooseEmployment = function () {
      var modalInstance = scModal.open('chooseEmployment');

      modalInstance.result.then(function (employmentName) {
        $scope.model.part.position = employmentName;
      });
    };
  }

  OrgStaffManagementCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

  angular.module('gva').controller('OrgStaffManagementCtrl', OrgStaffManagementCtrl);
}(angular));
