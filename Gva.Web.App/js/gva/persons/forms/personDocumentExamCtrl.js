/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentExamCtrl($scope, scModal, scFormParams) {
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

  PersonDocumentExamCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

  angular.module('gva').controller('PersonDocumentExamCtrl', PersonDocumentExamCtrl);
}(angular));
