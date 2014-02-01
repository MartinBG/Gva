/*global angular*/
(function (angular) {
  'use strict';

  function PersonFlyingExperienceCtrl($scope) {
    $scope.saveClicked = false;
  }

  PersonFlyingExperienceCtrl.$inject = ['$scope', '$stateParams', 'PersonFlyingExperience'];

  angular.module('gva').controller('PersonFlyingExperienceCtrl', PersonFlyingExperienceCtrl);
}(angular));