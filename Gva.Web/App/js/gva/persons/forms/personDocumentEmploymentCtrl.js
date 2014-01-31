/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentEmploymentCtrl($scope) {//, $stateParams, PersonDocumentEmployment) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true: false);
    };
  }

  PersonDocumentEmploymentCtrl.$inject = ['$scope', '$stateParams', 'PersonDocumentEmployment'];

  angular.module('gva').controller('PersonDocumentEmploymentCtrl', PersonDocumentEmploymentCtrl);
}(angular));
