﻿/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function LicencesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    PersonLicence,
    licences
  ) {
    $scope.licences = licences;
    
    $scope.isInvalidLicence = function(item){
      return item.part.valid.code === 'N';
    };
    $scope.isExpiredLicence = function(item) {
      var currentDate = new Date(),
        documentDateValidTo = $filter('last')(item.part.editions).documentDateValidTo;

      return moment(currentDate).isAfter(documentDateValidTo);
    };

    $scope.viewLicence = function (licence) {
      return $state.go('root.persons.view.licences.edit', {
        id: $stateParams.id,
        ind: licence.partIndex
      });
    };

    $scope.newLicence = function () {
      return $state.go('root.persons.view.licences.new');
    };
  }

  LicencesSearchCtrl.$inject =
    ['$scope', '$state', '$stateParams', '$filter', 'PersonLicence', 'licences'];

  LicencesSearchCtrl.$resolve = {
    licences: [
      '$stateParams',
      'PersonLicence',
      function ($stateParams, PersonLicence) {
        return PersonLicence.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicencesSearchCtrl', LicencesSearchCtrl);
}(angular, moment));