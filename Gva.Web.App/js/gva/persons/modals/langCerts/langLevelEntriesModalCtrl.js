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
    $scope.langLevelHistory = scModalParams.langLevelHistory;
    $scope.save = function () {
      return $scope.form.langLevelEntriesForm.$validate().then(function () {
        if ($scope.form.langLevelEntriesForm.$valid) {
          $scope.langLevelHistory.push(langLevelModel);

          if (!$scope.langCert.part.langLevelEntries) {
            $scope.langCert.part.langLevelEntries = [];
          }

          $scope.langCert.part.langLevelEntries.push({
            langLevelId: langLevelModel.langLevel.nomValueId,
            changeDate: langLevelModel.changeDate
          });
          $scope.langCert.part.langLevelId = langLevelModel.langLevel.nomValueId;

          return $modalInstance.close($scope.langLevelHistory);
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