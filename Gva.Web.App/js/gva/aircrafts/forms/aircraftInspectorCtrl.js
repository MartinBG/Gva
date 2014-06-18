/*global angular*/
(function (angular) {
  'use strict';

  function AircraftInspectorCtrl($scope, Nomenclature) {

    var deleteWatch = $scope.$watch('showInspectors', function (showInspectors) {
      if(showInspectors !== undefined || showInspectors !== null) {
        $scope.inspectorTypes = Nomenclature.query({
            alias: 'inspectorTypes',
            showInspectors: $scope.showInspectors
          });
        deleteWatch();
      }
    });

  }

  AircraftInspectorCtrl.$inject = [
    '$scope',
    'Nomenclature'
  ];

  angular.module('gva').controller('AircraftInspectorCtrl', AircraftInspectorCtrl);
}(angular));
