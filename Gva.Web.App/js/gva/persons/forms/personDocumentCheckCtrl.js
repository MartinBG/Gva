/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentCheckCtrl($scope, namedModal, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.choosePublisher = function () {
      var modalInstance = namedModal.open('choosePublisher');

      modalInstance.result.then(function (publisherName) {
        $scope.model.documentPublisher = publisherName;
      });

      return modalInstance.opened;
    };
  }

  PersonDocumentCheckCtrl.$inject = ['$scope', 'namedModal', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentCheckCtrl', PersonDocumentCheckCtrl);
}(angular));
