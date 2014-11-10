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

    $q.all([
      Persons.get({ id: $stateParams.id }).$promise,
      PersonDocumentMedicals.query({ id: $stateParams.id }).$promise
    ]).then(function (results) {
      $scope.person = results[0];
      var medicals = results[1];

      $scope.includedMedicals = 
        _.map($scope.currentLicenceEdition.part.includedMedicals, function (medical) {
          var includedMedical = _.where(medicals, { partIndex: medical.partIndex })[0];
          includedMedical.orderNum = medical.orderNum;
          return includedMedical;
        });
      $scope.includedMedicals = _.sortBy($scope.includedMedicals, 'orderNum');
    });

    $scope.addMedical = function () {
      var modalInstance = scModal.open('newMedical', {
        person: $scope.person,
        caseTypeId: $stateParams.caseTypeId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newMedical) {
        var lastOrderNum = 0,
          lastMedical = _.last($scope.includedMedicals);
        if (lastMedical) {
          lastOrderNum = _.last($scope.includedMedicals).orderNum;
        }

        newMedical.orderNum = ++lastOrderNum;
        $scope.includedMedicals.push(newMedical);

        $scope.currentLicenceEdition.part.includedMedicals =
          _.map($scope.includedMedicals, function(medical) {
            return {
              orderNum: medical.orderNum,
              partIndex: medical.partIndex
            };
          });
        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.addExistingMedical = function () {
      var modalInstance = scModal.open('chooseMedicals', {
        includedMedicals: _.pluck($scope.currentLicenceEdition.part.includedMedicals, 'partIndex'),
        person: $scope.person,
        lotId: $stateParams.id
      });

      modalInstance.result.then(function (selectedMedicals) {
        var lastOrderNum = 0,
          lastCert = _.last($scope.includedMedicals);
        if (lastCert) {
          lastOrderNum = _.last($scope.includedMedicals).orderNum;
        }

        _.forEach(selectedMedicals, function(medical) {
          var newlyAddedMedical = {
            orderNum: ++lastOrderNum,
            partIndex: medical.partIndex
          };
          $scope.currentLicenceEdition.part.includedMedicals.push(newlyAddedMedical);

          medical.orderNum = newlyAddedMedical.orderNum;
          $scope.includedMedicals.push(medical);
        });

        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.removeMedical = function (medical) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedMedicals = _.without($scope.includedMedicals, medical);
            _.remove($scope.currentLicenceEdition.part.includedMedicals,
              function(includedMedical) {
                return medical.partIndex === includedMedical.partIndex;
              });
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
      $scope.currentLicenceEdition.part.includedMedicals = [];
      _.forEach($scope.includedMedicals, function (medical) {
        var changedMedical = {
          orderNum: medical.orderNum,
          partIndex: medical.partIndex
        };
        $scope.currentLicenceEdition.part.includedMedicals.push(changedMedical);
      });
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
