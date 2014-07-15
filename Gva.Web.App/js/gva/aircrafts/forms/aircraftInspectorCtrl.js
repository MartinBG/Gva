/*global angular*/
(function (angular) {
  'use strict';

  function AircraftInspectorCtrl($scope) {
    $scope.$watch('model', function (model) {
      if (!model) {
        return;
      }

      if (model.inspector) {
        $scope.inspectorType = 'inspector';
      } else if (model.examiner) {
        $scope.inspectorType = 'examiner';
      } else if (model.other) {
        $scope.inspectorType = 'other';
      }
    });
  }

  AircraftInspectorCtrl.$inject = [
    '$scope'
  ];

  angular.module('gva').controller('AircraftInspectorCtrl', AircraftInspectorCtrl);
}(angular));
