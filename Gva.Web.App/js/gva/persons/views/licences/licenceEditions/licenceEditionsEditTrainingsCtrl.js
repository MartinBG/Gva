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

    PersonDocumentTrainings
      .query({ id: $stateParams.id })
      .$promise
      .then(function (trainings) {
        $scope.includedTrainings = 
          _.map($scope.currentLicenceEdition.part.includedTrainings, function (training) {
            var includedTraining = _.where(trainings, { partIndex: training.partIndex })[0];
            includedTraining.orderNum = training.orderNum;
            return includedTraining;
          });

        $scope.includedTrainings = _.sortBy($scope.includedTrainings, 'orderNum');
      });

    $scope.currentLicenceEdition = currentLicenceEdition;

    $scope.addTraining = function () {
      var modalInstance = scModal.open('newTraining', {
        lotId: $stateParams.id,
        caseTypeId: $stateParams.caseTypeId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newTraining) {
        var lastOrderNum = 0,
          lastTraining = _.last($scope.includedTrainings);
        if (lastTraining) {
          lastOrderNum = _.last($scope.includedTrainings).orderNum;
        }

        newTraining.orderNum = ++lastOrderNum;
        $scope.includedTrainings.push(newTraining);

        $scope.currentLicenceEdition.part.includedTrainings =
          _.map($scope.includedTrainings, function(training) {
            return {
              orderNum: training.orderNum,
              partIndex: training.partIndex
            };
          });
        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.addExistingTraining = function () {
      var modalInstance = scModal.open('chooseTrainings', {
        includedTrainings:
          _.pluck($scope.currentLicenceEdition.part.includedTrainings, 'partIndex'),
        lotId: $stateParams.id
      });

      modalInstance.result.then(function (selectedTrainings) {
        var lastOrderNum = 0,
          lastTraining = _.last($scope.includedTrainings);
        if (lastTraining) {
          lastOrderNum = _.last($scope.includedTrainings).orderNum;
        }

        _.forEach(selectedTrainings, function(training) {
          var newlyAddedTraining = {
            orderNum: ++lastOrderNum,
            partIndex: training.partIndex
          };
          $scope.currentLicenceEdition.part.includedTrainings.push(newlyAddedTraining);

          training.orderNum = newlyAddedTraining.orderNum;
          $scope.includedTrainings.push(training);
        });

        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.removeTraining = function (training) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedTrainings = _.without($scope.includedTrainings, training);
            _.remove($scope.currentLicenceEdition.part.includedTrainings,
              function(includedTraining) {
                return training.partIndex === includedTraining.partIndex;
              });
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
      $scope.currentLicenceEdition.part.includedTrainings = [];
      _.forEach($scope.includedTrainings, function (training) {
        var changedTraining = {
          orderNum: training.orderNum,
          partIndex: training.partIndex
        };
        $scope.currentLicenceEdition.part.includedTrainings.push(changedTraining);
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
