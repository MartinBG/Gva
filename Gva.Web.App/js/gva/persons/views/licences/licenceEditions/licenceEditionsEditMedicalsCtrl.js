/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditMedicalsCtrl(
    $scope,
    $q,
    $state,
    $stateParams,
    PersonLicenceEditions,
    PersonDocumentMedicals,
    currentLicenceEdition,
    licenceEditions,
    includedMedicals,
    person,
    scMessage,
    scModal
  ) {
    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;
    $scope.currentLicenceEdition.part.includedMedicals =
      $scope.currentLicenceEdition.part.includedMedicals || [];
    $scope.includedMedicals = includedMedicals;
    $scope.person = person;

    $scope.addMedical = function () {
      var modalInstance = scModal.open('newMedical', {
        person: $scope.person,
        caseTypeId: $scope.currentLicenceEdition.cases[0].caseType.nomValueId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newMedical) {
        $scope.currentLicenceEdition.part.includedMedicals.push(newMedical.partIndex);
        $scope.save();
        PersonDocumentMedicals.getMedicalView({
          id: $stateParams.id,
          ind: newMedical.partIndex
        }).$promise
          .then(function(medical) {
            $scope.includedMedicals.push(medical);
          });
      });

      return modalInstance.opened;
    };

    $scope.addExistingMedical = function () {
      var modalInstance = scModal.open('chooseMedicals', {
        includedMedicals: $scope.currentLicenceEdition.part.includedMedicals,
        person: $scope.person,
        lotId: $stateParams.id,
        caseTypeId: $scope.currentLicenceEdition.cases[0].caseType.nomValueId
      });

      modalInstance.result.then(function (selectedMedicals) {
        $scope.includedMedicals = $scope.includedMedicals.concat(selectedMedicals);

        $scope.currentLicenceEdition.part.includedMedicals = 
          $scope.currentLicenceEdition.part.includedMedicals
          .concat(_.pluck(selectedMedicals, 'partIndex'));

        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.removeMedical = function (medical) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedMedicals = _.without($scope.includedMedicals, medical);
            $scope.currentLicenceEdition.part.includedMedicals =
              _.pluck($scope.includedMedicals, 'partIndex');
            $scope.save();
          }
        });
    };

    $scope.changeOrder = function () {
      $scope.changeOrderMode = true;
    };

    $scope.saveOrder = function () {
      $scope.includedMedicals = _.sortBy($scope.includedMedicals, 'orderNum');
      $scope.changeOrderMode = false;
      $scope.currentLicenceEdition.part.includedMedicals =
        _.pluck($scope.includedMedicals, 'partIndex');
      return $scope.save();
    }; 

    $scope.cancelChangeOrder = function () {
      $scope.changeOrderMode = false;
    };

    $scope.save = function () {
      return PersonLicenceEditions
        .save({
          id: $stateParams.id,
          ind: $stateParams.ind,
          index: $stateParams.index,
          caseTypeId: $scope.caseTypeId
        }, $scope.currentLicenceEdition)
        .$promise;
    };
  }

  LicenceEditionsEditMedicalsCtrl.$inject = [
    '$scope',
    '$q',
    '$state',
    '$stateParams',
    'PersonLicenceEditions',
    'PersonDocumentMedicals',
    'currentLicenceEdition',
    'licenceEditions',
    'includedMedicals',
    'person',
    'scMessage',
    'scModal'
  ];

  LicenceEditionsEditMedicalsCtrl.$resolve = {
    includedMedicals: [
      '$stateParams',
      'PersonDocumentMedicals',
      'currentLicenceEdition',
      function ($stateParams, PersonDocumentMedicals, currentLicenceEdition) {
        return  PersonDocumentMedicals
          .query({ id: $stateParams.id })
          .$promise
          .then(function (medicals) {
            return _.map(currentLicenceEdition.part.includedMedicals, function (partIndex) {
              return _.where(medicals, { partIndex: partIndex })[0];
            });
          });
       }
    ],
    person: [
      '$stateParams',
      'Persons',
      function($stateParams, Persons) {
        return Persons.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('LicenceEditionsEditMedicalsCtrl', LicenceEditionsEditMedicalsCtrl);
}(angular, _));
