/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditTrainingsCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicenceEditions,
    PersonDocumentTrainings,
    currentLicenceEdition,
    licenceEditions,
    scMessage,
    scModal
  ) {
    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;
    $scope.currentLicenceEdition.part.includedTrainings =
      $scope.currentLicenceEdition.part.includedTrainings || [];

    PersonDocumentTrainings
      .query({ id: $stateParams.id })
      .$promise
      .then(function (trainings) {
        $scope.includedTrainings = 
          _.map($scope.currentLicenceEdition.part.includedTrainings, function (partIndex) {
            return _.where(trainings, { partIndex: partIndex })[0];
          });
      });

    $scope.currentLicenceEdition = currentLicenceEdition;

    $scope.addTraining = function () {
      var modalInstance = scModal.open('newTraining', {
        lotId: $stateParams.id,
        caseTypeId: $stateParams.caseTypeId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newTraining) {
        $scope.includedTrainings.push(newTraining);
        $scope.currentLicenceEdition.part.includedTrainings.push(newTraining.partIndex);
        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.addExistingTraining = function () {
      var modalInstance = scModal.open('chooseTrainings', {
        includedTrainings: $scope.currentLicenceEdition.part.includedTrainings,
        lotId: $stateParams.id
      });

      modalInstance.result.then(function (selectedTrainings) {
        $scope.includedTrainings = $scope.includedTrainings.concat(selectedTrainings);

        $scope.currentLicenceEdition.part.includedTrainings = 
          $scope.currentLicenceEdition.part.includedTrainings
          .concat(_.pluck(selectedTrainings, 'partIndex'));

        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.removeTraining = function (training) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedTrainings = _.without($scope.includedTrainings, training);
            $scope.currentLicenceEdition.part.includedTrainings =
              _.pluck($scope.includedTrainings, 'partIndex');
            $scope.save();
          }
        });
    };

    $scope.changeOrder = function () {
      $scope.changeOrderMode = true;
    };

    $scope.saveOrder = function () {
      $scope.includedTrainings = _.sortBy($scope.includedTrainings, 'orderNum');
      $scope.changeOrderMode = false;
      $scope.currentLicenceEdition.part.includedTrainings =
        _.pluck($scope.includedTrainings, 'partIndex');
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

  LicenceEditionsEditTrainingsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonLicenceEditions',
    'PersonDocumentTrainings',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];


  angular.module('gva')
    .controller('LicenceEditionsEditTrainingsCtrl', LicenceEditionsEditTrainingsCtrl);
}(angular, _));
