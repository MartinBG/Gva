/*global angular*/
(function (angular) {
  'use strict';

  function AircraftCertRegFMCtrl($scope, Nomenclature) {
    Nomenclature.query({alias: 'ownerTypes'})
      .$promise.then(function(ownerTypes){
        $scope.ownerTypes = ownerTypes;
      });

    $scope.setOwnerType = function (item) {
      $scope.model.ownerType = item;
    };

    $scope.setOperType = function (item) {
      $scope.model.operType = item;
    };

    $scope.setLessorType = function (item) {
      $scope.model.lessorType = item;
    };

  }

  AircraftCertRegFMCtrl.$inject = ['$scope', 'Nomenclature'];

  angular.module('gva').controller('AircraftCertRegFMCtrl', AircraftCertRegFMCtrl);
}(angular));
