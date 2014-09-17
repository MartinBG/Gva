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
      return !item.valid;
    };

    $scope.isExpiringLicence = function(item) {
      var today = moment(new Date()),
          difference = moment(item.dateValidTo).diff(today, 'days');

      return 0 <= difference && difference <= 30;
    };

    $scope.isExpiredLicence = function(item) {
      return moment(new Date()).isAfter(item.dateValidTo);
    };

    $scope.print = function (licence) {
      var params = {
        lotId: $stateParams.id,
        index: licence.partIndex,
        editionIndex: licence.editionPartIndex
      };

      var modalInstance = scModal.open('printLicence', params);

      modalInstance.result.then(function (savedLicence) {
        licence = savedLicence;
      });

      return modalInstance.opened;
    };

    $scope.licenceNumberFormatMask = function(item) {
      var licenceNumberMask = item.licenceNumber.toString(),
          licenceNumberLength = licenceNumberMask.length;

      if (licenceNumberLength < 5) {
        var i, difference = 5 - licenceNumberLength;
        for (i = 0; i < difference; i++) {
          licenceNumberMask = '0' + licenceNumberMask;
        }
      }

      return item.publisherCode + ' ' +
             item.licenceTypeCode + ' ' +
             licenceNumberMask;
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
