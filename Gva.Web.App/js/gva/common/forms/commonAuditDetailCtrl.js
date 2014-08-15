/*global angular*/
(function (angular) {
  'use strict';
  function CommonAuditDetailCtrl($scope, Nomenclatures, scFormParams) {

    $scope.setPart = scFormParams.setPart;

    $scope.addDisparity = function (detail) {
      $scope.$parent.addDisparity(detail);
    };

    $scope.insertAuditDetails = function () {
      var queryString = {};
      if ($scope.setPart === 'aircraft') {
        queryString = {
          alias: 'auditPartRequirements',
          type: 'aircrafts'
        };
      } else if ($scope.setPart === 'organization') {
        queryString = {
          alias: 'auditPartRequirements',
          auditPartCode: $scope.model.auditPart.code,
          type: 'organizations'
        };
      } else if ($scope.setPart === 'airport') {
        queryString = {
          alias: 'auditDetails',
          type: 'airports'
        };
      } else if ($scope.setPart === 'equipment') {
        queryString = {
          alias: 'auditDetails',
          type: 'equipments'
        };
      }

      return Nomenclatures.query(queryString)
        .$promise.then(function (details) {
          $scope.model.auditDetails = details;
        });
    };

  }

  CommonAuditDetailCtrl.$inject = ['$scope', 'Nomenclatures', 'scFormParams'];

  angular.module('gva').controller('CommonAuditDetailCtrl', CommonAuditDetailCtrl);
}(angular));
