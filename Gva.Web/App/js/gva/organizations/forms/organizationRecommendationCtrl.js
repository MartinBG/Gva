/*global angular, _*/
(function (angular, _) {
  'use strict';
  function OrganizationRecommendationCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationInspection,
    Nomenclature) {

    $scope.watchList = [];
    $scope.model.part.auditorsReview.auditDetails = [];
    $scope.model.part.auditorsReview.disparities = [];

    $scope.chooseAudits = function () {
      $state.go($state.current.name + '.chooseAudits');
    };

    if ($scope.model.part.includedAudits.length > 0) {
      $scope.model.part.auditorsReview = {
        auditDetails: [],
        disparities: []
      };
      _.each($scope.model.part.includedAudits, function (auditPartIndex) {
        OrganizationInspection.get({
          id: $stateParams.id,
          ind: auditPartIndex
        }).$promise.then(function (audit) {

          _.each(audit.part.auditDetails, function (detail) {
            detail.auditPart = audit.part.auditPart;
          });

          _.each(audit.part.disparities, function (disparity) {
            disparity.partIndex = audit.partIndex;
            disparity.auditPart = audit.part.auditPart;
          });

          $scope.model.part.auditorsReview.auditDetails =
            _.union($scope.model.part.auditorsReview.auditDetails, audit.part.auditDetails);
          $scope.model.part.auditorsReview.disparities =
            _.union($scope.model.part.auditorsReview.disparities, audit.part.disparities);
        });
      });
    }

    $scope.editDisparity = function (disparity) {
      return $state.go($state.current.name + '.editDisparity', {
        id: $stateParams.id,
        ind: $stateParams.ind,
        childInd: disparity.partIndex
      });
    };


    $scope.insertAuditDetails = function () {
      Nomenclature.query({ alias: 'auditDetails', type: 'organizationRecommendations'})
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

  OrganizationRecommendationCtrl.$inject =
    ['$scope', '$state', '$stateParams', 'OrganizationInspection', 'Nomenclature'];

  angular.module('gva')
    .controller('OrganizationRecommendationCtrl', OrganizationRecommendationCtrl);
}(angular, _));