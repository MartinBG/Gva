﻿/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentLangCertsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentLangCerts,
    personDocumentLangCert,
    scMessage
  ) {
    var originalLangCert = _.cloneDeep(personDocumentLangCert);

    $scope.personDocumentLangCert = personDocumentLangCert;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentLangCert = _.cloneDeep(originalLangCert);
    };

    $scope.save = function () {
      return $scope.editDocumentLangCertForm.$validate()
        .then(function () {
          if ($scope.editDocumentLangCertForm.$valid) {
            return PersonDocumentLangCerts
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentLangCert)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentLangCerts.search');
              });
          }
        });
    };

    $scope.deleteLangCert = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonDocumentLangCerts.remove({
            id: $stateParams.id,
            ind: personDocumentLangCert.partIndex
          }).$promise.then(function () {
            return $state.go('root.persons.view.documentLangCerts.search');
          });
        }
      });
    };
  }

  DocumentLangCertsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentLangCerts',
    'personDocumentLangCert',
    'scMessage'
  ];

  DocumentLangCertsEditCtrl.$resolve = {
    personDocumentLangCert: [
      '$stateParams',
      'PersonDocumentLangCerts',
      function ($stateParams, PersonDocumentLangCerts) {
        return PersonDocumentLangCerts.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentLangCertsEditCtrl', DocumentLangCertsEditCtrl);
}(angular));
