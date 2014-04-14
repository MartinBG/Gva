/*global angular, _, moment*/
(function (angular, _, moment) {
  'use strict';

  function PersonLicenceEditionCtrl(
    $scope,
    $state,
    $stateParams,
    $q,
    PersonRating,
    PersonDocumentTraining,
    PersonDocumentExam,
    PersonDocumentMedical,
    PersonLicence
  ) {

    $q.all([
      PersonRating.query({ id: $stateParams.id }).$promise,
      PersonDocumentTraining.query({ id: $stateParams.id }).$promise,
      PersonDocumentExam.query({ id: $stateParams.id }).$promise,
      PersonDocumentMedical.query({ id: $stateParams.id }).$promise,
      PersonLicence.query({ id: $stateParams.id }).$promise
    ]).then(function (results) {
      var ratings = results[0];
      var trainings = results[1];
      var exams = results[2];
      var medicals = results[3];
      var licences = results[4];

      $scope.$watch('model', function () {
        if (!$scope.model) {
          return;
        }

        $scope.model.includedRatings = $scope.model.includedRatings || [];
        $scope.model.includedTrainings = $scope.model.includedTrainings || [];
        $scope.model.includedExams = $scope.model.includedExams || [];
        $scope.model.includedMedicals = $scope.model.includedMedicals || [];
        $scope.model.includedLicences = $scope.model.includedLicences || [];

        // coming from a child state and carrying payload
        if ($state.previous && $state.previous.includes[$state.current.name] && $state.payload) {
          if ($state.payload.selectedMedicals) {
            [].push.apply($scope.model.includedMedicals, $state.payload.selectedMedicals);
          }

          if ($state.payload.selectedExams) {
            [].push.apply($scope.model.includedExams, $state.payload.selectedExams);
          }

          if ($state.payload.selectedTrainings) {
            [].push.apply($scope.model.includedTrainings, $state.payload.selectedTrainings);
          }

          if ($state.payload.selectedRatings) {
            [].push.apply($scope.model.includedRatings, $state.payload.selectedRatings);
          }

          if ($state.payload.selectedLicences) {
            [].push.apply($scope.model.includedLicences, $state.payload.selectedLicences);
          }
        }
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
            return _.find(ratings, { partIndex: ind });
          });
        });

        $scope.$watchCollection('model.includedTrainings', function () {
          if (!$scope.model) {
            return;
          }

          $scope.includedTrainings = _.map($scope.model.includedTrainings, function (ind) {
            return _.find(trainings, { partIndex: ind });
          });
        });

        $scope.$watchCollection('model.includedExams', function () {
          if (!$scope.model) {
            return;
          }

          $scope.includedExams = _.map($scope.model.includedExams, function (ind) {
            return _.where(exams, { partIndex: ind })[0];
          });
        });

        $scope.$watchCollection('model.includedMedicals', function () {
          if (!$scope.model) {
            return;
          }

          $scope.includedMedicals = _.map($scope.model.includedMedicals, function (ind) {
            return _.find(medicals, { partIndex: ind });
          });
        });

        $scope.$watchCollection('model.includedLicences', function () {
          if (!$scope.model) {
            return;
          }

          $scope.includedLicences = _.map($scope.model.includedLicences, function (ind) {
            return _.find(licences, { partIndex: ind });
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
      return $state.go('.newRating');
    };

    $scope.addTraining = function () {
      return $state.go('.newTraining');
    };

    $scope.addExam = function () {
      return $state.go('.newExam');
    };

    $scope.addMedical = function () {
      return $state.go('.newMedical');
    };

    $scope.addExistingRating = function () {
      return $state.go('.chooseRating', {}, {}, {
        selectedRatings: $scope.model.includedRatings
      });
    };

    $scope.addExistingTraining = function () {
      return $state.go('.chooseTraining', {}, {}, {
        selectedTrainings: $scope.model.includedTrainings
      });
    };

    $scope.addExistingExam = function () {
      return $state.go('.chooseExam', {}, {}, {
        selectedExams: $scope.model.includedExams
      });
    };

    $scope.addExistingMedical = function () {
      return $state.go('.chooseMedical', {}, {}, {
        selectedMedicals: $scope.model.includedMedicals
      });
    };

    $scope.addExistingLicence = function () {
      return $state.go('.chooseLicence', {}, {}, {
        selectedLicences: $scope.model.includedLicences
      });
    };

    $scope.removeRating = function (rating) {
      $scope.model.includedRatings =
        _.without($scope.model.includedRatings, rating.partIndex);
    };

    $scope.removeTraining = function (training) {
      $scope.model.includedTrainings =
        _.without($scope.model.includedTrainings, training.partIndex);
    };

    $scope.removeExam = function (exam) {
      $scope.model.includedExams =
        _.without($scope.model.includedExams, exam.partIndex);
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
    '$q',
    'PersonRating',
    'PersonDocumentTraining',
    'PersonDocumentExam',
    'PersonDocumentMedical',
    'PersonLicence'
  ];

  angular.module('gva').controller('PersonLicenceEditionCtrl', PersonLicenceEditionCtrl);
}(angular, _, moment));
