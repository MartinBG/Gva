/*global angular*/
(function (angular) {
  'use strict';
  function CommonAuditDetailCtrl($scope, Nomenclatures) {

    $scope.addDisparity = function (detail) {
      $scope.$parent.addDisparity(detail);
    };

    $scope.insertAuditDetails = function () {
      var queryString = {};
      if ($scope.$parent.form.$name === 'aircraftInspectionForm') {
        queryString = {
          alias: 'auditPartRequirements',
          type: 'aircrafts'
        };
      } else if ($scope.$parent.form.$name === 'organizationInspectionForm') {
        queryString = {
          alias: 'auditPartRequirements',
          auditPartCode: $scope.model.auditPart.code,
          type: 'organizations'
        };
      } else if ($scope.$parent.form.$name === 'airportInspectionForm') {
        queryString = {
          alias: 'auditDetails',
          type: 'airports'
        };
      }
      return Nomenclatures.query(queryString)
        .$promise.then(function (details) {
          $scope.model.auditDetails = details;
        });
    };

  }

  CommonAuditDetailCtrl.$inject = ['$scope', 'Nomenclatures'];

  angular.module('gva').controller('CommonAuditDetailCtrl', CommonAuditDetailCtrl);
}(angular));
