/*global angular*/
(function (angular) {
  'use strict';

  function PersonFlyingExperienceCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
  }

  PersonFlyingExperienceCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonFlyingExperienceCtrl', PersonFlyingExperienceCtrl);
}(angular));
