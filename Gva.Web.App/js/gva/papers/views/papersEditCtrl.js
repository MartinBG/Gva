/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PapersEditCtrl(
    $scope,
    $state,
    $stateParams,
    Papers,
    paper,
    scMessage
  ) {
    var originalPaper = _.cloneDeep(paper);

    $scope.paper = paper;
    $scope.editMode = null;
    $scope.paperId = $stateParams.id;
    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.paper = _.cloneDeep(originalPaper);
    };

    $scope.save = function () {
      return $scope.papersEditForm.$validate()
        .then(function () {
          if ($scope.papersEditForm.$valid) {
            return Papers
              .save({ paperId: $scope.paperId }, $scope.paper)
              .$promise
              .then(function () {
                return $state.go('root.papers.search');
              });
          }
        });
    };

    $scope.deletePaper = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return Papers.remove({
            paperId: $scope.paperId
          }).$promise.then(function () {
            return $state.go('root.papers.search');
          });
        }
      });
    };
  }

  PapersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Papers',
    'paper',
    'scMessage'
  ];

  PapersEditCtrl.$resolve = {
    paper: [
      '$stateParams',
      'Papers',
      function ($stateParams, Papers) {
        return Papers.get({
          paperId: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('PapersEditCtrl', PapersEditCtrl);
}(angular, _));
