/*global angular*/
(function (angular) {
  'use strict';

  function InspDataCtrl($scope, Nomenclatures) {
    if (!$scope.model) {
      Nomenclatures.get({ alias: 'caa', valueAlias: 'BGR' })
        .$promise.then(function (caa) {
          $scope.model = {
            caaId: caa.nomValueId
          };
        });
    }
  }

  InspDataCtrl.$inject = ['$scope', 'Nomenclatures'];

  angular.module('gva').controller('InspDataCtrl', InspDataCtrl);
}(angular));
