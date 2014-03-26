/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChooseAuditsCtrl(
    $state,
    $stateParams,
    $scope,
    OrganizationInspection,
    organizationRecommendation,
    availableAudits,
    usedAudits) {
    $scope.availableAudits = availableAudits;
    $scope.usedAudits = usedAudits;

    $scope.addAudits = function () {
      organizationRecommendation.part.includedAudits
        .splice(0, organizationRecommendation.part.includedAudits.length);

      _.each(_.filter($scope.availableAudits, { 'checked': true }), function (audit) {
        organizationRecommendation.part.includedAudits.push(audit.partIndex);
      });
      return $state.go('^');
    };

    $scope.removeAudit = function (audit) {
      organizationRecommendation.part.includedAudits =
        _.without(organizationRecommendation.part.includedAudits, audit.partIndex);
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

  }

  ChooseAuditsCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'OrganizationInspection',
    'organizationRecommendation',
    'availableAudits',
    'usedAudits'
  ];

  ChooseAuditsCtrl.$resolve = {
    availableAudits: [
      '$stateParams',
      'OrganizationInspection',
      'organizationRecommendation',
      function ($stateParams, OrganizationInspection, organizationRecommendation) {
        return OrganizationInspection.query({ id: $stateParams.id })
            .$promise.then(function (availableAudits) {
              return _.map(availableAudits, function (availableAudit) {
                if (_.contains(organizationRecommendation.part.includedAudits,
                  availableAudit.partIndex)) {
                  availableAudit.checked = true;
                }
                return availableAudit;
              });
            });
      }
    ],
    usedAudits: [
      '$stateParams',
      'OrganizationInspection',
      'organizationRecommendation',
      function ($stateParams, OrganizationInspection, organizationRecommendation) {
        return OrganizationInspection.query({ id: $stateParams.id })
            .$promise.then(function (availableAudits) {
              return _.filter(availableAudits, function (availableAudit) {
                return _.contains(organizationRecommendation.part.includedAudits,
                  availableAudit.partIndex);
              });
            });
      }
    ]
  };

  angular.module('gva').controller('ChooseAuditsCtrl', ChooseAuditsCtrl);
}(angular, _));
