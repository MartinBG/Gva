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
    application
    ) {
    $scope.application = application;

    $scope.docPartType = null;

    $scope.search = function () {
      $scope.showDocumentId = false;
      $scope.showDocumentEducation = false;
      $scope.showDocumentEmployment = false;
      $scope.showDocumentMed = false;
      $scope.showDocumentCheck = false;
      $scope.showDocumentTraining = false;
      $scope.showDocumentOther = false;
      $scope.showDocumentApplication = false;

      if ($scope.docPartType) {
        if ($scope.docPartType.alias === 'documentId') {
          PersonDocumentId.query({ id: $scope.application.lotId })
            .$promise.then(function (documentIds) {
              $scope.documentPart = documentIds;
              $scope.showDocumentId = !!documentIds;
            });
        }
        else if ($scope.docPartType.alias === 'education') {
          PersonDocumentEducation.query({ id: $scope.application.lotId })
            .$promise.then(function (documentEducations) {
              $scope.documentPart = documentEducations;
              $scope.showDocumentEducation = !!documentEducations;
            });
        }
        else if ($scope.docPartType.alias === 'employment') {
          PersonDocumentEmployment.query({ id: $scope.application.lotId })
            .$promise.then(function (employments) {
              $scope.documentPart = employments;
              $scope.showDocumentEmployment = !!employments;
            });
        }
        else if ($scope.docPartType.alias === 'medical') {
          PersonDocumentMedical.query({ id: $scope.application.lotId })
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
        else if ($scope.docPartType.alias === 'check') {
          PersonDocumentCheck.query({ id: $scope.application.lotId })
            .$promise.then(function (checks) {
              $scope.documentPart = checks;
              $scope.showDocumentCheck = !!checks;
            });
        }
        else if ($scope.docPartType.alias === 'training') {
          PersonDocumentTraining.query({ id: $scope.application.lotId })
            .$promise.then(function (documentTrainings) {
              $scope.documentPart = documentTrainings;
              $scope.showDocumentTraining = !!documentTrainings;
            });
        }
        else if ($scope.docPartType.alias === 'other') {
          PersonDocumentOther.query({ id: $scope.application.lotId })
            .$promise.then(function (documentOthers) {
              $scope.documentPart = documentOthers;
              $scope.showDocumentOther = !!documentOthers;
            });
        }
        else if ($scope.docPartType.alias === 'application') {
          PersonDocumentApplication.query({ id: $scope.application.lotId })
            .$promise.then(function (documentApplications) {
              $scope.documentPart = documentApplications;
              $scope.showDocumentApplication = !!documentApplications;
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
    'application'
  ];

  angular.module('gva').controller('ApplicationsEditLinkPartCtrl', ApplicationsEditLinkPartCtrl);
}(angular
));
