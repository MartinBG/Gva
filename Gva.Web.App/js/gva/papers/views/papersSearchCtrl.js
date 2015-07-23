/*global angular*/
(function (angular) {
  'use strict';

  function PapersSearchCtrl(
    $scope,
    papers
  ) {
    $scope.papers = papers;
  }

  PapersSearchCtrl.$inject = [
    '$scope',
    'papers'
  ];

  PapersSearchCtrl.$resolve = {
    papers: [
      'Papers',
      function (Papers) {
        return Papers.getPapers().$promise;
      }
    ]
  };

  angular.module('gva').controller('PapersSearchCtrl', PapersSearchCtrl);
}(angular));