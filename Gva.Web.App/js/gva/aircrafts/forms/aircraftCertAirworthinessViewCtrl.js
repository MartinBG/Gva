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
