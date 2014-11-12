/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditAddPartCtrl(
    $scope,
    $state,
    $stateParams,
    l10n,
    AplicationsCase,
    applicationPart,
    docPartType,
    application,
    person
    ) {
    $scope.applicationPart = applicationPart;
    $scope.title = l10n.get('applications.edit.addPart.title') + ': ' + docPartType.name;
    $scope.setPartAlias = $stateParams.setPartAlias;
    $scope.lotId = application.lotId;
    $scope.caseReadonly = !!$stateParams.docFileId;

    if ($stateParams.setPartAlias === 'personMedical') {
      $scope.personLin = person.lin;
    }

    $scope.cancel = function () {
      return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
    };

    $scope.linkNew = function () {
      return $scope.addFormWrapper.$validate().then(function () {
        if ($scope.addFormWrapper.$valid) {
          return AplicationsCase.linkNewPart({
            id: $stateParams.id,
            docId: $stateParams.docId,
            setPartAlias: $scope.setPartAlias
          }, $scope.applicationPart)
          .$promise.then(function () {
            return $state.transitionTo('root.applications.edit.case',
              $stateParams, { reload: true });
          });
        }
      });
    };
  }

  ApplicationsEditAddPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'l10n',
    'AplicationsCase',
    'applicationPart',
    'docPartType',
    'application',
    'person'
  ];

  ApplicationsEditAddPartCtrl.$resolve = {
    applicationPart: [
      '$stateParams',
      'AplicationsCase',
      function ($stateParams, AplicationsCase) {
        return AplicationsCase.newPart({
          lotId: $stateParams.lotId,
          setPartAlias: $stateParams.setPartAlias,
          docId: $stateParams.docId,
          docFileId: $stateParams.docFileId,
          caseTypeId: $stateParams.caseTypeId
        }).$promise;
      }
    ],
    docPartType: [
      '$stateParams',
      'Nomenclatures',
      function ($stateParams, Nomenclatures) {
        return Nomenclatures.get({
          alias: 'documentParts',
          valueAlias: $stateParams.setPartAlias
        }).$promise;
      }
    ],
    person: [
      '$stateParams',
      'Persons',
      function ($stateParams, Persons) {
        if ($stateParams.setPartAlias === 'personMedical') {
          return Persons.get({ id: $stateParams.lotId }).$promise;
        } else {
          return null;
        }
      }
    ]
  };

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular));
