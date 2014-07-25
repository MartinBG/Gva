/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsEditAddPartCtrl(
    $scope,
    $state,
    $stateParams,
    Applications,
    applicationPart,
    person,
    selectedPublisher
    ) {
    $scope.applicationPart = applicationPart;

    if ($stateParams.setPartAlias === 'personMedical') {
      $scope.personLin = person.lin;
    }
    
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

            return Applications
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

            return Applications
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
    'Applications',
    'applicationPart',
    'person',
    'selectedPublisher'
  ];

  ApplicationsEditAddPartCtrl.$resolve = {
    applicationPart: [
      '$q',
      '$stateParams',
      'Nomenclatures',
      'l10n',
      'Applications',
      'application',
      function ($q, $stateParams, Nomenclatures, l10n, Applications, application) {
        var docFile, doc, docValues;

        if (($stateParams.setPartAlias === 'personApplication' ||
          $stateParams.setPartAlias === 'organizationApplication' ||
          $stateParams.setPartAlias === 'aircraftApplication' ||
          $stateParams.setPartAlias === 'airportApplication' ||
          $stateParams.setPartAlias === 'equipmentApplication') &&
          !!$stateParams.docId
          ) {
          doc = Applications.getDoc({ docId: $stateParams.docId });
        }
        if (!!$stateParams.docFileId) {
          docFile = Applications.getDocFile({ docFileId: $stateParams.docFileId });

          docValues = Applications.getPersonDocumentValues({ docFileId: $stateParams.docFileId });
        }

        return $q.all({
          doc: doc && doc.$promise,
          docFile: docFile && docFile.$promise,
          docValues: docValues && docValues.$promise,
          docPartType: Nomenclatures.query({ alias: 'documentParts' }).$promise
        }).then(function (res) {
          var part = {};
          var docPartTypeName = _(res.docPartType).filter({
            alias: $stateParams.setPartAlias
          }).first().name;

          res.docFile = res.docFile || { name: docPartTypeName, docFileKindId: 2 };

          if (res.docValues && res.docValues.values) {
            _.assign(part, res.docValues.values);
          }

          //add lotId for the applicationDocument form to filter the case type by lot
          res.docFile.lotId = application.lotId;

          if ($stateParams.setPartAlias === 'personApplication' ||
            $stateParams.setPartAlias === 'organizationApplication' ||
            $stateParams.setPartAlias === 'aircraftApplication' ||
            $stateParams.setPartAlias === 'airportApplication' ||
            $stateParams.setPartAlias === 'equipmentApplication') {
            part.documentNumber = res.doc.documentNumber;
          }

          return {
            docId: $stateParams.docId,
            hasDocFile: !!$stateParams.docFileId,
            setPartAlias: $stateParams.setPartAlias,
            appFile: res.docFile,
            part: part,
            docPartTypeName: l10n.applications.edit.addPart.title + ': ' + docPartTypeName
          };
        });
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
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular, _));
