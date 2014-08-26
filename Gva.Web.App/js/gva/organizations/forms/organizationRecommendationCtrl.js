/*global angular, _*/
(function (angular, _) {
  'use strict';
  function OrganizationRecommendationCtrl(
    $scope,
    AuditPartSectionDetails,
    scModal,
    scFormParams
  ) {
    $scope.lotId = scFormParams.lotId;
    $scope.parts = {
      part1: true,
      part2: false,
      part3: false,
      part4: false,
      part5: false
    };

    function fillInspectionsDisparities() {
      $scope.inspectionsDisparities = [];

      _.each($scope.inspections, function (inspection) {
        $scope.inspectionsDisparities =
          $scope.inspectionsDisparities.concat(inspection.part.disparities);
      });
    }

    function fillDetailDisparities() {
      $scope.detailDisparities = {};

      if (!$scope.model.part.recommendationDetails || !$scope.model.part.disparities) {
        return;
      }

      _.each($scope.model.part.disparities, function (d, i) {
        $scope.detailDisparities[d.detailCode] = $scope.detailDisparities[d.detailCode] || [];
        $scope.detailDisparities[d.detailCode].push(i);
      });
    }

    fillDetailDisparities();

    $scope.inspections = scFormParams.inspections;
    fillInspectionsDisparities();

    $scope.chooseInspections = function () {
      var modalInstance = scModal.open('chooseInspections', {
        includedInspections: $scope.model.part.inspections,
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedInspections) {
        $scope.inspections = selectedInspections;
        $scope.model.part.inspections = _.pluck(selectedInspections, 'partIndex');
        fillInspectionsDisparities();
      });

      return modalInstance.opened;
    };

    $scope.deleteInspection = function (inspection) {
      var index = $scope.inspections.indexOf(inspection);
      Array.prototype.splice.apply($scope.inspections, [index, 1]);
      Array.prototype.splice.apply($scope.model.part.inspections, [index, 1]);
      fillInspectionsDisparities();
    };

    $scope.insertRecommendationDetails = function () {
      return AuditPartSectionDetails
        .query({
          auditPartCode: $scope.model.part.auditPart.code
        })
        .$promise
        .then(function (details) {
          $scope.model.part.recommendationDetails = details;
        });
    };

    $scope.addDisparity = function (recommendationDetail) {
      var disparity = {
        detailCode: recommendationDetail.code
      };
      var modalInstance = scModal.open('editDisparity', {
        disparity: disparity
      });

      modalInstance.result.then(function () {
        $scope.model.part.disparities.push(disparity);
        fillDetailDisparities();
      });

      return modalInstance.opened;
    };

    $scope.deleteDisparity = function (disparity) {
      var i = $scope.model.part.disparities.indexOf(disparity);
      $scope.model.part.disparities.splice(i, 1);
      fillDetailDisparities();
    };

    $scope.editDisparity = function (disparity) {
      var modalInstance = scModal.open('editDisparity', {
        disparity: disparity
      });

      return modalInstance.opened;
    };
  }

  OrganizationRecommendationCtrl.$inject = [
    '$scope',
    'AuditPartSectionDetails',
    'scModal',
    'scFormParams'
  ];

  angular.module('gva')
    .controller('OrganizationRecommendationCtrl', OrganizationRecommendationCtrl);
}(angular, _));