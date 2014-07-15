/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentOtherCtrl($scope, namedModal) {
    $scope.choosePublisher = function () {
      var modalInstance = namedModal.open('choosePublisher');

      modalInstance.result.then(function (publisherName) {
        $scope.model.documentPublisher = publisherName;
      });

      return modalInstance.opened;
    };
  }

  PersonDocumentOtherCtrl.$inject = ['$scope', 'namedModal'];

  angular.module('gva').controller('PersonDocumentOtherCtrl', PersonDocumentOtherCtrl);
}(angular));
