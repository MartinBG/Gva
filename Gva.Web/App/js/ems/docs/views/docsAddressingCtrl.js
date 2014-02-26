/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsAddressingCtrl(
    $state,
    $scope,
    $stateParams,
    doc,
    selectedCorrs,
    selectedUnits
  ) {
    $scope.doc = doc;

    $scope.selectCorr = function selectCorr() {
      var doc = $scope.doc;
      selectedCorrs.corrs.splice(0);
      selectedCorrs.corrs = _.assign(selectedCorrs.corrs, doc.docCorrespondents);
      selectedCorrs.onCorrSelect = function (corr) {
        doc.docCorrespondents.push(corr);
        selectedCorrs.onCorrSelect = null;
      };

      return $state.go('root.docs.edit.addressing.selectCorr');
    };

    $scope.selectUnit = function selectUnit(message) {
      var doc = $scope.doc;
      selectedUnits.units.splice(0);

      if (message.type === 'from') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsFrom);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsFrom.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }
      else if (message.type === 'to') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsTo);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsTo.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }
      else if (message.type === 'cCopy') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsCCopy);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsCCopy.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }
      else if (message.type === 'importedBy') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsImportedBy);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsImportedBy.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }
      else if (message.type === 'madeBy') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsMadeBy);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsMadeBy.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }
      else if (message.type === 'inCharge') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsInCharge);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsInCharge.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }
      else if (message.type === 'controlling') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsControlling);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsControlling.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }
      else if (message.type === 'roleReaders') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsRoleReaders);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsRoleReaders.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }
      else if (message.type === 'editors') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsEditors);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsEditors.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }
      else if (message.type === 'registrators') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsRoleRegistrators);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsRoleRegistrators.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }

      return $state.go('root.docs.edit.addressing.selectUnit');
    };

    $scope.removeDocClassification = function () {
    };

    $scope.addDocClassification = function () {
    };
  }

  DocsAddressingCtrl.$inject = [
    '$state',
    '$scope',
    '$stateParams',
    'doc',
    'selectedCorrs',
    'selectedUnits'
  ];

  DocsAddressingCtrl.$resolve = {
    selectedCorrs: [
      function resolveSelectedCorrs() {
        return {
          corrs: [],
          onCorrSelect: null
        };
      }
    ],
    selectedUnits: [
      function resolveSelectedUnits() {
        return {
          units: [],
          onUnitSelect: null
        };
      }
    ]
  };

  angular.module('ems').controller('DocsAddressingCtrl', DocsAddressingCtrl);
}(angular, _));
