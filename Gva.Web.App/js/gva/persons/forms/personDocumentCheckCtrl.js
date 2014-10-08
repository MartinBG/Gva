/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentCheckCtrl($scope, scModal, scFormParams) {
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

  PersonDocumentCheckCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentCheckCtrl', PersonDocumentCheckCtrl);
}(angular));
