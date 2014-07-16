/*global angular*/
(function (angular) {
  'use strict';
  function OrgStaffManagementCtrl($scope, namedModal) {
    $scope.chooseEmployment = function () {
      var modalInstance = namedModal.open('chooseEmployment');

      modalInstance.result.then(function (employmentName) {
        $scope.model.part.position = employmentName;
      });
    };
  }

  OrgStaffManagementCtrl.$inject = ['$scope', 'namedModal'];

  angular.module('gva').controller('OrgStaffManagementCtrl', OrgStaffManagementCtrl);
}(angular));
