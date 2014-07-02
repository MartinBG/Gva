/*global angular*/
(function (angular) {
  'use strict';

  function ManualRegisterCtrl(
    $scope,
    $state,
    $stateParams,
    Docs,
    doc
  ) {
    $scope.save = function () {
      return $scope.manualRegisterForm.$validate().then(function () {
        if ($scope.manualRegisterForm.$valid) {
          return Docs
            .manualRegister({
              id: doc.docId,
              docVersion: doc.version,
              regUri: $scope.regUri,
              regDate: $scope.regDate
            }, {})
            .$promise
            .then(function () {
              return $state.go('root.docs.edit.view', { id: doc.docId }, { reload: true });
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  ManualRegisterCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Docs',
    'doc'
  ];

  angular.module('ems').controller('ManualRegisterCtrl', ManualRegisterCtrl);
}(angular));
