/*global angular*/
(function (angular) {
  'use strict';

  function DocumentLangCertsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentLangCerts,
    personDocumentLangCert,
    langLevelModel
  ) {
    $scope.personDocumentLangCert = personDocumentLangCert;
    $scope.lotId = $stateParams.id;
    $scope.appId = $stateParams.appId;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.langLevelModel = langLevelModel;

    $scope.save = function () {
      return $scope.newDocumentLangCertForm.$validate()
        .then(function () {
          if ($scope.newDocumentLangCertForm.$valid) {
            if (!$scope.personDocumentLangCert.part.langLevelEntries) {
              $scope.personDocumentLangCert.part.langLevelEntries = [];
            }
            $scope.personDocumentLangCert.part.langLevel = $scope.langLevelModel.langLevel;

            return PersonDocumentLangCerts
              .save({ id: $stateParams.id }, $scope.personDocumentLangCert)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentLangCerts.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentLangCerts.search');
    };
  }

  DocumentLangCertsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentLangCerts',
    'personDocumentLangCert',
    'langLevelModel'
  ];

  DocumentLangCertsNewCtrl.$resolve = {
    personDocumentLangCert: [
      '$stateParams',
      'PersonDocumentLangCerts',
      function ($stateParams, PersonDocumentLangCerts) {
        return PersonDocumentLangCerts.newLangCert({
          id: $stateParams.id
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

  angular.module('gva').controller('DocumentLangCertsNewCtrl', DocumentLangCertsNewCtrl);
}(angular));
