/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditNewFileCtrl(
    $scope,
    $state,
    $stateParams,
    application,
    docPartType
    ) {
    $scope.lotSetId = application.lotSetId;
    $scope.lotId = application.lotId;
    $scope.docPartType = docPartType;
    $scope.chooseDocPartType = !docPartType;
    $scope.caseType = null;

    $scope.cancel = function () {
      return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
    };

    $scope.addPart = function () {
      return $scope.addDocPartType.$validate()
        .then(function () {
          if ($scope.addDocPartType.$valid) {
            return $state.go('root.applications.edit.case.addPart', {
              id: $stateParams.id,
              lotId: $scope.lotId,
              docId: $stateParams.docId,
              docFileId: $stateParams.docFileId,
              setPartAlias: $scope.docPartType.alias,
              caseTypeId: $scope.caseType.nomValueId
            });
          }
        });
    };
  }

  ApplicationsEditNewFileCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'application',
    'docPartType'
  ];

  ApplicationsEditNewFileCtrl.$resolve = {
    docPartType: [
      '$stateParams',
      'Nomenclatures',
      function ($stateParams, Nomenclatures) {
        if ($stateParams.setPartAlias) {
          return Nomenclatures.get({
            alias: 'documentParts',
            valueAlias: $stateParams.setPartAlias
          }).$promise;
        }
        else {
          return null;
        }
      }
    ]
  };

  angular.module('gva').controller('ApplicationsEditNewFileCtrl', ApplicationsEditNewFileCtrl);
}(angular));
