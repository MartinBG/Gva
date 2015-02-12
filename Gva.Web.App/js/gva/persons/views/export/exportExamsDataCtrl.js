/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ExportExamsDataCtrl(
    $scope,
    scModal
  ) {
    $scope.examsForExport = [];

    $scope.addExams = function () {
      var modalInstance = scModal.open('chooseAppExams',
        { includedExams: $scope.examsForExport });

      modalInstance.result.then(function (exams) {
        $scope.examsForExport = $scope.examsForExport.concat(exams);
        
      });

      return modalInstance.opened;
    };

    $scope.removeExam = function (exam) {
      $scope.examsForExport = _.without($scope.examsForExport, exam);
    };

    $scope.$watch('examsForExport', function() {
        $scope.examsData = _.map($scope.examsForExport, function(exam) {
          return {
            lotId: exam.lotId,
            appPartId: exam.appPartId,
            certCampCode: exam.certCampCode,
            examCode: exam.examCode
          };
        });
      });
  }

  ExportExamsDataCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva').controller('ExportExamsDataCtrl', ExportExamsDataCtrl);
}(angular, _));
