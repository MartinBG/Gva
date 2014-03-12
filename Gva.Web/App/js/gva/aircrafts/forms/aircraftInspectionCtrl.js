/*global angular, _*/
(function (angular, _) {
  'use strict';
  function AircraftInspectionCtrl($scope, $q, AuditResult, AuditPartRequirement) {

    $scope.deleteExaminer = function removeExaminer(examiner) {
      var index = $scope.model.examiners.indexOf(examiner);
      $scope.model.examiners.splice(index, 1);
    };

    $scope.addExaminer = function () {
      var sortOder = 1;
      if ($scope.model.examiners.length > 0) {
        var lastNumber = _.max(_.pluck($scope.model.examiners, 'sortOrder'));
        sortOder = ++lastNumber;
      }

      $scope.model.examiners.push({
        sortOrder: sortOder
      });
    };

    $scope.addDisparity = function (detail) {
      detail.disparitiesList = detail.disparitiesList || [];

      var maxSortNumber = $scope.model.disparityNumber;

      _.each($scope.model.auditDetails, function (disparity) {
        var max = _.max(disparity.disparitiesList);
        if( max > maxSortNumber) {
          maxSortNumber = max;
        }
      });

      $scope.model.disparityNumber = ++maxSortNumber;

      detail.disparitiesList.push($scope.model.disparityNumber);

      $scope.model.disparities.push({
        sortOrder: $scope.model.disparityNumber,
        subject: detail.subject
      });
    };

    $scope.deleteDisparity = function (disparity) {
      var auditDetail = _.where($scope.model.auditDetails, { subject: disparity.subject })[0];

      var sortOrderIndex = auditDetail.disparitiesList.indexOf(disparity.sortOrder);
      auditDetail.disparitiesList.splice(sortOrderIndex, 1);

      var index = $scope.model.disparities.indexOf(disparity);
      $scope.model.disparities.splice(index, 1);
    };

    $scope.insertAscertainments = function () {
      var auditPartRequirementPromise = AuditPartRequirement.query().$promise,
        auditResultPromise = AuditResult.get({ alias: 'Not executed' }).$promise;

      $q.all([auditPartRequirementPromise, auditResultPromise]).then(function (results) {
        var auditPartRequirements = results[0],
          defaultAuditResult = results[1];

        _.each(auditPartRequirements, function (requirement) {
          $scope.model.auditDetails.push({
            subject: requirement,
            code: requirement.nomValueId,
            auditResult: defaultAuditResult
          });
        });
      });
    };

    $scope.changedSortOrder = function (disparity) {
      var auditDetail = _.where($scope.model.auditDetails, { subject: disparity.subject })[0];

      var sortOrderIndex = auditDetail.disparitiesList.indexOf(disparity.oldValue);
      auditDetail.disparitiesList[sortOrderIndex] = disparity.sortOrder;
      disparity.oldValue = disparity.sortOrder;
    };

    $scope.deleteSortOrderOldValue = function (disparity) {
      delete disparity.oldValue;
    };

    $scope.saveSortOrderOldValue = function (disparity) {
      disparity.oldValue = disparity.sortOrder;
    };
  }

  AircraftInspectionCtrl.$inject = [ '$scope', '$q', 'AuditResult', 'AuditPartRequirement' ];

  angular.module('gva').controller('AircraftInspectionCtrl', AircraftInspectionCtrl);
}(angular, _));
