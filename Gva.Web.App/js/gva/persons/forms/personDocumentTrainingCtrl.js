/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentTrainingCtrl($scope, namedModal, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.choosePublisher = function () {
      var modalInstance = namedModal.open('choosePublisher');

      modalInstance.result.then(function (publisherName) {
        $scope.model.documentPublisher = publisherName;
      });

      return modalInstance.opened;
    };
  }

  PersonDocumentTrainingCtrl.$inject = ['$scope', 'namedModal', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentTrainingCtrl', PersonDocumentTrainingCtrl);
}(angular));
