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

    PersonDocumentTrainings
      .getExams({ id: $stateParams.id })
      .$promise
      .then(function (exams) {
        $scope.includedExams = 
          _.map($scope.currentLicenceEdition.part.includedExams, function (exam) {
            var includedExam = _.where(exams, { partIndex: exam.partIndex })[0];
            includedExam.orderNum = exam.orderNum;
            return includedExam;
          });

        $scope.includedExams = _.sortBy($scope.includedExams, 'orderNum');
      });

    $scope.addExam = function () {
      var modalInstance = scModal.open('newExam', {
        lotId: $stateParams.id,
        caseTypeId: $stateParams.caseTypeId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newExam) {
        var lastOrderNum = 0,
          lastExam = _.last($scope.includedExams);
        if (lastExam) {
          lastOrderNum = _.last($scope.includedExams).orderNum;
        }

        newExam.orderNum = ++lastOrderNum;
        $scope.includedExams.push(newExam);

        $scope.currentLicenceEdition.part.includedExams =
          _.map($scope.includedExams, function(exam) {
            return {
              orderNum: exam.orderNum,
              partIndex: exam.partIndex
            };
          });
        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.addExistingExam = function () {
      var modalInstance = scModal.open('chooseExams', {
        includedExams: _.pluck($scope.currentLicenceEdition.part.includedExams, 'partIndex'),
        lotId: $stateParams.id
      });

      modalInstance.result.then(function (selectedExams) {
        var lastOrderNum = 0,
          lastExam = _.last($scope.includedExams);
        if (lastExam) {
          lastOrderNum = _.last($scope.includedExams).orderNum;
        }

        _.forEach(selectedExams, function(exam) {
          var newlyAddedExam = {
            orderNum: ++lastOrderNum,
            partIndex: exam.partIndex
          };
          $scope.currentLicenceEdition.part.includedExams.push(newlyAddedExam);

          exam.orderNum = newlyAddedExam.orderNum;
          $scope.includedExams.push(exam);
        });

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
              function(includedExam) {
                return exam.partIndex === includedExam.partIndex;
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
      $scope.currentLicenceEdition.part.includedExams = [];
      _.forEach($scope.includedExams, function (exam) {
        var changedExam = {
          orderNum: exam.orderNum,
          partIndex: exam.partIndex
        };
        $scope.currentLicenceEdition.part.includedExams.push(changedExam);
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
