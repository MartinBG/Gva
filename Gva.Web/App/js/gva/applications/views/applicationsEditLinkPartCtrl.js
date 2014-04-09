/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditLinkPartCtrl(
    $scope,
    $state,
    $stateParams,
    Application,
    PersonDocumentId,
    PersonDocumentEducation,
    PersonDocumentEmployment,
    PersonDocumentMedical,
    PersonDocumentCheck,
    PersonDocumentTraining,
    PersonDocumentOther,
    PersonDocumentApplication,
    OrganizationDocumentOther,
    OrganizationDocumentApplication,
    AircraftDocumentOther,
    AircraftDocumentOccurrence,
    AircraftInspection,
    AircraftDocumentDebtFM,
    AircraftDocumentOwner,
    AircraftDocumentApplication,
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
      $scope.showAInspection = false;
      $scope.showADocumentDebtFM = false;
      $scope.showADocumentOwner = false;
      $scope.showADocumentApplication = false;

      if ($scope.docPartType) {
        if ($scope.docPartType.alias === 'personDocumentId') {
          return PersonDocumentId.query({ id: $scope.application.lotId })
            .$promise.then(function (documentIds) {
              $scope.documentPart = documentIds;
              $scope.showDocumentId = !!documentIds;
            });
        }
        else if ($scope.docPartType.alias === 'personEducation') {
          return PersonDocumentEducation.query({ id: $scope.application.lotId })
            .$promise.then(function (documentEducations) {
              $scope.documentPart = documentEducations;
              $scope.showDocumentEducation = !!documentEducations;
            });
        }
        else if ($scope.docPartType.alias === 'personEmployment') {
          return PersonDocumentEmployment.query({ id: $scope.application.lotId })
            .$promise.then(function (employments) {
              $scope.documentPart = employments;
              $scope.showDocumentEmployment = !!employments;
            });
        }
        else if ($scope.docPartType.alias === 'personMedical') {
          return PersonDocumentMedical.query({ id: $scope.application.lotId })
            .$promise.then(function (medicals) {
              $scope.documentPart = medicals.map(function (medical) {
                var testimonial = medical.part.documentNumberPrefix + ' ' +
                  medical.part.documentNumber + ' ' +
                  medical.part.documentNumberSuffix;

                medical.part.testimonial = testimonial;

                var limitations = '';
                for (var i = 0; i < medical.part.limitationsTypes.length; i++) {
                  limitations += medical.part.limitationsTypes[i].name + ', ';
                }
                limitations = limitations.substring(0, limitations.length - 2);
                medical.part.limitations = limitations;

                return medical;
              });
              $scope.showDocumentMed = !!medicals;
            });
        }
        else if ($scope.docPartType.alias === 'personCheck') {
          return PersonDocumentCheck.query({ id: $scope.application.lotId })
            .$promise.then(function (checks) {
              $scope.documentPart = checks;
              $scope.showDocumentCheck = !!checks;
            });
        }
        else if ($scope.docPartType.alias === 'personTraining') {
          return PersonDocumentTraining.query({ id: $scope.application.lotId })
            .$promise.then(function (documentTrainings) {
              $scope.documentPart = documentTrainings;
              $scope.showDocumentTraining = !!documentTrainings;
            });
        }
        else if ($scope.docPartType.alias === 'personOther') {
          return PersonDocumentOther.query({ id: $scope.application.lotId })
            .$promise.then(function (documentOthers) {
              $scope.documentPart = documentOthers;
              $scope.showDocumentOther = !!documentOthers;
            });
        }
        else if ($scope.docPartType.alias === 'personApplication') {
          return PersonDocumentApplication.query({ id: $scope.application.lotId })
            .$promise.then(function (documentApplications) {
              $scope.documentPart = documentApplications;
              $scope.showDocumentApplication = !!documentApplications;
            });
        }
        else if ($scope.docPartType.alias === 'organizationOther') {
          return OrganizationDocumentOther.query({ id: $scope.application.lotId })
            .$promise.then(function (organizationOthers) {
              $scope.documentPart = organizationOthers;
              $scope.showODocumentOther = !!organizationOthers;
            });
        }
        else if ($scope.docPartType.alias === 'organizationApplication') {
          return OrganizationDocumentApplication.query({ id: $scope.application.lotId })
            .$promise.then(function (organizationApplications) {
              $scope.documentPart = organizationApplications;
              $scope.showODocumentApplication = !!organizationApplications;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftOwner') {
          return AircraftDocumentOwner.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftDocumentOwners) {
              $scope.documentPart = aircraftDocumentOwners;
              $scope.showADocumentOwner = !!aircraftDocumentOwners;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftDebtFM') {
          return AircraftDocumentDebtFM.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftDocumentDebtFMs) {
              $scope.documentPart = aircraftDocumentDebtFMs;
              $scope.showADocumentDebtFM = !!aircraftDocumentDebtFMs;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftInspection') {
          return AircraftInspection.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftInspections) {
              $scope.documentPart = aircraftInspections;
              $scope.showAInspection = !!aircraftInspections;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftOccurrence') {
          return AircraftDocumentOccurrence.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftDocumentOccurrences) {
              $scope.documentPart = aircraftDocumentOccurrences;
              $scope.showADocumentOccurrence = !!aircraftDocumentOccurrences;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftApplication') {
          return AircraftDocumentApplication.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftDocumentApplications) {
              $scope.documentPart = aircraftDocumentApplications;
              $scope.showADocumentApplication = !!aircraftDocumentApplications;
            });
        }
        else if ($scope.docPartType.alias === 'aircraftOther') {
          return AircraftDocumentOther.query({ id: $scope.application.lotId })
            .$promise.then(function (aircraftDocumentOthers) {
              $scope.documentPart = aircraftDocumentOthers;
              $scope.showADocumentOther = !!aircraftDocumentOthers;
            });
        }
      }
    };

    $scope.linkPart = function (partId) {
      var linkExisting = {
        docFileId: $stateParams.docFileId,
        partId: partId
      };

      return Application
        .linkExistingPart({ id: $stateParams.id }, linkExisting)
          .$promise.then(function () {
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
    'Application',
    'PersonDocumentId',
    'PersonDocumentEducation',
    'PersonDocumentEmployment',
    'PersonDocumentMedical',
    'PersonDocumentCheck',
    'PersonDocumentTraining',
    'PersonDocumentOther',
    'PersonDocumentApplication',
    'OrganizationDocumentOther',
    'OrganizationDocumentApplication',
    'AircraftDocumentOther',
    'AircraftDocumentOccurrence',
    'AircraftInspection',
    'AircraftDocumentDebtFM',
    'AircraftDocumentOwner',
    'AircraftDocumentApplication',
    'application'
  ];

  angular.module('gva').controller('ApplicationsEditLinkPartCtrl', ApplicationsEditLinkPartCtrl);
}(angular
));
