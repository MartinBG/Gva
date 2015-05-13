/*global angular*/
(function (angular) {
  'use strict';

  function AircraftDocumentDebtCloseCtrl($scope) {
    $scope.$watch('model.inspector', function (inspectorModel) {
      if (!inspectorModel) {
        return;
      }

      if (inspectorModel.inspector) {
        $scope.inspectorType = 'inspector';
      } else if (inspectorModel.other) {
        $scope.inspectorType = 'other';
      }
    });
  }

  AircraftDocumentDebtCloseCtrl.$inject = ['$scope'];

  angular.module('gva').controller('AircraftDocumentDebtCloseCtrl', AircraftDocumentDebtCloseCtrl);
}(angular));
