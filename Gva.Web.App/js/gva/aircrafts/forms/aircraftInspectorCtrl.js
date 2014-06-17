/*global angular*/
(function (angular) {
  'use strict';

  function AircraftInspectorCtrl($scope, $attrs) {

    $scope.showInspectors = $attrs.showInspectors;

    $attrs.$observe('readonly', function (val) {
      $scope.isReadonly = val;
    });
  }

  AircraftInspectorCtrl.$inject = [
    '$scope',
    '$attrs'
  ];

  angular.module('gva').controller(
    'AircraftInspectorCtrl',
    AircraftInspectorCtrl
    );
}(angular));
