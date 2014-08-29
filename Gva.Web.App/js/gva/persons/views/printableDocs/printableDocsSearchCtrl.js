/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PrintableDocsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    scModal,
    docs
  ) {
    $scope.docs = docs;

    $scope.filters = {
      lin: null,
      names: null,
      licenceType: null,
      licenceAction: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.search = function () {
      return $state.go('root.printableDocs.search', $scope.filters, { reload: true });
    };

    $scope.print = function (doc) {
      var params = {
        lotId: doc.lotId,
        index: doc.partIndex
      };

      var modalInstance = scModal.open('printLicence', params);

      modalInstance.result.then(function (savedLicence) {
        var edition = _.find(savedLicence.part.editions, function (edition) {
          return edition.index === doc.editionIndex;
        });

        doc.stampNumber = edition.stampNumber;
      });

      return modalInstance.opened;
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

    $scope.viewApplication = function (lotId, partIndex) {
      return $state.go('root.persons.view.documentApplications.edit', {
        id: lotId,
        ind: partIndex
      });
    };
  }

  PrintableDocsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'scModal',
    'docs'
  ];

  PrintableDocsSearchCtrl.$resolve = {
    docs: [
      '$stateParams',
      'Persons',
      function resolveDocs($stateParams, Persons) {
        return Persons.getPrintableDocs($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PrintableDocsSearchCtrl', PrintableDocsSearchCtrl);
}(angular, _));
