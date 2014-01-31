/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentIdCtrl($scope) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true : false);
    };
  }

  PersonDocumentIdCtrl.$inject = ['$scope'];

  angular.module('gva').controller('PersonDocumentIdCtrl', PersonDocumentIdCtrl);
}(angular));
