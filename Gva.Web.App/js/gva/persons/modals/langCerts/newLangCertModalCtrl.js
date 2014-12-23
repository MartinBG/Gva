/*global angular*/
(function (angular) {
  'use strict';

  function NewLangCertModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentLangCerts,
    scModalParams,
    newLangCert,
    langLevelModel
  ) {
    $scope.form = {};
    $scope.newLangCert = newLangCert;
    $scope.langLevelModel = langLevelModel;
    $scope.caseTypeId = scModalParams.caseTypeId;
    $scope.appId = scModalParams.appId;
    $scope.lotId = scModalParams.lotId;
    $scope.withoutCertsAliases = scModalParams.withoutCertsAliases;

    $scope.save = function () {
      return $scope.form.newLangCertForm.$validate()
        .then(function () {
          if ($scope.form.newLangCertForm.$valid) {
            if (!$scope.newLangCert.part.langLevelEntries) {
              $scope.newLangCert.part.langLevelEntries = [];
            }

            $scope.newLangCert.part.langLevelEntries.push($scope.langLevelModel);

            return PersonDocumentLangCerts
              .save({ id: $scope.lotId }, $scope.newLangCert)
              .$promise
              .then(function (savedLangCert) {
                return $modalInstance.close(savedLangCert);
              });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewLangCertModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonDocumentLangCerts',
    'scModalParams',
    'newLangCert',
    'langLevelModel'
  ];

  NewLangCertModalCtrl.$resolve = {
    newLangCert: [
      'PersonDocumentLangCerts',
      'scModalParams',
      function (PersonDocumentLangCerts, scModalParams) {
        return PersonDocumentLangCerts.newLangCert({
          id: scModalParams.lotId
        }).$promise;
      }
    ],
    langLevelModel: [
      '$stateParams',
      'PersonDocumentLangCerts',
      function ($stateParams, PersonDocumentLangCerts) {
        return PersonDocumentLangCerts.newLangLevel({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('NewLangCertModalCtrl', NewLangCertModalCtrl);
}(angular));
