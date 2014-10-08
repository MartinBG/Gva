﻿/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentOtherCtrl($scope, scModal, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;

    $scope.choosePublisher = function () {
      var modalInstance = scModal.open('choosePublisher');

      modalInstance.result.then(function (publisherName) {
        $scope.model.part.documentPublisher = publisherName;
      });

      return modalInstance.opened;
    };
  }

  PersonDocumentOtherCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentOtherCtrl', PersonDocumentOtherCtrl);
}(angular));
