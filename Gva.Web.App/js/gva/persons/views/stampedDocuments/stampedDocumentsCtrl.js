/*global angular, $, _*/
(function (angular, $, _) {
  'use strict';

  function StampedDocumentsCtrl(
    $scope,
    $state,
    $stateParams,
    Persons,
    documents,
    scModal
    ) {
      $scope.documents = documents;

      $scope.save = function () {
      var documentsForStamp = _.map($scope.documents, function(document){
        var stageAliases = [];
        if(document.licenceReady) {
          stageAliases.push('licenceReady');
        } 
        if (document.done) {
          stageAliases.push('done');
        }
        if (document.returned) {
          stageAliases.push('returned');
        }

        if (stageAliases !== []) {
          return {
            applicationId: document.application.applicationId,
            stageAliases: stageAliases
          };
        }
      });

      return Persons
        .saveStampedDocuments(documentsForStamp)
        .$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.selectCheck = function (event, item, action) {
      if ($(event.target).is(':checked')) {
        item[action] = true;
      }
      else {
        item[action] = false;
      }
    };

    $scope.viewApplication = function (lotId, partIndex) {
      var modalInstance = scModal.open('viewApplication', {
        lotId: lotId,
        path: 'personDocumentApplications',
        partIndex: partIndex,
        setPart: 'person'
      });

      modalInstance.result.then(function () {
        return $state.go('root.persons.view.documentApplications.edit', {
          id: lotId,
          ind: partIndex
        });
      });

      return modalInstance.opened;
    };

    $scope.viewDoc = function (doc) {
      var params = {
        lotId: doc.lotId,
        licencePartIndex: doc.partIndex,
        editionIndex: doc.editionIndex
      };

      var modalInstance = scModal.open('editLicence', params);

      modalInstance.result.then(function (savedLicence) {
        var edition = _.find(savedLicence.part.editions, function (edition) {
          return edition.index === doc.editionIndex;
        });

        doc.licenceNumber = savedLicence.part.licenceNumber;
        doc.licenceTypeId = savedLicence.part.licenceType.nomValueId;
        doc.dateValidFrom = edition.documentDateValidFrom;
        doc.dateValidTo = edition.documentDateValidTo;
        doc.licenceActionId = edition.licenceAction.nomValueId;
        doc.application = edition.applications[0];
      });

      return modalInstance.opened;
    };
  }

  StampedDocumentsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Persons',
    'documents',
    'scModal'
  ];

  StampedDocumentsCtrl.$resolve = {
    documents: [
      'Persons',
      function (Persons) {
        return Persons.getStampedDocuments().$promise;
      }
    ]
  };

  angular.module('gva').controller('StampedDocumentsCtrl', StampedDocumentsCtrl);
}(angular, $, _));
