/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessViewCtrl($scope, $state, $stateParams) {
    $scope.aircraftId = $stateParams.id;

    $scope.validFromDate = function () {
      if (!$scope.model || !$scope.model.reviews || !$scope.model.reviews.length) {
        return null;
      }

      var lastReview = $scope.model.reviews[$scope.model.reviews.length - 1];

      return lastReview.amendment2 ? lastReview.amendment2.issueDate :
             lastReview.amendment1 ? lastReview.amendment1.issueDate :
             lastReview.issueDate;
    };

    $scope.validToDate = function () {
      if (!$scope.model || !$scope.model.reviews || !$scope.model.reviews.length) {
        return null;
      }

      var lastReview = $scope.model.reviews[$scope.model.reviews.length - 1];

      return lastReview.amendment2 ? lastReview.amendment2.validToDate :
             lastReview.amendment1 ? lastReview.amendment1.validToDate :
             lastReview.validToDate;
    };

    $scope.inspectorName = function () {
      if (!$scope.model || !$scope.model.inspector || !$scope.model.inspector.inspectorType) {
        return null;
      }

      switch ($scope.model.inspector.inspectorType.alias) {
        case 'gvaInspector':
          return $scope.model.inspector.gvaInspector && $scope.model.inspector.gvaInspector.name;
        case 'examiner':
          return $scope.model.inspector.examiner && $scope.model.inspector.examiner.name;
        case 'other':
          return $scope.model.inspector.other;
      }
    };

    $scope.newAw = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.new');
    };
  }

  AircraftCertAirworthinessViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams'
  ];

  angular.module('gva').controller(
    'AircraftCertAirworthinessViewCtrl',
    AircraftCertAirworthinessViewCtrl
    );
}(angular));
