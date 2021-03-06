﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsEditCaseCtrl(
    $scope,
    $state,
    $stateParams,
    scMessage,
    Applications,
    application
    ) {
    $scope.application = application;

    _.map(application.appDocCase, function (adc) {
      switch (adc.docDocStatusAlias) {
        case 'Draft':
        case 'Processed':
          adc.hasNextStatus = true;
          break;
        default:
          adc.hasNextStatus = false;
          break;
      }

      return adc;
    });

    $scope.linkNew = function (docId, docFileId) {
      return $state.go('root.applications.edit.case.newFile', {
        docId: docId,
        docFileId: docFileId
      });
    };

    $scope.linkPart = function (docFileId) {
      return $state.go('root.applications.edit.case.linkPart', {
        docFileId: docFileId
      });
    };

    $scope.newDocFile = function (docId) {
      return $state.go('root.applications.edit.case.newDocFile', {
        docId: docId
      });
    };

    $scope.childDoc = function (docId) {
      return $state.go('root.applications.edit.case.childDoc', {
        parentDocId: docId
      });
    };

    $scope.viewPart = function (value) {
      var state;

      if(value.setPartAlias.indexOf('Application') > 0) {
        return $state.go('root.applications.edit.data', {
          id: application.applicationId
        });
      }

      if (value.setPartAlias === 'personDocumentId') {
        state = 'root.persons.view.documentIds.edit';
      }
      else if (value.setPartAlias === 'personEducation') {
        state = 'root.persons.view.documentEducations.edit';
      }
      else if (value.setPartAlias === 'personEmployment') {
        state = 'root.persons.view.employments.edit';
      }
      else if (value.setPartAlias === 'personMedical') {
        state = 'root.persons.view.medicals.edit';
      }
      else if (value.setPartAlias === 'personCheck') {
        state = 'root.persons.view.checks.edit';
      }
      else if (value.setPartAlias === 'personReport') {
        state = 'root.persons.view.reports.edit';
      }
      else if (value.setPartAlias === 'personTraining') {
        state = 'root.persons.view.documentTrainings.edit';
      }
      else if (value.setPartAlias === 'personLangCert') {
        state = 'root.persons.view.documentLangCerts.edit';
      }
      else if (value.setPartAlias === 'personOther') {
        state = 'root.persons.view.documentOthers.edit';
      }
      else if (value.setPartAlias === 'organizationOther') {
        state = 'root.organizations.view.documentOthers.edit';
      }
      else if (value.setPartAlias === 'aircraftOwner') {
        state = 'root.aircrafts.view.owners.edit';
      }
      else if (value.setPartAlias === 'aircraftDebtFM') {
        state = 'root.aircrafts.view.debtsFM.edit';
      }
      else if (value.setPartAlias === 'aircraftOccurrence') {
        state = 'root.aircrafts.view.occurrences.edit';
      }
      else if (value.setPartAlias === 'aircraftOther') {
        state = 'root.aircrafts.view.others.edit';
      }
      else if (value.setPartAlias === 'airportOwner') {
        state = 'root.airports.view.owners.edit';
      }
      else if (value.setPartAlias === 'airportOther') {
        state = 'root.airports.view.others.edit';
      }
      else if (value.setPartAlias === 'airportInspection') {
        state = 'root.airports.view.inspections.edit';
      }
      else if (value.setPartAlias === 'equipmentOwner') {
        state = 'root.equipments.view.owners.edit';
      }
      else if (value.setPartAlias === 'equipmentOther') {
        state = 'root.equipments.view.others.edit';
      }
      else if (value.setPartAlias === 'equipmentInspection') {
        state = 'root.equipments.view.inspections.edit';
      }
      else if (value.setPartAlias === 'aircraftInspection') {
        state = 'root.aircrafts.view.inspections.edit';
      }
      else if (value.setPartAlias === 'airportInspection') {
        state = 'root.airports.view.inspections.edit';
      }
      else if (value.setPartAlias === 'equipmentInspection') {
        state = 'root.equipments.view.inspections.edit';
      }
      else if (value.setPartAlias === 'organizationInspection') {
        state = 'root.organizations.view.inspections.edit';
      }
      else if (value.setPartAlias === 'organizationRecommendation') {
        state = 'root.organizations.view.edit';
      }
      else if (value.setPartAlias === 'organizationStaffManagement') {
        state = 'root.organizations.view.staffManagement.edit';
      }

      return $state.go(state, {
        id: application.lotId,
        ind: value.partIndex,
        appId: application.applicationId,
        set: $stateParams.set
      });
    };

    $scope.viewDoc = function (docId) {
      return $state.go('root.docs.edit.case', { id: docId });
    };

    $scope.unlink = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return Applications
            .remove({ id: $stateParams.id })
            .$promise.then(function () {
              if (application.lotSetAlias === 'person') {
                return $state.go(
                  'root.persons.view.documentApplications.search', { id: application.lotId });
              }
              else if (application.lotSetAlias === 'organization') {
                return $state.go(
                  'root.organizations.view.documentApplications.search', { id: application.lotId });
              }
              else if (application.lotSetAlias === 'aircraft') {
                return $state.go(
                  'root.aircrafts.view.documentApplications.search', { id: application.lotId });
              }
              else if (application.lotSetAlias === 'airport') {
                return $state.go(
                  'root.airports.view.documentApplications.search', { id: application.lotId });
              }
              else if (application.lotSetAlias === 'equipment') {
                return $state.go(
                  'root.equipments.view.documentApplications.search', { id: application.lotId });
              }
            });
        }
      });
    };

    $scope.moveToCase = function (item) {
      return Applications.movePartToCase({
        id: $stateParams.id,
        gvaLotFileId: item.gvaLotFileId
      }).$promise.then(function () {
        return $state.transitionTo($state.$current, $stateParams, { reload: true });
      });
    };
  }

  ApplicationsEditCaseCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'scMessage',
    'Applications',
    'application'
  ];

  ApplicationsEditCaseCtrl.$resolve = {
    application: [
      '$stateParams',
      'Applications',
      function ResolveApplication($stateParams, Applications) {
        return Applications.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ApplicationsEditCaseCtrl', ApplicationsEditCaseCtrl);
}(angular, _));
