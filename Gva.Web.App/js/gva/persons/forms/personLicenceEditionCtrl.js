/*global angular, _, moment*/
(function (angular, _, moment) {
  'use strict';

  function PersonLicenceEditionCtrl(
    $scope,
    $state,
    $stateParams,
    namedModal,
    $q,
    Persons,
    PersonRatings,
    PersonDocumentTrainings,
    PersonDocumentChecks,
    PersonDocumentMedicals,
    PersonLicences
  ) {
    $q.all([
      Persons.get({ id: $stateParams.id }).$promise,
      PersonRatings.query({ id: $stateParams.id }).$promise,
      PersonDocumentTrainings.query({ id: $stateParams.id }).$promise,
      PersonDocumentChecks.query({ id: $stateParams.id }).$promise,
      PersonDocumentMedicals.query({ id: $stateParams.id }).$promise,
      PersonLicences.query({ id: $stateParams.id }).$promise
    ]).then(function (results) {
      $scope.person = results[0];
      $scope.ratings = results[1];
      $scope.trainings = results[2];
      $scope.checks = results[3];
      $scope.medicals = results[4];
      $scope.licences = results[5];

      $scope.$watch('model', function () {
        if (!$scope.model) {
          return;
        }

        $scope.model.includedRatings = $scope.model.includedRatings || [];
        $scope.model.includedTrainings = $scope.model.includedTrainings || [];
        $scope.model.includedChecks = $scope.model.includedChecks || [];
        $scope.model.includedMedicals = $scope.model.includedMedicals || [];
        $scope.model.includedLicences = $scope.model.includedLicences || [];
      });

      var unbindWatcher = $scope.$watch('model', function () {
        if (!$scope.model) {
          return;
        }

        $scope.$watchCollection('model.includedRatings', function () {
          if (!$scope.model) {
            return;
          }

          $scope.includedRatings = _.map($scope.model.includedRatings, function (ind) {
            return _.find($scope.ratings, { partIndex: ind });
          });
        });

        $scope.$watchCollection('model.includedTrainings', function () {
          if (!$scope.model) {
            return;
          }

          $scope.includedTrainings = _.map($scope.model.includedTrainings, function (ind) {
            return _.find($scope.trainings, { partIndex: ind });
          });
        });

        $scope.$watchCollection('model.includedChecks', function () {
          if (!$scope.model) {
            return;
          }

          $scope.includedChecks = _.map($scope.model.includedChecks, function (ind) {
            return _.where($scope.checks, { partIndex: ind })[0];
          });
        });

        $scope.$watchCollection('model.includedMedicals', function () {
          if (!$scope.model) {
            return;
          }

          $scope.includedMedicals = _.map($scope.model.includedMedicals, function (ind) {
            return _.find($scope.medicals, { partIndex: ind });
          });
        });

        $scope.$watchCollection('model.includedLicences', function () {
          if (!$scope.model) {
            return;
          }

          $scope.includedLicences = _.map($scope.model.includedLicences, function (ind) {
            return _.find($scope.licences, { partIndex: ind });
          });
        });

        // removing the watcher after the model have been set
        unbindWatcher();
      });
    });

    $scope.$watch('isNew', function(){
      if($scope.isNew && $scope.model.documentDateValidFrom === undefined){
        $scope.model.documentDateValidFrom = moment(new Date());
      }
    });
    
    $scope.addRating = function () {
      var modalInstance = namedModal.open('newRating');

      modalInstance.result.then(function (newRatingIndex) {
        PersonRatings.query({ id: $stateParams.id }).$promise.then(function (ratings) {
          $scope.ratings = ratings;
          $scope.model.includedRatings.push(newRatingIndex);
        });
      });

      return modalInstance.opened;
    };

    $scope.addTraining = function () {
      var modalInstance = namedModal.open('newTraining');

      modalInstance.result.then(function (newTrainingIndex) {
        PersonDocumentTrainings.query({ id: $stateParams.id }).$promise.then(function (trainings) {
          $scope.trainings = trainings;
          $scope.model.includedTrainings.push(newTrainingIndex);
        });
      });

      return modalInstance.opened;
    };

    $scope.addCheck = function () {
      var modalInstance = namedModal.open('newCheck');

      modalInstance.result.then(function (newCheckIndex) {
        PersonDocumentChecks.query({ id: $stateParams.id }).$promise.then(function (checks) {
          $scope.checks = checks;
          $scope.model.includedChecks.push(newCheckIndex);
        });
      });

      return modalInstance.opened;
    };

    $scope.addMedical = function () {
      var modalInstance = namedModal.open('newMedical');

      modalInstance.result.then(function (newMedicalIndex) {
        PersonDocumentMedicals.query({ id: $stateParams.id }).$promise.then(function (medicals) {
          $scope.medicals = medicals;
          $scope.model.includedMedicals.push(newMedicalIndex);
        });
      });

      return modalInstance.opened;
    };

    $scope.addExistingRating = function () {
      var modalInstance = namedModal.open('chooseRatings', {
        includedRatings: $scope.model.includedRatings
      });

      modalInstance.result.then(function (selectedRatings) {
        PersonRatings.query({ id: $stateParams.id }).$promise.then(function (ratings) {
          $scope.ratings = ratings;
          $scope.model.includedRatings = $scope.model.includedRatings.concat(selectedRatings);
        });
      });

      return modalInstance.opened;
    };

    $scope.addExistingTraining = function () {
      var modalInstance = namedModal.open('chooseTrainings', {
        includedTrainings: $scope.model.includedTrainings
      });

      modalInstance.result.then(function (selectedTrainings) {
        PersonDocumentTrainings.query({ id: $stateParams.id }).$promise.then(function (trainings) {
          $scope.trainings = trainings;
          $scope.model.includedTrainings = $scope.model.includedTrainings.concat(selectedTrainings);
        });
      });

      return modalInstance.opened;
    };

    $scope.addExistingCheck = function () {
      var modalInstance = namedModal.open('chooseChecks', {
        includedChecks: $scope.model.includedChecks
      });

      modalInstance.result.then(function (selectedChecks) {
        PersonDocumentChecks.query({ id: $stateParams.id }).$promise.then(function (checks) {
          $scope.checks = checks;
          $scope.model.includedChecks = $scope.model.includedChecks.concat(selectedChecks);
        });
      });

      return modalInstance.opened;
    };

    $scope.addExistingMedical = function () {
      var modalInstance = namedModal.open('chooseMedicals', {
        includedMedicals: $scope.model.includedMedicals
      });

      modalInstance.result.then(function (selectedMedicals) {
        PersonDocumentMedicals.query({ id: $stateParams.id }).$promise.then(function (medicals) {
          $scope.medicals = medicals;
          $scope.model.includedMedicals = $scope.model.includedMedicals.concat(selectedMedicals);
        });
      });

      return modalInstance.opened;
    };

    $scope.addExistingLicence = function () {
      var hideLicences = _.clone($scope.model.includedLicences);

      if ($stateParams.ind) {
        hideLicences.push(parseInt($stateParams.ind, 10));
      }

      var modalInstance = namedModal.open('chooseLicences', {
        includedLicences: hideLicences
      });

      modalInstance.result.then(function (selectedLicences) {
        PersonLicences.query({ id: $stateParams.id }).$promise.then(function (licences) {
          $scope.licences = licences;
          $scope.model.includedLicences = $scope.model.includedLicences.concat(selectedLicences);
        });
      });

      return modalInstance.opened;
    };

    $scope.removeRating = function (rating) {
      $scope.model.includedRatings =
        _.without($scope.model.includedRatings, rating.partIndex);
    };

    $scope.removeTraining = function (training) {
      $scope.model.includedTrainings =
        _.without($scope.model.includedTrainings, training.partIndex);
    };

    $scope.removeCheck = function (check) {
      $scope.model.includedChecks =
        _.without($scope.model.includedChecks, check.partIndex);
    };

    $scope.removeMedical = function (document) {
      $scope.model.includedMedicals =
        _.without($scope.model.includedMedicals, document.partIndex);
    };

    $scope.removeLicence = function (document) {
      $scope.model.includedLicences =
        _.without($scope.model.includedLicences, document.partIndex);
    };
  }

  PersonLicenceEditionCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'namedModal',
    '$q',
    'Persons',
    'PersonRatings',
    'PersonDocumentTrainings',
    'PersonDocumentChecks',
    'PersonDocumentMedicals',
    'PersonLicences'
  ];

  angular.module('gva').controller('PersonLicenceEditionCtrl', PersonLicenceEditionCtrl);
}(angular, _, moment));
