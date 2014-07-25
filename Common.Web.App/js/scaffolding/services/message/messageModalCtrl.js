/*global angular*/
(function (angular) {
  'use strict';

  function MessageModalCtrl(
    $scope,
    $modalInstance,
    l10nText,
    buttons) {
    $scope.l10nText = l10nText;
    $scope.buttons = buttons;

    $scope.close = function (result) {
      return $modalInstance.close(result);
    };
  }

  MessageModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'l10nText',
    'buttons'
  ];

  angular.module('scaffolding').controller('MessageModalCtrl', MessageModalCtrl);
}(angular));
