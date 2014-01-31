/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentTrainingCtrl($scope) {//, $stateParams, PersonDocumentTraining) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true : false);
    };
  }

  PersonDocumentTrainingCtrl.$inject = ['$scope', '$stateParams', 'PersonDocumentTraining'];

  angular.module('gva').controller('PersonDocumentTrainingCtrl', PersonDocumentTrainingCtrl);
}(angular));
