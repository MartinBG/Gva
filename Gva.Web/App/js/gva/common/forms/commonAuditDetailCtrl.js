/*global angular*/
(function (angular) {
  'use strict';
  function CommonAuditDetailCtrl($scope, AuditDetails) {

    $scope.addDisparity = function (detail) {
      $scope.$parent.addDisparity(detail);
    };

    $scope.insertAuditDetails = function () {
      if ($scope.$parent.form.$name === 'aircraftInspectionForm') {
        $scope.model.auditDetails = AuditDetails.query({ type: 'aircrafts' });
      } else if ($scope.$parent.form.$name === 'organizationInspectionForm') {
        $scope.model.auditDetails = AuditDetails.query({ type: 'organizations' });
      }
    };

  }

  CommonAuditDetailCtrl.$inject = ['$scope', 'AuditDetails'];

  angular.module('gva').controller('CommonAuditDetailCtrl', CommonAuditDetailCtrl);
}(angular));
