/*global angular, _, moment*/
(function (angular, _, moment) {
  'use strict';

  function PersonLicenceEditionCtrl(
    $scope,
    scModal,
    $q,
    scMessage,
    Persons,
    PersonRatings,
    PersonDocumentLangCerts,
    PersonDocumentTrainings,
    PersonDocumentChecks,
    PersonDocumentMedicals,
    PersonLicences,
    scFormParams
  ) {
    $scope.lotId = scFormParams.lotId;
    $scope.isNew = scFormParams.isNew;

    $scope.notesShouldBeFilled = function () {
      if(!$scope.model.part.notes && $scope.model.part.notesAlt) {
        return false;
      }
      return true;
    };

    $scope.notesAltShouldBeFilled = function () {
      if($scope.model.part.notes && !$scope.model.part.notesAlt) {
        return false;
      }
      return true;
    };

    $q.all([
      Persons.get({ id: scFormParams.lotId }).$promise,
      PersonRatings.getRatingsWithAllEditions({ id: scFormParams.lotId }).$promise,
      PersonDocumentLangCerts.query({ id: scFormParams.lotId }).$promise,
      PersonDocumentTrainings.getExams({ id: scFormParams.lotId }).$promise,
      PersonDocumentTrainings.query({ id: scFormParams.lotId }).$promise,
      PersonDocumentChecks.query({ id: scFormParams.lotId }).$promise,
      PersonDocumentMedicals.query({ id: scFormParams.lotId }).$promise,
      PersonLicences.query({ id: scFormParams.lotId }).$promise
    ]).then(function (results) {
      $scope.person = results[0];
      var ratings = results[1],
          langCerts = results[2],
          exams = results[3],
          trainings = results[4],
          checks = results[5],
          medicals = results[6],
          licences = results[7];

      var unbindWatcher = $scope.$watch('model', function () {
        if (!$scope.model) {
          return;
        }

        $scope.model.part.includedRatings = $scope.model.part.includedRatings || [];
        $scope.includedRatings = _.map($scope.model.part.includedRatings, function (rating) {
          return _.find(ratings, { ratingPartIndex: rating.ind,  editionPartIndex: rating.index });
        });
        $scope.$watchCollection('includedRatings', function () {
          $scope.model.part.includedRatings = _.map($scope.includedRatings, function (rating) {
            return {
              ind: rating.ratingPartIndex,
              index: rating.editionPartIndex
            };
          });
        });

        $scope.model.part.includedLangCerts = $scope.model.part.includedLangCerts || [];
        $scope.includedLangCerts = _.map($scope.model.part.includedLangCerts, function (ind) {
          return _.find(langCerts, { partIndex: ind });
        });
        $scope.$watchCollection('includedLangCerts', function () {
          $scope.model.part.includedLangCerts = _.pluck($scope.includedLangCerts, 'partIndex');
        });

        $scope.model.part.includedExams = $scope.model.part.includedExams || [];
        $scope.includedExams = _.map($scope.model.part.includedExams, function (ind) {
          return _.find(exams, { partIndex: ind });
        });
        $scope.$watchCollection('includedExams', function () {
          $scope.model.part.includedExams = _.pluck($scope.includedExams, 'partIndex');
        });

        $scope.model.part.includedTrainings = $scope.model.part.includedTrainings || [];
        $scope.includedTrainings = _.map($scope.model.part.includedTrainings, function (ind) {
          return _.find(trainings, { partIndex: ind });
        });
        $scope.$watchCollection('includedTrainings', function () {
          $scope.model.part.includedTrainings = _.pluck($scope.includedTrainings, 'partIndex');
        });

        $scope.model.part.includedChecks = $scope.model.part.includedChecks || [];
        $scope.includedChecks = _.map($scope.model.part.includedChecks, function (ind) {
          return _.where(checks, { partIndex: ind })[0];
        });
        $scope.$watchCollection('includedChecks', function () {
          $scope.model.part.includedChecks = _.pluck($scope.includedChecks, 'partIndex');
        });

        $scope.model.part.includedMedicals = $scope.model.part.includedMedicals || [];
        $scope.includedMedicals = _.map($scope.model.part.includedMedicals, function (ind) {
          return _.find(medicals, { partIndex: ind });
        });
        $scope.$watchCollection('includedMedicals', function () {
          $scope.model.part.includedMedicals = _.pluck($scope.includedMedicals, 'partIndex');
        });

        $scope.model.part.includedLicences = $scope.model.part.includedLicences || [];
        $scope.includedLicences = _.map($scope.model.part.includedLicences, function (ind) {
          return _.find(licences, { partIndex: ind });
        });
        $scope.$watchCollection('includedLicences', function () {
          $scope.model.part.includedLicences = _.pluck($scope.includedLicences, 'partIndex');
        });

        // removing the watcher after the model have been set
        unbindWatcher();
      });
    });

    if($scope.isNew && $scope.model.part.documentDateValidFrom === undefined){
      $scope.model.part.documentDateValidFrom = moment(new Date());
    }

    $scope.addRating = function () {
      var modalInstance = scModal.open('newRating', {
        lotId: scFormParams.lotId,
        appId: scFormParams.appId
      });

      modalInstance.result.then(function (newRating) {
        PersonRatings.query({ id: scFormParams.lotId })
          .$promise
          .then(function (ratings) {
            var rating = null;
            _.find(ratings, function (r) {
              if (r.partIndex === newRating.rating.partIndex) {
                rating = r;
              }
            });
            $scope.includedRatings.push(rating);
          });
        
      });

      return modalInstance.opened;
    };

    $scope.addExistingRating = function () {
      var modalInstance = scModal.open('chooseRatings', {
        includedRatings: $scope.model.part.includedRatings,
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

    $scope.addLangCert = function () {
      var modalInstance = scModal.open('newLangCert', {
        lotId: scFormParams.lotId,
        caseTypeId: scFormParams.caseTypeId,
        appId: scFormParams.appId
      });

      modalInstance.result.then(function (newLangCert) {
        PersonDocumentLangCerts.query({
          id: scFormParams.lotId
        }).$promise.then(function (langCerts) {
          var langCert = null;
          _.find(langCerts, function (lc) {
            if (lc.partIndex === newLangCert.partIndex) {
              langCert = lc;
            }
          });
          $scope.includedLangCerts.push(langCert);
        });

      });

      return modalInstance.opened;
    };

    $scope.addExistingLangCert = function () {
      var modalInstance = scModal.open('chooseLangCerts', {
        includedLangCerts: $scope.model.part.includedLangCerts,
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedLangCerts) {
        $scope.includedLangCerts = $scope.includedLangCerts.concat(selectedLangCerts);
      });

      return modalInstance.opened;
    };

    $scope.removeLangCert = function (langCert) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedLangCerts = _.without($scope.includedLangCerts, langCert);
          }
        });
    };

    $scope.addExam = function () {
      var modalInstance = scModal.open('newExam', {
        lotId: scFormParams.lotId,
        caseTypeId: scFormParams.caseTypeId,
        appId: scFormParams.appId
      });

      modalInstance.result.then(function (newExam) {
        $scope.includedExams.push(newExam);
      });

      return modalInstance.opened;
    };

    $scope.addExistingExam = function () {
      var modalInstance = scModal.open('chooseExams', {
        includedExams: $scope.model.part.includedExams,
        lotId: scFormParams.lotId
      });

      modalInstance.result.then(function (selectedExams) {
        $scope.includedExams = $scope.includedExams.concat(selectedExams);
      });

      return modalInstance.opened;
    };

    $scope.removeExam = function (exam) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedExams = _.without($scope.includedExams, exam);
          }
        });
    };

    $scope.addTraining = function () {
      var modalInstance = scModal.open('newTraining', {
        lotId: scFormParams.lotId,
        caseTypeId: scFormParams.caseTypeId,
        appId: scFormParams.appId
      });

      modalInstance.result.then(function (newTraining) {
        $scope.includedTrainings.push(newTraining);
      });

      return modalInstance.opened;
    };

    $scope.addExistingTraining = function () {
      var modalInstance = scModal.open('chooseTrainings', {
        includedTrainings: $scope.model.part.includedTrainings,
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
        caseTypeId: scFormParams.caseTypeId,
        appId: scFormParams.appId
      });

      modalInstance.result.then(function (newCheck) {
        $scope.includedChecks.push(newCheck);
      });

      return modalInstance.opened;
    };

    $scope.addExistingCheck = function () {
      var modalInstance = scModal.open('chooseChecks', {
        includedChecks: $scope.model.part.includedChecks,
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
        caseTypeId: scFormParams.caseTypeId,
        appId: scFormParams.appId
      });

      modalInstance.result.then(function (newMedical) {
        $scope.includedMedicals.push(newMedical);
      });

      return modalInstance.opened;
    };

    $scope.addExistingMedical = function () {
      var modalInstance = scModal.open('chooseMedicals', {
        includedMedicals: $scope.model.part.includedMedicals,
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
      var hideLicences = _.clone($scope.model.part.includedLicences);

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
    'PersonDocumentLangCerts',
    'PersonDocumentTrainings',
    'PersonDocumentChecks',
    'PersonDocumentMedicals',
    'PersonLicences',
    'scFormParams'
  ];

  angular.module('gva').controller('PersonLicenceEditionCtrl', PersonLicenceEditionCtrl);
}(angular, _, moment));
