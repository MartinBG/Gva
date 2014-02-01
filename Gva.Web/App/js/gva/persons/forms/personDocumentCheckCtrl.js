/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentCheckCtrl($scope) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true: false);
    };
  }

  PersonDocumentCheckCtrl.$inject = ['$scope'];

  angular.module('gva').controller('PersonDocumentCheckCtrl', PersonDocumentCheckCtrl);
}(angular));
