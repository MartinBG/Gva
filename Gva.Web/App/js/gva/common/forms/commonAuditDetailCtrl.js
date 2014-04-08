/*global angular*/
(function (angular) {
  'use strict';
  function CommonAuditDetailCtrl($scope, Nomenclature) {

    $scope.addDisparity = function (detail) {
      $scope.$parent.addDisparity(detail);
    };

    $scope.insertAuditDetails = function () {
      if ($scope.$parent.form.$name === 'aircraftInspectionForm') {
        $scope.model.auditDetails = Nomenclature.query({
          alias: 'auditPartRequirements',
          type: 'aircrafts'
        });
      } else if ($scope.$parent.form.$name === 'organizationInspectionForm') {
        $scope.model.auditDetails = Nomenclature.query({
          alias: 'auditPartRequirements',
          auditPartCode: $scope.model.auditPart.code,
          type: 'organizations'
        });
      } else if ($scope.$parent.form.$name === 'airportInspectionForm') {
        $scope.model.auditDetails = Nomenclature.query({
          alias: 'auditDetails',
          type: 'airports'
        });
      }
    };

  }

  CommonAuditDetailCtrl.$inject = ['$scope', 'Nomenclature'];

  angular.module('gva').controller('CommonAuditDetailCtrl', CommonAuditDetailCtrl);
}(angular));
