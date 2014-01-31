/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditLinkPartCtrl(
    $scope,
    $state,
    $stateParams,
    ApplicationDocPart,
    PersonDocumentId,
    PersonDocumentEducation,
    PersonDocumentEmployment,
    PersonDocumentMedical,
    PersonDocumentCheck
    ) {

    $scope.search = function () {
      $scope.showDocumentId = false;
      $scope.showDocumentEducation = false;
      $scope.showDocumentEmployment = false;
      $scope.showDocumentMed = false;
      $scope.showDocumentCheck = false;

      if ($scope.documentData.docPartType) {
        if ($scope.documentData.docPartType.content.setPartId === 1) {
          PersonDocumentId.query($stateParams).$promise.then(function (documentIds) {
            $scope.wrapper.documentPart = documentIds;
            $scope.showDocumentId = !!documentIds;
          });
        }
        else if ($scope.documentData.docPartType.content.setPartId === 2) {
          PersonDocumentEducation.query($stateParams).$promise.then(function (documentEducations) {
            $scope.wrapper.documentPart = documentEducations;
            $scope.showDocumentEducation = !!documentEducations;
          });
        }
        else if ($scope.documentData.docPartType.content.setPartId === 3) {
          PersonDocumentEmployment.query($stateParams).$promise.then(function (employments) {
            $scope.wrapper.documentPart = employments;
            $scope.showDocumentEmployment = !!employments;
          });
        }
        else if ($scope.documentData.docPartType.content.setPartId === 4) {
          PersonDocumentMedical.query($stateParams).$promise.then(function (medicals) {
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
        else if ($scope.documentData.docPartType.content.setPartId === 5) {
          PersonDocumentCheck.query($stateParams).$promise.then(function (checks) {
            $scope.wrapper.documentPart = checks;
            $scope.showDocumentCheck = !!checks;
          });
        }

        //todo add more
      }
    };

    $scope.linkPart = function (item) {
      return ApplicationDocPart
        .linkExisting({
          id: $stateParams.id,
          setPartId: $scope.documentData.docPartType.content.setPartId
        }, {
          personId: $scope.application.person.id,
          currentDocId: $scope.documentData.currentDocId,
          partIndex: item.partIndex,
          docFileId: $scope.documentData.docFiles[0].key
        }).$promise.then(function () {
          return $state.transitionTo('applications/edit/case', $stateParams, { reload: true });
        });

    };

    $scope.cancel = function () {
      $scope.documentData = null;
      return $state.transitionTo('applications/edit/case', $stateParams, { reload: true });
    };
  }

  ApplicationsEditLinkPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'ApplicationDocPart',
    'PersonDocumentId',
    'PersonDocumentEducation',
    'PersonDocumentEmployment',
    'PersonDocumentMedical',
    'PersonDocumentCheck'
  ];

  angular.module('gva').controller('ApplicationsEditLinkPartCtrl', ApplicationsEditLinkPartCtrl);
}(angular
));
