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
      } else {
        return $scope.model.issueDate;
      }
    };

    $scope.validToDate = function () {
      if (!$scope.model) {
        return null;
      }
      else if ($scope.model.reviews && $scope.model.reviews.length) {
        return $scope.model.reviews[$scope.model.reviews.length - 1].validToDate;
      } else {
        return $scope.model.validToDate;
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
