/*global angular*/
(function (angular) {
  'use strict';

  function ScSelectCtrl($scope) {
    $scope.select = '';

    $scope.options = [
      { id: 1, text: 'option1' },
      { id: 2, text: 'option2' },
      { id: 3, text: 'option3' },
      { id: 4, text: 'option4' }
    ];

    $scope.change = function () {
      if ($scope.select) {
        $scope.select = $scope.select.id;
      }
      else {
        $scope.select = '';
      }
    };
  }

  ScSelectCtrl.$inject = ['$scope'];

  angular.module('scaffolding').controller('ScSelectTestbedCtrl', ScSelectCtrl);
}(angular));
