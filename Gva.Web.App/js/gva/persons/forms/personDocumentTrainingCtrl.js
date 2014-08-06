/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentTrainingCtrl($scope, scModal, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.choosePublisher = function () {
      var modalInstance = scModal.open('choosePublisher');

      modalInstance.result.then(function (publisherName) {
        $scope.model.documentPublisher = publisherName;
      });

      return modalInstance.opened;
    };
  }

  PersonDocumentTrainingCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentTrainingCtrl', PersonDocumentTrainingCtrl);
}(angular));
