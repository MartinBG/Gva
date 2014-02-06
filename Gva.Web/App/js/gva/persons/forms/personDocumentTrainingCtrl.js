/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentTrainingCtrl($scope) {
    $scope.isPositive = function (value) {
      if (value === null || value === undefined) {
        return true;
      }

      return (value >= 0 ? true : false);
    };
  }

  PersonDocumentTrainingCtrl.$inject = ['$scope', '$stateParams', 'PersonDocumentTraining'];

  angular.module('gva').controller('PersonDocumentTrainingCtrl', PersonDocumentTrainingCtrl);
}(angular));
