/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditNewDocFileCtrl(
    $scope,
    $state,
    $stateParams,
    Applications
    ) {

    $scope.files = {};

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.save = function () {
      if (!$scope.files.docFiles) {
        return $state.transitionTo('root.applications.edit.case', {
          id: $stateParams.id
        });
      }

      return $scope.newDocFile.$validate().then(function () {
        if ($scope.newDocFile.$valid) {
          return Applications
            .attachDocFile({
              id: $stateParams.id,
              docId: $stateParams.docId
            }, $scope.files.docFiles)
            .$promise
            .then(function () {
              return $state.transitionTo('root.applications.edit.case', {
                id: $stateParams.id
              }, { reload: true });
            });
        }
      });
    };
  }

  ApplicationsEditNewDocFileCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Applications'
  ];

  angular.module('gva')
    .controller('ApplicationsEditNewDocFileCtrl', ApplicationsEditNewDocFileCtrl);
}(angular));
