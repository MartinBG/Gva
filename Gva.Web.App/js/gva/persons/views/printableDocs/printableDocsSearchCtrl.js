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
        index: doc.partIndex,
        editionIndex: doc.editionPartIndex
      };

      var modalInstance = scModal.open('printLicence', params);

      modalInstance.result.then(function (savedLicenceEdition) {
        var edition = savedLicenceEdition;

        doc.stampNumber = edition.part.stampNumber;
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

      modalInstance.result.then(function (returnValue) {
        var edition = returnValue.savedLicenceEdition,
            licence = returnValue.licence;

        doc.licenceNumber = licence.part.licenceNumber;
        doc.licenceTypeId = licence.part.licenceType.nomValueId;
        doc.dateValidFrom = edition.part.documentDateValidFrom;
        doc.dateValidTo = edition.part.documentDateValidTo;
        doc.licenceActionId = edition.part.licenceAction.nomValueId;
        doc.application = edition.files.length !== 0 ? edition.files[0].applications[0] : null;
      });

      return modalInstance.opened;
    };

    $scope.viewApplication = function (lotId, partIndex) {
      return $state.go('root.persons.view.documentApplications.edit', {
        id: lotId,
        ind: partIndex
      });
    };

    $scope.licenceNumberFormatMask = function (item) {
      var licenceNumberMask = item.licenceNumber.toString(),
          licenceNumberLength = licenceNumberMask.length;

      if (licenceNumberLength < 5) {
        var i, difference = 5 - licenceNumberLength;
        for (i = 0; i < difference; i++) {
          licenceNumberMask = '0' + licenceNumberMask;
        }
      }

      return item.publisherCode + ' ' +
             item.licenceTypeCaCode + '- ' +
             licenceNumberMask;
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
