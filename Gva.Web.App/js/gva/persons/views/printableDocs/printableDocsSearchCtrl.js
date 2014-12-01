﻿/*global angular, _*/
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

    $scope.viewApplication = function (appId, lotId, partIndex) {
      var modalInstance = scModal.open('viewApplication', {
        lotId: lotId,
        path: 'personDocumentApplications',
        partIndex: partIndex,
        setPart: 'person'
      });

      modalInstance.result.then(function () {
        return $state.go('root.applications.edit.data', {
          id: appId,
          set: 'person',
          lotId: lotId,
          ind: partIndex,
          setPartPath: 'personDocumentApplications'
        });
      });

      return modalInstance.opened;
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
