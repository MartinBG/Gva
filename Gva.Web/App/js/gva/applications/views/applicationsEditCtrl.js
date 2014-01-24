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

    $scope.newfile = function () {
      
      return $state.go('applications/edit/newfile');
    };

    $scope.addpart = function () {
      return $state.go('applications/edit/addpart');
    };

    $scope.linkpart = function () {
      return $state.go('applications/edit/linkpart');
    };

    $scope.viewDoc = function (docId) {
      return $state.go('docs/edit/addressing', { docId: docId });
    };

    $scope.docFileType = null;

    $scope.personView = function (id) {
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
