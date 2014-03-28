/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditAddPartCtrl(
    $scope,
    $state,
    $stateParams,
    Application,
    applicationPart,
    selectedPublisher
    ) {
    $scope.applicationPart = applicationPart;
 
    $scope.applicationPart.part.documentPublisher = selectedPublisher.pop() ||
      $scope.applicationPart.part.documentPublisher;

    $scope.cancel = function () {
      return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
    };

    $scope.addPart = function () {
      return $scope.addFormWrapper.$validate()
        .then(function () {
          if ($scope.addFormWrapper.$valid) {
            var newPart = {
              docId: $scope.applicationPart.docId,
              appFile: $scope.applicationPart.appFile,
              setPartAlias: $scope.applicationPart.setPartAlias,
              appPart: $scope.applicationPart.part
            };

            return Application
              .createPart({ id: $stateParams.id }, newPart)
              .$promise.then(function () {
                return $state.transitionTo('root.applications.edit.case',
                  $stateParams, { reload: true });
              });
          }
        });
    };

    $scope.linkNew = function () {
      return $scope.addFormWrapper.$validate()
        .then(function () {
          if ($scope.addFormWrapper.$valid) {
            var linkNew = {
              appFile: $scope.applicationPart.appFile,
              setPartAlias: $scope.applicationPart.setPartAlias,
              appPart: $scope.applicationPart.part
            };

            return Application
                .linkNewPart({ id: $stateParams.id }, linkNew)
                .$promise.then(function () {
                  return $state.transitionTo('root.applications.edit.case',
                    $stateParams, { reload: true });
                });
          }
        });
    };

    $scope.choosePublisher = function () {
      return $state.go('root.applications.edit.case.addPart.choosePublisher');
    };
  }

  ApplicationsEditAddPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Application',
    'applicationPart',
    'selectedPublisher'
  ];

  ApplicationsEditAddPartCtrl.$resolve = {
    applicationPart: [
      '$stateParams',
      'Application',
      'application',
      function ($stateParams, Application, application) {
        if (!!$stateParams.docFileId) {
          return Application.getDocFile({ docFileId: $stateParams.docFileId }).$promise
            .then(function (docFile) {
              docFile.lotId = application.lotId;
              return {
                hasDocFile: !!$stateParams.docFileId,
                setPartAlias: $stateParams.setPartAlias,
                appFile: docFile,
                part: {}
              };
            });
        }

        return {
          docId: $stateParams.docId,
          hasDocFile: !!$stateParams.docFileId,
          setPartAlias: $stateParams.setPartAlias,
          appFile: {
            lotId: application.lotId
          },
          part: {}
        };
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular));
