/*global angular*/
(function (angular) {
  'use strict';

  function DocumentLangCertsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    langCerts
  ) {
    $scope.documentLangCerts = langCerts;
  }

  DocumentLangCertsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'langCerts'
  ];

  DocumentLangCertsSearchCtrl.$resolve = {
    langCerts: [
      '$stateParams',
      'PersonDocumentLangCerts',
      function ($stateParams, PersonDocumentLangCerts) {
        return PersonDocumentLangCerts.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentLangCertsSearchCtrl', DocumentLangCertsSearchCtrl);
}(angular));
