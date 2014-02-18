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
    PersonDocumentTraining
    ) {
    $scope.docFileKey = $stateParams.docFileKey;

    $scope.search = function () {
      $scope.showDocumentId = false;
      $scope.showDocumentEducation = false;
      $scope.showDocumentEmployment = false;
      $scope.showDocumentMed = false;
      $scope.showDocumentCheck = false;
      $scope.showDocumentTraining = false;

      if ($scope.wrapper.docPartType) {
        if ($scope.wrapper.docPartType.alias === 'DocumentId') {
          PersonDocumentId.query({ id: $scope.application.lotId })
            .$promise.then(function (documentIds) {
              $scope.wrapper.documentPart = documentIds;
              $scope.showDocumentId = !!documentIds;
            });
        }
        else if ($scope.wrapper.docPartType.alias === 'DocumentEducation') {
          PersonDocumentEducation.query({ id: $scope.application.lotId })
            .$promise.then(function (documentEducations) {
              $scope.wrapper.documentPart = documentEducations;
              $scope.showDocumentEducation = !!documentEducations;
            });
        }
        else if ($scope.wrapper.docPartType.alias === 'DocumentEmployment') {
          PersonDocumentEmployment.query({ id: $scope.application.lotId })
            .$promise.then(function (employments) {
              $scope.wrapper.documentPart = employments;
              $scope.showDocumentEmployment = !!employments;
            });
        }
        else if ($scope.wrapper.docPartType.alias === 'DocumentMed') {
          PersonDocumentMedical.query({ id: $scope.application.lotId })
            .$promise.then(function (medicals) {
              $scope.wrapper.documentPart = medicals.map(function (medical) {
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
        else if ($scope.wrapper.docPartType.alias === 'DocumentCheck') {
          PersonDocumentCheck.query({ id: $scope.application.lotId })
            .$promise.then(function (checks) {
              $scope.wrapper.documentPart = checks;
              $scope.showDocumentCheck = !!checks;
            });
        }
        else if ($scope.wrapper.docPartType.alias === 'DocumentTraining') {
          PersonDocumentTraining.query({ id: $scope.application.lotId })
            .$promise.then(function (documentTrainings) {
              $scope.wrapper.documentPart = documentTrainings;
              $scope.showDocumentTraining = !!documentTrainings;
            });
        }

        //todo add more
      }
    };

    $scope.linkPart = function (item) {
      return Application
        .partslinkExisting({ id: $stateParams.id }, {
          docFileKey: $stateParams.docFileKey,
          setPartAlias: $scope.wrapper.docPartType.alias,
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
    'PersonDocumentTraining'
  ];

  angular.module('gva').controller('ApplicationsEditLinkPartCtrl', ApplicationsEditLinkPartCtrl);
}(angular
));
