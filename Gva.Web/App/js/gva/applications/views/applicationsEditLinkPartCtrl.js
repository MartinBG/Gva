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
    PersonDocumentApplication
    ) {
    $scope.docFileKey = $stateParams.docFileKey;
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
        if ($scope.docPartType.alias === 'DocumentId') {
          PersonDocumentId.query({ id: $scope.application.lotId })
            .$promise.then(function (documentIds) {
              $scope.documentPart = documentIds;
              $scope.showDocumentId = !!documentIds;
            });
        }
        else if ($scope.docPartType.alias === 'DocumentEducation') {
          PersonDocumentEducation.query({ id: $scope.application.lotId })
            .$promise.then(function (documentEducations) {
              $scope.documentPart = documentEducations;
              $scope.showDocumentEducation = !!documentEducations;
            });
        }
        else if ($scope.docPartType.alias === 'DocumentEmployment') {
          PersonDocumentEmployment.query({ id: $scope.application.lotId })
            .$promise.then(function (employments) {
              $scope.documentPart = employments;
              $scope.showDocumentEmployment = !!employments;
            });
        }
        else if ($scope.docPartType.alias === 'DocumentMed') {
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
        else if ($scope.docPartType.alias === 'DocumentCheck') {
          PersonDocumentCheck.query({ id: $scope.application.lotId })
            .$promise.then(function (checks) {
              $scope.documentPart = checks;
              $scope.showDocumentCheck = !!checks;
            });
        }
        else if ($scope.docPartType.alias === 'DocumentTraining') {
          PersonDocumentTraining.query({ id: $scope.application.lotId })
            .$promise.then(function (documentTrainings) {
              $scope.documentPart = documentTrainings;
              $scope.showDocumentTraining = !!documentTrainings;
            });
        }
        else if ($scope.docPartType.alias === 'DocumentOther') {
          PersonDocumentOther.query({ id: $scope.application.lotId })
            .$promise.then(function (documentOthers) {
              $scope.documentPart = documentOthers;
              $scope.showDocumentOther = !!documentOthers;
            });
        }
        else if ($scope.docPartType.alias === 'DocumentApplication') {
          PersonDocumentApplication.query({ id: $scope.application.lotId })
            .$promise.then(function (documentApplications) {
              $scope.documentPart = documentApplications;
              $scope.showDocumentApplication = !!documentApplications;
            });
        }
      }
    };

    $scope.linkPart = function (item) {
      return Application
        .partslinkExisting({ id: $stateParams.id }, {
          docFileKey: $stateParams.docFileKey,
          setPartAlias: $scope.docPartType.alias,
          partIndex: item.partIndex
        }).$promise.then(function () {
          return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
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
    'PersonDocumentApplication'
  ];

  angular.module('gva').controller('ApplicationsEditLinkPartCtrl', ApplicationsEditLinkPartCtrl);
}(angular
));
