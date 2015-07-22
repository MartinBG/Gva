/*global angular*/
(function (angular) {
  'use strict';

  function PapersNewCtrl(
    $scope,
    $state,
    Papers,
    paper
  ) {
    $scope.paper = paper;
    $scope.save = function () {
      return $scope.newPapersForm.$validate()
        .then(function () {
          if ($scope.newPapersForm.$valid) {
            return Papers
              .createNew($scope.paper)
              .$promise
              .then(function () {
                return $state.go('root.papers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.papers.search');
    };
  }

  PapersNewCtrl.$inject = [
    '$scope',
    '$state',
    'Papers',
    'paper'
  ];

  PapersNewCtrl.$resolve = {
    paper: [
      'Papers',
      function (Papers) {
        return Papers.newPaper().$promise;
      }
    ]
  };

  angular.module('gva').controller('PapersNewCtrl', PapersNewCtrl);
}(angular));
