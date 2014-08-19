/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function LicencesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    licences
  ) {
    $scope.licences = licences;
    
    $scope.isInvalidLicence = function(item){
      return item.part.valid && item.part.valid.code === 'N';
    };

    $scope.isExpiredLicence = function(item) {
      var currentDate = new Date(),
        documentDateValidTo = $filter('last')(item.part.editions).documentDateValidTo;

      return moment(currentDate).isAfter(documentDateValidTo);
    };
  }

  LicencesSearchCtrl.$inject =
    ['$scope', '$state', '$stateParams', '$filter', 'licences'];

  LicencesSearchCtrl.$resolve = {
    licences: [
      '$stateParams',
      'PersonLicences',
      function ($stateParams, PersonLicences) {
        return PersonLicences.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('LicencesSearchCtrl', LicencesSearchCtrl);
}(angular, moment));
