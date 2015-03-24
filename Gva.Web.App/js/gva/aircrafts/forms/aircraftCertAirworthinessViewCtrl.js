/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertAirworthinessViewCtrl($scope, $state, scFormParams) {
    $scope.aircraftId = scFormParams.lotId;

    if($scope.model) {
      if ($scope.model.reviews && $scope.model.reviews.length) {
        var lastReview = $scope.model.reviews[$scope.model.reviews.length - 1];
        $scope.inspectorModel = lastReview.inspector;
        $scope.validFromDate = lastReview.issueDate;
        $scope.validToDate = lastReview.validToDate;
      } else {
        $scope.inspectorModel = $scope.model.inspector;
        $scope.validFromDate = $scope.model.issueDate;
        $scope.validToDate = $scope.model.validToDate;
      }
    }

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
