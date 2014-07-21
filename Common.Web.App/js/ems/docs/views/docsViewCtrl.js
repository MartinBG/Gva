/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsViewCtrl(
    $state,
    $scope,
    $stateParams,
    doc,
    selectedUnits,
    namedModal
  ) {
    $scope.doc = doc;

    $scope.newCorr = function () {
      var modalInstance = namedModal.open('newCorr');

      modalInstance.result.then(function (nomItem) {
        var newCorr = $scope.doc.docCorrespondents.slice();
        newCorr.push(nomItem);
        $scope.doc.docCorrespondents = newCorr;
      });

      return modalInstance.opened;
    };

    $scope.selectCorr = function () {
      var modalInstance = namedModal.open('chooseCorr', {
        selectedCorrs: $scope.doc.docCorrespondents
      }, {
        corrs: [
          'Corrs',
          function (Corrs) {
            return Corrs.get().$promise;
          }
        ]
      });

      modalInstance.result.then(function (nomItem) {
        var newCorr = $scope.doc.docCorrespondents.slice();
        newCorr.push(nomItem);
        $scope.doc.docCorrespondents = newCorr;
      });

      return modalInstance.opened;
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
      else if (message.type === 'readers') {
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsReaders);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsReaders.push(unit);
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
        selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsRegistrators);
        selectedUnits.onUnitSelect = function (unit) {
          doc.docUnitsRegistrators.push(unit);
          selectedUnits.onUnitSelect = null;
        };
      }

      return $state.go('root.docs.edit.view.selectUnit');
    };
  }

  DocsViewCtrl.$inject = [
    '$state',
    '$scope',
    '$stateParams',
    'doc',
    'selectedUnits',
    'namedModal'
  ];

  DocsViewCtrl.$resolve = {
    selectedUnits: [
      function resolveSelectedUnits() {
        return {
          units: [],
          onUnitSelect: null
        };
      }
    ]
  };

  angular.module('ems').controller('DocsViewCtrl', DocsViewCtrl);
}(angular, _));
