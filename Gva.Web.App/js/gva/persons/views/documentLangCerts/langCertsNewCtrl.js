/*global angular*/
(function (angular) {
  'use strict';

  function DocumentLangCertsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentLangCerts,
    personDocumentLangCert
  ) {
    $scope.personDocumentLangCert = personDocumentLangCert;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newDocumentLangCertForm.$validate()
        .then(function () {
          if ($scope.newDocumentLangCertForm.$valid) {
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
    'personDocumentLangCert'
  ];

  DocumentLangCertsNewCtrl.$resolve = {
    personDocumentLangCert: [
      '$stateParams',
      'PersonDocumentLangCerts',
      function ($stateParams, PersonDocumentLangCerts) {
        return PersonDocumentLangCerts.newLangCert({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentLangCertsNewCtrl', DocumentLangCertsNewCtrl);
}(angular));
