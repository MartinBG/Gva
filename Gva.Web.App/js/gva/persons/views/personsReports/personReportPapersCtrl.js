/*global angular, _*/
(function (angular, _) {
  'use strict';
  function PersonReportPapersCtrl(
    $scope,
    $state,
    $stateParams,
    PersonsReports,
    papers
  ) {
    $scope.filters = {
      paperId: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.papers = papers;

    $scope.search = function () {
      return $state.go('root.personsReports.papers', {
        paperId: $scope.filters.paperId
      });
    };
  }

  PersonReportPapersCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonsReports',
    'papers'
  ];

  PersonReportPapersCtrl.$resolve = {
    papers: [
      '$stateParams',
      'PersonsReports',
      function ($stateParams, PersonsReports) {
        return PersonsReports.getPapers($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonReportPapersCtrl', PersonReportPapersCtrl);
}(angular, _));
