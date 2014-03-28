/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsEditAddPartCtrl(
    $scope,
    $state,
    $stateParams,
    Application,
    application,
    applicationCommonData,
    selectedPublisher
    ) {
    if (_.isEmpty(applicationCommonData)) {
      return $state.go('^');
    }
    else {
      $scope.applicationDocPart = {
        isLinkNew: applicationCommonData.isLinkNew,
        currentDocId: applicationCommonData.currentDocId,
        setPartAlias: applicationCommonData.setPartAlias,
        part: {},
        appFile: {
          lotId: application.lotId,
          name: applicationCommonData.name,
          docFileId: applicationCommonData.docFileId,
          docFileKindId: applicationCommonData.docFileKindId,
          docFileTypeId: applicationCommonData.docFileTypeId,
          docFile: null
        }
      };

      if (applicationCommonData.isLinkNew) {
        $scope.applicationDocPart.appFile.docFile = {
          key: applicationCommonData.docFileKey,
          name: applicationCommonData.docFileName,
          relativePath: null
        };
      }
    }

    $scope.applicationDocPart.part.documentPublisher = selectedPublisher.pop() ||
      $scope.applicationDocPart.part.documentPublisher;

    $scope.cancel = function () {
      return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
    };

    $scope.addPart = function () {
      $scope.addFormWrapper.$validate()
        .then(function () {
          if ($scope.addFormWrapper.$valid) {
            var newPart = {
              docId: $scope.applicationDocPart.currentDocId,
              appFile: $scope.applicationDocPart.appFile,
              setPartAlias: $scope.applicationDocPart.setPartAlias,
              appPart: $scope.applicationDocPart.part
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
      $scope.addFormWrapper.$validate()
        .then(function () {
          if ($scope.addFormWrapper.$valid) {
            var linkNew = {
              appFile: $scope.applicationDocPart.appFile,
              setPartAlias: $scope.applicationDocPart.setPartAlias,
              appPart: $scope.applicationDocPart.part
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
    'application',
    'applicationCommonData',
    'selectedPublisher'
  ];

  ApplicationsEditAddPartCtrl.$resolve = {

    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular, _));
