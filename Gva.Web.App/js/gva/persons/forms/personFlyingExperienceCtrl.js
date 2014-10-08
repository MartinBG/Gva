/*global angular*/
(function (angular) {
  'use strict';

  function PersonFlyingExperienceCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
  }

  PersonFlyingExperienceCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonFlyingExperienceCtrl', PersonFlyingExperienceCtrl);
}(angular));
