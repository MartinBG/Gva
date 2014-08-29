/*global angular, $, _*/
(function (angular, $, _) {
  'use strict';

  function StampedDocumentsCtrl(
    $scope,
    $state,
    $stateParams,
    PersonStampedDocuments,
    documents,
    scModal
    ) {
      $scope.documents = documents;

      $scope.save = function () {
      var documentsForStamp = _.map($scope.documents, function(document){
        var stageAlias = '';
        if(document.licenceReady) {
          stageAlias = 'licenceReady';
        } else if (document.done) {
          stageAlias = 'done';
        } else if (document) {
          stageAlias = 'returned';
        }
        if (stageAlias !== '') {
          return {
            applicationId: document.application.applicationId,
            stageAlias: stageAlias
          };
        }
      });
      return PersonStampedDocuments
        .save(documentsForStamp)
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
      return $state.go('root.persons.view.documentApplications.edit', {
        id: lotId,
        ind: partIndex
      });
    };

    $scope.viewDoc = function (doc) {
      var params = {
        lotId: doc.lotId,
        licenceIndex: doc.partIndex,
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
    'PersonStampedDocuments',
    'documents',
    'scModal'
  ];

  StampedDocumentsCtrl.$resolve = {
    documents: [
      'PersonStampedDocuments',
      function (PersonStampedDocuments) {
        return PersonStampedDocuments.query().$promise;
      }
    ]
  };

  angular.module('gva').controller('StampedDocumentsCtrl', StampedDocumentsCtrl);
}(angular, $, _));
