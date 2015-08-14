/*global angular*/
(function (angular) {
  'use strict';

  function DocApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    personDocumentApplications
  ) {
    $scope.personDocumentApplications = personDocumentApplications;
    $scope.lotId = $stateParams.id;

    $scope.isDeclinedApp = function(item) {
      if (item.stage) {
        return item.stage.alias === 'declined';
      }

      return false;
    };

    $scope.isDoneApp = function(item) {
      if (item.stage) {
        return item.stage.alias === 'done';
      }

      return false;
    };
  }

  DocApplicationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'personDocumentApplications'
  ];

  DocApplicationsSearchCtrl.$resolve = {
    personDocumentApplications: [
      '$stateParams',
      'PersonDocumentApplications',
      function ($stateParams, PersonDocumentApplications) {
        return PersonDocumentApplications.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocApplicationsSearchCtrl', DocApplicationsSearchCtrl);
}(angular));
