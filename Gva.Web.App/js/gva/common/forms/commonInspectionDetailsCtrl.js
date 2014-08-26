/*global angular,_*/
(function (angular, _) {
  'use strict';
  function CommonInspectionDetailsCtrl($scope, AuditPartRequirementDetails, scFormParams, scModal) {
    $scope.setPart = scFormParams.setPart;

    function fillDetailDisparities() {
      $scope.detailDisparities = {};

      if (!$scope.model || !$scope.model.inspectionDetails || !$scope.model.disparities) {
        return;
      }

      _.each($scope.model.disparities, function (d, i) {
        $scope.detailDisparities[d.detailCode] = $scope.detailDisparities[d.detailCode] || [];
        $scope.detailDisparities[d.detailCode].push(i);
      });
    }

    fillDetailDisparities();

    $scope.insertinspectionDetails = function () {
      var query = {};
      if ($scope.setPart === 'aircraft') {
        query = {
          type: 'aircrafts'
        };
      } else if ($scope.setPart === 'organization') {
        query = {
          auditPartCode: $scope.model.auditPart.code,
          type: 'organizations'
        };
      } else if ($scope.setPart === 'airport') {
        query = {
          type: 'airports'
        };
      } else if ($scope.setPart === 'equipment') {
        query = {
          type: 'equipments'
        };
      }

      return AuditPartRequirementDetails
        .query(query)
        .$promise.then(function (details) {
          $scope.model.inspectionDetails = details;
          $scope.model.disparities = [];
        });
    };

    $scope.addDisparity = function (inspectionDetail) {
      var disparity = {
        detailCode: inspectionDetail.code
      };
      var modalInstance = scModal.open('editDisparity', {
        disparity: disparity
      });

      modalInstance.result.then(function () {
        $scope.model.disparities.push(disparity);
        fillDetailDisparities();
      });

      return modalInstance.opened;
    };

    $scope.deleteDisparity = function (disparity) {
      var i = $scope.model.disparities.indexOf(disparity);
      $scope.model.disparities.splice(i, 1);
      fillDetailDisparities();
    };

    $scope.editDisparity = function (disparity) {
      var modalInstance = scModal.open('editDisparity', {
        disparity: disparity
      });

      return modalInstance.opened;
    };
  }

  CommonInspectionDetailsCtrl.$inject = [
    '$scope',
    'AuditPartRequirementDetails',
    'scFormParams',
    'scModal'
  ];

  angular.module('gva').controller('CommonInspectionDetailsCtrl', CommonInspectionDetailsCtrl);
}(angular, _));
