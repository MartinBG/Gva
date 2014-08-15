﻿/*global angular, _, moment*/
(function (angular, _, moment) {
  'use strict';

  function PersonLicenceEditionCtrl(
    $scope,
    scModal,
    $q,
    scMessage,
    Persons,
    PersonRatings,
    PersonDocumentTrainings,
    PersonDocumentChecks,
    PersonDocumentMedicals,
    PersonLicences,
    scFormParams
  ) {
    $scope.lotId = scFormParams.lotId;

    $q.all([
      Persons.get({ id: scFormParams.lotId }).$promise,
      PersonRatings.query({ id: scFormParams.lotId }).$promise,
      PersonDocumentTrainings.query({ id: scFormParams.lotId }).$promise,
      PersonDocumentChecks.query({ id: scFormParams.lotId }).$promise,
      PersonDocumentMedicals.query({ id: scFormParams.lotId }).$promise,
      PersonLicences.query({ id: scFormParams.lotId }).$promise
    ]).then(function (results) {
      $scope.person = results[0];
      var ratings = results[1],
          trainings = results[2],
          checks = results[3],
          medicals = results[4],
          licences = results[5];

      var unbindWatcher = $scope.$watch('model', function () {
        if (!$scope.model) {
          return;
        }

        $scope.model.includedRatings = $scope.model.includedRatings || [];
        $scope.includedRatings = _.map($scope.model.includedRatings, function (ind) {
          return _.find(ratings, { partIndex: ind });
        });
        $scope.$watchCollection('includedRatings', function () {
          $scope.model.includedRatings = _.pluck($scope.includedRatings, 'partIndex');
        });

        $scope.model.includedTrainings = $scope.model.includedTrainings || [];
        $scope.includedTrainings = _.map($scope.model.includedTrainings, function (ind) {
          return _.find(trainings, { partIndex: ind });
        });
        $scope.$watchCollection('includedTrainings', function () {
          $scope.model.includedTrainings = _.pluck($scope.includedTrainings, 'partIndex');
        });

        $scope.model.includedChecks = $scope.model.includedChecks || [];
        $scope.includedChecks = _.map($scope.model.includedChecks, function (ind) {
          return _.where(checks, { partIndex: ind })[0];
        });
        $scope.$watchCollection('includedChecks', function () {
          $scope.model.includedChecks = _.pluck($scope.includedChecks, 'partIndex');
        });

        $scope.model.includedMedicals = $scope.model.includedMedicals || [];
        $scope.includedMedicals = _.map($scope.model.includedMedicals, function (ind) {
          return _.find(medicals, { partIndex: ind });
        });
        $scope.$watchCollection('includedMedicals', function () {
          $scope.model.includedMedicals = _.pluck($scope.includedMedicals, 'partIndex');
        });

        $scope.model.includedLicences = $scope.model.includedLicences || [];
        $scope.includedLicences = _.map($scope.model.includedLicences, function (ind) {
          return _.find(licences, { partIndex: ind });
        });
        $scope.$watchCollection('includedLicences', function () {
          $scope.model.includedLicences = _.pluck($scope.includedLicences, 'partIndex');
        });

        // removing the watcher after the model have been set
        unbindWatcher();
      });
    });

    $scope.isNew = scFormParams.isNew;
    if($scope.isNew && $scope.model.documentDateValidFrom === undefined){
      $scope.model.documentDateValidFrom = moment(new Date());
    }
    
    $scope.addRating = function () {
      var modalInstance = scModal.open('newRating', { lotId: scFormParams.lotId });

      modalInstance.result.then(function (newRating) {
        $scope.includedRatings.push(newRating);
      });

      return modalInstance.opened;
    };

    $scope.addExistingRating = function () {
      var modalInstance = scModal.open('chooseRatings', {
        includedRatings: $scope.model.includedRatings,
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedRatings) {
        $scope.includedRatings = $scope.includedRatings.concat(selectedRatings);
      });

      return modalInstance.opened;
    };

    $scope.removeRating = function (rating) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedRatings = _.without($scope.includedRatings, rating);
          }
        });
    };

    $scope.addTraining = function () {
      var modalInstance = scModal.open('newTraining', {
        lotId: scFormParams.lotId,
        caseTypeId: scFormParams.caseTypeId
      });

      modalInstance.result.then(function (newTraining) {
        $scope.includedTrainings.push(newTraining);
      });

      return modalInstance.opened;
    };

    $scope.addExistingTraining = function () {
      var modalInstance = scModal.open('chooseTrainings', {
        includedTrainings: $scope.model.includedTrainings,
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedTrainings) {
        $scope.includedTrainings = $scope.includedTrainings.concat(selectedTrainings);
      });

      return modalInstance.opened;
    };

    $scope.removeTraining = function (training) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedTrainings = _.without($scope.includedTrainings, training);
          }
        });
    };

    $scope.addCheck = function () {
      var modalInstance = scModal.open('newCheck', {
        lotId: scFormParams.lotId,
        caseTypeId: scFormParams.caseTypeId
      });

      modalInstance.result.then(function (newCheck) {
        $scope.includedChecks.push(newCheck);
      });

      return modalInstance.opened;
    };

    $scope.addExistingCheck = function () {
      var modalInstance = scModal.open('chooseChecks', {
        includedChecks: $scope.model.includedChecks,
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedChecks) {
        $scope.includedChecks = $scope.includedChecks.concat(selectedChecks);
      });

      return modalInstance.opened;
    };

    $scope.removeCheck = function (check) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedChecks = _.without($scope.includedChecks, check);
          }
        });
    };

    $scope.addMedical = function () {
      var modalInstance = scModal.open('newMedical', {
        person: $scope.person,
        caseTypeId: scFormParams.caseTypeId
      });

      modalInstance.result.then(function (newMedical) {
        $scope.includedMedicals.push(newMedical);
      });

      return modalInstance.opened;
    };

    $scope.addExistingMedical = function () {
      var modalInstance = scModal.open('chooseMedicals', {
        includedMedicals: $scope.model.includedMedicals,
        person: $scope.person,
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedMedicals) {
        $scope.includedMedicals = $scope.includedMedicals.concat(selectedMedicals);
      });

      return modalInstance.opened;
    };

    $scope.removeMedical = function (medical) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedMedicals = _.without($scope.includedMedicals, medical);
          }
        });
    };

    $scope.addExistingLicence = function () {
      var hideLicences = _.clone($scope.model.includedLicences);

      if (scFormParams.partIndex) {
        hideLicences.push(parseInt(scFormParams.partIndex, 10));
      }

      var modalInstance = scModal.open('chooseLicences', {
        includedLicences: hideLicences,
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedLicences) {
        $scope.includedLicences = $scope.includedLicences.concat(selectedLicences);
      });

      return modalInstance.opened;
    };

    $scope.removeLicence = function (licence) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedLicences = _.without($scope.includedLicences, licence);
          }
        });
    };
  }

  PersonLicenceEditionCtrl.$inject = [
    '$scope',
    'scModal',
    '$q',
    'scMessage',
    'Persons',
    'PersonRatings',
    'PersonDocumentTrainings',
    'PersonDocumentChecks',
    'PersonDocumentMedicals',
    'PersonLicences',
    'scFormParams'
  ];

  angular.module('gva').controller('PersonLicenceEditionCtrl', PersonLicenceEditionCtrl);
}(angular, _, moment));
