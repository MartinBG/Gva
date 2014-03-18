/*global angular, _*/
(function (angular, _) {
  'use strict';
  function AircraftInspectionCtrl($scope, AuditDetails) {
    
    $scope.deleteExaminer = function removeExaminer(examiner) {
      var index = $scope.model.examiners.indexOf(examiner);
      $scope.model.examiners.splice(index, 1);
    };

    $scope.addExaminer = function () {
      var sortOder = Math.max(0, _.max(_.pluck($scope.model.examiners, 'sortOrder'))) + 1;

      $scope.model.examiners.push({
        sortOrder: sortOder
      });
    };

    $scope.watchList = [];

    $scope.addDisparity = function (detail) {
      detail.disparities = detail.disparities || [];

      var maxSortNumber = 0;

      _.each($scope.model.auditDetails, function (auditDetail) {
        var max = _.max(auditDetail.disparities);
        if( max > maxSortNumber) {
          maxSortNumber = max;
        }
      });
      maxSortNumber = ++maxSortNumber;
      detail.disparities.push(maxSortNumber);

      $scope.model.disparities.push({
        sortOrder: maxSortNumber,
        subject: detail.subject
      });
      var watchString = 'model.disparities[' + $scope.model.disparities.length + '].sortOrder';
      $scope.watchList.push($scope.$watch(watchString, $scope.changedSortOrder));
    };

    $scope.deleteDisparity = function (disparity) {
      var auditDetail = _.where($scope.model.auditDetails, { subject: disparity.subject })[0];

      var sortOrderIndex = auditDetail.disparities.indexOf(disparity.sortOrder);
      auditDetail.disparities.splice(sortOrderIndex, 1);

      var index = $scope.model.disparities.indexOf(disparity);
      $scope.model.disparities.splice(index, 1);
      $scope.watchList[index]();
    };

    $scope.insertAuditDetails = function () {
      $scope.model.auditDetails = AuditDetails.query({ type: 'aircrafts' });
    };

    $scope.changedSortOrder = function (newValue, oldValue) {
      if (_.where($scope.model.disparities, { sortOrder: newValue })[0]) {
        var subject = _.where($scope.model.disparities, { sortOrder: newValue })[0].subject,
          auditDetail = _.where($scope.model.auditDetails, { subject: subject })[0],
          sortOrderIndex = auditDetail.disparities.indexOf(oldValue);
        auditDetail.disparities[sortOrderIndex] = newValue;
      }
    };

    for (var index = 0; index < $scope.model.disparities.length; index++) {
      var watchString = 'model.disparities[' + index + '].sortOrder';
      $scope.watchList.push($scope.$watch(watchString, $scope.changedSortOrder));
    }

  }

  AircraftInspectionCtrl.$inject = ['$scope', 'AuditDetails'];

  angular.module('gva').controller('AircraftInspectionCtrl', AircraftInspectionCtrl);
}(angular, _));
