/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function LicencesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    $filter,
    scModal,
    licences
  ) {
    $scope.licences = licences;
    
    $scope.isInvalidLicence = function(item){
      return item.part.valid && item.part.valid.code === 'N';
    };

    $scope.isExpiringLicence = function(item) {
      var today = moment(new Date()),
        documentDateValidTo = $filter('last')(item.part.editions).documentDateValidTo,
        difference = moment(documentDateValidTo).diff(today, 'days');

      return 0 <= difference && difference <= 30;
    };

    $scope.isExpiredLicence = function(item) {
      var currentDate = new Date(),
        documentDateValidTo = $filter('last')(item.part.editions).documentDateValidTo;

      return moment(currentDate).isAfter(documentDateValidTo);
    };

    $scope.print = function (licence) {
      var params = {
        lotId: $stateParams.id,
        index: licence.partIndex
      };

      var modalInstance = scModal.open('printLicence', params);

      modalInstance.result.then(function (savedLicence) {
        licence = savedLicence;
      });

      return modalInstance.opened;
    };
  }

  LicencesSearchCtrl.$inject =
    ['$scope', '$state', '$stateParams', '$filter', 'scModal','licences'];

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
