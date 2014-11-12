/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function DocumentTrainingsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    trainings
  ) {
    $scope.documentTrainings = trainings;

    $scope.isInvalidLicence = function(item){
      return item.part.valid.code === 'N';
    };

    $scope.isExpiringDocument = function(item) {
      var today = moment(new Date()),
          difference = moment(item.part.documentDateValidTo).diff(today, 'days');

      return 0 <= difference && difference <= 30;
    };

    $scope.isExpiredDocument = function(item) {
      return moment(new Date()).isAfter(item.part.documentDateValidTo);
    };
  }

  DocumentTrainingsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'trainings'
  ];

  DocumentTrainingsSearchCtrl.$resolve = {
    trainings: [
      '$stateParams',
      'PersonDocumentTrainings',
      function ($stateParams, PersonDocumentTrainings) {
        return PersonDocumentTrainings.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentTrainingsSearchCtrl', DocumentTrainingsSearchCtrl);
}(angular, moment));
