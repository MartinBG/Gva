/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentCheckCtrl($scope, namedModal) {
    $scope.choosePublisher = function () {
      var modalInstance = namedModal.open('choosePublisher');

      modalInstance.result.then(function (publisherName) {
        $scope.model.documentPublisher = publisherName;
      });

      return modalInstance.opened;
    };
  }

  PersonDocumentCheckCtrl.$inject = ['$scope', 'namedModal'];

  angular.module('gva').controller('PersonDocumentCheckCtrl', PersonDocumentCheckCtrl);
}(angular));
