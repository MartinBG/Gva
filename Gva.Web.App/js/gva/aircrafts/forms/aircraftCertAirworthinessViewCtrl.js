/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessViewCtrl($scope, $state, scFormParams) {
    $scope.aircraftId = scFormParams.lotId;

    $scope.validFromDate = function () {
      if (!$scope.model) {
        return null;
      }
      else if ($scope.model.reviews && $scope.model.reviews.length) {
        return $scope.model.reviews[$scope.model.reviews.length - 1].issueDate;
      }
      else if ($scope.model.form15Amendments && $scope.model.form15Amendments.amendment1) {
        var amendments = $scope.model.form15Amendments;
        return amendments.amendment2 ? amendments.amendment2.issueDate :
             amendments.amendment1 ? amendments.amendment1.issueDate : null;
      }
    };

    $scope.validToDate = function () {
      if (!$scope.model) {
        return null;
      }
      else if ($scope.model.reviews && $scope.model.reviews.length) {
        return $scope.model.reviews[$scope.model.reviews.length - 1].validToDate;
      }
      else if ($scope.model.form15Amendments && $scope.model.form15Amendments.amendment1) {
        var amendments = $scope.model.form15Amendments;
        return amendments.amendment2 ? amendments.amendment2.validToDate :
             amendments.amendment1 ? amendments.amendment1.validToDate : null;
      }
    };

    $scope.inspectorName = function () {
      var inspectorModel = null;
      if (!$scope.model) {
        return null;
      }
      else if ($scope.model.reviews && $scope.model.reviews.length) {
        var lastReview = $scope.model.reviews[$scope.model.reviews.length - 1];
        inspectorModel = lastReview.inspector.inspector ||
          lastReview.inspector.examiner ||
          lastReview.inspector.other;
      }
      else if ($scope.model.form15Amendments && $scope.model.form15Amendments.amendment1) {
        var amendments = $scope.model.form15Amendments;
        if (amendments.amendment2 && amendments.amendment2.inspector) {
          inspectorModel = amendments.amendment2.inspector.inspector ||
            amendments.amendment2.inspector.examiner || 
            amendments.amendment2.inspector.other;
        } else if (amendments.amendment1.inspector) {
          inspectorModel = amendments.amendment1.inspector.inspector ||
            amendments.amendment1.inspector.examiner ||
            amendments.amendment1.inspector.other;
        }
      }
      if (inspectorModel) {
        return inspectorModel.name || inspectorModel;
      }
    };

    $scope.newAw = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.new');
    };
  }

  AircraftCertAirworthinessViewCtrl.$inject = ['$scope', '$state', 'scFormParams'];

  angular.module('gva').controller(
    'AircraftCertAirworthinessViewCtrl',
    AircraftCertAirworthinessViewCtrl
    );
}(angular));
