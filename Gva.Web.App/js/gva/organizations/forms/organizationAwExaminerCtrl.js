/*global angular*/
(function (angular) {
  'use strict';
  function OrganizationAwExaminerCtrl($scope) {
    $scope.deleteAircraft = function (aircraft) {
      var index = $scope.model.part.approvedAircrafts.indexOf(aircraft);
      $scope.model.part.approvedAircrafts.splice(index, 1);
    };

    $scope.addAircraft = function () {
      $scope.model.part.approvedAircrafts.push({});
    };
  }

  OrganizationAwExaminerCtrl.$inject = ['$scope'];

  angular.module('gva').controller('OrganizationAwExaminerCtrl', OrganizationAwExaminerCtrl);
}(angular));
