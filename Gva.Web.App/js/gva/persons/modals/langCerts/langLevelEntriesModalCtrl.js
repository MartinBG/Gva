/*global angular*/
(function (angular) {
  'use strict';

  function LangLevelEntriesModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    langLevelModel
  ) {

    $scope.form = {};
    $scope.langLevelModel = langLevelModel;
    $scope.langCert = scModalParams.langCert;

    $scope.save = function () {
      return $scope.form.langLevelEntriesForm.$validate().then(function () {
        if ($scope.form.langLevelEntriesForm.$valid) {
          if (!$scope.langCert.part.langLevelEntries) {
            $scope.langCert.part.langLevelEntries = [];
          }

          $scope.langCert.part.langLevelEntries.push(langLevelModel);
          $scope.langCert.part.langLevel = langLevelModel.langLevel;

          return $modalInstance.close();
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

  }

  LangLevelEntriesModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'langLevelModel'
  ];

  LangLevelEntriesModalCtrl.$resolve = {
    langLevelModel: [
      'scModalParams',
      'PersonDocumentLangCerts',
      function (scModalParams, PersonDocumentLangCerts) {
        return PersonDocumentLangCerts.newLangLevel({
          id: scModalParams.lotId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('LangLevelEntriesModalCtrl', LangLevelEntriesModalCtrl);
}(angular));