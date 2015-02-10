/*global angular*/
(function (angular) {
  'use strict';

  function CertCampaignsCtrl(
    $scope,
    certCampaigns
  ) {
    $scope.certCampaigns = certCampaigns;
  }

  CertCampaignsCtrl.$inject = [
    '$scope',
    'certCampaigns'
  ];

  CertCampaignsCtrl.$resolve = {
    certCampaigns: [
      'ExaminationSystem',
      function (ExaminationSystem) {
        return ExaminationSystem.getCertCampaigns().$promise;
      }
    ]
  };

  angular.module('gva').controller('CertCampaignsCtrl', CertCampaignsCtrl);
}(angular));