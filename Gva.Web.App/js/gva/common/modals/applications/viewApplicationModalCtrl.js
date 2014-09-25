/*global angular*/
(function (angular) {
  'use strict';

  function ViewApplicationModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    application) {
    $scope.application = application;
    $scope.setPart = scModalParams.setPart;
    $scope.lotId = scModalParams.lotId;

    $scope.toApplication = function () {
      return $modalInstance.close(true);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ViewApplicationModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'application'
  ];

  ViewApplicationModalCtrl.$resolve = {
    application: [
      'Applications',
      'scModalParams',
      function (Applications, scModalParams) {
        return Applications.getAppPart({
          lotId: scModalParams.lotId,
          path: scModalParams.path,
          ind: scModalParams.partIndex
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ViewApplicationModalCtrl', ViewApplicationModalCtrl);
}(angular));
