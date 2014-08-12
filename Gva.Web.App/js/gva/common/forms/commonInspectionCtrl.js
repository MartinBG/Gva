/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CommonInspectionCtrl(
    $scope,
    $state,
    Organizations,
    scFormParams,
    scModal
  ) {
    $scope.setPart = scFormParams.setPart;
    $scope.lotId = scFormParams.lotId;

    $scope.model.part.examiners = $scope.model.part.examiners || [];
    $scope.model.part.auditDetails = $scope.model.part.auditDetails || [];
    $scope.model.part.disparities = $scope.model.part.disparities || [];

    if($scope.setPart === 'organization' && $scope.model.partIndex) {
      Organizations.getRecommendations({
        id: $scope.lotId,
        ind: $scope.model.partIndex
      }).$promise.then(function (result) {
        $scope.recommendationReports = result.reports;
      });
    }

    $scope.addDisparity = function (detail) {
      var disparity = {
        subject: detail.subject
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
        $scope.model.part.disparities.push(disparity);
      });

      return modalInstance.opened;
    };

    $scope.deleteDisparity = function (disparity) {
      var auditDetail = _.where($scope.model.part.auditDetails, { subject: disparity.subject })[0];

      var sortOrderIndex = auditDetail.disparities.indexOf(disparity.sortOrder);
      auditDetail.disparities.splice(sortOrderIndex, 1);

      var index = $scope.model.part.disparities.indexOf(disparity);
      $scope.model.part.disparities.splice(index, 1);
    };

    $scope.editDisparity = function (disparity) {
      var modalInstance = scModal.open('editDisparity', {
        disparity: disparity
      });

      return modalInstance.opened;
    };

    $scope.viewRecommendation = function (recommendation) {
      return $state.go('root.organizations.view.recommendations.edit', {
        id: $scope.lotId,
        ind: recommendation.partIndex
      });
    };
  }

  CommonInspectionCtrl.$inject = [
    '$scope',
    '$state',
    'Organizations',
    'scFormParams',
    'scModal'
  ];

  angular.module('gva').controller('CommonInspectionCtrl', CommonInspectionCtrl);
}(angular, _));
