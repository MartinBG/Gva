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
      'application',
      'AplicationsCase',
      function ($stateParams, application, AplicationsCase) {
        return AplicationsCase.newPart({
          lotId: application.lotId,
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
      'application',
      'Persons',
      function ($stateParams, application, Persons) {
        if ($stateParams.setPartAlias === 'personMedical') {
          return Persons.get({ id: application.lotId }).$promise;
        } else {
          return null;
        }
      }
    ]
  };

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular));
