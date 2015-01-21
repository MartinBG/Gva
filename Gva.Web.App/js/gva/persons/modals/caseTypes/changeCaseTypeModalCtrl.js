/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChangeCaseTypeModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    PersonDocumentEducations,
    PersonDocumentTrainings,
    PersonDocumentMedicals,
    PersonDocumentChecks,
    PersonDocumentOthers,
    Applications,
    PersonDocumentEmployments,
    PersonLicenceEditions,
    PersonDocumentLangCerts,
    partData,
    lotCaseTypes
  ) {
    $scope.partData = partData;

    $scope.caseTypes = _.filter(lotCaseTypes, function (caseType) {
      return $scope.partData['case'].caseType.nomValueId !== caseType.nomValueId;
    });

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectCaseType = function (caseType) {
      $scope.partData['case'].caseType = caseType;
      var resource,
        promise;

      if(scModalParams.setPartAlias === 'personApplication') {
        promise = Applications
              .editAppPart({
                lotId: scModalParams.lotId,
                ind: scModalParams.partIndex
              }, $scope.partData)
              .$promise;
      } else {
        switch (scModalParams.setPartAlias) {
          case 'personEducation':
            resource = PersonDocumentEducations;
            break;
          case 'personTraining':
            resource = PersonDocumentTrainings;
            break;
          case 'personMedical':
            resource = PersonDocumentMedicals;
            break;
          case 'personCheck':
            resource = PersonDocumentChecks;
            break;
          case 'personOther':
            resource = PersonDocumentOthers;
            break;
          case 'personEmployment':
            resource = PersonDocumentEmployments;
            break;
          case 'personLicence':
            resource = PersonLicenceEditions;
            break;
          case 'personLangCert':
            resource = PersonDocumentLangCerts;
        }
        promise = resource
          .save({
            id: scModalParams.lotId,
            ind: scModalParams.partIndex 
          }, $scope.partData)
          .$promise;
      }
      return promise.then(function () {
        return $modalInstance.close(caseType);
      });
    };
  }

  ChangeCaseTypeModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'PersonDocumentEducations',
    'PersonDocumentTrainings',
    'PersonDocumentMedicals',
    'PersonDocumentChecks',
    'PersonDocumentOthers',
    'Applications',
    'PersonDocumentEmployments',
    'PersonLicenceEditions',
    'PersonDocumentLangCerts',
    'partData',
    'lotCaseTypes'
  ];

  ChangeCaseTypeModalCtrl.$resolve = {
    lotCaseTypes: [
      '$q',
      'Nomenclatures',
      'scModalParams',
      function ($q, Nomenclatures, scModalParams) {
        return Nomenclatures.query({
          alias: 'caseTypes',
          lotId: scModalParams.lotId
        }).$promise;
      }
    ],
    partData: [
      'PersonDocumentEducations',
      'PersonDocumentTrainings',
      'PersonDocumentMedicals',
      'PersonDocumentChecks',
      'PersonDocumentOthers',
      'Applications',
      'PersonDocumentEmployments',
      'PersonLicenceEditions',
      'PersonDocumentLangCerts',
      'scModalParams',
      function (
        PersonDocumentEducations,
        PersonDocumentTrainings,
        PersonDocumentMedicals,
        PersonDocumentChecks,
        PersonDocumentOthers,
        Applications,
        PersonDocumentEmployments,
        PersonLicenceEditions,
        PersonDocumentLangCerts,
        scModalParams) {
        var params;

        if(scModalParams.setPartAlias === 'personLicence') {
          params = {
            id: scModalParams.lotId,
            ind: scModalParams.parentPartIndex,
            index: scModalParams.partIndex
          };
        } else if (scModalParams.setPartAlias === 'personApplication') {
          params = { 
            lotId: scModalParams.lotId,
            ind: scModalParams.partIndex,
            id: scModalParams.applicationId
          };
        } else {
          params = {
            id: scModalParams.lotId,
            ind: scModalParams.partIndex
          };
        }

        switch (scModalParams.setPartAlias) {
          case 'personEducation':
            return PersonDocumentEducations.get(params).$promise;
          case 'personTraining':
            return PersonDocumentTrainings.get(params).$promise;
          case 'personMedical':
            return PersonDocumentMedicals.get(params).$promise;
          case 'personCheck':
            return PersonDocumentChecks.get(params).$promise;
          case 'personOther':
            return PersonDocumentOthers.get(params).$promise;
          case 'personApplication':
            return Applications.getAppPart(params).$promise;
          case 'personEmployment':
            return PersonDocumentEmployments.get(params).$promise;
          case 'personLicence':
            return PersonLicenceEditions.get(params).$promise;
          case 'personLangCert':
            return PersonDocumentLangCerts.get(params).$promise;
        }
      }
    ]
  };

  angular.module('gva').controller('ChangeCaseTypeModalCtrl', ChangeCaseTypeModalCtrl);
}(angular, _));
