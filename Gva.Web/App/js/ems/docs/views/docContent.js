/*global angular*/
(function (angular) {
  'use strict';

  function DocContentCtrl(
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

  DocContentCtrl.$inject = [
    //'$q',
    '$scope'
    //'$filter',
    //'$state',
    //'$stateParams',
    //'Doc'
  ];

  angular.module('ems').controller('DocContentCtrl', DocContentCtrl);
}(angular));
