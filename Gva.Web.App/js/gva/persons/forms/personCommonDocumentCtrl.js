/*global angular*/
(function (angular) {
  'use strict';

  function PersonCommonDocCtrl($scope, scModal, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.categoryAlias = scFormParams.categoryAlias;

    $scope.choosePublisher = function () {
      var modalInstance = scModal.open('choosePublisher');

      modalInstance.result.then(function (publisherName) {
        $scope.model.part.documentPublisher = publisherName;
      });

      return modalInstance.opened;
    };
  }

  PersonCommonDocCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

  angular.module('gva').controller('PersonCommonDocCtrl', PersonCommonDocCtrl);
}(angular));
