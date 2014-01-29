/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditLinkPartCtrl(
    $scope,
    $state,
    $stateParams,
    ApplicationDocFile,
    PersonDocumentId,
    PersonDocumentEducation,
    PersonDocumentEmployment,
    PersonDocumentMedical,
    PersonDocumentCheck
    ) {

    if ($scope.$parent.docFileType) {
      if ($scope.$parent.docFileType.alias === 'DocumentId') {
        PersonDocumentId.query($stateParams).$promise.then(function (documentIds) {
          $scope.documentPart = documentIds;
        });
      }
      else if ($scope.$parent.docFileType.alias === 'DocumentEducation') {
        PersonDocumentEducation.query($stateParams).$promise.then(function (documentEducations) {
          $scope.documentPart = documentEducations;
        });
      }
      else if ($scope.$parent.docFileType.alias === 'DocumentEmployment') {
        PersonDocumentEmployment.query($stateParams).$promise.then(function (employments) {
          $scope.documentPart = employments;
        });
      }
      else if ($scope.$parent.docFileType.alias === 'DocumentMed') {
        PersonDocumentMedical.query($stateParams).$promise.then(function (medicals) {
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
        });
      }
      else if ($scope.$parent.docFileType.alias === 'DocumentCheck') {
        PersonDocumentCheck.query($stateParams).$promise.then(function (checks) {
          $scope.documentPart = checks;
        });
      }

      //todo add more
    }

    $scope.linkPart = function (item) {
      return ApplicationDocFile
        .linkExisting({ id: $stateParams.id, docFileId: $scope.$parent.docFileId }, {
          personId: $scope.application.person.id,
          currentDocId: $scope.$parent.currentDocId,
          partIndex: item.partIndex
        }
        ).$promise.then(function () {
          $scope.documentPart = null;
          return $state.transitionTo('applications/edit/case', $stateParams, { reload: true });
        });

    };

    $scope.goBack = function () {
      return $state.go('applications/edit/case');
    };
  }

  ApplicationsEditLinkPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'ApplicationDocFile',
    'PersonDocumentId',
    'PersonDocumentEducation',
    'PersonDocumentEmployment',
    'PersonDocumentMedical',
    'PersonDocumentCheck'
  ];

  angular.module('gva').controller('ApplicationsEditLinkPartCtrl', ApplicationsEditLinkPartCtrl);
}(angular
));
