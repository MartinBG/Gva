/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditMedicalsCtrl(
    $scope,
    $q,
    $state,
    $stateParams,
    Persons,
    PersonLicenceEditions,
    PersonDocumentMedicals,
    currentLicenceEdition,
    licenceEditions,
    scMessage,
    scModal
  ) {
    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;
    $scope.currentLicenceEdition.part.includedMedicals =
      $scope.currentLicenceEdition.part.includedMedicals || [];
    $q.all([
      Persons.get({ id: $stateParams.id }).$promise,
      PersonDocumentMedicals.query({ id: $stateParams.id }).$promise
    ]).then(function (results) {
      $scope.person = results[0];
      var medicals = results[1];

      $scope.includedMedicals = 
        _.map($scope.currentLicenceEdition.part.includedMedicals, function (partIndex) {
          return _.where(medicals, { partIndex: partIndex })[0];
        });
    });

    $scope.addMedical = function () {
      var modalInstance = scModal.open('newMedical', {
        person: $scope.person,
        caseTypeId: $stateParams.caseTypeId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newMedical) {
        $scope.includedMedicals.push(newMedical);
        $scope.currentLicenceEdition.part.includedMedicals.push(newMedical.partIndex);
        $scope.save();
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
    'Persons',
    'PersonLicenceEditions',
    'PersonDocumentMedicals',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];


  angular.module('gva')
    .controller('LicenceEditionsEditMedicalsCtrl', LicenceEditionsEditMedicalsCtrl);
}(angular, _));
