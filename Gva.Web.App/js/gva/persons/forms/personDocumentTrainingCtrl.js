/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentTrainingCtrl($scope, scModal, scFormParams) {
    $scope.personLin = scFormParams.personLin;
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

  PersonDocumentTrainingCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentTrainingCtrl', PersonDocumentTrainingCtrl);
}(angular));
