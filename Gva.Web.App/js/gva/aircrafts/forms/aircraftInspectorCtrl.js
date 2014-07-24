/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AircraftInspectorCtrl($scope) {
    $scope.inspectorTypesHash = {};

    $scope.$watch('inspectorTypes', function (inspectorTypes) {
      if (inspectorTypes) {
        _.each(inspectorTypes, function (it) {
          $scope.inspectorTypesHash[it] = true;
        });

        if (inspectorTypes.length === 1) {
          $scope.inspectorType = inspectorTypes[0];
        }
      } else {
        $scope.inspectorTypesHash.inspector = true;
        $scope.inspectorTypesHash.examiner = true;
        $scope.inspectorTypesHash.other = true;
      }
    });

    $scope.$watch('model', function (model) {
      if (!model) {
        return;
      }

      if (model.inspector) {
        $scope.inspectorType = 'inspector';
        $scope.inspectorTypesHash.inspector = true;
      } else if (model.examiner) {
        $scope.inspectorType = 'examiner';
        $scope.inspectorTypesHash.examiner = true;
      } else if (model.other) {
        $scope.inspectorType = 'other';
        $scope.inspectorTypesHash.other = true;
      }
    });

    $scope.$watch('inspectorType', function (inspectorType) {
      if (!$scope.model) {
        return;
      }

      switch (inspectorType) {
        case 'inspector':
          $scope.model.examiner = null;
          $scope.model.other = null;
          break;
        case 'examiner':
          $scope.model.inspector = null;
          $scope.model.other = null;
          break;
        case 'other':
          $scope.model.inspector = null;
          $scope.model.examiner = null;
          break;
      }
    });
  }

  AircraftInspectorCtrl.$inject = [
    '$scope'
  ];

  angular.module('gva').controller('AircraftInspectorCtrl', AircraftInspectorCtrl);
}(angular, _));
