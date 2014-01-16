/*global angular*/
(function (angular) {
  'use strict';

  function DocsContentCtrl(
    //$q,
    $scope
    //$filter,
    //$state,
    //$stateParams,
    //Doc
  ) {

    $scope.viewFile = function () {
    };

    $scope.editFile = function () {
    };

    $scope.detachFile = function () {
    };

    $scope.attachPrivateFile = function () {
    };

    $scope.createPrivateFile = function () {
    };

  }

  DocsContentCtrl.$inject = [
    //'$q',
    '$scope'
    //'$filter',
    //'$state',
    //'$stateParams',
    //'Doc'
  ];

  angular.module('ems').controller('DocsContentCtrl', DocsContentCtrl);
}(angular));
