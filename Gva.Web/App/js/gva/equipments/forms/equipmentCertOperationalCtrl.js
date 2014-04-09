/*global angular*/
(function (angular) {
  'use strict';
  function EquipmentCertOperationalCtrl($scope) {

    $scope.deleteDoc= function (doc) {
      var index = $scope.model.includedDocuments.indexOf(doc);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.addDoc = function () {
      $scope.model.includedDocuments.push({});
    };
  }

  angular.module('gva').controller('EquipmentCertOperationalCtrl', EquipmentCertOperationalCtrl);
}(angular));
