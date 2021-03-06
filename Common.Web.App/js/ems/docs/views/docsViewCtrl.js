﻿/*global angular*/
(function (angular) {
  'use strict';

  function DocsViewCtrl(
    $state,
    $scope,
    $stateParams,
    doc,
    scModal
  ) {
    $scope.doc = doc;

    $scope.newCorr = function () {
      var modalInstance = scModal.open('newCorr');

      modalInstance.result.then(function (nomItem) {
        var newCorr = $scope.doc.docCorrespondents.slice();
        newCorr.push(nomItem);
        $scope.doc.docCorrespondents = newCorr;
      });

      return modalInstance.opened;
    };

    $scope.selectCorr = function () {
      var modalInstance = scModal.open('chooseCorr', {
        selectedCorrs: $scope.doc.docCorrespondents,
        corr: {}
      });

      modalInstance.result.then(function (nomItem) {
        var newCorr = $scope.doc.docCorrespondents.slice();
        newCorr.push(nomItem);
        $scope.doc.docCorrespondents = newCorr;
      });

      return modalInstance.opened;
    };

    $scope.selectUnit = function selectUnit(message) {
      var selectedUnits, modalInstance, onUnitSelect;

      if (message.type === 'from') {
        selectedUnits = $scope.doc.docUnitsFrom;

        onUnitSelect = function (unit) {
          var newUnit = $scope.doc.docUnitsFrom.slice();
          newUnit.push(unit);
          $scope.doc.docUnitsFrom = newUnit;
        };
      }
      else if (message.type === 'to') {
        selectedUnits = $scope.doc.docUnitsTo;

        onUnitSelect = function (unit) {
          var newUnit = $scope.doc.docUnitsTo.slice();
          newUnit.push(unit);
          $scope.doc.docUnitsTo = newUnit;
        };
      }
      else if (message.type === 'cCopy') {
        selectedUnits = $scope.doc.docUnitsCCopy;

        onUnitSelect = function (unit) {
          var newUnit = $scope.doc.docUnitsCCopy.slice();
          newUnit.push(unit);
          $scope.doc.docUnitsCCopy = newUnit;
        };
      }
      else if (message.type === 'importedBy') {
        selectedUnits = $scope.doc.docUnitsImportedBy;

        onUnitSelect = function (unit) {
          var newUnit = $scope.doc.docUnitsImportedBy.slice();
          newUnit.push(unit);
          $scope.doc.docUnitsImportedBy = newUnit;
        };
      }
      else if (message.type === 'madeBy') {
        selectedUnits = $scope.doc.docUnitsMadeBy;

        onUnitSelect = function (unit) {
          var newUnit = $scope.doc.docUnitsMadeBy.slice();
          newUnit.push(unit);
          $scope.doc.docUnitsMadeBy = newUnit;
        };
      }
      else if (message.type === 'inCharge') {
        selectedUnits = $scope.doc.docUnitsInCharge;

        onUnitSelect = function (unit) {
          var newUnit = $scope.doc.docUnitsInCharge.slice();
          newUnit.push(unit);
          $scope.doc.docUnitsInCharge = newUnit;
        };
      }
      else if (message.type === 'controlling') {
        selectedUnits = $scope.doc.docUnitsControlling;

        onUnitSelect = function (unit) {
          var newUnit = $scope.doc.docUnitsControlling.slice();
          newUnit.push(unit);
          $scope.doc.docUnitsControlling = newUnit;
        };
      }
      else if (message.type === 'readers') {
        selectedUnits = $scope.doc.docUnitsReaders;

        onUnitSelect = function (unit) {
          var newUnit = $scope.doc.docUnitsReaders.slice();
          newUnit.push(unit);
          $scope.doc.docUnitsReaders = newUnit;
        };
      }
      else if (message.type === 'editors') {
        selectedUnits = $scope.doc.docUnitsEditors;

        onUnitSelect = function (unit) {
          var newUnit = $scope.doc.docUnitsEditors.slice();
          newUnit.push(unit);
          $scope.doc.docUnitsEditors = newUnit;
        };
      }
      else if (message.type === 'registrators') {
        selectedUnits = $scope.doc.docUnitsRegistrators;

        onUnitSelect = function (unit) {
          var newUnit = $scope.doc.docUnitsRegistrators.slice();
          newUnit.push(unit);
          $scope.doc.docUnitsRegistrators = newUnit;
        };
      }

      modalInstance = scModal.open('chooseUnit', {
        selectedUnits: selectedUnits
      });

      modalInstance.result.then(function (unit) {
        onUnitSelect(unit);
      });

      return modalInstance.opened;
    };
  }

  DocsViewCtrl.$inject = [
    '$state',
    '$scope',
    '$stateParams',
    'doc',
    'scModal'
  ];

  angular.module('ems').controller('DocsViewCtrl', DocsViewCtrl);
}(angular));
