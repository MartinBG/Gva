/*global angular, _*/
(function (angular, _) {
  'use strict';

  function SecurityExamBatchPartCtrl(
    $scope,
    $state,
    PersonExams,
    pageModel
  ) {
    if (!$scope.file) {
      return $state.go('root.persons.securityExam');
    }

    $scope.exams = null;
    $scope.pageModel = pageModel;
    $scope.personExam = $scope.personExams[$scope.pageModel.currentPage - 1];

    if (!$scope.personExam.formReadonly) {
      $scope.personExam.part.examDate = $scope.commonData.examDate;
      $scope.personExam.part.commonQuestion = $scope.commonData.commonQuestion;
      $scope.personExam.part.specializedQuestion = $scope.commonData.specializedQuestion;
      $scope.personExam.part.inspectors = $scope.commonData.inspectors;
      $scope.personExam.cases[0].caseType = $scope.commonData.caseType;
    }

    $scope.$watch('personExam.lot.id', function (newValue, oldValue) {
      if ((newValue !== oldValue) || (newValue === oldValue)) {
        if (!!newValue) {
          return PersonExams.query({ id: $scope.personExam.lot.id }).$promise
            .then(function (data) {
              $scope.exams = _.first(_.sortBy(data, 'partIndex').reverse(), 2);
            });
        }
        else {
          $scope.exams = null;
        }

      }
    });
    $scope.$watch('personExam.part.examDate', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        $scope.commonData.examDate = newValue;
      }
    });
    $scope.$watch('personExam.part.commonQuestion', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        $scope.commonData.commonQuestion = newValue;
      }
    });
    $scope.$watch('personExam.part.specializedQuestion', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        $scope.commonData.specializedQuestion = newValue;
      }
    });
    $scope.$watch('personExam.part.inspectors', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        $scope.commonData.inspectors = newValue;
      }
    });
    $scope.$watch('personExam.cases[0].caseType', function (newValue, oldValue) {
      if (((newValue !== oldValue) || (newValue === oldValue)) && !!newValue) {
        $scope.commonData.caseType = newValue;
      }
    });

    $scope.save = function () {
      return $scope.newExamBatchForm.$validate().then(function () {
        if ($scope.newExamBatchForm.$valid) {
          var exam = {
            part: $scope.personExam.part,
            cases: $scope.personExam.cases
          };

          return PersonExams.save({
            id: $scope.personExam.lot.id,
            ind: $scope.personExam.partIndex
          }, exam).$promise.then(function (data) {
            if (data.partIndex !== null || data.partIndex !== undefined) {
              $scope.personExam.partIndex = data.partIndex;
            }

            if ($scope.pageModel.currentPage + 1 > $scope.pageModel.pageCount) {
              $scope.personExam.formReadonly = true;
              return;
            }

            return $scope.setCurrentPage($scope.pageModel.currentPage + 1).then(function () {
              $scope.personExam.formReadonly = true;
            });
          });
        }
      });
    };

    $scope.edit = function () {
      $scope.personExam.formReadonly = false;
    };
  }

  SecurityExamBatchPartCtrl.$inject = [
    '$scope',
    '$state',
    'PersonExams',
    'pageModel'
  ];

  angular.module('gva').controller('SecurityExamBatchPartCtrl', SecurityExamBatchPartCtrl);
}(angular, _));
