/*global angular*/
(function (angular) {
  'use strict';

  function InspDataCtrl($scope, Nomenclature) {
    if (!$scope.model) {
      Nomenclature.get({ alias: 'caa', valueAlias: 'BG' }).$promise.then(function (caa) {
        $scope.model = {
          caa: caa
        };
      });
    }
  }

  InspDataCtrl.$inject = ['$scope', 'Nomenclature'];

  angular.module('gva').controller('InspDataCtrl', InspDataCtrl);
}(angular));
