/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentOtherCtrl($scope, scModal) {
    $scope.choosePublisher = function () {
      var modalInstance = scModal.open('choosePublisher');

      modalInstance.result.then(function (publisherName) {
        $scope.model.documentPublisher = publisherName;
      });

      return modalInstance.opened;
    };
  }

  PersonDocumentOtherCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva').controller('PersonDocumentOtherCtrl', PersonDocumentOtherCtrl);
}(angular));
