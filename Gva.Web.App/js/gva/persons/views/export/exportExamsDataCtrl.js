/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ExportExamsDataCtrl(
    $scope,
    scModal,
    Applications
  ) {
     Applications.getExams().$promise.then(function(result)
    {
      $scope.examsForExport = result;
    });

    $scope.addExams = function () {
      var modalInstance = scModal.open('chooseApplicationExams',
        { includedExams: $scope.examsForExport });

      modalInstance.result.then(function (exams) {
        $scope.examsForExport = $scope.examsForExport.concat(exams);
      });

      return modalInstance.opened;
    };

    $scope.removeExam = function (exam) {
      $scope.examsForExport = _.without($scope.examsForExport, exam);
    };
  }

  ExportExamsDataCtrl.$inject = ['$scope', 'scModal', 'Applications'];

  angular.module('gva').controller('ExportExamsDataCtrl', ExportExamsDataCtrl);
}(angular, _));
