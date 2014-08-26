/*global angular*/
(function (angular) {
  'use strict';
  function OrganizationDataCtrl($scope) {
    $scope.requireCaseTypes = function (caseTypes) {
      return caseTypes.length > 0;
    };
  }

  angular.module('gva')
    .controller('OrganizationDataCtrl', OrganizationDataCtrl);
}(angular));