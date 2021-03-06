﻿/*global angular,_*/
(function (angular, _) {
  'use strict';

  function DocumentLangCertsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentLangCerts,
    personDocumentLangCert,
    langLevelHistory,
    scMessage
  ) {
    var originalLangCert = _.cloneDeep(personDocumentLangCert);

    $scope.personDocumentLangCert = personDocumentLangCert;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;
    $scope.partIndex = $stateParams.ind;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.langLevelHistory = langLevelHistory;
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
            if ($scope.personDocumentLangCert.part.langLevelEntries.length > 0) {
              $scope.personDocumentLangCert.part.langLevelId = 
              _.last($scope.personDocumentLangCert.part.langLevelEntries).langLevelId;
            }

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
            ind: $stateParams.ind
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
    'langLevelHistory',
    'scMessage'
  ];

  DocumentLangCertsEditCtrl.$resolve = {
    personDocumentLangCert: [
      '$stateParams',
      'PersonDocumentLangCerts',
      function ($stateParams, PersonDocumentLangCerts) {
        return PersonDocumentLangCerts.get($stateParams).$promise;
      }
    ],
    langLevelHistory: [
      '$stateParams',
      'PersonDocumentLangCerts',
      function ($stateParams, PersonDocumentLangCerts) {
        return  PersonDocumentLangCerts.getLangLevelHistory($stateParams).$promise;
      }
    ]
  };
  angular.module('gva').controller('DocumentLangCertsEditCtrl', DocumentLangCertsEditCtrl);
}(angular, _));
