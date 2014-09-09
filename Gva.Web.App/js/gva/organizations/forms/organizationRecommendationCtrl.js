/*global angular, _*/
(function (angular, _) {
  'use strict';
  function OrganizationRecommendationCtrl(
    $scope,
    AuditPartSectionDetails,
    OrganizationInspections,
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

    function fillInspections() {
      $scope.inspections = [];
      $scope.inspectionsDisparities = [];

      if (!$scope.model || !$scope.model.part.inspections.length) {
        return;
      }

      OrganizationInspections
        .query({ id: $scope.lotId, partIndexes: $scope.model.part.inspections })
        .$promise
        .then(function (inspections) {
          _(inspections)
            .sortBy(function (i) {
              return $scope.model.part.inspections.indexOf(i.partIndex);
            })
            .each(function (i) {
              $scope.inspections.push({
                partIndex: i.partIndex, //used for deletion
                startDate: i.part.startDate,
                endDate: i.part.endDate,
                subject: i.part.subject
              });

              _.each(i.part.disparities, function (d) {
                $scope.inspectionsDisparities.push({
                  inspectionPartIndex: i.partIndex, //used for deletion
                  refNumber: d.refNumber,
                  description: d.description,
                  disparityLevel: d.disparityLevel,
                  removalDate: d.removalDate,
                  closureDate: d.closureDate,
                  rectifyAction: d.rectifyAction,
                  closureDocument: d.closureDocument
                });
              });
            });
        });
    }

    function fillDetailDisparities() {
      $scope.detailDisparities = {};

      if (!$scope.model ||
        !$scope.model.part.recommendationDetails.length ||
        !$scope.model.part.disparities.length
      ) {
        return;
      }

      _.each($scope.model.part.disparities, function (d, i) {
        $scope.detailDisparities[d.detailCode] = $scope.detailDisparities[d.detailCode] || [];
        $scope.detailDisparities[d.detailCode].push(i);
      }); 
    }

    $scope.$watch(function() {
      return $scope.model;
    }, function () {
      fillInspections();
      fillDetailDisparities();
    });

    $scope.chooseInspections = function () {
      var modalInstance = scModal.open('chooseInspections', {
        includedInspections: $scope.model.part.inspections,
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedInspections) {
        $scope.model.part.inspections = _.pluck(selectedInspections, 'partIndex');

        fillInspections();
      });

      return modalInstance.opened;
    };

    $scope.deleteInspection = function (inspectionPartIndex) {
      var index = $scope.model.part.inspections.indexOf(inspectionPartIndex);
      $scope.model.part.inspections.splice(index, 1);

      $scope.inspections = _.filter($scope.inspections, function (i) {
        return i.partIndex !== inspectionPartIndex;
      });

      $scope.inspectionsDisparities = _.filter($scope.inspectionsDisparities, function (d) {
        return d.inspectionPartIndex !== inspectionPartIndex;
      });
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
    'OrganizationInspections',
    'scModal',
    'scFormParams'
  ];

  angular.module('gva')
    .controller('OrganizationRecommendationCtrl', OrganizationRecommendationCtrl);
}(angular, _));