/*global angular*/
(function (angular) {
  'use strict';

  function AircraftInspectorCtrl($scope, Nomenclatures) {

    var deleteWatch = $scope.$watch('showInspectors', function (showInspectors) {
      if(showInspectors !== undefined || showInspectors !== null) {
        $scope.inspectorTypes = Nomenclatures.query({
            alias: 'inspectorTypes',
            showInspectors: $scope.showInspectors
          });
        deleteWatch();
      }
    });

  }

  AircraftInspectorCtrl.$inject = [
    '$scope',
    'Nomenclatures'
  ];

  angular.module('gva').controller('AircraftInspectorCtrl', AircraftInspectorCtrl);
}(angular));
