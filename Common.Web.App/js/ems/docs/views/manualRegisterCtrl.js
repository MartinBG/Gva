/*global angular*/
(function (angular) {
  'use strict';

  function ManualRegisterCtrl(
    $scope,
    $state,
    $stateParams,
    Doc,
    doc
  ) {
    $scope.save = function () {
      return $scope.manualRegisterForm.$validate().then(function () {
        if ($scope.manualRegisterForm.$valid) {
          return Doc
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
    'Doc',
    'doc'
  ];

  angular.module('ems').controller('ManualRegisterCtrl', ManualRegisterCtrl);
}(angular));
