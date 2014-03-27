/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsEditNewFileCtrl(
    $scope,
    $state,
    $stateParams,
    applicationCommonData
    ) {
    if (_.isEmpty(applicationCommonData)) {
      return $state.go('^');
    }

    $scope.docPartType = null;

    $scope.cancel = function () {
      return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
    };

    $scope.addPart = function () {
      $scope.addDocPartType.$validate()
        .then(function () {
          if ($scope.addDocPartType.$valid) {
            applicationCommonData.setPartAlias = $scope.docPartType.alias;

            return $state.go('root.applications.edit.case.addPart');
          }
        });
    };
  }

  ApplicationsEditNewFileCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'applicationCommonData'
  ];

  angular.module('gva').controller('ApplicationsEditNewFileCtrl', ApplicationsEditNewFileCtrl);
}(angular, _));
