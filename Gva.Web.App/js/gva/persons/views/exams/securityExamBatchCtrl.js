/*global angular, _*/
(function (angular, _) {
  'use strict';

  function SecurityExamBatchCtrl(
    $scope,
    $state,
    SecurityExam,
    PersonExams,
    personExam,
    pageModel
  ) {
    $scope.pageModel = pageModel;
    $scope.commonData = {};
    $scope.personExams = [];

    $scope.setCurrentPage = function (page) {
      if (!page || page <= 0 || page > $scope.pageModel.pageCount ||
        page === $scope.pageModel.currentPage) {
        return;
      }
      $scope.btnClicked = true;
      $scope.pageModel.currentPage = parseInt(page, 10);

      if ($scope.personExams[page - 1].formReadonly) {
        return PersonExams.get({
          id: $scope.personExams[page - 1].lot.id,
          ind: $scope.personExams[page - 1].partIndex
        }).$promise.then(function (data) {
          return $state.go('root.persons.securityExam.part', { id: page }).then(function () {
            $scope.setPaging(page);
            $scope.personExams[page - 1].part = data.part;
            $scope.personExams[page - 1].files = data.files;
            $scope.btnClicked = false;
          });
        });
      }

      return SecurityExam.getPreview({ gvaFileId: $scope.gvaFileIds[page - 1] }, {}).$promise
        .then(function (data) {
          return $state.go('root.persons.securityExam.part', { id: page }).then(function () {
            $scope.setPaging(page);
            $scope.personExams[page - 1].files[0].file = data.gvaFile;
            $scope.personExams[page - 1].files[0].isAdded = true;
            $scope.btnClicked = false;
          });
        });
    };

    $scope.setPaging = function (page) {
      $scope.pageModel.pageable = true;

      var i, l, pageCount = $scope.pageModel.pageCount;
      if (pageCount <= $scope.pageModel.numOfPageButtons) {
        $scope.pageModel.pagingContents = _.range(1, pageCount + 1);
      }
      else {
        if (page < $scope.pageModel.numOfPageButtons - 2) {
          $scope.pageModel.pagingContents = _.range(1, $scope.pageModel.numOfPageButtons - 1);
          $scope.pageModel.pagingContents.push(null);
          $scope.pageModel.pagingContents.push(pageCount);
        }
        else if (page > pageCount - $scope.pageModel.numOfPageButtons + 3) {
          $scope.pageModel.pagingContents = [];
          $scope.pageModel.pagingContents.push(1);
          $scope.pageModel.pagingContents.push(null);
          for (i = pageCount - $scope.pageModel.numOfPageButtons + 3, l = pageCount; i <= l; i++) {
            $scope.pageModel.pagingContents.push(i);
          }
        }
        else {
          $scope.pageModel.pagingContents = [];
          $scope.pageModel.pagingContents.push(1);
          $scope.pageModel.pagingContents.push(null);
          for (i = page - 1, l = page + 2; i < l; i++) {
            $scope.pageModel.pagingContents.push(i);
          }
          $scope.pageModel.pagingContents.push(undefined);
          $scope.pageModel.pagingContents.push(pageCount);
        }
      }
    };

    $scope.extractPages = function () {
      return $scope.fileForm.$validate().then(function () {
        if ($scope.fileForm.$valid) {
          return SecurityExam.extractPages({
            fileKey: $scope.file.key,
            name: $scope.file.name
          }, {}).$promise.then(function (data) {
            var i;

            if (!!data.pageCount) {
              $scope.personExams = [];
              for (i = 0; i < data.pageCount; i++) {
                $scope.personExams.push(_.cloneDeep(personExam));
              }

              $scope.gvaFileIds = data.gvaFileIds;
              $scope.pageModel.pageCount = data.pageCount;

              return $scope.setCurrentPage(1);
            }
          });
        }
      });
    };
  }

  SecurityExamBatchCtrl.$inject = [
    '$scope',
    '$state',
    'SecurityExam',
    'PersonExams',
    'personExam',
    'pageModel'
  ];

  SecurityExamBatchCtrl.$resolve = {
    personExam: [
      '$stateParams',
      'PersonExams',
      function ($stateParams, PersonExams) {
        return PersonExams.newExam().$promise.then(function (exam) {
          exam.formReadonly = false;
          exam.lot = {};

          return exam;
        });
      }
    ],
    pageModel: [
      'l10n',
      function (l10n) {
        return {
          currentPage: null,
          pageCount: 1,
          pagingContents: [],
          numOfPageButtons: 7,
          pageable: false,
          pageTexts: {
            previousPage: l10n.get('scaffolding.scDatatable.previousPage'),
            nextPage: l10n.get('scaffolding.scDatatable.nextPage')
          }
        };
      }
    ]
  };

  angular.module('gva').controller('SecurityExamBatchCtrl', SecurityExamBatchCtrl);
}(angular, _));
