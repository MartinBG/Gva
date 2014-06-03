/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertRegFMCtrl($scope, Nomenclature) {
    Nomenclature.query({alias: 'ownerTypes'})
      .$promise.then(function(ownerTypes){
        $scope.ownerTypes = ownerTypes;
      });
    $scope.ownerIsOrg = true;
    $scope.operIsOrg = true;
    $scope.lessorIsOrg = true;

  }

  AircraftCertRegFMCtrl.$inject = ['$scope', 'Nomenclature'];

  angular.module('gva').controller('AircraftCertRegFMCtrl', AircraftCertRegFMCtrl);
}(angular));
