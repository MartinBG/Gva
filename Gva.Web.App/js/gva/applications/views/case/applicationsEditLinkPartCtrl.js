/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditLinkPartCtrl(
    $scope,
    $state,
    $stateParams,
    AplicationsCase,
    PersonDocumentIds,
    PersonDocumentEducations,
    PersonDocumentEmployments,
    PersonDocumentMedicals,
    PersonDocumentChecks,
    PersonDocumentTrainings,
    PersonDocumentOthers,
    PersonDocumentApplications,
    OrganizationDocumentOthers,
    OrganizationDocumentApplications,
    AircraftDocumentOthers,
    AircraftDocumentOccurrences,
    AircraftDocumentDebtsFM,
    AircraftDocumentOwners,
    AircraftDocumentApplications,
    AirportDocumentOwners,
    AirportDocumentOthers,
    AirportDocumentApplications,
    EquipmentDocumentOwners,
    EquipmentDocumentOthers,
    EquipmentDocumentApplications,
    application
    ) {
    $scope.application = application;

    $scope.docPartType = null;

    $scope.search = function () {
      $scope.showPDocumentId = false;
      $scope.showPDocumentEducation = false;
      $scope.showPDocumentEmployment = false;
      $scope.showPDocumentMed = false;
      $scope.showPDocumentCheck = false;
      $scope.showPDocumentTraining = false;
      $scope.showPDocumentOther = false;
      $scope.showPDocumentApplication = false;
      $scope.showODocumentOther = false;
      $scope.showODocumentApplication = false;
      $scope.showADocumentOther = false;
      $scope.showADocumentOccurrence = false;
      $scope.showADocumentDebtFM = false;
      $scope.showADocumentOwner = false;
      $scope.showADocumentApplication = false;
      $scope.showAPDocumentOwner = false;
      $scope.showAPDocumentOther = false;
      $scope.showAPDocumentApplication = false;
      $scope.showEDocumentOwner = false;
      $scope.showEDocumentOther = false;
      $scope.showEDocumentApplication = false;

      if ($scope.docPartType) {
        if ($scope.docPartType.alias === 'personDocumentId') {
          return PersonDocumentIds.query({ id: $scope.application.lotId })
            .$promise.then(function (documentIds) {
              $scope.documentPart = documentIds;
              $scope.showPDocumentId = !!documentIds;
            });
        }
        else if ($scope.docPartType.alias === 'personEducation') {
          return PersonDocumentEducations.query({ id: $scope.application.lotId })
            .$promise.then(function (documentEducations) {
              $scope.documentPart = documentEducations;
              $scope.showPDocumentEducation = !!documentEducations;
            });
        }
        else if ($scope.docPartType.alias === 'personEmployment') {
          return PersonDocumentEmployments.query({ id: $scope.application.lotId })
            .$promise.then(function (employments) {
              $scope.documentPart = employments;
              $scope.showPDocumentEmployment = !!employments;
            });
        }
        else if ($scope.docPartType.alias === 'personMedical') {
          return PersonDocumentMedicals.query({ id: $scope.application.lotId })
            .$promise.then(function (medicals) {
              $scope.documentPart = medicals.map(function (medical) {
                var testimonial = medical.part.documentNumberPrefix + ' ' +
                  medical.part.documentNumber + ' ' +
                  medical.part.documentNumberSuffix;

                medical.part.testimonial = testimonial;

                var limitations = '';
                for (var i = 0; i < medical.part.limitations.length; i++) {
                  limitations += medical.part.limitations[i].name + ', ';
                }
                limitations = limitations.substring(0, limitations.length - 2);
                medical.part.limitations = limitations;

                return medical;
              });
              $scope.showPDocumentMed = !!medicals;
            });
        }
        else if ($scope.docPartType.alias === 'personCheck') {
          return PersonDocumentChecks.query({ id: $scope.application.lotId })
            .$promise.then(function (checks) {
              $scope.documentPart = checks;
              $scope.showPDocumentCheck = !!checks;
            });
        }
        else if ($scope.docPartType.alias === 'personTraining') {
          return PersonDocumentTrainings.query({ id: $scope.application.lotId })
            .$promise.then(function (documentTrainings) {
              $scope.documentPart = documentTrainings;
              $scope.showPDocumentTraining = !!documentTrainings;
            });
        }
        else if ($scope.docPartType.alias === 'personOther') {
          return PersonDocumentOthers.query({ id: $scope.application.lotId })
            .$promise.then(function (documentOthers) {
              $scope.documentPart = documentOthers;
              $scope.showPDocumentOther = !!documentOthers;
            });
        }
        else if ($scope.docPartType.alias === 'personApplication') {
          return PersonDocumentApplications.query({ id: $scope.application.lotId })
            .$promise.then(function (documentApplications) {
              $scope.documentPart = documentApplications;
              $scope.showPDocumentApplication = !!documentApplications;
            });
        }
        else if ($scope.docPartType.alias === 'organizationOther') {
          return OrganizationDocumentOthers.query({ id: $scope.application.lotId })
            .$promise.then(function (organizationOthers) {
              $scope.documentPart = organizationOthers;
              $scope.showODocumentOther = !!organizationOthers;
            });
        }
        else if ($scope.docPartType.alias === 'organizationApplication') {
          return OrganizationDocumentApplications.query({ id: $scope.application.lotId })
            .$promise.then(function (organizationApplications) {
              $scope.documentPart = organizationApplications;
              $scope.showODocumentApplication = !!organizationApplications;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftOwner') {
          return AircraftDocumentOwners.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftDocumentOwners) {
              $scope.documentPart = aircraftDocumentOwners;
              $scope.showADocumentOwner = !!aircraftDocumentOwners;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftDebtFM') {
          return AircraftDocumentDebtsFM.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftDocumentDebtFMs) {
              $scope.documentPart = aircraftDocumentDebtFMs;
              $scope.showADocumentDebtFM = !!aircraftDocumentDebtFMs;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftOccurrence') {
          return AircraftDocumentOccurrences.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftDocumentOccurrences) {
              $scope.documentPart = aircraftDocumentOccurrences;
              $scope.showADocumentOccurrence = !!aircraftDocumentOccurrences;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftApplication') {
          return AircraftDocumentApplications.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftDocumentApplications) {
              $scope.documentPart = aircraftDocumentApplications;
              $scope.showADocumentApplication = !!aircraftDocumentApplications;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftOther') {
          return AircraftDocumentOthers.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftDocumentOthers) {
              $scope.documentPart = aircraftDocumentOthers;
              $scope.showADocumentOther = !!aircraftDocumentOthers;
            });
        }

        else if ($scope.docPartType.alias === 'airportApplication') {
          return AirportDocumentApplications.query({ id: $scope.application.lotId })
            .$promise.then(function (airportDocumentApplications) {
              $scope.documentPart = airportDocumentApplications;
              $scope.showAPDocumentApplication = !!airportDocumentApplications;
            });
        }
        else if ($scope.docPartType.alias === 'airportOwner') {
          return AirportDocumentOwners.query({ id: $scope.application.lotId })
            .$promise.then(function (airportDocumentOwners) {
              $scope.documentPart = airportDocumentOwners;
              $scope.showAPDocumentOwner = !!airportDocumentOwners;
            });
        }
        else if ($scope.docPartType.alias === 'airportOther') {
          return AirportDocumentOthers.query({ id: $scope.application.lotId })
            .$promise.then(function (airportDocumentOthers) {
              $scope.documentPart = airportDocumentOthers;
              $scope.showAPDocumentOther = !!airportDocumentOthers;
            });
        }
        else if ($scope.docPartType.alias === 'equipmentOwner') {
          return EquipmentDocumentOwners.query({ id: $scope.application.lotId })
            .$promise.then(function (equipmentDocumentOwners) {
              $scope.documentPart = equipmentDocumentOwners;
              $scope.showEDocumentOwner = !!equipmentDocumentOwners;
            });
        }
        else if ($scope.docPartType.alias === 'equipmentOther') {
          return EquipmentDocumentOthers.query({ id: $scope.application.lotId })
            .$promise.then(function (equipmentDocumentOthers) {
              $scope.documentPart = equipmentDocumentOthers;
              $scope.showEDocumentOther = !!equipmentDocumentOthers;
            });
        }
        else if ($scope.docPartType.alias === 'equipmentApplication') {
          return EquipmentDocumentApplications.query({ id: $scope.application.lotId })
            .$promise.then(function (equipmentDocumentApplications) {
              $scope.documentPart = equipmentDocumentApplications;
              $scope.showEDocumentApplication = !!equipmentDocumentApplications;
            });
        }
      }
    };

    $scope.linkPart = function (partId) {
      return AplicationsCase.linkExistingPart({
        id: $stateParams.id,
        docFileId: $stateParams.docFileId,
        partId: partId
      }).$promise.then(function () {
        return $state.transitionTo('root.applications.edit.case',
          $stateParams, { reload: true }
        );
      });
    };

    $scope.cancel = function () {
      return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
    };
  }

  ApplicationsEditLinkPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AplicationsCase',
    'PersonDocumentIds',
    'PersonDocumentEducations',
    'PersonDocumentEmployments',
    'PersonDocumentMedicals',
    'PersonDocumentChecks',
    'PersonDocumentTrainings',
    'PersonDocumentOthers',
    'PersonDocumentApplications',
    'OrganizationDocumentOthers',
    'OrganizationDocumentApplications',
    'AircraftDocumentOthers',
    'AircraftDocumentOccurrences',
    'AircraftDocumentDebtsFM',
    'AircraftDocumentOwners',
    'AircraftDocumentApplications',
    'AirportDocumentOwners',
    'AirportDocumentOthers',
    'AirportDocumentApplications',
    'EquipmentDocumentOwners',
    'EquipmentDocumentOthers',
    'EquipmentDocumentApplications',
    'application'
  ];

  angular.module('gva').controller('ApplicationsEditLinkPartCtrl', ApplicationsEditLinkPartCtrl);
}(angular
));
