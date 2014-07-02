/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseLicenceCtrl(
    $state,
    $stateParams,
    $scope,
    licences
  ) {
    if (!($state.payload && $state.payload.selectedLicences)) {
      $state.go('^');
      return;
    }

    $scope.selectedLicences = [];

    $scope.licences = _.filter(licences, function (licence) {
      return !_.contains($state.payload.selectedLicences, licence.partIndex);
    });

    $scope.addLicences = function () {
      return $state.go('^', {}, {}, {
        selectedLicences: _.pluck($scope.selectedLicences, 'partIndex')
      });
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

    $scope.selectLicence = function (event, licence) {
      if ($(event.target).is(':checked')) {
        $scope.selectedLicences.push(licence);
      }
      else {
        $scope.selectedLicences = _.without($scope.selectedLicences, licence);
      }
    };
  }

  ChooseLicenceCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'licences'
  ];

  ChooseLicenceCtrl.$resolve = {
    licences: [
      '$stateParams',
      'PersonLicences',
      function ($stateParams, PersonLicences) {
        return PersonLicences.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseLicenceCtrl', ChooseLicenceCtrl);
}(angular, _, $));
