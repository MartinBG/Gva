/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditCtrl(
    $stateParams,
    $state,
    $scope,
    Application
    ) {
    $scope.application = Application.get({ id: $stateParams.id });

    $scope.docFileType = null;
    $scope.docFileId = null;
    $scope.currentDocId = null;
    $scope.isLinkNew = false;

    $scope.viewPerson = function (id) {
      return $state.go('persons.view', { id: id });
    };
  }

  ApplicationsEditCtrl.$inject = [
    '$stateParams',
    '$state',
    '$scope',
    'Application'
  ];

  angular.module('gva').controller('ApplicationsEditCtrl', ApplicationsEditCtrl);
}(angular
));
