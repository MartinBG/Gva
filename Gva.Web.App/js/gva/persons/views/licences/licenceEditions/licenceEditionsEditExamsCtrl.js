/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditExamsCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicenceEditions,
    PersonDocumentTrainings,
    includedExams,
    currentLicenceEdition,
    licenceEditions,
    scMessage,
    scModal
  ) {
    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;
    $scope.currentLicenceEdition.part.includedExams =
      $scope.currentLicenceEdition.part.includedExams || [];
    $scope.includedExams = includedExams;

    $scope.addExam = function () {
      var modalInstance = scModal.open('newExam', {
        lotId: $stateParams.id,
        appId: $stateParams.appId,
        caseTypeId: $scope.currentLicenceEdition.cases[0].caseType.nomValueId
      });

      modalInstance.result.then(function (newExam) {
        $scope.currentLicenceEdition.part.includedExams.push(newExam.partIndex);
        $scope.save();
        PersonDocumentTrainings.getTrainingView({
          id: $stateParams.id,
          ind: newExam.partIndex
        }).$promise
          .then(function(exam) {
            $scope.includedExams.push(exam);
          });
      });

      return modalInstance.opened;
    };

    $scope.addExistingExam = function () {
      var modalInstance = scModal.open('chooseExams', {
        includedExams: $scope.currentLicenceEdition.part.includedExams,
        lotId: $stateParams.id,
        caseTypeId: $scope.currentLicenceEdition.cases[0].caseType.nomValueId
      });

      modalInstance.result.then(function (selectedExams) {
        $scope.includedExams = $scope.includedExams.concat(selectedExams);

        $scope.currentLicenceEdition.part.includedExams = 
          $scope.currentLicenceEdition.part.includedExams
          .concat(_.pluck(selectedExams, 'partIndex'));

        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.removeExam = function (exam) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedExams = _.without($scope.includedExams, exam);

            _.remove($scope.currentLicenceEdition.part.includedExams,
              function(includedExamsPartIndex) {
                return exam.partIndex === includedExamsPartIndex;
              });
            $scope.save();
          }
        });
    };

    $scope.changeOrder = function () {
      $scope.changeOrderMode = true;
    };

    $scope.saveOrder = function () {
      $scope.includedExams = _.sortBy($scope.includedExams, 'orderNum');
      $scope.changeOrderMode = false;
      $scope.currentLicenceEdition.part.includedExams =
        _.pluck($scope.includedExams, 'partIndex');
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

  LicenceEditionsEditExamsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonLicenceEditions',
    'PersonDocumentTrainings',
    'includedExams',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];

  LicenceEditionsEditExamsCtrl.$resolve = {
    includedExams: [
      '$stateParams',
      'PersonDocumentTrainings',
      'currentLicenceEdition',
      function ($stateParams, PersonDocumentTrainings, currentLicenceEdition) {
        return  PersonDocumentTrainings
          .getExams({ id: $stateParams.id })
          .$promise
          .then(function (exams) {
            return _.map(currentLicenceEdition.part.includedExams, function (partIndex) {
              return _.where(exams, { partIndex: partIndex })[0];
            });
          });
       }
    ]
  };

  angular.module('gva')
    .controller('LicenceEditionsEditExamsCtrl', LicenceEditionsEditExamsCtrl);
}(angular, _));
