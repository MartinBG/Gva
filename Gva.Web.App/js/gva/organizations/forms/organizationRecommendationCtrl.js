/*global angular, _*/
(function (angular, _) {
  'use strict';
  function OrganizationRecommendationCtrl(
    $scope,
    $state,
    OrganizationInspections,
    Nomenclatures,
    scModal,
    scFormParams
    ) {
    $scope.lotId = scFormParams.lotId;

    $scope.auditorsReview = {
      auditDetails: [],
      disparities: []
    };

    var fillAuditorsReview = function (inspections) {
      $scope.auditorsReview.auditDetails = [];
      $scope.auditorsReview.disparities = [];

      _.each($scope.model.part.includedAudits, function (ind) {
        var inspection = _.find(inspections, { partIndex: ind });

        _.each(inspection.part.auditDetails, function (detail) {
          detail.auditPart = inspection.part.auditPart;
        });

        _.each(inspection.part.disparities, function (disparity) {
          disparity.partIndex = inspection.partIndex;
          disparity.auditPart = inspection.part.auditPart;
        });

        $scope.auditorsReview.auditDetails = $scope.auditorsReview.auditDetails
          .concat(inspection.part.auditDetails);
        $scope.auditorsReview.disparities = $scope.auditorsReview.disparities
          .concat(inspection.part.disparities);
      });
    };

    OrganizationInspections.query({ id: scFormParams.lotId }).$promise.then(function (inspections) {
      var unbindWatcher = $scope.$watch('model', function () {
        if (!$scope.model) {
          return;
        }

        fillAuditorsReview(inspections);
        unbindWatcher();
      });
    });

    $scope.chooseAudits = function () {
      var modalInstance = scModal.open('chooseInspections', {
        includedInspections: $scope.model.part.includedAudits,
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedInspections) {
        $scope.model.part.includedAudits = _.pluck(selectedInspections, 'partIndex');
        fillAuditorsReview(selectedInspections);
      });

      return modalInstance.opened;
    };

    $scope.insertAuditDetails = function () {
      return Nomenclatures.query({
        alias: 'auditDetails',
        type: 'organizationRecommendations',
        auditPartCode: $scope.model.part.recommendationPart.code
      })
        .$promise.then(function (details) {
        $scope.model.part.descriptionReview.auditDetails = details;
      });
    };

    $scope.editAuditorsReviewDisparity = function (disparity) {
      return $state.go('root.organizations.view.inspections.edit', {
        id: scFormParams.lotId,
        ind: disparity.partIndex
      });
    };

    $scope.editDescriptionReviewDisparity = function (disparity) {
      var modalInstance = scModal.open('editDisparity', {
        disparity: disparity
      });

      return modalInstance.opened;
    };

    $scope.addDisparity = function (detail) {
      var maxSortNumber = 0;

      _.each($scope.model.part.descriptionReview.auditDetails, function (auditDetail) {
        _.each(auditDetail.group, function (item) {
          maxSortNumber = Math.max(maxSortNumber, _.max(item.disparities));
        });
      });

      var disparity = {
          subject: detail.subject,
          auditPart: detail.auditPart
        };
      var modalInstance = scModal.open('editDisparity', {
        disparity: disparity
      });

      modalInstance.result.then(function () {
        var maxSortNumber = 0;
        _.each($scope.model.part.auditDetails, function (auditDetail) {
          maxSortNumber = Math.max(maxSortNumber, _.max(auditDetail.disparities));
        });
        detail.disparities.push(++maxSortNumber);
        disparity.sortOrder = maxSortNumber;
        $scope.model.part.descriptionReview.disparities.push(disparity);
      });

      return modalInstance.opened;
    };

    $scope.deleteDisparity = function (disparity) {
      var auditDetail;
      _.each($scope.model.part.descriptionReview.auditDetails, function (detail) {
        if (!!auditDetail) {
          return;
        }
        auditDetail = _.where(detail.group, { subject: disparity.subject })[0];
      });

      var sortOrderIndex = auditDetail.disparities.indexOf(disparity.sortOrder);
      auditDetail.disparities.splice(sortOrderIndex, 1);

      var index = $scope.model.part.descriptionReview.disparities.indexOf(disparity);
      $scope.model.part.descriptionReview.disparities.splice(index, 1);
    };
  }

  OrganizationRecommendationCtrl.$inject = [
    '$scope',
    '$state',
    'OrganizationInspections',
    'Nomenclatures',
    'scModal',
    'scFormParams'
  ];

  angular.module('gva')
    .controller('OrganizationRecommendationCtrl', OrganizationRecommendationCtrl);
}(angular, _));