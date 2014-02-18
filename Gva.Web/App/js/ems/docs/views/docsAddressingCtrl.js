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

    $scope.selectUnitFrom = function selectUnitFrom() {
      var doc = $scope.doc;
      selectedUnits.units.splice(0);
      selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsFrom);
      selectedUnits.onUnitSelect = function (unit) {
        doc.docUnitsFrom.push(unit);
        selectedUnits.onUnitSelect = null;
      };

      return $scope.selectUnit();
    };

    $scope.selectUnitTo = function selectUnitTo() {
      var doc = $scope.doc;
      selectedUnits.units.splice(0);
      selectedUnits.units = _.assign(selectedUnits.units, doc.docUnitsTo);
      selectedUnits.onUnitSelect = function (unit) {
        doc.docUnitsTo.push(unit);
        selectedUnits.onUnitSelect = null;
      };

      return $scope.selectUnit();
    };

    $scope.selectUnit = function selectUnit() {
      return $state.go('root.docs.edit.addressing.selectUnit');
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
