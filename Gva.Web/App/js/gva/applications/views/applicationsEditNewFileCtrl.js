/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditNewFileCtrl(
    $scope,
    $state
    ) {

    $scope.cancel = function () {
      $scope.$parent.docFileType = null;
      return $state.go('applications/edit/case');
    };
  }

  ApplicationsEditNewFileCtrl.$inject = [
    '$scope',
    '$state'
  ];

  angular.module('gva').controller('ApplicationsEditNewFileCtrl', ApplicationsEditNewFileCtrl);
}(angular
));
