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
              appFile: $scope.applicationPart.appFile,
              appPart: $scope.applicationPart.part
            };

            return Application
              .createPart({
                id: $stateParams.id,
                docId: $scope.applicationPart.docId,
                setPartAlias: $scope.applicationPart.setPartAlias
              }, newPart)
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
              appPart: $scope.applicationPart.part
            };

            return Application
              .linkNewPart({
                id: $stateParams.id,
                setPartAlias: $scope.applicationPart.setPartAlias
              }, linkNew)
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
      '$q',
      '$stateParams',
      'Application',
      'application',
      function ($q, $stateParams, Application, application) {
        var docFile = $q.defer(),
            doc = $q.defer();

        if ($stateParams.setPartAlias === 'application' && !!$stateParams.docId) {
          doc = Application.getDoc({ docId: $stateParams.docId });
        }
        if (!!$stateParams.docFileId) {
          docFile = Application.getDocFile({ docFileId: $stateParams.docFileId });
        }

        return $q.all({
          doc: doc.$promise,
          docFile: docFile.$promise
        }).then(function (res) {
          var part = {};
          res.docFile = res.docFile || {};
          res.docFile.lotId = application.lotId;

          if ($stateParams.setPartAlias === 'application') {
            part.documentNumber = res.doc.documentNumber;
            //applicationType = docType?
          }

          return {
            docId: $stateParams.docId,
            hasDocFile: !!$stateParams.docFileId,
            setPartAlias: $stateParams.setPartAlias,
            appFile: res.docFile,
            part: part
          };
        });
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular));
