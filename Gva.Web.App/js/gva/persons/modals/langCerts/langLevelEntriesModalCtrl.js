/*global angular*/
(function (angular) {
  'use strict';

  function LangLevelEntriesModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentLangCerts,
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

          return PersonDocumentLangCerts.save({
            id: scModalParams.lotId,
            ind: $scope.langCert.partIndex
          },
          $scope.langCert).$promise.then(function () {
           return $modalInstance.close();
          });
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
    'PersonDocumentLangCerts',
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