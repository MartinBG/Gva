/*global angular*/
(function (angular) {
  'use strict';

  function AircraftAirworthinessReviewCtrl($scope, $attrs) {

    $attrs.$observe('showAmendmentType', function (val) {
       $scope.showAmendmentType = val;
    });
  }

  AircraftAirworthinessReviewCtrl.$inject = [
    '$scope',
    '$attrs'
  ];

  angular.module('gva').controller(
    'AircraftAirworthinessReviewCtrl',
    AircraftAirworthinessReviewCtrl
    );
}(angular));
