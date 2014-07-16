/*global angular, _*/
(function (angular, _) {
  'use strict';
  function OrganizationRecommendationCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationInspections,
    Nomenclatures,
    namedModal
    ) {
    $scope.auditorsReview = {
      auditDetails: [],
      disparities: []
    };
    $scope.watchList = [];

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

    OrganizationInspections.query({ id: $stateParams.id }).$promise.then(function (inspections) {
      var unbindWatcher = $scope.$watch('model', function () {
        if (!$scope.model) {
          return;
        }

        fillAuditorsReview(inspections);
        unbindWatcher();
      });
    });

    $scope.chooseAudits = function () {
      var modalInstance = namedModal.open('chooseInspections', {
        includedInspections: $scope.model.part.includedAudits
      });

      modalInstance.result.then(function (selectedInspections) {
        $scope.model.part.includedAudits = _.pluck(selectedInspections, 'partIndex');
        fillAuditorsReview(selectedInspections);
      });

      return modalInstance.opened;
    };

    $scope.editDisparity = function (disparity) {
      return $state.go('root.organizations.view.inspections.edit', {
        id: $stateParams.id,
        ind: disparity.partIndex
      });
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

    $scope.changedSortOrder = function (newValue, oldValue) {
      if (_.where($scope.model.part.descriptionReview.disparities,
        { sortOrder: newValue })[0]) {
        var subject =
          _.where($scope.model.part.descriptionReview.disparities,
          { sortOrder: newValue })[0].subject,
          auditDetail,
          sortOrderIndex;

        _.each($scope.model.part.descriptionReview.auditDetails, function (detail) {
          if (!!auditDetail) {
            return;
          }
          auditDetail = _.where(detail.group, { subject: subject })[0];
        });

        sortOrderIndex = auditDetail.disparities.indexOf(oldValue);
        auditDetail.disparities[sortOrderIndex] = newValue;
      }
    };

    for (var index = 0; index < $scope.model.part.descriptionReview.disparities.length; index++) {
      var watchString = 'model.part.descriptionReview.disparities[' + index + '].sortOrder';
      $scope.watchList.push($scope.$watch(watchString, $scope.changedSortOrder));
    }

    $scope.addDisparity = function (detail) {
      var maxSortNumber = 0;

      _.each($scope.model.part.descriptionReview.auditDetails, function (auditDetail) {
        _.each(auditDetail.group, function (item) {
          maxSortNumber = Math.max(maxSortNumber, _.max(item.disparities));
        });
      });

      detail.disparities.push(++maxSortNumber);

      $scope.model.part.descriptionReview.disparities.push({
        sortOrder: maxSortNumber,
        subject: detail.subject,
        auditPart: detail.auditPart
      });

      var watchString = 'model.part.descriptionReview.disparities[' +
        $scope.model.part.descriptionReview.disparities.length +
        '].sortOrder';
      $scope.watchList.push($scope.$watch(watchString, $scope.changedSortOrder));

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
      $scope.watchList[index]();
    };
  }

  OrganizationRecommendationCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationInspections',
    'Nomenclatures',
    'namedModal'
  ];

  angular.module('gva')
    .controller('OrganizationRecommendationCtrl', OrganizationRecommendationCtrl);
}(angular, _));