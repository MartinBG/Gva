/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditNewDocFileCtrl(
    $scope,
    $state,
    $stateParams,
    AplicationsCase,
    docFile
    ) {
    $scope.file = docFile;

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.save = function () {
      return $scope.newDocFile.$validate().then(function () {
        if ($scope.newDocFile.$valid) {
          return AplicationsCase.attachDocFile({
              docId: $stateParams.docId
            }, $scope.file)
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
    'AplicationsCase',
    'docFile'
  ];

  ApplicationsEditNewDocFileCtrl.$resolve = {
    docFile: [
      '$stateParams',
      'AplicationsCase',
      function ($stateParams, AplicationsCase) {
        return AplicationsCase.newDocFile({ docId: $stateParams.docId }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('ApplicationsEditNewDocFileCtrl', ApplicationsEditNewDocFileCtrl);
}(angular));
