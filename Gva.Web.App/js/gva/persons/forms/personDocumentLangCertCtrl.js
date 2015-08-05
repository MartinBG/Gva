/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentLangCertCtrl($scope, scFormParams, scModal) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
    $scope.lotId = scFormParams.lotId;
    $scope.appId = scFormParams.appId;
    $scope.withoutCertsAliases = scFormParams.withoutCertsAliases;
    $scope.langLevelHistory = scFormParams.langLevelHistory;

    $scope.viewLangLevels = function () {
      var params = {
        langCert: $scope.model,
        lotId: $scope.lotId,
        langLevelHistory: $scope.langLevelHistory
      };

      var modalInstance = scModal.open('langLevelEntries', params);
      modalInstance.result.then(function (langLevelHistory) {
        $scope.langLevelHistory = langLevelHistory;
      });
      return modalInstance.opened;
    };

  }

  PersonDocumentLangCertCtrl.$inject = ['$scope', 'scFormParams', 'scModal'];

  angular.module('gva').controller('PersonDocumentLangCertCtrl', PersonDocumentLangCertCtrl);
}(angular));
