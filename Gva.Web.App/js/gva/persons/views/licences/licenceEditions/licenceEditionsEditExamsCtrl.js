/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditExamsCtrl(
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

    $scope.currentLicenceEdition.part.includedExams =
      $scope.currentLicenceEdition.part.includedExams || [];
    PersonDocumentTrainings
      .getExams({ id: $stateParams.id })
      .$promise
      .then(function (exams) {
        $scope.includedExams = 
          _.map($scope.currentLicenceEdition.part.includedExams, function (partIndex) {
            return _.where(exams, { partIndex: partIndex })[0];
          });
      });

    $scope.addExam = function () {
      var modalInstance = scModal.open('newExam', {
        lotId: $stateParams.id,
        caseTypeId: $stateParams.caseTypeId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newExam) {
        $scope.includedExams.push(newExam);
        $scope.currentLicenceEdition.part.includedExams.push(newExam.partIndex);
        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.addExistingExam = function () {
      var modalInstance = scModal.open('chooseExams', {
        includedExams: $scope.currentLicenceEdition.part.includedExams,
        lotId: $stateParams.id
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
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];


  angular.module('gva')
    .controller('LicenceEditionsEditExamsCtrl', LicenceEditionsEditExamsCtrl);
}(angular, _));
