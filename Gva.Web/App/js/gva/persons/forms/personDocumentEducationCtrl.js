/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentEducationCtrl($scope) {//, $stateParams, PersonDocumentEducation) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true: false);
    };
  }

  PersonDocumentEducationCtrl.$inject = ['$scope', '$stateParams', 'PersonDocumentEducation'];

  angular.module('gva').controller('PersonDocumentEducationCtrl', PersonDocumentEducationCtrl);
}(angular));
